using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Model
{
    public class Item
    {
        public Item() { }
        public Item(Item item)
        {
            SKUId = item.SKUId;
            Quantity = item.Quantity;
        }

        public char SKUId { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{SKUId} {Quantity}";
        }
    }
}
