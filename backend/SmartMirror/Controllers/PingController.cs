using Microsoft.AspNetCore.Mvc;

namespace SmartMirror.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PingController : ControllerBase
	{
		[HttpGet]
		public string Get() => "pong";
	}
}
