using LibraryManager.Api.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.WebForms
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:44301/api/") };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            var response = await _client.GetAsync($"books/");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return  JsonConvert.DeserializeObject<List<BookDto>>(json);
        }

        public async Task<BookDto> GetBookAsync(int id)
        {
            var response = await _client.GetAsync($"books/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookDto>(json);
        }

        public async Task<BookDto> CreateBookAsync(BookDto book)
        {
            var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("books", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookDto>(json);
        }

        public async Task<string> UpdateBookAsync(BookDto book)
        {
            var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"books/{book.BookId}", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var response = await _client.DeleteAsync($"books/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var response = await _client.GetAsync("categories");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CategoryDto>>(json);
        }
    }
}