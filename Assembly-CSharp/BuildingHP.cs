using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006CD RID: 1741
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/BuildingHP")]
public class BuildingHP : Workable
{
	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000F85B6 File Offset: 0x000F67B6
	public int HitPoints
	{
		get
		{
			return this.hitpoints;
		}
	}

	// Token: 0x06002AF1 RID: 10993 RVA: 0x000F85BE File Offset: 0x000F67BE
	public void SetHitPoints(int hp)
	{
		this.hitpoints = hp;
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000F85C7 File Offset: 0x000F67C7
	public int MaxHitPoints
	{
		get
		{
			return this.building.Def.HitPoints;
		}
	}

	// Token: 0x06002AF3 RID: 10995 RVA: 0x000F85D9 File Offset: 0x000F67D9
	public BuildingHP.DamageSourceInfo GetDamageSourceInfo()
	{
		return this.damageSourceInfo;
	}

	// Token: 0x06002AF4 RID: 10996 RVA: 0x000F85E1 File Offset: 0x000F67E1
	protected override void OnLoadLevel()
	{
		this.smi = null;
		base.OnLoadLevel();
	}

	// Token: 0x06002AF5 RID: 10997 RVA: 0x000F85F0 File Offset: 0x000F67F0
	public void DoDamage(int damage)
	{
		if (!this.invincible)
		{
			damage = Math.Max(0, damage);
			this.hitpoints = Math.Max(0, this.hitpoints - damage);
			base.Trigger(-1964935036, this);
		}
	}

	// Token: 0x06002AF6 RID: 10998 RVA: 0x000F8624 File Offset: 0x000F6824
	public void Repair(int repair_amount)
	{
		if (this.hitpoints + repair_amount < this.hitpoints)
		{
			this.hitpoints = this.building.Def.HitPoints;
		}
		else
		{
			this.hitpoints = Math.Min(this.hitpoints + repair_amount, this.building.Def.HitPoints);
		}
		base.Trigger(-1699355994, this);
		if (this.hitpoints >= this.building.Def.HitPoints)
		{
			base.Trigger(-1735440190, this);
		}
	}

	// Token: 0x06002AF7 RID: 10999 RVA: 0x000F86AC File Offset: 0x000F68AC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetWorkTime(10f);
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
	}

