using System;
using UnityEngine;

// Token: 0x0200051E RID: 1310
[Serializable]
public class BuildingDamageSoundEvent : SoundEvent
{
	// Token: 0x06001C24 RID: 7204 RVA: 0x00098DB3 File Offset: 0x00096FB3
	public BuildingDamageSoundEvent(string file_name, string sound_name, int frame)
		: base(file_name, sound_name, frame, false, false, (float)SoundEvent.IGNORE_INTERVAL, false)
	{
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x00098DC8 File Offset: 0x00096FC8
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		Vector3 vector = behaviour.position;
		vector.z = 0f;
		GameObject gameObject = behaviour.controller.gameObject;
		base.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
		if (base.objectIsSelectedAndVisible)
		{
			vector = SoundEvent.AudioHighlightListenerPosition(vector);
		}
		WorkerBase component = behaviour.GetComponent<WorkerBase>();
		if (component == null)
		{
			string sound = GlobalAssets.GetSound("Building_Dmg_Metal", false);
			if (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, sound, base.looping, this.isDynamic))
			{
				SoundEvent.PlayOneShot(base.sound, vector, SoundEvent.GetVolume(base.objectIsSelectedAndVisible));
				return;
			}
		}
		Workable workable = component.GetWorkable();
		if (workable != null)
		{
			Building component2 = workable.GetComponent<Building>();
			if (component2 != null)
			{
				BuildingDef def = component2.Def;
				string text = GlobalAssets.GetSound(StringFormatter.Combine(base.name, "_", def.AudioCategory), false);
				if (text == null)
				{
					text = GlobalAssets.GetSound("Building_Dmg_Metal", false);
				}
				if (text != null && (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, text, base.looping, this.isDynamic)))
				{
					SoundEvent.PlayOneShot(text, vector, SoundEvent.GetVolume(base.objectIsSelectedAndVisible));
				}
			}
		}
	}
}
