using System;
using System.Collections.Generic;

// Token: 0x0200053C RID: 1340
internal class ObjectCountOneShotUpdater : OneShotSoundParameterUpdater
{
	// Token: 0x06001DA1 RID: 7585 RVA: 0x000A0943 File Offset: 0x0009EB43
	public ObjectCountOneShotUpdater()
		: base("objectCount")
	{
	}

	// Token: 0x06001DA2 RID: 7586 RVA: 0x000A0960 File Offset: 0x0009EB60
	public override void Update(float dt)
	{
		this.soundCounts.Clear();
	}

	// Token: 0x06001DA3 RID: 7587 RVA: 0x000A0970 File Offset: 0x0009EB70
	public override void Play(OneShotSoundParameterUpdater.Sound sound)
	{
		UpdateObjectCountParameter.Settings settings = UpdateObjectCountParameter.GetSettings(sound.path, sound.description);
		int num = 0;
		this.soundCounts.TryGetValue(sound.path, out num);
		num = (this.soundCounts[sound.path] = num + 1);
		UpdateObjectCountParameter.ApplySettings(sound.ev, num, settings);
	}

	// Token: 0x04001144 RID: 4420
	private Dictionary<HashedString, int> soundCounts = new Dictionary<HashedString, int>();
}
