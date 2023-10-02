using static MVC_MinimalAPI.StaticDetails;

namespace MVC_MinimalAPI.Models
{
	public class APIRequest
	{
		public APIType APIType { get; set; }
		public string URL { get; set; }
		public object Data { get; set; }
		public string AccessToken { get; set; }
	}
}
