using Dapper;
using LeasedCarService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeasedCarService.Repository
{
    public class WorkingCarListDapperRepository<T> where T : class
    {
        private IConfiguration _config;

        public IEnumerable<T> GetData(IConfiguration config, string ID, string sToday, string sPreMon)
        {
            this._config = config;
            var cn = new SqlConnection(_config.GetValue<string>("ConnectionStrings:CarList"));
            IEnumerable<T> list = cn.Query<T>(
                        @"select Distinct 
	                                CarStatus=Convert(int,Case when (@Today between c.DateB And c.DateE) And c.Status=0 then 0 else 1 end)
	                                ,LicensePlate = r.LicensePlate
	                                ,DriverID = d.IdNum
	                                ,DriverName = d.Name
	                                ,DriverPhone = d.ContactPhone
	                                ,PrincipalName = cust.Contact
	                                ,PrincipalPhone = Case when Replace(isnull(cust.ContactPhone, ''), ' ', '') = '' then Replace(isnull(cust.ContactPhone2, ''), ' ', '') else Replace(isnull(cust.ContactPhone, ''), ' ', '') end
                                from dbo.Contract c
                                inner join dbo.Orders o on c.OrdersId=o.OrdersId
                                inner join dbo.Repository r on c.ContractId=r.ContractId
                                inner join dbo.Driver d on o.OrdersId=d.OrdersId
                                inner join dbo.Custom cust on c.CustomId=cust.CustomId
                                where @PreMon <= (Case when c.Status in(1,2) then r.EditDate else c.DateE End)
                                And @Today >= c.DateB
                                And @ID = cust.IdNum
                                And c.Status >=0
                                And r.Status>=0
                                And c.Kind in ('B')"
                    , new { Id = ID, Today = sToday, PreMon = sPreMon });
            return list;
        }
    }
}
