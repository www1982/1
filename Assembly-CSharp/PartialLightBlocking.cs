using System;
using KSerialization;

// Token: 0x020007A3 RID: 1955
[SerializationConfig(MemberSerialization.OptIn)]
public class PartialLightBlocking : KMonoBehaviour
{
	// Token: 0x060033C2 RID: 13250 RVA: 0x001228B1 File Offset: 0x00120AB1
	protected override void OnSpawn()
	{
		this.SetLightBlocking();
		base.OnSpawn();
	}

	// Token: 0x060033C3 RID: 13251 RVA: 0x001228BF File Offset: 0x00120ABF
	protected override void OnCleanUp()
	{
		this.ClearLightBlocking();
		base.OnCleanUp();
	}

	// Token: 0x060033C4 RID: 13252 RVA: 0x001228D0 File Offset: 0x00120AD0
	public void SetLightBlocking()
	{
		int[] placementCells = base.GetComponent<Building>().PlacementCells;
		for (int i = 0; i < placementCells.Length; i++)
		{
			SimMessages.SetCellProperties(placementCells[i], 48);
		}
	}

	// Token: 0x060033C5 RID: 13253 RVA: 0x00122904 File Offset: 0x00120B04
	public void ClearLightBlocking()
	{
		int[] placementCells = base.GetComponent<Building>().PlacementCells;
		for (int i = 0; i < placementCells.Length; i++)
		{
			SimMessages.ClearCellProperties(placementCells[i], 48);
		}
	}

	// Token: 0x04001F20 RID: 7968
	private const byte PartialLightBlockingProperties = 48;
}
