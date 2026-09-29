using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    internal class ServiceItem : StockItem, Interfaces.IDiscountable
    {
        private double laborHours;

        public double LaborHours { get { return laborHours; } }
        public bool IsOnSale()
        {
            if (laborHours >= 2.0)
            {
                return true;
            }
            return false;
        }

        public ServiceItem(string sku, string name, decimal unitPrice, int quantityOnHand, double laborHours)
            : base(sku, name, unitPrice, quantityOnHand)
        {
            this.laborHours = laborHours;
            if (laborHours < 0)
            {
                this.laborHours = 0;
            }
        }

        public override string Category()
        {
            return "Service";
        }

        public override decimal HandlingFee()
        {
            return 0m;
        }

        public decimal SalePrice()
        {
            if (IsOnSale())
            {
                return UnitPrice * 0.85m; // 15% discount
            }
            return UnitPrice;
        }

        public override string Describe()
        {
            return $"{base.Describe()}, {laborHours} labor hours";
        }
    }
}
