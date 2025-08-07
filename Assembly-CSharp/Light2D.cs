using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000AA8 RID: 2728
[AddComponentMenu("KMonoBehaviour/scripts/Light2D")]
public class Light2D : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06004F18 RID: 20248 RVA: 0x001C93D7 File Offset: 0x001C75D7
	private T MaybeDirty<T>(T old_value, T new_value, ref bool dirty)
	{
		if (!EqualityComparer<T>.Default.Equals(old_value, new_value))
		{
			dirty = true;
			return new_value;
		}
		return old_value;
	}

	// Token: 0x17000577 RID: 1399
	// (get) Token: 0x06004F19 RID: 20249 RVA: 0x001C93ED File Offset: 0x001C75ED
	// (set) Token: 0x06004F1A RID: 20250 RVA: 0x001C93FA File Offset: 0x001C75FA
	public global::LightShape shape
	{
		get
		{
			return this.pending_emitter_state.shape;
		}
		set
		{
			this.pending_emitter_state.shape = this.MaybeDirty<global::LightShape>(this.pending_emitter_state.shape, value, ref this.dirty_shape);
		}
	}

	// Token: 0x17000578 RID: 1400
	// (get) Token: 0x06004F1B RID: 20251 RVA: 0x001C941F File Offset: 0x001C761F
	// (set) Token: 0x06004F1C RID: 20252 RVA: 0x001C9427 File Offset: 0x001C7627
	public LightGridManager.LightGridEmitter emitter { get; private set; }

	// Token: 0x17000579 RID: 1401
	// (get) Token: 0x06004F1D RID: 20253 RVA: 0x001C9430 File Offset: 0x001C7630
	// (set) Token: 0x06004F1E RID: 20254 RVA: 0x001C943D File Offset: 0x001C763D
	public Color Color
	{
		get
		{
			return this.pending_emitter_state.colour;
		}
		set
		{
			this.pending_emitter_state.colour = value;
		}
	}

	// Token: 0x1700057A RID: 1402
	// (get) Token: 0x06004F1F RID: 20255 RVA: 0x001C944B File Offset: 0x001C764B
	// (set) Token: 0x06004F20 RID: 20256 RVA: 0x001C9458 File Offset: 0x001C7658
	public int Lux
	{
		get
		{
			return this.pending_emitter_state.intensity;
		}
		set
		{
			this.pending_emitter_state.intensity = value;
		}
	}

	// Token: 0x1700057B RID: 1403
	// (get) Token: 0x06004F21 RID: 20257 RVA: 0x001C9466 File Offset: 0x001C7666
	// (set) Token: 0x06004F22 RID: 20258 RVA: 0x001C9473 File Offset: 0x001C7673
	public DiscreteShadowCaster.Direction LightDirection
	{
		get
		{
			return this.pending_emitter_state.direction;
		}
		set
		{
			this.pending_emitter_state.direction = this.MaybeDirty<DiscreteShadowCaster.Direction>(this.pending_emitter_state.direction, value, ref this.dirty_shape);
		}
	}

	// Token: 0x1700057C RID: 1404
	// (get) Token: 0x06004F23 RID: 20259 RVA: 0x001C9498 File Offset: 0x001C7698
	// (set) Token: 0x06004F24 RID: 20260 RVA: 0x001C94A5 File Offset: 0x001C76A5
	public int Width
	{
		get
		{
			return this.pending_emitter_state.width;
		}
		set
		{
			this.pending_emitter_state.width = this.MaybeDirty<int>(this.pending_emitter_state.width, value, ref this.dirty_shape);
		}
	}

	// Token: 0x1700057D RID: 1405
	// (get) Token: 0x06004F25 RID: 20261 RVA: 0x001C94CA File Offset: 0x001C76CA
	// (set) Token: 0x06004F26 RID: 20262 RVA: 0x001C94D7 File Offset: 0x001C76D7
	public float Range
	{
		get
		{
			return this.pending_emitter_state.radius;
		}
		set
		{
			this.pending_emitter_state.radius = this.MaybeDirty<float>(this.pending_emitter_state.radius, value, ref this.dirty_shape);
		}
	}

	// Token: 0x1700057E RID: 1406
	// (get) Token: 0x06004F27 RID: 20263 RVA: 0x001C94FC File Offset: 0x001C76FC
	// (set) Token: 0x06004F28 RID: 20264 RVA: 0x001C9509 File Offset: 0x001C7709
	private int origin
	{
		get
		{
			return this.pending_emitter_state.origin;
		}
		set
		{
			this.pending_emitter_state.origin = this.MaybeDirty<int>(this.pending_emitter_state.origin, value, ref this.dirty_position);
		}
	}

	// Token: 0x1700057F RID: 1407
	// (get) Token: 0x06004F29 RID: 20265 RVA: 0x001C952E File Offset: 0x001C772E
	// (set) Token: 0x06004F2A RID: 20266 RVA: 0x001C953B File Offset: 0x001C773B
	public float FalloffRate
	{
		get
		{
			return this.pending_emitter_state.falloffRate;
		}
		set
		{
			this.pending_emitter_state.falloffRate = this.MaybeDirty<float>(this.pending_emitter_state.falloffRate, value, ref this.dirty_falloff);
		}
	}

	// Token: 0x17000580 RID: 1408
	// (get) Token: 0x06004F2B RID: 20267 RVA: 0x001C9560 File Offset: 0x001C7760
	// (set) Token: 0x06004F2C RID: 20268 RVA: 0x001C9568 File Offset: 0x001C7768
	public float IntensityAnimation { get; set; }

	// Token: 0x17000581 RID: 1409
	// (get) Token: 0x06004F2D RID: 20269 RVA: 0x001C9571 File Offset: 0x001C7771
	// (set) Token: 0x06004F2E RID: 20270 RVA: 0x001C9579 File Offset: 0x001C7779
	public Vector2 Offset
	{
		get
		{
			return this._offset;
		}
		set
		{
			if (this._offset != value)
			{
				this._offset = value;
				this.origin = Grid.PosToCell(base.transform.GetPosition() + this._offset);
			}
		}
	}

	// Token: 0x17000582 RID: 1410
	// (get) Token: 0x06004F2F RID: 20271 RVA: 0x001C95B6 File Offset: 0x001C77B6
	private bool isRegistered
	{
		get
		{
			return this.solidPartitionerEntry != HandleVector<int>.InvalidHandle;
		}
	}

	// Token: 0x06004F30 RID: 20272 RVA: 0x001C95C8 File Offset: 0x001C77C8
	public Light2D()
	{
		this.emitter = new LightGridManager.LightGridEmitter();
		this.Range = 5f;
		this.Lux = 1000;
	}

	// Token: 0x06004F31 RID: 20273 RVA: 0x001C9624 File Offset: 0x001C7824
	protected override void OnPrefabInit()
	{
		base.Subscribe<Light2D>(-592767678, Light2D.OnOperationalChangedDelegate);
		if (this.disableOnStore)
		{
			base.Subscribe(856640610, new Action<object>(this.OnStore));
		}
		this.IntensityAnimation = 1f;
	}

	// Token: 0x06004F32 RID: 20274 RVA: 0x001C9664 File Offset: 0x001C7864
	private void OnStore(object data)
	{
		global::Debug.Assert(this.disableOnStore, "Only Light2Ds that are disabled on storage should be subscribed to OnStore.");
		Storage storage = data as Storage;
		if (storage != null)
		{
			base.enabled = storage.GetComponent<ItemPedestal>() != null || storage.GetComponent<MinionIdentity>() != null;
			return;
		}
		base.enabled = true;
	}

	// Token: 0x06004F33 RID: 20275 RVA: 0x001C96BC File Offset: 0x001C78BC
	protected override void OnCmpEnable()
	{
		this.materialPropertyBlock = new MaterialPropertyBlock();
		base.OnCmpEnable();
		Components.Light2Ds.Add(this);
		if (base.isSpawned)
		{
			this.AddToScenePartitioner();
			this.emitter.Refresh(this.pending_emitter_state, true);
		}
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnMoved), "Light2D.OnMoved");
	}

	// Token: 0x06004F34 RID: 20276 RVA: 0x001C9728 File Offset: 0x001C7928
	protected override void OnCmpDisable()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnMoved));
		Components.Light2Ds.Remove(this);
		base.OnCmpDisable();
		this.FullRemove();
	}

	// Token: 0x06004F35 RID: 20277 RVA: 0x001C9760 File Offset: 0x001C7960
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.origin = Grid.PosToCell(base.transform.GetPosition() + this.Offset);
		this.cachedCell = this.origin;
		if (base.isActiveAndEnabled)
		{
			this.AddToScenePartitioner();
			this.emitter.Refresh(this.pending_emitter_state, true);
		}
	}

	// Token: 0x06004F36 RID: 20278 RVA: 0x001C97C6 File Offset: 0x001C79C6
	protected override void OnCleanUp()
	{
		this.FullRemove();
	}

	// Token: 0x06004F37 RID: 20279 RVA: 0x001C97CE File Offset: 0x001C79CE
	private void OnMoved()
	{
		if (base.isSpawned)
		{
			this.FullRefresh();
		}
	}

	// Token: 0x06004F38 RID: 20280 RVA: 0x001C97DE File Offset: 0x001C79DE
	private HandleVector<int>.Handle AddToLayer(Extents ext, ScenePartitionerLayer layer)
	{
		return GameScenePartitioner.Instance.Add("Light2D", base.gameObject, ext, layer, new Action<object>(this.OnWorldChanged));
	}

	// Token: 0x06004F39 RID: 20281 RVA: 0x001C9804 File Offset: 0x001C7A04
	private Extents ComputeExtents()
	{
		Vector2I vector2I = Grid.CellToXY(this.origin);
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		global::LightShape shape = this.shape;
		if (shape > global::LightShape.Cone)
		{
			if (shape == global::LightShape.Quad)
			{
				num3 = this.Width;
				num4 = (int)this.Range;
				int num5 = ((this.Width % 2 == 0) ? (this.Width / 2 - 1) : Mathf.FloorToInt((float)(this.Width - 1) * 0.5f));
				Vector2I vector2I2 = vector2I - DiscreteShadowCaster.TravelDirectionToOrtogonalDiractionVector(this.LightDirection) * num5;
				num = vector2I2.x;
				switch (this.LightDirection)
				{
				case DiscreteShadowCaster.Direction.North:
					num2 = vector2I2.y;
					goto IL_0119;
				case DiscreteShadowCaster.Direction.South:
					num2 = vector2I2.y - num4;
					goto IL_0119;
				}
				num2 = vector2I2.y - DiscreteShadowCaster.TravelDirectionToOrtogonalDiractionVector(this.LightDirection).y * num5;
			}
		}
		else
		{
			int num6 = (int)this.Range;
			int num7 = num6 * 2;
			num = vector2I.x - num6;
			num2 = vector2I.y - num6;
			num3 = num7;
			num4 = ((this.shape == global::LightShape.Circle) ? num7 : num6);
		}
		IL_0119:
		return new Extents(num, num2, num3, num4);
	}

	// Token: 0x06004F3A RID: 20282 RVA: 0x001C9934 File Offset: 0x001C7B34
	private void AddToScenePartitioner()
	{
		Extents extents = this.ComputeExtents();
		this.solidPartitionerEntry = this.AddToLayer(extents, GameScenePartitioner.Instance.solidChangedLayer);
		this.liquidPartitionerEntry = this.AddToLayer(extents, GameScenePartitioner.Instance.liquidChangedLayer);
	}

	// Token: 0x06004F3B RID: 20283 RVA: 0x001C9976 File Offset: 0x001C7B76
	private void RemoveFromScenePartitioner()
	{
		if (this.isRegistered)
		{
			GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
			GameScenePartitioner.Instance.Free(ref this.liquidPartitionerEntry);
		}
	}

	// Token: 0x06004F3C RID: 20284 RVA: 0x001C99A0 File Offset: 0x001C7BA0
	private void MoveInScenePartitioner()
	{
		GameScenePartitioner.Instance.UpdatePosition(this.solidPartitionerEntry, this.ComputeExtents());
		GameScenePartitioner.Instance.UpdatePosition(this.liquidPartitionerEntry, this.ComputeExtents());
	}

	// Token: 0x06004F3D RID: 20285 RVA: 0x001C99CE File Offset: 0x001C7BCE
	private void EmitterRefresh()
	{
		this.emitter.Refresh(this.pending_emitter_state, true);
	}

	// Token: 0x06004F3E RID: 20286 RVA: 0x001C99E3 File Offset: 0x001C7BE3
	[ContextMenu("Refresh")]
	public void FullRefresh()
	{
		if (!base.isSpawned || !base.isActiveAndEnabled)
		{
			return;
		}
		DebugUtil.DevAssert(this.isRegistered, "shouldn't be refreshing if we aren't spawned and enabled", null);
		this.RefreshShapeAndPosition();
		this.EmitterRefresh();
	}

	// Token: 0x06004F3F RID: 20287 RVA: 0x001C9A14 File Offset: 0x001C7C14
	public void FullRemove()
	{
		this.RemoveFromScenePartitioner();
		this.emitter.RemoveFromGrid();
		this.cachedCell = Grid.InvalidCell;
	}

	// Token: 0x06004F40 RID: 20288 RVA: 0x001C9A34 File Offset: 0x001C7C34
	public Light2D.RefreshResult RefreshShapeAndPosition()
	{
		if (!base.isSpawned)
		{
			return Light2D.RefreshResult.None;
		}
		if (!base.isActiveAndEnabled)
		{
			this.FullRemove();
			return Light2D.RefreshResult.Removed;
		}
		int num = Grid.PosToCell(base.transform.GetPosition() + this.Offset);
		if (!Grid.IsValidCell(num))
		{
			this.FullRemove();
			return Light2D.RefreshResult.Removed;
		}
		this.origin = num;
		if (this.dirty_shape)
		{
			this.RemoveFromScenePartitioner();
			this.AddToScenePartitioner();
		}
		else if (this.dirty_position)
		{
			this.MoveInScenePartitioner();
		}
		if (this.dirty_falloff)
		{
			this.EmitterRefresh();
		}
		this.dirty_shape = false;
		this.dirty_position = false;
		this.dirty_falloff = false;
		this.cachedCell = num;
		return Light2D.RefreshResult.Updated;
	}

	// Token: 0x06004F41 RID: 20289 RVA: 0x001C9AE2 File Offset: 0x001C7CE2
	private void OnWorldChanged(object data)
	{
		this.FullRefresh();
	}

	// Token: 0x06004F42 RID: 20290 RVA: 0x001C9AEC File Offset: 0x001C7CEC
	public virtual List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.EMITS_LIGHT, this.Range), UI.GAMEOBJECTEFFECTS.TOOLTIPS.EMITS_LIGHT, Descriptor.DescriptorType.Effect, false),
			new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.EMITS_LIGHT_LUX, this.Lux), UI.GAMEOBJECTEFFECTS.TOOLTIPS.EMITS_LIGHT_LUX, Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x0400347C RID: 13436
	public bool autoRespondToOperational = true;

	// Token: 0x0400347D RID: 13437
	private bool dirty_shape;

	// Token: 0x0400347E RID: 13438
	private bool dirty_position;

	// Token: 0x0400347F RID: 13439
	private bool dirty_falloff;

	// Token: 0x04003480 RID: 13440
	public int cachedCell;

	// Token: 0x04003481 RID: 13441
	[SerializeField]
	private LightGridManager.LightGridEmitter.State pending_emitter_state = LightGridManager.LightGridEmitter.State.DEFAULT;

	// Token: 0x04003484 RID: 13444
	public float Angle;

	// Token: 0x04003485 RID: 13445
	public Vector2 Direction;

	// Token: 0x04003486 RID: 13446
	[SerializeField]
	private Vector2 _offset;

	// Token: 0x04003487 RID: 13447
	public bool drawOverlay;

	// Token: 0x04003488 RID: 13448
	public Color overlayColour;

	// Token: 0x04003489 RID: 13449
	public MaterialPropertyBlock materialPropertyBlock;

	// Token: 0x0400348A RID: 13450
	private HandleVector<int>.Handle solidPartitionerEntry = HandleVector<int>.InvalidHandle;

	// Token: 0x0400348B RID: 13451
	private HandleVector<int>.Handle liquidPartitionerEntry = HandleVector<int>.InvalidHandle;

	// Token: 0x0400348C RID: 13452
	public bool disableOnStore;

	// Token: 0x0400348D RID: 13453
	private static readonly EventSystem.IntraObjectHandler<Light2D> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<Light2D>(delegate(Light2D light, object data)
	{
		if (light.autoRespondToOperational)
		{
			light.enabled = (bool)data;
		}
	});

	// Token: 0x02001B8B RID: 7051
	public enum RefreshResult
	{
		// Token: 0x0400832A RID: 33578
		None,
		// Token: 0x0400832B RID: 33579
		Removed,
		// Token: 0x0400832C RID: 33580
		Updated
	}
}
