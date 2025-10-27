using UnityEngine;
using System;

namespace Leaderboard
{
	[Serializable]
	public struct PlayerTypeStyle : ISerializationCallbackReceiver
	{
		public string Type;
		[Range(1f, 1.15f)] public float ScaleFactor;
		public Color Color;

		public void OnBeforeSerialize() => ScaleFactor = Mathf.Clamp(ScaleFactor, 1, 1.15f);

		public void OnAfterDeserialize() => ScaleFactor = Mathf.Clamp(ScaleFactor, 1, 1.15f);
	}
}