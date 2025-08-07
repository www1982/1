using System;
using System.Collections.Generic;
using FMOD.Studio;
using KSerialization;
using UnityEngine;

// Token: 0x020009A2 RID: 2466
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Comet")]
public class LargeComet : KMonoBehaviour, ISim33ms
{
	// Token: 0x17000502 RID: 1282
	// (get) Token: 0x06004798 RID: 18328 RVA: 0x0019C6CF File Offset: 0x0019A8CF
	// (set) Token: 0x06004797 RID: 18327 RVA: 0x0019C6C6 File Offset: 0x0019A8C6
	public float LandingProgress { get; private set; }

	// Token: 0x17000503 RID: 1283
	// (get) Token: 0x06004799 RID: 18329 RVA: 0x0019C6D7 File Offset: 0x0019A8D7
	public Vector3 VisualPosition
	{
		get
		{
			return base.transform.position + this.anim.Offset;
		}
	}

	// Token: 0x17000504 RID: 1284
	// (get) Token: 0x0600479A RID: 18330 RVA: 0x0019C6F4 File Offset: 0x0019A8F4
	public Vector3 VisualPositionCentredImage
	{
		get
		{
			return this.VisualPosition + new Vector3(0f, (float)Mathf.Abs(this.lowestTemplateYLocalPosition), 0f);
		}
	}

	// Token: 0x17000505 RID: 1285
	// (get) Token: 0x0600479B RID: 18331 RVA: 0x0019C71C File Offset: 0x0019A91C
	// (set) Token: 0x0600479C RID: 18332 RVA: 0x0019C724 File Offset: 0x0019A924
	public Vector2 Velocity
	{
		get
		{
			return this.velocity;
		}
		set
		{
			this.velocity = value;
		}
	}

	// Token: 0x0600479D RID: 18333 RVA: 0x0019C730 File Offset: 0x0019A930
	private float GetVolume(GameObject gameObject)
	{
		float num = 1f;
		if (gameObject != null && this.selectable != null && this.selectable.IsSelected)
		{
			num = 1f;
		}
		return num;
	}

