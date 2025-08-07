using System;
using UnityEngine;

// Token: 0x02000A75 RID: 2677
public class ClusterCoverPostFX : MonoBehaviour
{
	// Token: 0x06004D97 RID: 19863 RVA: 0x001C13CC File Offset: 0x001BF5CC
	private void Awake()
	{
		if (this.shader != null)
		{
			this.material = new Material(this.shader);
		}
	}

	// Token: 0x06004D98 RID: 19864 RVA: 0x001C13ED File Offset: 0x001BF5ED
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.SetupUVs();
		Graphics.Blit(source, destination, this.material, 0);
	}

	// Token: 0x06004D99 RID: 19865 RVA: 0x001C1404 File Offset: 0x001BF604
	private void SetupUVs()
	{
		if (this.myCamera == null)
		{
			this.myCamera = base.GetComponent<Camera>();
			if (this.myCamera == null)
			{
				return;
			}
		}
		Ray ray = this.myCamera.ViewportPointToRay(Vector3.zero);
		float num = Mathf.Abs(ray.origin.z / ray.direction.z);
		Vector3 point = ray.GetPoint(num);
		ray = this.myCamera.ViewportPointToRay(Vector3.one);
		num = Mathf.Abs(ray.origin.z / ray.direction.z);
		Vector3 point2 = ray.GetPoint(num);
		Vector4 vector;
		vector.x = point.x;
		vector.y = point.y;
		vector.z = point2.x - point.x;
		vector.w = point2.y - point.y;
		this.material.SetVector("_CameraCoords", vector);
		Vector4 vector2;
		if (ClusterManager.Instance != null && !CameraController.Instance.ignoreClusterFX)
		{
			WorldContainer activeWorld = ClusterManager.Instance.activeWorld;
			Vector2I worldOffset = activeWorld.WorldOffset;
			Vector2I worldSize = activeWorld.WorldSize;
			vector2 = new Vector4((float)worldOffset.x, (float)worldOffset.y, (float)worldSize.x, (float)worldSize.y);
			this.material.SetFloat("_HideSurface", ClusterManager.Instance.activeWorld.FullyEnclosedBorder ? 1f : 0f);
		}
		else
		{
			vector2 = new Vector4(0f, 0f, (float)Grid.WidthInCells, (float)Grid.HeightInCells);
			this.material.SetFloat("_HideSurface", 0f);
		}
		this.material.SetVector("_WorldCoords", vector2);
	}

	// Token: 0x04003397 RID: 13207
	[SerializeField]
	private Shader shader;

	// Token: 0x04003398 RID: 13208
	private Material material;

	// Token: 0x04003399 RID: 13209
	private Camera myCamera;
}
