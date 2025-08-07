using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000A52 RID: 2642
public class ParallaxBackgroundObject : KMonoBehaviour
{
	// Token: 0x1700053E RID: 1342
	// (get) Token: 0x06004C95 RID: 19605 RVA: 0x001BC6D5 File Offset: 0x001BA8D5
	public static Mesh Mesh
	{
		get
		{
			if (ParallaxBackgroundObject.mesh == null)
			{
				ParallaxBackgroundObject.mesh = Resources.GetBuiltinResource<Mesh>("Quad.fbx");
			}
			return ParallaxBackgroundObject.mesh;
		}
	}

	// Token: 0x1700053F RID: 1343
	// (get) Token: 0x06004C96 RID: 19606 RVA: 0x001BC6F8 File Offset: 0x001BA8F8
	public static int Layer
	{
		get
		{
			int num = ParallaxBackgroundObject.layer.GetValueOrDefault();
			if (ParallaxBackgroundObject.layer == null)
			{
				num = LayerMask.NameToLayer("Default");
				ParallaxBackgroundObject.layer = new int?(num);
			}
			return ParallaxBackgroundObject.layer.Value;
		}
	}

	// Token: 0x17000540 RID: 1344
	// (get) Token: 0x06004C97 RID: 19607 RVA: 0x001BC73C File Offset: 0x001BA93C
	public static float Depth
	{
		get
		{
			float num = ParallaxBackgroundObject.depth.GetValueOrDefault();
			if (ParallaxBackgroundObject.depth == null)
			{
				num = Grid.GetLayerZ(Grid.SceneLayer.Background) + 0.8f;
				ParallaxBackgroundObject.depth = new float?(num);
			}
			return ParallaxBackgroundObject.depth.Value;
		}
	}

	// Token: 0x06004C98 RID: 19608 RVA: 0x001BC784 File Offset: 0x001BA984
	private void OnActiveWorldChanged(object data)
	{
		if (this.worldId == null)
		{
			return;
		}
		int first = ((global::Tuple<int, int>)data).first;
		this.visible = first == this.worldId.Value;
	}

	// Token: 0x06004C99 RID: 19609 RVA: 0x001BC7BF File Offset: 0x001BA9BF
	public void Initialize(string texture)
	{
		this.sprite = Assets.GetSprite(texture);
	}

	// Token: 0x06004C9A RID: 19610 RVA: 0x001BC7D2 File Offset: 0x001BA9D2
	public void SetVisibilityState(bool visible)
	{
		this.visible = visible;
	}

	// Token: 0x06004C9B RID: 19611 RVA: 0x001BC7DB File Offset: 0x001BA9DB
	public void PlayPlayerClickFeedback()
	{
		this.material.SetFloat("_LastTimePlayerClickedNotification", Time.unscaledTime);
	}

	// Token: 0x06004C9C RID: 19612 RVA: 0x001BC7F2 File Offset: 0x001BA9F2
	public void PlayExplosion()
	{
		this.material.SetFloat("_LastTimeExploding", Time.unscaledTime);
	}

