using MinimalAPI_BookStore.Models.DTOs;

namespace MVC_MinimalAPI.Services
{
	public interface IBookService
	{
		Task<T> GetAllBooks<T>();
		Task<T> GetBookById<T>(int id);
		Task<T> CreateBookAsync<T>(BookCreateDTO bookCreateDTO);
		Task<T> UpdateBookAsync<T>(BookUpdateDTO bookUpdateDTO);
		Task<T> DeleteBookAsync<T>(int id);
	}
}
