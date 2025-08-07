using System;
using UnityEngine;

// Token: 0x02000543 RID: 1347
[AddComponentMenu("KMonoBehaviour/scripts/AudioDebug")]
public class AudioDebug : KMonoBehaviour
{
	// Token: 0x06001DCD RID: 7629 RVA: 0x000A1BBC File Offset: 0x0009FDBC
	public static AudioDebug Get()
	{
		return AudioDebug.instance;
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x000A1BC3 File Offset: 0x0009FDC3
	protected override void OnPrefabInit()
	{
		AudioDebug.instance = this;
	}

	// Token: 0x06001DCF RID: 7631 RVA: 0x000A1BCB File Offset: 0x0009FDCB
	public void ToggleMusic()
	{
		if (Game.Instance != null)
		{
			Game.Instance.SetMusicEnabled(this.musicEnabled);
		}
		this.musicEnabled = !this.musicEnabled;
	}

	// Token: 0x04001156 RID: 4438
	private static AudioDebug instance;

	// Token: 0x04001157 RID: 4439
	public bool musicEnabled;

	// Token: 0x04001158 RID: 4440
	public bool debugSoundEvents;

	// Token: 0x04001159 RID: 4441
	public bool debugFloorSounds;

	// Token: 0x0400115A RID: 4442
	public bool debugGameEventSounds;

	// Token: 0x0400115B RID: 4443
	public bool debugNotificationSounds;

	// Token: 0x0400115C RID: 4444
	public bool debugVoiceSounds;
}
