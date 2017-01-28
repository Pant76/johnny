using System;
using System.Collections;
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
    public class WordPressDataSource<T>:IWordPressDataSource<T> where T: IObservableEntityData,ITaggable
    {

        private HttpClient _httpClient;
        private string _concreteType;
        private IEnumerable<T> _data;


        private async Task<IEnumerable<T>> GetFromUrl(string filter="")
        {
            var response = await _httpClient.GetAsync(string.Concat(_concreteType,"?per_page=100&_embed&", filter));
            
            if (response.IsSuccessStatusCode)
            {
                var raw = await response.Content.ReadAsStringAsync();
                _data = JsonConvert.DeserializeObject<List<T>>(raw);
                return  _data;
            }
            throw new InvalidOperationException("Errore di comunicazione");
        }

        public WordPressDataSource(string url,string concreteType,IEnumerable<T> mockData =null)
        {
            if (mockData != null)
            {
                _data = mockData;
                return;
            }
            _concreteType = concreteType;
            _httpClient = new HttpClient {BaseAddress = new Uri(url)};
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        event DataSyncErrorEventHandler<T> IDataSource<T>.OnDataSyncError
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<T>> GetItemsByTag(IEnumerable<string> tags)
        {
            var filterStr =string.Concat("filter[tag]=", string.Join("+", tags));
            return await GetFromUrl(filterStr);
        }


        public async Task<IEnumerable<T>> GetItems()
        {
            if (_data!=null)
            {
                return _data;
            }
            return await GetFromUrl();
        }

        public async Task<T> GetItem(string id)
        {
            var response = await _httpClient.GetAsync(string.Concat(_concreteType,"/",id));
            if (response.IsSuccessStatusCode)
            {
                var raw = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<T>(raw);
                return res;
            }
            throw new InvalidOperationException("Errore di comunicazione");
        }



        public Task<bool> AddItem(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItem(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveItem(T item)
        {
            throw new NotImplementedException();
        }

        public event DataSyncErrorEventHandler<Dish> OnDataSyncError;
        public async Task<IEnumerable<string>> GetTags(string startsWith,string onlyElementsWithTag=null)
        {
            IEnumerable<T> res;

            if(string.IsNullOrEmpty(onlyElementsWithTag))
                res=await GetItems();
            else
            {
                var r = await GetItems();
                res = r.Where(i => i.Tags.Any(t => t == onlyElementsWithTag));
            }
            var foundTags = res.SelectMany(i => i.Tags).Where(i => i.StartsWith(startsWith)).Distinct();
            return foundTags;
        }
    }
}
