using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    /// <summary>
    /// Represents a service-type stock item (no shipping/handling fees).
    /// Implements IDiscountable to indicate services may be on sale.
    /// </summary>
    internal class ServiceItem : StockItem, Interfaces.IDiscountable
    {
        private double laborHours;

        /// <summary>Estimated labor hours required for the service.</summary>
        public double LaborHours { get { return laborHours; } }
        /// <summary>Services with sufficient labor may be offered at a sale price.</summary>
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
