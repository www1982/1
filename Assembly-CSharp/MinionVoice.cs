using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000E RID: 14
public readonly struct MinionVoice
{
	// Token: 0x06000038 RID: 56 RVA: 0x0000393C File Offset: 0x00001B3C
	public MinionVoice(int voiceIndex)
	{
		this.voiceIndex = voiceIndex;
		this.voiceId = (voiceIndex + 1).ToString("D2");
		this.isValid = true;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000396D File Offset: 0x00001B6D
	public static MinionVoice ByPersonality(Personality personality)
	{
		return MinionVoice.ByPersonality(personality.Id);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000397C File Offset: 0x00001B7C
	public static MinionVoice ByPersonality(string personalityId)
	{
		if (personalityId == "JORGE")
		{
			return new MinionVoice(-2);
		}
		if (personalityId == "MEEP")
		{
			return new MinionVoice(2);
		}
		MinionVoice minionVoice;
		if (!MinionVoice.personalityVoiceMap.TryGetValue(personalityId, out minionVoice))
		{
			minionVoice = MinionVoice.Random();
			MinionVoice.personalityVoiceMap.Add(personalityId, minionVoice);
		}
		return minionVoice;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000039D4 File Offset: 0x00001BD4
	public static MinionVoice Random()
	{
		return new MinionVoice(global::UnityEngine.Random.Range(0, 4));
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000039E4 File Offset: 0x00001BE4
	public static Option<MinionVoice> ByObject(global::UnityEngine.Object unityObject)
	{
		GameObject gameObject = unityObject as GameObject;
		GameObject gameObject2;
		if (gameObject != null)
		{
			gameObject2 = gameObject;
		}
		else
		{
			Component component = unityObject as Component;
			if (component != null)
			{
				gameObject2 = component.gameObject;
			}
			else
			{
				gameObject2 = null;
			}
		}
		if (gameObject2.IsNullOrDestroyed())
		{
			return Option.None;
		}
		MinionVoiceProviderMB componentInParent = gameObject2.GetComponentInParent<MinionVoiceProviderMB>();
		if (componentInParent.IsNullOrDestroyed())
		{
			return Option.None;
		}
		return componentInParent.voice;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003A48 File Offset: 0x00001C48
	public string GetSoundAssetName(string localName)
	{
		global::Debug.Assert(this.isValid);
		string text = localName;
		if (localName.Contains(":"))
		{
			text = localName.Split(':', StringSplitOptions.None)[0];
		}
		return StringFormatter.Combine("DupVoc_", this.voiceId, "_", text);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003A91 File Offset: 0x00001C91
	public string GetSoundPath(string localName)
	{
		return GlobalAssets.GetSound(this.GetSoundAssetName(localName), true);
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003AA0 File Offset: 0x00001CA0
	public void PlaySoundUI(string localName)
	{
		global::Debug.Assert(this.isValid);
		string soundPath = this.GetSoundPath(localName);
		try
		{
			if (SoundListenerController.Instance == null)
			{
				KFMOD.PlayUISound(soundPath);
			}
			else
			{
				KFMOD.PlayOneShot(soundPath, SoundListenerController.Instance.transform.GetPosition(), 1f);
			}
		}
		catch
		{
			DebugUtil.LogWarningArgs(new object[] { "AUDIOERROR: Missing [" + soundPath + "]" });
		}
	}

	// Token: 0x0400003A RID: 58
	public readonly int voiceIndex;

	// Token: 0x0400003B RID: 59
	public readonly string voiceId;

	// Token: 0x0400003C RID: 60
	public readonly bool isValid;

	// Token: 0x0400003D RID: 61
	private static Dictionary<string, MinionVoice> personalityVoiceMap = new Dictionary<string, MinionVoice>();
}
