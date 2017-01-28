using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JonnyGallo.Abstractions;
using JonnyGallo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JonnyGallo.Data
{
    public class CategoriesDataSources : IDataSource<Category>
    {
        public CategoriesDataSources()
        {
        }

        public async Task<IEnumerable<Category>> GetItems()
        {
            var categories = new List<Category>() {
                new Category(){
                    Title="Antipasti",
                    Text="Antipasti",
                }
            };
            return await Task.Run(() => { return categories; });
        }

        public async Task<IEnumerable<Category>> GetItemsByTag(IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetItem(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddItem(Category item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItem(Category item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveItem(Category item)
        {
            throw new NotImplementedException();
        }

        public event DataSyncErrorEventHandler<Category> OnDataSyncError;
    }
}
