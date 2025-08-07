using System;
using System.Collections.Generic;

// Token: 0x020004FB RID: 1275
public class ClosestOxygenCanisterSensor : ClosestPickupableSensor<Pickupable>
{
	// Token: 0x06001B4C RID: 6988 RVA: 0x00096048 File Offset: 0x00094248
	public ClosestOxygenCanisterSensor(Sensors sensors, bool shouldStartActive)
		: base(sensors, GameTags.Gas, shouldStartActive)
	{
		this.requiredTags = new Tag[] { GameTags.Breathable };
		this.BreathableGasses = ElementLoader.FindElements((Element element) => element.HasTag(GameTags.Breathable) && element.HasTag(GameTags.Gas));
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x000960A4 File Offset: 0x000942A4
	public override HashSet<Tag> GetForbbidenTags()
	{
		if (this.consumableConsumer == null)
		{
			return new HashSet<Tag>(0);
		}
		HashSet<Tag> forbbidenTags = base.GetForbbidenTags();
		if (forbbidenTags == null || forbbidenTags.Count <= 0)
		{
			return forbbidenTags;
		}
		Tag[] array = new Tag[forbbidenTags.Count];
		base.GetForbbidenTags().CopyTo(array);
		HashSet<Tag> hashSet = new HashSet<Tag>();
		int i = 0;
		while (i < array.Length)
		{
			Tag tag = array[i];
			if (tag == ClosestOxygenCanisterSensor.GenericBreathableGassesTankTag)
			{
				using (List<Element>.Enumerator enumerator = this.BreathableGasses.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Element element = enumerator.Current;
						hashSet.Add(element.id.ToString());
					}
					goto IL_00BB;
				}
				goto IL_00B2;
			}
			goto IL_00B2;
			IL_00BB:
			i++;
			continue;
			IL_00B2:
			hashSet.Add(tag);
			goto IL_00BB;
		}
		return hashSet;
	}

	// Token: 0x04001012 RID: 4114
	public static readonly Tag GenericBreathableGassesTankTag = new Tag("BreathableGasTank");

	// Token: 0x04001013 RID: 4115
	private List<Element> BreathableGasses;
}
