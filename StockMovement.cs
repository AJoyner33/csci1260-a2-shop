using System;

namespace Shop_OOP_Lab2
{
    /// <summary>
    /// Represents a single inventory movement (receive or release) for a StockItem.
    /// </summary>
    internal class StockMovement
    {
        private int seq;
        private string kind;
        private int count;

        public int Seq { get { return seq; } }
        public string Kind { get { return kind; } }
        public int Count { get { return count; } }

        public StockMovement(int seq, string kind, int count)
        {
            this.seq = seq;
            this.kind = kind;
            this.count = count;
        }

        /// <summary>
        /// Returns a human-readable description of the movement.
        /// </summary>
        public string Describe()
        {
            return $"move {seq}: {kind} {count}";
        }

        public override string ToString()
        {
            return Describe();
        }
    }
}
