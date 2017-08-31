using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PDIS.DataAccess
{
    public class Class1
    {
        private DbContext context = new PDIS();




        public void testfunction()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                Guid g = Guid.NewGuid();

                Price price1 = new Price();
                price1.ValidFrom = new DateTime(2017, 8, 31);
                price1.Id = g.ToString();
                price1.WeightFrom = 0;
                price1.WeightTo = 0;
                price1.Price1 = 100;

                context.Set<Price>().Add(price1);
                context.SaveChanges();
                transaction.Commit();
            }
        }



    }
}
