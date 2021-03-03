namespace SmartMirror.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SmartMirror.Dmi;
	using System.Threading.Tasks;

	[ApiController]
	[Route("api/[controller]")]
	public class PollenController : ControllerBase
	{
		private readonly IPollenProvider pollenProvider;

		public PollenController(IPollenProvider pollenProvider)
		{
			this.pollenProvider = pollenProvider;
		}

		[HttpGet]
		public Task<PollenNumbers> Get()
		{
			return this.pollenProvider.GetPollenNumbers();
		}
	}
}
