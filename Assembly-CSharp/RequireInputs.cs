using System;
using UnityEngine;

// Token: 0x02000ABD RID: 2749
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/RequireInputs")]
public class RequireInputs : KMonoBehaviour, ISim200ms
{
	// Token: 0x1700058A RID: 1418
	// (get) Token: 0x06004FC5 RID: 20421 RVA: 0x001CDCA3 File Offset: 0x001CBEA3
	public bool RequiresPower
	{
		get
		{
			return this.requirePower;
		}
	}

	// Token: 0x1700058B RID: 1419
	// (get) Token: 0x06004FC6 RID: 20422 RVA: 0x001CDCAB File Offset: 0x001CBEAB
	public bool RequiresInputConduit
	{
		get
		{
			return this.requireConduit;
		}
	}

	// Token: 0x06004FC7 RID: 20423 RVA: 0x001CDCB3 File Offset: 0x001CBEB3
	public void SetRequirements(bool power, bool conduit)
	{
		this.requirePower = power;
		this.requireConduit = conduit;
	}

	// Token: 0x1700058C RID: 1420
	// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x001CDCC3 File Offset: 0x001CBEC3
	public bool RequirementsMet
	{
		get
		{
			return this.requirementsMet;
		}
	}

	// Token: 0x06004FC9 RID: 20425 RVA: 0x001CDCCB File Offset: 0x001CBECB
	protected override void OnPrefabInit()
	{
		this.Bind();
	}

	// Token: 0x06004FCA RID: 20426 RVA: 0x001CDCD3 File Offset: 0x001CBED3
	protected override void OnSpawn()
	{
		this.CheckRequirements(true);
		this.Bind();
	}

	// Token: 0x06004FCB RID: 20427 RVA: 0x001CDCE4 File Offset: 0x001CBEE4
	[ContextMenu("Bind")]
	private void Bind()
	{
		if (this.requirePower)
		{
			this.energy = base.GetComponent<IEnergyConsumer>();
			this.button = base.GetComponent<BuildingEnabledButton>();
		}
		if (this.requireConduit && !this.conduitConsumer)
		{
			this.conduitConsumer = base.GetComponent<ConduitConsumer>();
		}
	}

	// Token: 0x06004FCC RID: 20428 RVA: 0x001CDD32 File Offset: 0x001CBF32
	public void Sim200ms(float dt)
	{
		this.CheckRequirements(false);
	}

