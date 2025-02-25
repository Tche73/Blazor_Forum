using Azure;
using Blazor_Forum.Models;

namespace Blazor_Forum.Services
{
    public interface IForumService
    {
        //Message
        Task<List<Message>> GetMessagesAsync();
        Task<Message> GetMessageAsync(int id);
        Task<Message> CreateMessageAsync(Message message);
        Task UpdateMessageAsync(int id, Message message);
        Task DeleteMessageAsync(int id);
        //Reponse
        Task<List<Reponse>> GetResponsesAsync();
        Task<List<Reponse>> GetResponsesByMessageAsync(int messageId);
        Task<Reponse> CreateResponseAsync(Reponse reponse);
        // User
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        // Like
        Task<Like> AddLikeAsync(Like like);
        Task RemoveLikeAsync(int id);


    }
}
