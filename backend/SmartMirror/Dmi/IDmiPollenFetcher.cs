namespace SmartMirror.Dmi
{
	using System.Threading.Tasks;

	public interface IDmiPollenFetcher
	{
		Task<string> GetPollenHtml();
	}
}
