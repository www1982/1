using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F36 RID: 3894
	public class EquipNDupes : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AA9 RID: 31401 RVA: 0x00308E39 File Offset: 0x00307039
		public EquipNDupes(AssignableSlot equipmentSlot, int numToEquip)
		{
			this.equipmentSlot = equipmentSlot;
			this.numToEquip = numToEquip;
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x00308E50 File Offset: 0x00307050
		public override bool Success()
		{
			int num = 0;
			foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
			{
				Equipment equipment = minionIdentity.GetEquipment();
				if (equipment != null && equipment.IsSlotOccupied(this.equipmentSlot))
				{
					num++;
				}
			}
			return num >= this.numToEquip;
		}

		// Token: 0x06007AAB RID: 31403 RVA: 0x00308ED0 File Offset: 0x003070D0
		public void Deserialize(IReader reader)
		{
			string text = reader.ReadKleiString();
			this.equipmentSlot = Db.Get().AssignableSlots.Get(text);
			this.numToEquip = reader.ReadInt32();
		}

		// Token: 0x06007AAC RID: 31404 RVA: 0x00308F08 File Offset: 0x00307108
		public override string GetProgress(bool complete)
		{
			int num = 0;
			foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
			{
				Equipment equipment = minionIdentity.GetEquipment();
				if (equipment != null && equipment.IsSlotOccupied(this.equipmentSlot))
				{
					num++;
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CLOTHE_DUPES, complete ? this.numToEquip : num, this.numToEquip);
		}

		// Token: 0x040059B4 RID: 22964
		private AssignableSlot equipmentSlot;

		// Token: 0x040059B5 RID: 22965
		private int numToEquip;
	}
}
