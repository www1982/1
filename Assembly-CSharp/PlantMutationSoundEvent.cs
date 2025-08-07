using System;
using UnityEngine;

// Token: 0x02000536 RID: 1334
public class PlantMutationSoundEvent : SoundEvent
{
	// Token: 0x06001D68 RID: 7528 RVA: 0x0009F9F8 File Offset: 0x0009DBF8
	public PlantMutationSoundEvent(string file_name, string sound_name, int frame, float min_interval)
		: base(file_name, sound_name, frame, false, false, min_interval, true)
	{
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x0009FA08 File Offset: 0x0009DC08
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		MutantPlant component = behaviour.controller.gameObject.GetComponent<MutantPlant>();
		Vector3 position = behaviour.position;
		if (component != null)
		{
			for (int i = 0; i < component.GetSoundEvents().Count; i++)
			{
				SoundEvent.PlayOneShot(component.GetSoundEvents()[i], position, 1f);
			}
		}
	}
}
