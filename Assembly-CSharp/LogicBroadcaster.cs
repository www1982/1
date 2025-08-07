using System;
using KSerialization;

// Token: 0x0200075C RID: 1884
public class LogicBroadcaster : KMonoBehaviour, ISimEveryTick
{
	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06003017 RID: 12311 RVA: 0x00113A13 File Offset: 0x00111C13
	// (set) Token: 0x06003018 RID: 12312 RVA: 0x00113A1B File Offset: 0x00111C1B
	public int BroadCastChannelID
	{
		get
		{
			return this.broadcastChannelID;
		}
		private set
		{
			this.broadcastChannelID = value;
		}
	}

	// Token: 0x06003019 RID: 12313 RVA: 0x00113A24 File Offset: 0x00111C24
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.LogicBroadcasters.Add(this);
	}

	// Token: 0x0600301A RID: 12314 RVA: 0x00113A37 File Offset: 0x00111C37
	protected override void OnCleanUp()
	{
		Components.LogicBroadcasters.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x0600301B RID: 12315 RVA: 0x00113A4C File Offset: 0x00111C4C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<LogicBroadcaster>(-801688580, LogicBroadcaster.OnLogicValueChangedDelegate);
		base.Subscribe(-592767678, new Action<object>(this.OnOperationalChanged));
		this.operational.SetFlag(LogicBroadcaster.spaceVisible, this.IsSpaceVisible());
		this.wasOperational = !this.operational.IsOperational;
		this.OnOperationalChanged(null);
	}

	// Token: 0x0600301C RID: 12316 RVA: 0x00113AB9 File Offset: 0x00111CB9
	public bool IsSpaceVisible()
	{
		return base.gameObject.GetMyWorld().IsModuleInterior || Grid.ExposedToSunlight[Grid.PosToCell(base.gameObject)] > 0;
	}

	// Token: 0x0600301D RID: 12317 RVA: 0x00113AE7 File Offset: 0x00111CE7
	public int GetCurrentValue()
	{
		return base.GetComponent<LogicPorts>().GetInputValue(this.PORT_ID);
	}

	// Token: 0x0600301E RID: 12318 RVA: 0x00113AFF File Offset: 0x00111CFF
	private void OnLogicValueChanged(object data)
	{
	}

	// Token: 0x0600301F RID: 12319 RVA: 0x00113B04 File Offset: 0x00111D04
	public void SimEveryTick(float dt)
	{
		bool flag = this.IsSpaceVisible();
		this.operational.SetFlag(LogicBroadcaster.spaceVisible, flag);
		if (!flag)
		{
			if (this.spaceNotVisibleStatusItem == Guid.Empty)
			{
				this.spaceNotVisibleStatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoSurfaceSight, null);
				return;
			}
		}
		else if (this.spaceNotVisibleStatusItem != Guid.Empty)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(this.spaceNotVisibleStatusItem, false);
			this.spaceNotVisibleStatusItem = Guid.Empty;
		}
	}

	// Token: 0x06003020 RID: 12320 RVA: 0x00113B90 File Offset: 0x00111D90
	private void OnOperationalChanged(object data)
	{
		if (this.operational.IsOperational)
		{
			if (!this.wasOperational)
			{
				this.wasOperational = true;
				this.animController.Queue("on_pre", KAnim.PlayMode.Once, 1f, 0f);
				this.animController.Queue("on", KAnim.PlayMode.Loop, 1f, 0f);
				return;
			}
		}
		else if (this.wasOperational)
		{
			this.wasOperational = false;
			this.animController.Queue("on_pst", KAnim.PlayMode.Once, 1f, 0f);
			this.animController.Queue("off", KAnim.PlayMode.Loop, 1f, 0f);
		}
	}

	// Token: 0x04001CB8 RID: 7352
	public static int RANGE = 5;

	// Token: 0x04001CB9 RID: 7353
	private static int INVALID_CHANNEL_ID = -1;

	// Token: 0x04001CBA RID: 7354
	public string PORT_ID = "";

	// Token: 0x04001CBB RID: 7355
	private bool wasOperational;

	// Token: 0x04001CBC RID: 7356
	[Serialize]
	private int broadcastChannelID = LogicBroadcaster.INVALID_CHANNEL_ID;

	// Token: 0x04001CBD RID: 7357
	private static readonly EventSystem.IntraObjectHandler<LogicBroadcaster> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicBroadcaster>(delegate(LogicBroadcaster component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001CBE RID: 7358
	public static readonly Operational.Flag spaceVisible = new Operational.Flag("spaceVisible", Operational.Flag.Type.Requirement);

	// Token: 0x04001CBF RID: 7359
	private Guid spaceNotVisibleStatusItem = Guid.Empty;

	// Token: 0x04001CC0 RID: 7360
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001CC1 RID: 7361
	[MyCmpGet]
	private KBatchedAnimController animController;
}
