using Blazor_Forum.Dto_s;
using Blazored.LocalStorage;

namespace Blazor_Forum.Repositories
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginModel);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

                // Stocker tous les éléments, y compris la date d'expiration
                await _localStorage.SetItemAsync("authToken", result.Token);
                await _localStorage.SetItemAsync("userId", result.UserId.ToString());
                await _localStorage.SetItemAsync("username", result.Username);
                await _localStorage.SetItemAsync("tokenExpiration", result.ExpirationDate.ToString("o")); // Format ISO 8601

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de connexion : {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RegisterAsync(RegisterDto registerModel)
        {
            try
            {
                // Logs détaillés avant l'envoi
                Console.WriteLine("Tentative d'inscription :");
                Console.WriteLine($"Username: {registerModel.Username}");
                Console.WriteLine($"Email: {registerModel.Email}");

                // Envoyez les données à l'API
                var response = await _httpClient.PostAsJsonAsync("api/Auth/register", registerModel);

                // Logs sur la réponse
                Console.WriteLine($"Statut de la réponse : {response.StatusCode}");

                // Lisez le contenu de la réponse
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Contenu de la réponse : {responseContent}");

                // Vérifiez si la requête a réussi
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseContent);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Logs d'erreur détaillés
                Console.WriteLine($"Erreur d'inscription : {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        //public async Task<bool> RegisterAsync(RegisterDto registerModel)
        //{
        //    try
        //    {
        //        Console.WriteLine("Tentative d'inscription :");
        //        Console.WriteLine($"Username: {registerModel.Username}");
        //        Console.WriteLine($"Email: {registerModel.Email}");
        //        var response = await _httpClient.PostAsJsonAsync("api/Auth/register", registerModel);
        //        Console.WriteLine($"Statut de la réponse : {response.StatusCode}");
        //        return response.IsSuccessStatusCode;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Erreur d'inscription : {ex.Message}");
        //        throw;
        //    }
        //}

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("userId");
            await _localStorage.RemoveItemAsync("username");
        }
        public async Task<bool> IsTokenExpired()
        {
            // Récupérez la date d'expiration depuis le localStorage
            var expirationDateString = await _localStorage.GetItemAsync<string>("tokenExpiration");

            if (string.IsNullOrEmpty(expirationDateString))
                return true;

            // Convertissez la chaîne en DateTime
            if (DateTime.TryParse(expirationDateString, out DateTime expirationDate))
            {
                return expirationDate <= DateTime.UtcNow;
            }

            return true;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            // Vérifiez si le token existe ET n'est pas expiré
            return !string.IsNullOrEmpty(token) && !await IsTokenExpired();
        }

        public async Task<string> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }


    }
}
