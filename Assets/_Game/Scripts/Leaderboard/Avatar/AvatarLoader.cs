using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Threading;
using UnityEngine;

namespace Leaderboard.Avatar
{
	public sealed class AvatarLoader : IAvatarLoader
	{
		private readonly Dictionary<string, Sprite> _cache = new();

		public async Task<Sprite> LoadSpriteAsync(string url, int ppu, CancellationToken ct)
		{
			if (string.IsNullOrWhiteSpace(url)) return null;
			if (_cache.TryGetValue(url, out var s) && s) return s;

			using var req = UnityWebRequestTexture.GetTexture(url, false);
			var op = req.SendWebRequest();
			while (!op.isDone)
			{
				if (ct.IsCancellationRequested) throw new TaskCanceledException();
				await Task.Yield();
			}
			if (req.result != UnityWebRequest.Result.Success) return null;

			var tex = DownloadHandlerTexture.GetContent(req);
			if (!tex) return null;

			var sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(0.5f,0.5f), ppu <= 0 ? 100 : ppu);
			_cache[url] = sprite;
			return sprite;
		}
	}
}