
using Blazor_Forum.Models;

namespace Blazor_Forum.Services
{
    public interface IForumService
    {
        // Messages
        Task<List<Message>> GetMessagesAsync();
        Task<Message> GetMessageAsync(int id);
        Task<Message> CreateMessageAsync(Message message);
        Task UpdateMessageAsync(int id, Message message);
        Task DeleteMessageAsync(int id);

        // Reponses
        Task<List<Reponse>> GetReponsesAsync();
        Task<List<Reponse>> GetReponsesByMessageAsync(int messageId);
        Task<Reponse> CreateReponseAsync(Reponse reponse);
        Task<List<Like>> GetLikesByReponseAsync(int reponseId);

        // Users
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);

        // Likes
        Task<Like> AddLikeAsync(Like like);
        Task RemoveLikeAsync(int id);
    }
}
