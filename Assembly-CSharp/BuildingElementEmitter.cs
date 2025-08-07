using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006CB RID: 1739
[AddComponentMenu("KMonoBehaviour/scripts/BuildingElementEmitter")]
public class BuildingElementEmitter : KMonoBehaviour, IGameObjectEffectDescriptor, IElementEmitter, ISim200ms
{
	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x000F7EAC File Offset: 0x000F60AC
	public float AverageEmitRate
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.accumulator);
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x000F7EC3 File Offset: 0x000F60C3
	public float EmitRate
	{
		get
		{
			return this.emitRate;
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x000F7ECB File Offset: 0x000F60CB
	public SimHashes Element
	{
		get
		{
			return this.element;
		}
	}

	// Token: 0x06002AD7 RID: 10967 RVA: 0x000F7ED3 File Offset: 0x000F60D3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.accumulator = Game.Instance.accumulators.Add("Element", this);
		base.Subscribe<BuildingElementEmitter>(824508782, BuildingElementEmitter.OnActiveChangedDelegate);
		this.SimRegister();
	}

	// Token: 0x06002AD8 RID: 10968 RVA: 0x000F7F0D File Offset: 0x000F610D
	protected override void OnCleanUp()
	{
		Game.Instance.accumulators.Remove(this.accumulator);
		this.SimUnregister();
		base.OnCleanUp();
	}

	// Token: 0x06002AD9 RID: 10969 RVA: 0x000F7F31 File Offset: 0x000F6131
	private void OnActiveChanged(object data)
	{
		this.simActive = ((Operational)data).IsActive;
		this.dirty = true;
	}

	// Token: 0x06002ADA RID: 10970 RVA: 0x000F7F4B File Offset: 0x000F614B
	public void Sim200ms(float dt)
	{
		this.UnsafeUpdate(dt);
	}

	// Token: 0x06002ADB RID: 10971 RVA: 0x000F7F54 File Offset: 0x000F6154
	private unsafe void UnsafeUpdate(float dt)
	{
		if (!Sim.IsValidHandle(this.simHandle))
		{
			return;
		}
		this.UpdateSimState();
		int handleIndex = Sim.GetHandleIndex(this.simHandle);
		Sim.EmittedMassInfo emittedMassInfo = Game.Instance.simData.emittedMassEntries[handleIndex];
		if (emittedMassInfo.mass > 0f)
		{
			Game.Instance.accumulators.Accumulate(this.accumulator, emittedMassInfo.mass);
			if (this.element == SimHashes.Oxygen)
			{
				ReportManager.Instance.ReportValue(ReportManager.ReportType.OxygenCreated, emittedMassInfo.mass, base.gameObject.GetProperName(), null);
			}
		}
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x000F7FF4 File Offset: 0x000F61F4
	private void UpdateSimState()
	{
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		if (this.simActive)
		{
			if (this.element != (SimHashes)0 && this.emitRate > 0f)
			{
				int num = Grid.PosToCell(new Vector3(base.transform.GetPosition().x + this.modifierOffset.x, base.transform.GetPosition().y + this.modifierOffset.y, 0f));
				SimMessages.ModifyElementEmitter(this.simHandle, num, (int)this.emitRange, this.element, 0.2f, this.emitRate * 0.2f, this.temperature, float.MaxValue, this.emitDiseaseIdx, this.emitDiseaseCount);
			}
			this.statusHandle = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.EmittingElement, this);
			return;
		}
		SimMessages.ModifyElementEmitter(this.simHandle, 0, 0, SimHashes.Vacuum, 0f, 0f, 0f, 0f, byte.MaxValue, 0);
		this.statusHandle = base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, this);
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x000F812C File Offset: 0x000F632C
	private void SimRegister()
	{
		if (base.isSpawned && this.simHandle == -1)
		{
			this.simHandle = -2;
			SimMessages.AddElementEmitter(float.MaxValue, Game.Instance.simComponentCallbackManager.Add(new Action<int, object>(BuildingElementEmitter.OnSimRegisteredCallback), this, "BuildingElementEmitter").index, -1, -1);
		}
	}

	// Token: 0x06002ADE RID: 10974 RVA: 0x000F8187 File Offset: 0x000F6387
	private void SimUnregister()
	{
		if (this.simHandle != -1)
		{
			if (Sim.IsValidHandle(this.simHandle))
			{
				SimMessages.RemoveElementEmitter(-1, this.simHandle);
			}
			this.simHandle = -1;
		}
	}

	// Token: 0x06002ADF RID: 10975 RVA: 0x000F81B2 File Offset: 0x000F63B2
	private static void OnSimRegisteredCallback(int handle, object data)
	{
		((BuildingElementEmitter)data).OnSimRegistered(handle);
	}

	// Token: 0x06002AE0 RID: 10976 RVA: 0x000F81C0 File Offset: 0x000F63C0
	private void OnSimRegistered(int handle)
	{
		if (this != null)
		{
			this.simHandle = handle;
			return;
		}
		SimMessages.RemoveElementEmitter(-1, handle);
	}

	// Token: 0x06002AE1 RID: 10977 RVA: 0x000F81DC File Offset: 0x000F63DC
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = ElementLoader.FindElementByHash(this.element).tag.ProperName();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_FIXEDTEMP, text, GameUtil.GetFormattedMass(this.EmitRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(this.temperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_FIXEDTEMP, text, GameUtil.GetFormattedMass(this.EmitRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(this.temperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x0400193B RID: 6459
	[SerializeField]
	public float emitRate = 0.3f;

	// Token: 0x0400193C RID: 6460
	[SerializeField]
	[Serialize]
	public float temperature = 293f;

	// Token: 0x0400193D RID: 6461
	[SerializeField]
	[HashedEnum]
	public SimHashes element = SimHashes.Oxygen;

	// Token: 0x0400193E RID: 6462
	[SerializeField]
	public Vector2 modifierOffset;

	// Token: 0x0400193F RID: 6463
	[SerializeField]
	public byte emitRange = 1;

	// Token: 0x04001940 RID: 6464
	[SerializeField]
	public byte emitDiseaseIdx = byte.MaxValue;

	// Token: 0x04001941 RID: 6465
	[SerializeField]
	public int emitDiseaseCount;

	// Token: 0x04001942 RID: 6466
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x04001943 RID: 6467
	private int simHandle = -1;

	// Token: 0x04001944 RID: 6468
	private bool simActive;

	// Token: 0x04001945 RID: 6469
	private bool dirty = true;

	// Token: 0x04001946 RID: 6470
	private Guid statusHandle;

	// Token: 0x04001947 RID: 6471
	private static readonly EventSystem.IntraObjectHandler<BuildingElementEmitter> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<BuildingElementEmitter>(delegate(BuildingElementEmitter component, object data)
	{
		component.OnActiveChanged(data);
	});
}
