using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP_Lab2.Interfaces
{
    internal interface IDiscountable
    {
        public bool IsOnSale();
        public decimal SalePrice();
    }
}
