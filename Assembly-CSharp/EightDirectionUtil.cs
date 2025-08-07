using System;
using KSerialization;
using UnityEngine;

// Token: 0x020008D6 RID: 2262
[SerializationConfig(MemberSerialization.OptIn)]
public class EightDirectionUtil
{
	// Token: 0x06003EB1 RID: 16049 RVA: 0x00161578 File Offset: 0x0015F778
	public static int GetDirectionIndex(EightDirection direction)
	{
		return (int)direction;
	}

	// Token: 0x06003EB2 RID: 16050 RVA: 0x0016157B File Offset: 0x0015F77B
	public static EightDirection AngleToDirection(int angle)
	{
		return (EightDirection)Mathf.Floor((float)angle / 45f);
	}

	// Token: 0x06003EB3 RID: 16051 RVA: 0x0016158B File Offset: 0x0015F78B
	public static Vector3 GetNormal(EightDirection direction)
	{
		return EightDirectionUtil.normals[EightDirectionUtil.GetDirectionIndex(direction)];
	}

	// Token: 0x06003EB4 RID: 16052 RVA: 0x0016159D File Offset: 0x0015F79D
	public static float GetAngle(EightDirection direction)
	{
		return (float)(45 * EightDirectionUtil.GetDirectionIndex(direction));
	}

	// Token: 0x040026B9 RID: 9913
	public static readonly Vector3[] normals = new Vector3[]
	{
		Vector3.up,
		(Vector3.up + Vector3.left).normalized,
		Vector3.left,
		(Vector3.down + Vector3.left).normalized,
		Vector3.down,
		(Vector3.down + Vector3.right).normalized,
		Vector3.right,
		(Vector3.up + Vector3.right).normalized
	};
}
