namespace VV.Common.HttpClients;

public class VVHttpClient
{
	public readonly HttpClient Client;

	public VVHttpClient(HttpClient client)
	{
		Client = client;
	}
}
