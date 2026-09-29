using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    public abstract class PhysicalGood : StockItem
    {
        private double weightPounds;

        public const decimal HandlingRate = 0.60m;

        public double WeightPounds { get { return weightPounds; } }

        

        protected PhysicalGood(string sku, string name, decimal unitPrice, int quantityOnHand, double weightPounds)
            : base(sku, name, unitPrice, quantityOnHand)
        {
            this.weightPounds = weightPounds;
            if (weightPounds < 0)
            {
                this.weightPounds = 0;
            }
        }

        public decimal ShippingCost()
        {
            return HandlingRate * (decimal)weightPounds;
        }

        public override string Describe()
        {
            return $"{base.Describe()}, Weight: {weightPounds} lbs, Shipping Cost: {ShippingCost():C}";
        }
    }
}
