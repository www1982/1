using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000525 RID: 1317
public abstract class KAnimControllerBase : MonoBehaviour, ISerializationCallbackReceiver
{
	// Token: 0x06001C3C RID: 7228 RVA: 0x00099A8C File Offset: 0x00097C8C
	protected KAnimControllerBase()
	{
		this.previousFrame = -1;
		this.currentFrame = -1;
		this.PlaySpeedMultiplier = 1f;
		this.synchronizer = new KAnimSynchronizer(this);
		this.layering = new KAnimLayering(this, this.fgLayer);
		this.isVisible = true;
	}

	// Token: 0x06001C3D RID: 7229
	public abstract KAnim.Anim GetAnim(int index);

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06001C3E RID: 7230 RVA: 0x00099B95 File Offset: 0x00097D95
	// (set) Token: 0x06001C3F RID: 7231 RVA: 0x00099B9D File Offset: 0x00097D9D
	public string debugName { get; private set; }

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00099BA6 File Offset: 0x00097DA6
	// (set) Token: 0x06001C41 RID: 7233 RVA: 0x00099BAE File Offset: 0x00097DAE
	public KAnim.Build curBuild { get; protected set; }

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06001C42 RID: 7234 RVA: 0x00099BB8 File Offset: 0x00097DB8
	// (remove) Token: 0x06001C43 RID: 7235 RVA: 0x00099BF0 File Offset: 0x00097DF0
	public event Action<Color32> OnOverlayColourChanged;

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06001C44 RID: 7236 RVA: 0x00099C25 File Offset: 0x00097E25
	// (set) Token: 0x06001C45 RID: 7237 RVA: 0x00099C2D File Offset: 0x00097E2D
	public new bool enabled
	{
		get
		{
			return this._enabled;
		}
		set
		{
			this._enabled = value;
			if (!this.hasAwakeRun)
			{
				return;
			}
			if (this._enabled)
			{
				this.Enable();
				return;
			}
			this.Disable();
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00099C54 File Offset: 0x00097E54
	public bool HasBatchInstanceData
	{
		get
		{
			return this.batchInstanceData != null;
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00099C5F File Offset: 0x00097E5F
	// (set) Token: 0x06001C48 RID: 7240 RVA: 0x00099C67 File Offset: 0x00097E67
	public SymbolInstanceGpuData symbolInstanceGpuData { get; protected set; }

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00099C70 File Offset: 0x00097E70
	// (set) Token: 0x06001C4A RID: 7242 RVA: 0x00099C78 File Offset: 0x00097E78
	public SymbolOverrideInfoGpuData symbolOverrideInfoGpuData { get; protected set; }

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00099C81 File Offset: 0x00097E81
	// (set) Token: 0x06001C4C RID: 7244 RVA: 0x00099C94 File Offset: 0x00097E94
	public Color32 TintColour
	{
		get
		{
			return this.batchInstanceData.GetTintColour();
		}
		set
		{
			if (this.batchInstanceData != null && this.batchInstanceData.SetTintColour(value))
			{
				this.SetDirty();
				this.SuspendUpdates(false);
				if (this.OnTintChanged != null)
				{
					this.OnTintChanged(value);
				}
			}
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00099CE2 File Offset: 0x00097EE2
	// (set) Token: 0x06001C4E RID: 7246 RVA: 0x00099CF4 File Offset: 0x00097EF4
	public Color32 HighlightColour
	{
		get
		{
			return this.batchInstanceData.GetHighlightcolour();
		}
		set
		{
			if (this.batchInstanceData.SetHighlightColour(value))
			{
				this.SetDirty();
				this.SuspendUpdates(false);
				if (this.OnHighlightChanged != null)
				{
					this.OnHighlightChanged(value);
				}
			}
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00099D2F File Offset: 0x00097F2F
	// (set) Token: 0x06001C50 RID: 7248 RVA: 0x00099D3C File Offset: 0x00097F3C
	public Color OverlayColour
	{
		get
		{
			return this.batchInstanceData.GetOverlayColour();
		}
		set
		{
			if (this.batchInstanceData.SetOverlayColour(value))
			{
				this.SetDirty();
				this.SuspendUpdates(false);
				if (this.OnOverlayColourChanged != null)
				{
					this.OnOverlayColourChanged(value);
				}
			}
		}
	}

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06001C51 RID: 7249 RVA: 0x00099D74 File Offset: 0x00097F74
	// (remove) Token: 0x06001C52 RID: 7250 RVA: 0x00099DAC File Offset: 0x00097FAC
	public event KAnimControllerBase.KAnimEvent onAnimEnter;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06001C53 RID: 7251 RVA: 0x00099DE4 File Offset: 0x00097FE4
	// (remove) Token: 0x06001C54 RID: 7252 RVA: 0x00099E1C File Offset: 0x0009801C
	public event KAnimControllerBase.KAnimEvent onAnimComplete;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06001C55 RID: 7253 RVA: 0x00099E54 File Offset: 0x00098054
	// (remove) Token: 0x06001C56 RID: 7254 RVA: 0x00099E8C File Offset: 0x0009808C
	public event Action<int> onLayerChanged;

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00099EC1 File Offset: 0x000980C1
	// (set) Token: 0x06001C58 RID: 7256 RVA: 0x00099EC9 File Offset: 0x000980C9
	public int previousFrame { get; protected set; }

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00099ED2 File Offset: 0x000980D2
	// (set) Token: 0x06001C5A RID: 7258 RVA: 0x00099EDA File Offset: 0x000980DA
	public int currentFrame { get; protected set; }

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00099EE4 File Offset: 0x000980E4
	public HashedString currentAnim
	{
		get
		{
			if (this.curAnim == null)
			{
				return default(HashedString);
			}
			return this.curAnim.hash;
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00099F17 File Offset: 0x00098117
	// (set) Token: 0x06001C5C RID: 7260 RVA: 0x00099F0E File Offset: 0x0009810E
	public float PlaySpeedMultiplier { get; set; }

	// Token: 0x06001C5E RID: 7262 RVA: 0x00099F1F File Offset: 0x0009811F
	public void SetFGLayer(Grid.SceneLayer layer)
	{
		this.fgLayer = layer;
		this.GetLayering();
		if (this.layering != null)
		{
			this.layering.SetLayer(this.fgLayer);
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00099F48 File Offset: 0x00098148
	// (set) Token: 0x06001C60 RID: 7264 RVA: 0x00099F50 File Offset: 0x00098150
	public KAnim.PlayMode PlayMode
	{
		get
		{
			return this.mode;
		}
		set
		{
			this.mode = value;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00099F59 File Offset: 0x00098159
	// (set) Token: 0x06001C62 RID: 7266 RVA: 0x00099F61 File Offset: 0x00098161
	public bool FlipX
	{
		get
		{
			return this.flipX;
		}
		set
		{
			this.flipX = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.SetDirty();
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00099F83 File Offset: 0x00098183
	// (set) Token: 0x06001C64 RID: 7268 RVA: 0x00099F8B File Offset: 0x0009818B
	public bool FlipY
	{
		get
		{
			return this.flipY;
		}
		set
		{
			this.flipY = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.SetDirty();
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00099FAD File Offset: 0x000981AD
	// (set) Token: 0x06001C66 RID: 7270 RVA: 0x00099FB5 File Offset: 0x000981B5
	public Vector3 Offset
	{
		get
		{
			return this.offset;
		}
		set
		{
			this.offset = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.DeRegister();
			this.Register();
			this.RefreshVisibilityListener();
			this.SetDirty();
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00099FE9 File Offset: 0x000981E9
	// (set) Token: 0x06001C68 RID: 7272 RVA: 0x00099FF1 File Offset: 0x000981F1
	public float Rotation
	{
		get
		{
			return this.rotation;
		}
		set
		{
			this.rotation = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.SetDirty();
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0009A013 File Offset: 0x00098213
	// (set) Token: 0x06001C6A RID: 7274 RVA: 0x0009A01B File Offset: 0x0009821B
	public Vector3 Pivot
	{
		get
		{
			return this.pivot;
		}
		set
		{
			this.pivot = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.SetDirty();
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0009A03D File Offset: 0x0009823D
	public Vector3 PositionIncludingOffset
	{
		get
		{
			return base.transform.GetPosition() + this.Offset;
		}
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x0009A055 File Offset: 0x00098255
	public KAnimBatchGroup.MaterialType GetMaterialType()
	{
		return this.materialType;
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x0009A060 File Offset: 0x00098260
	public Vector3 GetWorldPivot()
	{
		Vector3 position = base.transform.GetPosition();
		KBoxCollider2D component = base.GetComponent<KBoxCollider2D>();
		if (component != null)
		{
			position.x += component.offset.x;
			position.y += component.offset.y - component.size.y / 2f;
		}
		return position;
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x0009A0C8 File Offset: 0x000982C8
	public KAnim.Anim GetCurrentAnim()
	{
		return this.curAnim;
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x0009A0D0 File Offset: 0x000982D0
	public KAnimHashedString GetBuildHash()
	{
		if (this.curBuild == null)
		{
			return KAnimBatchManager.NO_BATCH;
		}
		return this.curBuild.fileHash;
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x0009A0F0 File Offset: 0x000982F0
	protected float GetDuration()
	{
		if (this.curAnim != null)
		{
			return (float)this.curAnim.numFrames / this.curAnim.frameRate;
		}
		return 0f;
	}

	// Token: 0x06001C71 RID: 7281 RVA: 0x0009A118 File Offset: 0x00098318
	protected int GetFrameIdxFromOffset(int offset)
	{
		int num = -1;
		if (this.curAnim != null)
		{
			num = offset + this.curAnim.firstFrameIdx;
		}
		return num;
	}

	// Token: 0x06001C72 RID: 7282 RVA: 0x0009A140 File Offset: 0x00098340
	public int GetFrameIdx(float time, bool absolute)
	{
		int num = -1;
		if (this.curAnim != null)
		{
			num = this.curAnim.GetFrameIdx(this.mode, time) + (absolute ? this.curAnim.firstFrameIdx : 0);
		}
		return num;
	}

	// Token: 0x06001C73 RID: 7283 RVA: 0x0009A17D File Offset: 0x0009837D
	public bool IsStopped()
	{
		return this.stopped;
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0009A185 File Offset: 0x00098385
	public KAnim.Anim CurrentAnim
	{
		get
		{
			return this.curAnim;
		}
	}

	// Token: 0x06001C75 RID: 7285 RVA: 0x0009A18D File Offset: 0x0009838D
	public KAnimSynchronizer GetSynchronizer()
	{
		return this.synchronizer;
	}

	// Token: 0x06001C76 RID: 7286 RVA: 0x0009A195 File Offset: 0x00098395
	public KAnimLayering GetLayering()
	{
		if (this.layering == null && this.fgLayer != Grid.SceneLayer.NoLayer)
		{
			this.layering = new KAnimLayering(this, this.fgLayer);
		}
		return this.layering;
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x0009A1C1 File Offset: 0x000983C1
	public KAnim.PlayMode GetMode()
	{
		return this.mode;
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x0009A1C9 File Offset: 0x000983C9
	public static string GetModeString(KAnim.PlayMode mode)
	{
		switch (mode)
		{
		case KAnim.PlayMode.Loop:
			return "Loop";
		case KAnim.PlayMode.Once:
			return "Once";
		case KAnim.PlayMode.Paused:
			return "Paused";
		default:
			return "Unknown";
		}
	}

	// Token: 0x06001C79 RID: 7289 RVA: 0x0009A1F6 File Offset: 0x000983F6
	public float GetPlaySpeed()
	{
		return this.playSpeed;
	}

	// Token: 0x06001C7A RID: 7290 RVA: 0x0009A1FE File Offset: 0x000983FE
	public void SetElapsedTime(float value)
	{
		this.elapsedTime = value;
	}

	// Token: 0x06001C7B RID: 7291 RVA: 0x0009A207 File Offset: 0x00098407
	public float GetElapsedTime()
	{
		return this.elapsedTime;
	}

	// Token: 0x06001C7C RID: 7292
	protected abstract void SuspendUpdates(bool suspend);

	// Token: 0x06001C7D RID: 7293
	protected abstract void OnStartQueuedAnim();

	// Token: 0x06001C7E RID: 7294
	public abstract void SetDirty();

	// Token: 0x06001C7F RID: 7295
	protected abstract void RefreshVisibilityListener();

	// Token: 0x06001C80 RID: 7296
	protected abstract void DeRegister();

	// Token: 0x06001C81 RID: 7297
	protected abstract void Register();

	// Token: 0x06001C82 RID: 7298
	protected abstract void OnAwake();

	// Token: 0x06001C83 RID: 7299
	protected abstract void OnStart();

	// Token: 0x06001C84 RID: 7300
	protected abstract void OnStop();

	// Token: 0x06001C85 RID: 7301
	protected abstract void Enable();

	// Token: 0x06001C86 RID: 7302
	protected abstract void Disable();

	// Token: 0x06001C87 RID: 7303
	protected abstract void UpdateFrame(float t);

	// Token: 0x06001C88 RID: 7304
	public abstract Matrix2x3 GetTransformMatrix();

	// Token: 0x06001C89 RID: 7305
	public abstract Matrix2x3 GetSymbolLocalTransform(HashedString symbol, out bool symbolVisible);

	// Token: 0x06001C8A RID: 7306
	public abstract void UpdateAllHiddenSymbols();

	// Token: 0x06001C8B RID: 7307
	public abstract void UpdateHiddenSymbol(KAnimHashedString specificSymbol);

	// Token: 0x06001C8C RID: 7308
	public abstract void UpdateHiddenSymbolSet(HashSet<KAnimHashedString> specificSymbols);

	// Token: 0x06001C8D RID: 7309
	public abstract void TriggerStop();

	// Token: 0x06001C8E RID: 7310 RVA: 0x0009A20F File Offset: 0x0009840F
	public virtual void SetLayer(int layer)
	{
		if (this.onLayerChanged != null)
		{
			this.onLayerChanged(layer);
		}
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x0009A228 File Offset: 0x00098428
	public Vector3 GetPivotSymbolPosition()
	{
		bool flag = false;
		Matrix4x4 symbolTransform = this.GetSymbolTransform(KAnimControllerBase.snaptoPivot, out flag);
		Vector3 position = base.transform.GetPosition();
		if (flag)
		{
			position = new Vector3(symbolTransform[0, 3], symbolTransform[1, 3], symbolTransform[2, 3]);
		}
		return position;
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x0009A277 File Offset: 0x00098477
	public virtual Matrix4x4 GetSymbolTransform(HashedString symbol, out bool symbolVisible)
	{
		symbolVisible = false;
		return Matrix4x4.identity;
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x0009A284 File Offset: 0x00098484
	private void Awake()
	{
		this.aem = Singleton<AnimEventManager>.Instance;
		this.debugName = base.name;
		this.SetFGLayer(this.fgLayer);
		this.OnAwake();
		if (!string.IsNullOrEmpty(this.initialAnim))
		{
			this.SetDirty();
			this.Play(this.initialAnim, this.initialMode, 1f, 0f);
		}
		this.hasAwakeRun = true;
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x0009A2F5 File Offset: 0x000984F5
	private void Start()
	{
		this.OnStart();
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x0009A300 File Offset: 0x00098500
	protected virtual void OnDestroy()
	{
		this.animFiles = null;
		this.curAnim = null;
		this.curBuild = null;
		this.synchronizer = null;
		this.layering = null;
		this.animQueue = null;
		this.overrideAnims = null;
		this.anims = null;
		this.synchronizer = null;
		this.layering = null;
		this.overrideAnimFiles = null;
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x0009A35A File Offset: 0x0009855A
	protected void AnimEnter(HashedString hashed_name)
	{
		if (this.onAnimEnter != null)
		{
			this.onAnimEnter(hashed_name);
		}
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x0009A370 File Offset: 0x00098570
	public void Play(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		if (!this.stopped)
		{
			this.Stop();
		}
		this.Queue(anim_name, mode, speed, time_offset);
	}

	// Token: 0x06001C96 RID: 7318 RVA: 0x0009A38C File Offset: 0x0009858C
	public void Play(HashedString[] anim_names, KAnim.PlayMode mode = KAnim.PlayMode.Once)
	{
		if (!this.stopped)
		{
			this.Stop();
		}
		for (int i = 0; i < anim_names.Length - 1; i++)
		{
			this.Queue(anim_names[i], KAnim.PlayMode.Once, 1f, 0f);
		}
		global::Debug.Assert(anim_names.Length != 0, "Play was called with an empty anim array");
		this.Queue(anim_names[anim_names.Length - 1], mode, 1f, 0f);
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x0009A3FC File Offset: 0x000985FC
	public void Queue(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		this.animQueue.Enqueue(new KAnimControllerBase.AnimData
		{
			anim = anim_name,
			mode = mode,
			speed = speed,
			timeOffset = time_offset
		});
		this.mode = ((mode == KAnim.PlayMode.Paused) ? KAnim.PlayMode.Paused : KAnim.PlayMode.Once);
		if (this.aem != null)
		{
			this.aem.SetMode(this.eventManagerHandle, this.mode);
		}
		if (this.animQueue.Count == 1 && this.stopped)
		{
			this.StartQueuedAnim();
		}
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x0009A487 File Offset: 0x00098687
	public void QueueAndSyncTransition(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		this.SyncTransition();
		this.Queue(anim_name, mode, speed, time_offset);
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x0009A49A File Offset: 0x0009869A
	public void SyncTransition()
	{
		this.elapsedTime %= Mathf.Max(float.Epsilon, this.GetDuration());
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x0009A4B9 File Offset: 0x000986B9
	public void ClearQueue()
	{
		this.animQueue.Clear();
	}

	// Token: 0x06001C9B RID: 7323 RVA: 0x0009A4C8 File Offset: 0x000986C8
	private void Restart(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		if (this.curBuild == null)
		{
			string[] array = new string[5];
			array[0] = "[";
			array[1] = base.gameObject.name;
			array[2] = "] Missing build while trying to play anim [";
			int num = 3;
			HashedString hashedString = anim_name;
			array[num] = hashedString.ToString();
			array[4] = "]";
			global::Debug.LogWarning(string.Concat(array), base.gameObject);
			return;
		}
		Queue<KAnimControllerBase.AnimData> queue = new Queue<KAnimControllerBase.AnimData>();
		queue.Enqueue(new KAnimControllerBase.AnimData
		{
			anim = anim_name,
			mode = mode,
			speed = speed,
			timeOffset = time_offset
		});
		while (this.animQueue.Count > 0)
		{
			queue.Enqueue(this.animQueue.Dequeue());
		}
		this.animQueue = queue;
		if (this.animQueue.Count == 1 && this.stopped)
		{
			this.StartQueuedAnim();
		}
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x0009A5A8 File Offset: 0x000987A8
	protected void StartQueuedAnim()
	{
		this.StopAnimEventSequence();
		this.previousFrame = -1;
		this.currentFrame = -1;
		this.SuspendUpdates(false);
		this.stopped = false;
		this.OnStartQueuedAnim();
		KAnimControllerBase.AnimData animData = this.animQueue.Dequeue();
		while (animData.mode == KAnim.PlayMode.Loop && this.animQueue.Count > 0)
		{
			animData = this.animQueue.Dequeue();
		}
		KAnimControllerBase.AnimLookupData animLookupData;
		if (this.overrideAnims == null || !this.overrideAnims.TryGetValue(animData.anim, out animLookupData))
		{
			if (!this.anims.TryGetValue(animData.anim, out animLookupData))
			{
				bool flag = true;
				if (this.showWhenMissing != null)
				{
					this.showWhenMissing.SetActive(true);
				}
				if (flag)
				{
					this.TriggerStop();
					return;
				}
			}
			else if (this.showWhenMissing != null)
			{
				this.showWhenMissing.SetActive(false);
			}
		}
		this.curAnim = this.GetAnim(animLookupData.animIndex);
		int num = 0;
		if (animData.mode == KAnim.PlayMode.Loop && this.randomiseLoopedOffset)
		{
			num = global::UnityEngine.Random.Range(0, this.curAnim.numFrames - 1);
		}
		this.prevAnimFrame = -1;
		this.curAnimFrameIdx = this.GetFrameIdxFromOffset(num);
		this.currentFrame = this.curAnimFrameIdx;
		this.mode = animData.mode;
		this.playSpeed = animData.speed * this.PlaySpeedMultiplier;
		this.SetElapsedTime((float)num / this.curAnim.frameRate + animData.timeOffset);
		this.synchronizer.Sync();
		this.StartAnimEventSequence();
		this.AnimEnter(animData.anim);
	}

	// Token: 0x06001C9D RID: 7325 RVA: 0x0009A72C File Offset: 0x0009892C
	public bool GetSymbolVisiblity(KAnimHashedString symbol)
	{
		return !this.hiddenSymbolsSet.Contains(symbol);
	}

	// Token: 0x06001C9E RID: 7326 RVA: 0x0009A73D File Offset: 0x0009893D
	public void SetSymbolVisiblity(KAnimHashedString symbol, bool is_visible)
	{
		if (is_visible)
		{
			this.hiddenSymbolsSet.Remove(symbol);
		}
		else if (!this.hiddenSymbolsSet.Contains(symbol))
		{
			this.hiddenSymbolsSet.Add(symbol);
		}
		if (this.curBuild != null)
		{
			this.UpdateHiddenSymbol(symbol);
		}
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x0009A77C File Offset: 0x0009897C
	public void BatchSetSymbolsVisiblity(HashSet<KAnimHashedString> symbols, bool is_visible)
	{
		foreach (KAnimHashedString kanimHashedString in symbols)
		{
			if (is_visible)
			{
				this.hiddenSymbolsSet.Remove(kanimHashedString);
			}
			else if (!this.hiddenSymbolsSet.Contains(kanimHashedString))
			{
				this.hiddenSymbolsSet.Add(kanimHashedString);
			}
		}
		if (this.curBuild != null)
		{
			this.UpdateHiddenSymbolSet(symbols);
		}
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x0009A800 File Offset: 0x00098A00
	public void AddAnimOverrides(KAnimFile kanim_file, float priority = 0f)
	{
		if (kanim_file == null)
		{
			global::Debug.LogError(string.Format("AddAnimOverrides tried to add a null override to {0} at position {1}", base.gameObject.name, base.transform.position));
		}
		if (kanim_file.GetData().build != null && kanim_file.GetData().build.symbols.Length != 0)
		{
			SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
			DebugUtil.Assert(component != null, "Anim overrides containing additional symbols require a symbol override controller.");
			component.AddBuildOverride(kanim_file.GetData(), 0);
		}
		this.overrideAnimFiles.Add(new KAnimControllerBase.OverrideAnimFileData
		{
			priority = priority,
			file = kanim_file
		});
		this.overrideAnimFiles.Sort((KAnimControllerBase.OverrideAnimFileData a, KAnimControllerBase.OverrideAnimFileData b) => b.priority.CompareTo(a.priority));
		this.RebuildOverrides(kanim_file);
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x0009A8D8 File Offset: 0x00098AD8
	public void RemoveAnimOverrides(KAnimFile kanim_file)
	{
		if (kanim_file == null)
		{
			global::Debug.LogError(string.Format("RemoveAnimOverrides tried to remove a null override to {0} at position {1}", base.gameObject.name, base.transform.position));
		}
		if (kanim_file.GetData().build != null && kanim_file.GetData().build.symbols.Length != 0)
		{
			SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
			DebugUtil.Assert(component != null, "Anim overrides containing additional symbols require a symbol override controller.");
			component.TryRemoveBuildOverride(kanim_file.GetData(), 0);
		}
		for (int i = 0; i < this.overrideAnimFiles.Count; i++)
		{
			if (this.overrideAnimFiles[i].file == kanim_file)
			{
				this.overrideAnimFiles.RemoveAt(i);
				break;
			}
		}
		this.RebuildOverrides(kanim_file);
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x0009A9A0 File Offset: 0x00098BA0
	private void RebuildOverrides(KAnimFile kanim_file)
	{
		bool flag = false;
		this.overrideAnims.Clear();
		for (int i = 0; i < this.overrideAnimFiles.Count; i++)
		{
			KAnimControllerBase.OverrideAnimFileData overrideAnimFileData = this.overrideAnimFiles[i];
			KAnimFileData data = overrideAnimFileData.file.GetData();
			for (int j = 0; j < data.animCount; j++)
			{
				KAnim.Anim anim = data.GetAnim(j);
				if (anim.animFile.hashName != data.hashName)
				{
					global::Debug.LogError(string.Format("How did we get an anim from another file? [{0}] != [{1}] for anim [{2}]", data.name, anim.animFile.name, j));
				}
				KAnimControllerBase.AnimLookupData animLookupData = default(KAnimControllerBase.AnimLookupData);
				animLookupData.animIndex = anim.index;
				HashedString hashedString = new HashedString(anim.name);
				if (!this.overrideAnims.ContainsKey(hashedString))
				{
					this.overrideAnims[hashedString] = animLookupData;
				}
				if (this.curAnim != null && this.curAnim.hash == hashedString && overrideAnimFileData.file == kanim_file)
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			this.Restart(this.curAnim.name, this.mode, this.playSpeed, 0f);
		}
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x0009AAF0 File Offset: 0x00098CF0
	public bool HasAnimation(HashedString anim_name)
	{
		bool flag = anim_name.IsValid;
		if (flag)
		{
			bool flag2 = this.anims.ContainsKey(anim_name);
			bool flag3 = !flag2 && this.overrideAnims.ContainsKey(anim_name);
			flag = flag2 || flag3;
		}
		return flag;
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x0009AB2C File Offset: 0x00098D2C
	public bool HasAnimationFile(KAnimHashedString anim_file_name)
	{
		KAnimFile kanimFile = null;
		return this.TryGetAnimationFile(anim_file_name, out kanimFile);
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x0009AB44 File Offset: 0x00098D44
	public bool TryGetAnimationFile(KAnimHashedString anim_file_name, out KAnimFile match)
	{
		match = null;
		if (!anim_file_name.IsValid())
		{
			return false;
		}
		KAnimFileData kanimFileData = null;
		int num = 0;
		int num2 = this.overrideAnimFiles.Count - 1;
		int num3 = (int)((float)this.overrideAnimFiles.Count * 0.5f);
		while (num3 > 0 && match == null && num < num3)
		{
			if (this.overrideAnimFiles[num].file != null)
			{
				kanimFileData = this.overrideAnimFiles[num].file.GetData();
			}
			if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
			{
				match = this.overrideAnimFiles[num].file;
				break;
			}
			if (this.overrideAnimFiles[num2].file != null)
			{
				kanimFileData = this.overrideAnimFiles[num2].file.GetData();
			}
			if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
			{
				match = this.overrideAnimFiles[num2].file;
			}
			num++;
			num2--;
		}
		if (match == null && this.overrideAnimFiles.Count % 2 != 0)
		{
			if (this.overrideAnimFiles[num].file != null)
			{
				kanimFileData = this.overrideAnimFiles[num].file.GetData();
			}
			if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
			{
				match = this.overrideAnimFiles[num].file;
			}
		}
		kanimFileData = null;
		if (match == null && this.animFiles != null)
		{
			num = 0;
			num2 = this.animFiles.Length - 1;
			num3 = (int)((float)this.animFiles.Length * 0.5f);
			while (num3 > 0 && match == null && num < num3)
			{
				if (this.animFiles[num] != null)
				{
					kanimFileData = this.animFiles[num].GetData();
				}
				if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
				{
					match = this.animFiles[num];
					break;
				}
				if (this.animFiles[num2] != null)
				{
					kanimFileData = this.animFiles[num2].GetData();
				}
				if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
				{
					match = this.animFiles[num2];
				}
				num++;
				num2--;
			}
			if (match == null && this.animFiles.Length % 2 != 0)
			{
				if (this.animFiles[num] != null)
				{
					kanimFileData = this.animFiles[num].GetData();
				}
				if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
				{
					match = this.animFiles[num];
				}
			}
		}
		return match != null;
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x0009AE20 File Offset: 0x00099020
	public void AddAnims(KAnimFile anim_file)
	{
		KAnimFileData data = anim_file.GetData();
		if (data == null)
		{
			global::Debug.LogError("AddAnims() Null animfile data");
			return;
		}
		this.maxSymbols = Mathf.Max(this.maxSymbols, data.maxVisSymbolFrames);
		for (int i = 0; i < data.animCount; i++)
		{
			KAnim.Anim anim = data.GetAnim(i);
			if (anim.animFile.hashName != data.hashName)
			{
				global::Debug.LogErrorFormat("How did we get an anim from another file? [{0}] != [{1}] for anim [{2}]", new object[]
				{
					data.name,
					anim.animFile.name,
					i
				});
			}
			this.anims[anim.hash] = new KAnimControllerBase.AnimLookupData
			{
				animIndex = anim.index
			};
		}
		if (this.usingNewSymbolOverrideSystem && data.buildIndex != -1 && data.build.symbols != null && data.build.symbols.Length != 0)
		{
			base.GetComponent<SymbolOverrideController>().AddBuildOverride(anim_file.GetData(), -1);
		}
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x0009AF22 File Offset: 0x00099122
	// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x0009AF2C File Offset: 0x0009912C
	public KAnimFile[] AnimFiles
	{
		get
		{
			return this.animFiles;
		}
		set
		{
			DebugUtil.AssertArgs(value.Length != 0, new object[] { "Controller has no anim files.", base.gameObject });
			DebugUtil.AssertArgs(value[0] != null, new object[] { "First anim file needs to be non-null.", base.gameObject });
			DebugUtil.AssertArgs(value[0].IsBuildLoaded, new object[] { "First anim file needs to be the build file.", base.gameObject });
			for (int i = 0; i < value.Length; i++)
			{
				DebugUtil.AssertArgs(value[i] != null, new object[] { "Anim file is null", base.gameObject });
			}
			this.animFiles = new KAnimFile[value.Length];
			for (int j = 0; j < value.Length; j++)
			{
				this.animFiles[j] = value[j];
			}
		}
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x0009AFFD File Offset: 0x000991FD
	public IReadOnlyList<KAnimControllerBase.OverrideAnimFileData> OverrideAnimFiles
	{
		get
		{
			return this.overrideAnimFiles;
		}
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x0009B008 File Offset: 0x00099208
	public void Stop()
	{
		if (this.curAnim != null)
		{
			this.StopAnimEventSequence();
		}
		this.animQueue.Clear();
		this.stopped = true;
		if (this.onAnimComplete != null)
		{
			this.onAnimComplete((this.curAnim == null) ? HashedString.Invalid : this.curAnim.hash);
		}
		this.OnStop();
	}

	// Token: 0x06001CAB RID: 7339 RVA: 0x0009B068 File Offset: 0x00099268
	public void StopAndClear()
	{
		if (!this.stopped)
		{
			this.Stop();
		}
		this.bounds.center = Vector3.zero;
		this.bounds.extents = Vector3.zero;
		if (this.OnUpdateBounds != null)
		{
			this.OnUpdateBounds(this.bounds);
		}
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x0009B0BC File Offset: 0x000992BC
	public float GetPositionPercent()
	{
		return this.GetElapsedTime() / this.GetDuration();
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x0009B0CC File Offset: 0x000992CC
	public void SetPositionPercent(float percent)
	{
		if (this.curAnim == null)
		{
			return;
		}
		this.SetElapsedTime(percent * (float)this.curAnim.numFrames / this.curAnim.frameRate);
		int frameIdx = this.curAnim.GetFrameIdx(this.mode, this.elapsedTime);
		if (this.currentFrame != frameIdx)
		{
			this.SetDirty();
			this.UpdateAnimEventSequenceTime();
			this.SuspendUpdates(false);
		}
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x0009B138 File Offset: 0x00099338
	protected void StartAnimEventSequence()
	{
		if (!this.layering.GetIsForeground() && this.aem != null)
		{
			this.eventManagerHandle = this.aem.PlayAnim(this, this.curAnim, this.mode, this.elapsedTime, this.visibilityType == KAnimControllerBase.VisibilityType.Always);
		}
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x0009B187 File Offset: 0x00099387
	protected void UpdateAnimEventSequenceTime()
	{
		if (this.eventManagerHandle.IsValid() && this.aem != null)
		{
			this.aem.SetElapsedTime(this.eventManagerHandle, this.elapsedTime);
		}
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x0009B1B8 File Offset: 0x000993B8
	protected void StopAnimEventSequence()
	{
		if (this.eventManagerHandle.IsValid() && this.aem != null)
		{
			if (!this.stopped && this.mode != KAnim.PlayMode.Paused)
			{
				this.SetElapsedTime(this.aem.GetElapsedTime(this.eventManagerHandle));
			}
			this.aem.StopAnim(this.eventManagerHandle);
			this.eventManagerHandle = HandleVector<int>.InvalidHandle;
		}
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x0009B21E File Offset: 0x0009941E
	protected void DestroySelf()
	{
		if (this.onDestroySelf != null)
		{
			this.onDestroySelf(base.gameObject);
			return;
		}
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06001CB2 RID: 7346 RVA: 0x0009B245 File Offset: 0x00099445
	void ISerializationCallbackReceiver.OnBeforeSerialize()
	{
		this.hiddenSymbols.Clear();
		this.hiddenSymbols = new List<KAnimHashedString>(this.hiddenSymbolsSet);
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x0009B263 File Offset: 0x00099463
	void ISerializationCallbackReceiver.OnAfterDeserialize()
	{
		this.hiddenSymbolsSet = new HashSet<KAnimHashedString>(this.hiddenSymbols);
		this.hiddenSymbols.Clear();
	}

	// Token: 0x0400108D RID: 4237
	[NonSerialized]
	public GameObject showWhenMissing;

	// Token: 0x0400108E RID: 4238
	[SerializeField]
	public KAnimBatchGroup.MaterialType materialType;

	// Token: 0x0400108F RID: 4239
	[SerializeField]
	public string initialAnim;

	// Token: 0x04001090 RID: 4240
	[SerializeField]
	public KAnim.PlayMode initialMode = KAnim.PlayMode.Once;

	// Token: 0x04001091 RID: 4241
	[SerializeField]
	protected KAnimFile[] animFiles = new KAnimFile[0];

	// Token: 0x04001092 RID: 4242
	[SerializeField]
	protected Vector3 offset;

	// Token: 0x04001093 RID: 4243
	[SerializeField]
	protected Vector3 pivot;

	// Token: 0x04001094 RID: 4244
	[SerializeField]
	protected float rotation;

	// Token: 0x04001095 RID: 4245
	[SerializeField]
	public bool destroyOnAnimComplete;

	// Token: 0x04001096 RID: 4246
	[SerializeField]
	public bool inactiveDisable;

	// Token: 0x04001097 RID: 4247
	[SerializeField]
	protected bool flipX;

	// Token: 0x04001098 RID: 4248
	[SerializeField]
	protected bool flipY;

	// Token: 0x04001099 RID: 4249
	[SerializeField]
	public bool forceUseGameTime;

	// Token: 0x0400109A RID: 4250
	public string defaultAnim;

	// Token: 0x0400109C RID: 4252
	protected KAnim.Anim curAnim;

	// Token: 0x0400109D RID: 4253
	protected int curAnimFrameIdx = -1;

	// Token: 0x0400109E RID: 4254
	protected int prevAnimFrame = -1;

	// Token: 0x0400109F RID: 4255
	public bool usingNewSymbolOverrideSystem;

	// Token: 0x040010A1 RID: 4257
	protected HandleVector<int>.Handle eventManagerHandle = HandleVector<int>.InvalidHandle;

	// Token: 0x040010A2 RID: 4258
	protected List<KAnimControllerBase.OverrideAnimFileData> overrideAnimFiles = new List<KAnimControllerBase.OverrideAnimFileData>();

	// Token: 0x040010A3 RID: 4259
	protected DeepProfiler DeepProfiler = new DeepProfiler(false);

	// Token: 0x040010A4 RID: 4260
	public bool randomiseLoopedOffset;

	// Token: 0x040010A5 RID: 4261
	protected float elapsedTime;

	// Token: 0x040010A6 RID: 4262
	protected float playSpeed = 1f;

	// Token: 0x040010A7 RID: 4263
	protected KAnim.PlayMode mode = KAnim.PlayMode.Once;

	// Token: 0x040010A8 RID: 4264
	protected bool stopped = true;

	// Token: 0x040010A9 RID: 4265
	public float animHeight = 1f;

	// Token: 0x040010AA RID: 4266
	public float animWidth = 1f;

	// Token: 0x040010AB RID: 4267
	protected bool isVisible;

	// Token: 0x040010AC RID: 4268
	protected Bounds bounds;

	// Token: 0x040010AD RID: 4269
	public Action<Bounds> OnUpdateBounds;

	// Token: 0x040010AE RID: 4270
	public Action<Color> OnTintChanged;

	// Token: 0x040010AF RID: 4271
	public Action<Color> OnHighlightChanged;

	// Token: 0x040010B1 RID: 4273
	protected KAnimSynchronizer synchronizer;

	// Token: 0x040010B2 RID: 4274
	protected KAnimLayering layering;

	// Token: 0x040010B3 RID: 4275
	[SerializeField]
	protected bool _enabled = true;

	// Token: 0x040010B4 RID: 4276
	protected bool hasEnableRun;

	// Token: 0x040010B5 RID: 4277
	protected bool hasAwakeRun;

	// Token: 0x040010B6 RID: 4278
	protected KBatchedAnimInstanceData batchInstanceData;

	// Token: 0x040010B9 RID: 4281
	public KAnimControllerBase.VisibilityType visibilityType;

	// Token: 0x040010BD RID: 4285
	public Action<GameObject> onDestroySelf;

	// Token: 0x040010C0 RID: 4288
	[SerializeField]
	protected List<KAnimHashedString> hiddenSymbols = new List<KAnimHashedString>();

	// Token: 0x040010C1 RID: 4289
	[SerializeField]
	protected HashSet<KAnimHashedString> hiddenSymbolsSet = new HashSet<KAnimHashedString>();

	// Token: 0x040010C2 RID: 4290
	protected Dictionary<HashedString, KAnimControllerBase.AnimLookupData> anims = new Dictionary<HashedString, KAnimControllerBase.AnimLookupData>();

	// Token: 0x040010C3 RID: 4291
	protected Dictionary<HashedString, KAnimControllerBase.AnimLookupData> overrideAnims = new Dictionary<HashedString, KAnimControllerBase.AnimLookupData>();

	// Token: 0x040010C4 RID: 4292
	protected Queue<KAnimControllerBase.AnimData> animQueue = new Queue<KAnimControllerBase.AnimData>();

	// Token: 0x040010C5 RID: 4293
	protected int maxSymbols;

	// Token: 0x040010C7 RID: 4295
	public Grid.SceneLayer fgLayer = Grid.SceneLayer.NoLayer;

	// Token: 0x040010C8 RID: 4296
	protected AnimEventManager aem;

	// Token: 0x040010C9 RID: 4297
	private static HashedString snaptoPivot = new HashedString("snapTo_pivot");

	// Token: 0x02001380 RID: 4992
	public struct OverrideAnimFileData
	{
		// Token: 0x04006985 RID: 27013
		public float priority;

		// Token: 0x04006986 RID: 27014
		public KAnimFile file;
	}

	// Token: 0x02001381 RID: 4993
	public struct AnimLookupData
	{
		// Token: 0x04006987 RID: 27015
		public int animIndex;
	}

	// Token: 0x02001382 RID: 4994
	public struct AnimData
	{
		// Token: 0x04006988 RID: 27016
		public HashedString anim;

		// Token: 0x04006989 RID: 27017
		public KAnim.PlayMode mode;

		// Token: 0x0400698A RID: 27018
		public float speed;

		// Token: 0x0400698B RID: 27019
		public float timeOffset;
	}

	// Token: 0x02001383 RID: 4995
	public enum VisibilityType
	{
		// Token: 0x0400698D RID: 27021
		Default,
		// Token: 0x0400698E RID: 27022
		OffscreenUpdate,
		// Token: 0x0400698F RID: 27023
		Always
	}

	// Token: 0x02001384 RID: 4996
	// (Invoke) Token: 0x06008AB8 RID: 35512
	public delegate void KAnimEvent(HashedString name);
}
