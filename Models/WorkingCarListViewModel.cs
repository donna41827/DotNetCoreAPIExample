using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeasedCarService.Models
{
    public class WorkingCarListViewModel
    {
        public int Role { get; set; }
        public Principal Principal { get; set; }
        public List<CarList> CarList { get; set; }
    }
    public class Principal
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class CarList 
    {
        public string LicensePlate { get; set; }
        public string DriverID { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public int CarStatus { get; set; }
    }
    public class WorkingCarListQueryResult 
    {
        public int CarStatus { get; set; }
        public string LicensePlate { get; set; }
        public string DriverID { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalPhone { get; set; }
    }
}
