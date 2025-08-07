using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020008F7 RID: 2295
[AddComponentMenu("KMonoBehaviour/scripts/EquipmentConfigManager")]
public class EquipmentConfigManager : KMonoBehaviour
{
	// Token: 0x0600400B RID: 16395 RVA: 0x00167A3E File Offset: 0x00165C3E
	public static void DestroyInstance()
	{
		EquipmentConfigManager.Instance = null;
	}

	// Token: 0x0600400C RID: 16396 RVA: 0x00167A46 File Offset: 0x00165C46
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		EquipmentConfigManager.Instance = this;
	}

	// Token: 0x0600400D RID: 16397 RVA: 0x00167A54 File Offset: 0x00165C54
	public void RegisterEquipment(IEquipmentConfig config)
	{
		string[] array = null;
		string[] array2 = null;
		if (config.GetDlcIds() != null)
		{
			DlcManager.ConvertAvailableToRequireAndForbidden(config.GetDlcIds(), out array, out array2);
			DebugUtil.DevLogError(string.Format("{0} implements GetDlcIds, which is obsolete.", config.GetType()));
		}
		else
		{
			IHasDlcRestrictions hasDlcRestrictions = config as IHasDlcRestrictions;
			if (hasDlcRestrictions != null)
			{
				array = hasDlcRestrictions.GetRequiredDlcIds();
				array2 = hasDlcRestrictions.GetForbiddenDlcIds();
			}
		}
		if (!DlcManager.IsCorrectDlcSubscribed(array, array2))
		{
			return;
		}
		EquipmentDef equipmentDef = config.CreateEquipmentDef();
		GameObject gameObject = EntityTemplates.CreateLooseEntity(equipmentDef.Id, equipmentDef.Name, equipmentDef.RecipeDescription, equipmentDef.Mass, true, equipmentDef.Anim, "object", Grid.SceneLayer.Ore, equipmentDef.CollisionShape, equipmentDef.width, equipmentDef.height, true, 0, equipmentDef.OutputElement, null);
		Equippable equippable = gameObject.AddComponent<Equippable>();
		equippable.def = equipmentDef;
		global::Debug.Assert(equippable.def != null);
		equippable.slotID = equipmentDef.Slot;
		global::Debug.Assert(equippable.slot != null);
		config.DoPostConfigure(gameObject);
		Assets.AddPrefab(gameObject.GetComponent<KPrefabID>());
		if (equipmentDef.wornID != null)
		{
			GameObject gameObject2 = EntityTemplates.CreateLooseEntity(equipmentDef.wornID, equipmentDef.WornName, equipmentDef.WornDesc, equipmentDef.Mass, true, equipmentDef.Anim, "worn_out", Grid.SceneLayer.Ore, equipmentDef.CollisionShape, equipmentDef.width, equipmentDef.height, true, 0, SimHashes.Creature, null);
			RepairableEquipment repairableEquipment = gameObject2.AddComponent<RepairableEquipment>();
			repairableEquipment.def = equipmentDef;
			global::Debug.Assert(repairableEquipment.def != null);
			SymbolOverrideControllerUtil.AddToPrefab(gameObject2);
			foreach (Tag tag in equipmentDef.AdditionalTags)
			{
				gameObject2.GetComponent<KPrefabID>().AddTag(tag, false);
			}
			Assets.AddPrefab(gameObject2.GetComponent<KPrefabID>());
		}
	}

	// Token: 0x0600400E RID: 16398 RVA: 0x00167C0C File Offset: 0x00165E0C
	private void LoadRecipe(EquipmentDef def, Equippable equippable)
	{
		Recipe recipe = new Recipe(def.Id, 1f, (SimHashes)0, null, def.RecipeDescription, 0);
		recipe.SetFabricator(def.FabricatorId, def.FabricationTime);
		recipe.TechUnlock = def.RecipeTechUnlock;
		foreach (KeyValuePair<string, float> keyValuePair in def.InputElementMassMap)
		{
			recipe.AddIngredient(new Recipe.Ingredient(keyValuePair.Key, keyValuePair.Value));
		}
	}

	// Token: 0x0600400F RID: 16399 RVA: 0x00167CAC File Offset: 0x00165EAC
	[Conditional("UNITY_EDITOR")]
	private void ValidateEquipmentConfig(IEquipmentConfig equipmentConfig)
	{
		if (equipmentConfig == null)
		{
			throw new ArgumentNullException("equipmentConfig");
		}
		Type type = equipmentConfig.GetType();
		Type typeFromHandle = typeof(IHasDlcRestrictions);
		bool flag = type.GetMethod("GetRequiredDlcIds", Type.EmptyTypes) != null;
		bool flag2 = type.GetMethod("GetForbiddenDlcIds", Type.EmptyTypes) != null;
		bool flag3 = typeFromHandle.IsAssignableFrom(type);
		if ((flag || flag2) && !flag3)
		{
			DebugUtil.LogErrorArgs(new object[] { type.Name + " is an IEquipmentConfig and has GetRequiredDlcIds or GetForbiddenDlcIds but does not implement IHasDlcRestrictions." });
		}
	}

	// Token: 0x040027B5 RID: 10165
	public static EquipmentConfigManager Instance;
}
