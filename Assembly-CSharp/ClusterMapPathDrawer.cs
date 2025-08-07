using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000C76 RID: 3190
public class ClusterMapPathDrawer : MonoBehaviour
{
	// Token: 0x06006178 RID: 24952 RVA: 0x002430BC File Offset: 0x002412BC
	public ClusterMapPath AddPath()
	{
		ClusterMapPath clusterMapPath = global::UnityEngine.Object.Instantiate<ClusterMapPath>(this.pathPrefab, this.pathContainer);
		clusterMapPath.Init();
		return clusterMapPath;
	}

	// Token: 0x06006179 RID: 24953 RVA: 0x002430D5 File Offset: 0x002412D5
	public static List<Vector2> GetDrawPathList(Vector2 startLocation, List<AxialI> pathPoints)
	{
		List<Vector2> list = new List<Vector2>();
		list.Add(startLocation);
		list.AddRange(pathPoints.Select((AxialI point) => point.ToWorld2D()));
		return list;
	}

	// Token: 0x0400420E RID: 16910
	public ClusterMapPath pathPrefab;

	// Token: 0x0400420F RID: 16911
	public Transform pathContainer;
}
