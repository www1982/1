using System;
using UnityEngine;

// Token: 0x02000800 RID: 2048
public static class CameraSaveData
{
	// Token: 0x060037C0 RID: 14272 RVA: 0x0013544C File Offset: 0x0013364C
	public static void Load(FastReader reader)
	{
		CameraSaveData.position = reader.ReadVector3();
		CameraSaveData.localScale = reader.ReadVector3();
		CameraSaveData.rotation = reader.ReadQuaternion();
		CameraSaveData.orthographicsSize = reader.ReadSingle();
		CameraSaveData.valid = true;
	}

	// Token: 0x040021D3 RID: 8659
	public static bool valid;

	// Token: 0x040021D4 RID: 8660
	public static Vector3 position;

	// Token: 0x040021D5 RID: 8661
	public static Vector3 localScale;

	// Token: 0x040021D6 RID: 8662
	public static Quaternion rotation;

	// Token: 0x040021D7 RID: 8663
	public static float orthographicsSize;
}
