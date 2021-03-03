namespace SmartMirror.Tado.Basic
{
	using System.Threading.Tasks;

	public interface ITadoBasicInfoProvider
	{
		Task<TadoBasicInfo> GetBasicInfo();
	}
}
