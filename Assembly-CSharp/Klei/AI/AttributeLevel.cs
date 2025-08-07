using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FD7 RID: 4055
	public class AttributeLevel
	{
		// Token: 0x06007D50 RID: 32080 RVA: 0x0032201C File Offset: 0x0032021C
		public AttributeLevel(AttributeInstance attribute)
		{
			this.notification = new Notification(MISC.NOTIFICATIONS.LEVELUP.NAME, NotificationType.Good, new Func<List<Notification>, object, string>(AttributeLevel.OnLevelUpTooltip), null, true, 0f, null, null, null, true, false, false);
			this.attribute = attribute;
		}

		// Token: 0x06007D51 RID: 32081 RVA: 0x00322070 File Offset: 0x00320270
		public int GetLevel()
		{
			return this.level;
		}

		// Token: 0x06007D52 RID: 32082 RVA: 0x00322078 File Offset: 0x00320278
		public void Apply(AttributeLevels levels)
		{
			Attributes attributes = levels.GetAttributes();
			if (this.modifier != null)
			{
				attributes.Remove(this.modifier);
				this.modifier = null;
			}
			this.modifier = new AttributeModifier(this.attribute.Id, (float)this.GetLevel(), DUPLICANTS.MODIFIERS.SKILLLEVEL.NAME, false, false, true);
			attributes.Add(this.modifier);
		}

		// Token: 0x06007D53 RID: 32083 RVA: 0x003220DD File Offset: 0x003202DD
		public void SetExperience(float experience)
		{
			this.experience = experience;
		}

		// Token: 0x06007D54 RID: 32084 RVA: 0x003220E6 File Offset: 0x003202E6
		public void SetLevel(int level)
		{
			this.level = level;
		}

		// Token: 0x06007D55 RID: 32085 RVA: 0x003220F0 File Offset: 0x003202F0
		public float GetExperienceForNextLevel()
		{
			float num = Mathf.Pow((float)this.level / (float)this.maxGainedLevel, DUPLICANTSTATS.ATTRIBUTE_LEVELING.EXPERIENCE_LEVEL_POWER) * (float)DUPLICANTSTATS.ATTRIBUTE_LEVELING.TARGET_MAX_LEVEL_CYCLE * 600f;
			return Mathf.Pow(((float)this.level + 1f) / (float)this.maxGainedLevel, DUPLICANTSTATS.ATTRIBUTE_LEVELING.EXPERIENCE_LEVEL_POWER) * (float)DUPLICANTSTATS.ATTRIBUTE_LEVELING.TARGET_MAX_LEVEL_CYCLE * 600f - num;
		}

		// Token: 0x06007D56 RID: 32086 RVA: 0x00322152 File Offset: 0x00320352
		public float GetPercentComplete()
		{
			return this.experience / this.GetExperienceForNextLevel();
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x00322164 File Offset: 0x00320364
		public void LevelUp(AttributeLevels levels)
		{
			this.level++;
			this.experience = 0f;
			this.Apply(levels);
			this.experience = 0f;
			if (PopFXManager.Instance != null)
			{
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, this.attribute.modifier.Name, levels.transform, new Vector3(0f, 0.5f, 0f), 1.5f, false, false);
			}
			levels.GetComponent<Notifier>().Add(this.notification, string.Format(MISC.NOTIFICATIONS.LEVELUP.SUFFIX, this.attribute.modifier.Name, this.level));
			StateMachine.Instance instance = new UpgradeFX.Instance(levels.GetComponent<KMonoBehaviour>(), new Vector3(0f, 0f, -0.1f));
			ReportManager.Instance.ReportValue(ReportManager.ReportType.LevelUp, 1f, levels.GetProperName(), null);
			instance.StartSM();
			levels.Trigger(-110704193, this.attribute.Id);
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x0032227C File Offset: 0x0032047C
		public bool AddExperience(AttributeLevels levels, float experience)
		{
			if (this.level >= this.maxGainedLevel)
			{
				return false;
			}
			this.experience += experience;
			this.experience = Mathf.Max(0f, this.experience);
			if (this.experience >= this.GetExperienceForNextLevel())
			{
				this.LevelUp(levels);
				return true;
			}
			return false;
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x003222D5 File Offset: 0x003204D5
		private static string OnLevelUpTooltip(List<Notification> notifications, object data)
		{
			return MISC.NOTIFICATIONS.LEVELUP.TOOLTIP + notifications.ReduceMessages(false);
		}

		// Token: 0x04005E9D RID: 24221
		public float experience;

		// Token: 0x04005E9E RID: 24222
		public int level;

		// Token: 0x04005E9F RID: 24223
		public AttributeInstance attribute;

		// Token: 0x04005EA0 RID: 24224
		public AttributeModifier modifier;

		// Token: 0x04005EA1 RID: 24225
		public Notification notification;

		// Token: 0x04005EA2 RID: 24226
		public int maxGainedLevel = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MAX_GAINED_ATTRIBUTE_LEVEL;
	}
}
