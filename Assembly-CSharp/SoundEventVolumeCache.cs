using System;
using System.Collections.Generic;

// Token: 0x02000539 RID: 1337
public class SoundEventVolumeCache : Singleton<SoundEventVolumeCache>
{
	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0009FEBD File Offset: 0x0009E0BD
	public static SoundEventVolumeCache instance
	{
		get
		{
			return Singleton<SoundEventVolumeCache>.Instance;
		}
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x0009FEC4 File Offset: 0x0009E0C4
	public void AddVolume(string animFile, string eventName, EffectorValues vals)
	{
		HashedString hashedString = new HashedString(animFile + ":" + eventName);
		if (!this.volumeCache.ContainsKey(hashedString))
		{
			this.volumeCache.Add(hashedString, vals);
			return;
		}
		this.volumeCache[hashedString] = vals;
	}

	// Token: 0x06001D72 RID: 7538 RVA: 0x0009FF10 File Offset: 0x0009E110
	public EffectorValues GetVolume(string animFile, string eventName)
	{
		HashedString hashedString = new HashedString(animFile + ":" + eventName);
		if (!this.volumeCache.ContainsKey(hashedString))
		{
			return default(EffectorValues);
		}
		return this.volumeCache[hashedString];
	}

	// Token: 0x04001136 RID: 4406
	public Dictionary<HashedString, EffectorValues> volumeCache = new Dictionary<HashedString, EffectorValues>();
}
