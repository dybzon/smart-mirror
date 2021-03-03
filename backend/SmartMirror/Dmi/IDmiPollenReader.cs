namespace SmartMirror.Dmi
{
	public interface IDmiPollenReader
	{
		PollenNumbers ReadPollenNumbers(string html);
	}
}
