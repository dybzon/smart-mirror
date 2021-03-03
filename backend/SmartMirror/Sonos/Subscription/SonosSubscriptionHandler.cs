namespace SmartMirror.Sonos.Subscription
{
	using Microsoft.AspNetCore.Http;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class SonosSubscriptionHandler : ISonosSubscriptionHandler
	{
		private readonly Dictionary<Guid, HttpContext> subscribers = new Dictionary<Guid, HttpContext>();

		public async Task PostUpdate(StateUpdate update)
		{
			// We only want "transport-state" updates, which are sent when the current track changes.
			// Volume changes etc. are not relevant unless we start displaying volume in the UI.
			if (!update.Type.Equals("transport-state"))
			{
				return;
			}

			// We do not want to send updates if they do not identify the unit (speaker) they belong to.
			if(update.Data?.uuid == null)
			{
				return;
			}

			// Send the update as json to each subscriber
			foreach (var httpContext in this.subscribers.Values)
			{
				var response = httpContext.Response;
				var data = update.Data;
				await response.WriteAsync("data: {\n");
				await response.WriteAsync($"data: \"uuid\": \"{data.uuid}\",\n");
				await response.WriteAsync($"data: \"messagetype\": \"{update.Type}\"");

				var state = data.state;
				if(state != null)
				{
					await response.WriteAsync(",\n");
					await response.WriteAsync($"data: \"playbackState\": \"{state.playbackState}\"");
				}

				var currentTrack = state?.currentTrack;
				if (currentTrack != null)
				{
					await response.WriteAsync(",\n");
					await response.WriteAsync($"data: \"artist\": \"{currentTrack.artist}\",\n");
					await response.WriteAsync($"data: \"title\": \"{currentTrack.title}\",\n");
					await response.WriteAsync($"data: \"album\": \"{currentTrack.album}\",\n");
					await response.WriteAsync($"data: \"type\": \"{currentTrack.type}\",\n");
					await response.WriteAsync($"data: \"absoluteAlbumArtUri\": \"{currentTrack.absoluteAlbumArtUri}\"");
				}

				await response.WriteAsync("\n");
				await response.WriteAsync("data: }\r\r");
				await response.Body.FlushAsync();
			}
		}

		public void Register(Guid uid, HttpContext context)
		{
			this.subscribers.Add(uid, context);
		}

		public void Remove(Guid uid)
		{
			this.subscribers.Remove(uid);
		}
	}
}
