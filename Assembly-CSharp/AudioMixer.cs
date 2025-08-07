using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000545 RID: 1349
public class AudioMixer
{
	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000A218D File Offset: 0x000A038D
	public static AudioMixer instance
	{
		get
		{
			return AudioMixer._instance;
		}
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x000A2194 File Offset: 0x000A0394
	public static AudioMixer Create()
	{
		AudioMixer._instance = new AudioMixer();
		AudioMixerSnapshots audioMixerSnapshots = AudioMixerSnapshots.Get();
		if (audioMixerSnapshots != null)
		{
			audioMixerSnapshots.ReloadSnapshots();
		}
		return AudioMixer._instance;
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x000A21C5 File Offset: 0x000A03C5
	public static void Destroy()
	{
		AudioMixer._instance.StopAll(FMOD.Studio.STOP_MODE.IMMEDIATE);
		AudioMixer._instance = null;
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x000A21D8 File Offset: 0x000A03D8
	public EventInstance Start(EventReference event_ref)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		return this.Start(text);
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x000A2204 File Offset: 0x000A0404
	public EventInstance Start(string snapshot)
	{
		EventInstance eventInstance;
		if (!this.activeSnapshots.TryGetValue(snapshot, out eventInstance))
		{
			if (RuntimeManager.IsInitialized)
			{
				eventInstance = KFMOD.CreateInstance(snapshot);
				this.activeSnapshots[snapshot] = eventInstance;
				eventInstance.start();
				eventInstance.setParameterByName("snapshotActive", 1f, false);
			}
			else
			{
				eventInstance = default(EventInstance);
			}
		}
		AudioMixer.instance.Log("Start Snapshot: " + snapshot);
		return eventInstance;
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x000A2284 File Offset: 0x000A0484
	public bool Stop(EventReference event_ref, FMOD.Studio.STOP_MODE stop_mode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		return this.Stop(text, stop_mode);
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x000A22B4 File Offset: 0x000A04B4
	public bool Stop(HashedString snapshot, FMOD.Studio.STOP_MODE stop_mode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
	{
		bool flag = false;
		EventInstance eventInstance;
		if (this.activeSnapshots.TryGetValue(snapshot, out eventInstance))
		{
			eventInstance.setParameterByName("snapshotActive", 0f, false);
			eventInstance.stop(stop_mode);
			eventInstance.release();
			this.activeSnapshots.Remove(snapshot);
			flag = true;
			AudioMixer instance = AudioMixer.instance;
			string[] array = new string[5];
			array[0] = "Stop Snapshot: [";
			int num = 1;
			HashedString hashedString = snapshot;
			array[num] = hashedString.ToString();
			array[2] = "] with fadeout mode: [";
			array[3] = stop_mode.ToString();
			array[4] = "]";
			instance.Log(string.Concat(array));
		}
		else
		{
			AudioMixer instance2 = AudioMixer.instance;
			string text = "Tried to stop snapshot: [";
			HashedString hashedString = snapshot;
			instance2.Log(text + hashedString.ToString() + "] but it wasn't active.");
		}
		return flag;
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x000A2383 File Offset: 0x000A0583
	public void Reset()
	{
		this.StopAll(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x000A238C File Offset: 0x000A058C
	public void StopAll(FMOD.Studio.STOP_MODE stop_mode = FMOD.Studio.STOP_MODE.IMMEDIATE)
	{
		List<HashedString> list = new List<HashedString>();
		foreach (KeyValuePair<HashedString, EventInstance> keyValuePair in this.activeSnapshots)
		{
			if (keyValuePair.Key != AudioMixer.UserVolumeSettingsHash)
			{
				list.Add(keyValuePair.Key);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			this.Stop(list[i], stop_mode);
		}
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x000A2420 File Offset: 0x000A0620
	public bool SnapshotIsActive(EventReference event_ref)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		return this.SnapshotIsActive(text);
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x000A244F File Offset: 0x000A064F
	public bool SnapshotIsActive(HashedString snapshot_name)
	{
		return this.activeSnapshots.ContainsKey(snapshot_name);
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x000A2464 File Offset: 0x000A0664
	public void SetSnapshotParameter(EventReference event_ref, string parameter_name, float parameter_value, bool shouldLog = true)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		this.SetSnapshotParameter(text, parameter_name, parameter_value, shouldLog);
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x000A2494 File Offset: 0x000A0694
	public void SetSnapshotParameter(string snapshot_name, string parameter_name, float parameter_value, bool shouldLog = true)
	{
		if (shouldLog)
		{
			this.Log(string.Format("Set Param {0}: {1}, {2}", snapshot_name, parameter_name, parameter_value));
		}
		EventInstance eventInstance;
		if (this.activeSnapshots.TryGetValue(snapshot_name, out eventInstance))
		{
			eventInstance.setParameterByName(parameter_name, parameter_value, false);
			return;
		}
		this.Log(string.Concat(new string[]
		{
			"Tried to set [",
			parameter_name,
			"] to [",
			parameter_value.ToString(),
			"] but [",
			snapshot_name,
			"] is not active."
		}));
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x000A2524 File Offset: 0x000A0724
	public void StartPersistentSnapshots()
	{
		this.persistentSnapshotsActive = true;
		this.Start(AudioMixerSnapshots.Get().DuplicantCountAttenuatorMigrated);
		this.Start(AudioMixerSnapshots.Get().DuplicantCountMovingSnapshot);
		this.Start(AudioMixerSnapshots.Get().DuplicantCountSleepingSnapshot);
		this.spaceVisibleInst = this.Start(AudioMixerSnapshots.Get().SpaceVisibleSnapshot);
		this.facilityVisibleInst = this.Start(AudioMixerSnapshots.Get().FacilityVisibleSnapshot);
		this.Start(AudioMixerSnapshots.Get().PulseSnapshot);
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x000A25A8 File Offset: 0x000A07A8
	public void StopPersistentSnapshots()
	{
		this.persistentSnapshotsActive = false;
		this.Stop(AudioMixerSnapshots.Get().DuplicantCountAttenuatorMigrated, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().DuplicantCountMovingSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().DuplicantCountSleepingSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().SpaceVisibleSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().FacilityVisibleSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().PulseSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x000A2628 File Offset: 0x000A0828
	private string GetSnapshotName(EventReference event_ref)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		return text;
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x000A264C File Offset: 0x000A084C
	public void UpdatePersistentSnapshotParameters()
	{
		this.SetVisibleDuplicants();
		string snapshotName = this.GetSnapshotName(AudioMixerSnapshots.Get().DuplicantCountMovingSnapshot);
		if (this.activeSnapshots.TryGetValue(snapshotName, out this.duplicantCountMovingInst))
		{
			this.duplicantCountMovingInst.setParameterByName("duplicantCount", (float)Mathf.Max(0, this.visibleDupes["moving"] - AudioMixer.VISIBLE_DUPLICANTS_BEFORE_ATTENUATION), false);
		}
		string snapshotName2 = this.GetSnapshotName(AudioMixerSnapshots.Get().DuplicantCountSleepingSnapshot);
		if (this.activeSnapshots.TryGetValue(snapshotName2, out this.duplicantCountSleepingInst))
		{
			this.duplicantCountSleepingInst.setParameterByName("duplicantCount", (float)Mathf.Max(0, this.visibleDupes["sleeping"] - AudioMixer.VISIBLE_DUPLICANTS_BEFORE_ATTENUATION), false);
		}
		string snapshotName3 = this.GetSnapshotName(AudioMixerSnapshots.Get().DuplicantCountAttenuatorMigrated);
		if (this.activeSnapshots.TryGetValue(snapshotName3, out this.duplicantCountInst))
		{
			this.duplicantCountInst.setParameterByName("duplicantCount", (float)Mathf.Max(0, this.visibleDupes["visible"] - AudioMixer.VISIBLE_DUPLICANTS_BEFORE_ATTENUATION), false);
		}
		string snapshotName4 = this.GetSnapshotName(AudioMixerSnapshots.Get().PulseSnapshot);
		if (this.activeSnapshots.TryGetValue(snapshotName4, out this.pulseInst))
		{
			float num = AudioMixer.PULSE_SNAPSHOT_BPM / 60f;
			int speed = SpeedControlScreen.Instance.GetSpeed();
			if (speed == 1)
			{
				num /= 2f;
			}
			else if (speed == 2)
			{
				num /= 3f;
			}
			float num2 = Mathf.Abs(Mathf.Sin(Time.time * 3.1415927f * num));
			this.pulseInst.setParameterByName("Pulse", num2, false);
		}
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x000A27FB File Offset: 0x000A09FB
	public void UpdateSpaceVisibleSnapshot(float percent)
	{
		this.spaceVisibleInst.setParameterByName("spaceVisible", percent, false);
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x000A2810 File Offset: 0x000A0A10
	public void PauseSpaceVisibleSnapshot(bool pause)
	{
		this.spaceVisibleInst.setParameterByName("spaceVisible", 0f, true);
		this.spaceVisibleInst.setPaused(pause);
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x000A2836 File Offset: 0x000A0A36
	public void UpdateFacilityVisibleSnapshot(float percent)
	{
		this.facilityVisibleInst.setParameterByName("facilityVisible", percent, false);
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x000A284C File Offset: 0x000A0A4C
	private void SetVisibleDuplicants()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
		{
			Vector3 position = Components.LiveMinionIdentities[i].transform.GetPosition();
			if (CameraController.Instance.IsVisiblePos(position))
			{
				num++;
				Navigator component = Components.LiveMinionIdentities[i].GetComponent<Navigator>();
				if (component != null && component.IsMoving())
				{
					num2++;
				}
				else
				{
					StaminaMonitor.Instance smi = Components.LiveMinionIdentities[i].GetComponent<WorkerBase>().GetSMI<StaminaMonitor.Instance>();
					if (smi != null && smi.IsSleeping())
					{
						num3++;
					}
				}
			}
		}
		this.visibleDupes["visible"] = num;
		this.visibleDupes["moving"] = num2;
		this.visibleDupes["sleeping"] = num3;
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x000A292C File Offset: 0x000A0B2C
	public void StartUserVolumesSnapshot()
	{
		this.Start(AudioMixerSnapshots.Get().UserVolumeSettingsSnapshot);
		string snapshotName = this.GetSnapshotName(AudioMixerSnapshots.Get().UserVolumeSettingsSnapshot);
		EventInstance eventInstance;
		if (this.activeSnapshots.TryGetValue(snapshotName, out eventInstance))
		{
			EventDescription eventDescription;
			eventInstance.getDescription(out eventDescription);
			USER_PROPERTY user_PROPERTY;
			eventDescription.getUserProperty("buses", out user_PROPERTY);
			string text = user_PROPERTY.stringValue();
			char c = '-';
			string[] array = text.Split(c, StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				float num = 1f;
				string text2 = "Volume_" + array[i];
				if (KPlayerPrefs.HasKey(text2))
				{
					num = KPlayerPrefs.GetFloat(text2);
				}
				AudioMixer.UserVolumeBus userVolumeBus = new AudioMixer.UserVolumeBus();
				userVolumeBus.busLevel = num;
				userVolumeBus.labelString = Strings.Get("STRINGS.UI.FRONTEND.AUDIO_OPTIONS_SCREEN.AUDIO_BUS_" + array[i].ToUpper());
				this.userVolumeSettings.Add(array[i], userVolumeBus);
				this.SetUserVolume(array[i], userVolumeBus.busLevel);
			}
		}
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x000A2A40 File Offset: 0x000A0C40
	public void SetUserVolume(string bus, float value)
	{
		if (!this.userVolumeSettings.ContainsKey(bus))
		{
			global::Debug.LogError("The provided bus doesn't exist. Check yo'self fool!");
			return;
		}
		if (value > 1f)
		{
			value = 1f;
		}
		else if (value < 0f)
		{
			value = 0f;
		}
		this.userVolumeSettings[bus].busLevel = value;
		KPlayerPrefs.SetFloat("Volume_" + bus, value);
		string snapshotName = this.GetSnapshotName(AudioMixerSnapshots.Get().UserVolumeSettingsSnapshot);
		EventInstance eventInstance;
		if (this.activeSnapshots.TryGetValue(snapshotName, out eventInstance))
		{
			eventInstance.setParameterByName("userVolume_" + bus, this.userVolumeSettings[bus].busLevel, false);
		}
		else
		{
			this.Log(string.Concat(new string[]
			{
				"Tried to set [",
				bus,
				"] to [",
				value.ToString(),
				"] but UserVolumeSettingsSnapshot is not active."
			}));
		}
		if (bus == "Music")
		{
			this.SetSnapshotParameter(AudioMixerSnapshots.Get().DynamicMusicPlayingSnapshot, "userVolume_Music", value, true);
		}
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x000A2B51 File Offset: 0x000A0D51
	private void Log(string s)
	{
	}

	// Token: 0x04001166 RID: 4454
	private static AudioMixer _instance = null;

	// Token: 0x04001167 RID: 4455
	private const string DUPLICANT_COUNT_ID = "duplicantCount";

	// Token: 0x04001168 RID: 4456
	private const string PULSE_ID = "Pulse";

	// Token: 0x04001169 RID: 4457
	private const string SNAPSHOT_ACTIVE_ID = "snapshotActive";

	// Token: 0x0400116A RID: 4458
	private const string SPACE_VISIBLE_ID = "spaceVisible";

	// Token: 0x0400116B RID: 4459
	private const string FACILITY_VISIBLE_ID = "facilityVisible";

	// Token: 0x0400116C RID: 4460
	private const string FOCUS_BUS_PATH = "bus:/SFX/Focus";

	// Token: 0x0400116D RID: 4461
	public Dictionary<HashedString, EventInstance> activeSnapshots = new Dictionary<HashedString, EventInstance>();

	// Token: 0x0400116E RID: 4462
	public List<HashedString> SnapshotDebugLog = new List<HashedString>();

	// Token: 0x0400116F RID: 4463
	public bool activeNIS;

	// Token: 0x04001170 RID: 4464
	public static float LOW_PRIORITY_CUTOFF_DISTANCE = 10f;

	// Token: 0x04001171 RID: 4465
	public static float PULSE_SNAPSHOT_BPM = 120f;

	// Token: 0x04001172 RID: 4466
	public static int VISIBLE_DUPLICANTS_BEFORE_ATTENUATION = 2;

	// Token: 0x04001173 RID: 4467
	private EventInstance duplicantCountInst;

	// Token: 0x04001174 RID: 4468
	private EventInstance pulseInst;

	// Token: 0x04001175 RID: 4469
	private EventInstance duplicantCountMovingInst;

	// Token: 0x04001176 RID: 4470
	private EventInstance duplicantCountSleepingInst;

	// Token: 0x04001177 RID: 4471
	private EventInstance spaceVisibleInst;

	// Token: 0x04001178 RID: 4472
	private EventInstance facilityVisibleInst;

	// Token: 0x04001179 RID: 4473
	private static readonly HashedString UserVolumeSettingsHash = new HashedString("event:/Snapshots/Mixing/Snapshot_UserVolumeSettings");

	// Token: 0x0400117A RID: 4474
	public bool persistentSnapshotsActive;

	// Token: 0x0400117B RID: 4475
	private Dictionary<string, int> visibleDupes = new Dictionary<string, int>();

	// Token: 0x0400117C RID: 4476
	public Dictionary<string, AudioMixer.UserVolumeBus> userVolumeSettings = new Dictionary<string, AudioMixer.UserVolumeBus>();

	// Token: 0x0200139B RID: 5019
	public class UserVolumeBus
	{
		// Token: 0x040069F8 RID: 27128
		public string labelString;

		// Token: 0x040069F9 RID: 27129
		public float busLevel;
	}
}
