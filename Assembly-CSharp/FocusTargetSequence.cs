using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200044B RID: 1099
public static class FocusTargetSequence
{
	// Token: 0x060016D1 RID: 5841 RVA: 0x00081255 File Offset: 0x0007F455
	public static void Start(MonoBehaviour coroutineRunner, FocusTargetSequence.Data sequenceData)
	{
		FocusTargetSequence.sequenceCoroutine = coroutineRunner.StartCoroutine(FocusTargetSequence.RunSequence(sequenceData));
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x00081268 File Offset: 0x0007F468
	public static void Cancel(MonoBehaviour coroutineRunner)
	{
		if (FocusTargetSequence.sequenceCoroutine == null)
		{
			return;
		}
		coroutineRunner.StopCoroutine(FocusTargetSequence.sequenceCoroutine);
		FocusTargetSequence.sequenceCoroutine = null;
		if (FocusTargetSequence.prevSpeed >= 0)
		{
			SpeedControlScreen.Instance.SetSpeed(FocusTargetSequence.prevSpeed);
		}
		if (SpeedControlScreen.Instance.IsPaused && !FocusTargetSequence.wasPaused)
		{
			SpeedControlScreen.Instance.Unpause(false);
		}
		if (!SpeedControlScreen.Instance.IsPaused && FocusTargetSequence.wasPaused)
		{
			SpeedControlScreen.Instance.Pause(false, false);
		}
		FocusTargetSequence.SetUIVisible(true);
		CameraController.Instance.SetWorldInteractive(true);
		SelectTool.Instance.Select(FocusTargetSequence.prevSelected, true);
		FocusTargetSequence.prevSelected = null;
		FocusTargetSequence.wasPaused = false;
		FocusTargetSequence.prevSpeed = -1;
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x00081315 File Offset: 0x0007F515
	public static IEnumerator RunSequence(FocusTargetSequence.Data sequenceData)
	{
		SaveGame.Instance.GetComponent<UserNavigation>();
		CameraController.Instance.FadeOut(1f, 1f, null);
		FocusTargetSequence.prevSpeed = SpeedControlScreen.Instance.GetSpeed();
		SpeedControlScreen.Instance.SetSpeed(0);
		FocusTargetSequence.wasPaused = SpeedControlScreen.Instance.IsPaused;
		if (!FocusTargetSequence.wasPaused)
		{
			SpeedControlScreen.Instance.Pause(false, false);
		}
		PlayerController.Instance.CancelDragging();
		CameraController.Instance.SetWorldInteractive(false);
		yield return CameraController.Instance.activeFadeRoutine;
		FocusTargetSequence.prevSelected = SelectTool.Instance.selected;
		SelectTool.Instance.Select(null, true);
		FocusTargetSequence.SetUIVisible(false);
		ClusterManager.Instance.SetActiveWorld(sequenceData.WorldId);
		ManagementMenu.Instance.CloseAll();
		CameraController.Instance.SnapTo(sequenceData.Target, sequenceData.OrthographicSize);
		if (sequenceData.PopupData != null)
		{
			EventInfoScreen.ShowPopup(sequenceData.PopupData);
		}
		CameraController.Instance.FadeIn(0f, 2f, null);
		if (sequenceData.TargetSize - sequenceData.OrthographicSize > Mathf.Epsilon)
		{
			CameraController.Instance.StartCoroutine(CameraController.Instance.DoCinematicZoom(sequenceData.TargetSize));
		}
		if (sequenceData.CanCompleteCB != null)
		{
			SpeedControlScreen.Instance.Unpause(false);
			while (!sequenceData.CanCompleteCB())
			{
				yield return SequenceUtil.WaitForNextFrame;
			}
			SpeedControlScreen.Instance.Pause(false, false);
		}
		CameraController.Instance.SetWorldInteractive(true);
		SpeedControlScreen.Instance.SetSpeed(FocusTargetSequence.prevSpeed);
		if (SpeedControlScreen.Instance.IsPaused && !FocusTargetSequence.wasPaused)
		{
			SpeedControlScreen.Instance.Unpause(false);
		}
		if (sequenceData.CompleteCB != null)
		{
			sequenceData.CompleteCB();
		}
		FocusTargetSequence.SetUIVisible(true);
		SelectTool.Instance.Select(FocusTargetSequence.prevSelected, true);
		sequenceData.Clear();
		FocusTargetSequence.sequenceCoroutine = null;
		FocusTargetSequence.prevSpeed = -1;
		FocusTargetSequence.wasPaused = false;
		FocusTargetSequence.prevSelected = null;
		yield break;
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x00081324 File Offset: 0x0007F524
	private static void SetUIVisible(bool visible)
	{
		NotificationScreen.Instance.Show(visible);
		OverlayMenu.Instance.Show(visible);
		ManagementMenu.Instance.Show(visible);
		ToolMenu.Instance.Show(visible);
		ToolMenu.Instance.PriorityScreen.Show(visible);
		PinnedResourcesPanel.Instance.Show(visible);
		TopLeftControlScreen.Instance.Show(visible);
		global::DateTime.Instance.Show(visible);
		BuildWatermark.Instance.Show(visible);
		BuildWatermark.Instance.Show(visible);
		ColonyDiagnosticScreen.Instance.Show(visible);
		RootMenu.Instance.Show(visible);
		if (PlanScreen.Instance != null)
		{
			PlanScreen.Instance.Show(visible);
		}
		if (BuildMenu.Instance != null)
		{
			BuildMenu.Instance.Show(visible);
		}
		if (WorldSelector.Instance != null)
		{
			WorldSelector.Instance.Show(visible);
		}
	}

	// Token: 0x04000D5B RID: 3419
	private static Coroutine sequenceCoroutine = null;

	// Token: 0x04000D5C RID: 3420
	private static KSelectable prevSelected = null;

	// Token: 0x04000D5D RID: 3421
	private static bool wasPaused = false;

	// Token: 0x04000D5E RID: 3422
	private static int prevSpeed = -1;

	// Token: 0x02001222 RID: 4642
	public struct Data
	{
		// Token: 0x0600851F RID: 34079 RVA: 0x00338255 File Offset: 0x00336455
		public void Clear()
		{
			this.PopupData = null;
			this.CompleteCB = null;
			this.CanCompleteCB = null;
		}

		// Token: 0x04006538 RID: 25912
		public int WorldId;

		// Token: 0x04006539 RID: 25913
		public float OrthographicSize;

		// Token: 0x0400653A RID: 25914
		public float TargetSize;

		// Token: 0x0400653B RID: 25915
		public Vector3 Target;

		// Token: 0x0400653C RID: 25916
		public EventInfoData PopupData;

		// Token: 0x0400653D RID: 25917
		public global::System.Action CompleteCB;

		// Token: 0x0400653E RID: 25918
		public Func<bool> CanCompleteCB;
	}
}