	// Token: 0x06002AF8 RID: 11000 RVA: 0x000F86E0 File Offset: 0x000F68E0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new BuildingHP.SMInstance(this);
		this.smi.StartSM();
		base.Subscribe<BuildingHP>(-794517298, BuildingHP.OnDoBuildingDamageDelegate);
		if (this.destroyOnDamaged)
		{
			base.Subscribe<BuildingHP>(774203113, BuildingHP.DestroyOnDamagedDelegate);
		}
		if (this.hitpoints <= 0)
		{
			base.Trigger(774203113, this);
		}
	}

	// Token: 0x06002AF9 RID: 11001 RVA: 0x000F8749 File Offset: 0x000F6949
	private void DestroyOnDamaged(object data)
	{
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06002AFA RID: 11002 RVA: 0x000F8758 File Offset: 0x000F6958
	protected override void OnCompleteWork(WorkerBase worker)
	{
		int num = (int)Db.Get().Attributes.Machinery.Lookup(worker).GetTotalValue();
		int num2 = 10 + Math.Max(0, num * 10);
		this.Repair(num2);
	}

	// Token: 0x06002AFB RID: 11003 RVA: 0x000F8796 File Offset: 0x000F6996
	private void OnDoBuildingDamage(object data)
	{
		if (this.invincible)
		{
			return;
		}
		this.damageSourceInfo = (BuildingHP.DamageSourceInfo)data;
		this.DoDamage(this.damageSourceInfo.damage);
		this.DoDamagePopFX(this.damageSourceInfo);
		this.DoTakeDamageFX(this.damageSourceInfo);
	}

	// Token: 0x06002AFC RID: 11004 RVA: 0x000F87D8 File Offset: 0x000F69D8
	private void DoTakeDamageFX(BuildingHP.DamageSourceInfo info)
	{
		if (info.takeDamageEffect != SpawnFXHashes.None)
		{
			BuildingDef def = base.GetComponent<BuildingComplete>().Def;
			int num = Grid.OffsetCell(Grid.PosToCell(this), 0, def.HeightInCells - 1);
			Game.Instance.SpawnFX(info.takeDamageEffect, num, 0f);
		}
	}

	// Token: 0x06002AFD RID: 11005 RVA: 0x000F8824 File Offset: 0x000F6A24
	private void DoDamagePopFX(BuildingHP.DamageSourceInfo info)
	{
		if (info.popString != null && Time.time > this.lastPopTime + this.minDamagePopInterval)
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Building, info.popString, base.gameObject.transform, 1.5f, false);
			this.lastPopTime = Time.time;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000F8884 File Offset: 0x000F6A84
	public bool IsBroken
	{
		get
		{
			return this.hitpoints == 0;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06002AFF RID: 11007 RVA: 0x000F888F File Offset: 0x000F6A8F
	public bool NeedsRepairs
	{
		get
		{
			return this.HitPoints < this.building.Def.HitPoints;
		}
	}

	// Token: 0x0400194F RID: 6479
	[Serialize]
	[SerializeField]
	private int hitpoints;

	// Token: 0x04001950 RID: 6480
	[Serialize]
	private BuildingHP.DamageSourceInfo damageSourceInfo;

	// Token: 0x04001951 RID: 6481
	private static readonly EventSystem.IntraObjectHandler<BuildingHP> OnDoBuildingDamageDelegate = new EventSystem.IntraObjectHandler<BuildingHP>(delegate(BuildingHP component, object data)
	{
		component.OnDoBuildingDamage(data);
	});

	// Token: 0x04001952 RID: 6482
	private static readonly EventSystem.IntraObjectHandler<BuildingHP> DestroyOnDamagedDelegate = new EventSystem.IntraObjectHandler<BuildingHP>(delegate(BuildingHP component, object data)
	{
		component.DestroyOnDamaged(data);
	});

	// Token: 0x04001953 RID: 6483
	public static List<Meter> kbacQueryList = new List<Meter>();

	// Token: 0x04001954 RID: 6484
	public bool destroyOnDamaged;

	// Token: 0x04001955 RID: 6485
	public bool invincible;

	// Token: 0x04001956 RID: 6486
	[MyCmpGet]
	private Building building;

	// Token: 0x04001957 RID: 6487
	private BuildingHP.SMInstance smi;

	// Token: 0x04001958 RID: 6488
	private float minDamagePopInterval = 4f;

	// Token: 0x04001959 RID: 6489
	private float lastPopTime;

	// Token: 0x0200154F RID: 5455
	public struct DamageSourceInfo
	{
		// Token: 0x060090AF RID: 37039 RVA: 0x00360E73 File Offset: 0x0035F073
		public override string ToString()
		{
			return this.source;
		}

		// Token: 0x04006F42 RID: 28482
		public int damage;

		// Token: 0x04006F43 RID: 28483
		public string source;

		// Token: 0x04006F44 RID: 28484
		public string popString;

		// Token: 0x04006F45 RID: 28485
		public SpawnFXHashes takeDamageEffect;

		// Token: 0x04006F46 RID: 28486
		public string fullDamageEffectName;

		// Token: 0x04006F47 RID: 28487
		public string statusItemID;
	}

	// Token: 0x02001550 RID: 5456
	public class SMInstance : GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.GameInstance
	{
		// Token: 0x060090B0 RID: 37040 RVA: 0x00360E7B File Offset: 0x0035F07B
		public SMInstance(BuildingHP master)
			: base(master)
		{
		}

		// Token: 0x060090B1 RID: 37041 RVA: 0x00360E84 File Offset: 0x0035F084
		public Notification CreateBrokenMachineNotification()
		{
			return new Notification(MISC.NOTIFICATIONS.BROKENMACHINE.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.BROKENMACHINE.TOOLTIP + notificationList.ReduceMessages(false), "/t• " + base.master.damageSourceInfo.source, false, 0f, null, null, null, true, false, false);
		}

		// Token: 0x060090B2 RID: 37042 RVA: 0x00360EE8 File Offset: 0x0035F0E8
		public void ShowProgressBar(bool show)
		{
			if (show && Grid.IsValidCell(Grid.PosToCell(base.gameObject)) && Grid.IsVisible(Grid.PosToCell(base.gameObject)))
			{
				this.CreateProgressBar();
				return;
			}
			if (this.progressBar != null)
			{
				this.progressBar.gameObject.DeleteObject();
				this.progressBar = null;
			}
		}

		// Token: 0x060090B3 RID: 37043 RVA: 0x00360F48 File Offset: 0x0035F148
		public void UpdateMeter()
		{
			if (this.progressBar == null)
			{
				this.ShowProgressBar(true);
			}
			if (this.progressBar)
			{
				this.progressBar.Update();
			}
		}

		// Token: 0x060090B4 RID: 37044 RVA: 0x00360F77 File Offset: 0x0035F177
		private float HealthPercent()
		{
			return (float)base.smi.master.HitPoints / (float)base.smi.master.building.Def.HitPoints;
		}

		// Token: 0x060090B5 RID: 37045 RVA: 0x00360FA8 File Offset: 0x0035F1A8
		private void CreateProgressBar()
		{
			if (this.progressBar != null)
			{
				return;
			}
			this.progressBar = Util.KInstantiateUI<ProgressBar>(ProgressBarsConfig.Instance.progressBarPrefab, null, false);
			this.progressBar.transform.SetParent(GameScreenManager.Instance.worldSpaceCanvas.transform);
			this.progressBar.name = base.smi.master.name + "." + base.smi.master.GetType().Name + " ProgressBar";
			this.progressBar.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("ProgressBar");
			this.progressBar.SetUpdateFunc(new Func<float>(this.HealthPercent));
			this.progressBar.barColor = ProgressBarsConfig.Instance.GetBarColor("HealthBar");
			CanvasGroup component = this.progressBar.GetComponent<CanvasGroup>();
			component.interactable = false;
			component.blocksRaycasts = false;
			this.progressBar.Update();
			float num = 0.15f;
			Vector3 vector = base.gameObject.transform.GetPosition() + Vector3.down * num;
			vector.z += 0.05f;
			Rotatable component2 = base.GetComponent<Rotatable>();
			if (component2 == null || component2.GetOrientation() == Orientation.Neutral || base.smi.master.building.Def.WidthInCells < 2 || base.smi.master.building.Def.HeightInCells < 2)
			{
				vector -= Vector3.right * 0.5f * (float)(base.smi.master.building.Def.WidthInCells % 2);
			}
			else
			{
				vector += Vector3.left * (1f + 0.5f * (float)(base.smi.master.building.Def.WidthInCells % 2));
			}
			this.progressBar.transform.SetPosition(vector);
			this.progressBar.SetVisibility(true);
		}

		// Token: 0x060090B6 RID: 37046 RVA: 0x003611D8 File Offset: 0x0035F3D8
		private static string ToolTipResolver(List<Notification> notificationList, object data)
		{
			string text = "";
			for (int i = 0; i < notificationList.Count; i++)
			{
				Notification notification = notificationList[i];
				text += string.Format(BUILDINGS.DAMAGESOURCES.NOTIFICATION_TOOLTIP, notification.NotifierName, (string)notification.tooltipData);
				if (i < notificationList.Count - 1)
				{
					text += "\n";
				}
			}
			return text;
		}

		// Token: 0x060090B7 RID: 37047 RVA: 0x00361244 File Offset: 0x0035F444
		public void ShowDamagedEffect()
		{
			if (base.master.damageSourceInfo.takeDamageEffect != SpawnFXHashes.None)
			{
				BuildingDef def = base.master.GetComponent<BuildingComplete>().Def;
				int num = Grid.OffsetCell(Grid.PosToCell(base.master), 0, def.HeightInCells - 1);
				Game.Instance.SpawnFX(base.master.damageSourceInfo.takeDamageEffect, num, 0f);
			}
		}

		// Token: 0x060090B8 RID: 37048 RVA: 0x003612B0 File Offset: 0x0035F4B0
		public FXAnim.Instance InstantiateDamageFX()
		{
			if (base.master.damageSourceInfo.fullDamageEffectName == null)
			{
				return null;
			}
			BuildingDef def = base.master.GetComponent<BuildingComplete>().Def;
			Vector3 zero = Vector3.zero;
			if (def.HeightInCells > 1)
			{
				zero = new Vector3(0f, (float)(def.HeightInCells - 1), 0f);
			}
			else
			{
				zero = new Vector3(0f, 0.5f, 0f);
			}
			return new FXAnim.Instance(base.smi.master, base.master.damageSourceInfo.fullDamageEffectName, "idle", KAnim.PlayMode.Loop, zero, Color.white);
		}

		// Token: 0x060090B9 RID: 37049 RVA: 0x00361354 File Offset: 0x0035F554
		public void SetCrackOverlayValue(float value)
		{
			KBatchedAnimController component = base.master.GetComponent<KBatchedAnimController>();
			if (component == null)
			{
				return;
			}
			component.SetBlendValue(value);
			BuildingHP.kbacQueryList.Clear();
			base.master.GetComponentsInChildren<Meter>(BuildingHP.kbacQueryList);
			for (int i = 0; i < BuildingHP.kbacQueryList.Count; i++)
			{
				BuildingHP.kbacQueryList[i].GetComponent<KBatchedAnimController>().SetBlendValue(value);
			}
		}

		// Token: 0x04006F48 RID: 28488
		private ProgressBar progressBar;
	}

	// Token: 0x02001551 RID: 5457
	public class States : GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP>
	{
		// Token: 0x060090BA RID: 37050 RVA: 0x003613C4 File Offset: 0x0035F5C4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.healthy;
			this.healthy.DefaultState(this.healthy.imperfect).EventTransition(GameHashes.BuildingReceivedDamage, this.damaged, (BuildingHP.SMInstance smi) => smi.master.HitPoints <= 0);
			this.healthy.imperfect.Enter(delegate(BuildingHP.SMInstance smi)
			{
				smi.ShowProgressBar(true);
			}).DefaultState(this.healthy.imperfect.playEffect).EventTransition(GameHashes.BuildingPartiallyRepaired, this.healthy.perfect, (BuildingHP.SMInstance smi) => smi.master.HitPoints == smi.master.building.Def.HitPoints)
				.EventHandler(GameHashes.BuildingPartiallyRepaired, delegate(BuildingHP.SMInstance smi)
				{
					smi.UpdateMeter();
				})
				.ToggleStatusItem(delegate(BuildingHP.SMInstance smi)
				{
					if (smi.master.damageSourceInfo.statusItemID == null)
					{
						return null;
					}
					return Db.Get().BuildingStatusItems.Get(smi.master.damageSourceInfo.statusItemID);
				}, null)
				.Exit(delegate(BuildingHP.SMInstance smi)
				{
					smi.ShowProgressBar(false);
				});
			this.healthy.imperfect.playEffect.Transition(this.healthy.imperfect.waiting, (BuildingHP.SMInstance smi) => true, UpdateRate.SIM_200ms);
			this.healthy.imperfect.waiting.ScheduleGoTo((BuildingHP.SMInstance smi) => global::UnityEngine.Random.Range(15f, 30f), this.healthy.imperfect.playEffect);
			this.healthy.perfect.EventTransition(GameHashes.BuildingReceivedDamage, this.healthy.imperfect, (BuildingHP.SMInstance smi) => smi.master.HitPoints < smi.master.building.Def.HitPoints);
			this.damaged.Enter(delegate(BuildingHP.SMInstance smi)
			{
				Operational component = smi.GetComponent<Operational>();
				if (component != null)
				{
					component.SetFlag(BuildingHP.States.healthyFlag, false);
				}
				smi.ShowProgressBar(true);
				smi.master.Trigger(774203113, smi.master);
				smi.SetCrackOverlayValue(1f);
			}).ToggleNotification((BuildingHP.SMInstance smi) => smi.CreateBrokenMachineNotification()).ToggleStatusItem(Db.Get().BuildingStatusItems.Broken, null)
				.ToggleFX((BuildingHP.SMInstance smi) => smi.InstantiateDamageFX())
				.EventTransition(GameHashes.BuildingPartiallyRepaired, this.healthy.perfect, (BuildingHP.SMInstance smi) => smi.master.HitPoints == smi.master.building.Def.HitPoints)
				.EventHandler(GameHashes.BuildingPartiallyRepaired, delegate(BuildingHP.SMInstance smi)
				{
					smi.UpdateMeter();
				})
				.Exit(delegate(BuildingHP.SMInstance smi)
				{
					Operational component2 = smi.GetComponent<Operational>();
					if (component2 != null)
					{
						component2.SetFlag(BuildingHP.States.healthyFlag, true);
					}
					smi.ShowProgressBar(false);
					smi.SetCrackOverlayValue(0f);
				});
		}

		// Token: 0x060090BB RID: 37051 RVA: 0x003616E8 File Offset: 0x0035F8E8
		private Chore CreateRepairChore(BuildingHP.SMInstance smi)
		{
			return new WorkChore<BuildingHP>(Db.Get().ChoreTypes.Repair, smi.master, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x04006F49 RID: 28489
		private static readonly Operational.Flag healthyFlag = new Operational.Flag("healthy", Operational.Flag.Type.Functional);

		// Token: 0x04006F4A RID: 28490
		public GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State damaged;

		// Token: 0x04006F4B RID: 28491
		public BuildingHP.States.Healthy healthy;

		// Token: 0x0200275D RID: 10077
		public class Healthy : GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State
		{
			// Token: 0x0400ADB5 RID: 44469
			public BuildingHP.States.ImperfectStates imperfect;

			// Token: 0x0400ADB6 RID: 44470
			public GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State perfect;
		}

		// Token: 0x0200275E RID: 10078
		public class ImperfectStates : GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State
		{
			// Token: 0x0400ADB7 RID: 44471
			public GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State playEffect;

			// Token: 0x0400ADB8 RID: 44472
			public GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State waiting;
		}
	}
}