	// Token: 0x06004C9D RID: 19613 RVA: 0x001BC80C File Offset: 0x001BAA0C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.Subscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
		this.distanceUpdate = true;
		this.startOffset = new Vector2(0f, 0f);
		this.endOffset = new Vector2(0.5f, 0.2f);
		Material material = Assets.GetMaterial("BGPlanet");
		this.material = new Material(material);
		this.material.SetTexture("_MainTex", this.sprite.texture);
		this.material.SetFloat("_LastTimeDamaged", float.MinValue);
		this.material.SetFloat("_LastTimePlayerClickedNotification", float.MinValue);
		this.material.SetFloat("_SizeProgress", 0f);
		this.material.renderQueue = RenderQueues.Stars;
	}

	// Token: 0x06004C9E RID: 19614 RVA: 0x001BC8ED File Offset: 0x001BAAED
	public void TriggerShaderDamagedEffect(int _)
	{
		this.material.SetFloat("_LastTimeDamaged", Time.unscaledTime);
	}

	// Token: 0x17000541 RID: 1345
	// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x001BC90D File Offset: 0x001BAB0D
	// (set) Token: 0x06004C9F RID: 19615 RVA: 0x001BC904 File Offset: 0x001BAB04
	public float lastScaleUsed { get; private set; } = 1f;

	// Token: 0x06004CA1 RID: 19617 RVA: 0x001BC918 File Offset: 0x001BAB18
	private void LateUpdate()
	{
		if (this.motion == null)
		{
			return;
		}
		if (!this.visible)
		{
			return;
		}
		if (this.distanceUpdate)
		{
			float duration = this.motion.GetDuration();
			this.normalizedDistance = ((duration == 0f) ? 1f : (1f - Mathf.Pow(this.motion.GetETA() / duration, 4f)));
			this.motion.OnNormalizedDistanceChanged(this.normalizedDistance);
		}
		Color color = new Color(0.16862746f, 0.22745098f, 0.36078432f, 0f);
		this.material.color = Color.Lerp(color, Color.white, this.normalizedDistance);
		float num = Mathf.Lerp(this.scaleMin, this.scaleMax, this.normalizedDistance);
		this.lastScaleUsed = num;
		Vector2 vector = Vector2.Lerp(this.startOffset, this.endOffset, this.normalizedDistance);
		Vector3 position = CameraController.Instance.baseCamera.transform.position;
		Vector3 vector2 = new Vector3(position.x * this.parallaxFactor, position.y * this.parallaxFactor, ParallaxBackgroundObject.Depth);
		float num2 = CameraController.Instance.baseCamera.orthographicSize / 1f;
		Vector3 vector3 = vector2 + vector * num2;
		Vector3 vector4 = num * num2 * Vector3.one;
		Quaternion quaternion = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0f, 0f, -20f), this.normalizedDistance);
		this.material.SetFloat("_UnscaledTime", Time.unscaledTime);
		this.material.SetVector("_Random", new Vector4(global::UnityEngine.Random.value, global::UnityEngine.Random.value));
		this.material.SetFloat("_SizeProgress", this.normalizedDistance);
		Matrix4x4 matrix4x = Matrix4x4.Translate(vector3) * Matrix4x4.Scale(vector4) * Matrix4x4.Rotate(quaternion);
		Graphics.DrawMesh(ParallaxBackgroundObject.Mesh, matrix4x, this.material, ParallaxBackgroundObject.Layer);
	}

	// Token: 0x040032C3 RID: 12995
	private static Mesh mesh;

	// Token: 0x040032C4 RID: 12996
	private static int? layer;

	// Token: 0x040032C5 RID: 12997
	private static float? depth;

	// Token: 0x040032C6 RID: 12998
	[SerializeField]
	private Sprite sprite;

	// Token: 0x040032C7 RID: 12999
	[SerializeField]
	private float parallaxFactor = 1f;

	// Token: 0x040032C8 RID: 13000
	[Range(0f, 5f)]
	public float scaleMin = 0.25f;

	// Token: 0x040032C9 RID: 13001
	[Range(0f, 5f)]
	public float scaleMax = 3f;

	// Token: 0x040032CA RID: 13002
	[Serialize]
	private bool visible = true;

	// Token: 0x040032CB RID: 13003
	private const string SHADER_DAMAGED_TIME_VARIABLE_NAME = "_LastTimeDamaged";

	// Token: 0x040032CC RID: 13004
	private const string SHADER_PLAYER_CLICKED_TIME_VARIABLE_NAME = "_LastTimePlayerClickedNotification";

	// Token: 0x040032CD RID: 13005
	private const string SHADER_SIZE_PROGRESS_VARIABLE_NAME = "_SizeProgress";

	// Token: 0x040032CE RID: 13006
	private const string SHADER_EXPLOSION_START_TIME_VARIABLE_NAME = "_LastTimeExploding";

	// Token: 0x040032CF RID: 13007
	[SerializeField]
	private Material material;

	// Token: 0x040032D0 RID: 13008
	[SerializeField]
	[Range(0f, 1f)]
	private float normalizedDistance;

	// Token: 0x040032D1 RID: 13009
	[SerializeField]
	private bool distanceUpdate;

	// Token: 0x040032D2 RID: 13010
	[SerializeField]
	private Vector2 startOffset;

	// Token: 0x040032D3 RID: 13011
	[SerializeField]
	private Vector2 endOffset;

	// Token: 0x040032D4 RID: 13012
	[Serialize]
	public int? worldId;

	// Token: 0x040032D5 RID: 13013
	public ParallaxBackgroundObject.IMotion motion;

	// Token: 0x02001B0D RID: 6925
	public interface IMotion
	{
		// Token: 0x0600A5FA RID: 42490
		float GetETA();

		// Token: 0x0600A5FB RID: 42491
		float GetDuration();

		// Token: 0x0600A5FC RID: 42492
		void OnNormalizedDistanceChanged(float normalizedDistance);
	}
}
