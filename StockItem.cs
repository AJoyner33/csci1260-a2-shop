using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shop_OOP_Lab2
{
    public abstract class StockItem : Interfaces.IReportable
    {
        private string sku;
        private string name;
        private decimal unitPrice;
        private int quantityOnHand;
        private List<StockMovement> history;
        private int nextSeq;

        public string Sku { get { return sku; } }
        public string Name { get { return name; } }
        public decimal UnitPrice { get { return unitPrice; } }
        public int QuantityOnHand { get { return quantityOnHand; } }
        public int MoveCount { get { return history.Count; } }

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
            if (quantityOnHand > 0)
            {
                this.quantityOnHand = 0;
            }
            this.history = new List<StockMovement>();
            this.nextSeq = 1;
        }

        public abstract string Category();

        public abstract decimal HandlingFee();

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

        public bool Release(int count)
        {
            if (count <= 0 || count > quantityOnHand)
            {
                return false;
            }
            quantityOnHand -= count;
            history.Add(new StockMovement(nextSeq++, "Released", -count));
            return true;
        }

        public string MovementLines()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var move in history)
            {
                sb.AppendLine(move.ToString());
            }
            return sb.ToString();
        }

        public virtual string Describe()
        {
            return $"SKU: {Sku}, Name: {Name}, Unit Price: {UnitPrice}, Quantity On Hand: {QuantityOnHand}";
        }

        public string ReportLine()
        {
            return $"{Sku}, {Name}, {UnitPrice}, {QuantityOnHand}";
        }

        public override string ToString()
        {
            return Describe();
        }
    }
}
