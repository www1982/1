using System;
using System.Diagnostics;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x0200053A RID: 1338
[DebuggerDisplay("{Name}")]
public class SoundEvent : AnimEvent
{
	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0009FF67 File Offset: 0x0009E167
	// (set) Token: 0x06001D75 RID: 7541 RVA: 0x0009FF6F File Offset: 0x0009E16F
	public string sound { get; private set; }

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0009FF78 File Offset: 0x0009E178
	// (set) Token: 0x06001D77 RID: 7543 RVA: 0x0009FF80 File Offset: 0x0009E180
	public HashedString soundHash { get; private set; }

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0009FF89 File Offset: 0x0009E189
	// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0009FF91 File Offset: 0x0009E191
	public bool looping { get; private set; }

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0009FF9A File Offset: 0x0009E19A
	// (set) Token: 0x06001D7B RID: 7547 RVA: 0x0009FFA2 File Offset: 0x0009E1A2
	public bool ignorePause { get; set; }

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0009FFAB File Offset: 0x0009E1AB
	// (set) Token: 0x06001D7D RID: 7549 RVA: 0x0009FFB3 File Offset: 0x0009E1B3
	public bool shouldCameraScalePosition { get; set; }

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0009FFBC File Offset: 0x0009E1BC
	// (set) Token: 0x06001D7F RID: 7551 RVA: 0x0009FFC4 File Offset: 0x0009E1C4
	public float minInterval { get; private set; }

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0009FFCD File Offset: 0x0009E1CD
	// (set) Token: 0x06001D81 RID: 7553 RVA: 0x0009FFD5 File Offset: 0x0009E1D5
	public bool objectIsSelectedAndVisible { get; set; }

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06001D82 RID: 7554 RVA: 0x0009FFDE File Offset: 0x0009E1DE
	// (set) Token: 0x06001D83 RID: 7555 RVA: 0x0009FFE6 File Offset: 0x0009E1E6
	public EffectorValues noiseValues { get; set; }

	// Token: 0x06001D84 RID: 7556 RVA: 0x0009FFEF File Offset: 0x0009E1EF
	public SoundEvent()
	{
	}

	// Token: 0x06001D85 RID: 7557 RVA: 0x0009FFF8 File Offset: 0x0009E1F8
	public SoundEvent(string file_name, string sound_name, int frame, bool do_load, bool is_looping, float min_interval, bool is_dynamic)
		: base(file_name, sound_name, frame)
	{
		this.shouldCameraScalePosition = true;
		if (do_load)
		{
			this.sound = GlobalAssets.GetSound(sound_name, false);
			this.soundHash = new HashedString(this.sound);
			string.IsNullOrEmpty(this.sound);
		}
		this.minInterval = min_interval;
		this.looping = is_looping;
		this.isDynamic = is_dynamic;
		this.noiseValues = SoundEventVolumeCache.instance.GetVolume(file_name, sound_name);
	}

	// Token: 0x06001D86 RID: 7558 RVA: 0x000A006D File Offset: 0x0009E26D
	public static bool ObjectIsSelectedAndVisible(GameObject go)
	{
		return false;
	}

	// Token: 0x06001D87 RID: 7559 RVA: 0x000A0070 File Offset: 0x0009E270
	public static Vector3 AudioHighlightListenerPosition(Vector3 sound_pos)
	{
		Vector3 position = SoundListenerController.Instance.transform.position;
		float num = 1f * sound_pos.x + 0f * position.x;
		float num2 = 1f * sound_pos.y + 0f * position.y;
		float num3 = 0f * position.z;
		return new Vector3(num, num2, num3);
	}

	// Token: 0x06001D88 RID: 7560 RVA: 0x000A00D8 File Offset: 0x0009E2D8
	public static float GetVolume(bool objectIsSelectedAndVisible)
	{
		float num = 1f;
		if (objectIsSelectedAndVisible)
		{
			num = 1f;
		}
		return num;
	}

	// Token: 0x06001D89 RID: 7561 RVA: 0x000A00F5 File Offset: 0x0009E2F5
	public static bool ShouldPlaySound(KBatchedAnimController controller, string sound, bool is_looping, bool is_dynamic)
	{
		return SoundEvent.ShouldPlaySound(controller, sound, sound, is_looping, is_dynamic);
	}

