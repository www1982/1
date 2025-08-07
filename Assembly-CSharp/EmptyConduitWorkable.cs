using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008E2 RID: 2274
[AddComponentMenu("KMonoBehaviour/Workable/EmptyConduitWorkable")]
public class EmptyConduitWorkable : Workable, IEmptyConduitWorkable
{
	// Token: 0x06003F42 RID: 16194 RVA: 0x00164378 File Offset: 0x00162578
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		base.SetWorkTime(float.PositiveInfinity);
		this.faceTargetWhenWorking = true;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		base.Subscribe<EmptyConduitWorkable>(2127324410, EmptyConduitWorkable.OnEmptyConduitCancelledDelegate);
		if (EmptyConduitWorkable.emptyLiquidConduitStatusItem == null)
		{
			EmptyConduitWorkable.emptyLiquidConduitStatusItem = new StatusItem("EmptyLiquidConduit", BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.NAME, BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.TOOLTIP, "status_item_empty_pipe", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.LiquidConduits.ID, 66, true, null);
			EmptyConduitWorkable.emptyGasConduitStatusItem = new StatusItem("EmptyGasConduit", BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.NAME, BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.TOOLTIP, "status_item_empty_pipe", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.GasConduits.ID, 130, true, null);
		}
		this.requiredSkillPerk = Db.Get().SkillPerks.CanDoPlumbing.Id;
		this.shouldShowSkillPerkStatusItem = false;
	}

	// Token: 0x06003F43 RID: 16195 RVA: 0x0016446C File Offset: 0x0016266C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.elapsedTime != -1f)
		{
			this.MarkForEmptying();
		}
	}

	// Token: 0x06003F44 RID: 16196 RVA: 0x00164488 File Offset: 0x00162688
	public void MarkForEmptying()
	{
		if (this.chore == null && this.HasContents())
		{
			StatusItem statusItem = this.GetStatusItem();
			base.GetComponent<KSelectable>().ToggleStatusItem(statusItem, true, null);
			this.CreateWorkChore();
		}
	}

	// Token: 0x06003F45 RID: 16197 RVA: 0x001644C4 File Offset: 0x001626C4
	private bool HasContents()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return this.GetFlowManager().GetContents(num).mass > 0f;
	}

	// Token: 0x06003F46 RID: 16198 RVA: 0x001644FD File Offset: 0x001626FD
	private void CancelEmptying()
	{
		this.CleanUpVisualization();
		if (this.chore != null)
		{
			this.chore.Cancel("Cancel");
			this.chore = null;
			this.shouldShowSkillPerkStatusItem = false;
			this.UpdateStatusItem(null);
		}
	}

	// Token: 0x06003F47 RID: 16199 RVA: 0x00164534 File Offset: 0x00162734
	private void CleanUpVisualization()
	{
		StatusItem statusItem = this.GetStatusItem();
		KSelectable component = base.GetComponent<KSelectable>();
		if (component != null)
		{
			component.ToggleStatusItem(statusItem, false, null);
		}
		this.elapsedTime = -1f;
		if (this.chore != null)
		{
			base.GetComponent<Prioritizable>().RemoveRef();
		}
	}

	// Token: 0x06003F48 RID: 16200 RVA: 0x00164580 File Offset: 0x00162780
	protected override void OnCleanUp()
	{
		this.CancelEmptying();
		base.OnCleanUp();
	}

	// Token: 0x06003F49 RID: 16201 RVA: 0x0016458E File Offset: 0x0016278E
	private ConduitFlow GetFlowManager()
	{
		if (this.conduit.type != ConduitType.Gas)
		{
			return Game.Instance.liquidConduitFlow;
		}
		return Game.Instance.gasConduitFlow;
	}

	// Token: 0x06003F4A RID: 16202 RVA: 0x001645B3 File Offset: 0x001627B3
	private void OnEmptyConduitCancelled(object data)
	{
		this.CancelEmptying();
	}

	// Token: 0x06003F4B RID: 16203 RVA: 0x001645BC File Offset: 0x001627BC
	private StatusItem GetStatusItem()
	{
		ConduitType type = this.conduit.type;
		StatusItem statusItem;
		if (type != ConduitType.Gas)
		{
			if (type != ConduitType.Liquid)
			{
				throw new ArgumentException();
			}
			statusItem = EmptyConduitWorkable.emptyLiquidConduitStatusItem;
		}
		else
		{
			statusItem = EmptyConduitWorkable.emptyGasConduitStatusItem;
		}
		return statusItem;
	}

	// Token: 0x06003F4C RID: 16204 RVA: 0x001645F8 File Offset: 0x001627F8
	private void CreateWorkChore()
	{
		base.GetComponent<Prioritizable>().AddRef();
		this.chore = new WorkChore<EmptyConduitWorkable>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanDoPlumbing.Id);
		this.elapsedTime = 0f;
		this.emptiedPipe = false;
		this.shouldShowSkillPerkStatusItem = true;
		this.UpdateStatusItem(null);
	}

	// Token: 0x06003F4D RID: 16205 RVA: 0x00164688 File Offset: 0x00162888
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (this.elapsedTime == -1f)
		{
			return true;
		}
		bool flag = false;
		this.elapsedTime += dt;
		if (!this.emptiedPipe)
		{
			if (this.elapsedTime > 4f)
			{
				this.EmptyContents();
				this.emptiedPipe = true;
				this.elapsedTime = 0f;
			}
		}
		else if (this.elapsedTime > 2f)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			if (this.GetFlowManager().GetContents(num).mass > 0f)
			{
				this.elapsedTime = 0f;
				this.emptiedPipe = false;
			}
			else
			{
				this.CleanUpVisualization();
				this.chore = null;
				flag = true;
				this.shouldShowSkillPerkStatusItem = false;
				this.UpdateStatusItem(null);
			}
		}
		return flag;
	}

	// Token: 0x06003F4E RID: 16206 RVA: 0x00164751 File Offset: 0x00162951
	public override bool InstantlyFinish(WorkerBase worker)
	{
		worker.Work(4f);
		return true;
	}

	// Token: 0x06003F4F RID: 16207 RVA: 0x00164760 File Offset: 0x00162960
	public void EmptyContents()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		ConduitFlow.ConduitContents conduitContents = this.GetFlowManager().RemoveElement(num, float.PositiveInfinity);
		this.elapsedTime = 0f;
		if (conduitContents.mass > 0f && conduitContents.element != SimHashes.Vacuum)
		{
			ConduitType type = this.conduit.type;
			IChunkManager chunkManager;
			if (type != ConduitType.Gas)
			{
				if (type != ConduitType.Liquid)
				{
					throw new ArgumentException();
				}
				chunkManager = LiquidSourceManager.Instance;
			}
			else
			{
				chunkManager = GasSourceManager.Instance;
			}
			chunkManager.CreateChunk(conduitContents.element, conduitContents.mass, conduitContents.temperature, conduitContents.diseaseIdx, conduitContents.diseaseCount, Grid.CellToPosCCC(num, Grid.SceneLayer.Ore)).Trigger(580035959, base.worker);
		}
	}

	// Token: 0x06003F50 RID: 16208 RVA: 0x0016481F File Offset: 0x00162A1F
	public override float GetPercentComplete()
	{
		return Mathf.Clamp01(this.elapsedTime / 4f);
	}

	// Token: 0x04002755 RID: 10069
	[MyCmpReq]
	private Conduit conduit;

	// Token: 0x04002756 RID: 10070
	private static StatusItem emptyLiquidConduitStatusItem;

	// Token: 0x04002757 RID: 10071
	private static StatusItem emptyGasConduitStatusItem;

	// Token: 0x04002758 RID: 10072
	private Chore chore;

	// Token: 0x04002759 RID: 10073
	private const float RECHECK_PIPE_INTERVAL = 2f;

	// Token: 0x0400275A RID: 10074
	private const float TIME_TO_EMPTY_PIPE = 4f;

	// Token: 0x0400275B RID: 10075
	private const float NO_EMPTY_SCHEDULED = -1f;

	// Token: 0x0400275C RID: 10076
	[Serialize]
	private float elapsedTime = -1f;

	// Token: 0x0400275D RID: 10077
	private bool emptiedPipe = true;

	// Token: 0x0400275E RID: 10078
	private static readonly EventSystem.IntraObjectHandler<EmptyConduitWorkable> OnEmptyConduitCancelledDelegate = new EventSystem.IntraObjectHandler<EmptyConduitWorkable>(delegate(EmptyConduitWorkable component, object data)
	{
		component.OnEmptyConduitCancelled(data);
	});
}
