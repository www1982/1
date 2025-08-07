using System;
using UnityEngine;

// Token: 0x020005BE RID: 1470
[AddComponentMenu("KMonoBehaviour/scripts/GasSourceManager")]
public class GasSourceManager : KMonoBehaviour, IChunkManager
{
	// Token: 0x060021E4 RID: 8676 RVA: 0x000C3D4A File Offset: 0x000C1F4A
	protected override void OnPrefabInit()
	{
		GasSourceManager.Instance = this;
	}

	// Token: 0x060021E5 RID: 8677 RVA: 0x000C3D52 File Offset: 0x000C1F52
	public SubstanceChunk CreateChunk(SimHashes element_id, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position)
	{
		return this.CreateChunk(ElementLoader.FindElementByHash(element_id), mass, temperature, diseaseIdx, diseaseCount, position);
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x000C3D68 File Offset: 0x000C1F68
	public SubstanceChunk CreateChunk(Element element, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position)
	{
		return GeneratedOre.CreateChunk(element, mass, temperature, diseaseIdx, diseaseCount, position);
	}

	// Token: 0x040013D0 RID: 5072
	public static GasSourceManager Instance;
}