	// Token: 0x06004FCD RID: 20429 RVA: 0x001CDD3C File Offset: 0x001CBF3C
	private void CheckRequirements(bool forceEvent)
	{
		bool flag = true;
		bool flag2 = false;
		if (this.requirePower)
		{
			bool isConnected = this.energy.IsConnected;
			bool isPowered = this.energy.IsPowered;
			flag = flag && isPowered && isConnected;
			bool flag3 = this.VisualizeRequirement(RequireInputs.Requirements.NeedPower) && isConnected && !isPowered && (this.button == null || this.button.IsEnabled);
			bool flag4 = this.VisualizeRequirement(RequireInputs.Requirements.NoWire) && !isConnected;
			this.needPowerStatusGuid = this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NeedPower, this.needPowerStatusGuid, flag3, this);
			this.noWireStatusGuid = this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NoWireConnected, this.noWireStatusGuid, flag4, this);
			flag2 = flag != this.RequirementsMet && base.GetComponent<Light2D>() != null;
		}
		if (this.requireConduit)
		{
			bool flag5 = !this.conduitConsumer.enabled || this.conduitConsumer.IsConnected;
			bool flag6 = !this.conduitConsumer.enabled || this.conduitConsumer.IsSatisfied;
			if (this.VisualizeRequirement(RequireInputs.Requirements.ConduitConnected) && this.previouslyConnected != flag5)
			{
				this.previouslyConnected = flag5;
				StatusItem statusItem = null;
				ConduitType conduitType = this.conduitConsumer.TypeOfConduit;
				if (conduitType != ConduitType.Gas)
				{
					if (conduitType == ConduitType.Liquid)
					{
						statusItem = Db.Get().BuildingStatusItems.NeedLiquidIn;
					}
				}
				else
				{
					statusItem = Db.Get().BuildingStatusItems.NeedGasIn;
				}
				if (statusItem != null)
				{
					this.selectable.ToggleStatusItem(statusItem, !flag5, new global::Tuple<ConduitType, Tag>(this.conduitConsumer.TypeOfConduit, this.conduitConsumer.capacityTag));
				}
				this.operational.SetFlag(RequireInputs.inputConnectedFlag, flag5);
			}
			flag = flag && flag5;
			if (this.VisualizeRequirement(RequireInputs.Requirements.ConduitEmpty) && this.previouslySatisfied != flag6)
			{
				this.previouslySatisfied = flag6;
				StatusItem statusItem2 = null;
				ConduitType conduitType = this.conduitConsumer.TypeOfConduit;
				if (conduitType != ConduitType.Gas)
				{
					if (conduitType == ConduitType.Liquid)
					{
						statusItem2 = Db.Get().BuildingStatusItems.LiquidPipeEmpty;
					}
				}
				else
				{
					statusItem2 = Db.Get().BuildingStatusItems.GasPipeEmpty;
				}
				if (this.requireConduitHasMass)
				{
					if (statusItem2 != null)
					{
						this.selectable.ToggleStatusItem(statusItem2, !flag6, this);
					}
					this.operational.SetFlag(RequireInputs.pipesHaveMass, flag6);
				}
			}
		}
		this.requirementsMet = flag;
		if (flag2)
		{
			Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
			if (roomOfGameObject != null)
			{
				Game.Instance.roomProber.UpdateRoom(roomOfGameObject.cavity);
			}
		}
	}

	// Token: 0x06004FCE RID: 20430 RVA: 0x001CDFD8 File Offset: 0x001CC1D8
	public bool VisualizeRequirement(RequireInputs.Requirements r)
	{
		return (this.visualizeRequirements & r) == r;
	}

	// Token: 0x040035B7 RID: 13751
	[SerializeField]
	private bool requirePower = true;

	// Token: 0x040035B8 RID: 13752
	[SerializeField]
	private bool requireConduit;

	// Token: 0x040035B9 RID: 13753
	public bool requireConduitHasMass = true;

	// Token: 0x040035BA RID: 13754
	public RequireInputs.Requirements visualizeRequirements = RequireInputs.Requirements.All;

	// Token: 0x040035BB RID: 13755
	private static readonly Operational.Flag inputConnectedFlag = new Operational.Flag("inputConnected", Operational.Flag.Type.Requirement);

	// Token: 0x040035BC RID: 13756
	private static readonly Operational.Flag pipesHaveMass = new Operational.Flag("pipesHaveMass", Operational.Flag.Type.Requirement);

	// Token: 0x040035BD RID: 13757
	private Guid noWireStatusGuid;

	// Token: 0x040035BE RID: 13758
	private Guid needPowerStatusGuid;

	// Token: 0x040035BF RID: 13759
	private bool requirementsMet;

	// Token: 0x040035C0 RID: 13760
	private BuildingEnabledButton button;

	// Token: 0x040035C1 RID: 13761
	private IEnergyConsumer energy;

	// Token: 0x040035C2 RID: 13762
	public ConduitConsumer conduitConsumer;

	// Token: 0x040035C3 RID: 13763
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040035C4 RID: 13764
	[MyCmpGet]
	private Operational operational;

	// Token: 0x040035C5 RID: 13765
	private bool previouslyConnected = true;

	// Token: 0x040035C6 RID: 13766
	private bool previouslySatisfied = true;

	// Token: 0x02001B94 RID: 7060
	[Flags]
	public enum Requirements
	{
		// Token: 0x04008346 RID: 33606
		None = 0,
		// Token: 0x04008347 RID: 33607
		NoWire = 1,
		// Token: 0x04008348 RID: 33608
		NeedPower = 2,
		// Token: 0x04008349 RID: 33609
		ConduitConnected = 4,
		// Token: 0x0400834A RID: 33610
		ConduitEmpty = 8,
		// Token: 0x0400834B RID: 33611
		AllPower = 3,
		// Token: 0x0400834C RID: 33612
		AllConduit = 12,
		// Token: 0x0400834D RID: 33613
		All = 15
	}
}
