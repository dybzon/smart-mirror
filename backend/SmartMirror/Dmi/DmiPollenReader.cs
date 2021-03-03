namespace SmartMirror.Dmi
{
	using HtmlAgilityPack;
	using SmartMirror.Extensions;
	using System.Collections.Generic;
	using System.Linq;

	public class DmiPollenReader : IDmiPollenReader
	{
		private const string BirchName = "Birk";

		private const string GrassName = "Græs";

		public PollenNumbers ReadPollenNumbers(string html)
		{
			var document = new HtmlDocument();
			document.LoadHtml(html);
			var table = this.GetTable(document);
			if(table is null)
			{
				return null;
			}

			var keys = this.GetPollenKeys(table);
			var values = this.GetPollenValues(table);
			var pollenDictionary = this.GetPollenDictionary(keys, values);
			return this.GetPollenNumbers(pollenDictionary);
		}

		private HtmlNode GetTable(HtmlDocument html)
		{
			var tables = html.DocumentNode.SelectNodes("//table");
			var firstTable = tables.FirstOrDefault(t => {
				var nodes = t.SelectNodes("./tbody/tr/th");
				var firstNode = nodes.First();
				var firstNodeText = firstNode.InnerText;
				var isCph = firstNodeText.Contains("København", System.StringComparison.OrdinalIgnoreCase);
				return isCph;
			});
			return firstTable;
		}

		private List<string> GetPollenKeys(HtmlNode table)
		{
			return table.SelectNodes("./thead/tr//th")
				.Select(node => node.InnerHtml)
				.Where(v => !string.IsNullOrWhiteSpace(v))
				.ToList();
		}

		private List<int> GetPollenValues(HtmlNode table)
		{
			return table.SelectNodes("./tbody/tr//td")
				.Select(node => node.InnerHtml)
				.Select(v => int.TryParse(v, out int result) ? result : 0)
				.ToList();
		}

		private IDictionary<string, int> GetPollenDictionary(List<string> keys, List<int> values)
		{
			var dictionary = new Dictionary<string, int>();
			var index = 0;
			foreach(var key in keys)
			{
				dictionary.Add(key, values.ElementAtOrDefault(index));
				index++;
			}

			return dictionary;
		}

		private PollenNumbers GetPollenNumbers(IDictionary<string, int> pollenDictionary)
		{
			return new PollenNumbers
			{
				Birch = pollenDictionary.GetValueOrDefault(BirchName),
				Grass = pollenDictionary.GetValueOrDefault(GrassName),
			};
		}
	}
}
