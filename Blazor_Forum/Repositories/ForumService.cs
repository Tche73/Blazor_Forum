
using Blazor_Forum.Models;
using Blazor_Forum.Services;
using System.Text.Json;

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
                // Utilisez GetFromJsonAsync pour simplifier la récupération
                var messages = await _httpClient.GetFromJsonAsync<List<Message>>("api/Messages");

                // Ajoutez des logs pour le débogage
                Console.WriteLine($"Nombre de messages récupérés : {messages?.Count ?? 0}");

                // Pour chaque message, ajoutez des logs sur les réponses
                if (messages != null)
                {
                    foreach (var message in messages)
                    {
                        Console.WriteLine($"Message ID: {message.Id}, Nombre de réponses: {message.Reponses?.Count ?? 0}");
                    }
                }

                return messages ?? new List<Message>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des messages : {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
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
        public async Task<List<Reponse>> GetReponsesAsync()
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

        public async Task<List<Reponse>> GetReponsesByMessageAsync(int messageId)
        {
            try
            {
                var apiUrl = $"api/Messages/{messageId}/Reponses";
                Console.WriteLine($"Appel de l'API pour les réponses: {_httpClient.BaseAddress}{apiUrl}");

                var response = await _httpClient.GetAsync(apiUrl);
                Console.WriteLine($"Statut de la réponse: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erreur API: {response.StatusCode}");
                    return new List<Reponse>();
                }

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Contenu de la réponse pour les réponses: {content}");

                var reponses = JsonSerializer.Deserialize<List<Reponse>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return reponses ?? new List<Reponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des réponses du message {messageId}: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                return new List<Reponse>();
            }
        }

        public async Task<Reponse> CreateReponseAsync(Reponse reponse)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Reponses", reponse);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Reponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de la réponse: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Like>> GetLikesByReponseAsync(int reponseId)
        {
            try
            {
                var apiUrl = $"api/Reponses/{reponseId}/Likes";
                Console.WriteLine($"Appel de l'API pour les likes: {_httpClient.BaseAddress}{apiUrl}");

                var response = await _httpClient.GetAsync(apiUrl);
                Console.WriteLine($"Statut de la réponse: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erreur API: {response.StatusCode}");
                    return new List<Like>();
                }

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Contenu de la réponse pour les likes: {content}");

                var likes = JsonSerializer.Deserialize<List<Like>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return likes ?? new List<Like>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des likes de la réponse {reponseId}: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                return new List<Like>();
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

