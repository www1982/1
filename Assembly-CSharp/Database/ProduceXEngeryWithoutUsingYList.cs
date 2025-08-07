using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000F38 RID: 3896
	public class ProduceXEngeryWithoutUsingYList : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AB1 RID: 31409 RVA: 0x003091F8 File Offset: 0x003073F8
		public ProduceXEngeryWithoutUsingYList(float amountToProduce, List<Tag> disallowedBuildings)
		{
			this.disallowedBuildings = disallowedBuildings;
			this.amountToProduce = amountToProduce;
			this.usedDisallowedBuilding = false;
		}

		// Token: 0x06007AB2 RID: 31410 RVA: 0x00309220 File Offset: 0x00307420
		public override bool Success()
		{
			float num = 0f;
			foreach (KeyValuePair<Tag, float> keyValuePair in Game.Instance.savedInfo.powerCreatedbyGeneratorType)
			{
				if (!this.disallowedBuildings.Contains(keyValuePair.Key))
				{
					num += keyValuePair.Value;
				}
			}
			return num / 1000f > this.amountToProduce;
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x003092A8 File Offset: 0x003074A8
		public override bool Fail()
		{
			foreach (Tag tag in this.disallowedBuildings)
			{
				if (Game.Instance.savedInfo.powerCreatedbyGeneratorType.ContainsKey(tag))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007AB4 RID: 31412 RVA: 0x00309314 File Offset: 0x00307514
		public void Deserialize(IReader reader)
		{
			int num = reader.ReadInt32();
			this.disallowedBuildings = new List<Tag>(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.disallowedBuildings.Add(new Tag(text));
			}
			this.amountProduced = (float)reader.ReadDouble();
			this.amountToProduce = (float)reader.ReadDouble();
			this.usedDisallowedBuilding = reader.ReadByte() > 0;
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x00309384 File Offset: 0x00307584
		public float GetProductionAmount(bool complete)
		{
			if (complete)
			{
				return this.amountToProduce * 1000f;
			}
			float num = 0f;
			foreach (KeyValuePair<Tag, float> keyValuePair in Game.Instance.savedInfo.powerCreatedbyGeneratorType)
			{
				if (!this.disallowedBuildings.Contains(keyValuePair.Key))
				{
					num += keyValuePair.Value;
				}
			}
			return num;
		}

		// Token: 0x040059BA RID: 22970
		public List<Tag> disallowedBuildings = new List<Tag>();

		// Token: 0x040059BB RID: 22971
		public float amountToProduce;

		// Token: 0x040059BC RID: 22972
		private float amountProduced;

		// Token: 0x040059BD RID: 22973
		private bool usedDisallowedBuilding;
	}
}
