using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    /// <summary>
    /// Represents a collection of StockItem objects and provides reporting and aggregate operations.
    /// Responsible for adding/finding items and computing value/sales summaries.
    /// </summary>
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

        /// <summary>
        /// Adds an item to the shop catalog if it is not null and its SKU is unique.
        /// Returns true on success, false when the item is null or a duplicate SKU exists.
        /// </summary>
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

        /// <summary>
        /// Finds a StockItem by SKU or returns null when not found.
        /// </summary>
        public StockItem Find(string sku)
        {
            if (items.Exists(i => i.Sku == sku) == true)
            {
                return items.Find(i => i.Sku == sku);
            }
            return null;
        }

        /// <summary>
        /// Computes the total inventory value using each item's ExtendedValue.
        /// </summary>
        public decimal TotalValue()
        {
            decimal total = 0.00m;
            foreach (StockItem item in items)
            {
                total += item.ExtendedValue();
            }
            return total;
        }

        /// <summary>
        /// Computes the hypothetical sales value if sale pricing were applied where eligible.
        /// This is used for comparison with the total inventory value.
        /// </summary>
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
                    total += item.ExtendedValue();
                }
            }
            return total;
        }

        /// <summary>
        /// Counts how many items implement IDiscountable (useful for contract checks).
        /// </summary>
        public int SignedCount()
        {
            int total = 0;
            foreach (StockItem item in items)
            {
                if (item is Interfaces.IDiscountable discountableItem)
                {
                    total += 1;
                }
            }
            return total;
        }

        /// <summary>
        /// Counts items currently considered on sale according to IDiscountable.IsOnSale.
        /// </summary>
        public int OnSaleCount()
        {
            int total = 0;
            foreach (StockItem item in items)
            {
                if (item is Interfaces.IDiscountable discountableItem && discountableItem.IsOnSale() == true)
                {
                    total += 1;
                }
            }
            return total;
        }

        /// <summary>
        /// In-place sort of items by computed value (higher first). Stable ordering is not guaranteed.
        /// </summary>
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

        /// <summary>
        /// A short one-line summary used when opening the catalog.
        /// Formatted so numeric columns align with the detailed report output.
        /// </summary>
        public string ReportLine()
        {
            // Format to align with StockItem.ReportLine columns:
            // Sku(8) + Name(23) + Category(12) = 43 chars for left area
            // Qty (6), then a space, then $ and value (9)
            return string.Format("{0}: {1} items, ${2:N2} on hand", Name, Count, TotalValue());
        }

        /// <summary>
        /// Prints a fixed-width inventory report to the console including a header, item lines,
        /// and summary totals. Formatting is deliberately fixed-width to produce tabular output.
        /// </summary>
        public void PrintReport()
        {
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"  {Name.ToUpper()} : INVENTORY REPORT"); 
            Console.WriteLine(new string('=', 60));
            // Header uses the same fixed-width columns as StockItem.ReportLine
            Console.WriteLine($"{"SKU",-8}{"Item",-23}{"Category",-12}{"Qty",6} {"$"}{"Value",9}");
            Console.WriteLine(new string('-', 60));
            foreach (StockItem item in items)
            {
                Console.WriteLine(item.ReportLine());
            }
            Console.WriteLine(new string('-', 60));
            // Summary lines aligned to the same columns: label in left area, count in Qty column, $value in Value column
            // Show only the record count in the Qty column; leave the $ value blank
            Console.WriteLine("{0,-43}{1,6} {2,10}", "Records on file:", Count, "");
            Console.WriteLine("{0,-43}{1,6} ${2,9:0.00}", "Total value on hand:", "", TotalValue());
            Console.WriteLine("{0,-43}{1,6} ${2,9:0.00}", "Value if every sale price were taken:", "", SalesValue());
            Console.WriteLine(new string('=', 60));
        }
    }
}
