using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    internal class Shop : Interfaces.IReportable
    {
        public string name { get; private set; }
        private List<StockItem> items;

        public string Name { get { return name; } }
        public int Count { get { return items.Count; } }

        public Shop(string name)
        {
            this.name = name;
            this.items = new List<StockItem>();
        }

        public bool Add(StockItem item)
        {
            if (item == null)
            {
                return false;
            }
            else if (items.Exists(i => i.Sku == item.Sku))
            {
                return false;
            }
            items.Add(item);
            return true;
        }

        public StockItem Find(string sku)
        {
            if (items.Exists(i => i.Sku == sku) == true)
            {
                return items.Find(i => i.Sku == sku);
            }
            return null;
        }

        public decimal TotalValue()
        {
            decimal total = 0;
            foreach (StockItem item in items)
            {
                total += item.UnitPrice * item.QuantityOnHand;
            }
            return total;
        }

        public decimal SalesValue()
        {
            decimal total = 0;
            foreach (StockItem item in items)
            {
                if (item is Interfaces.IDiscountable discountableItem && discountableItem.IsOnSale() == true)
                {
                    total += (discountableItem.SalePrice() + item.HandlingFee()) * item.QuantityOnHand;
                }
                else
                {
                    total += item.UnitPrice * item.QuantityOnHand;
                }
            }
            return total;
        }

        public int SignedCount()
        {
            int total = 0;
            foreach (StockItem item in items)
            {
                if (item is Interfaces.IDiscountable discountableItem)
                {
                    total += item.QuantityOnHand;
                }
            }
            return total;
        }

        public int OnSaleCount()
        {
            int total = 0;
            foreach (StockItem item in items)
            {
                if (item is Interfaces.IDiscountable discountableItem && discountableItem.IsOnSale() == true)
                {
                    total += item.QuantityOnHand;
                }
            }
            return total;
        }

        public void SortByValue()
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                int best = i;
                for (int j = i + 1; j < items.Count; j++)
                    if (Beats(items[j], items[best]))
                        best = j;
                if (best != i)
                {
                    StockItem hold = items[i];
                    items[i] = items[best];
                    items[best] = hold;
                }
            }
        }

        private bool Beats(StockItem a, StockItem b)
        {
            if (ReferenceEquals(a, b)) return false;
            if (a == null) return false;
            if (b == null) return true;

            decimal valueOf(StockItem it)
            {
                if (it is Interfaces.IDiscountable d && d.IsOnSale())
                    return (d.SalePrice() + it.HandlingFee()) * it.QuantityOnHand;
                return it.UnitPrice * it.QuantityOnHand;
            }

            return valueOf(a) > valueOf(b);
        }

        public string ReportLine()
        {
            return $"{Name}: {Count} items, ${TotalValue()} on hand";
        }

        public void PrintReport()
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"  {Name}: {ReportLine()}");
        }
    }
}
