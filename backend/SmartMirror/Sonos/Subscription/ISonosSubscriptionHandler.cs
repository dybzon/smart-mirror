namespace SmartMirror.Sonos.Subscription
{
	using Microsoft.AspNetCore.Http;
	using System;
	using System.Threading.Tasks;

	public interface ISonosSubscriptionHandler
	{
		Task PostUpdate(StateUpdate update);

		void Register(Guid uid, HttpContext context);

		void Remove(Guid uid);
	}
}
