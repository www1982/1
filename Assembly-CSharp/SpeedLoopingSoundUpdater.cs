using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000E56 RID: 3670
public class SpeedLoopingSoundUpdater : LoopingSoundParameterUpdater
{
	// Token: 0x060074E1 RID: 29921 RVA: 0x002C9EFD File Offset: 0x002C80FD
	public SpeedLoopingSoundUpdater()
		: base("Speed")
	{
	}

	// Token: 0x060074E2 RID: 29922 RVA: 0x002C9F1C File Offset: 0x002C811C
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		SpeedLoopingSoundUpdater.Entry entry = new SpeedLoopingSoundUpdater.Entry
		{
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x060074E3 RID: 29923 RVA: 0x002C9F68 File Offset: 0x002C8168
	public override void Update(float dt)
	{
		float speedParameterValue = SpeedLoopingSoundUpdater.GetSpeedParameterValue();
		foreach (SpeedLoopingSoundUpdater.Entry entry in this.entries)
		{
			EventInstance ev = entry.ev;
			ev.setParameterByID(entry.parameterId, speedParameterValue, false);
		}
	}

	// Token: 0x060074E4 RID: 29924 RVA: 0x002C9FD4 File Offset: 0x002C81D4
	public override void Remove(LoopingSoundParameterUpdater.Sound sound)
	{
		for (int i = 0; i < this.entries.Count; i++)
		{
			if (this.entries[i].ev.handle == sound.ev.handle)
			{
				this.entries.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x060074E5 RID: 29925 RVA: 0x002CA02C File Offset: 0x002C822C
	public static float GetSpeedParameterValue()
	{
		return Time.timeScale * 1f;
	}

	// Token: 0x040050D8 RID: 20696
	private List<SpeedLoopingSoundUpdater.Entry> entries = new List<SpeedLoopingSoundUpdater.Entry>();

	// Token: 0x02002052 RID: 8274
	private struct Entry
	{
		// Token: 0x040093B3 RID: 37811
		public EventInstance ev;

		// Token: 0x040093B4 RID: 37812
		public PARAMETER_ID parameterId;
	}
}
