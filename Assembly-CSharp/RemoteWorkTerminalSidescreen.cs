using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E24 RID: 3620
public class RemoteWorkTerminalSidescreen : SideScreenContent
{
	// Token: 0x06007279 RID: 29305 RVA: 0x002B83EA File Offset: 0x002B65EA
	public override string GetTitle()
	{
		return UI.UISIDESCREENS.REMOTE_WORK_TERMINAL_SIDE_SCREEN.TITLE;
	}

	// Token: 0x0600727A RID: 29306 RVA: 0x002B83F6 File Offset: 0x002B65F6
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		this.rowPrefab.SetActive(false);
		if (show)
		{
			this.RefreshOptions(null);
		}
	}

	// Token: 0x0600727B RID: 29307 RVA: 0x002B8415 File Offset: 0x002B6615
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<RemoteWorkTerminal>() != null;
	}

	// Token: 0x0600727C RID: 29308 RVA: 0x002B8423 File Offset: 0x002B6623
	public override void SetTarget(GameObject target)
	{
		this.targetTerminal = target.GetComponent<RemoteWorkTerminal>();
		this.RefreshOptions(null);
		this.uiRefreshSubHandle = target.Subscribe(1980521255, new Action<object>(this.RefreshOptions));
	}

	// Token: 0x0600727D RID: 29309 RVA: 0x002B8455 File Offset: 0x002B6655
	public override void ClearTarget()
	{
		if (this.uiRefreshSubHandle != -1 && this.targetTerminal != null)
		{
			this.targetTerminal.gameObject.Unsubscribe(this.uiRefreshSubHandle);
			this.uiRefreshSubHandle = -1;
		}
	}

	// Token: 0x0600727E RID: 29310 RVA: 0x002B848C File Offset: 0x002B668C
	private void RefreshOptions(object data = null)
	{
		int num = 0;
		this.SetRow(num++, UI.UISIDESCREENS.REMOTE_WORK_TERMINAL_SIDE_SCREEN.NOTHING_SELECTED, Assets.GetSprite("action_building_disabled"), null);
		foreach (RemoteWorkerDock remoteWorkerDock in Components.RemoteWorkerDocks.GetItems(this.targetTerminal.GetMyWorldId()))
		{
			remoteWorkerDock.GetProperName();
			Sprite first = Def.GetUISprite(remoteWorkerDock.gameObject, "ui", false).first;
			int num2 = num++;
			string text = UI.StripLinkFormatting(remoteWorkerDock.GetProperName());
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(remoteWorkerDock.gameObject, "ui", false);
			this.SetRow(num2, text, (uisprite != null) ? uisprite.first : null, remoteWorkerDock);
		}
		for (int i = num; i < this.rowContainer.childCount; i++)
		{
			this.rowContainer.GetChild(i).gameObject.SetActive(false);
		}
	}

	// Token: 0x0600727F RID: 29311 RVA: 0x002B8590 File Offset: 0x002B6790
	private void ClearRows()
	{
		for (int i = this.rowContainer.childCount - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.rowContainer.GetChild(i));
		}
		this.rows.Clear();
	}

	// Token: 0x06007280 RID: 29312 RVA: 0x002B85D4 File Offset: 0x002B67D4
	private void SetRow(int idx, string name, Sprite icon, RemoteWorkerDock dock)
	{
		dock == null;
		GameObject gameObject;
		if (idx < this.rowContainer.childCount)
		{
			gameObject = this.rowContainer.GetChild(idx).gameObject;
		}
		else
		{
			gameObject = Util.KInstantiateUI(this.rowPrefab, this.rowContainer.gameObject, true);
		}
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		LocText reference = component.GetReference<LocText>("label");
		reference.text = name;
		reference.ApplySettings();
		Image reference2 = component.GetReference<Image>("icon");
		reference2.sprite = icon;
		reference2.color = Color.white;
		ToolTip toolTip = gameObject.GetComponentsInChildren<ToolTip>().First<ToolTip>();
		toolTip.SetSimpleTooltip(UI.UISIDESCREENS.REMOTE_WORK_TERMINAL_SIDE_SCREEN.DOCK_TOOLTIP);
		toolTip.enabled = dock != null;
		MultiToggle component2 = gameObject.GetComponent<MultiToggle>();
		component2.ChangeState((this.targetTerminal.FutureDock == dock) ? 1 : 0);
		component2.onClick = delegate
		{
			this.targetTerminal.FutureDock = dock;
			this.RefreshOptions(null);
		};
		component2.onDoubleClick = delegate
		{
			GameUtil.FocusCamera((dock == null) ? this.targetTerminal.transform.GetPosition() : dock.transform.GetPosition(), 2f, true, true);
			return true;
		};
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
	}

	// Token: 0x04004ED4 RID: 20180
	private RemoteWorkTerminal targetTerminal;

	// Token: 0x04004ED5 RID: 20181
	public GameObject rowPrefab;

	// Token: 0x04004ED6 RID: 20182
	public RectTransform rowContainer;

	// Token: 0x04004ED7 RID: 20183
	public Dictionary<object, GameObject> rows = new Dictionary<object, GameObject>();

	// Token: 0x04004ED8 RID: 20184
	private int uiRefreshSubHandle = -1;
}
