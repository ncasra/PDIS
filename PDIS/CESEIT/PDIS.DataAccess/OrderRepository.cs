using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PDIS.DataAccess
{
    public class OrderRepository
    {
        private PDIS _context;

        public OrderRepository()
        {
            _context = new PDIS();
        }

        public string CreateExternalOrder(string supplierId, double price, string start, string finish, string type_Id, double weight, double largestDim, double time, DateTime validUntil)
        {
            string OrderID;
            using (var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                var order = new Quote()
                {
                    Supplier_Id = supplierId,
                    QuotedPrice = (decimal)price,
                    Origin = start,
                    Destination = finish,
                    Type_Id = type_Id,
                    Weight = (decimal)weight,
                    Widest_Dimension = (decimal)largestDim,
                    TransportTime = (decimal)time,
                    ValidUntil = validUntil,
                    Accepted = false,
                };
                var returnOrder = _context.Set<Quote>().Add(order);
                _context.SaveChanges();
                transaction.Commit();
                OrderID = returnOrder.Id;
            }
            return OrderID;
        }



        public Boolean CompleteExternalOrder(string id)
        {
            var now = DateTime.Now;
            return true;
        }
    }
}
