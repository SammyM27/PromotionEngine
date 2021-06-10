﻿using Microsoft.AspNetCore.Mvc;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController_B : ControllerBase
    {
        static readonly IEnumerable<SKUPrice> PriceList =
       new List<SKUPrice> {
         new SKUPrice { SKU_Id = 'A', UnitPrice = 50 },
         new SKUPrice { SKU_Id = 'B', UnitPrice = 30 },
         new SKUPrice { SKU_Id = 'C', UnitPrice = 20 },
         new SKUPrice { SKU_Id = 'D', UnitPrice = 15 } };

        static readonly IEnumerable<Promotion> Promotions =
          new List<Promotion> {
         new Promotion {
           Items = new List<Item> {
             new Item { SKUId = 'A', Quantity = 3 }},
           TotalAmount = 130 }, // 3 of A for 130
         new Promotion {
           Items = new List<Item> {
             new Item { SKUId = 'B', Quantity = 2 }},
           TotalAmount = 45 }, // 2 of B for 45
      }; 
        //PromotionData promotionalData = new PromotionData();
        static readonly Engine actualEngine = new Engine(PriceList, Promotions);

       [ HttpGet]
        public string Scenario_B()
        {
            var order =
              new Order
              {
                  Items = new List<Item>
                {
            new Item { SKUId = 'A', Quantity = 5 },
            new Item { SKUId = 'B', Quantity = 5 },
            new Item { SKUId = 'C', Quantity = 1 }}
              };

            actualEngine.CheckOut(order);
            return order.TotalAmount.ToString();
        }
    }
}
