using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class ScoutRoverConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000680 RID: 1664 RVA: 0x0002EB6F File Offset: 0x0002CD6F
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0002EB76 File Offset: 0x0002CD76
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0002EB7C File Offset: 0x0002CD7C
	public GameObject CreatePrefab()
	{
		return BaseRoverConfig.BaseRover("ScoutRover", global::STRINGS.ROBOTS.MODELS.SCOUT.NAME, GameTags.Robots.Models.ScoutRover, global::STRINGS.ROBOTS.MODELS.SCOUT.DESC, "scout_bot_kanim", 100f, 1f, 2f, global::TUNING.ROBOTS.SCOUTBOT.CARRY_CAPACITY, global::TUNING.ROBOTS.SCOUTBOT.DIGGING, global::TUNING.ROBOTS.SCOUTBOT.CONSTRUCTION, global::TUNING.ROBOTS.SCOUTBOT.ATHLETICS, global::TUNING.ROBOTS.SCOUTBOT.HIT_POINTS, global::TUNING.ROBOTS.SCOUTBOT.BATTERY_CAPACITY, global::TUNING.ROBOTS.SCOUTBOT.BATTERY_DEPLETION_RATE, Db.Get().Amounts.InternalChemicalBattery, false);
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0002EBF3 File Offset: 0x0002CDF3
	public void OnPrefabInit(GameObject inst)
	{
		BaseRoverConfig.OnPrefabInit(inst, Db.Get().Amounts.InternalChemicalBattery);
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0002EC0C File Offset: 0x0002CE0C
	public void OnSpawn(GameObject inst)
	{
		BaseRoverConfig.OnSpawn(inst);
		Effects effects = inst.GetComponent<Effects>();
		if (inst.transform.parent == null)
		{
			if (effects.HasEffect("ScoutBotCharging"))
			{
				effects.Remove("ScoutBotCharging");
			}
		}
		else if (!effects.HasEffect("ScoutBotCharging"))
		{
			effects.Add("ScoutBotCharging", false);
		}
		inst.Subscribe(856640610, delegate(object data)
		{
			if (inst.transform.parent == null)
			{
				if (effects.HasEffect("ScoutBotCharging"))
				{
					effects.Remove("ScoutBotCharging");
					return;
				}
			}
			else if (!effects.HasEffect("ScoutBotCharging"))
			{
				effects.Add("ScoutBotCharging", false);
			}
		});
	}

	// Token: 0x040004E5 RID: 1253
	public const string ID = "ScoutRover";

	// Token: 0x040004E6 RID: 1254
	public const float MASS = 100f;

	// Token: 0x040004E7 RID: 1255
	private const float WIDTH = 1f;

	// Token: 0x040004E8 RID: 1256
	private const float HEIGHT = 2f;

	// Token: 0x040004E9 RID: 1257
	public const int MAXIMUM_TECH_CONSTRUCTION_TIER = 2;
}
