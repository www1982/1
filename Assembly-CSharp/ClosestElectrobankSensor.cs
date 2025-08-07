using System;
using System.Collections.Generic;

// Token: 0x020004F9 RID: 1273
public class ClosestElectrobankSensor : ClosestPickupableSensor<Electrobank>
{
	// Token: 0x06001B49 RID: 6985 RVA: 0x00095FA6 File Offset: 0x000941A6
	public ClosestElectrobankSensor(Sensors sensors, bool shouldStartActive)
		: base(sensors, GameTags.ChargedPortableBattery, shouldStartActive)
	{
		this.bionicIncompatiobleElectrobankTags = new Tag[GameTags.BionicIncompatibleBatteries.Count];
		GameTags.BionicIncompatibleBatteries.CopyTo(this.bionicIncompatiobleElectrobankTags, 0);
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x00095FDC File Offset: 0x000941DC
	public override HashSet<Tag> GetForbbidenTags()
	{
		HashSet<Tag> forbbidenTags = base.GetForbbidenTags();
		if (this.bionicIncompatiobleElectrobankTags != null && this.bionicIncompatiobleElectrobankTags.Length != 0)
		{
			HashSet<Tag> hashSet = forbbidenTags;
			foreach (Tag tag in this.bionicIncompatiobleElectrobankTags)
			{
				if (!forbbidenTags.Contains(tag))
				{
					hashSet.Add(tag);
				}
			}
			return hashSet;
		}
		return forbbidenTags;
	}

	// Token: 0x04001011 RID: 4113
	private Tag[] bionicIncompatiobleElectrobankTags;
}
