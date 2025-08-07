using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000A35 RID: 2613
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/NoisePolluter")]
public class NoisePolluter : KMonoBehaviour, IPolluter
{
	// Token: 0x06004BC4 RID: 19396 RVA: 0x001B722F File Offset: 0x001B542F
	public static bool IsNoiseableCell(int cell)
	{
		return Grid.IsValidCell(cell) && (Grid.IsGas(cell) || !Grid.IsSubstantialLiquid(cell, 0.35f));
	}

	// Token: 0x06004BC5 RID: 19397 RVA: 0x001B7253 File Offset: 0x001B5453
	public void ResetCells()
	{
		if (this.radius == 0)
		{
			global::Debug.LogFormat("[{0}] has a 0 radius noise, this will disable it", new object[] { this.GetName() });
			return;
		}
	}

	// Token: 0x06004BC6 RID: 19398 RVA: 0x001B7277 File Offset: 0x001B5477
	public void SetAttributes(Vector2 pos, int dB, GameObject go, string name)
	{
		this.sourceName = name;
		this.noise = dB;
	}

	// Token: 0x06004BC7 RID: 19399 RVA: 0x001B7288 File Offset: 0x001B5488
	public int GetRadius()
	{
		return this.radius;
	}

	// Token: 0x06004BC8 RID: 19400 RVA: 0x001B7290 File Offset: 0x001B5490
	public int GetNoise()
	{
		return this.noise;
	}

	// Token: 0x06004BC9 RID: 19401 RVA: 0x001B7298 File Offset: 0x001B5498
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06004BCA RID: 19402 RVA: 0x001B72A0 File Offset: 0x001B54A0
	public void SetSplat(NoiseSplat new_splat)
	{
		this.splat = new_splat;
	}

	// Token: 0x06004BCB RID: 19403 RVA: 0x001B72A9 File Offset: 0x001B54A9
	public void Clear()
	{
		if (this.splat != null)
		{
			this.splat.Clear();
			this.splat = null;
		}
	}

	// Token: 0x06004BCC RID: 19404 RVA: 0x001B72C5 File Offset: 0x001B54C5
	public Vector2 GetPosition()
	{
		return base.transform.GetPosition();
	}

	// Token: 0x1700052F RID: 1327
	// (get) Token: 0x06004BCD RID: 19405 RVA: 0x001B72D7 File Offset: 0x001B54D7
	// (set) Token: 0x06004BCE RID: 19406 RVA: 0x001B72DF File Offset: 0x001B54DF
	public string sourceName { get; private set; }

	// Token: 0x17000530 RID: 1328
	// (get) Token: 0x06004BCF RID: 19407 RVA: 0x001B72E8 File Offset: 0x001B54E8
	// (set) Token: 0x06004BD0 RID: 19408 RVA: 0x001B72F0 File Offset: 0x001B54F0
	public bool active { get; private set; }

	// Token: 0x06004BD1 RID: 19409 RVA: 0x001B72F9 File Offset: 0x001B54F9
	public void SetActive(bool active = true)
	{
		if (!active && this.splat != null)
		{
			AudioEventManager.Get().ClearNoiseSplat(this.splat);
			this.splat.Clear();
		}
		this.active = active;
	}

	// Token: 0x06004BD2 RID: 19410 RVA: 0x001B7328 File Offset: 0x001B5528
	public void Refresh()
	{
		if (this.active)
		{
			if (this.splat != null)
			{
				AudioEventManager.Get().ClearNoiseSplat(this.splat);
				this.splat.Clear();
			}
			KSelectable component = base.GetComponent<KSelectable>();
			string text = ((component != null) ? component.GetName() : base.name);
			GameObject gameObject = base.GetComponent<KMonoBehaviour>().gameObject;
			this.splat = AudioEventManager.Get().CreateNoiseSplat(this.GetPosition(), this.noise, this.radius, text, gameObject);
		}
	}

