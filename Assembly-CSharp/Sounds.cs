using System;
using FMODUnity;
using UnityEngine;

// Token: 0x02000B24 RID: 2852
[AddComponentMenu("KMonoBehaviour/scripts/Sounds")]
public class Sounds : KMonoBehaviour
{
	// Token: 0x170005ED RID: 1517
	// (get) Token: 0x06005437 RID: 21559 RVA: 0x001EA2BC File Offset: 0x001E84BC
	// (set) Token: 0x06005438 RID: 21560 RVA: 0x001EA2C3 File Offset: 0x001E84C3
	public static Sounds Instance { get; private set; }

	// Token: 0x06005439 RID: 21561 RVA: 0x001EA2CB File Offset: 0x001E84CB
	public static void DestroyInstance()
	{
		Sounds.Instance = null;
	}

	// Token: 0x0600543A RID: 21562 RVA: 0x001EA2D3 File Offset: 0x001E84D3
	protected override void OnPrefabInit()
	{
		Sounds.Instance = this;
	}

	// Token: 0x04003890 RID: 14480
	public FMODAsset BlowUp_Generic;

	// Token: 0x04003891 RID: 14481
	public FMODAsset Build_Generic;

	// Token: 0x04003892 RID: 14482
	public FMODAsset InUse_Fabricator;

	// Token: 0x04003893 RID: 14483
	public FMODAsset InUse_OxygenGenerator;

	// Token: 0x04003894 RID: 14484
	public FMODAsset Place_OreOnSite;

	// Token: 0x04003895 RID: 14485
	public FMODAsset Footstep_rock;

	// Token: 0x04003896 RID: 14486
	public FMODAsset Ice_crack;

	// Token: 0x04003897 RID: 14487
	public FMODAsset BuildingPowerOn;

	// Token: 0x04003898 RID: 14488
	public FMODAsset ElectricGridOverload;

	// Token: 0x04003899 RID: 14489
	public FMODAsset IngameMusic;

	// Token: 0x0400389A RID: 14490
	public FMODAsset[] OreSplashSounds;

	// Token: 0x0400389C RID: 14492
	public EventReference BlowUp_GenericMigrated;

	// Token: 0x0400389D RID: 14493
	public EventReference Build_GenericMigrated;

	// Token: 0x0400389E RID: 14494
	public EventReference InUse_FabricatorMigrated;

	// Token: 0x0400389F RID: 14495
	public EventReference InUse_OxygenGeneratorMigrated;

	// Token: 0x040038A0 RID: 14496
	public EventReference Place_OreOnSiteMigrated;

	// Token: 0x040038A1 RID: 14497
	public EventReference Footstep_rockMigrated;

	// Token: 0x040038A2 RID: 14498
	public EventReference Ice_crackMigrated;

	// Token: 0x040038A3 RID: 14499
	public EventReference BuildingPowerOnMigrated;

	// Token: 0x040038A4 RID: 14500
	public EventReference ElectricGridOverloadMigrated;

	// Token: 0x040038A5 RID: 14501
	public EventReference IngameMusicMigrated;

	// Token: 0x040038A6 RID: 14502
	public EventReference[] OreSplashSoundsMigrated;
}
