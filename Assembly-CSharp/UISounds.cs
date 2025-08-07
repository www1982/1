using System;
using UnityEngine;

// Token: 0x02000BBF RID: 3007
[AddComponentMenu("KMonoBehaviour/scripts/UISounds")]
public class UISounds : KMonoBehaviour
{
	// Token: 0x1700068A RID: 1674
	// (get) Token: 0x06005A02 RID: 23042 RVA: 0x00208173 File Offset: 0x00206373
	// (set) Token: 0x06005A03 RID: 23043 RVA: 0x0020817A File Offset: 0x0020637A
	public static UISounds Instance { get; private set; }

	// Token: 0x06005A04 RID: 23044 RVA: 0x00208182 File Offset: 0x00206382
	public static void DestroyInstance()
	{
		UISounds.Instance = null;
	}

	// Token: 0x06005A05 RID: 23045 RVA: 0x0020818A File Offset: 0x0020638A
	protected override void OnPrefabInit()
	{
		UISounds.Instance = this;
	}

	// Token: 0x06005A06 RID: 23046 RVA: 0x00208192 File Offset: 0x00206392
	public static void PlaySound(UISounds.Sound sound)
	{
		UISounds.Instance.PlaySoundInternal(sound);
	}

	// Token: 0x06005A07 RID: 23047 RVA: 0x002081A0 File Offset: 0x002063A0
	private void PlaySoundInternal(UISounds.Sound sound)
	{
		for (int i = 0; i < this.soundData.Length; i++)
		{
			if (this.soundData[i].sound == sound)
			{
				if (this.logSounds)
				{
					DebugUtil.LogArgs(new object[]
					{
						"Play sound",
						this.soundData[i].name
					});
				}
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound(this.soundData[i].name, false));
			}
		}
	}

	// Token: 0x04003BC5 RID: 15301
	[SerializeField]
	private bool logSounds;

	// Token: 0x04003BC6 RID: 15302
	[SerializeField]
	private UISounds.SoundData[] soundData;

	// Token: 0x02001CF0 RID: 7408
	public enum Sound
	{
		// Token: 0x040087B5 RID: 34741
		NegativeNotification,
		// Token: 0x040087B6 RID: 34742
		PositiveNotification,
		// Token: 0x040087B7 RID: 34743
		Select,
		// Token: 0x040087B8 RID: 34744
		Negative,
		// Token: 0x040087B9 RID: 34745
		Back,
		// Token: 0x040087BA RID: 34746
		ClickObject,
		// Token: 0x040087BB RID: 34747
		HUD_Mouseover,
		// Token: 0x040087BC RID: 34748
		Object_Mouseover,
		// Token: 0x040087BD RID: 34749
		ClickHUD,
		// Token: 0x040087BE RID: 34750
		Object_AutoSelected
	}

	// Token: 0x02001CF1 RID: 7409
	[Serializable]
	private struct SoundData
	{
		// Token: 0x040087BF RID: 34751
		public string name;

		// Token: 0x040087C0 RID: 34752
		public UISounds.Sound sound;
	}
}
