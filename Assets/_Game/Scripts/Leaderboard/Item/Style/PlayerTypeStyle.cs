using UnityEngine;
using System;

namespace Leaderboard
{
	[Serializable]
	public struct PlayerTypeStyle
	{
		public string type;
		[Range(60, 90)] public int fontSize;
		public Color color;
	}
}