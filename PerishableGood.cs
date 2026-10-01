using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    /// <summary>
    /// Physical good that has a shelf life and may be subject to a super-charge fee.
    /// Implements IDiscountable to indicate sale eligibility.
    /// </summary>
    internal class PerishableGood : PhysicalGood, Interfaces.IDiscountable
    {
        private int shelfLifeDays;

        /// <summary>Additional handling fee applied to perishable goods.</summary>
        public const decimal SuperChargeFee = 0.40m;
        /// <summary>Remaining shelf life in days.</summary>
        public int ShelfLifeDays { get { return shelfLifeDays; } }
        /// <summary>True when the remaining shelf life makes the item eligible for sale pricing.</summary>
        public bool IsOnSale()
        {
            if (shelfLifeDays <= 3)
            {
                return true;
            }
            return false;
        }

        public PerishableGood(string sku, string name, decimal unitPrice, int quantityOnHand, double weightPounds, int shelfLifeDays)
            : base(sku, name, unitPrice, quantityOnHand, weightPounds)
        {
            this.shelfLifeDays = shelfLifeDays;
            if (shelfLifeDays < 0)
            {
                this.shelfLifeDays = 0;
            }
        }

        public override string Category()
        {
            return "Perishable";
        }

        public override decimal HandlingFee()
        {
            return ShippingCost() + SuperChargeFee;
        }

        /// <summary>
        /// Returns the sale price when the item is on sale; otherwise the regular unit price.
        /// </summary>
        public decimal SalePrice()
        {
            if (IsOnSale())
            {
                // Apply a 30% discount when the item is on sale
                return UnitPrice * 0.70m;
            }
            return UnitPrice;
        }

        public override string Describe()
        {
            return $"{base.Describe()}, {shelfLifeDays} days left";
        }
    }
}
