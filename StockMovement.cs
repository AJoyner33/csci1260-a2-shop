using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2
{
    internal class StockMovement
    {
        private int seq;
        private string kind;
        private int cuont;

        public int Seq { get { return seq; } }
        public string Kind { get { return kind; } }
        public int Count { get { return cuont; } }

        public StockMovement(int seq, string kind, int count)
        {
            this.seq = seq;
            this.kind = kind;
            this.cuont = count;
        }

        public string Describe()
        {
            return $"move {seq}: {kind} {cuont}";
        }
    }
}
