using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020007FD RID: 2045
[AddComponentMenu("KMonoBehaviour/Workable/Butcherable")]
public class Butcherable : Workable, ISaveLoadable
{
	// Token: 0x060037A3 RID: 14243 RVA: 0x00134B10 File Offset: 0x00132D10
	public void SetDrops(string[] drops)
	{
		Dictionary<string, float> dictionary = new Dictionary<string, float>();
		for (int i = 0; i < drops.Length; i++)
		{
			if (!dictionary.ContainsKey(drops[i]))
			{
				dictionary.Add(drops[i], 0f);
			}
			Dictionary<string, float> dictionary2 = dictionary;
			string text = drops[i];
			dictionary2[text] += 1f;
		}
		this.SetDrops(dictionary);
	}

	// Token: 0x060037A4 RID: 14244 RVA: 0x00134B6B File Offset: 0x00132D6B
	public void SetDrops(Dictionary<string, float> drops)
	{
		this.drops = drops;
	}

	// Token: 0x060037A5 RID: 14245 RVA: 0x00134B74 File Offset: 0x00132D74
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Butcherable>(1272413801, Butcherable.SetReadyToButcherDelegate);
		base.Subscribe<Butcherable>(493375141, Butcherable.OnRefreshUserMenuDelegate);
		this.workTime = 3f;
		this.multitoolContext = "harvest";
		this.multitoolHitEffectTag = "fx_harvest_splash";
	}

	// Token: 0x060037A6 RID: 14246 RVA: 0x00134BD4 File Offset: 0x00132DD4
	public void SetReadyToButcher(object param)
	{
		this.readyToButcher = true;
	}

	// Token: 0x060037A7 RID: 14247 RVA: 0x00134BDD File Offset: 0x00132DDD
	public void SetReadyToButcher(bool ready)
	{
		this.readyToButcher = ready;
	}

	// Token: 0x060037A8 RID: 14248 RVA: 0x00134BE8 File Offset: 0x00132DE8
	public void ActivateChore(object param)
	{
		if (this.chore != null)
		{
			return;
		}
		this.chore = new WorkChore<Butcherable>(Db.Get().ChoreTypes.Harvest, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		this.OnRefreshUserMenu(null);
	}

	// Token: 0x060037A9 RID: 14249 RVA: 0x00134C31 File Offset: 0x00132E31
	public void CancelChore(object param)
	{
		if (this.chore == null)
		{
			return;
		}
		this.chore.Cancel("User cancelled");
		this.chore = null;
	}

	// Token: 0x060037AA RID: 14250 RVA: 0x00134C53 File Offset: 0x00132E53
	private void OnClickCancel()
	{
		this.CancelChore(null);
	}

	// Token: 0x060037AB RID: 14251 RVA: 0x00134C5C File Offset: 0x00132E5C
	private void OnClickButcher()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.OnButcherComplete();
			return;
		}
		this.ActivateChore(null);
	}

	// Token: 0x060037AC RID: 14252 RVA: 0x00134C74 File Offset: 0x00132E74
	private void OnRefreshUserMenu(object data)
	{
		if (!this.readyToButcher)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((this.chore != null) ? new KIconButtonMenu.ButtonInfo("action_harvest", "Cancel Meatify", new global::System.Action(this.OnClickCancel), global::Action.NumActions, null, null, null, "", true) : new KIconButtonMenu.ButtonInfo("action_harvest", "Meatify", new global::System.Action(this.OnClickButcher), global::Action.NumActions, null, null, null, "", true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x060037AD RID: 14253 RVA: 0x00134D02 File Offset: 0x00132F02
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.OnButcherComplete();
	}

	// Token: 0x060037AE RID: 14254 RVA: 0x00134D0C File Offset: 0x00132F0C
	public GameObject[] CreateDrops(float multiplier = 1f)
	{
		GameObject[] array = new GameObject[this.drops.Count];
		int num = 0;
		foreach (KeyValuePair<string, float> keyValuePair in this.drops)
		{
			GameObject gameObject = Scenario.SpawnPrefab(this.GetDropSpawnLocation(), 0, 0, keyValuePair.Key, Grid.SceneLayer.Ore);
			gameObject.SetActive(true);
			gameObject.GetComponent<PrimaryElement>().Mass = gameObject.GetComponent<PrimaryElement>().Mass * multiplier * keyValuePair.Value;
			Edible component = gameObject.GetComponent<Edible>();
			if (component)
			{
				ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, component.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.BUTCHERED, "{0}", gameObject.GetProperName()), UI.ENDOFDAYREPORT.NOTES.BUTCHERED_CONTEXT);
			}
			array[num] = gameObject;
			num++;
		}
		return array;
	}

	// Token: 0x060037AF RID: 14255 RVA: 0x00134E08 File Offset: 0x00133008
	public void OnButcherComplete()
	{
		if (this.butchered)
		{
			return;
		}
		KSelectable component = base.GetComponent<KSelectable>();
		if (component && component.IsSelected)
		{
			SelectTool.Instance.Select(null, false);
		}
		Pickupable component2 = base.GetComponent<Pickupable>();
		Storage storage = ((component2 != null) ? component2.storage : null);
		GameObject[] array = this.CreateDrops(1f);
		if (array != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (storage != null && storage.storeDropsFromButcherables)
				{
					storage.Store(array[i], false, false, true, false);
				}
			}
		}
		this.chore = null;
		this.butchered = true;
		this.readyToButcher = false;
		Game.Instance.userMenu.Refresh(base.gameObject);
		base.Trigger(395373363, array);
	}

	// Token: 0x060037B0 RID: 14256 RVA: 0x00134ED4 File Offset: 0x001330D4
	private int GetDropSpawnLocation()
	{
		int num = Grid.PosToCell(base.gameObject);
		int num2 = Grid.CellAbove(num);
		if (Grid.IsValidCell(num2) && !Grid.Solid[num2])
		{
			return num2;
		}
		return num;
	}

	// Token: 0x040021BE RID: 8638
	[MyCmpGet]
	private KAnimControllerBase controller;

	// Token: 0x040021BF RID: 8639
	[MyCmpGet]
	private Harvestable harvestable;

	// Token: 0x040021C0 RID: 8640
	private bool readyToButcher;

	// Token: 0x040021C1 RID: 8641
	private bool butchered;

	// Token: 0x040021C2 RID: 8642
	public Dictionary<string, float> drops;

	// Token: 0x040021C3 RID: 8643
	private Chore chore;

	// Token: 0x040021C4 RID: 8644
	private static readonly EventSystem.IntraObjectHandler<Butcherable> SetReadyToButcherDelegate = new EventSystem.IntraObjectHandler<Butcherable>(delegate(Butcherable component, object data)
	{
		component.SetReadyToButcher(data);
	});

	// Token: 0x040021C5 RID: 8645
	private static readonly EventSystem.IntraObjectHandler<Butcherable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Butcherable>(delegate(Butcherable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
