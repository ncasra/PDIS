using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIS.DataAccess
{
    public class PriceRepository
    {
        private PDISContext _context;


        public PriceRepository()
        {
            _context = new PDISContext();
        }



        //Get price and time given start and finish and date from RouteData table

        public Tuple<double, double> GetSG(string source, string target, DateTime date, string ctype, double weight, double largestDim)
        {
            var poidwa = Get(source, target, date, ctype, weight, largestDim);
            return new Tuple<double, double>(poidwa.time, poidwa.price);
        }

        public (double time, double price) Get(string source, string target, DateTime date, string ctype, double weight, double largestDim)
        {
            IQueryable<RouteData> routeData = _context.Set<RouteData>();
            IQueryable<Price> prices = _context.Set<Price>();
            IQueryable<CargoType> types = _context.Set<CargoType>();

            var segments = from routeDatum in routeData.Where(d => (d.From == source && d.To == target) || (d.From == target && d.To == source))
                           select routeDatum;
            var segResult = segments.First();

            var dw = (decimal)weight;
            var p = from price in prices.Where(pr => pr.ValidFrom <= date && pr.WeightFrom <= dw && pr.WeightTo >= dw)
                    select price;
            var priceResult = p.First();

            string strType = ctype.ToString();
            var type = from t in types.Where(ty => ty.Name == strType)
                       select t;
            var typeResult = type.First();


            var finalCost = segResult.Distance * priceResult.Price1 * typeResult.ChargeValue;
            var finalTime = segResult.Distance * 12;

            return ((double)finalTime, (double)finalCost);
        }


        //Get modifiers given cargotype
        public double GetModifier(CargoType type)
        {


            throw new NotImplementedException();
        }
    }
}
