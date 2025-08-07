using System;
using UnityEngine;

// Token: 0x02000BBC RID: 3004
public class TrapTrigger : KMonoBehaviour
{
	// Token: 0x060059EC RID: 23020 RVA: 0x00207C90 File Offset: 0x00205E90
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameObject gameObject = base.gameObject;
		this.SetTriggerCell(Grid.PosToCell(gameObject));
		foreach (GameObject gameObject2 in this.storage.items)
		{
			this.SetStoredPosition(gameObject2);
			KBoxCollider2D component = gameObject2.GetComponent<KBoxCollider2D>();
			if (component != null)
			{
				component.enabled = true;
			}
		}
	}

	// Token: 0x060059ED RID: 23021 RVA: 0x00207D18 File Offset: 0x00205F18
	public void SetTriggerCell(int cell)
	{
		HandleVector<int>.Handle handle = this.partitionerEntry;
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Trap", base.gameObject, cell, GameScenePartitioner.Instance.trapsLayer, new Action<object>(this.OnCreatureOnTrap));
	}

	// Token: 0x060059EE RID: 23022 RVA: 0x00207D70 File Offset: 0x00205F70
	public void SetStoredPosition(GameObject go)
	{
		if (go == null)
		{
			return;
		}
		KBatchedAnimController component = go.GetComponent<KBatchedAnimController>();
		Vector3 vector = Grid.CellToPosCBC(Grid.PosToCell(base.transform.GetPosition()), Grid.SceneLayer.BuildingBack);
		if (this.addTrappedAnimationOffset)
		{
			vector.x += this.trappedOffset.x - component.Offset.x;
			vector.y += this.trappedOffset.y - component.Offset.y;
		}
		else
		{
			vector.x += this.trappedOffset.x;
			vector.y += this.trappedOffset.y;
		}
		go.transform.SetPosition(vector);
		go.GetComponent<Pickupable>().UpdateCachedCell(Grid.PosToCell(vector));
		component.SetSceneLayer(Grid.SceneLayer.BuildingFront);
	}

	// Token: 0x060059EF RID: 23023 RVA: 0x00207E48 File Offset: 0x00206048
	public void OnCreatureOnTrap(object data)
	{
		if (!base.enabled)
		{
			return;
		}
		if (!this.storage.IsEmpty())
		{
			return;
		}
		Trappable trappable = (Trappable)data;
		if (trappable.HasTag(GameTags.Stored))
		{
			return;
		}
		if (trappable.HasTag(GameTags.Trapped))
		{
			return;
		}
		if (trappable.HasTag(GameTags.Creatures.Bagged))
		{
			return;
		}
		bool flag = false;
		foreach (Tag tag in this.trappableCreatures)
		{
			if (trappable.HasTag(tag))
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return;
		}
		if (this.customConditionsToTrap != null && !this.customConditionsToTrap(trappable.gameObject))
		{
			return;
		}
		this.storage.Store(trappable.gameObject, true, false, true, false);
		this.SetStoredPosition(trappable.gameObject);
		base.Trigger(-358342870, trappable.gameObject);
	}

	// Token: 0x060059F0 RID: 23024 RVA: 0x00207F1E File Offset: 0x0020611E
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x04003BBD RID: 15293
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04003BBE RID: 15294
	public Func<GameObject, bool> customConditionsToTrap;

	// Token: 0x04003BBF RID: 15295
	public Tag[] trappableCreatures;

	// Token: 0x04003BC0 RID: 15296
	public Vector2 trappedOffset = Vector2.zero;

	// Token: 0x04003BC1 RID: 15297
	public bool addTrappedAnimationOffset = true;

	// Token: 0x04003BC2 RID: 15298
	[MyCmpReq]
	private Storage storage;
}
