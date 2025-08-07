using System;
using UnityEngine;

// Token: 0x02000990 RID: 2448
public static class StampToolPreviewUtil
{
	// Token: 0x0600470E RID: 18190 RVA: 0x00198725 File Offset: 0x00196925
	public static Material MakeMaterial(Texture texture)
	{
		Material material = new Material(Shader.Find("Sprites/Default"));
		material.SetTexture("_MainTex", texture);
		return material;
	}

	// Token: 0x0600470F RID: 18191 RVA: 0x00198744 File Offset: 0x00196944
	public static void MakeQuad(out GameObject gameObject, out MeshRenderer meshRenderer, float mesh_size, Vector4? uvBox = null)
	{
		gameObject = new GameObject();
		gameObject.layer = LayerMask.NameToLayer("Place");
		float num = mesh_size / 2f;
		float num2 = mesh_size / 2f;
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[]
		{
			new Vector3(-num, -num2, 0f),
			new Vector3(num, -num2, 0f),
			new Vector3(-num, num2, 0f),
			new Vector3(num, num2, 0f)
		};
		mesh.triangles = new int[] { 0, 2, 1, 2, 3, 1 };
		mesh.normals = new Vector3[]
		{
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};
		Mesh mesh2 = mesh;
		Vector2[] array2;
		if (uvBox != null)
		{
			Vector2[] array = new Vector2[4];
			array[0] = new Vector2(uvBox.Value.x, uvBox.Value.w);
			array[1] = new Vector2(uvBox.Value.z, uvBox.Value.w);
			array[2] = new Vector2(uvBox.Value.x, uvBox.Value.y);
			array2 = array;
			array[3] = new Vector2(uvBox.Value.z, uvBox.Value.y);
		}
		else
		{
			Vector2[] array3 = new Vector2[4];
			array3[0] = new Vector2(0f, 0f);
			array3[1] = new Vector2(1f, 0f);
			array3[2] = new Vector2(0f, 1f);
			array2 = array3;
			array3[3] = new Vector2(1f, 1f);
		}
		mesh2.uv = array2;
		Mesh mesh3 = mesh;
		gameObject.AddComponent<MeshFilter>().mesh = mesh3;
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
	}

	// Token: 0x04002F02 RID: 12034
	public static readonly Color COLOR_OK = Color.white;

	// Token: 0x04002F03 RID: 12035
	public static readonly Color COLOR_ERROR = Color.red;

	// Token: 0x04002F04 RID: 12036
	public const float SOLID_VIS_ALPHA = 1f;

	// Token: 0x04002F05 RID: 12037
	public const float LIQUID_VIS_ALPHA = 1f;

	// Token: 0x04002F06 RID: 12038
	public const float GAS_VIS_ALPHA = 1f;

	// Token: 0x04002F07 RID: 12039
	public const float BACKGROUND_ALPHA = 1f;
}
