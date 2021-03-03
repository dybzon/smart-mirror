namespace SmartMirror.Tado.Authentication
{
	using System.Threading.Tasks;

	public interface ITadoAccessTokenProvider
	{
		Task<string> GetAccessToken();
	}
}
