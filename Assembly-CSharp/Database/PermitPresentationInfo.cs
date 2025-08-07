using System;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000EFC RID: 3836
	public struct PermitPresentationInfo
	{
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060079B2 RID: 31154 RVA: 0x002FF85A File Offset: 0x002FDA5A
		// (set) Token: 0x060079B3 RID: 31155 RVA: 0x002FF862 File Offset: 0x002FDA62
		public string facadeFor { readonly get; private set; }

		// Token: 0x060079B4 RID: 31156 RVA: 0x002FF86B File Offset: 0x002FDA6B
		public static Sprite GetUnknownSprite()
		{
			return Assets.GetSprite("unknown");
		}

		// Token: 0x060079B5 RID: 31157 RVA: 0x002FF87C File Offset: 0x002FDA7C
		public void SetFacadeForPrefabName(string prefabName)
		{
			this.facadeFor = UI.KLEI_INVENTORY_SCREEN.ITEM_FACADE_FOR.Replace("{ConfigProperName}", prefabName);
		}

		// Token: 0x060079B6 RID: 31158 RVA: 0x002FF894 File Offset: 0x002FDA94
		public void SetFacadeForPrefabID(string prefabId)
		{
			if (Assets.TryGetPrefab(prefabId) == null)
			{
				this.facadeFor = UI.KLEI_INVENTORY_SCREEN.ITEM_DLC_REQUIRED;
				return;
			}
			this.facadeFor = UI.KLEI_INVENTORY_SCREEN.ITEM_FACADE_FOR.Replace("{ConfigProperName}", Assets.GetPrefab(prefabId).GetProperName());
		}

		// Token: 0x060079B7 RID: 31159 RVA: 0x002FF8EA File Offset: 0x002FDAEA
		public void SetFacadeForText(string text)
		{
			this.facadeFor = text;
		}

		// Token: 0x0400587E RID: 22654
		public Sprite sprite;
	}
}
