using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


using DataAccess.BussinessObjects;
namespace AssigmentPRN
{
    public class Number {
        public string name { get; set; }
        public int count { get; set; }

    }
    public class MainViewModel
    {
     public static    FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();

        public static  List<Number> list()
        {
            
                var query = (from a in flightManagementDbContext.Airports
                             join f in flightManagementDbContext.Flights on a.Id equals f.ArrivingAirport
                             group a by a.Name into g
                             select new Number
                             {
                                 name = g.Key,
                                 count = g.Count()
                             }).ToList();

                return query;
            
        }
        public static List<Number> list2()
        {

            var query = (from a in flightManagementDbContext.Airports
                         join f in flightManagementDbContext.Flights on a.Id equals f.DepartingAirport
                         group a by a.Name into g
                         select new Number
                         {
                             name = g.Key,
                             count = g.Count()
                         }).ToList();

            return query;

        }

    }



} 



