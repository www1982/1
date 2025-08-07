using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200088D RID: 2189
[AddComponentMenu("KMonoBehaviour/scripts/WiltCondition")]
public class WiltCondition : KMonoBehaviour
{
	// Token: 0x06003C3C RID: 15420 RVA: 0x0014DA59 File Offset: 0x0014BC59
	public bool IsWilting()
	{
		return this.wilting;
	}

	// Token: 0x06003C3D RID: 15421 RVA: 0x0014DA64 File Offset: 0x0014BC64
	public List<WiltCondition.Condition> CurrentWiltSources()
	{
		List<WiltCondition.Condition> list = new List<WiltCondition.Condition>();
		foreach (KeyValuePair<int, bool> keyValuePair in this.WiltConditions)
		{
			if (!keyValuePair.Value)
			{
				list.Add((WiltCondition.Condition)keyValuePair.Key);
			}
		}
		return list;
	}

	// Token: 0x06003C3E RID: 15422 RVA: 0x0014DAD0 File Offset: 0x0014BCD0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.WiltConditions.Add(0, true);
		this.WiltConditions.Add(1, true);
		this.WiltConditions.Add(2, true);
		this.WiltConditions.Add(3, true);
		this.WiltConditions.Add(4, true);
		this.WiltConditions.Add(5, true);
		this.WiltConditions.Add(6, true);
		this.WiltConditions.Add(7, true);
		this.WiltConditions.Add(9, true);
		this.WiltConditions.Add(10, true);
		this.WiltConditions.Add(11, true);
		this.WiltConditions.Add(12, true);
		this.WiltConditions.Add(13, true);
		base.Subscribe<WiltCondition>(-107174716, WiltCondition.SetTemperatureFalseDelegate);
		base.Subscribe<WiltCondition>(-1758196852, WiltCondition.SetTemperatureFalseDelegate);
		base.Subscribe<WiltCondition>(-1234705021, WiltCondition.SetTemperatureFalseDelegate);
		base.Subscribe<WiltCondition>(-55477301, WiltCondition.SetTemperatureFalseDelegate);
		base.Subscribe<WiltCondition>(115888613, WiltCondition.SetTemperatureTrueDelegate);
		base.Subscribe<WiltCondition>(-593125877, WiltCondition.SetPressureFalseDelegate);
		base.Subscribe<WiltCondition>(-1175525437, WiltCondition.SetPressureFalseDelegate);
		base.Subscribe<WiltCondition>(-907106982, WiltCondition.SetPressureTrueDelegate);
		base.Subscribe<WiltCondition>(103243573, WiltCondition.SetPressureFalseDelegate);
		base.Subscribe<WiltCondition>(646131325, WiltCondition.SetPressureFalseDelegate);
		base.Subscribe<WiltCondition>(221594799, WiltCondition.SetAtmosphereElementFalseDelegate);
		base.Subscribe<WiltCondition>(777259436, WiltCondition.SetAtmosphereElementTrueDelegate);
		base.Subscribe<WiltCondition>(1949704522, WiltCondition.SetDrowningFalseDelegate);
		base.Subscribe<WiltCondition>(99949694, WiltCondition.SetDrowningTrueDelegate);
		base.Subscribe<WiltCondition>(-2057657673, WiltCondition.SetDryingOutFalseDelegate);
		base.Subscribe<WiltCondition>(1555379996, WiltCondition.SetDryingOutTrueDelegate);
		base.Subscribe<WiltCondition>(-370379773, WiltCondition.SetIrrigationFalseDelegate);
		base.Subscribe<WiltCondition>(207387507, WiltCondition.SetIrrigationTrueDelegate);
		base.Subscribe<WiltCondition>(-1073674739, WiltCondition.SetFertilizedFalseDelegate);
		base.Subscribe<WiltCondition>(-1396791468, WiltCondition.SetFertilizedTrueDelegate);
		base.Subscribe<WiltCondition>(1113102781, WiltCondition.SetIlluminationComfortTrueDelegate);
		base.Subscribe<WiltCondition>(1387626797, WiltCondition.SetIlluminationComfortFalseDelegate);
		base.Subscribe<WiltCondition>(1628751838, WiltCondition.SetReceptacleTrueDelegate);
		base.Subscribe<WiltCondition>(960378201, WiltCondition.SetReceptacleFalseDelegate);
		base.Subscribe<WiltCondition>(-1089732772, WiltCondition.SetEntombedDelegate);
		base.Subscribe<WiltCondition>(912965142, WiltCondition.SetRootHealthDelegate);
		base.Subscribe<WiltCondition>(874353739, WiltCondition.SetRadiationComfortTrueDelegate);
		base.Subscribe<WiltCondition>(1788072223, WiltCondition.SetRadiationComfortFalseDelegate);
		base.Subscribe<WiltCondition>(-200207042, WiltCondition.SetPollinatedDelegate);
	}

	// Token: 0x06003C3F RID: 15423 RVA: 0x0014DD80 File Offset: 0x0014BF80
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.CheckShouldWilt();
		if (this.wilting)
		{
			this.DoWilt();
			if (!this.goingToWilt)
			{
				this.goingToWilt = true;
				this.Recover();
				return;
			}
		}
		else
		{
			this.DoRecover();
			if (this.goingToWilt)
			{
				this.goingToWilt = false;
				this.Wilt();
			}
		}
	}

	// Token: 0x06003C40 RID: 15424 RVA: 0x0014DDD8 File Offset: 0x0014BFD8
	protected override void OnCleanUp()
	{
		this.wiltSchedulerHandler.ClearScheduler();
		this.recoverSchedulerHandler.ClearScheduler();
		base.OnCleanUp();
	}

	// Token: 0x06003C41 RID: 15425 RVA: 0x0014DDF8 File Offset: 0x0014BFF8
	public bool IsConditionSatisifed(WiltCondition.Condition condition)
	{
		bool flag;
		return this.WiltConditions.TryGetValue((int)condition, out flag) && flag;
	}

	// Token: 0x06003C42 RID: 15426 RVA: 0x0014DE15 File Offset: 0x0014C015
	private void SetCondition(WiltCondition.Condition condition, bool satisfiedState)
	{
		if (!this.WiltConditions.ContainsKey((int)condition))
		{
			return;
		}
		this.WiltConditions[(int)condition] = satisfiedState;
		this.CheckShouldWilt();
	}

	// Token: 0x06003C43 RID: 15427 RVA: 0x0014DE3C File Offset: 0x0014C03C
	private void CheckShouldWilt()
	{
		bool flag = false;
		foreach (KeyValuePair<int, bool> keyValuePair in this.WiltConditions)
		{
			if (!keyValuePair.Value)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			if (!this.goingToWilt)
			{
				this.Wilt();
				return;
			}
		}
		else if (this.goingToWilt)
		{
			this.Recover();
		}
	}

	// Token: 0x06003C44 RID: 15428 RVA: 0x0014DEB8 File Offset: 0x0014C0B8
	private void Wilt()
	{
		if (!this.goingToWilt)
		{
			this.goingToWilt = true;
			this.recoverSchedulerHandler.ClearScheduler();
			if (!this.wiltSchedulerHandler.IsValid)
			{
				this.wiltSchedulerHandler = GameScheduler.Instance.Schedule("Wilt", this.WiltDelay, new Action<object>(WiltCondition.DoWiltCallback), this, null);
			}
		}
	}

	// Token: 0x06003C45 RID: 15429 RVA: 0x0014DF18 File Offset: 0x0014C118
	private void Recover()
	{
		if (this.goingToWilt)
		{
			this.goingToWilt = false;
			this.wiltSchedulerHandler.ClearScheduler();
			if (!this.recoverSchedulerHandler.IsValid)
			{
				this.recoverSchedulerHandler = GameScheduler.Instance.Schedule("Recover", this.RecoveryDelay, new Action<object>(WiltCondition.DoRecoverCallback), this, null);
			}
		}
	}

	// Token: 0x06003C46 RID: 15430 RVA: 0x0014DF75 File Offset: 0x0014C175
	private static void DoWiltCallback(object data)
	{
		((WiltCondition)data).DoWilt();
	}

	// Token: 0x06003C47 RID: 15431 RVA: 0x0014DF84 File Offset: 0x0014C184
	private void DoWilt()
	{
		this.wiltSchedulerHandler.ClearScheduler();
		KSelectable component = base.GetComponent<KSelectable>();
		component.GetComponent<KPrefabID>().AddTag(GameTags.Wilting, false);
		if (!this.wilting)
		{
			this.wilting = true;
			base.Trigger(-724860998, null);
		}
		if (this.rm != null)
		{
			if (this.rm.Replanted)
			{
				component.AddStatusItem(Db.Get().CreatureStatusItems.WiltingDomestic, base.GetComponent<ReceptacleMonitor>());
				return;
			}
			component.AddStatusItem(Db.Get().CreatureStatusItems.Wilting, base.GetComponent<ReceptacleMonitor>());
			return;
		}
		else
		{
			ReceptacleMonitor.StatesInstance smi = component.GetSMI<ReceptacleMonitor.StatesInstance>();
			if (smi != null && !smi.IsInsideState(smi.sm.wild))
			{
				component.AddStatusItem(Db.Get().CreatureStatusItems.WiltingNonGrowingDomestic, this);
				return;
			}
			component.AddStatusItem(Db.Get().CreatureStatusItems.WiltingNonGrowing, this);
			return;
		}
	}

	// Token: 0x06003C48 RID: 15432 RVA: 0x0014E070 File Offset: 0x0014C270
	public string WiltCausesString()
	{
		string text = "";
		List<IWiltCause> allSMI = this.GetAllSMI<IWiltCause>();
		allSMI.AddRange(base.GetComponents<IWiltCause>());
		foreach (IWiltCause wiltCause in allSMI)
		{
			foreach (WiltCondition.Condition condition in wiltCause.Conditions)
			{
				if (this.WiltConditions.ContainsKey((int)condition) && !this.WiltConditions[(int)condition])
				{
					text += "\n";
					text += wiltCause.WiltStateString;
					break;
				}
			}
		}
		return text;
	}

	// Token: 0x06003C49 RID: 15433 RVA: 0x0014E128 File Offset: 0x0014C328
	private static void DoRecoverCallback(object data)
	{
		((WiltCondition)data).DoRecover();
	}

	// Token: 0x06003C4A RID: 15434 RVA: 0x0014E138 File Offset: 0x0014C338
	private void DoRecover()
	{
		this.recoverSchedulerHandler.ClearScheduler();
		KSelectable component = base.GetComponent<KSelectable>();
		this.wilting = false;
		component.RemoveStatusItem(Db.Get().CreatureStatusItems.WiltingDomestic, false);
		component.RemoveStatusItem(Db.Get().CreatureStatusItems.Wilting, false);
		component.RemoveStatusItem(Db.Get().CreatureStatusItems.WiltingNonGrowing, false);
		component.RemoveStatusItem(Db.Get().CreatureStatusItems.WiltingNonGrowingDomestic, false);
		component.GetComponent<KPrefabID>().RemoveTag(GameTags.Wilting);
		base.Trigger(712767498, null);
	}

	// Token: 0x040024ED RID: 9453
	[MyCmpGet]
	private ReceptacleMonitor rm;

	// Token: 0x040024EE RID: 9454
	[Serialize]
	private bool goingToWilt;

	// Token: 0x040024EF RID: 9455
	[Serialize]
	private bool wilting;

	// Token: 0x040024F0 RID: 9456
	private Dictionary<int, bool> WiltConditions = new Dictionary<int, bool>();

	// Token: 0x040024F1 RID: 9457
	public float WiltDelay = 1f;

	// Token: 0x040024F2 RID: 9458
	public float RecoveryDelay = 1f;

	// Token: 0x040024F3 RID: 9459
	private SchedulerHandle wiltSchedulerHandler;

	// Token: 0x040024F4 RID: 9460
	private SchedulerHandle recoverSchedulerHandler;

	// Token: 0x040024F5 RID: 9461
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetTemperatureFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Temperature, false);
	});

	// Token: 0x040024F6 RID: 9462
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetTemperatureTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Temperature, true);
	});

	// Token: 0x040024F7 RID: 9463
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetPressureFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Pressure, false);
	});

	// Token: 0x040024F8 RID: 9464
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetPressureTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Pressure, true);
	});

	// Token: 0x040024F9 RID: 9465
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetAtmosphereElementFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.AtmosphereElement, false);
	});

	// Token: 0x040024FA RID: 9466
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetAtmosphereElementTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.AtmosphereElement, true);
	});

	// Token: 0x040024FB RID: 9467
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetDrowningFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Drowning, false);
	});

	// Token: 0x040024FC RID: 9468
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetDrowningTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Drowning, true);
	});

	// Token: 0x040024FD RID: 9469
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetDryingOutFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.DryingOut, false);
	});

	// Token: 0x040024FE RID: 9470
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetDryingOutTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.DryingOut, true);
	});

	// Token: 0x040024FF RID: 9471
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetIrrigationFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Irrigation, false);
	});

	// Token: 0x04002500 RID: 9472
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetIrrigationTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Irrigation, true);
	});

	// Token: 0x04002501 RID: 9473
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetFertilizedFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Fertilized, false);
	});

	// Token: 0x04002502 RID: 9474
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetFertilizedTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Fertilized, true);
	});

	// Token: 0x04002503 RID: 9475
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetIlluminationComfortFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.IlluminationComfort, false);
	});

	// Token: 0x04002504 RID: 9476
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetIlluminationComfortTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.IlluminationComfort, true);
	});

	// Token: 0x04002505 RID: 9477
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetReceptacleFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Receptacle, false);
	});

	// Token: 0x04002506 RID: 9478
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetReceptacleTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Receptacle, true);
	});

	// Token: 0x04002507 RID: 9479
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetEntombedDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Entombed, !(bool)data);
	});

	// Token: 0x04002508 RID: 9480
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetRootHealthDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.UnhealthyRoot, (bool)data);
	});

	// Token: 0x04002509 RID: 9481
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetRadiationComfortFalseDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Radiation, false);
	});

	// Token: 0x0400250A RID: 9482
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetRadiationComfortTrueDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Radiation, true);
	});

	// Token: 0x0400250B RID: 9483
	private static readonly EventSystem.IntraObjectHandler<WiltCondition> SetPollinatedDelegate = new EventSystem.IntraObjectHandler<WiltCondition>(delegate(WiltCondition component, object data)
	{
		component.SetCondition(WiltCondition.Condition.Pollination, (bool)data);
	});

	// Token: 0x02001857 RID: 6231
	public enum Condition
	{
		// Token: 0x040078A7 RID: 30887
		Temperature,
		// Token: 0x040078A8 RID: 30888
		Pressure,
		// Token: 0x040078A9 RID: 30889
		AtmosphereElement,
		// Token: 0x040078AA RID: 30890
		Drowning,
		// Token: 0x040078AB RID: 30891
		Fertilized,
		// Token: 0x040078AC RID: 30892
		DryingOut,
		// Token: 0x040078AD RID: 30893
		Irrigation,
		// Token: 0x040078AE RID: 30894
		IlluminationComfort,
		// Token: 0x040078AF RID: 30895
		Darkness,
		// Token: 0x040078B0 RID: 30896
		Receptacle,
		// Token: 0x040078B1 RID: 30897
		Entombed,
		// Token: 0x040078B2 RID: 30898
		UnhealthyRoot,
		// Token: 0x040078B3 RID: 30899
		Radiation,
		// Token: 0x040078B4 RID: 30900
		Pollination,
		// Token: 0x040078B5 RID: 30901
		Count
	}
}
