using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020004B2 RID: 1202
[AddComponentMenu("KMonoBehaviour/scripts/ConsumableConsumer")]
public class ConsumableConsumer : KMonoBehaviour
{
	// Token: 0x0600199B RID: 6555 RVA: 0x0008D2E0 File Offset: 0x0008B4E0
	[OnDeserialized]
	[Obsolete]
	private void OnDeserialized()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 29))
		{
			this.forbiddenTagSet = new HashSet<Tag>(this.forbiddenTags);
			this.forbiddenTags = null;
		}
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x0008D31C File Offset: 0x0008B51C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (ConsumerManager.instance != null)
		{
			this.forbiddenTagSet = new HashSet<Tag>(ConsumerManager.instance.DefaultForbiddenTagsList);
			this.SetModelDietaryRestrictions();
			return;
		}
		this.forbiddenTagSet = new HashSet<Tag>();
		this.dietaryRestrictionTagSet = new HashSet<Tag>();
	}

	// Token: 0x0600199D RID: 6557 RVA: 0x0008D36E File Offset: 0x0008B56E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.SetModelDietaryRestrictions();
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x0008D37C File Offset: 0x0008B57C
	private void SetModelDietaryRestrictions()
	{
		if (this.HasTag(GameTags.Minions.Models.Standard))
		{
			this.dietaryRestrictionTagSet = new HashSet<Tag>(ConsumerManager.instance.StandardDuplicantDietaryRestrictions);
			return;
		}
		if (this.HasTag(GameTags.Minions.Models.Bionic))
		{
			this.dietaryRestrictionTagSet = new HashSet<Tag>(ConsumerManager.instance.BionicDuplicantDietaryRestrictions);
		}
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x0008D3D0 File Offset: 0x0008B5D0
	public bool IsPermitted(string consumable_id)
	{
		Tag tag = new Tag(consumable_id);
		return !this.forbiddenTagSet.Contains(tag) && !this.dietaryRestrictionTagSet.Contains(tag);
	}

	// Token: 0x060019A0 RID: 6560 RVA: 0x0008D404 File Offset: 0x0008B604
	public bool IsDietRestricted(string consumable_id)
	{
		Tag tag = new Tag(consumable_id);
		return this.dietaryRestrictionTagSet.Contains(tag);
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x0008D428 File Offset: 0x0008B628
	public void SetPermitted(string consumable_id, bool is_allowed)
	{
		Tag tag = new Tag(consumable_id);
		is_allowed = is_allowed && !this.dietaryRestrictionTagSet.Contains(consumable_id);
		if (is_allowed)
		{
			this.forbiddenTagSet.Remove(tag);
		}
		else
		{
			this.forbiddenTagSet.Add(tag);
		}
		this.consumableRulesChanged.Signal();
	}

	// Token: 0x04000EB8 RID: 3768
	[Obsolete("Deprecated, use forbiddenTagSet")]
	[Serialize]
	[HideInInspector]
	public Tag[] forbiddenTags;

	// Token: 0x04000EB9 RID: 3769
	[Serialize]
	public HashSet<Tag> forbiddenTagSet;

	// Token: 0x04000EBA RID: 3770
	public HashSet<Tag> dietaryRestrictionTagSet;

	// Token: 0x04000EBB RID: 3771
	public global::System.Action consumableRulesChanged;
}
