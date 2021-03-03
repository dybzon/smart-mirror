namespace SmartMirror.Dmi
{
	using System.Threading.Tasks;
	using OpenQA.Selenium.Chrome;

	public class DmiPhantomPollenFetcher : IDmiPollenFetcher
	{
		private const string DmiPollenUrl = "https://www.dmi.dk/danmark/#pollen";

		public Task<string> GetPollenHtml()
		{
			var driver = this.BuildChromeDriver();
			driver.Url = DmiPollenUrl;
			driver.Navigate();
			return Task.FromResult(driver.PageSource);
		}

		private ChromeDriver BuildChromeDriver()
		{
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddArguments("headless");
			return new ChromeDriver(chromeOptions);
		}
	}
}
