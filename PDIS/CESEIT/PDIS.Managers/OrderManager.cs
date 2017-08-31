using PDIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIS.Managers
{
    public class OrderManager
    {
        private OrderRepository _repository;


        public OrderManager()
        {
            _repository = new OrderRepository();
        }


        public string CreateExternalOrder(string supplierId, double price, string start, string finish, string type_Id, double weight, double largestDim, double time, DateTime validUntil)
        {
            return _repository.CreateExternalOrder( supplierId,  price,  start,  finish,  type_Id,  weight,  largestDim,  time,  validUntil);
        }

        public string CompleteExternalOrder(string orderID)
        {
            _repository.CompleteExternalOrder(orderID);
        }


    }
}
