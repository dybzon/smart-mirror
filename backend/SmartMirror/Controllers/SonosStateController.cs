namespace SmartMirror.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SmartMirror.Sonos.Subscription;
	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// A controller for sonos data.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class SonosStateController : ControllerBase
	{
		private readonly ISonosSubscriptionHandler subscriptionHandler;

		public SonosStateController(ISonosSubscriptionHandler subscriptionHandler)
		{
			this.subscriptionHandler = subscriptionHandler;
		}

		[HttpPost]
		public async Task Post([FromBody] StateUpdate update)
		{
			await this.subscriptionHandler.PostUpdate(update);
		}

		[HttpGet("subscribe")]
		public Task Subscribe()
		{
			var response = this.Response;
			response.Headers.Add("Content-Type", "text/event-stream");
			var uid = Guid.NewGuid();

			this.subscriptionHandler.Register(uid, this.HttpContext);

			// Await completion until the connection is cancelled by the client
			this.HttpContext.RequestAborted.WaitHandle.WaitOne();

			// Remove the context from registry when the connection is closed by the client
			this.subscriptionHandler.Remove(uid);

			return Task.CompletedTask;
		}
	}
}
