using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000CA5 RID: 3237
[AddComponentMenu("KMonoBehaviour/scripts/CreatureFeeder")]
public class CreatureFeeder : KMonoBehaviour
{
	// Token: 0x0600638F RID: 25487 RVA: 0x0025685D File Offset: 0x00254A5D
	protected override void OnSpawn()
	{
		this.storages = base.GetComponents<Storage>();
		Components.CreatureFeeders.Add(this.GetMyWorldId(), this);
		base.Subscribe<CreatureFeeder>(-1452790913, CreatureFeeder.OnAteFromStorageDelegate);
	}

	// Token: 0x06006390 RID: 25488 RVA: 0x0025688D File Offset: 0x00254A8D
	protected override void OnCleanUp()
	{
		Components.CreatureFeeders.Remove(this.GetMyWorldId(), this);
	}

	// Token: 0x06006391 RID: 25489 RVA: 0x002568A0 File Offset: 0x00254AA0
	private void OnAteFromStorage(object data)
	{
		if (string.IsNullOrEmpty(this.effectId))
		{
			return;
		}
		(data as GameObject).GetComponent<Effects>().Add(this.effectId, true);
	}

	// Token: 0x06006392 RID: 25490 RVA: 0x002568C8 File Offset: 0x00254AC8
	public bool StoragesAreEmpty()
	{
		foreach (Storage storage in this.storages)
		{
			if (!(storage == null) && storage.Count > 0)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06006393 RID: 25491 RVA: 0x00256903 File Offset: 0x00254B03
	public Vector2I GetTargetFeederCell()
	{
		return Grid.CellToXY(Grid.OffsetCell(Grid.PosToCell(this), this.feederOffset));
	}

	// Token: 0x040043B5 RID: 17333
	public Storage[] storages;

	// Token: 0x040043B6 RID: 17334
	public string effectId;

	// Token: 0x040043B7 RID: 17335
	public CellOffset feederOffset = CellOffset.none;

	// Token: 0x040043B8 RID: 17336
	private static readonly EventSystem.IntraObjectHandler<CreatureFeeder> OnAteFromStorageDelegate = new EventSystem.IntraObjectHandler<CreatureFeeder>(delegate(CreatureFeeder component, object data)
	{
		component.OnAteFromStorage(data);
	});
}
