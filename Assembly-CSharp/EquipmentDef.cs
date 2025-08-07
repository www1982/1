using System;
using System.Collections.Generic;
using Klei.AI;

// Token: 0x020008F8 RID: 2296
public class EquipmentDef : Def
{
	// Token: 0x17000493 RID: 1171
	// (get) Token: 0x06004011 RID: 16401 RVA: 0x00167D3B File Offset: 0x00165F3B
	public override string Name
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".NAME");
		}
	}

	// Token: 0x17000494 RID: 1172
	// (get) Token: 0x06004012 RID: 16402 RVA: 0x00167D61 File Offset: 0x00165F61
	public string Desc
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".DESC");
		}
	}

	// Token: 0x17000495 RID: 1173
	// (get) Token: 0x06004013 RID: 16403 RVA: 0x00167D87 File Offset: 0x00165F87
	public string Effect
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".EFFECT");
		}
	}

	// Token: 0x17000496 RID: 1174
	// (get) Token: 0x06004014 RID: 16404 RVA: 0x00167DAD File Offset: 0x00165FAD
	public string GenericName
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".GENERICNAME");
		}
	}

	// Token: 0x17000497 RID: 1175
	// (get) Token: 0x06004015 RID: 16405 RVA: 0x00167DD3 File Offset: 0x00165FD3
	public string WornName
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".WORN_NAME");
		}
	}

	// Token: 0x17000498 RID: 1176
	// (get) Token: 0x06004016 RID: 16406 RVA: 0x00167DF9 File Offset: 0x00165FF9
	public string WornDesc
	{
		get
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + this.Id.ToUpper() + ".WORN_DESC");
		}
	}

	// Token: 0x040027B6 RID: 10166
	public string Id;

	// Token: 0x040027B7 RID: 10167
	public string Slot;

	// Token: 0x040027B8 RID: 10168
	public string FabricatorId;

	// Token: 0x040027B9 RID: 10169
	public float FabricationTime;

	// Token: 0x040027BA RID: 10170
	public string RecipeTechUnlock;

	// Token: 0x040027BB RID: 10171
	public SimHashes OutputElement;

	// Token: 0x040027BC RID: 10172
	public Dictionary<string, float> InputElementMassMap;

	// Token: 0x040027BD RID: 10173
	public float Mass;

	// Token: 0x040027BE RID: 10174
	public KAnimFile Anim;

	// Token: 0x040027BF RID: 10175
	public string SnapOn;

	// Token: 0x040027C0 RID: 10176
	public string SnapOn1;

	// Token: 0x040027C1 RID: 10177
	public KAnimFile BuildOverride;

	// Token: 0x040027C2 RID: 10178
	public int BuildOverridePriority;

	// Token: 0x040027C3 RID: 10179
	public bool IsBody;

	// Token: 0x040027C4 RID: 10180
	public List<AttributeModifier> AttributeModifiers;

	// Token: 0x040027C5 RID: 10181
	public string RecipeDescription;

	// Token: 0x040027C6 RID: 10182
	public List<Effect> EffectImmunites = new List<Effect>();

	// Token: 0x040027C7 RID: 10183
	public Action<Equippable> OnEquipCallBack;

	// Token: 0x040027C8 RID: 10184
	public Action<Equippable> OnUnequipCallBack;

	// Token: 0x040027C9 RID: 10185
	public EntityTemplates.CollisionShape CollisionShape;

	// Token: 0x040027CA RID: 10186
	public float width;

	// Token: 0x040027CB RID: 10187
	public float height = 0.325f;

	// Token: 0x040027CC RID: 10188
	public Tag[] AdditionalTags;

	// Token: 0x040027CD RID: 10189
	public string wornID;

	// Token: 0x040027CE RID: 10190
	public List<Descriptor> additionalDescriptors = new List<Descriptor>();
}
