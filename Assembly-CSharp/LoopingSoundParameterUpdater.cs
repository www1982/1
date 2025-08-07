using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x020005D6 RID: 1494
public abstract class LoopingSoundParameterUpdater
{
	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06002291 RID: 8849 RVA: 0x000C6522 File Offset: 0x000C4722
	// (set) Token: 0x06002292 RID: 8850 RVA: 0x000C652A File Offset: 0x000C472A
	public HashedString parameter { get; private set; }

	// Token: 0x06002293 RID: 8851 RVA: 0x000C6533 File Offset: 0x000C4733
	public LoopingSoundParameterUpdater(HashedString parameter)
	{
		this.parameter = parameter;
	}

	// Token: 0x06002294 RID: 8852
	public abstract void Add(LoopingSoundParameterUpdater.Sound sound);

	// Token: 0x06002295 RID: 8853
	public abstract void Update(float dt);

	// Token: 0x06002296 RID: 8854
	public abstract void Remove(LoopingSoundParameterUpdater.Sound sound);

	// Token: 0x02001469 RID: 5225
	public struct Sound
	{
		// Token: 0x04006C82 RID: 27778
		public EventInstance ev;

		// Token: 0x04006C83 RID: 27779
		public HashedString path;

		// Token: 0x04006C84 RID: 27780
		public Transform transform;

		// Token: 0x04006C85 RID: 27781
		public SoundDescription description;

		// Token: 0x04006C86 RID: 27782
		public bool objectIsSelectedAndVisible;
	}
}
