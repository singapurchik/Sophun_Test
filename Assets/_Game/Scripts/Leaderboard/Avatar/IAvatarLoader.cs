using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Leaderboard.Avatar
{
	public interface IAvatarLoader
	{
		public Task<Sprite> LoadSpriteAsync(string url, int pixelsPerUnit, CancellationToken ct);
	}
}