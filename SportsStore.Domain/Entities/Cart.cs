using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            //добавить продукт и количество в картлайн
            CartLine Line = lineCollection.Where(p => p.product.ProductID == product.ProductID).FirstOrDefault();
            if (Line == null)
            {
                CartLine NewLine = new CartLine();
                NewLine.product = product;
                NewLine.Quantity = quantity;

                lineCollection.Add(Line);
            }
            else
            {
                Line.Quantity = quantity;
            }

        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(p => p.product.Price * p.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
}
