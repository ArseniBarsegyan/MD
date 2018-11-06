using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MD.Xamarin.Models;
using Newtonsoft.Json;

namespace MD.Xamarin.Rest
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://192.168.56.1:54866/api/values";

        public RestClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<NoteModel>> GetNotes()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var noteModels = JsonConvert.DeserializeObject<List<NoteModel>>(await response.Content.ReadAsStringAsync());
                return noteModels;
            }
            else
            {
                return new List<NoteModel>();
            }
        }
    }
}