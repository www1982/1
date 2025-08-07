using System;
using UnityEngine;

// Token: 0x020005D3 RID: 1491
[AddComponentMenu("KMonoBehaviour/scripts/LiquidSourceManager")]
public class LiquidSourceManager : KMonoBehaviour, IChunkManager
{
	// Token: 0x0600227D RID: 8829 RVA: 0x000C5AAF File Offset: 0x000C3CAF
	protected override void OnPrefabInit()
	{
		LiquidSourceManager.Instance = this;
	}

	// Token: 0x0600227E RID: 8830 RVA: 0x000C5AB7 File Offset: 0x000C3CB7
	public SubstanceChunk CreateChunk(SimHashes element_id, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position)
	{
		return this.CreateChunk(ElementLoader.FindElementByHash(element_id), mass, temperature, diseaseIdx, diseaseCount, position);
	}

	// Token: 0x0600227F RID: 8831 RVA: 0x000C5ACD File Offset: 0x000C3CCD
	public SubstanceChunk CreateChunk(Element element, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position)
	{
		return GeneratedOre.CreateChunk(element, mass, temperature, diseaseIdx, diseaseCount, position);
	}

	// Token: 0x04001410 RID: 5136
	public static LiquidSourceManager Instance;
}
