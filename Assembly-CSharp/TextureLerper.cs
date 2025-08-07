using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000AB9 RID: 2745
public class TextureLerper
{
	// Token: 0x06004F96 RID: 20374 RVA: 0x001CC734 File Offset: 0x001CA934
	public TextureLerper(Texture target_texture, string name, FilterMode filter_mode = FilterMode.Bilinear, TextureFormat texture_format = TextureFormat.ARGB32)
	{
		this.name = name;
		this.Init(target_texture.width, target_texture.height, name, filter_mode, texture_format);
		this.Material.SetTexture("_TargetTex", target_texture);
	}

	// Token: 0x06004F97 RID: 20375 RVA: 0x001CC78C File Offset: 0x001CA98C
	private void Init(int width, int height, string name, FilterMode filter_mode, TextureFormat texture_format)
	{
		for (int i = 0; i < 2; i++)
		{
			this.BlendTextures[i] = new RenderTexture(width, height, 0, TextureUtil.GetRenderTextureFormat(texture_format));
			this.BlendTextures[i].filterMode = filter_mode;
			this.BlendTextures[i].name = name;
		}
		this.Material = new Material(Shader.Find("Klei/LerpEffect"));
		this.Material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
		this.mesh = new Mesh();
		this.mesh.name = "LerpEffect";
		this.mesh.vertices = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(1f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(1f, 0f, 0f)
		};
		this.mesh.triangles = new int[] { 0, 1, 2, 0, 3, 1 };
		this.mesh.uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 1f),
			new Vector2(0f, 1f),
			new Vector2(1f, 0f)
		};
		int num = LayerMask.NameToLayer("RTT");
		int mask = LayerMask.GetMask(new string[] { "RTT" });
		this.cameraGO = new GameObject();
		this.cameraGO.name = "TextureLerper_" + name;
		this.textureCam = this.cameraGO.AddComponent<Camera>();
		this.textureCam.transform.SetPosition(new Vector3((float)TextureLerper.offsetCounter + 0.5f, 0.5f, 0f));
		this.textureCam.clearFlags = CameraClearFlags.Nothing;
		this.textureCam.depth = -100f;
		this.textureCam.allowHDR = false;
		this.textureCam.orthographic = true;
		this.textureCam.orthographicSize = 0.5f;
		this.textureCam.cullingMask = mask;
		this.textureCam.targetTexture = this.dest;
		this.textureCam.nearClipPlane = -5f;
		this.textureCam.farClipPlane = 5f;
		this.textureCam.useOcclusionCulling = false;
		this.textureCam.aspect = 1f;
		this.textureCam.rect = new Rect(0f, 0f, 1f, 1f);
		this.meshGO = new GameObject();
		this.meshGO.name = "mesh";
		this.meshGO.transform.parent = this.cameraGO.transform;
		this.meshGO.transform.SetLocalPosition(new Vector3(-0.5f, -0.5f, 0f));
		this.meshGO.isStatic = true;
		MeshRenderer meshRenderer = this.meshGO.AddComponent<MeshRenderer>();
		meshRenderer.receiveShadows = false;
		meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
		meshRenderer.lightProbeUsage = LightProbeUsage.Off;
		meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
		this.meshGO.AddComponent<MeshFilter>().mesh = this.mesh;
		meshRenderer.sharedMaterial = this.Material;
		this.cameraGO.SetLayerRecursively(num);
		TextureLerper.offsetCounter++;
	}

	// Token: 0x06004F98 RID: 20376 RVA: 0x001CCB1C File Offset: 0x001CAD1C
	public void LongUpdate(float dt)
	{
		this.BlendDt = dt;
		this.BlendTime = 0f;
	}

	// Token: 0x06004F99 RID: 20377 RVA: 0x001CCB30 File Offset: 0x001CAD30
	public Texture Update()
	{
		float num = Time.deltaTime * this.Speed;
		if (Time.deltaTime == 0f)
		{
			num = Time.unscaledDeltaTime * this.Speed;
		}
		float num2 = Mathf.Min(num / Mathf.Max(this.BlendDt - this.BlendTime, 0f), 1f);
		this.BlendTime += num;
		if (GameUtil.IsCapturingTimeLapse())
		{
			num2 = 1f;
		}
		this.source = this.BlendTextures[this.BlendIdx];
		this.BlendIdx = (this.BlendIdx + 1) % 2;
		this.dest = this.BlendTextures[this.BlendIdx];
		Vector4 visibleCellRange = this.GetVisibleCellRange();
		visibleCellRange = new Vector4(0f, 0f, (float)Grid.WidthInCells, (float)Grid.HeightInCells);
		this.Material.SetFloat("_Lerp", num2);
		this.Material.SetTexture("_SourceTex", this.source);
		this.Material.SetVector("_MeshParams", visibleCellRange);
		this.textureCam.targetTexture = this.dest;
		return this.dest;
	}

	// Token: 0x06004F9A RID: 20378 RVA: 0x001CCC4C File Offset: 0x001CAE4C
	private Vector4 GetVisibleCellRange()
	{
		Camera main = Camera.main;
		float cellSizeInMeters = Grid.CellSizeInMeters;
		Ray ray = main.ViewportPointToRay(Vector3.zero);
		float num = Mathf.Abs(ray.origin.z / ray.direction.z);
		Vector3 vector = ray.GetPoint(num);
		int num2 = Grid.PosToCell(vector);
		float num3 = -Grid.HalfCellSizeInMeters;
		vector = Grid.CellToPos(num2, num3, num3, num3);
		int num4 = Math.Max(0, (int)(vector.x / cellSizeInMeters));
		int num5 = Math.Max(0, (int)(vector.y / cellSizeInMeters));
		ray = main.ViewportPointToRay(Vector3.one);
		num = Mathf.Abs(ray.origin.z / ray.direction.z);
		vector = ray.GetPoint(num);
		int num6 = Mathf.CeilToInt(vector.x / cellSizeInMeters);
		int num7 = Mathf.CeilToInt(vector.y / cellSizeInMeters);
		num6 = Mathf.Min(num6, Grid.WidthInCells - 1);
		num7 = Mathf.Min(num7, Grid.HeightInCells - 1);
		return new Vector4((float)num4, (float)num5, (float)num6, (float)num7);
	}

	// Token: 0x0400358D RID: 13709
	private static int offsetCounter;

	// Token: 0x0400358E RID: 13710
	public string name;

	// Token: 0x0400358F RID: 13711
	private RenderTexture[] BlendTextures = new RenderTexture[2];

	// Token: 0x04003590 RID: 13712
	private float BlendDt;

	// Token: 0x04003591 RID: 13713
	private float BlendTime;

	// Token: 0x04003592 RID: 13714
	private int BlendIdx;

	// Token: 0x04003593 RID: 13715
	private Material Material;

	// Token: 0x04003594 RID: 13716
	public float Speed = 1f;

	// Token: 0x04003595 RID: 13717
	private Mesh mesh;

	// Token: 0x04003596 RID: 13718
	private RenderTexture source;

	// Token: 0x04003597 RID: 13719
	private RenderTexture dest;

	// Token: 0x04003598 RID: 13720
	private GameObject meshGO;

	// Token: 0x04003599 RID: 13721
	private GameObject cameraGO;

	// Token: 0x0400359A RID: 13722
	private Camera textureCam;

	// Token: 0x0400359B RID: 13723
	private float blend;
}