	// Token: 0x06004BD3 RID: 19411 RVA: 0x001B73B0 File Offset: 0x001B55B0
	private void OnActiveChanged(object data)
	{
		bool isActive = ((Operational)data).IsActive;
		this.SetActive(isActive);
		this.Refresh();
	}

	// Token: 0x06004BD4 RID: 19412 RVA: 0x001B73D6 File Offset: 0x001B55D6
	public void SetValues(EffectorValues values)
	{
		this.noise = values.amount;
		this.radius = values.radius;
	}

	// Token: 0x06004BD5 RID: 19413 RVA: 0x001B73F0 File Offset: 0x001B55F0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.radius == 0 || this.noise == 0)
		{
			global::Debug.LogWarning(string.Concat(new string[]
			{
				"Noisepollutor::OnSpawn [",
				this.GetName(),
				"] noise: [",
				this.noise.ToString(),
				"] radius: [",
				this.radius.ToString(),
				"]"
			}));
			global::UnityEngine.Object.Destroy(this);
			return;
		}
		this.ResetCells();
		Operational component = base.GetComponent<Operational>();
		if (component != null)
		{
			base.Subscribe<NoisePolluter>(824508782, NoisePolluter.OnActiveChangedDelegate);
		}
		this.refreshCallback = new global::System.Action(this.Refresh);
		this.refreshPartionerCallback = delegate(object data)
		{
			this.Refresh();
		};
		this.onCollectNoisePollutersCallback = new Action<object>(this.OnCollectNoisePolluters);
		Attributes attributes = this.GetAttributes();
		Db db = Db.Get();
		this.dB = attributes.Add(db.BuildingAttributes.NoisePollution);
		this.dBRadius = attributes.Add(db.BuildingAttributes.NoisePollutionRadius);
		if (this.noise != 0 && this.radius != 0)
		{
			AttributeModifier attributeModifier = new AttributeModifier(db.BuildingAttributes.NoisePollution.Id, (float)this.noise, UI.TOOLTIPS.BASE_VALUE, false, false, true);
			AttributeModifier attributeModifier2 = new AttributeModifier(db.BuildingAttributes.NoisePollutionRadius.Id, (float)this.radius, UI.TOOLTIPS.BASE_VALUE, false, false, true);
			attributes.Add(attributeModifier);
			attributes.Add(attributeModifier2);
		}
		else
		{
			global::Debug.LogWarning(string.Concat(new string[]
			{
				"Noisepollutor::OnSpawn [",
				this.GetName(),
				"] radius: [",
				this.radius.ToString(),
				"] noise: [",
				this.noise.ToString(),
				"]"
			}));
		}
		KBatchedAnimController component2 = base.GetComponent<KBatchedAnimController>();
		this.isMovable = component2 != null && component2.isMovable;
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange), "NoisePolluter.OnSpawn");
		AttributeInstance attributeInstance = this.dB;
		attributeInstance.OnDirty = (global::System.Action)Delegate.Combine(attributeInstance.OnDirty, this.refreshCallback);
		AttributeInstance attributeInstance2 = this.dBRadius;
		attributeInstance2.OnDirty = (global::System.Action)Delegate.Combine(attributeInstance2.OnDirty, this.refreshCallback);
		if (component != null)
		{
			this.OnActiveChanged(component.IsActive);
		}
	}

	// Token: 0x06004BD6 RID: 19414 RVA: 0x001B7671 File Offset: 0x001B5871
	private void OnCellChange()
	{
		this.Refresh();
	}

	// Token: 0x06004BD7 RID: 19415 RVA: 0x001B7679 File Offset: 0x001B5879
	private void OnCollectNoisePolluters(object data)
	{
		((List<NoisePolluter>)data).Add(this);
	}

	// Token: 0x06004BD8 RID: 19416 RVA: 0x001B7687 File Offset: 0x001B5887
	public string GetName()
	{
		if (string.IsNullOrEmpty(this.sourceName))
		{
			this.sourceName = base.GetComponent<KSelectable>().GetName();
		}
		return this.sourceName;
	}

	// Token: 0x06004BD9 RID: 19417 RVA: 0x001B76B0 File Offset: 0x001B58B0
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (base.isSpawned)
		{
			if (this.dB != null)
			{
				AttributeInstance attributeInstance = this.dB;
				attributeInstance.OnDirty = (global::System.Action)Delegate.Remove(attributeInstance.OnDirty, this.refreshCallback);
				AttributeInstance attributeInstance2 = this.dBRadius;
				attributeInstance2.OnDirty = (global::System.Action)Delegate.Remove(attributeInstance2.OnDirty, this.refreshCallback);
			}
			if (this.isMovable)
			{
				Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
			}
		}
		if (this.splat != null)
		{
			AudioEventManager.Get().ClearNoiseSplat(this.splat);
			this.splat.Clear();
		}
	}

	// Token: 0x06004BDA RID: 19418 RVA: 0x001B775C File Offset: 0x001B595C
	public float GetNoiseForCell(int cell)
	{
		return this.splat.GetDBForCell(cell);
	}

	// Token: 0x06004BDB RID: 19419 RVA: 0x001B776C File Offset: 0x001B596C
	public List<Descriptor> GetEffectDescriptions()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.dB != null && this.dBRadius != null)
		{
			float totalValue = this.dB.GetTotalValue();
			float totalValue2 = this.dBRadius.GetTotalValue();
			string text = ((this.noise > 0) ? UI.BUILDINGEFFECTS.TOOLTIPS.NOISE_POLLUTION_INCREASE : UI.BUILDINGEFFECTS.TOOLTIPS.NOISE_POLLUTION_DECREASE);
			text = text + "\n\n" + this.dB.GetAttributeValueTooltip();
			string text2 = GameUtil.AddPositiveSign(totalValue.ToString(), totalValue > 0f);
			Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.NOISE_CREATED, text2, totalValue2), string.Format(text, text2, totalValue2), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
		}
		else if (this.noise != 0)
		{
			string text3 = ((this.noise >= 0) ? UI.BUILDINGEFFECTS.TOOLTIPS.NOISE_POLLUTION_INCREASE : UI.BUILDINGEFFECTS.TOOLTIPS.NOISE_POLLUTION_DECREASE);
			string text4 = GameUtil.AddPositiveSign(this.noise.ToString(), this.noise > 0);
			Descriptor descriptor2 = new Descriptor(string.Format(UI.BUILDINGEFFECTS.NOISE_CREATED, text4, this.radius), string.Format(text3, text4, this.radius), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06004BDC RID: 19420 RVA: 0x001B78B1 File Offset: 0x001B5AB1
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return this.GetEffectDescriptions();
	}

	// Token: 0x0400323B RID: 12859
	public const string ID = "NoisePolluter";

	// Token: 0x0400323C RID: 12860
	public int radius;

	// Token: 0x0400323D RID: 12861
	public int noise;

	// Token: 0x0400323E RID: 12862
	public AttributeInstance dB;

	// Token: 0x0400323F RID: 12863
	public AttributeInstance dBRadius;

	// Token: 0x04003240 RID: 12864
	private NoiseSplat splat;

	// Token: 0x04003242 RID: 12866
	public global::System.Action refreshCallback;

	// Token: 0x04003243 RID: 12867
	public Action<object> refreshPartionerCallback;

	// Token: 0x04003244 RID: 12868
	public Action<object> onCollectNoisePollutersCallback;

	// Token: 0x04003245 RID: 12869
	public bool isMovable;

	// Token: 0x04003246 RID: 12870
	[MyCmpReq]
	public OccupyArea occupyArea;

	// Token: 0x04003248 RID: 12872
	private static readonly EventSystem.IntraObjectHandler<NoisePolluter> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<NoisePolluter>(delegate(NoisePolluter component, object data)
	{
		component.OnActiveChanged(data);
	});
}
