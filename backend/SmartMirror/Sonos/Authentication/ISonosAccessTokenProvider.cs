namespace SmartMirror.Sonos.Authentication
{
	using System.Threading.Tasks;

	public interface ISonosAccessTokenProvider
	{
		Task<string> GetAccessToken();
	}
}
