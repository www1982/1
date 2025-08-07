using System;
using UnityEngine;

// Token: 0x02000964 RID: 2404
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Insulator")]
public class Insulator : KMonoBehaviour
{
	// Token: 0x06004504 RID: 17668 RVA: 0x0018D8F2 File Offset: 0x0018BAF2
	protected override void OnSpawn()
	{
		SimMessages.SetInsulation(Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), this.offset), this.building.Def.ThermalConductivity);
	}

	// Token: 0x06004505 RID: 17669 RVA: 0x0018D924 File Offset: 0x0018BB24
	protected override void OnCleanUp()
	{
		SimMessages.SetInsulation(Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), this.offset), 1f);
	}

	// Token: 0x04002E2F RID: 11823
	[MyCmpReq]
	private Building building;

	// Token: 0x04002E30 RID: 11824
	[SerializeField]
	public CellOffset offset = CellOffset.none;
}
