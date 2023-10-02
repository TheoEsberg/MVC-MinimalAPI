using Microsoft.Identity.Client;
using Microsoft.OpenApi.Writers;
using MinimalAPI_BookStore.Models.DTOs;
using MVC_MinimalAPI.Models;

namespace MVC_MinimalAPI.Services
{
	public class BookService : BaseService, IBookService
	{
		private readonly IHttpClientFactory _clientFactory;
        public BookService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
			this._clientFactory = clientFactory;
        }

        public async Task<T> CreateBookAsync<T>(BookCreateDTO bookCreateDTO)
		{
			return await this.SendAsync<T>(new APIRequest()
			{
				APIType = StaticDetails.APIType.POST,
				Data = bookCreateDTO,
				URL = StaticDetails.BookApiBase + "/api/AddBook",
				AccessToken = ""
			});
		}

		public async Task<T> DeleteBookAsync<T>(int id)
		{
			return await this.SendAsync<T>(new APIRequest()
			{
				APIType = StaticDetails.APIType.DELETE,
				URL = StaticDetails.BookApiBase + "/api/DeleteBook/" + id,
				AccessToken = ""
			});
		}

		public async Task<T> GetAllBooks<T>()
		{
			return await this.SendAsync<T>(new APIRequest()
			{
				APIType = StaticDetails.APIType.GET,
				URL = StaticDetails.BookApiBase + "/api/books",
				AccessToken = ""
			});
		}

		public async Task<T> GetBookById<T>(int id)
		{
			return await this.SendAsync<T>(new APIRequest()
			{
				APIType = StaticDetails.APIType.GET,
				URL = StaticDetails.BookApiBase + "/api/books/" + id,
				AccessToken = ""
			});
		}

		public async Task<T> UpdateBookAsync<T>(BookUpdateDTO bookUpdateDTO)
		{
			return await this.SendAsync<T>(new APIRequest()
			{
				APIType = StaticDetails.APIType.PUT,	
				URL = StaticDetails.BookApiBase + "/api/UpdateBook",
				Data = bookUpdateDTO,
				AccessToken = ""
			});
		}
	}
}
