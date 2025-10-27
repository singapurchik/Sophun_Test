using UnityEngine;

namespace Leaderboard
{
	[CreateAssetMenu(fileName = "Player Type Style Set", menuName = "Leaderboard/Player Type Styles")]
	public sealed class PlayerTypeStyleSet : ScriptableObject
	{
		[SerializeField] private PlayerTypeStyle[] _styles;

		public PlayerTypeStyle Get(string keyString)
		{
			if (!string.IsNullOrWhiteSpace(keyString))
			{
				var key = keyString.Trim().ToLowerInvariant();
				
				for (int i = 0; i < (_styles?.Length ?? 0); i++)
				{
					if ((_styles[i].Type ?? "").Trim().ToLowerInvariant() == key)
						return _styles[i];
				}	
			}
			return Default();
		}

		private static PlayerTypeStyle Default()
			=> new PlayerTypeStyle { Type = "default", ScaleFactor = 60, Color = Color.white };
	}
}