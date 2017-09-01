using PDIS.DataAccess.NAVService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PDIS.DataAccess
{
    public class OrderRepository
    {
        private PDISContext _context;
        private string _NAVstring = "http://navvm-eitdk.westeurope.cloudapp.azure.com:7047/NAV/WS/CRONUS%20International%20Ltd./Codeunit/SalesInvoiceManagement";
        private string _NAVuser = "admin-eitdk";
        private string _NAVpass = "Eastindia4thewin";
        private NAVService.SalesInvoiceManagement cli;
        

        public OrderRepository()
        {
            _context = new PDISContext();
            cli = new NAVService.SalesInvoiceManagement();
        }

        public string CreateExternalOrder(string supplierId, double price, string start, string finish, string type_Id, double weight, double largestDim, double time, DateTime validUntil)
        {
            string OrderID;

            using (var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                var counters = _context.Set<Counter>();
                var qu = from c in counters.Where(c => c.CounterName == "ExternalOrderID")
                         select c;
                var count = qu.First();
                count.Number++;

                var order = new Quote()
                {
                    Id = count.Number.ToString(),
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

        public bool CreateInternalOrder(string id, double totalCost, string start, string finish, string cargotype, double weight)
        {
            try
            {
                //var tester = new Sa
                //_repository.CreateInternalOrder("0", info.TotalCost, info.RouteStops.First(), info.RouteStops.Last(), type.ToString(), weight);
                var counterset = _context.Set<Counter>();
                var query = from count in counterset.Where(c => c.CounterName == "NAVCounter")
                            select count;

                cli.Credentials = new NetworkCredential("admin-eitdk", "Eastindia4thewin", "nclan");
                cli.SalesHeader("1", "C00010", DateTime.Now, "1");

                var navcounter = query.First();
                cli.SalesLine(navcounter.Number.ToString(), 1, (decimal)totalCost, "1");
                //SalesHeader head = new SalesHeader("1", "1", DateTime.Now.ToString(), "1");            
                //cli.SalesHeader(head);
                //SalesLine line = new SalesLine(navcounter.Number.ToString(), 1, (decimal)totalCost, "1");
                //cli.SalesLine(line);


                using (var trans = _context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    navcounter.Number += 1;
                    _context.SaveChanges();
                    trans.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean CompleteExternalOrder(string orderid, string supplierid)
        {
            var now = DateTime.Now;
            IQueryable<Quote> qs = _context.Set<Quote>();

            var query = from q in qs.Where(q => q.ValidUntil > now && q.Supplier_Id == supplierid)
                        select q;
            var result = query.ToList();
            if (!result.Any())
                return false;
            var specQ = result.First();
            try
            {
                using (var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    specQ.Accepted = true;
                    _context.SaveChanges();
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
