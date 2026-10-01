using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shop_OOP_Lab2
{
    /// <summary>
    /// Abstract base for any item tracked in the shop inventory.
    /// Encapsulates common properties such as SKU, name, unit price and quantity,
    /// and maintains a history of StockMovement records.
    /// </summary>
    public abstract class StockItem : Interfaces.IReportable
    {
        private string sku;
        private string name;
        private decimal unitPrice;
        private int quantityOnHand;
        private List<StockMovement> history;
        private int nextSeq;

        /// <summary>Unique stock-keeping identifier.</summary>
        public string Sku { get { return sku; } }
        /// <summary>Descriptive name of the item.</summary>
        public string Name { get { return name; } }
        /// <summary>Unit price before handling or discounts.</summary>
        public decimal UnitPrice { get { return unitPrice; } }
        /// <summary>Quantity currently on hand.</summary>
        public int QuantityOnHand { get { return quantityOnHand; } }
        /// <summary>Number of recorded movements for this item.</summary>
        public int MoveCount { get { return history.Count; } }

        /// <summary>
        /// Protected constructor for use by derived item types.
        /// Validates non-negative price and quantity.
        /// </summary>
        protected StockItem(string sku, string name, decimal unitPrice, int quantityOnHand)
        {
            this.sku = sku;
            this.name = name;
            this.unitPrice = unitPrice;
            if (unitPrice < 0)
            {
                this.unitPrice = 0;
            }
            this.quantityOnHand = quantityOnHand;
            if (quantityOnHand < 0)
            {
                this.quantityOnHand = 0;
            }
            this.history = new List<StockMovement>();
            this.nextSeq = 1;
        }

        /// <summary>Returns the category name for reporting (e.g. "Perishable").</summary>
        public abstract string Category();

        /// <summary>Returns any additional handling fee applied per unit.</summary>
        public abstract decimal HandlingFee();

        /// <summary>
        /// The extended value includes the unit price plus handling fee multiplied by quantity.
        /// Used for consistent reporting of value where handling cost is relevant.
        /// </summary>
        public decimal ExtendedValue()
        {
            return (unitPrice + HandlingFee()) * quantityOnHand;
        }

        /// <summary>
        /// Increase the on-hand quantity and add a Received movement. Returns false for non-positive counts.
        /// </summary>
        public bool Recieve(int count)
        {
            if (count <= 0)
            {
                return false;
            }
            quantityOnHand += count;
            history.Add(new StockMovement(nextSeq++, "Received", count));
            return true;
        }

        public bool Release(int count) //why isn
        {
            if (count <= 0 || count > quantityOnHand)
            {
                return false;
            }
            quantityOnHand -= count;
            history.Add(new StockMovement(nextSeq++, "Released", count));
            return true;
        }

        /// <summary>
        /// Returns the movement history as newline-separated lines suitable for console output.
        /// </summary>
        public string MovementLines()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var move in history)
            {
                sb.AppendLine(move.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// A short one-line description used by ToString.
        /// </summary>
        public virtual string Describe()
        {
            return String.Format("{0} {1} ({2})", Sku, Name, Category());
        }

        /// <summary>
        /// Returns a fixed-width formatted report line matching the shop report columns.
        /// </summary>
        public string ReportLine()
        {
            var value = ExtendedValue();
        // Qty then a single space, then a dollar sign column, then the value right-aligned.
            return $"{Sku,-8}{Name,-23}{Category(),-12}{QuantityOnHand,6} ${value,9:0.00}";
        }

        public override string ToString()
        {
            return Describe();
        }
    }
}
