using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x0200100A RID: 4106
	public class Trait : Modifier, IHasDlcRestrictions
	{
		// Token: 0x06007E95 RID: 32405 RVA: 0x003297AA File Offset: 0x003279AA
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06007E96 RID: 32406 RVA: 0x003297B2 File Offset: 0x003279B2
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x06007E97 RID: 32407 RVA: 0x003297BC File Offset: 0x003279BC
		public Trait(string id, string name, string description, float rating, bool should_save, ChoreGroup[] disallowed_chore_groups, bool positive_trait, bool is_valid_starter_trait)
			: base(id, name, description)
		{
			this.Rating = rating;
			this.ShouldSave = should_save;
			this.disabledChoreGroups = disallowed_chore_groups;
			this.PositiveTrait = positive_trait;
			this.ValidStarterTrait = is_valid_starter_trait;
			this.ignoredEffects = new string[0];
			this.requiredDlcIds = null;
			this.forbiddenDlcIds = null;
		}

		// Token: 0x06007E98 RID: 32408 RVA: 0x00329814 File Offset: 0x00327A14
		public Trait(string id, string name, string description, float rating, bool should_save, ChoreGroup[] disallowed_chore_groups, bool positive_trait, bool is_valid_starter_trait, string[] requiredDlcIds, string[] forbiddenDlcIds)
			: base(id, name, description)
		{
			this.Rating = rating;
			this.ShouldSave = should_save;
			this.disabledChoreGroups = disallowed_chore_groups;
			this.PositiveTrait = positive_trait;
			this.ValidStarterTrait = is_valid_starter_trait;
			this.ignoredEffects = new string[0];
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x00329870 File Offset: 0x00327A70
		public void AddIgnoredEffects(string[] effects)
		{
			List<string> list = new List<string>(this.ignoredEffects);
			list.AddRange(effects);
			this.ignoredEffects = list.ToArray();
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x0032989C File Offset: 0x00327A9C
		public string GetName()
		{
			if (this.NameCB != null)
			{
				return this.NameCB();
			}
			return this.Name;
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x003298B8 File Offset: 0x00327AB8
		public string GetTooltip()
		{
			string text;
			if (this.TooltipCB != null)
			{
				text = this.TooltipCB();
			}
			else
			{
				text = this.description;
				text += this.GetAttributeModifiersString(true);
				text += this.GetDisabledChoresString(true);
				text += this.GetIgnoredEffectsString(true);
				text += this.GetExtendedTooltipStr();
			}
			return text;
		}

		// Token: 0x06007E9C RID: 32412 RVA: 0x0032991C File Offset: 0x00327B1C
		public string GetAttributeModifiersString(bool list_entry)
		{
			string text = "";
			foreach (AttributeModifier attributeModifier in this.SelfModifiers)
			{
				Attribute attribute = Db.Get().Attributes.Get(attributeModifier.AttributeId);
				if (list_entry)
				{
					text += DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY;
				}
				text += string.Format(DUPLICANTS.TRAITS.ATTRIBUTE_MODIFIERS, attribute.Name, attributeModifier.GetFormattedString());
			}
			return text;
		}

		// Token: 0x06007E9D RID: 32413 RVA: 0x003299BC File Offset: 0x00327BBC
		public string GetDisabledChoresString(bool list_entry)
		{
			string text = "";
			if (this.disabledChoreGroups != null)
			{
				string text2 = DUPLICANTS.TRAITS.CANNOT_DO_TASK;
				if (this.isTaskBeingRefused)
				{
					text2 = DUPLICANTS.TRAITS.REFUSES_TO_DO_TASK;
				}
				foreach (ChoreGroup choreGroup in this.disabledChoreGroups)
				{
					if (list_entry)
					{
						text += DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY;
					}
					text += string.Format(text2, choreGroup.Name);
				}
			}
			return text;
		}

		// Token: 0x06007E9E RID: 32414 RVA: 0x00329A38 File Offset: 0x00327C38
		public string GetIgnoredEffectsString(bool list_entry)
		{
			string text = "";
			if (this.ignoredEffects != null && this.ignoredEffects.Length != 0)
			{
				for (int i = 0; i < this.ignoredEffects.Length; i++)
				{
					string text2 = this.ignoredEffects[i];
					if (list_entry)
					{
						text += DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY;
					}
					string text3 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text2.ToUpper() + ".NAME");
					text += string.Format(DUPLICANTS.TRAITS.IGNORED_EFFECTS, text3);
					if (!list_entry && i < this.ignoredEffects.Length - 1)
					{
						text += "\n";
					}
				}
			}
			return text;
		}

		// Token: 0x06007E9F RID: 32415 RVA: 0x00329AE8 File Offset: 0x00327CE8
		public string GetExtendedTooltipStr()
		{
			string text = "";
			if (this.ExtendedTooltip != null)
			{
				foreach (Func<string> func in this.ExtendedTooltip.GetInvocationList())
				{
					text = text + "\n" + func();
				}
			}
			return text;
		}

		// Token: 0x06007EA0 RID: 32416 RVA: 0x00329B3C File Offset: 0x00327D3C
		public override void AddTo(Attributes attributes)
		{
			base.AddTo(attributes);
			ChoreConsumer component = attributes.gameObject.GetComponent<ChoreConsumer>();
			if (component != null && this.disabledChoreGroups != null)
			{
				foreach (ChoreGroup choreGroup in this.disabledChoreGroups)
				{
					component.SetPermittedByTraits(choreGroup, false);
				}
			}
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x00329B90 File Offset: 0x00327D90
		public override void RemoveFrom(Attributes attributes)
		{
			base.RemoveFrom(attributes);
			ChoreConsumer component = attributes.gameObject.GetComponent<ChoreConsumer>();
			if (component != null && this.disabledChoreGroups != null)
			{
				foreach (ChoreGroup choreGroup in this.disabledChoreGroups)
				{
					component.SetPermittedByTraits(choreGroup, true);
				}
			}
		}

		// Token: 0x04005F69 RID: 24425
		public float Rating;

		// Token: 0x04005F6A RID: 24426
		public bool ShouldSave;

		// Token: 0x04005F6B RID: 24427
		public bool PositiveTrait;

		// Token: 0x04005F6C RID: 24428
		public bool ValidStarterTrait;

		// Token: 0x04005F6D RID: 24429
		public Action<GameObject> OnAddTrait;

		// Token: 0x04005F6E RID: 24430
		public Func<string> TooltipCB;

		// Token: 0x04005F6F RID: 24431
		public Func<string> ExtendedTooltip;

		// Token: 0x04005F70 RID: 24432
		public Func<string> ShortDescCB;

		// Token: 0x04005F71 RID: 24433
		public Func<string> ShortDescTooltipCB;

		// Token: 0x04005F72 RID: 24434
		public Func<string> NameCB;

		// Token: 0x04005F73 RID: 24435
		public ChoreGroup[] disabledChoreGroups;

		// Token: 0x04005F74 RID: 24436
		public bool isTaskBeingRefused;

		// Token: 0x04005F75 RID: 24437
		public string[] ignoredEffects;

		// Token: 0x04005F76 RID: 24438
		public string[] requiredDlcIds;

		// Token: 0x04005F77 RID: 24439
		public string[] forbiddenDlcIds;
	}
}
