using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020005E4 RID: 1508
[AddComponentMenu("KMonoBehaviour/Workable/Moppable")]
public class Moppable : Workable, ISim1000ms, ISim200ms
{
	// Token: 0x06002301 RID: 8961 RVA: 0x000C90C8 File Offset: 0x000C72C8
	private Moppable()
	{
		this.showProgressBar = false;
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x000C9124 File Offset: 0x000C7324
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Mopping;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.childRenderer = base.GetComponentInChildren<MeshRenderer>();
		Prioritizable.AddRef(base.gameObject);
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x000C91A8 File Offset: 0x000C73A8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (!this.IsThereLiquid())
		{
			base.gameObject.DeleteObject();
			return;
		}
		Grid.Objects[Grid.PosToCell(base.gameObject), 8] = base.gameObject;
		new WorkChore<Moppable>(Db.Get().ChoreTypes.Mop, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		base.SetWorkTime(float.PositiveInfinity);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().MiscStatusItems.WaitingForMop, null);
		base.Subscribe<Moppable>(493375141, Moppable.OnRefreshUserMenuDelegate);
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_mop_dirtywater_kanim") };
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Moppable.OnSpawn", base.gameObject, new Extents(Grid.PosToCell(this), new CellOffset[]
		{
			new CellOffset(0, 0)
		}), GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnLiquidChanged));
		this.Refresh();
		base.Subscribe<Moppable>(-1432940121, Moppable.OnReachableChangedDelegate);
		new ReachabilityMonitor.Instance(this).StartSM();
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06002304 RID: 8964 RVA: 0x000C92F4 File Offset: 0x000C74F4
	private void OnRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("icon_cancel", UI.USERMENUACTIONS.CANCELMOP.NAME, new global::System.Action(this.OnCancel), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCELMOP.TOOLTIP, true), 1f);
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x000C934E File Offset: 0x000C754E
	private void OnCancel()
	{
		DetailsScreen.Instance.Show(false);
		base.gameObject.Trigger(2127324410, null);
	}

	// Token: 0x06002306 RID: 8966 RVA: 0x000C936C File Offset: 0x000C756C
	protected override void OnStartWork(WorkerBase worker)
	{
		SimAndRenderScheduler.instance.Add(this, false);
		this.Refresh();
		this.MopTick(this.amountMoppedPerTick);
	}

	// Token: 0x06002307 RID: 8967 RVA: 0x000C938C File Offset: 0x000C758C
	protected override void OnStopWork(WorkerBase worker)
	{
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x000C9399 File Offset: 0x000C7599
	protected override void OnCompleteWork(WorkerBase worker)
	{
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x000C93A6 File Offset: 0x000C75A6
	public override bool InstantlyFinish(WorkerBase worker)
	{
		this.MopTick(1000f);
		return true;
	}

	// Token: 0x0600230A RID: 8970 RVA: 0x000C93B4 File Offset: 0x000C75B4
	public void Sim1000ms(float dt)
	{
		if (this.amountMopped > 0f)
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, GameUtil.GetFormattedMass(-this.amountMopped, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), base.transform, 1.5f, false);
			this.amountMopped = 0f;
		}
	}

	// Token: 0x0600230B RID: 8971 RVA: 0x000C940E File Offset: 0x000C760E
	public void Sim200ms(float dt)
	{
		if (base.worker != null)
		{
			this.Refresh();
			this.MopTick(this.amountMoppedPerTick);
		}
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x000C9430 File Offset: 0x000C7630
	private void OnCellMopped(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		if (this == null)
		{
			return;
		}
		if (mass_cb_info.mass > 0f)
		{
			this.amountMopped += mass_cb_info.mass;
			int num = Grid.PosToCell(this);
			SubstanceChunk substanceChunk = LiquidSourceManager.Instance.CreateChunk(ElementLoader.elements[(int)mass_cb_info.elemIdx], mass_cb_info.mass, mass_cb_info.temperature, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount, Grid.CellToPosCCC(num, Grid.SceneLayer.Ore));
			substanceChunk.transform.SetPosition(substanceChunk.transform.GetPosition() + new Vector3((global::UnityEngine.Random.value - 0.5f) * 0.5f, 0f, 0f));
		}
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x000C94E8 File Offset: 0x000C76E8
	public static void MopCell(int cell, float amount, Action<Sim.MassConsumedCallback, object> cb)
	{
		if (Grid.Element[cell].IsLiquid)
		{
			int num = -1;
			if (cb != null)
			{
				num = Game.Instance.massConsumedCallbackManager.Add(cb, null, "Moppable").index;
			}
			SimMessages.ConsumeMass(cell, Grid.Element[cell].id, amount, 1, num);
		}
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x000C953C File Offset: 0x000C773C
	private void MopTick(float mopAmount)
	{
		int num = Grid.PosToCell(this);
		for (int i = 0; i < this.offsets.Length; i++)
		{
			int num2 = Grid.OffsetCell(num, this.offsets[i]);
			if (Grid.Element[num2].IsLiquid)
			{
				Moppable.MopCell(num2, mopAmount, new Action<Sim.MassConsumedCallback, object>(this.OnCellMopped));
			}
		}
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x000C9598 File Offset: 0x000C7798
	private bool IsThereLiquid()
	{
		int num = Grid.PosToCell(this);
		bool flag = false;
		for (int i = 0; i < this.offsets.Length; i++)
		{
			int num2 = Grid.OffsetCell(num, this.offsets[i]);
			if (Grid.Element[num2].IsLiquid && Grid.Mass[num2] <= MopTool.maxMopAmt)
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x06002310 RID: 8976 RVA: 0x000C95F8 File Offset: 0x000C77F8
	private void Refresh()
	{
		if (!this.IsThereLiquid())
		{
			if (!this.destroyHandle.IsValid)
			{
				this.destroyHandle = GameScheduler.Instance.Schedule("DestroyMoppable", 1f, delegate(object moppable)
				{
					this.TryDestroy();
				}, this, null);
				return;
			}
		}
		else if (this.destroyHandle.IsValid)
		{
			this.destroyHandle.ClearScheduler();
		}
	}

	// Token: 0x06002311 RID: 8977 RVA: 0x000C965B File Offset: 0x000C785B
	private void OnLiquidChanged(object data)
	{
		this.Refresh();
	}

	// Token: 0x06002312 RID: 8978 RVA: 0x000C9663 File Offset: 0x000C7863
	private void TryDestroy()
	{
		if (this != null)
		{
			base.gameObject.DeleteObject();
		}
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x000C9679 File Offset: 0x000C7879
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x06002314 RID: 8980 RVA: 0x000C9694 File Offset: 0x000C7894
	private void OnReachableChanged(object data)
	{
		if (this.childRenderer != null)
		{
			Material material = this.childRenderer.material;
			bool flag = (bool)data;
			if (material.color == Game.Instance.uiColours.Dig.invalidLocation)
			{
				return;
			}
			KSelectable component = base.GetComponent<KSelectable>();
			if (flag)
			{
				material.color = Game.Instance.uiColours.Dig.validLocation;
				component.RemoveStatusItem(Db.Get().BuildingStatusItems.MopUnreachable, false);
				return;
			}
			component.AddStatusItem(Db.Get().BuildingStatusItems.MopUnreachable, this);
			GameScheduler.Instance.Schedule("Locomotion Tutorial", 2f, delegate(object obj)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Locomotion, true);
			}, null, null);
			material.color = Game.Instance.uiColours.Dig.unreachable;
		}
	}

	// Token: 0x04001453 RID: 5203
	[MyCmpReq]
	private KSelectable Selectable;

	// Token: 0x04001454 RID: 5204
	[MyCmpAdd]
	private Prioritizable prioritizable;

	// Token: 0x04001455 RID: 5205
	public float amountMoppedPerTick = 1000f;

	// Token: 0x04001456 RID: 5206
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001457 RID: 5207
	private SchedulerHandle destroyHandle;

	// Token: 0x04001458 RID: 5208
	private float amountMopped;

	// Token: 0x04001459 RID: 5209
	private MeshRenderer childRenderer;

	// Token: 0x0400145A RID: 5210
	private CellOffset[] offsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(1, 0),
		new CellOffset(-1, 0)
	};

	// Token: 0x0400145B RID: 5211
	private static readonly EventSystem.IntraObjectHandler<Moppable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Moppable>(delegate(Moppable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x0400145C RID: 5212
	private static readonly EventSystem.IntraObjectHandler<Moppable> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<Moppable>(delegate(Moppable component, object data)
	{
		component.OnReachableChanged(data);
	});
}
