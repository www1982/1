using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200080B RID: 2059
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ChoreGroupManager")]
public class ChoreGroupManager : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x060037FD RID: 14333 RVA: 0x00136A4F File Offset: 0x00134C4F
	public static void DestroyInstance()
	{
		ChoreGroupManager.instance = null;
	}

	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x060037FE RID: 14334 RVA: 0x00136A57 File Offset: 0x00134C57
	public List<Tag> DefaultForbiddenTagsList
	{
		get
		{
			return this.defaultForbiddenTagsList;
		}
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x060037FF RID: 14335 RVA: 0x00136A5F File Offset: 0x00134C5F
	public Dictionary<Tag, int> DefaultChorePermission
	{
		get
		{
			return this.defaultChorePermissions;
		}
	}

	// Token: 0x06003800 RID: 14336 RVA: 0x00136A68 File Offset: 0x00134C68
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ChoreGroupManager.instance = this;
		this.ConvertOldVersion();
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			if (!this.defaultChorePermissions.ContainsKey(choreGroup.Id.ToTag()))
			{
				this.defaultChorePermissions.Add(choreGroup.Id.ToTag(), 2);
			}
		}
	}

	// Token: 0x06003801 RID: 14337 RVA: 0x00136B00 File Offset: 0x00134D00
	private void ConvertOldVersion()
	{
		foreach (Tag tag in this.defaultForbiddenTagsList)
		{
			if (!this.defaultChorePermissions.ContainsKey(tag))
			{
				this.defaultChorePermissions.Add(tag, -1);
			}
			this.defaultChorePermissions[tag] = 0;
		}
		this.defaultForbiddenTagsList.Clear();
	}

	// Token: 0x04002213 RID: 8723
	public static ChoreGroupManager instance;

	// Token: 0x04002214 RID: 8724
	[Serialize]
	private List<Tag> defaultForbiddenTagsList = new List<Tag>();

	// Token: 0x04002215 RID: 8725
	[Serialize]
	private Dictionary<Tag, int> defaultChorePermissions = new Dictionary<Tag, int>();
}
