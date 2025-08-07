using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FMOD.Studio;
using KSerialization;
using ProcGen;
using UnityEngine;

// Token: 0x02000ABA RID: 2746
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/TimeOfDay")]
public class TimeOfDay : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x17000583 RID: 1411
	// (get) Token: 0x06004F9B RID: 20379 RVA: 0x001CCD58 File Offset: 0x001CAF58
	public static bool IsMilestoneApproaching
	{
		get
		{
			if (TimeOfDay.Instance != null && GameClock.Instance != null)
			{
				int currentTimeRegion = (int)TimeOfDay.Instance.GetCurrentTimeRegion();
				int cycle = GameClock.Instance.GetCycle();
				return currentTimeRegion == 2 && TimeOfDay.MILESTONE_CYCLES != null && TimeOfDay.MILESTONE_CYCLES.Contains(cycle + 1);
			}
			return false;
		}
	}

	// Token: 0x17000584 RID: 1412
	// (get) Token: 0x06004F9C RID: 20380 RVA: 0x001CCDB0 File Offset: 0x001CAFB0
	public static bool IsMilestoneDay
	{
		get
		{
			if (TimeOfDay.Instance != null && GameClock.Instance != null)
			{
				int currentTimeRegion = (int)TimeOfDay.Instance.GetCurrentTimeRegion();
				int cycle = GameClock.Instance.GetCycle();
				return currentTimeRegion == 1 && TimeOfDay.MILESTONE_CYCLES != null && TimeOfDay.MILESTONE_CYCLES.Contains(cycle);
			}
			return false;
		}
	}

	// Token: 0x17000585 RID: 1413
	// (get) Token: 0x06004F9E RID: 20382 RVA: 0x001CCE0E File Offset: 0x001CB00E
	// (set) Token: 0x06004F9D RID: 20381 RVA: 0x001CCE05 File Offset: 0x001CB005
	public TimeOfDay.TimeRegion timeRegion { get; private set; }

	// Token: 0x06004F9F RID: 20383 RVA: 0x001CCE16 File Offset: 0x001CB016
	public static void DestroyInstance()
	{
		TimeOfDay.Instance = null;
	}

	// Token: 0x06004FA0 RID: 20384 RVA: 0x001CCE1E File Offset: 0x001CB01E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		TimeOfDay.Instance = this;
	}

	// Token: 0x06004FA1 RID: 20385 RVA: 0x001CCE2C File Offset: 0x001CB02C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		TimeOfDay.Instance = null;
	}

	// Token: 0x06004FA2 RID: 20386 RVA: 0x001CCE3C File Offset: 0x001CB03C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.timeRegion = this.GetCurrentTimeRegion();
		string clusterId = SaveLoader.Instance.GameInfo.clusterId;
		ClusterLayout clusterData = SettingsCache.clusterLayouts.GetClusterData(clusterId);
		if (clusterData != null && !string.IsNullOrWhiteSpace(clusterData.clusterAudio.stingerDay))
		{
			this.stingerDay = clusterData.clusterAudio.stingerDay;
		}
		else
		{
			this.stingerDay = "Stinger_Day";
		}
		if (clusterData != null && !string.IsNullOrWhiteSpace(clusterData.clusterAudio.stingerNight))
		{
			this.stingerNight = clusterData.clusterAudio.stingerNight;
		}
		else
		{
			this.stingerNight = "Stinger_Loop_Night";
		}
		if (!MusicManager.instance.SongIsPlaying(this.stingerNight) && this.GetCurrentTimeRegion() == TimeOfDay.TimeRegion.Night)
		{
			MusicManager.instance.PlaySong(this.stingerNight, false);
			MusicManager.instance.SetSongParameter(this.stingerNight, "Music_PlayStinger", 0f, true);
		}
		this.UpdateSunlightIntensity();
	}

	// Token: 0x06004FA3 RID: 20387 RVA: 0x001CCF2B File Offset: 0x001CB12B
	[OnDeserialized]
	private void OnDeserialized()
	{
		this.UpdateVisuals();
	}

	// Token: 0x06004FA4 RID: 20388 RVA: 0x001CCF33 File Offset: 0x001CB133
	public TimeOfDay.TimeRegion GetCurrentTimeRegion()
	{
		if (GameClock.Instance.IsNighttime())
		{
			return TimeOfDay.TimeRegion.Night;
		}
		return TimeOfDay.TimeRegion.Day;
	}

	// Token: 0x06004FA5 RID: 20389 RVA: 0x001CCF44 File Offset: 0x001CB144
	private void Update()
	{
		this.UpdateVisuals();
		TimeOfDay.TimeRegion currentTimeRegion = this.GetCurrentTimeRegion();
		int cycle = GameClock.Instance.GetCycle();
		if (currentTimeRegion != this.timeRegion)
		{
			if (TimeOfDay.IsMilestoneApproaching)
			{
				Game.Instance.Trigger(-720092972, cycle);
			}
			if (TimeOfDay.IsMilestoneDay)
			{
				Game.Instance.Trigger(2070437606, cycle);
			}
			this.TriggerSoundChange(currentTimeRegion, TimeOfDay.IsMilestoneDay);
			this.timeRegion = currentTimeRegion;
			base.Trigger(1791086652, null);
		}
	}

	// Token: 0x06004FA6 RID: 20390 RVA: 0x001CCFCC File Offset: 0x001CB1CC
	private void UpdateVisuals()
	{
		float num = 0.875f;
		float num2 = 0.2f;
		float num3 = 1f;
		float num4 = 0f;
		if (GameClock.Instance.GetCurrentCycleAsPercentage() >= num)
		{
			num4 = num3;
		}
		this.scale = Mathf.Lerp(this.scale, num4, Time.deltaTime * num2);
		float num5 = this.UpdateSunlightIntensity();
		Shader.SetGlobalVector("_TimeOfDay", new Vector4(this.scale, num5, 0f, 0f));
	}

	// Token: 0x06004FA7 RID: 20391 RVA: 0x001CD042 File Offset: 0x001CB242
	public void Sim4000ms(float dt)
	{
		this.UpdateSunlightIntensity();
	}

	// Token: 0x06004FA8 RID: 20392 RVA: 0x001CD04B File Offset: 0x001CB24B
	public void SetEclipse(bool eclipse)
	{
		this.isEclipse = eclipse;
	}

	// Token: 0x06004FA9 RID: 20393 RVA: 0x001CD054 File Offset: 0x001CB254
	private float UpdateSunlightIntensity()
	{
		float daytimeDurationInPercentage = GameClock.Instance.GetDaytimeDurationInPercentage();
		float num = GameClock.Instance.GetCurrentCycleAsPercentage() / daytimeDurationInPercentage;
		if (num >= 1f || this.isEclipse)
		{
			num = 0f;
		}
		float num2 = Mathf.Sin(num * 3.1415927f);
		Game.Instance.currentFallbackSunlightIntensity = num2 * 80000f;
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			worldContainer.currentSunlightIntensity = num2 * (float)worldContainer.sunlight;
			worldContainer.currentCosmicIntensity = (float)worldContainer.cosmicRadiation;
		}
		return num2;
	}

	// Token: 0x06004FAA RID: 20394 RVA: 0x001CD114 File Offset: 0x001CB314
	private void TriggerSoundChange(TimeOfDay.TimeRegion new_region, bool milestoneReached)
	{
		if (new_region == TimeOfDay.TimeRegion.Day)
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().NightStartedMigrated, STOP_MODE.ALLOWFADEOUT);
			if (MusicManager.instance.SongIsPlaying(this.stingerNight))
			{
				MusicManager.instance.StopSong(this.stingerNight, true, STOP_MODE.ALLOWFADEOUT);
			}
			if (milestoneReached)
			{
				MusicManager.instance.PlaySong("Stinger_Day_Celebrate", false);
			}
			else
			{
				MusicManager.instance.PlaySong(this.stingerDay, false);
			}
			MusicManager.instance.PlayDynamicMusic();
			return;
		}
		if (new_region != TimeOfDay.TimeRegion.Night)
		{
			return;
		}
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().NightStartedMigrated);
		MusicManager.instance.PlaySong(this.stingerNight, false);
	}

	// Token: 0x06004FAB RID: 20395 RVA: 0x001CD1BB File Offset: 0x001CB3BB
	public void SetScale(float new_scale)
	{
		this.scale = new_scale;
	}

	// Token: 0x0400359C RID: 13724
	private const string MILESTONE_CYCLE_REACHED_AUDIO_NAME = "Stinger_Day_Celebrate";

	// Token: 0x0400359D RID: 13725
	public static List<int> MILESTONE_CYCLES = new List<int>(2) { 99, 999 };

	// Token: 0x0400359E RID: 13726
	[Serialize]
	private float scale;

	// Token: 0x040035A0 RID: 13728
	private EventInstance nightLPEvent;

	// Token: 0x040035A1 RID: 13729
	public static TimeOfDay Instance;

	// Token: 0x040035A2 RID: 13730
	public string stingerDay;

	// Token: 0x040035A3 RID: 13731
	public string stingerNight;

	// Token: 0x040035A4 RID: 13732
	private bool isEclipse;

	// Token: 0x02001B92 RID: 7058
	public enum TimeRegion
	{
		// Token: 0x0400833F RID: 33599
		Invalid,
		// Token: 0x04008340 RID: 33600
		Day,
		// Token: 0x04008341 RID: 33601
		Night
	}
}
