using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x0200051F RID: 1311
public class ClusterMapSoundEvent : SoundEvent
{
	// Token: 0x06001C26 RID: 7206 RVA: 0x00098F03 File Offset: 0x00097103
	public ClusterMapSoundEvent(string file_name, string sound_name, int frame, bool looping)
		: base(file_name, sound_name, frame, true, looping, (float)SoundEvent.IGNORE_INTERVAL, false)
	{
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x00098F18 File Offset: 0x00097118
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		if (ClusterMapScreen.Instance != null && ClusterMapScreen.Instance.IsActive())
		{
			this.PlaySound(behaviour);
		}
	}

	// Token: 0x06001C28 RID: 7208 RVA: 0x00098F3C File Offset: 0x0009713C
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component == null)
			{
				global::Debug.Log(behaviour.name + " (Cluster Map Object) is missing LoopingSounds component.");
				return;
			}
			if (!component.StartSound(base.sound, true, false, false))
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", base.sound, behaviour.name) });
				return;
			}
		}
		else
		{
			EventInstance eventInstance = KFMOD.BeginOneShot(base.sound, Vector3.zero, 1f);
			eventInstance.setParameterByName(ClusterMapSoundEvent.X_POSITION_PARAMETER, behaviour.controller.transform.GetPosition().x / (float)Screen.width, false);
			eventInstance.setParameterByName(ClusterMapSoundEvent.Y_POSITION_PARAMETER, behaviour.controller.transform.GetPosition().y / (float)Screen.height, false);
			eventInstance.setParameterByName(ClusterMapSoundEvent.ZOOM_PARAMETER, ClusterMapScreen.Instance.CurrentZoomPercentage(), false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x0009903C File Offset: 0x0009723C
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null)
			{
				component.StopSound(base.sound);
			}
		}
	}

	// Token: 0x04001084 RID: 4228
	private static string X_POSITION_PARAMETER = "Starmap_Position_X";

	// Token: 0x04001085 RID: 4229
	private static string Y_POSITION_PARAMETER = "Starmap_Position_Y";

	// Token: 0x04001086 RID: 4230
	private static string ZOOM_PARAMETER = "Starmap_Zoom_Percentage";
}
