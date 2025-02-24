using Blazor_Forum.Models;
using Blazor_Forum.Services;
using Microsoft.AspNetCore.Http.Connections;

namespace Blazor_Forum.Repositories
{
    public class ForumService : IForumService
    {
        private readonly HttpClient _httpClient;
        public ForumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Message>> GetMessagesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Message>>("api/messages");
            }
            catch (Exception)
            {
                return new List<Message>();
            }
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Message>($"api/messages/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/messages", message);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Message>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> CreateResponseAsync(Response response)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync("api/responses", response);
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadFromJsonAsync<Response>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Like> CreateLikeAsync(Like like)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync("api/likes", like);
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadFromJsonAsync<Like>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/messages/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteResponseAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/responses/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteLikeAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/likes/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
}
