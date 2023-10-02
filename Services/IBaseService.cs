using MVC_MinimalAPI.Models;

namespace MVC_MinimalAPI.Services
{
	public interface IBaseService : IDisposable
	{
		ResponseDTO responseModel { get; set; }
		Task<T> SendAsync<T>(APIRequest apiRequest);
	}
}
