using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MD.WPF.Helpers;
using MD.WPF.Models;
using MD.WPF.Properties;
using Newtonsoft.Json;

namespace MD.WPF.Services
{
    public class ServiceClient
    {
        private static ServiceClient _instance;
        private readonly HttpClient _httpClient = new HttpClient();
        // private const string BaseUrl = "http://192.168.100.98:51870";
        private const string BaseUrl = "http://10.132.34.144:51870";

        private ServiceClient() {}

        public static ServiceClient GetInstance()
        {
            return _instance ?? (_instance = new ServiceClient());
        }

        /// <summary>
        /// Return notes from REST API.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Note>> GetNotesAsync()
        {
            var notes = new List<Note>();
            try
            {
                string url = BaseUrl + "/api/notes";
                if (!string.IsNullOrEmpty(Settings.Default.Token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Settings.Default.Token);
                }
                var allNotes = await _httpClient.GetStringAsync(url);
                notes = JsonConvert.DeserializeObject<List<Note>>(allNotes);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return notes.OrderByDescending(x => x.Id).ToList();
        }

        /// <summary>
        /// Login through the REST service and update token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Login(LoginModel model)
        {
            try
            {
                var loginUrl = BaseUrl + "/Account/Login";
                var postModel = JsonConvert.SerializeObject(model);

                var postData = new StringContent(postModel, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(loginUrl, postData);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (HttpContent content = response.Content)
                    {
                        var token = await content.ReadAsStringAsync();

                        if (token != null)
                        {
                            Settings.Default.Token = token;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Create note through the REST service.
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task<string> CreateNote(Note note)
        {
            try
            {
                string url = BaseUrl + "/api/notes";
                if (!string.IsNullOrEmpty(Settings.Default.Token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Settings.Default.Token);
                }
                var postModel = JsonConvert.SerializeObject(note);

                var postData = new StringContent(postModel, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, postData);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return ConstantsHelper.Ok;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ConstantsHelper.UnknownError;
        }
    }
}