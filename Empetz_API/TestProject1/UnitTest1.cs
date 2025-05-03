namespace TestProject1
{
	public class UnitTest1
	{
		protected readonly HttpClient _httpClient;
		public UnitTest1()
		{

			_httpClient = new HttpClient();
			_httpClient.BaseAddress =new Uri( "http://localhost:5286");
		//	ApiWebApplicationFactory _factory = new ApiWebApplicationFactory();
		//	_httpClient = _factory.CreateClient();
		//string accessToken = getAccessToken();
		//_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

		}
		[Fact]
		public void Test1()
		{

		}
	}
}