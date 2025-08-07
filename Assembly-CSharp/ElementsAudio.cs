using System;

// Token: 0x02000553 RID: 1363
public class ElementsAudio
{
	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06001E13 RID: 7699 RVA: 0x000A37C6 File Offset: 0x000A19C6
	public static ElementsAudio Instance
	{
		get
		{
			if (ElementsAudio._instance == null)
			{
				ElementsAudio._instance = new ElementsAudio();
			}
			return ElementsAudio._instance;
		}
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x000A37DE File Offset: 0x000A19DE
	public void LoadData(ElementsAudio.ElementAudioConfig[] elements_audio_configs)
	{
		this.elementAudioConfigs = elements_audio_configs;
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x000A37E8 File Offset: 0x000A19E8
	public ElementsAudio.ElementAudioConfig GetConfigForElement(SimHashes id)
	{
		if (this.elementAudioConfigs != null)
		{
			for (int i = 0; i < this.elementAudioConfigs.Length; i++)
			{
				if (this.elementAudioConfigs[i].elementID == id)
				{
					return this.elementAudioConfigs[i];
				}
			}
		}
		return null;
	}

	// Token: 0x04001185 RID: 4485
	private static ElementsAudio _instance;

	// Token: 0x04001186 RID: 4486
	private ElementsAudio.ElementAudioConfig[] elementAudioConfigs;

	// Token: 0x0200139E RID: 5022
	public class ElementAudioConfig : Resource
	{
		// Token: 0x04006A19 RID: 27161
		public SimHashes elementID;

		// Token: 0x04006A1A RID: 27162
		public AmbienceType ambienceType = AmbienceType.None;

		// Token: 0x04006A1B RID: 27163
		public SolidAmbienceType solidAmbienceType = SolidAmbienceType.None;

		// Token: 0x04006A1C RID: 27164
		public string miningSound = "";

		// Token: 0x04006A1D RID: 27165
		public string miningBreakSound = "";

		// Token: 0x04006A1E RID: 27166
		public string oreBumpSound = "";

		// Token: 0x04006A1F RID: 27167
		public string floorEventAudioCategory = "";

		// Token: 0x04006A20 RID: 27168
		public string creatureChewSound = "";
	}
}
