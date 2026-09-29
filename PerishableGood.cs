using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    internal class PerishableGood : PhysicalGood, Interfaces.IDiscountable
    {
        private int shelfLifeDays;

        public const decimal SuperChargeFee = 0.40m;
        public int ShelfLifeDays { get { return shelfLifeDays; } }
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

        public decimal SalePrice()
        {
            if (IsOnSale())
            {
                return UnitPrice * 0.70m; // 30% discount
            }
            return UnitPrice;
        }

        public override string Describe()
        {
            return $"{base.Describe()}, {shelfLifeDays} days left";
        }
    }
}
