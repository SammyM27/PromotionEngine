using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Model
{
    public class Engine
    {
        private IEnumerable<SKUPrice> PriceList;
        private IEnumerable<Promotion> Promotions;

        public Engine(IEnumerable<SKUPrice> priceList, IEnumerable<Promotion> promotions)
        {
            this.PriceList = priceList;
            this.Promotions = promotions;
        }

        public void CheckOut(Order order)
        {
            var foundItems = new List<Item>();
            if (Promotions != null && Promotions.Count() > 0)
                foreach (var promotion in Promotions)
                {
                    var validatedItems = promotion.Validate(order, foundItems);
                    UpdateValidatedItems(foundItems, validatedItems);
                }

            ApplyRegularPrice(order, foundItems);
        }

        private void ApplyRegularPrice(Order order, List<Item> foundItems)
        {
            foreach (var item in order.Items)
            {
                var validateItem = foundItems.FirstOrDefault(x => x.SKUId == item.SKUId) ?? item;
                var quantity = validateItem.Quantity;
                if (quantity > 0)
                    order.TotalAmount += quantity * PriceList.First(x => x.SKU_Id == item.SKUId).UnitPrice;
            }
        }

        private static void UpdateValidatedItems(List<Item> foundItems, IEnumerable<Item> validatedItems)
        {
            if (validatedItems == null || validatedItems.Count() < 1)
                return;

            foreach (var item in validatedItems)
                if (!foundItems.Any(x => x.SKUId == item.SKUId))
                    foundItems.Add(item);
        }
    }
}
