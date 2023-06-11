using FnId = Bertis.Runtime.FunctionUtility.Id;

namespace Bertis.Test
{
	using UnityEngine;
	using Bertis.Runtime;
	using Bertis.Game;
	using System;

	public class Tester : MonoBehaviour
	{
		private void Awake()
		{
			Stage.OnProgress += OnStageCleared;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				StageHandler.GotoNextStage();
			}
		}

		private void OnStageCleared(int left)
		{
			Diag.Log($"Progress: {left}");
		}

	}
}