	// Token: 0x0600479E RID: 18334 RVA: 0x0019C76E File Offset: 0x0019A96E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.loopingSounds = base.gameObject.GetComponent<LoopingSounds>();
		this.flyingSound = GlobalAssets.GetSound("Meteor_LP", false);
		this.SetVelocity();
	}

	// Token: 0x0600479F RID: 18335 RVA: 0x0019C7A0 File Offset: 0x0019A9A0
	protected override void OnSpawn()
	{
		this.anim.Offset = this.offsetPosition;
		this.SetupOffset();
		this.child_controllers = base.GetComponents<KBatchedAnimController>();
		KBatchedAnimController[] array = this.child_controllers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Offset = this.anim.Offset;
		}
		base.OnSpawn();
		this.StartLoopingSound();
		bool flag = this.offsetPosition.x != 0f || this.offsetPosition.y != 0f;
		this.selectable.enabled = !flag;
		Vector3 position = base.gameObject.transform.position;
		foreach (KeyValuePair<string, string> keyValuePair in this.additionalAnimFiles)
		{
			this.additionalAnimControllers.Add(this.AddEffectAnim(keyValuePair.Key, keyValuePair.Value, position));
			position.z -= 0.001f;
		}
		KBatchedAnimController kbatchedAnimController = this.AddEffectAnim(this.mainAnimFile.Key, this.mainAnimFile.Value, position);
		this.additionalAnimControllers.Add(kbatchedAnimController);
		this.mainChildrenAnimController = kbatchedAnimController;
		this.mainChildrenAnimController.materialType = KAnimBatchGroup.MaterialType.Invisible;
		this.initialPosition = this.VisualPosition;
		this.lowestTemplateYLocalPosition = this.asteroidTemplate.GetTemplateBounds(0).yMin;
		this.templateWidth = this.asteroidTemplate.GetTemplateBounds(0).width;
		this.InitializeMaterial();
		CameraController.Instance.RegisterCustomScreenPostProcessingEffect(new Func<RenderTexture, Material>(this.DrawComet));
		this.fromStampToCrashPosition = this.stampLocation - this.crashPosition;
	}

	// Token: 0x060047A0 RID: 18336 RVA: 0x0019C978 File Offset: 0x0019AB78
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		CameraController.Instance.UnregisterCustomScreenPostProcessingEffect(new Func<RenderTexture, Material>(this.DrawComet));
	}

	// Token: 0x060047A1 RID: 18337 RVA: 0x0019C998 File Offset: 0x0019AB98
	private KBatchedAnimController AddEffectAnim(string anim_file, string anim_name, Vector3 startPosition)
	{
		KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect(anim_file, startPosition, null, false, Grid.SceneLayer.Front, false);
		kbatchedAnimController.Play(anim_name, KAnim.PlayMode.Loop, 1f, 0f);
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		kbatchedAnimController.animScale = 0.1f;
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.Offset = this.anim.Offset;
		return kbatchedAnimController;
	}

	// Token: 0x060047A2 RID: 18338 RVA: 0x0019C9F4 File Offset: 0x0019ABF4
	protected void SetupOffset()
	{
		Vector3 position = base.transform.GetPosition();
		Vector3 position2 = base.transform.GetPosition();
		position2.z = 0f;
		Vector3 vector = new Vector3(this.velocity.x, this.velocity.y, 0f);
		WorldContainer myWorld = base.gameObject.GetMyWorld();
		float num = (float)(myWorld.WorldOffset.y + myWorld.Height + MissileLauncher.Def.launchRange.y) * Grid.CellSizeInMeters - position2.y;
		float num2 = Vector3.Angle(Vector3.up, -vector) * 0.017453292f;
		float num3 = Mathf.Abs(num / Mathf.Cos(num2));
		Vector3 vector2 = position2 - vector.normalized * num3;
		float num4 = (float)(myWorld.WorldOffset.x + myWorld.Width) * Grid.CellSizeInMeters;
		if (vector2.x < (float)myWorld.WorldOffset.x * Grid.CellSizeInMeters || vector2.x > num4)
		{
			float num5 = ((vector.x < 0f) ? (num4 - position2.x) : (position2.x - (float)myWorld.WorldOffset.x * Grid.CellSizeInMeters));
			num2 = Vector3.Angle((vector.x < 0f) ? Vector3.right : Vector3.left, -vector) * 0.017453292f;
			num3 = Mathf.Abs(num5 / Mathf.Cos(num2));
		}
		Vector3 vector3 = -vector.normalized * num3;
		(position2 + vector3).z = position.z;
		this.offsetPosition = vector3;
		this.anim.Offset = this.offsetPosition;
		this.worldID = myWorld.id;
		this.previousVisualPosition = this.VisualPosition;
	}

	// Token: 0x060047A3 RID: 18339 RVA: 0x0019CBD0 File Offset: 0x0019ADD0
	public void SetVelocity()
	{
		int num = -90;
		float num2 = (float)num * 3.1415927f / 180f;
		int num3 = 12;
		this.velocity = new Vector2(-Mathf.Cos(num2) * (float)num3, Mathf.Sin(num2) * (float)num3);
		base.GetComponent<KBatchedAnimController>().Rotation = (float)(-(float)num) - 90f;
	}

	// Token: 0x060047A4 RID: 18340 RVA: 0x0019CC24 File Offset: 0x0019AE24
	private void Explode(Vector3 pos)
	{
		this.PlayImpactSound(pos);
		if (this.OnImpact != null)
		{
			this.OnImpact();
		}
		foreach (KAnimControllerBase kanimControllerBase in this.additionalAnimControllers)
		{
			global::Util.KDestroyGameObject(kanimControllerBase);
		}
		global::Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x060047A5 RID: 18341 RVA: 0x0019CC9C File Offset: 0x0019AE9C
	public void Sim33ms(float dt)
	{
		if (this.hasExploded)
		{
			return;
		}
		if (this.offsetPosition.y > 0f)
		{
			Vector3 vector = new Vector3(this.velocity.x * dt, this.velocity.y * dt, 0f);
			Vector3 vector2 = this.offsetPosition + vector;
			this.offsetPosition = vector2;
			this.anim.Offset = this.offsetPosition;
			using (List<KAnimControllerBase>.Enumerator enumerator = this.additionalAnimControllers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KAnimControllerBase kanimControllerBase = enumerator.Current;
					kanimControllerBase.Offset = this.offsetPosition;
				}
				goto IL_01E2;
			}
		}
		if (this.anim.Offset != Vector3.zero)
		{
			this.anim.Offset = Vector3.zero;
			foreach (KAnimControllerBase kanimControllerBase2 in this.additionalAnimControllers)
			{
				kanimControllerBase2.Offset = this.anim.Offset;
			}
		}
		Vector3 position = base.transform.GetPosition();
		Vector3 vector3 = position + new Vector3(this.velocity.x * dt, this.velocity.y * dt, 0f);
		this.loopingSounds.UpdateVelocity(this.flyingSound, vector3 - position);
		base.transform.SetPosition(vector3);
		Vector3 vector4 = vector3;
		foreach (KAnimControllerBase kanimControllerBase3 in this.additionalAnimControllers)
		{
			kanimControllerBase3.transform.SetPosition(vector4);
			vector4.z -= 0.001f;
		}
		if (vector3.y < (float)this.crashPosition.y)
		{
			this.Explode(vector3);
		}
		IL_01E2:
		Vector2I vector2I = Grid.PosToXY(this.previousVisualPosition);
		Vector2I vector2I2 = Grid.PosToXY(this.VisualPosition);
		vector2I.y = Mathf.Clamp(vector2I.y, this.crashPosition.y, int.MaxValue);
		vector2I2.y = Mathf.Clamp(vector2I2.y, this.crashPosition.y, int.MaxValue);
		if (vector2I2.y != vector2I.y)
		{
			Grid.CollectCellsInLine(Grid.XYToCell(vector2I.x, vector2I.y), Grid.XYToCell(vector2I2.x, vector2I2.y), this.cellsCentrePassedThrough);
			bool flag = false;
			Vector3 vector5 = Vector3.zero;
			foreach (int num in this.cellsCentrePassedThrough)
			{
				foreach (CellOffset cellOffset in this.bottomCellsOffsetOfTemplate.Values)
				{
					int num2 = Grid.OffsetCell(Grid.OffsetCell(num, 0, Mathf.Abs(this.lowestTemplateYLocalPosition)), cellOffset.x, cellOffset.y);
					if (Grid.IsValidCellInWorld(num2, this.worldID) && this.DestroyCell(num2) && !flag)
					{
						Vector3 vector6 = Grid.CellToPos(num2);
						if (this.IsPositionFarAwayFromOtherExplosions(vector6))
						{
							flag = true;
							vector5 = Grid.CellToPos(num2);
						}
					}
				}
			}
			if (flag)
			{
				this.PlayExplosionEffectOnPosition(vector5);
			}
		}
		float num3 = Mathf.Clamp(1f - (this.VisualPosition.y - (float)this.crashPosition.y) / (this.initialPosition.y - (float)this.crashPosition.y), 0f, 1f);
		this.mainChildrenAnimController.postProcessingParameters = Mathf.Clamp(Mathf.Ceil(num3 * (Mathf.Pow(10f, 3f) - 1f)), 0f, float.MaxValue);
		this.LandingProgress = num3;
		this.previousVisualPosition = this.VisualPosition;
		this.age += dt;
	}

	// Token: 0x060047A6 RID: 18342 RVA: 0x0019D0EC File Offset: 0x0019B2EC
	private bool IsPositionFarAwayFromOtherExplosions(Vector3 position)
	{
		this.activeExplosionPosition.z = position.z;
		for (int i = 0; i < 30; i++)
		{
			if (this.ShaderExplosions[i].z >= 0f && Time.timeSinceLevelLoad - this.ShaderExplosions[i].z < 1.2333333f)
			{
				this.activeExplosionPosition.x = this.ShaderExplosions[i].x;
				this.activeExplosionPosition.y = this.ShaderExplosions[i].y;
				if ((this.activeExplosionPosition - position).magnitude < this.minSeparationBetweenExplosions)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060047A7 RID: 18343 RVA: 0x0019D1AC File Offset: 0x0019B3AC
	private void PlayExplosionEffectOnPosition(Vector3 position)
	{
		for (int i = 0; i < 30; i++)
		{
			if (this.ShaderExplosions[i].z < 0f || Time.timeSinceLevelLoad - this.ShaderExplosions[i].z > 1.2333333f)
			{
				this.ShaderExplosions[i].x = position.x;
				this.ShaderExplosions[i].y = position.y;
				this.ShaderExplosions[i].z = Time.timeSinceLevelLoad;
				KFMOD.PlayOneShot(GlobalAssets.GetSound("Battery_explode", false), position, 1f);
				this.lastExplosionPosition = position;
				return;
			}
		}
	}

	// Token: 0x060047A8 RID: 18344 RVA: 0x0019D264 File Offset: 0x0019B464
	private void PlayImpactSound(Vector3 pos)
	{
		if (this.impactSound == null)
		{
			this.impactSound = "Meteor_Large_Impact";
		}
		this.loopingSounds.StopSound(this.flyingSound);
		string sound = GlobalAssets.GetSound(this.impactSound, false);
		int num = Grid.PosToCell(pos);
		if (Grid.IsValidCell(num) && (int)Grid.WorldIdx[num] == ClusterManager.Instance.activeWorldId)
		{
			float volume = this.GetVolume(base.gameObject);
			pos.z = 0f;
			EventInstance eventInstance = KFMOD.BeginOneShot(sound, pos, volume);
			eventInstance.setParameterByName("userVolume_SFX", KPlayerPrefs.GetFloat("Volume_SFX"), false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x060047A9 RID: 18345 RVA: 0x0019D308 File Offset: 0x0019B508
	public bool DestroyCell(int cell)
	{
		bool flag = false;
		ListPool<GameObject, LargeComet>.PooledList pooledList = ListPool<GameObject, LargeComet>.Allocate();
		GameObject gameObject = Grid.Objects[cell, 1];
		flag = gameObject != null;
		pooledList.Add(gameObject);
		pooledList.Add(Grid.Objects[cell, 2]);
		pooledList.Add(Grid.Objects[cell, 12]);
		pooledList.Add(Grid.Objects[cell, 15]);
		pooledList.Add(Grid.Objects[cell, 16]);
		pooledList.Add(Grid.Objects[cell, 19]);
		pooledList.Add(Grid.Objects[cell, 20]);
		pooledList.Add(Grid.Objects[cell, 23]);
		pooledList.Add(Grid.Objects[cell, 26]);
		pooledList.Add(Grid.Objects[cell, 29]);
		pooledList.Add(Grid.Objects[cell, 31]);
		pooledList.Add(Grid.Objects[cell, 30]);
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			if (Grid.PosToCell(minionIdentity) == cell)
			{
				pooledList.Add(minionIdentity.gameObject);
				SaveGame.Instance.ColonyAchievementTracker.deadDupeCounter++;
			}
		}
		foreach (GameObject gameObject2 in pooledList)
		{
			if (gameObject2 != null)
			{
				global::Util.KDestroyGameObject(gameObject2);
			}
		}
		this.ClearCellPickupables(cell);
		Element element = ElementLoader.elements[(int)Grid.ElementIdx[cell]];
		if (element.id == SimHashes.Void)
		{
			SimMessages.ReplaceElement(cell, SimHashes.Void, CellEventLogger.Instance.DebugTool, 0f, 0f, byte.MaxValue, 0, -1);
		}
		else
		{
			SimMessages.ReplaceElement(cell, SimHashes.Vacuum, CellEventLogger.Instance.DebugTool, 0f, 0f, byte.MaxValue, 0, -1);
		}
		flag = flag || element.IsSolid;
		pooledList.Recycle();
		return flag;
	}

	// Token: 0x060047AA RID: 18346 RVA: 0x0019D554 File Offset: 0x0019B754
	public void ClearCellPickupables(int cell)
	{
		GameObject gameObject = Grid.Objects[cell, 3];
		if (gameObject != null)
		{
			ObjectLayerListItem objectLayerListItem = gameObject.GetComponent<Pickupable>().objectLayerListItem;
			while (objectLayerListItem != null)
			{
				GameObject gameObject2 = objectLayerListItem.gameObject;
				objectLayerListItem = objectLayerListItem.nextItem;
				if (!(gameObject2 == null))
				{
					global::Util.KDestroyGameObject(gameObject2);
				}
			}
		}
	}

	// Token: 0x060047AB RID: 18347 RVA: 0x0019D5A8 File Offset: 0x0019B7A8
	private void InitializeMaterial()
	{
		this.largeCometMaterial = new Material(Shader.Find("Klei/DLC4/LargeImpactorCometShader"));
		this.largeCometTexture = Assets.GetSprite("Demolior_final_broken");
		this.explosionTexture = Assets.GetSprite("contact_explode_fx_animationSheet");
		for (int i = 0; i < 30; i++)
		{
			this.ShaderExplosions[i] = Vector4.one * -1f;
			this.ShaderExplosions[i].w = (this.minSeparationBetweenExplosions - 1f) * 2f;
		}
	}

	// Token: 0x060047AC RID: 18348 RVA: 0x0019D640 File Offset: 0x0019B840
	private Material DrawComet(RenderTexture source)
	{
		this.largeCometMaterial.SetTexture("_CometTex", this.largeCometTexture.texture);
		this.largeCometMaterial.SetTexture("_ExplosionTex", this.explosionTexture.texture);
		this.largeCometMaterial.SetVector("_CometWorldPosition", this.VisualPositionCentredImage);
		this.largeCometMaterial.SetFloat("_LandingProgress", this.LandingProgress);
		this.largeCometMaterial.SetFloat("_CometWidth", (float)this.templateWidth);
		this.largeCometMaterial.SetFloat("_CometRatio", (float)this.largeCometTexture.texture.height / (float)this.largeCometTexture.texture.width);
		this.largeCometMaterial.SetFloat("_UnscaledTime", Time.unscaledTime);
		this.largeCometMaterial.SetVectorArray("_ExplosionLocations", this.ShaderExplosions);
		return this.largeCometMaterial;
	}

	// Token: 0x060047AD RID: 18349 RVA: 0x0019D72F File Offset: 0x0019B92F
	private void StartLoopingSound()
	{
		this.loopingSounds.StartSound(this.flyingSound);
		this.loopingSounds.UpdateFirstParameter(this.flyingSound, LargeComet.FLYING_SOUND_ID_PARAMETER, (float)this.flyingSoundID);
	}

	// Token: 0x04002F49 RID: 12105
	private static HashedString FLYING_SOUND_ID_PARAMETER = "meteorType";

	// Token: 0x04002F4A RID: 12106
	public string impactSound;

	// Token: 0x04002F4B RID: 12107
	public string flyingSound;

	// Token: 0x04002F4C RID: 12108
	public int flyingSoundID;

	// Token: 0x04002F4E RID: 12110
	public List<KeyValuePair<string, string>> additionalAnimFiles = new List<KeyValuePair<string, string>>();

	// Token: 0x04002F4F RID: 12111
	public KeyValuePair<string, string> mainAnimFile;

	// Token: 0x04002F50 RID: 12112
	public bool affectedByDifficulty = true;

	// Token: 0x04002F51 RID: 12113
	public bool destroyOnExplode = true;

	// Token: 0x04002F52 RID: 12114
	public bool spawnWithOffset;

	// Token: 0x04002F53 RID: 12115
	public Vector2I stampLocation;

	// Token: 0x04002F54 RID: 12116
	public Vector2I crashPosition;

	// Token: 0x04002F55 RID: 12117
	public Dictionary<int, CellOffset> bottomCellsOffsetOfTemplate;

	// Token: 0x04002F56 RID: 12118
	public TemplateContainer asteroidTemplate;

	// Token: 0x04002F57 RID: 12119
	public Ref<KPrefabID> ignoreObstacleForDamage = new Ref<KPrefabID>();

	// Token: 0x04002F58 RID: 12120
	private bool hasExploded;

	// Token: 0x04002F59 RID: 12121
	private float age;

	// Token: 0x04002F5A RID: 12122
	private int lowestTemplateYLocalPosition;

	// Token: 0x04002F5B RID: 12123
	private int templateWidth;

	// Token: 0x04002F5C RID: 12124
	private int worldID;

	// Token: 0x04002F5D RID: 12125
	private Vector3 previousVisualPosition;

	// Token: 0x04002F5E RID: 12126
	private Vector3 initialPosition;

	// Token: 0x04002F5F RID: 12127
	private Vector2I prevCell;

	// Token: 0x04002F60 RID: 12128
	public global::System.Action OnImpact;

	// Token: 0x04002F61 RID: 12129
	[Serialize]
	protected Vector3 offsetPosition;

	// Token: 0x04002F62 RID: 12130
	[Serialize]
	protected Vector2 velocity;

	// Token: 0x04002F63 RID: 12131
	[MyCmpGet]
	private KBatchedAnimController anim;

	// Token: 0x04002F64 RID: 12132
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04002F65 RID: 12133
	private LoopingSounds loopingSounds;

	// Token: 0x04002F66 RID: 12134
	private KBatchedAnimController[] child_controllers;

	// Token: 0x04002F67 RID: 12135
	private List<KAnimControllerBase> additionalAnimControllers = new List<KAnimControllerBase>();

	// Token: 0x04002F68 RID: 12136
	private KBatchedAnimController mainChildrenAnimController;

	// Token: 0x04002F69 RID: 12137
	private Vector2I fromStampToCrashPosition;

	// Token: 0x04002F6A RID: 12138
	private HashSet<int> cellsCentrePassedThrough = new HashSet<int>();

	// Token: 0x04002F6B RID: 12139
	private Vector3 activeExplosionPosition = Vector3.zero;

	// Token: 0x04002F6C RID: 12140
	private Material largeCometMaterial;

	// Token: 0x04002F6D RID: 12141
	private Sprite largeCometTexture;

	// Token: 0x04002F6E RID: 12142
	private Sprite explosionTexture;

	// Token: 0x04002F6F RID: 12143
	private float minSeparationBetweenExplosions = 8f;

	// Token: 0x04002F70 RID: 12144
	private Vector3 lastExplosionPosition;

	// Token: 0x04002F71 RID: 12145
	private const string LARGE_COMET_SHADER_NAME = "Klei/DLC4/LargeImpactorCometShader";

	// Token: 0x04002F72 RID: 12146
	private const int MAX_SHADER_EXPLOSION_COUNT = 30;

	// Token: 0x04002F73 RID: 12147
	private const float EXPLOSION_ANIMATION_FRAME_COUNT = 37f;

	// Token: 0x04002F74 RID: 12148
	private const float EXPLOSION_ANIMATION_DURATION = 1.2333333f;

	// Token: 0x04002F75 RID: 12149
	private Vector4[] ShaderExplosions = new Vector4[30];
}
