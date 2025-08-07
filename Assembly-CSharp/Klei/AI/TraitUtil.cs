using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x0200100B RID: 4107
	public class TraitUtil
	{
		// Token: 0x06007EA2 RID: 32418 RVA: 0x00329BE2 File Offset: 0x00327DE2
		public static global::System.Action CreateDisabledTaskTrait(string id, string name, string desc, string disabled_chore_group, bool is_valid_starter_trait)
		{
			return delegate
			{
				ChoreGroup[] array = new ChoreGroup[] { Db.Get().ChoreGroups.Get(disabled_chore_group) };
				Db.Get().CreateTrait(id, name, desc, null, true, array, false, is_valid_starter_trait);
			};
		}

		// Token: 0x06007EA3 RID: 32419 RVA: 0x00329C18 File Offset: 0x00327E18
		public static global::System.Action CreateTrait(string id, string name, string desc, string attributeId, float delta, string[] chore_groups, bool positiveTrait = false)
		{
			return delegate
			{
				List<ChoreGroup> list = new List<ChoreGroup>();
				foreach (string text in chore_groups)
				{
					list.Add(Db.Get().ChoreGroups.Get(text));
				}
				Db.Get().CreateTrait(id, name, desc, null, true, list.ToArray(), positiveTrait, true).Add(new AttributeModifier(attributeId, delta, name, false, false, true));
			};
		}

		// Token: 0x06007EA4 RID: 32420 RVA: 0x00329C6C File Offset: 0x00327E6C
		public static global::System.Action CreateAttributeEffectTrait(string id, string name, string desc, string attributeId, float delta, string attributeId2, float delta2, bool positiveTrait = false)
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, true);
				trait.Add(new AttributeModifier(attributeId, delta, name, false, false, true));
				trait.Add(new AttributeModifier(attributeId2, delta2, name, false, false, true));
			};
		}

		// Token: 0x06007EA5 RID: 32421 RVA: 0x00329CC5 File Offset: 0x00327EC5
		public static global::System.Action CreateAttributeEffectTrait(string id, string name, string desc, string[] attributeIds, float[] deltas, bool positiveTrait = false)
		{
			return delegate
			{
				global::Debug.Assert(attributeIds.Length == deltas.Length, "CreateAttributeEffectTrait must have an equal number of attributeIds and deltas");
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, true);
				for (int i = 0; i < attributeIds.Length; i++)
				{
					trait.Add(new AttributeModifier(attributeIds[i], deltas[i], name, false, false, true));
				}
			};
		}

		// Token: 0x06007EA6 RID: 32422 RVA: 0x00329D04 File Offset: 0x00327F04
		public static global::System.Action CreateAttributeEffectTrait(string id, string name, string desc, string attributeId, float delta, bool positiveTrait = false, Action<GameObject> on_add = null, bool is_valid_starter_trait = true)
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, is_valid_starter_trait);
				trait.Add(new AttributeModifier(attributeId, delta, name, false, false, true));
				trait.OnAddTrait = on_add;
			};
		}

		// Token: 0x06007EA7 RID: 32423 RVA: 0x00329D5D File Offset: 0x00327F5D
		public static global::System.Action CreateEffectModifierTrait(string id, string name, string desc, string[] ignoredEffects, bool positiveTrait = false)
		{
			return delegate
			{
				Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, true).AddIgnoredEffects(ignoredEffects);
			};
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x00329D93 File Offset: 0x00327F93
		public static global::System.Action CreateNamedTrait(string id, string name, string desc, bool positiveTrait = false)
		{
			return delegate
			{
				Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, true);
			};
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x00329DC4 File Offset: 0x00327FC4
		public static global::System.Action CreateTrait(string id, string name, string desc, Action<GameObject> on_add, ChoreGroup[] disabled_chore_groups = null, bool positiveTrait = false, Func<string> extendedDescFn = null)
		{
			return TraitUtil.CreateTrait(id, name, desc, on_add, null, null, disabled_chore_groups, positiveTrait, extendedDescFn);
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x00329DE4 File Offset: 0x00327FE4
		public static global::System.Action CreateTrait(string id, string name, string desc, Action<GameObject> on_add, string[] requiredDlcIds, string[] forbiddenDlcIds = null, ChoreGroup[] disabled_chore_groups = null, bool positiveTrait = false, Func<string> extendedDescFn = null)
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, disabled_chore_groups, positiveTrait, true, requiredDlcIds, forbiddenDlcIds);
				trait.OnAddTrait = on_add;
				if (extendedDescFn != null)
				{
					Trait trait2 = trait;
					trait2.ExtendedTooltip = (Func<string>)Delegate.Combine(trait2.ExtendedTooltip, extendedDescFn);
				}
			};
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x00329E45 File Offset: 0x00328045
		public static global::System.Action CreateComponentTrait<T>(string id, string name, string desc, bool positiveTrait = false, Func<string> extendedDescFn = null) where T : KMonoBehaviour
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, true);
				trait.OnAddTrait = delegate(GameObject go)
				{
					go.FindOrAddUnityComponent<T>();
				};
				if (extendedDescFn != null)
				{
					Trait trait2 = trait;
					trait2.ExtendedTooltip = (Func<string>)Delegate.Combine(trait2.ExtendedTooltip, extendedDescFn);
				}
			};
		}

		// Token: 0x06007EAC RID: 32428 RVA: 0x00329E7B File Offset: 0x0032807B
		public static global::System.Action CreateSkillGrantingTrait(string id, string name, string desc, string skillId)
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, true, true);
				trait.TooltipCB = () => string.Format(DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_DESC, desc, SkillWidget.SkillPerksString(Db.Get().Skills.Get(skillId)));
				trait.OnAddTrait = delegate(GameObject go)
				{
					MinionResume component = go.GetComponent<MinionResume>();
					if (component != null)
					{
						component.GrantSkill(skillId);
					}
				};
			};
		}

		// Token: 0x06007EAD RID: 32429 RVA: 0x00329EAC File Offset: 0x003280AC
		public static string GetSkillGrantingTraitNameById(string id)
		{
			string text = "";
			StringEntry stringEntry;
			if (Strings.TryGet("STRINGS.DUPLICANTS.TRAITS.GRANTSKILL_" + id.ToUpper() + ".NAME", out stringEntry))
			{
				text = stringEntry.String;
			}
			return text;
		}

		// Token: 0x06007EAE RID: 32430 RVA: 0x00329EE5 File Offset: 0x003280E5
		public static global::System.Action CreateBionicUpgradeTrait(string id, string effectsDescription)
		{
			return delegate
			{
				string name = Strings.Get("STRINGS.DUPLICANTS.TRAITS." + id.ToUpper() + ".NAME");
				string desc = Strings.Get("STRINGS.DUPLICANTS.TRAITS." + id.ToUpper() + ".DESC");
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, true, true);
				trait.TooltipCB = () => desc + "\n\n" + effectsDescription;
				trait.NameCB = () => name;
				string shortDescTooltip = Strings.Get("STRINGS.DUPLICANTS.TRAITS." + trait.Id.ToUpper() + ".SHORT_DESC_TOOLTIP");
				trait.ShortDescTooltipCB = () => shortDescTooltip + "\n\n" + effectsDescription;
			};
		}
	}
}
