using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using TUNING;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FD8 RID: 4056
	[SerializationConfig(MemberSerialization.OptIn)]
	[AddComponentMenu("KMonoBehaviour/scripts/AttributeLevels")]
	public class AttributeLevels : KMonoBehaviour, ISaveLoadable
	{
		// Token: 0x06007D5A RID: 32090 RVA: 0x003222ED File Offset: 0x003204ED
		public IEnumerator<AttributeLevel> GetEnumerator()
		{
			return this.levels.GetEnumerator();
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06007D5B RID: 32091 RVA: 0x003222FF File Offset: 0x003204FF
		// (set) Token: 0x06007D5C RID: 32092 RVA: 0x00322307 File Offset: 0x00320507
		public AttributeLevels.LevelSaveLoad[] SaveLoadLevels
		{
			get
			{
				return this.saveLoadLevels;
			}
			set
			{
				this.saveLoadLevels = value;
			}
		}

		// Token: 0x06007D5D RID: 32093 RVA: 0x00322310 File Offset: 0x00320510
		protected override void OnPrefabInit()
		{
			foreach (AttributeInstance attributeInstance in this.GetAttributes())
			{
				if (attributeInstance.Attribute.IsTrainable)
				{
					AttributeLevel attributeLevel = new AttributeLevel(attributeInstance);
					this.levels.Add(attributeLevel);
					attributeLevel.maxGainedLevel = this.maxAttributeLevel;
					attributeLevel.Apply(this);
				}
			}
		}

		// Token: 0x06007D5E RID: 32094 RVA: 0x0032238C File Offset: 0x0032058C
		[OnSerializing]
		public void OnSerializing()
		{
			this.saveLoadLevels = new AttributeLevels.LevelSaveLoad[this.levels.Count];
			for (int i = 0; i < this.levels.Count; i++)
			{
				this.saveLoadLevels[i].attributeId = this.levels[i].attribute.Attribute.Id;
				this.saveLoadLevels[i].experience = this.levels[i].experience;
				this.saveLoadLevels[i].level = this.levels[i].level;
			}
		}

		// Token: 0x06007D5F RID: 32095 RVA: 0x00322438 File Offset: 0x00320638
		[OnDeserialized]
		public void OnDeserialized()
		{
			foreach (AttributeLevels.LevelSaveLoad levelSaveLoad in this.saveLoadLevels)
			{
				this.SetExperience(levelSaveLoad.attributeId, levelSaveLoad.experience);
				this.SetLevel(levelSaveLoad.attributeId, levelSaveLoad.level);
			}
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x00322488 File Offset: 0x00320688
		public int GetLevel(Attribute attribute)
		{
			foreach (AttributeLevel attributeLevel in this.levels)
			{
				if (attribute == attributeLevel.attribute.Attribute)
				{
					return attributeLevel.GetLevel();
				}
			}
			return 1;
		}

		// Token: 0x06007D61 RID: 32097 RVA: 0x003224F0 File Offset: 0x003206F0
		public AttributeLevel GetAttributeLevel(string attribute_id)
		{
			foreach (AttributeLevel attributeLevel in this.levels)
			{
				if (attributeLevel.attribute.Attribute.Id == attribute_id)
				{
					return attributeLevel;
				}
			}
			return null;
		}

		// Token: 0x06007D62 RID: 32098 RVA: 0x0032255C File Offset: 0x0032075C
		public bool AddExperience(string attribute_id, float time_spent, float multiplier)
		{
			if (this.maxAttributeLevel == 0)
			{
				return false;
			}
			AttributeLevel attributeLevel = this.GetAttributeLevel(attribute_id);
			if (attributeLevel == null)
			{
				global::Debug.LogWarning(attribute_id + " has no level.");
				return false;
			}
			time_spent *= multiplier;
			AttributeConverterInstance attributeConverterInstance = Db.Get().AttributeConverters.TrainingSpeed.Lookup(this);
			if (attributeConverterInstance != null)
			{
				float num = attributeConverterInstance.Evaluate();
				time_spent += time_spent * num;
			}
			bool flag = attributeLevel.AddExperience(this, time_spent);
			attributeLevel.Apply(this);
			return flag;
		}

		// Token: 0x06007D63 RID: 32099 RVA: 0x003225D0 File Offset: 0x003207D0
		public void SetLevel(string attribute_id, int level)
		{
			AttributeLevel attributeLevel = this.GetAttributeLevel(attribute_id);
			if (attributeLevel != null)
			{
				attributeLevel.SetLevel(level);
				attributeLevel.Apply(this);
			}
		}

		// Token: 0x06007D64 RID: 32100 RVA: 0x003225F8 File Offset: 0x003207F8
		public void SetExperience(string attribute_id, float experience)
		{
			AttributeLevel attributeLevel = this.GetAttributeLevel(attribute_id);
			if (attributeLevel != null)
			{
				attributeLevel.SetExperience(experience);
				attributeLevel.Apply(this);
			}
		}

		// Token: 0x06007D65 RID: 32101 RVA: 0x0032261E File Offset: 0x0032081E
		public float GetPercentComplete(string attribute_id)
		{
			return this.GetAttributeLevel(attribute_id).GetPercentComplete();
		}

		// Token: 0x06007D66 RID: 32102 RVA: 0x0032262C File Offset: 0x0032082C
		public int GetMaxLevel()
		{
			int num = 0;
			foreach (AttributeLevel attributeLevel in this)
			{
				if (attributeLevel.GetLevel() > num)
				{
					num = attributeLevel.GetLevel();
				}
			}
			return num;
		}

		// Token: 0x04005EA3 RID: 24227
		private List<AttributeLevel> levels = new List<AttributeLevel>();

		// Token: 0x04005EA4 RID: 24228
		public int maxAttributeLevel = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MAX_GAINED_ATTRIBUTE_LEVEL;

		// Token: 0x04005EA5 RID: 24229
		[Serialize]
		private AttributeLevels.LevelSaveLoad[] saveLoadLevels = new AttributeLevels.LevelSaveLoad[0];

		// Token: 0x020025BF RID: 9663
		[Serializable]
		public struct LevelSaveLoad
		{
			// Token: 0x0400A8AA RID: 43178
			public string attributeId;

			// Token: 0x0400A8AB RID: 43179
			public float experience;

			// Token: 0x0400A8AC RID: 43180
			public int level;
		}
	}
}