	// Token: 0x06001D8A RID: 7562 RVA: 0x000A0108 File Offset: 0x0009E308
	public static bool ShouldPlaySound(KBatchedAnimController controller, string sound, HashedString soundHash, bool is_looping, bool is_dynamic)
	{
		CameraController instance = CameraController.Instance;
		if (instance == null)
		{
			return true;
		}
		Vector3 position = controller.transform.GetPosition();
		Vector3 offset = controller.Offset;
		position.x += offset.x;
		position.y += offset.y;
		if (!SoundCuller.IsAudibleWorld(position))
		{
			return false;
		}
		SpeedControlScreen instance2 = SpeedControlScreen.Instance;
		if (is_dynamic)
		{
			return (!(instance2 != null) || !instance2.IsPaused) && instance.IsAudibleSound(position);
		}
		if (sound == null || SoundEvent.IsLowPrioritySound(sound))
		{
			return false;
		}
		if (!instance.IsAudibleSound(position, soundHash))
		{
			if (!is_looping && !GlobalAssets.IsHighPriority(sound))
			{
				return false;
			}
		}
		else if (instance2 != null && instance2.IsPaused)
		{
			return false;
		}
		return true;
	}

	// Token: 0x06001D8B RID: 7563 RVA: 0x000A01D4 File Offset: 0x0009E3D4
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		GameObject gameObject = behaviour.controller.gameObject;
		this.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
		if (this.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, this.sound, this.soundHash, this.looping, this.isDynamic))
		{
			this.PlaySound(behaviour);
		}
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x000A0230 File Offset: 0x0009E430
	protected void PlaySound(AnimEventManager.EventPlayerData behaviour, string sound)
	{
		Vector3 vector = behaviour.controller.transform.GetPosition();
		vector.z = 0f;
		if (SoundEvent.ObjectIsSelectedAndVisible(behaviour.controller.gameObject))
		{
			vector = SoundEvent.AudioHighlightListenerPosition(vector);
		}
		KBatchedAnimController controller = behaviour.controller;
		if (controller != null)
		{
			Vector3 offset = controller.Offset;
			vector.x += offset.x;
			vector.y += offset.y;
		}
		AudioDebug audioDebug = AudioDebug.Get();
		if (audioDebug != null && audioDebug.debugSoundEvents)
		{
			string[] array = new string[7];
			array[0] = behaviour.name;
			array[1] = ", ";
			array[2] = sound;
			array[3] = ", ";
			array[4] = base.frame.ToString();
			array[5] = ", ";
			int num = 6;
			Vector3 vector2 = vector;
			array[num] = vector2.ToString();
			global::Debug.Log(string.Concat(array));
		}
		try
		{
			if (this.looping)
			{
				LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
				if (component == null)
				{
					global::Debug.Log(behaviour.name + " is missing LoopingSounds component. ");
				}
				else if (!component.StartSound(sound, behaviour, this.noiseValues, this.ignorePause, this.shouldCameraScalePosition))
				{
					DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", sound, behaviour.name) });
				}
			}
			else if (!SoundEvent.PlayOneShot(sound, behaviour, this.noiseValues, SoundEvent.GetVolume(this.objectIsSelectedAndVisible), this.objectIsSelectedAndVisible))
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", sound, behaviour.name) });
			}
		}
		catch (Exception ex)
		{
			string text = string.Format(("Error trying to trigger sound [{0}] in behaviour [{1}] [{2}]\n{3}" + sound != null) ? sound.ToString() : "null", behaviour.GetType().ToString(), ex.Message, ex.StackTrace);
			global::Debug.LogError(text);
			throw new ArgumentException(text, ex);
		}
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x000A0430 File Offset: 0x0009E630
	public virtual void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		this.PlaySound(behaviour, this.sound);
	}

	// Token: 0x06001D8E RID: 7566 RVA: 0x000A0440 File Offset: 0x0009E640
	public static Vector3 GetCameraScaledPosition(Vector3 pos, bool objectIsSelectedAndVisible = false)
	{
		Vector3 vector = Vector3.zero;
		if (CameraController.Instance != null)
		{
			vector = CameraController.Instance.GetVerticallyScaledPosition(pos, objectIsSelectedAndVisible);
		}
		return vector;
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x000A046E File Offset: 0x0009E66E
	public static FMOD.Studio.EventInstance BeginOneShot(EventReference event_ref, Vector3 pos, float volume = 1f, bool objectIsSelectedAndVisible = false)
	{
		return KFMOD.BeginOneShot(event_ref, SoundEvent.GetCameraScaledPosition(pos, objectIsSelectedAndVisible), volume);
	}

	// Token: 0x06001D90 RID: 7568 RVA: 0x000A047E File Offset: 0x0009E67E
	public static FMOD.Studio.EventInstance BeginOneShot(string ev, Vector3 pos, float volume = 1f, bool objectIsSelectedAndVisible = false)
	{
		return SoundEvent.BeginOneShot(RuntimeManager.PathToEventReference(ev), pos, volume, false);
	}

	// Token: 0x06001D91 RID: 7569 RVA: 0x000A048E File Offset: 0x0009E68E
	public static bool EndOneShot(FMOD.Studio.EventInstance instance)
	{
		return KFMOD.EndOneShot(instance);
	}

	// Token: 0x06001D92 RID: 7570 RVA: 0x000A0498 File Offset: 0x0009E698
	public static bool PlayOneShot(EventReference event_ref, Vector3 sound_pos, float volume = 1f)
	{
		bool flag = false;
		if (!event_ref.IsNull)
		{
			FMOD.Studio.EventInstance eventInstance = SoundEvent.BeginOneShot(event_ref, sound_pos, volume, false);
			if (eventInstance.isValid())
			{
				flag = SoundEvent.EndOneShot(eventInstance);
			}
		}
		return flag;
	}

	// Token: 0x06001D93 RID: 7571 RVA: 0x000A04CB File Offset: 0x0009E6CB
	public static bool PlayOneShot(string sound, Vector3 sound_pos, float volume = 1f)
	{
		return SoundEvent.PlayOneShot(RuntimeManager.PathToEventReference(sound), sound_pos, volume);
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x000A04DC File Offset: 0x0009E6DC
	public static bool PlayOneShot(string sound, AnimEventManager.EventPlayerData behaviour, EffectorValues noiseValues, float volume = 1f, bool objectIsSelectedAndVisible = false)
	{
		bool flag = false;
		if (!string.IsNullOrEmpty(sound))
		{
			Vector3 vector = behaviour.controller.transform.GetPosition();
			vector.z = 0f;
			if (objectIsSelectedAndVisible)
			{
				vector = SoundEvent.AudioHighlightListenerPosition(vector);
			}
			FMOD.Studio.EventInstance eventInstance = SoundEvent.BeginOneShot(sound, vector, volume, false);
			if (eventInstance.isValid())
			{
				flag = SoundEvent.EndOneShot(eventInstance);
			}
		}
		return flag;
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x000A0538 File Offset: 0x0009E738
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (this.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null)
			{
				component.StopSound(this.sound);
			}
		}
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x000A056A File Offset: 0x0009E76A
	protected static bool IsLowPrioritySound(string sound)
	{
		return sound != null && Camera.main != null && Camera.main.orthographicSize > AudioMixer.LOW_PRIORITY_CUTOFF_DISTANCE && !AudioMixer.instance.activeNIS && GlobalAssets.IsLowPriority(sound);
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x000A05A4 File Offset: 0x0009E7A4
	protected void PrintSoundDebug(string anim_name, string sound, string sound_name, Vector3 sound_pos)
	{
		if (sound != null)
		{
			string[] array = new string[7];
			array[0] = anim_name;
			array[1] = ", ";
			array[2] = sound_name;
			array[3] = ", ";
			array[4] = base.frame.ToString();
			array[5] = ", ";
			int num = 6;
			Vector3 vector = sound_pos;
			array[num] = vector.ToString();
			global::Debug.Log(string.Concat(array));
			return;
		}
		global::Debug.Log("Missing sound: " + anim_name + ", " + sound_name);
	}

	// Token: 0x04001137 RID: 4407
	public static int IGNORE_INTERVAL = -1;

	// Token: 0x04001140 RID: 4416
	protected bool isDynamic;
}
