using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    /// <summary>
    /// Base class for physical goods that have a weight and support shipping cost calculation.
    /// </summary>
    public abstract class PhysicalGood : StockItem
    {
        private double weightPounds;

        /// <summary>Rate used to compute shipping/handling cost per pound.</summary>
        public const decimal HandlingRate = 0.60m;

        /// <summary>Weight of the item in pounds.</summary>
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

        /// <summary>
        /// Calculates shipping cost based on a per-pound handling rate.
        /// </summary>
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
