using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIS.DataAccess
{
    public class PriceRepository
    {
        private PDIS _context;


        public PriceRepository()
        {
            _context = new PDIS();
        }



        //Get price and time given start and finish and date from RouteData table


        public (double time, double price) Get(string source, string target, DateTime date)
        {
            IQueryable<RouteData> routeData = _


            throw new NotImplementedException();
        }


        //Get modifiers given cargotype and 
        public 
    }
}
