using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000BDE RID: 3038
[AddComponentMenu("KMonoBehaviour/scripts/WaterCubes")]
public class WaterCubes : KMonoBehaviour
{
	// Token: 0x17000694 RID: 1684
	// (get) Token: 0x06005B11 RID: 23313 RVA: 0x0020E5D8 File Offset: 0x0020C7D8
	// (set) Token: 0x06005B12 RID: 23314 RVA: 0x0020E5DF File Offset: 0x0020C7DF
	public static WaterCubes Instance { get; private set; }

	// Token: 0x06005B13 RID: 23315 RVA: 0x0020E5E7 File Offset: 0x0020C7E7
	public static void DestroyInstance()
	{
		WaterCubes.Instance = null;
	}

	// Token: 0x06005B14 RID: 23316 RVA: 0x0020E5EF File Offset: 0x0020C7EF
	protected override void OnPrefabInit()
	{
		WaterCubes.Instance = this;
	}

	// Token: 0x06005B15 RID: 23317 RVA: 0x0020E5F8 File Offset: 0x0020C7F8
	public void Init()
	{
		this.cubes = Util.NewGameObject(base.gameObject, "WaterCubes");
		GameObject gameObject = new GameObject();
		gameObject.name = "WaterCubesMesh";
		gameObject.transform.parent = this.cubes.transform;
		this.material.renderQueue = RenderQueues.Liquid;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = this.material;
		meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
		meshRenderer.receiveShadows = false;
		meshRenderer.lightProbeUsage = LightProbeUsage.Off;
		meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
		meshRenderer.sharedMaterial.SetTexture("_MainTex2", this.waveTexture);
		meshFilter.sharedMesh = this.CreateNewMesh();
		meshRenderer.gameObject.layer = 0;
		meshRenderer.gameObject.transform.parent = base.transform;
		meshRenderer.gameObject.transform.SetPosition(new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.Liquid)));
	}

	// Token: 0x06005B16 RID: 23318 RVA: 0x0020E6F0 File Offset: 0x0020C8F0
	private Mesh CreateNewMesh()
	{
		Mesh mesh = new Mesh();
		mesh.name = "WaterCubes";
		int num = 4;
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		Vector3[] array3 = new Vector3[num];
		Vector4[] array4 = new Vector4[num];
		int[] array5 = new int[6];
		float layerZ = Grid.GetLayerZ(Grid.SceneLayer.Liquid);
		array = new Vector3[]
		{
			new Vector3(0f, 0f, layerZ),
			new Vector3((float)Grid.WidthInCells, 0f, layerZ),
			new Vector3(0f, Grid.HeightInMeters, layerZ),
			new Vector3(Grid.WidthInMeters, Grid.HeightInMeters, layerZ)
		};
		array2 = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		array3 = new Vector3[]
		{
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, -1f)
		};
		array4 = new Vector4[]
		{
			new Vector4(0f, 1f, 0f, -1f),
			new Vector4(0f, 1f, 0f, -1f),
			new Vector4(0f, 1f, 0f, -1f),
			new Vector4(0f, 1f, 0f, -1f)
		};
		array5 = new int[] { 0, 2, 1, 1, 2, 3 };
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.uv2 = array2;
		mesh.normals = array3;
		mesh.tangents = array4;
		mesh.triangles = array5;
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, 0f));
		return mesh;
	}

	// Token: 0x04003C7B RID: 15483
	public Material material;

	// Token: 0x04003C7C RID: 15484
	public Texture2D waveTexture;

	// Token: 0x04003C7D RID: 15485
	private GameObject cubes;
}
