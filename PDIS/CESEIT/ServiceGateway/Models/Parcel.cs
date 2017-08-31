using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceGateway.Models
{
    public class Parcel
    {
        public string Date;
        public double WeightInKg;
        public double LargestSizeInCm;
        public string GoodsType;
        public bool Recommended;
    }
}