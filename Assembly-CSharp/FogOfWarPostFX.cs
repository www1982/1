using System;
using UnityEngine;

// Token: 0x02000A76 RID: 2678
public class FogOfWarPostFX : MonoBehaviour
{
	// Token: 0x06004D9B RID: 19867 RVA: 0x001C15D4 File Offset: 0x001BF7D4
	private void Awake()
	{
		if (this.shader != null)
		{
			this.material = new Material(this.shader);
		}
	}

	// Token: 0x06004D9C RID: 19868 RVA: 0x001C15F5 File Offset: 0x001BF7F5
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.SetupUVs();
		Graphics.Blit(source, destination, this.material, 0);
	}

	// Token: 0x06004D9D RID: 19869 RVA: 0x001C160C File Offset: 0x001BF80C
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
		Vector3 vector = ray.GetPoint(num);
		Vector4 vector2;
		vector2.x = vector.x / Grid.WidthInMeters;
		vector2.y = vector.y / Grid.HeightInMeters;
		ray = this.myCamera.ViewportPointToRay(Vector3.one);
		num = Mathf.Abs(ray.origin.z / ray.direction.z);
		vector = ray.GetPoint(num);
		vector2.z = vector.x / Grid.WidthInMeters - vector2.x;
		vector2.w = vector.y / Grid.HeightInMeters - vector2.y;
		this.material.SetVector("_UVOffsetScale", vector2);
	}

	// Token: 0x0400339A RID: 13210
	[SerializeField]
	private Shader shader;

	// Token: 0x0400339B RID: 13211
	private Material material;

	// Token: 0x0400339C RID: 13212
	private Camera myCamera;
}
