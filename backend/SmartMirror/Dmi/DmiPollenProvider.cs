namespace SmartMirror.Dmi
{
	using System.Threading.Tasks;

	public class DmiPollenProvider : IPollenProvider
	{
		private readonly IDmiPollenFetcher fetcher;
		private readonly IDmiPollenReader reader;

		public DmiPollenProvider(IDmiPollenFetcher fetcher, IDmiPollenReader reader)
		{
			this.fetcher = fetcher;
			this.reader = reader;
		}

		public async Task<PollenNumbers> GetPollenNumbers()
		{
			var html = await this.fetcher.GetPollenHtml();
			return this.reader.ReadPollenNumbers(html);
		}
	}
}
