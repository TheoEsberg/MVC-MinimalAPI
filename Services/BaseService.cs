using MVC_MinimalAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC_MinimalAPI.Services
{
	public class BaseService : IBaseService
	{
		public ResponseDTO responseModel { get; set; }
		public IHttpClientFactory _httpClient { get; set; }

		public BaseService(IHttpClientFactory httpClient) 
		{ 
			this._httpClient = httpClient;
			this.responseModel = new ResponseDTO();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(true);
		}

		public async Task<T> SendAsync<T>(APIRequest apiRequest)
		{
			try
			{
				var client = _httpClient.CreateClient("MinimalBookStoreAPI");
				HttpRequestMessage message = new HttpRequestMessage();
				message.Headers.Add("Accept", "application/json");
				message.RequestUri = new Uri(apiRequest.URL);
				client.DefaultRequestHeaders.Clear();
				if (apiRequest.Data != null) 
				{ 
					message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
						Encoding.UTF8,
						"application/json");
				}

				HttpResponseMessage apiResponse = null;
				switch (apiRequest.APIType)
				{
					case StaticDetails.APIType.GET:
						message.Method = HttpMethod.Get;
						break;
					case StaticDetails.APIType.POST:
						message.Method = HttpMethod.Post;
						break;
					case StaticDetails.APIType.PUT:
						message.Method = HttpMethod.Put;
						break;
					case StaticDetails.APIType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
				}
				apiResponse = await client.SendAsync(message);

				var apiContent = await apiResponse.Content.ReadAsStringAsync();
				var apiResponseDTO = JsonConvert.DeserializeObject<T>(apiContent);
				return apiResponseDTO;
			}
			catch (Exception ex)
			{
				var dto = new ResponseDTO
				{
					DisplayMessage = "Error",
					ErrorMessages = new List<string> { ex.Message },
					IsSuccess = false
				};
				
				var res = JsonConvert.SerializeObject(dto);
				var apiResponseDTO = JsonConvert.DeserializeObject<T>(res);
				return apiResponseDTO;
			}
		}
	}
}
