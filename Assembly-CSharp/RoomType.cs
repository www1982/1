using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;

// Token: 0x02000658 RID: 1624
public class RoomType : Resource
{
	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x060027AA RID: 10154 RVA: 0x000E1CCC File Offset: 0x000DFECC
	// (set) Token: 0x060027AB RID: 10155 RVA: 0x000E1CD4 File Offset: 0x000DFED4
	public string tooltip { get; private set; }

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x060027AC RID: 10156 RVA: 0x000E1CDD File Offset: 0x000DFEDD
	// (set) Token: 0x060027AD RID: 10157 RVA: 0x000E1CE5 File Offset: 0x000DFEE5
	public string description { get; set; }

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x060027AE RID: 10158 RVA: 0x000E1CEE File Offset: 0x000DFEEE
	// (set) Token: 0x060027AF RID: 10159 RVA: 0x000E1CF6 File Offset: 0x000DFEF6
	public string effect { get; private set; }

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x060027B0 RID: 10160 RVA: 0x000E1CFF File Offset: 0x000DFEFF
	// (set) Token: 0x060027B1 RID: 10161 RVA: 0x000E1D07 File Offset: 0x000DFF07
	public RoomConstraints.Constraint primary_constraint { get; private set; }

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x060027B2 RID: 10162 RVA: 0x000E1D10 File Offset: 0x000DFF10
	// (set) Token: 0x060027B3 RID: 10163 RVA: 0x000E1D18 File Offset: 0x000DFF18
	public RoomConstraints.Constraint[] additional_constraints { get; private set; }

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x060027B4 RID: 10164 RVA: 0x000E1D21 File Offset: 0x000DFF21
	// (set) Token: 0x060027B5 RID: 10165 RVA: 0x000E1D29 File Offset: 0x000DFF29
	public int priority { get; private set; }

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000E1D32 File Offset: 0x000DFF32
	// (set) Token: 0x060027B7 RID: 10167 RVA: 0x000E1D3A File Offset: 0x000DFF3A
	public bool single_assignee { get; private set; }

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x060027B8 RID: 10168 RVA: 0x000E1D43 File Offset: 0x000DFF43
	// (set) Token: 0x060027B9 RID: 10169 RVA: 0x000E1D4B File Offset: 0x000DFF4B
	public RoomDetails.Detail[] display_details { get; private set; }

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x060027BA RID: 10170 RVA: 0x000E1D54 File Offset: 0x000DFF54
	// (set) Token: 0x060027BB RID: 10171 RVA: 0x000E1D5C File Offset: 0x000DFF5C
	public bool priority_building_use { get; private set; }

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x060027BC RID: 10172 RVA: 0x000E1D65 File Offset: 0x000DFF65
	// (set) Token: 0x060027BD RID: 10173 RVA: 0x000E1D6D File Offset: 0x000DFF6D
	public RoomTypeCategory category { get; private set; }

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x060027BE RID: 10174 RVA: 0x000E1D76 File Offset: 0x000DFF76
	// (set) Token: 0x060027BF RID: 10175 RVA: 0x000E1D7E File Offset: 0x000DFF7E
	public RoomType[] upgrade_paths { get; private set; }

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000E1D87 File Offset: 0x000DFF87
	// (set) Token: 0x060027C1 RID: 10177 RVA: 0x000E1D8F File Offset: 0x000DFF8F
	public string[] effects { get; private set; }

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x060027C2 RID: 10178 RVA: 0x000E1D98 File Offset: 0x000DFF98
	// (set) Token: 0x060027C3 RID: 10179 RVA: 0x000E1DA0 File Offset: 0x000DFFA0
	public int sortKey { get; private set; }

