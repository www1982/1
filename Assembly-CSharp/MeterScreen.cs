using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;

// Token: 0x02000D62 RID: 3426
public class MeterScreen : KScreen, IRender1000ms
{
	// Token: 0x17000786 RID: 1926
	// (get) Token: 0x06006A31 RID: 27185 RVA: 0x00280C55 File Offset: 0x0027EE55
	// (set) Token: 0x06006A32 RID: 27186 RVA: 0x00280C5C File Offset: 0x0027EE5C
	public static MeterScreen Instance { get; private set; }

	// Token: 0x06006A33 RID: 27187 RVA: 0x00280C64 File Offset: 0x0027EE64
	public static void DestroyInstance()
	{
		MeterScreen.Instance = null;
	}

	// Token: 0x17000787 RID: 1927
	// (get) Token: 0x06006A34 RID: 27188 RVA: 0x00280C6C File Offset: 0x0027EE6C
	public bool StartValuesSet
	{
		get
		{
			return this.startValuesSet;
		}
	}

	// Token: 0x06006A35 RID: 27189 RVA: 0x00280C74 File Offset: 0x0027EE74
	protected override void OnPrefabInit()
	{
		MeterScreen.Instance = this;
	}

	// Token: 0x06006A36 RID: 27190 RVA: 0x00280C7C File Offset: 0x0027EE7C
	protected override void OnSpawn()
	{
		this.RedAlertTooltip.OnToolTip = new Func<string>(this.OnRedAlertTooltip);
		MultiToggle redAlertButton = this.RedAlertButton;
		redAlertButton.onClick = (global::System.Action)Delegate.Combine(redAlertButton.onClick, new global::System.Action(delegate
		{
			this.OnRedAlertClick();
		}));
		Game.Instance.Subscribe(1983128072, delegate(object data)
		{
			this.Refresh();
		});
		Game.Instance.Subscribe(1585324898, delegate(object data)
		{
			this.RefreshRedAlertButtonState();
		});
		Game.Instance.Subscribe(-1393151672, delegate(object data)
		{
			this.RefreshRedAlertButtonState();
		});
	}

	// Token: 0x06006A37 RID: 27191 RVA: 0x00280D1C File Offset: 0x0027EF1C
	private void OnRedAlertClick()
	{
		bool flag = !ClusterManager.Instance.activeWorld.AlertManager.IsRedAlertToggledOn();
		ClusterManager.Instance.activeWorld.AlertManager.ToggleRedAlert(flag);
		if (flag)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Open", false));
			return;
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
	}

	// Token: 0x06006A38 RID: 27192 RVA: 0x00280D7A File Offset: 0x0027EF7A
	private void RefreshRedAlertButtonState()
	{
		this.RedAlertButton.ChangeState(ClusterManager.Instance.activeWorld.IsRedAlert() ? 1 : 0);
	}

	// Token: 0x06006A39 RID: 27193 RVA: 0x00280D9C File Offset: 0x0027EF9C
	public void Render1000ms(float dt)
	{
		this.Refresh();
	}

	// Token: 0x06006A3A RID: 27194 RVA: 0x00280DA4 File Offset: 0x0027EFA4
	public void InitializeValues()
	{
		if (this.startValuesSet)
		{
			return;
		}
		this.startValuesSet = true;
		this.Refresh();
	}

	// Token: 0x06006A3B RID: 27195 RVA: 0x00280DBC File Offset: 0x0027EFBC
	private void Refresh()
	{
		this.RefreshWorldMinionIdentities();
		this.RefreshMinions();
		for (int i = 0; i < this.valueDisplayers.Length; i++)
		{
			this.valueDisplayers[i].Refresh();
		}
		this.RefreshRedAlertButtonState();
	}

	// Token: 0x06006A3C RID: 27196 RVA: 0x00280DFC File Offset: 0x0027EFFC
	private void RefreshWorldMinionIdentities()
	{
		this.worldLiveMinionIdentities = new List<MinionIdentity>(from x in Components.LiveMinionIdentities.GetWorldItems(ClusterManager.Instance.activeWorldId, false)
			where !x.IsNullOrDestroyed()
			select x);
	}

	// Token: 0x06006A3D RID: 27197 RVA: 0x00280E4D File Offset: 0x0027F04D
	private List<MinionIdentity> GetWorldMinionIdentities()
	{
		if (this.worldLiveMinionIdentities == null)
		{
			this.RefreshWorldMinionIdentities();
		}
		return this.worldLiveMinionIdentities;
	}

	// Token: 0x06006A3E RID: 27198 RVA: 0x00280E64 File Offset: 0x0027F064
	private void RefreshMinions()
	{
		int count = Components.LiveMinionIdentities.Count;
		int count2 = this.GetWorldMinionIdentities().Count;
		if (count2 == this.cachedMinionCount)
		{
			return;
		}
		this.cachedMinionCount = count2;
		string text;
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			ClusterGridEntity component = ClusterManager.Instance.activeWorld.GetComponent<ClusterGridEntity>();
			text = string.Format(UI.TOOLTIPS.METERSCREEN_POPULATION_CLUSTER, component.Name, count2, count);
			this.currentMinions.text = string.Format("{0}/{1}", count2, count);
		}
		else
		{
			this.currentMinions.text = string.Format("{0}", count);
			text = string.Format(UI.TOOLTIPS.METERSCREEN_POPULATION, count.ToString("0"));
		}
		this.MinionsTooltip.ClearMultiStringTooltip();
		this.MinionsTooltip.AddMultiStringTooltip(text, this.ToolTipStyle_Header);
	}

	// Token: 0x06006A3F RID: 27199 RVA: 0x00280F50 File Offset: 0x0027F150
	private string OnRedAlertTooltip()
	{
		this.RedAlertTooltip.ClearMultiStringTooltip();
		this.RedAlertTooltip.AddMultiStringTooltip(UI.TOOLTIPS.RED_ALERT_TITLE, this.ToolTipStyle_Header);
		this.RedAlertTooltip.AddMultiStringTooltip(UI.TOOLTIPS.RED_ALERT_CONTENT, this.ToolTipStyle_Property);
		return "";
	}

	// Token: 0x04004870 RID: 18544
	[SerializeField]
	private LocText currentMinions;

	// Token: 0x04004872 RID: 18546
	public ToolTip MinionsTooltip;

	// Token: 0x04004873 RID: 18547
	public MeterScreen_ValueTrackerDisplayer[] valueDisplayers;

	// Token: 0x04004874 RID: 18548
	public TextStyleSetting ToolTipStyle_Header;

	// Token: 0x04004875 RID: 18549
	public TextStyleSetting ToolTipStyle_Property;

	// Token: 0x04004876 RID: 18550
	private bool startValuesSet;

	// Token: 0x04004877 RID: 18551
	public MultiToggle RedAlertButton;

	// Token: 0x04004878 RID: 18552
	public ToolTip RedAlertTooltip;

	// Token: 0x04004879 RID: 18553
	private MeterScreen.DisplayInfo immunityDisplayInfo = new MeterScreen.DisplayInfo
	{
		selectedIndex = -1
	};

	// Token: 0x0400487A RID: 18554
	private List<MinionIdentity> worldLiveMinionIdentities;

	// Token: 0x0400487B RID: 18555
	private int cachedMinionCount = -1;

	// Token: 0x02001F39 RID: 7993
	private struct DisplayInfo
	{
		// Token: 0x04009011 RID: 36881
		public int selectedIndex;
	}
}
