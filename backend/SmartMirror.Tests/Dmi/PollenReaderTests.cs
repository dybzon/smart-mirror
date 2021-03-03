using System.Threading.Tasks;
using System.IO;
using Xunit;
using smart_mirror.Dmi;
using FluentAssertions;

namespace SmartMirror.Tests.Dmi
{
	public class PollenReaderTests
	{
		public PollenReaderTests()
		{
			this.Sut = new DmiPollenReader();
		}

		private DmiPollenReader Sut { get; set; }

		[Fact]
		public async Task ShouldReturnZeroPollen_WhenTableContainsNoPollen()
		{
			var html = await File.ReadAllTextAsync("./Dmi/DmiPollenPage-NoPollen.html");
			var result = this.Sut.ReadPollenNumbers(html);
			result.Birch.Should().Be(0);
			result.Grass.Should().Be(0);
		}

		[Fact]
		public async Task ShouldReturnPollen_WhenTableContainsPollen()
		{
			var html = await File.ReadAllTextAsync("./Dmi/DmiPollenPage-WithPollen.html");
			var result = this.Sut.ReadPollenNumbers(html);
			result.Birch.Should().Be(3);
			result.Grass.Should().Be(120);
		}

		// Add more tests with additional scenarios:
		// Test where tables don't exist --> return 0 (design choice, we'll refrain from returning null)
		// Test where table cells contains other strange data, e.g. "blabla"
		// Test where table contains no cells?
		// Test where table headers do not contain grass or birch?

		// Use [Theory] and provide a simple IEnumerable<string, int, int> (file name, birch, grass).
	}
}
