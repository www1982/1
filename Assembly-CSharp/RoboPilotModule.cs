using System;

// Token: 0x02000B5E RID: 2910
public class RoboPilotModule : KMonoBehaviour
{
	// Token: 0x060056B5 RID: 22197 RVA: 0x001F684C File Offset: 0x001F4A4C
	protected override void OnSpawn()
	{
		this.databankStorage = base.GetComponent<Storage>();
		this.manualDeliveryChore = base.GetComponent<ManualDeliveryKG>();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame" });
		this.meter.gameObject.GetComponent<KBatchedAnimTracker>().matchParentOffset = true;
		this.UpdateMeter(null);
		this.databankStorage.SetOffsets(RoboPilotModule.dataDeliveryOffsets);
		base.Subscribe(-1697596308, new Action<object>(this.UpdateMeter));
		base.Subscribe(-778359855, new Action<object>(this.PlayDeliveryAnimation));
		base.Subscribe(-887025858, new Action<object>(this.OnRocketLanded));
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null)
		{
			component.CraftInterface.Subscribe(1655598572, new Action<object>(this.OnLaunchConditionChanged));
			component.CraftInterface.Subscribe(543433792, new Action<object>(this.RequestDataBanksForDestination));
		}
		else
		{
			base.Subscribe(705820818, new Action<object>(this.OnRocketLaunched));
			base.GetComponent<RocketModule>().FindLaunchConditionManager().Subscribe(929158128, new Action<object>(this.RequestDataBanksForDestination));
		}
		this.RequestDataBanksForDestination(null);
	}

	// Token: 0x060056B6 RID: 22198 RVA: 0x001F69B0 File Offset: 0x001F4BB0
	private void RequestDataBanksForDestination(object data = null)
	{
		int num = -1;
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null)
		{
			ClusterTraveler component2 = component.CraftInterface.GetComponent<ClusterTraveler>();
			if (component2 != null && component2.CurrentPath != null)
			{
				num = component2.RemainingTravelNodes() * 2;
			}
		}
		else
		{
			LaunchConditionManager launchConditionManager = base.GetComponent<RocketModule>().FindLaunchConditionManager();
			if (launchConditionManager != null)
			{
				SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(launchConditionManager);
				if (spacecraftDestination != null)
				{
					num = spacecraftDestination.OneBasedDistance * 2;
				}
			}
		}
		if (num > 0 && !this.HasResourcesToMove(num))
		{
			this.manualDeliveryChore.refillMass = MathF.Min(this.ResourcesRequiredToMove(num), this.databankStorage.Capacity() - this.databankStorage.UnitsStored());
		}
	}

	// Token: 0x060056B7 RID: 22199 RVA: 0x001F6A64 File Offset: 0x001F4C64
	protected override void OnCleanUp()
	{
		base.Unsubscribe(-1697596308, new Action<object>(this.UpdateMeter));
		base.Unsubscribe(-887025858, new Action<object>(this.OnRocketLanded));
		base.Unsubscribe(-778359855, new Action<object>(this.PlayDeliveryAnimation));
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null)
		{
			component.CraftInterface.Unsubscribe(1655598572, new Action<object>(this.OnLaunchConditionChanged));
			component.CraftInterface.Unsubscribe(543433792, new Action<object>(this.RequestDataBanksForDestination));
		}
		else
		{
			base.Unsubscribe(705820818, new Action<object>(this.OnRocketLaunched));
			base.GetComponent<RocketModule>().FindLaunchConditionManager().Unsubscribe(929158128, new Action<object>(this.RequestDataBanksForDestination));
		}
		base.OnCleanUp();
	}

	// Token: 0x060056B8 RID: 22200 RVA: 0x001F6B40 File Offset: 0x001F4D40
	private void OnLaunchConditionChanged(object data)
	{
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null && component.CraftInterface.IsLaunchRequested())
		{
			component.CraftInterface.GetComponent<Clustercraft>().Launch(false);
		}
	}

	// Token: 0x060056B9 RID: 22201 RVA: 0x001F6B7C File Offset: 0x001F4D7C
	private void OnRocketLanded(object o)
	{
		if (this.consumeDataBanksOnLand)
		{
			LaunchConditionManager launchConditionManager = base.GetComponent<RocketModule>().FindLaunchConditionManager();
			Spacecraft spacecraftFromLaunchConditionManager = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(launchConditionManager);
			float num = Math.Min((float)(SpacecraftManager.instance.GetSpacecraftDestination(spacecraftFromLaunchConditionManager.id).OneBasedDistance * this.dataBankConsumption * 2), this.databankStorage.MassStored());
			this.databankStorage.ConsumeIgnoringDisease(DatabankHelper.TAG, num);
		}
		this.RequestDataBanksForDestination(null);
	}

	// Token: 0x060056BA RID: 22202 RVA: 0x001F6BF4 File Offset: 0x001F4DF4
	private void OnRocketLaunched(object o)
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		component.Play("launch_pre", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue("launch", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue("launch_pst", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x060056BB RID: 22203 RVA: 0x001F6C57 File Offset: 0x001F4E57
	public void ConsumeDataBanksInFlight()
	{
		if (this.databankStorage != null)
		{
			this.databankStorage.ConsumeIgnoringDisease(DatabankHelper.TAG, (float)this.dataBankConsumption);
		}
	}

	// Token: 0x060056BC RID: 22204 RVA: 0x001F6C80 File Offset: 0x001F4E80
	private void PlayDeliveryAnimation(object data = null)
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		HashedString currentAnim = component.currentAnim;
		component.Play("databank_delivery_reaction", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue(currentAnim, KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x060056BD RID: 22205 RVA: 0x001F6CC6 File Offset: 0x001F4EC6
	private void UpdateMeter(object data = null)
	{
		this.meter.SetPositionPercent(this.databankStorage.MassStored() / this.databankStorage.Capacity());
	}

	// Token: 0x060056BE RID: 22206 RVA: 0x001F6CEA File Offset: 0x001F4EEA
	public bool HasResourcesToMove(int distance)
	{
		return this.databankStorage.UnitsStored() >= (float)(distance * this.dataBankConsumption);
	}

	// Token: 0x060056BF RID: 22207 RVA: 0x001F6D05 File Offset: 0x001F4F05
	public float ResourcesRequiredToMove(int distance)
	{
		return (float)(distance * this.dataBankConsumption);
	}

	// Token: 0x060056C0 RID: 22208 RVA: 0x001F6D10 File Offset: 0x001F4F10
	public bool IsFull()
	{
		return this.databankStorage.MassStored() >= this.databankStorage.Capacity();
	}

	// Token: 0x060056C1 RID: 22209 RVA: 0x001F6D2D File Offset: 0x001F4F2D
	public float GetDataBanksStored()
	{
		if (!(this.databankStorage != null))
		{
			return 0f;
		}
		return this.databankStorage.UnitsStored();
	}

	// Token: 0x060056C2 RID: 22210 RVA: 0x001F6D50 File Offset: 0x001F4F50
	public float GetDataBankRange()
	{
		if (this.databankStorage == null)
		{
			return 0f;
		}
		if (this.consumeDataBanksOnLand)
		{
			return this.databankStorage.UnitsStored() / (float)this.dataBankConsumption * RoboPilotCommandModuleConfig.DATABANKRANGE;
		}
		return this.databankStorage.UnitsStored() / (float)this.dataBankConsumption * 600f;
	}

	// Token: 0x040039F2 RID: 14834
	private MeterController meter;

	// Token: 0x040039F3 RID: 14835
	private Storage databankStorage;

	// Token: 0x040039F4 RID: 14836
	private ManualDeliveryKG manualDeliveryChore;

	// Token: 0x040039F5 RID: 14837
	public int dataBankConsumption = 2;

	// Token: 0x040039F6 RID: 14838
	public bool consumeDataBanksOnLand;

	// Token: 0x040039F7 RID: 14839
	private static CellOffset[] dataDeliveryOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(1, 0),
		new CellOffset(2, 0),
		new CellOffset(3, 0),
		new CellOffset(-1, 0),
		new CellOffset(-2, 0),
		new CellOffset(-3, 0)
	};
}
