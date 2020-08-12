﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using System.Text;

namespace Team5_LUSS.Controllers
{
    public class ItemCategoryController : Controller
    {
      
        public async Task<IActionResult> ItemCategory()
        {
            List<ItemCategory> catList = new List<ItemCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44312/ItemCategory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    catList = JsonConvert.DeserializeObject<List<ItemCategory>>(apiResponse);
                }
            }
            ViewData["products"] = catList;
            return View();
          
        }

        public ViewResult GetItemCategory() => View();


        [HttpPost]
        public async Task<IActionResult> GetItemCategory(int id)
        {
            ItemCategory itemCategory = new ItemCategory();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44312/SaveItemCategory/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemCategory = JsonConvert.DeserializeObject<ItemCategory>(apiResponse);
                }
            }
            return View(itemCategory);
        }
      

        public ViewResult AddItemCategory() => View();

        [HttpPost]
        public async Task<IActionResult> AddItemCategory(ItemCategory reservation)
        {
            ItemCategory receivedItemCategory = new ItemCategory();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44312/ItemCategory", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedItemCategory = JsonConvert.DeserializeObject<ItemCategory>(apiResponse);
                }
            }
            ViewData["products"] = receivedItemCategory;
            return View(receivedItemCategory);
        }
        public async Task<IActionResult> UpdateItemCategory(int id)
        {
            ItemCategory itemCategory = new ItemCategory();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44312/ItemCategory/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemCategory = JsonConvert.DeserializeObject<ItemCategory>(apiResponse);
                }
            }
            return View(itemCategory);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItemCategory(ItemCategory itemCategory)
        {
            ItemCategory receivedItemCategory = new ItemCategory();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(itemCategory.CategoryID.ToString()), "CategoryID");
                content.Add(new StringContent(itemCategory.CategoryName), "CategoryName");

                using (var response = await httpClient.PutAsync("https://localhost:44312/ItemCategory", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedItemCategory = JsonConvert.DeserializeObject<ItemCategory>(apiResponse);
                }
            }
            return View(receivedItemCategory);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItemCategory(int ItemCategoryID)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44312/ItemCategory/" + ItemCategoryID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("ItemCategory");
        }
    }
}
