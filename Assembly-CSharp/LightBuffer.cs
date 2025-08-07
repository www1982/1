using System;
using UnityEngine;

// Token: 0x02000AA9 RID: 2729
public class LightBuffer : MonoBehaviour
{
	// Token: 0x06004F44 RID: 20292 RVA: 0x001C9B7C File Offset: 0x001C7D7C
	private void Awake()
	{
		LightBuffer.Instance = this;
		this.ColorRangeTag = Shader.PropertyToID("_ColorRange");
		this.LightPosTag = Shader.PropertyToID("_LightPos");
		this.LightDirectionAngleTag = Shader.PropertyToID("_LightDirectionAngle");
		this.TintColorTag = Shader.PropertyToID("_TintColor");
		this.Camera = base.GetComponent<Camera>();
		this.Layer = LayerMask.NameToLayer("Lights");
		this.Mesh = new Mesh();
		this.Mesh.name = "Light Mesh";
		this.Mesh.vertices = new Vector3[]
		{
			new Vector3(-1f, -1f, 0f),
			new Vector3(-1f, 1f, 0f),
			new Vector3(1f, -1f, 0f),
			new Vector3(1f, 1f, 0f)
		};
		this.Mesh.uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 0f),
			new Vector2(1f, 1f)
		};
		this.Mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };
		this.Mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));
		this.Texture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBHalf);
		this.Texture.name = "LightBuffer";
		this.Camera.targetTexture = this.Texture;
	}

	// Token: 0x06004F45 RID: 20293 RVA: 0x001C9D6C File Offset: 0x001C7F6C
	private void LateUpdate()
	{
		if (PropertyTextures.instance == null)
		{
			return;
		}
		if (this.Texture.width != Screen.width || this.Texture.height != Screen.height)
		{
			this.Texture.DestroyRenderTexture();
			this.Texture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBHalf);
			this.Texture.name = "LightBuffer";
			this.Camera.targetTexture = this.Texture;
		}
		Matrix4x4 matrix4x = default(Matrix4x4);
		this.WorldLight = PropertyTextures.instance.GetTexture(PropertyTextures.Property.WorldLight);
		this.Material.SetTexture("_PropertyWorldLight", this.WorldLight);
		this.CircleMaterial.SetTexture("_PropertyWorldLight", this.WorldLight);
		this.ConeMaterial.SetTexture("_PropertyWorldLight", this.WorldLight);
		GridArea visibleAreaExtended = GridVisibleArea.GetVisibleAreaExtended((int)this.largestLightRange + 4);
		DictionaryPool<int, int, LightBuffer>.PooledDictionary pooledDictionary = DictionaryPool<int, int, LightBuffer>.Allocate();
		foreach (Light2D light2D in Components.Light2Ds.Items)
		{
			if (!(light2D == null) && visibleAreaExtended.Contains(light2D.cachedCell))
			{
				Vector3 position = light2D.transform.GetPosition();
				int num = Grid.PosToCell(position);
				int num2;
				pooledDictionary.TryGetValue(num, out num2);
				if (num2 < this.maxLights)
				{
					pooledDictionary[num] = num2 + 1;
					MaterialPropertyBlock materialPropertyBlock = light2D.materialPropertyBlock;
					materialPropertyBlock.SetVector(this.ColorRangeTag, new Vector4(light2D.Color.r * light2D.IntensityAnimation, light2D.Color.g * light2D.IntensityAnimation, light2D.Color.b * light2D.IntensityAnimation, light2D.Range));
					position.x += light2D.Offset.x;
					position.y += light2D.Offset.y;
					materialPropertyBlock.SetVector(this.LightPosTag, new Vector4(position.x, position.y, 0f, 0f));
					Vector2 normalized = light2D.Direction.normalized;
					materialPropertyBlock.SetVector(this.LightDirectionAngleTag, new Vector4(normalized.x, normalized.y, 0f, light2D.Angle));
					Graphics.DrawMesh(this.Mesh, Vector3.zero, Quaternion.identity, this.Material, this.Layer, this.Camera, 0, materialPropertyBlock, false, false);
					if (light2D.drawOverlay)
					{
						materialPropertyBlock.SetColor(this.TintColorTag, light2D.overlayColour);
						global::LightShape shape = light2D.shape;
						if (shape != global::LightShape.Circle)
						{
							if (shape == global::LightShape.Cone)
							{
								matrix4x.SetTRS(position - Vector3.up * (light2D.Range * 0.5f), Quaternion.identity, new Vector3(1f, 0.5f, 1f) * light2D.Range);
								Graphics.DrawMesh(this.Mesh, matrix4x, this.ConeMaterial, this.Layer, this.Camera, 0, materialPropertyBlock);
							}
						}
						else
						{
							matrix4x.SetTRS(position, Quaternion.identity, Vector3.one * light2D.Range);
							Graphics.DrawMesh(this.Mesh, matrix4x, this.CircleMaterial, this.Layer, this.Camera, 0, materialPropertyBlock);
						}
					}
					this.largestLightRange = Mathf.Max(this.largestLightRange, light2D.Range);
				}
			}
		}
		pooledDictionary.Recycle();
	}

	// Token: 0x06004F46 RID: 20294 RVA: 0x001CA130 File Offset: 0x001C8330
	private void OnDestroy()
	{
		LightBuffer.Instance = null;
	}

	// Token: 0x0400348E RID: 13454
	public int maxLights = 30;

	// Token: 0x0400348F RID: 13455
	private Mesh Mesh;

	// Token: 0x04003490 RID: 13456
	private Camera Camera;

	// Token: 0x04003491 RID: 13457
	public float largestLightRange = 16f;

	// Token: 0x04003492 RID: 13458
	[NonSerialized]
	public Material Material;

	// Token: 0x04003493 RID: 13459
	[NonSerialized]
	public Material CircleMaterial;

	// Token: 0x04003494 RID: 13460
	[NonSerialized]
	public Material ConeMaterial;

	// Token: 0x04003495 RID: 13461
	private int ColorRangeTag;

	// Token: 0x04003496 RID: 13462
	private int LightPosTag;

	// Token: 0x04003497 RID: 13463
	private int LightDirectionAngleTag;

	// Token: 0x04003498 RID: 13464
	private int TintColorTag;

	// Token: 0x04003499 RID: 13465
	private int Layer;

	// Token: 0x0400349A RID: 13466
	public RenderTexture Texture;

	// Token: 0x0400349B RID: 13467
	public Texture WorldLight;

	// Token: 0x0400349C RID: 13468
	public static LightBuffer Instance;

	// Token: 0x0400349D RID: 13469
	private const RenderTextureFormat RTFormat = RenderTextureFormat.ARGBHalf;
}
