using Blazor_Forum.Models;

namespace Blazor_Forum.Services
{
    public interface IForumService
    {
        Task<List<Message>> GetMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task<Message> CreateMessageAsync(Message message);
        Task<Response> CreateResponseAsync(Response response);
        Task<Like> CreateLikeAsync(Like like);
        Task<bool> DeleteMessageAsync(int id);
        Task<bool> DeleteResponseAsync(int id);
        Task<bool> DeleteLikeAsync(int id);
    }
}
