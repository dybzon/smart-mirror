namespace SmartMirror.Dmi
{
	using System.Threading.Tasks;

	public interface IPollenProvider
	{
		Task<PollenNumbers> GetPollenNumbers();
	}
}
