using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000E69 RID: 3689
[AddComponentMenu("KMonoBehaviour/scripts/ToolParameterMenu")]
public class ToolParameterMenu : KMonoBehaviour
{
	// Token: 0x14000031 RID: 49
	// (add) Token: 0x060075B4 RID: 30132 RVA: 0x002D0F24 File Offset: 0x002CF124
	// (remove) Token: 0x060075B5 RID: 30133 RVA: 0x002D0F5C File Offset: 0x002CF15C
	public event global::System.Action onParametersChanged;

	// Token: 0x060075B6 RID: 30134 RVA: 0x002D0F91 File Offset: 0x002CF191
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.ClearMenu();
	}

	// Token: 0x060075B7 RID: 30135 RVA: 0x002D0FA0 File Offset: 0x002CF1A0
	public void PopulateMenu(Dictionary<string, ToolParameterMenu.ToggleState> parameters)
	{
		this.ClearMenu();
		this.currentParameters = parameters;
		foreach (KeyValuePair<string, ToolParameterMenu.ToggleState> keyValuePair in parameters)
		{
			GameObject gameObject = Util.KInstantiateUI(this.widgetPrefab, this.widgetContainer, true);
			gameObject.GetComponentInChildren<LocText>().text = Strings.Get("STRINGS.UI.TOOLS.FILTERLAYERS." + keyValuePair.Key + ".NAME");
			ToolTip componentInChildren = gameObject.GetComponentInChildren<ToolTip>();
			if (componentInChildren != null)
			{
				componentInChildren.SetSimpleTooltip(Strings.Get("STRINGS.UI.TOOLS.FILTERLAYERS." + keyValuePair.Key + ".TOOLTIP"));
			}
			this.widgets.Add(keyValuePair.Key, gameObject);
			MultiToggle toggle = gameObject.GetComponentInChildren<MultiToggle>();
			ToolParameterMenu.ToggleState value = keyValuePair.Value;
			if (value == ToolParameterMenu.ToggleState.Disabled)
			{
				toggle.ChangeState(2);
			}
			else if (value == ToolParameterMenu.ToggleState.On)
			{
				toggle.ChangeState(1);
				this.lastEnabledFilter = keyValuePair.Key;
			}
			else
			{
				toggle.ChangeState(0);
			}
			MultiToggle toggle2 = toggle;
			toggle2.onClick = (global::System.Action)Delegate.Combine(toggle2.onClick, new global::System.Action(delegate
			{
				foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.widgets)
				{
					if (keyValuePair2.Value == toggle.transform.parent.gameObject)
					{
						if (this.currentParameters[keyValuePair2.Key] == ToolParameterMenu.ToggleState.Disabled)
						{
							break;
						}
						this.ChangeToSetting(keyValuePair2.Key);
						this.OnChange();
						break;
					}
				}
			}));
		}
		this.content.SetActive(true);
	}

	// Token: 0x060075B8 RID: 30136 RVA: 0x002D1124 File Offset: 0x002CF324
	public void ClearMenu()
	{
		this.content.SetActive(false);
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.widgets)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.widgets.Clear();
	}

	// Token: 0x060075B9 RID: 30137 RVA: 0x002D1194 File Offset: 0x002CF394
	private void ChangeToSetting(string key)
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.widgets)
		{
			if (this.currentParameters[keyValuePair.Key] != ToolParameterMenu.ToggleState.Disabled)
			{
				this.currentParameters[keyValuePair.Key] = ToolParameterMenu.ToggleState.Off;
			}
		}
		this.currentParameters[key] = ToolParameterMenu.ToggleState.On;
	}

	// Token: 0x060075BA RID: 30138 RVA: 0x002D1218 File Offset: 0x002CF418
	private void OnChange()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.widgets)
		{
			switch (this.currentParameters[keyValuePair.Key])
			{
			case ToolParameterMenu.ToggleState.On:
				keyValuePair.Value.GetComponentInChildren<MultiToggle>().ChangeState(1);
				this.lastEnabledFilter = keyValuePair.Key;
				break;
			case ToolParameterMenu.ToggleState.Off:
				keyValuePair.Value.GetComponentInChildren<MultiToggle>().ChangeState(0);
				break;
			case ToolParameterMenu.ToggleState.Disabled:
				keyValuePair.Value.GetComponentInChildren<MultiToggle>().ChangeState(2);
				break;
			}
		}
		if (this.onParametersChanged != null)
		{
			this.onParametersChanged();
		}
	}

	// Token: 0x060075BB RID: 30139 RVA: 0x002D12E8 File Offset: 0x002CF4E8
	public string GetLastEnabledFilter()
	{
		return this.lastEnabledFilter;
	}

	// Token: 0x040051A3 RID: 20899
	public GameObject content;

	// Token: 0x040051A4 RID: 20900
	public GameObject widgetContainer;

	// Token: 0x040051A5 RID: 20901
	public GameObject widgetPrefab;

	// Token: 0x040051A7 RID: 20903
	private Dictionary<string, GameObject> widgets = new Dictionary<string, GameObject>();

	// Token: 0x040051A8 RID: 20904
	private Dictionary<string, ToolParameterMenu.ToggleState> currentParameters;

	// Token: 0x040051A9 RID: 20905
	private string lastEnabledFilter;

	// Token: 0x02002062 RID: 8290
	public class FILTERLAYERS
	{
		// Token: 0x040093EB RID: 37867
		public static string BUILDINGS = "BUILDINGS";

		// Token: 0x040093EC RID: 37868
		public static string TILES = "TILES";

		// Token: 0x040093ED RID: 37869
		public static string WIRES = "WIRES";

		// Token: 0x040093EE RID: 37870
		public static string LIQUIDCONDUIT = "LIQUIDPIPES";

		// Token: 0x040093EF RID: 37871
		public static string GASCONDUIT = "GASPIPES";

		// Token: 0x040093F0 RID: 37872
		public static string SOLIDCONDUIT = "SOLIDCONDUITS";

		// Token: 0x040093F1 RID: 37873
		public static string CLEANANDCLEAR = "CLEANANDCLEAR";

		// Token: 0x040093F2 RID: 37874
		public static string DIGPLACER = "DIGPLACER";

		// Token: 0x040093F3 RID: 37875
		public static string LOGIC = "LOGIC";

		// Token: 0x040093F4 RID: 37876
		public static string BACKWALL = "BACKWALL";

		// Token: 0x040093F5 RID: 37877
		public static string CONSTRUCTION = "CONSTRUCTION";

		// Token: 0x040093F6 RID: 37878
		public static string DIG = "DIG";

		// Token: 0x040093F7 RID: 37879
		public static string CLEAN = "CLEAN";

		// Token: 0x040093F8 RID: 37880
		public static string OPERATE = "OPERATE";

		// Token: 0x040093F9 RID: 37881
		public static string METAL = "METAL";

		// Token: 0x040093FA RID: 37882
		public static string BUILDABLE = "BUILDABLE";

		// Token: 0x040093FB RID: 37883
		public static string FILTER = "FILTER";

		// Token: 0x040093FC RID: 37884
		public static string LIQUIFIABLE = "LIQUIFIABLE";

		// Token: 0x040093FD RID: 37885
		public static string LIQUID = "LIQUID";

		// Token: 0x040093FE RID: 37886
		public static string CONSUMABLEORE = "CONSUMABLEORE";

		// Token: 0x040093FF RID: 37887
		public static string ORGANICS = "ORGANICS";

		// Token: 0x04009400 RID: 37888
		public static string FARMABLE = "FARMABLE";

		// Token: 0x04009401 RID: 37889
		public static string GAS = "GAS";

		// Token: 0x04009402 RID: 37890
		public static string MISC = "MISC";

		// Token: 0x04009403 RID: 37891
		public static string HEATFLOW = "HEATFLOW";

		// Token: 0x04009404 RID: 37892
		public static string ABSOLUTETEMPERATURE = "ABSOLUTETEMPERATURE";

		// Token: 0x04009405 RID: 37893
		public static string RELATIVETEMPERATURE = "RELATIVETEMPERATURE";

		// Token: 0x04009406 RID: 37894
		public static string ADAPTIVETEMPERATURE = "ADAPTIVETEMPERATURE";

		// Token: 0x04009407 RID: 37895
		public static string STATECHANGE = "STATECHANGE";

		// Token: 0x04009408 RID: 37896
		public static string ALL = "ALL";
	}

	// Token: 0x02002063 RID: 8291
	public enum ToggleState
	{
		// Token: 0x0400940A RID: 37898
		On,
		// Token: 0x0400940B RID: 37899
		Off,
		// Token: 0x0400940C RID: 37900
		Disabled
	}
}
