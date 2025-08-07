using System;
using UnityEngine;

// Token: 0x02000B4E RID: 2894
public class LargeImpactorVanillaConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06005626 RID: 22054 RVA: 0x001F3CD3 File Offset: 0x001F1ED3
	public string[] GetRequiredDlcIds()
	{
		return new string[] { "", "DLC4_ID" };
	}

	// Token: 0x06005627 RID: 22055 RVA: 0x001F3CEB File Offset: 0x001F1EEB
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06005628 RID: 22056 RVA: 0x001F3CEE File Offset: 0x001F1EEE
	GameObject IEntityConfig.CreatePrefab()
	{
		return LargeImpactorVanillaConfig.ConfigCommon(LargeImpactorVanillaConfig.ID, LargeImpactorVanillaConfig.NAME);
	}

	// Token: 0x06005629 RID: 22057 RVA: 0x001F3D00 File Offset: 0x001F1F00
	public static GameObject ConfigCommon(string id, string name)
	{
		GameObject gameObject = EntityTemplates.CreateEntity(id, name, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<StateMachineController>();
		gameObject.AddOrGet<Notifier>();
		gameObject.AddOrGet<LoopingSounds>();
		LargeImpactorStatus.Def def = gameObject.AddOrGetDef<LargeImpactorStatus.Def>();
		def.MAX_HEALTH = 1000;
		def.EventID = "LargeImpactor";
		gameObject.AddOrGet<LargeImpactorVisualizer>();
		gameObject.AddOrGet<LargeImpactorCrashStamp>().largeStampTemplate = "dlc4::poi/asteroid_impacts/potato_large";
		gameObject.AddOrGetDef<LargeImpactorNotificationMonitor.Def>();
		gameObject.AddOrGet<ParallaxBackgroundObject>().Initialize("Demolior_final_whole");
		return gameObject;
	}

	// Token: 0x0600562A RID: 22058 RVA: 0x001F3D7A File Offset: 0x001F1F7A
	void IEntityConfig.OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600562B RID: 22059 RVA: 0x001F3D7C File Offset: 0x001F1F7C
	private static LargeImpactorStatus.Instance GetStatusMonitor()
	{
		return ((LargeImpactorEvent.StatesInstance)GameplayEventManager.Instance.GetGameplayEventInstance(Db.Get().GameplayEvents.LargeImpactor.Id, -1).smi).impactorInstance.GetSMI<LargeImpactorStatus.Instance>();
	}

	// Token: 0x0600562C RID: 22060 RVA: 0x001F3DB8 File Offset: 0x001F1FB8
	public static void SpawnCommon(GameObject inst)
	{
		ParallaxBackgroundObject component = inst.GetComponent<ParallaxBackgroundObject>();
		component.motion = new LargeImpactorVanillaConfig.BackgroundMotion();
		LargeImpactorStatus.Instance statusMonitor = LargeImpactorVanillaConfig.GetStatusMonitor();
		if (statusMonitor != null)
		{
			LargeImpactorStatus.Instance instance = statusMonitor;
			instance.OnDamaged = (Action<int>)Delegate.Combine(instance.OnDamaged, new Action<int>(component.TriggerShaderDamagedEffect));
		}
	}

	// Token: 0x0600562D RID: 22061 RVA: 0x001F3E02 File Offset: 0x001F2002
	void IEntityConfig.OnSpawn(GameObject inst)
	{
		LargeImpactorVanillaConfig.SpawnCommon(inst);
	}

	// Token: 0x040039AB RID: 14763
	public static string ID = "LargeImpactorVanilla";

	// Token: 0x040039AC RID: 14764
	public static string NAME = "LargestPotaytoeVanilla";

	// Token: 0x02001C74 RID: 7284
	public class BackgroundMotion : ParallaxBackgroundObject.IMotion
	{
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600AB25 RID: 43813 RVA: 0x003BD0CD File Offset: 0x003BB2CD
		private LargeImpactorStatus.Instance StatusMonitor
		{
			get
			{
				if (this.statusMonitor == null)
				{
					this.statusMonitor = LargeImpactorVanillaConfig.GetStatusMonitor();
				}
				return this.statusMonitor;
			}
		}

		// Token: 0x0600AB26 RID: 43814 RVA: 0x003BD0E8 File Offset: 0x003BB2E8
		public float GetETA()
		{
			if (!this.StatusMonitor.IsRunning())
			{
				return this.GetDuration();
			}
			return this.StatusMonitor.TimeRemainingBeforeCollision;
		}

		// Token: 0x0600AB27 RID: 43815 RVA: 0x003BD109 File Offset: 0x003BB309
		public float GetDuration()
		{
			return LargeImpactorEvent.GetImpactTime();
		}

		// Token: 0x0600AB28 RID: 43816 RVA: 0x003BD110 File Offset: 0x003BB310
		public void OnNormalizedDistanceChanged(float normalizedDistance)
		{
			AmbienceManager.Quadrant[] quadrants = Game.Instance.GetComponent<AmbienceManager>().quadrants;
			for (int i = 0; i < quadrants.Length; i++)
			{
				quadrants[i].spaceLayer.SetCustomParameter("distanceToMeteor", normalizedDistance);
			}
		}

		// Token: 0x04008664 RID: 34404
		private LargeImpactorStatus.Instance statusMonitor;
	}
}
