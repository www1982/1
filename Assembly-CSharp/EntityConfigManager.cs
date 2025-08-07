using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020008ED RID: 2285
[AddComponentMenu("KMonoBehaviour/scripts/EntityConfigManager")]
public class EntityConfigManager : KMonoBehaviour
{
	// Token: 0x06003FD0 RID: 16336 RVA: 0x001663F4 File Offset: 0x001645F4
	public static void DestroyInstance()
	{
		EntityConfigManager.Instance = null;
	}

	// Token: 0x06003FD1 RID: 16337 RVA: 0x001663FC File Offset: 0x001645FC
	protected override void OnPrefabInit()
	{
		EntityConfigManager.Instance = this;
	}

	// Token: 0x06003FD2 RID: 16338 RVA: 0x00166404 File Offset: 0x00164604
	private static int GetSortOrder(Type type)
	{
		foreach (Attribute attribute in type.GetCustomAttributes(true))
		{
			if (attribute.GetType() == typeof(EntityConfigOrder))
			{
				return (attribute as EntityConfigOrder).sortOrder;
			}
		}
		return 0;
	}

	// Token: 0x06003FD3 RID: 16339 RVA: 0x00166454 File Offset: 0x00164654
	public void LoadGeneratedEntities(List<Type> types)
	{
		Type typeFromHandle = typeof(IEntityConfig);
		Type typeFromHandle2 = typeof(IMultiEntityConfig);
		List<EntityConfigManager.ConfigEntry> list = new List<EntityConfigManager.ConfigEntry>();
		foreach (Type type in types)
		{
			if ((typeFromHandle.IsAssignableFrom(type) || typeFromHandle2.IsAssignableFrom(type)) && !type.IsAbstract && !type.IsInterface)
			{
				int sortOrder = EntityConfigManager.GetSortOrder(type);
				EntityConfigManager.ConfigEntry configEntry = new EntityConfigManager.ConfigEntry
				{
					type = type,
					sortOrder = sortOrder
				};
				list.Add(configEntry);
			}
		}
		list.Sort((EntityConfigManager.ConfigEntry x, EntityConfigManager.ConfigEntry y) => x.sortOrder.CompareTo(y.sortOrder));
		foreach (EntityConfigManager.ConfigEntry configEntry2 in list)
		{
			object obj = Activator.CreateInstance(configEntry2.type);
			if (obj is IEntityConfig)
			{
				IEntityConfig entityConfig = obj as IEntityConfig;
				string[] array = null;
				string[] array2 = null;
				if (entityConfig.GetDlcIds() != null)
				{
					DlcManager.ConvertAvailableToRequireAndForbidden(entityConfig.GetDlcIds(), out array, out array2);
					DebugUtil.DevLogError(string.Format("{0} implements GetDlcIds, which is obsolete.", configEntry2.type));
				}
				else
				{
					IHasDlcRestrictions hasDlcRestrictions = obj as IHasDlcRestrictions;
					if (hasDlcRestrictions != null)
					{
						array = hasDlcRestrictions.GetRequiredDlcIds();
						array2 = hasDlcRestrictions.GetForbiddenDlcIds();
					}
				}
				if (DlcManager.IsCorrectDlcSubscribed(array, array2))
				{
					this.RegisterEntity(entityConfig, array, array2);
				}
			}
			IMultiEntityConfig multiEntityConfig = obj as IMultiEntityConfig;
			if (multiEntityConfig != null)
			{
				DebugUtil.Assert(!(obj is IHasDlcRestrictions), "IMultiEntityConfig cannot implement IHasDlcRestrictions, wrap the individual config instead.");
				this.RegisterEntities(multiEntityConfig);
			}
		}
	}

	// Token: 0x06003FD4 RID: 16340 RVA: 0x0016662C File Offset: 0x0016482C
	[Conditional("UNITY_EDITOR")]
	private void ValidateEntityConfig(IEntityConfig entityConfig)
	{
		if (entityConfig == null)
		{
			throw new ArgumentNullException("entityConfig");
		}
		Type type = entityConfig.GetType();
		Type typeFromHandle = typeof(IHasDlcRestrictions);
		bool flag = type.GetMethod("GetRequiredDlcIds", Type.EmptyTypes) != null;
		bool flag2 = type.GetMethod("GetForbiddenDlcIds", Type.EmptyTypes) != null;
		bool flag3 = typeFromHandle.IsAssignableFrom(type);
		if ((flag || flag2) && !flag3)
		{
			DebugUtil.LogErrorArgs(new object[] { type.Name + " is an IEntityConfig and has GetRequiredDlcIds or GetForbiddenDlcIds but does not implement IHasDlcRestrictions." });
		}
	}

	// Token: 0x06003FD5 RID: 16341 RVA: 0x001666B4 File Offset: 0x001648B4
	[Conditional("UNITY_EDITOR")]
	private void ValidateMultiEntityConfig(IMultiEntityConfig entityConfig)
	{
		if (entityConfig == null)
		{
			throw new ArgumentNullException("entityConfig");
		}
		Type type = entityConfig.GetType();
		bool flag = type.GetMethod("GetRequiredDlcIds", Type.EmptyTypes) != null;
		bool flag2 = type.GetMethod("GetForbiddenDlcIds", Type.EmptyTypes) != null;
		if (flag || flag2)
		{
			DebugUtil.LogErrorArgs(new object[] { type.Name + " is an IMultiEntityConfig and you shouldn't be specifying GetRequiredDlcIds or GetForbiddenDlcIds. Wrap each config in a DLC check instead." });
		}
	}

	// Token: 0x06003FD6 RID: 16342 RVA: 0x00166728 File Offset: 0x00164928
	public void RegisterEntity(IEntityConfig config, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
	{
		GameObject gameObject = config.CreatePrefab();
		if (gameObject == null)
		{
			return;
		}
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.requiredDlcIds = requiredDlcIds;
		component.forbiddenDlcIds = forbiddenDlcIds;
		component.prefabInitFn += config.OnPrefabInit;
		component.prefabSpawnFn += config.OnSpawn;
		Assets.AddPrefab(component);
	}

	// Token: 0x06003FD7 RID: 16343 RVA: 0x00166788 File Offset: 0x00164988
	public void RegisterEntities(IMultiEntityConfig config)
	{
		foreach (GameObject gameObject in config.CreatePrefabs())
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			component.prefabInitFn += config.OnPrefabInit;
			component.prefabSpawnFn += config.OnSpawn;
			Assets.AddPrefab(component);
		}
	}

	// Token: 0x0400279F RID: 10143
	public static EntityConfigManager Instance;

	// Token: 0x02001897 RID: 6295
	private struct ConfigEntry
	{
		// Token: 0x04007960 RID: 31072
		public Type type;

		// Token: 0x04007961 RID: 31073
		public int sortOrder;
	}
}
