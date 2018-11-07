using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MD.Xamarin.Models;
using Newtonsoft.Json;

namespace MD.Xamarin.Rest
{
    public class NotesServiceClient : INotesServiceClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://192.168.56.1:54866/api/values";

        public NotesServiceClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<NoteModel>> GetNotes()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var noteModels = JsonConvert.DeserializeObject<List<NoteModel>>(await response.Content.ReadAsStringAsync());
                    return noteModels;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" --- Get all notes error: " + ex);
            }
            return new List<NoteModel>();
        }

        public async Task<NoteModel> Get(int id)
        {
            var url = BaseUrl + "/" + id;
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var noteModel = JsonConvert.DeserializeObject<NoteModel>(await response.Content.ReadAsStringAsync());
                return noteModel;
            }
            return null;
        }

        public async Task<bool> Create(NoteModel note)
        {
            try
            {
                var url = BaseUrl;
                var content = JsonConvert.SerializeObject(note);
                var data = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, data);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" --- Note creation error: " + ex);
            }
            return false;
        }

        public async Task<bool> UpdateNote(int id, NoteModel noteModel)
        {
            try
            {
                var url = BaseUrl + "/" + id;
                var content = JsonConvert.SerializeObject(noteModel);
                var data = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(url, data);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" --- Note update error: " + ex);
            }
            return false;
        }

        public async Task<bool> DeleteNote(int id)
        {
            try
            {
                var url = BaseUrl + "/" + id;
                var response = await _httpClient.DeleteAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" --- Delete note error: " + ex);
            }
            return false;
        }
    }
}