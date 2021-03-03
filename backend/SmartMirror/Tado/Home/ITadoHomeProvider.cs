using System.Threading.Tasks;

namespace SmartMirror.Tado.Home
{
	public interface ITadoHomeProvider
	{
		Task<TadoHome> GetHomeDetails(int homeId);
	}
}
