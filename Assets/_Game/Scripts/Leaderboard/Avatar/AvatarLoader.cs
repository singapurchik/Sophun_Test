using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Threading;
using UnityEngine;

namespace Leaderboard.Avatar
{
	public sealed class AvatarLoader : IAvatarLoader
	{
		private readonly Dictionary<string, Sprite> _cache = new ();

		public async Task<Sprite> LoadSpriteAsync(string url, int ppu, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(url))
				return null;
			
			if (_cache.TryGetValue(url, out var spriteAsync) && spriteAsync)
				return spriteAsync;

			using var req = UnityWebRequestTexture.GetTexture(url, false);
			
			var operation = req.SendWebRequest();
			
			while (!operation.isDone)
			{
				if (cancellationToken.IsCancellationRequested) throw new TaskCanceledException();
				await Task.Yield();
			}
			
			if (req.result != UnityWebRequest.Result.Success)
				return null;

			var texture = DownloadHandlerTexture.GetContent(req);
			
			if (!texture)
				return null;

			var sprite = Sprite.Create(texture, new Rect(0,0,texture.width,texture.height),
				new Vector2(0.5f,0.5f), ppu <= 0 ? 100 : ppu);
			
			_cache[url] = sprite;
			return sprite;
		}
	}
}