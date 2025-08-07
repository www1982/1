using System;
using UnityEngine;

// Token: 0x020007AD RID: 1965
[AddComponentMenu("KMonoBehaviour/scripts/Pump")]
public class Pump : KMonoBehaviour, ISim1000ms
{
	// Token: 0x06003421 RID: 13345 RVA: 0x0012434B File Offset: 0x0012254B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.consumer.EnableConsumption(false);
	}

	// Token: 0x06003422 RID: 13346 RVA: 0x0012435F File Offset: 0x0012255F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.elapsedTime = 0f;
		this.pumpable = this.UpdateOperational();
		this.dispenser.GetConduitManager().AddConduitUpdater(new Action<float>(this.OnConduitUpdate), ConduitFlowPriority.LastPostUpdate);
	}

	// Token: 0x06003423 RID: 13347 RVA: 0x0012439C File Offset: 0x0012259C
	protected override void OnCleanUp()
	{
		this.dispenser.GetConduitManager().RemoveConduitUpdater(new Action<float>(this.OnConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x06003424 RID: 13348 RVA: 0x001243C0 File Offset: 0x001225C0
	public void Sim1000ms(float dt)
	{
		this.elapsedTime += dt;
		if (this.elapsedTime >= 1f)
		{
			this.pumpable = this.UpdateOperational();
			this.elapsedTime = 0f;
		}
		if (this.operational.IsOperational && this.pumpable)
		{
			this.operational.SetActive(true, false);
			return;
		}
		this.operational.SetActive(false, false);
	}

	// Token: 0x06003425 RID: 13349 RVA: 0x00124430 File Offset: 0x00122630
	private bool UpdateOperational()
	{
		Element.State state = Element.State.Vacuum;
		ConduitType conduitType = this.dispenser.conduitType;
		if (conduitType != ConduitType.Gas)
		{
			if (conduitType == ConduitType.Liquid)
			{
				state = Element.State.Liquid;
			}
		}
		else
		{
			state = Element.State.Gas;
		}
		bool flag = this.IsPumpable(state, (int)this.consumer.consumptionRadius);
		StatusItem statusItem = ((state == Element.State.Gas) ? Db.Get().BuildingStatusItems.NoGasElementToPump : Db.Get().BuildingStatusItems.NoLiquidElementToPump);
		this.noElementStatusGuid = this.selectable.ToggleStatusItem(statusItem, this.noElementStatusGuid, !flag, null);
		this.operational.SetFlag(Pump.PumpableFlag, !this.storage.IsFull() && flag);
		return flag;
	}

	// Token: 0x06003426 RID: 13350 RVA: 0x001244D4 File Offset: 0x001226D4
	private bool IsPumpable(Element.State expected_state, int radius)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		for (int i = 0; i < (int)this.consumer.consumptionRadius; i++)
		{
			for (int j = 0; j < (int)this.consumer.consumptionRadius; j++)
			{
				int num2 = num + j + Grid.WidthInCells * i;
				if (Grid.Element[num2].IsState(expected_state))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06003427 RID: 13351 RVA: 0x0012453C File Offset: 0x0012273C
	private void OnConduitUpdate(float dt)
	{
		this.conduitBlockedStatusGuid = this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.ConduitBlocked, this.conduitBlockedStatusGuid, this.dispenser.blocked, null);
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x06003428 RID: 13352 RVA: 0x00124570 File Offset: 0x00122770
	public ConduitType conduitType
	{
		get
		{
			return this.dispenser.conduitType;
		}
	}

	// Token: 0x04001F5F RID: 8031
	public static readonly Operational.Flag PumpableFlag = new Operational.Flag("vent", Operational.Flag.Type.Requirement);

	// Token: 0x04001F60 RID: 8032
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001F61 RID: 8033
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04001F62 RID: 8034
	[MyCmpGet]
	private ElementConsumer consumer;

	// Token: 0x04001F63 RID: 8035
	[MyCmpGet]
	private ConduitDispenser dispenser;

	// Token: 0x04001F64 RID: 8036
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001F65 RID: 8037
	private const float OperationalUpdateInterval = 1f;

	// Token: 0x04001F66 RID: 8038
	private float elapsedTime;

	// Token: 0x04001F67 RID: 8039
	private bool pumpable;

	// Token: 0x04001F68 RID: 8040
	private Guid conduitBlockedStatusGuid;

	// Token: 0x04001F69 RID: 8041
	private Guid noElementStatusGuid;
}
