using System;
using UnityEngine;

// Token: 0x02000530 RID: 1328
public class LadderSoundEvent : SoundEvent
{
	// Token: 0x06001D5C RID: 7516 RVA: 0x0009F4E2 File Offset: 0x0009D6E2
	public LadderSoundEvent(string file_name, string sound_name, int frame)
		: base(file_name, sound_name, frame, false, false, (float)SoundEvent.IGNORE_INTERVAL, true)
	{
	}

	// Token: 0x06001D5D RID: 7517 RVA: 0x0009F4F8 File Offset: 0x0009D6F8
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		GameObject gameObject = behaviour.controller.gameObject;
		base.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
		if (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, base.sound, base.looping, this.isDynamic))
		{
			Vector3 vector = behaviour.position;
			vector.z = 0f;
			float num = 1f;
			if (base.objectIsSelectedAndVisible)
			{
				vector = SoundEvent.AudioHighlightListenerPosition(vector);
				num = SoundEvent.GetVolume(base.objectIsSelectedAndVisible);
			}
			int num2 = Grid.PosToCell(vector);
			BuildingDef buildingDef = null;
			if (Grid.IsValidCell(num2))
			{
				GameObject gameObject2 = Grid.Objects[num2, 1];
				if (gameObject2 != null && gameObject2.GetComponent<Ladder>() != null)
				{
					Building component = gameObject2.GetComponent<BuildingComplete>();
					if (component != null)
					{
						buildingDef = component.Def;
					}
				}
			}
			if (buildingDef != null)
			{
				string sound = GlobalAssets.GetSound((buildingDef.PrefabID == "LadderFast") ? StringFormatter.Combine(base.name, "_Plastic") : base.name, false);
				if (sound != null)
				{
					SoundEvent.PlayOneShot(sound, vector, num);
				}
			}
		}
	}
}
