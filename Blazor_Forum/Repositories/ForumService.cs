using Azure;
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

        // Messages
        public async Task<List<Message>> GetMessagesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Message>>("api/Messages") ?? new List<Message>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des messages: {ex.Message}");
                return new List<Message>();
            }
        }

        public async Task<Message> GetMessageAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Message>($"api/Messages/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du message {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Messages", message);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Message>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création du message: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateMessageAsync(int id, Message message)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Messages/{id}", message);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la mise à jour du message {id}: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteMessageAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Messages/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression du message {id}: {ex.Message}");
                throw;
            }
        }

        // Reponses
        public async Task<List<Reponse>> GetResponsesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Reponse>>("api/Reponses") ?? new List<Reponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des réponses: {ex.Message}");
                return new List<Reponse>();
            }
        }

        public async Task<List<Reponse>> GetResponsesByMessageAsync(int messageId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Reponse>>($"api/Messages/{messageId}/Reponses") ?? new List<Reponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des réponses du message {messageId}: {ex.Message}");
                return new List<Reponse>();
            }
        }

        public async Task<Reponse> CreateResponseAsync(Reponse reponse)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync("api/Reponses", reponse);
                httpResponse.EnsureSuccessStatusCode();
                return await httpResponse.Content.ReadFromJsonAsync<Reponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de la réponse: {ex.Message}");
                throw;
            }
        }

        // Users
        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<User>>("api/Users") ?? new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des utilisateurs: {ex.Message}");
                return new List<User>();
            }
        }

        public async Task<User> GetUserAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<User>($"api/Users/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération de l'utilisateur {id}: {ex.Message}");
                throw;
            }
        }

        // Likes
        public async Task<Like> AddLikeAsync(Like like)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Likes", like);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Like>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'ajout du like: {ex.Message}");
                throw;
            }
        }

        public async Task RemoveLikeAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Likes/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression du like {id}: {ex.Message}");
                throw;
            }
        }
    }

}

