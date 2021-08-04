using AutoMapper;
using Dapper;
using LeasedCarService.Models;
using LeasedCarService.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeasedCarService.Service
{
    public class LeasedCarListService
    {
        Lib lib = new Lib();
        private IConfiguration _config;
        WorkingCarListDapperRepository<WorkingCarListQueryResult> _repository = new WorkingCarListDapperRepository<WorkingCarListQueryResult>();
        public LeasedCarListService(IConfiguration config)
        {
            _config = config;
        }
        public WorkingCarListViewModel Lookup(string ID, string PhoneNum)
        {
            string sToday = (DateTime.Now.Year - 1911).ToString() + lib.Right("00" + DateTime.Now.Month.ToString(), 2) + lib.Right("00" + DateTime.Now.Day.ToString(), 2);
            string sPreMon = (DateTime.Now.Year - 1911).ToString() + lib.Right("00" + (DateTime.Now.Month - 1).ToString(), 2) + lib.Right("00" + DateTime.Now.Day.ToString(), 2);
            WorkingCarListViewModel vm = new WorkingCarListViewModel();
            
            IEnumerable<WorkingCarListQueryResult> list = _repository.GetData(_config, ID, sToday, sPreMon);
            if (list.Any(a => GetPhoneNum(a.PrincipalPhone) == GetPhoneNum(PhoneNum) || GetPhoneNum(a.DriverPhone) == GetPhoneNum(PhoneNum)))
            {
                int iRole = list.Any(a => GetPhoneNum(a.PrincipalPhone) == GetPhoneNum(PhoneNum)) ? 1 : 0;
                vm.Role = iRole;

                var listP = list.Select(s => new
                {
                    Name = s.PrincipalName,
                    PhoneNum = s.PrincipalPhone
                }).FirstOrDefault(f => f.Name != "" && f.PhoneNum != "");
                if (listP != null)
                {
                    vm.Principal = new Principal();
                    vm.Principal.Name = listP.Name;
                    vm.Principal.PhoneNumber = listP.PhoneNum;
                }
                var listC = list.Where(w => iRole == 1 || GetPhoneNum(w.DriverPhone) == GetPhoneNum(PhoneNum)).Select(s => new
                {
                    LicensePlate = s.LicensePlate,
                    DriverID = s.DriverID,
                    DriverName = s.DriverName,
                    DriverPhone = s.DriverPhone,
                    CarStatus = s.CarStatus
                }).ToList();
                vm.CarList = new List<CarList>();
                foreach (var c in listC)
                {
                    var cl = new CarList { LicensePlate = c.LicensePlate, DriverID = c.DriverID, DriverName = c.DriverName, DriverPhone = c.DriverPhone, CarStatus = c.CarStatus };
                    vm.CarList.Add(cl);
                }
            }
            else
            {
                vm = null;
            }

            return vm;
        }
        private string GetPhoneNum(string Phone)
        {
            return Regex.Replace(Phone, "[^0-9]", "");
        }
    }
}