	// Token: 0x060027C4 RID: 10180 RVA: 0x000E1DAC File Offset: 0x000DFFAC
	public RoomType(string id, string name, string description, string tooltip, string effect, RoomTypeCategory category, RoomConstraints.Constraint primary_constraint, RoomConstraints.Constraint[] additional_constraints, RoomDetails.Detail[] display_details, int priority = 0, RoomType[] upgrade_paths = null, bool single_assignee = false, bool priority_building_use = false, string[] effects = null, int sortKey = 0)
		: base(id, name)
	{
		this.tooltip = tooltip;
		this.description = description;
		this.effect = effect;
		this.category = category;
		this.primary_constraint = primary_constraint;
		this.additional_constraints = additional_constraints;
		this.display_details = display_details;
		this.priority = priority;
		this.upgrade_paths = upgrade_paths;
		this.single_assignee = single_assignee;
		this.priority_building_use = priority_building_use;
		this.effects = effects;
		this.sortKey = sortKey;
		if (this.upgrade_paths != null)
		{
			RoomType[] upgrade_paths2 = this.upgrade_paths;
			for (int i = 0; i < upgrade_paths2.Length; i++)
			{
				Debug.Assert(upgrade_paths2[i] != null, name + " has a null upgrade path. Maybe it wasn't initialized yet.");
			}
		}
	}

	// Token: 0x060027C5 RID: 10181 RVA: 0x000E1E5C File Offset: 0x000E005C
	public RoomType.RoomIdentificationResult isSatisfactory(Room candidate_room)
	{
		if (this.primary_constraint != null && !this.primary_constraint.isSatisfied(candidate_room))
		{
			return RoomType.RoomIdentificationResult.primary_unsatisfied;
		}
		if (this.additional_constraints != null)
		{
			RoomConstraints.Constraint[] additional_constraints = this.additional_constraints;
			for (int i = 0; i < additional_constraints.Length; i++)
			{
				if (!additional_constraints[i].isSatisfied(candidate_room))
				{
					return RoomType.RoomIdentificationResult.primary_satisfied;
				}
			}
		}
		return RoomType.RoomIdentificationResult.all_satisfied;
	}

	// Token: 0x060027C6 RID: 10182 RVA: 0x000E1EAC File Offset: 0x000E00AC
	public string GetCriteriaString()
	{
		string text = string.Concat(new string[]
		{
			"<b>",
			this.Name,
			"</b>\n",
			this.tooltip,
			"\n\n",
			ROOMS.CRITERIA.HEADER
		});
		if (this == Db.Get().RoomTypes.Neutral)
		{
			text = text + "\n    • " + ROOMS.CRITERIA.NEUTRAL_TYPE;
		}
		text += ((this.primary_constraint == null) ? "" : ("\n    • " + this.primary_constraint.name));
		if (this.additional_constraints != null)
		{
			foreach (RoomConstraints.Constraint constraint in this.additional_constraints)
			{
				text = text + "\n    • " + constraint.name;
			}
		}
		return text;
	}

	// Token: 0x060027C7 RID: 10183 RVA: 0x000E1F84 File Offset: 0x000E0184
	public string GetRoomEffectsString()
	{
		if (this.effects != null && this.effects.Length != 0)
		{
			string text = ROOMS.EFFECTS.HEADER;
			foreach (string text2 in this.effects)
			{
				Effect effect = Db.Get().effects.Get(text2);
				text += Effect.CreateTooltip(effect, false, "\n    • ", false);
			}
			return text;
		}
		return null;
	}

	// Token: 0x060027C8 RID: 10184 RVA: 0x000E1FF0 File Offset: 0x000E01F0
	public void TriggerRoomEffects(KPrefabID triggerer, Effects target, out List<EffectInstance> result)
	{
		result = null;
		if (this.primary_constraint == null)
		{
			return;
		}
		if (triggerer == null)
		{
			return;
		}
		if (this.effects == null)
		{
			return;
		}
		if (this.primary_constraint.building_criteria(triggerer))
		{
			result = new List<EffectInstance>();
			foreach (string text in this.effects)
			{
				result.Add(target.Add(text, true));
			}
		}
	}

	// Token: 0x060027C9 RID: 10185 RVA: 0x000E2060 File Offset: 0x000E0260
	public void TriggerRoomEffects(KPrefabID triggerer, Effects target)
	{
		List<EffectInstance> list;
		this.TriggerRoomEffects(triggerer, target, out list);
	}

	// Token: 0x020014E8 RID: 5352
	public enum RoomIdentificationResult
	{
		// Token: 0x04006E45 RID: 28229
		all_satisfied,
		// Token: 0x04006E46 RID: 28230
		primary_satisfied,
		// Token: 0x04006E47 RID: 28231
		primary_unsatisfied
	}
}
