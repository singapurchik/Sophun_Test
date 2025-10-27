using UnityEngine;

namespace Leaderboard
{
	[CreateAssetMenu(fileName = "Player Type Style Set", menuName = "Leaderboard/Player Type Styles")]
	public sealed class PlayerTypeStyleSet : ScriptableObject
	{
		[SerializeField] private PlayerTypeStyle[] _styles;

		public PlayerTypeStyle Get(string t)
		{
			if (string.IsNullOrWhiteSpace(t)) return Default();
			var key = t.Trim().ToLowerInvariant();
			for (int i = 0; i < (_styles?.Length ?? 0); i++)
			{
				if ((_styles[i].type ?? "").Trim().ToLowerInvariant() == key)
					return Clamp(_styles[i]);
			}
			return Default();
		}

		private static PlayerTypeStyle Default() => new PlayerTypeStyle { type = "default", fontSize = 60, color = Color.white };
		private static PlayerTypeStyle Clamp(PlayerTypeStyle s)
		{
			if (s.fontSize < 60) s.fontSize = 60;
			if (s.fontSize > 90) s.fontSize = 90;
			return s;
		}
	}
}