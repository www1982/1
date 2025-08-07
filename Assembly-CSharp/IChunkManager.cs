using System;
using UnityEngine;

// Token: 0x02000622 RID: 1570
public interface IChunkManager
{
	// Token: 0x06002618 RID: 9752
	SubstanceChunk CreateChunk(Element element, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position);

	// Token: 0x06002619 RID: 9753
	SubstanceChunk CreateChunk(SimHashes element_id, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position);
}
