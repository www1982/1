using System;
using UnityEngine;

// Token: 0x02000A3A RID: 2618
public class ObjectLayerListItem
{
	// Token: 0x17000532 RID: 1330
	// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x001B7F0A File Offset: 0x001B610A
	// (set) Token: 0x06004BF9 RID: 19449 RVA: 0x001B7F12 File Offset: 0x001B6112
	public ObjectLayerListItem previousItem { get; private set; }

	// Token: 0x17000533 RID: 1331
	// (get) Token: 0x06004BFA RID: 19450 RVA: 0x001B7F1B File Offset: 0x001B611B
	// (set) Token: 0x06004BFB RID: 19451 RVA: 0x001B7F23 File Offset: 0x001B6123
	public ObjectLayerListItem nextItem { get; private set; }

	// Token: 0x17000534 RID: 1332
	// (get) Token: 0x06004BFC RID: 19452 RVA: 0x001B7F2C File Offset: 0x001B612C
	// (set) Token: 0x06004BFD RID: 19453 RVA: 0x001B7F34 File Offset: 0x001B6134
	public GameObject gameObject { get; private set; }

	// Token: 0x06004BFE RID: 19454 RVA: 0x001B7F3D File Offset: 0x001B613D
	public ObjectLayerListItem(GameObject gameObject, ObjectLayer layer, int new_cell)
	{
		this.gameObject = gameObject;
		this.layer = layer;
		this.Refresh(new_cell);
	}

	// Token: 0x06004BFF RID: 19455 RVA: 0x001B7F66 File Offset: 0x001B6166
	public void Clear()
	{
		this.Refresh(Grid.InvalidCell);
	}

	// Token: 0x06004C00 RID: 19456 RVA: 0x001B7F74 File Offset: 0x001B6174
	public bool Refresh(int new_cell)
	{
		if (this.cell != new_cell)
		{
			if (this.cell != Grid.InvalidCell && Grid.Objects[this.cell, (int)this.layer] == this.gameObject)
			{
				GameObject gameObject = null;
				if (this.nextItem != null && this.nextItem.gameObject != null)
				{
					gameObject = this.nextItem.gameObject;
				}
				Grid.Objects[this.cell, (int)this.layer] = gameObject;
			}
			if (this.previousItem != null)
			{
				this.previousItem.nextItem = this.nextItem;
			}
			if (this.nextItem != null)
			{
				this.nextItem.previousItem = this.previousItem;
			}
			this.previousItem = null;
			this.nextItem = null;
			this.cell = new_cell;
			if (this.cell != Grid.InvalidCell)
			{
				GameObject gameObject2 = Grid.Objects[this.cell, (int)this.layer];
				if (gameObject2 != null && gameObject2 != this.gameObject)
				{
					ObjectLayerListItem objectLayerListItem = gameObject2.GetComponent<Pickupable>().objectLayerListItem;
					this.nextItem = objectLayerListItem;
					objectLayerListItem.previousItem = this;
				}
				Grid.Objects[this.cell, (int)this.layer] = this.gameObject;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06004C01 RID: 19457 RVA: 0x001B80B8 File Offset: 0x001B62B8
	public bool Update(int cell)
	{
		return this.Refresh(cell);
	}

	// Token: 0x0400325B RID: 12891
	private int cell = Grid.InvalidCell;

	// Token: 0x0400325C RID: 12892
	private ObjectLayer layer;
}
