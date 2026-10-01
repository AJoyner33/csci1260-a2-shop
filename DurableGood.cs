using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    /// <summary>
    /// Represents durable physical goods that include a warranty period.
    /// </summary>
    internal class DurableGood : PhysicalGood
    {
        private int warrantyMonths;

        /// <summary>Warranty period in months.</summary>
        public int WarrantyMonths { get { return warrantyMonths; } }

        public DurableGood(string sku, string name, decimal unitPrice, int quantityOnHand, double weightPounds, int warrantyMonths)
            : base(sku, name, unitPrice, quantityOnHand, weightPounds)
        {
            this.warrantyMonths = warrantyMonths;
            if (warrantyMonths < 0)
            {
                this.warrantyMonths = 0;
            }
        }

        public override string Category()
        {
            return "Durable";
        }

        public override decimal HandlingFee()
        {
            return ShippingCost();
        }

        public override string Describe()
        {
            return $"{base.Describe()}, {warrantyMonths} month warranty";
        }
    }
}
