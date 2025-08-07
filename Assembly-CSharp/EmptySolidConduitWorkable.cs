using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008E3 RID: 2275
[AddComponentMenu("KMonoBehaviour/Workable/EmptySolidConduitWorkable")]
public class EmptySolidConduitWorkable : Workable, IEmptyConduitWorkable
{
	// Token: 0x06003F53 RID: 16211 RVA: 0x00164868 File Offset: 0x00162A68
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		base.SetWorkTime(float.PositiveInfinity);
		this.faceTargetWhenWorking = true;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		base.Subscribe<EmptySolidConduitWorkable>(2127324410, EmptySolidConduitWorkable.OnEmptyConduitCancelledDelegate);
		if (EmptySolidConduitWorkable.emptySolidConduitStatusItem == null)
		{
			EmptySolidConduitWorkable.emptySolidConduitStatusItem = new StatusItem("EmptySolidConduit", BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.NAME, BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.TOOLTIP, "status_item_empty_pipe", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.SolidConveyor.ID, 32770, true, null);
		}
		this.requiredSkillPerk = Db.Get().SkillPerks.CanDoPlumbing.Id;
		this.shouldShowSkillPerkStatusItem = false;
	}

	// Token: 0x06003F54 RID: 16212 RVA: 0x00164928 File Offset: 0x00162B28
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.elapsedTime != -1f)
		{
			this.MarkForEmptying();
		}
	}

	// Token: 0x06003F55 RID: 16213 RVA: 0x00164944 File Offset: 0x00162B44
	public void MarkForEmptying()
	{
		if (this.chore == null && this.HasContents())
		{
			StatusItem statusItem = this.GetStatusItem();
			base.GetComponent<KSelectable>().ToggleStatusItem(statusItem, true, null);
			this.CreateWorkChore();
		}
	}

	// Token: 0x06003F56 RID: 16214 RVA: 0x00164980 File Offset: 0x00162B80
	private bool HasContents()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return this.GetFlowManager().GetContents(num).pickupableHandle.IsValid();
	}

	// Token: 0x06003F57 RID: 16215 RVA: 0x001649B7 File Offset: 0x00162BB7
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

	// Token: 0x06003F58 RID: 16216 RVA: 0x001649EC File Offset: 0x00162BEC
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

	// Token: 0x06003F59 RID: 16217 RVA: 0x00164A38 File Offset: 0x00162C38
	protected override void OnCleanUp()
	{
		this.CancelEmptying();
		base.OnCleanUp();
	}

	// Token: 0x06003F5A RID: 16218 RVA: 0x00164A46 File Offset: 0x00162C46
	private SolidConduitFlow GetFlowManager()
	{
		return Game.Instance.solidConduitFlow;
	}

	// Token: 0x06003F5B RID: 16219 RVA: 0x00164A52 File Offset: 0x00162C52
	private void OnEmptyConduitCancelled(object data)
	{
		this.CancelEmptying();
	}

	// Token: 0x06003F5C RID: 16220 RVA: 0x00164A5A File Offset: 0x00162C5A
	private StatusItem GetStatusItem()
	{
		return EmptySolidConduitWorkable.emptySolidConduitStatusItem;
	}

	// Token: 0x06003F5D RID: 16221 RVA: 0x00164A64 File Offset: 0x00162C64
	private void CreateWorkChore()
	{
		base.GetComponent<Prioritizable>().AddRef();
		this.chore = new WorkChore<EmptySolidConduitWorkable>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanDoPlumbing.Id);
		this.elapsedTime = 0f;
		this.emptiedPipe = false;
		this.shouldShowSkillPerkStatusItem = true;
		this.UpdateStatusItem(null);
	}

	// Token: 0x06003F5E RID: 16222 RVA: 0x00164AF4 File Offset: 0x00162CF4
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
			if (this.GetFlowManager().GetContents(num).pickupableHandle.IsValid())
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

	// Token: 0x06003F5F RID: 16223 RVA: 0x00164BBD File Offset: 0x00162DBD
	public override bool InstantlyFinish(WorkerBase worker)
	{
		worker.Work(4f);
		return true;
	}

	// Token: 0x06003F60 RID: 16224 RVA: 0x00164BCC File Offset: 0x00162DCC
	public void EmptyContents()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		this.GetFlowManager().RemovePickupable(num);
		this.elapsedTime = 0f;
	}

	// Token: 0x06003F61 RID: 16225 RVA: 0x00164C02 File Offset: 0x00162E02
	public override float GetPercentComplete()
	{
		return Mathf.Clamp01(this.elapsedTime / 4f);
	}

	// Token: 0x0400275F RID: 10079
	[MyCmpReq]
	private SolidConduit conduit;

	// Token: 0x04002760 RID: 10080
	private static StatusItem emptySolidConduitStatusItem;

	// Token: 0x04002761 RID: 10081
	private Chore chore;

	// Token: 0x04002762 RID: 10082
	private const float RECHECK_PIPE_INTERVAL = 2f;

	// Token: 0x04002763 RID: 10083
	private const float TIME_TO_EMPTY_PIPE = 4f;

	// Token: 0x04002764 RID: 10084
	private const float NO_EMPTY_SCHEDULED = -1f;

	// Token: 0x04002765 RID: 10085
	[Serialize]
	private float elapsedTime = -1f;

	// Token: 0x04002766 RID: 10086
	private bool emptiedPipe = true;

	// Token: 0x04002767 RID: 10087
	private static readonly EventSystem.IntraObjectHandler<EmptySolidConduitWorkable> OnEmptyConduitCancelledDelegate = new EventSystem.IntraObjectHandler<EmptySolidConduitWorkable>(delegate(EmptySolidConduitWorkable component, object data)
	{
		component.OnEmptyConduitCancelled(data);
	});
}
