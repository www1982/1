using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000233 RID: 563
public class GeothermalVentConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000B49 RID: 2889 RVA: 0x00044799 File Offset: 0x00042999
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x000447A0 File Offset: 0x000429A0
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x000447A4 File Offset: 0x000429A4
	public virtual GameObject CreatePrefab()
	{
		string text = "GeothermalVentEntity";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.GEOTHERMALVENT.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.GEOTHERMALVENT.EFFECT + "\n\n" + global::STRINGS.BUILDINGS.PREFABS.GEOTHERMALVENT.DESC;
		float num = 100f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.PENALTY.TIER4;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_geospout_kanim"), "off", Grid.SceneLayer.BuildingBack, 3, 4, tier, tier2, SimHashes.Unobtanium, new List<Tag> { GameTags.Gravitas }, 293f);
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddComponent<GeothermalVent>();
		gameObject.AddComponent<GeothermalPlantComponent>();
		gameObject.AddComponent<Operational>();
		gameObject.AddComponent<UserNameable>();
		Storage storage = gameObject.AddComponent<Storage>();
		storage.showCapacityAsMainStatus = false;
		storage.showCapacityStatusItem = false;
		storage.showDescriptor = false;
		return gameObject;
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00044878 File Offset: 0x00042A78
	public void OnPrefabInit(GameObject inst)
	{
		LogicPorts logicPorts = inst.AddOrGet<LogicPorts>();
		logicPorts.inputPortInfo = new LogicPorts.Port[0];
		logicPorts.outputPortInfo = new LogicPorts.Port[] { LogicPorts.Port.OutputPort("GEOTHERMAL_VENT_STATUS_PORT", new CellOffset(0, 0), global::STRINGS.BUILDINGS.PREFABS.GEOTHERMALVENT.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.GEOTHERMALVENT.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.GEOTHERMALVENT.LOGIC_PORT_INACTIVE, false, false) };
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x000448DF File Offset: 0x00042ADF
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040007BB RID: 1979
	public const string ID = "GeothermalVentEntity";

	// Token: 0x040007BC RID: 1980
	public const string OUTPUT_LOGIC_PORT_ID = "GEOTHERMAL_VENT_STATUS_PORT";

	// Token: 0x040007BD RID: 1981
	private const string ANIM_FILE = "gravitas_geospout_kanim";

	// Token: 0x040007BE RID: 1982
	public const string OFFLINE_ANIM = "off";

	// Token: 0x040007BF RID: 1983
	public const string QUEST_ENTOMBED_ANIM = "pooped";

	// Token: 0x040007C0 RID: 1984
	public const string IDLE_ANIM = "on";

	// Token: 0x040007C1 RID: 1985
	public const string OBSTRUCTED_ANIM = "over_pressure";

	// Token: 0x040007C2 RID: 1986
	public const int EMISSION_RANGE = 1;

	// Token: 0x040007C3 RID: 1987
	public const float EMISSION_INTERVAL_SEC = 0.2f;

	// Token: 0x040007C4 RID: 1988
	public const float EMISSION_MAX_PRESSURE_KG = 120f;

	// Token: 0x040007C5 RID: 1989
	public const float EMISSION_MAX_RATE_PER_TICK = 3f;

	// Token: 0x040007C6 RID: 1990
	public static string TOGGLE_ANIMATION = "working_loop";

	// Token: 0x040007C7 RID: 1991
	public static HashedString TOGGLE_ANIM_OVERRIDE = "anim_interacts_geospout_kanim";

	// Token: 0x040007C8 RID: 1992
	public const float TOGGLE_CHORE_DURATION_SECONDS = 10f;

	// Token: 0x040007C9 RID: 1993
	public static MathUtil.MinMax INITIAL_DEBRIS_VELOCIOTY = new MathUtil.MinMax(1f, 5f);

	// Token: 0x040007CA RID: 1994
	public static MathUtil.MinMax INITIAL_DEBRIS_ANGLE = new MathUtil.MinMax(200f, 340f);

	// Token: 0x040007CB RID: 1995
	public static MathUtil.MinMax DEBRIS_MASS_KG = new MathUtil.MinMax(30f, 34f);

	// Token: 0x040007CC RID: 1996
	public const string BAROMETER_ANIM = "meter";

	// Token: 0x040007CD RID: 1997
	public const string BAROMETER_TARGET = "meter_target";

	// Token: 0x040007CE RID: 1998
	public static string[] BAROMETER_SYMBOLS = new string[] { "meter_target" };

	// Token: 0x040007CF RID: 1999
	public const string CONNECTED_ANIM = "meter_connected";

	// Token: 0x040007D0 RID: 2000
	public const string CONNECTED_TARGET = "meter_connected_target";

	// Token: 0x040007D1 RID: 2001
	public static string[] CONNECTED_SYMBOLS = new string[] { "meter_connected_target" };

	// Token: 0x040007D2 RID: 2002
	public const float CONNECTED_PROGRESS = 1f;

	// Token: 0x040007D3 RID: 2003
	public const float DISCONNECTED_PROGRESS = 0f;
}
