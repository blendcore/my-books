using my_books.Data.Models;
using my_books.Data.ViewModels;
using System.Threading.Tasks;

namespace my_books.Repositories
{
    public interface IServiceBus
    {
        Task SendMessageAsync(BookVM book);
    }
}
