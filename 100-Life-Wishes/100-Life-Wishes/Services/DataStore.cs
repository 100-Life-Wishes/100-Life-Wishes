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
    public class DataStore : IDataStore<TaskItem>
    {
        private List<TaskItem> items;
        readonly string filePath;

        public DataStore()
        {
            items = new List<TaskItem>() ;
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.json");
        }
        private void LoadData()
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                items = JsonConvert.DeserializeObject<List<TaskItem>>(jsonData);
            }
        }

        private void SaveData()
        {
            var jsonData = JsonConvert.SerializeObject(items);
            File.WriteAllText(filePath, jsonData);
        }

        public async Task<bool> AddItemAsync(TaskItem item)
        {
            items.Add(item);
            SaveData();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(TaskItem item)
        {
            var oldItem = items.Where((TaskItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            SaveData();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((TaskItem arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            SaveData();

            return await Task.FromResult(true);
        }

        public async Task<TaskItem> GetItemAsync(string id)
        {

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TaskItem>> GetItemsAsync(bool forceRefresh = false)
        {
            LoadData();

            return await Task.FromResult(items);
        }
    }
}