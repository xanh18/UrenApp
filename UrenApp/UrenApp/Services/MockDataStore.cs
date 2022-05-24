using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenApp.Models;

namespace UrenApp.Services
{
    public class MockDataStore : IDataStore<Projects>
    {
        readonly List<Projects> items;

        public MockDataStore()
        {
            items = new List<Projects>()
            {
                new Projects { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Projects { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Projects { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Projects { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Projects { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Projects { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Projects item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Projects item)
        {
            var oldItem = items.Where((Projects arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Projects arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Projects> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Projects>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}