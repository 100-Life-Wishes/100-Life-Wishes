using _100_Life_Wishes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _100_Life_Wishes.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace _100_Life_Wishes.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        private List<Item> items;
        string filePath;

        public MockDataStore()
        {
            items = new List<Item>() ;
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.json");
        }
        private void LoadData()
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                items = JsonConvert.DeserializeObject<List<Item>>(jsonData);
            }
        }

        private void SaveData()
        {
            var jsonData = JsonConvert.SerializeObject(items);
            File.WriteAllText(filePath, jsonData);
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);
            SaveData();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            SaveData();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            SaveData();

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            LoadData();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            LoadData();

            return await Task.FromResult(items);
        }
    }
}