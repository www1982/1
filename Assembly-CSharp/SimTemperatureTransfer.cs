using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B10 RID: 2832
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/SimTemperatureTransfer")]
public class SimTemperatureTransfer : KMonoBehaviour
{
	// Token: 0x170005D3 RID: 1491
	// (get) Token: 0x06005365 RID: 21349 RVA: 0x001E5D7B File Offset: 0x001E3F7B
	// (set) Token: 0x06005366 RID: 21350 RVA: 0x001E5D83 File Offset: 0x001E3F83
	public float SurfaceArea
	{
		get
		{
			return this.surfaceArea;
		}
		set
		{
			this.surfaceArea = value;
		}
	}

	// Token: 0x170005D4 RID: 1492
	// (get) Token: 0x06005367 RID: 21351 RVA: 0x001E5D8C File Offset: 0x001E3F8C
	// (set) Token: 0x06005368 RID: 21352 RVA: 0x001E5D94 File Offset: 0x001E3F94
	public float Thickness
	{
		get
		{
			return this.thickness;
		}
		set
		{
			this.thickness = value;
		}
	}

	// Token: 0x170005D5 RID: 1493
	// (get) Token: 0x06005369 RID: 21353 RVA: 0x001E5D9D File Offset: 0x001E3F9D
	// (set) Token: 0x0600536A RID: 21354 RVA: 0x001E5DA5 File Offset: 0x001E3FA5
	public float GroundTransferScale
	{
		get
		{
			return this.groundTransferScale;
		}
		set
		{
			this.groundTransferScale = value;
		}
	}

	// Token: 0x170005D6 RID: 1494
	// (get) Token: 0x0600536B RID: 21355 RVA: 0x001E5DAE File Offset: 0x001E3FAE
	public int SimHandle
	{
		get
		{
			return this.simHandle;
		}
	}

	// Token: 0x0600536C RID: 21356 RVA: 0x001E5DB6 File Offset: 0x001E3FB6
	public static void ClearInstanceMap()
	{
		SimTemperatureTransfer.handleInstanceMap.Clear();
	}

	// Token: 0x0600536D RID: 21357 RVA: 0x001E5DC4 File Offset: 0x001E3FC4
	public static void DoOreMeltTransition(int sim_handle)
	{
		SimTemperatureTransfer simTemperatureTransfer = null;
		if (!SimTemperatureTransfer.handleInstanceMap.TryGetValue(sim_handle, out simTemperatureTransfer))
		{
			return;
		}
		if (simTemperatureTransfer == null)
		{
			return;
		}
		if (simTemperatureTransfer.HasTag(GameTags.Sealed))
		{
			return;
		}
		PrimaryElement primaryElement = simTemperatureTransfer.pe;
		Element element = primaryElement.Element;
		bool flag = primaryElement.Temperature >= element.highTemp;
		bool flag2 = primaryElement.Temperature <= element.lowTemp;
		if (!flag && !flag2)
		{
			return;
		}
		if (flag && element.highTempTransitionTarget == SimHashes.Unobtanium)
		{
			return;
		}
		if (flag2 && element.lowTempTransitionTarget == SimHashes.Unobtanium)
		{
			return;
		}
		if (primaryElement.Mass > 0f)
		{
			int num = Grid.PosToCell(simTemperatureTransfer.transform.GetPosition());
			float num2 = primaryElement.Mass;
			int num3 = primaryElement.DiseaseCount;
			SimHashes simHashes = (flag ? element.highTempTransitionTarget : element.lowTempTransitionTarget);
			SimHashes simHashes2 = (flag ? element.highTempTransitionOreID : element.lowTempTransitionOreID);
			float num4 = (flag ? element.highTempTransitionOreMassConversion : element.lowTempTransitionOreMassConversion);
			if (simHashes2 != (SimHashes)0)
			{
				float num5 = num2 * num4;
				int num6 = (int)((float)num3 * num4);
				if (num5 > 0.001f)
				{
					num2 -= num5;
					num3 -= num6;
					Element element2 = ElementLoader.FindElementByHash(simHashes2);
					if (element2.IsSolid)
					{
						GameObject gameObject = element2.substance.SpawnResource(simTemperatureTransfer.transform.GetPosition(), num5, primaryElement.Temperature, primaryElement.DiseaseIdx, num6, true, false, true);
						element2.substance.ActivateSubstanceGameObject(gameObject, primaryElement.DiseaseIdx, num6);
					}
					else
					{
						SimMessages.AddRemoveSubstance(num, element2.id, CellEventLogger.Instance.OreMelted, num5, primaryElement.Temperature, primaryElement.DiseaseIdx, num6, true, -1);
					}
				}
			}
			SimMessages.AddRemoveSubstance(num, simHashes, CellEventLogger.Instance.OreMelted, num2, primaryElement.Temperature, primaryElement.DiseaseIdx, num3, true, -1);
		}
		simTemperatureTransfer.OnCleanUp();
		Util.KDestroyGameObject(simTemperatureTransfer.gameObject);
	}

	// Token: 0x0600536E RID: 21358 RVA: 0x001E5FAC File Offset: 0x001E41AC
	protected override void OnPrefabInit()
	{
		this.pe.sttOptimizationHook = this;
		this.pe.getTemperatureCallback = new PrimaryElement.GetTemperatureCallback(SimTemperatureTransfer.OnGetTemperature);
		this.pe.setTemperatureCallback = new PrimaryElement.SetTemperatureCallback(SimTemperatureTransfer.OnSetTemperature);
		PrimaryElement primaryElement = this.pe;
		primaryElement.onDataChanged = (Action<PrimaryElement>)Delegate.Combine(primaryElement.onDataChanged, new Action<PrimaryElement>(this.OnDataChanged));
	}

	// Token: 0x0600536F RID: 21359 RVA: 0x001E601C File Offset: 0x001E421C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Element element = this.pe.Element;
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChanged), "SimTemperatureTransfer.OnSpawn");
		if (!Grid.IsValidCell(Grid.PosToCell(this)) || this.pe.Element.HasTag(GameTags.Special) || element.specificHeatCapacity == 0f)
		{
			base.enabled = false;
		}
		this.SimRegister();
	}

	// Token: 0x06005370 RID: 21360 RVA: 0x001E609B File Offset: 0x001E429B
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.SimRegister();
		if (Sim.IsValidHandle(this.simHandle))
		{
			SimTemperatureTransfer.OnSetTemperature(this.pe, this.pe.Temperature);
		}
	}

	// Token: 0x06005371 RID: 21361 RVA: 0x001E60CC File Offset: 0x001E42CC
	protected override void OnCmpDisable()
	{
		if (Sim.IsValidHandle(this.simHandle))
		{
			float temperature = this.pe.Temperature;
			this.pe.InternalTemperature = this.pe.Temperature;
			SimMessages.SetElementChunkData(this.simHandle, temperature, 0f);
		}
		base.OnCmpDisable();
	}

	// Token: 0x06005372 RID: 21362 RVA: 0x001E6120 File Offset: 0x001E4320
	private void OnCellChanged()
	{
		int num = Grid.PosToCell(this);
		if (!Grid.IsValidCell(num))
		{
			base.enabled = false;
			return;
		}
		this.SimRegister();
		if (Sim.IsValidHandle(this.simHandle))
		{
			SimMessages.MoveElementChunk(this.simHandle, num);
			return;
		}
		this.forceDataSyncOnRegister = true;
	}

	// Token: 0x06005373 RID: 21363 RVA: 0x001E616B File Offset: 0x001E436B
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChanged));
		this.SimUnregister();
		base.OnForcedCleanUp();
	}

	// Token: 0x06005374 RID: 21364 RVA: 0x001E6198 File Offset: 0x001E4398
	private unsafe static float OnGetTemperature(PrimaryElement primary_element)
	{
		SimTemperatureTransfer sttOptimizationHook = primary_element.sttOptimizationHook;
		float num;
		if (Sim.IsValidHandle(sttOptimizationHook.simHandle))
		{
			int handleIndex = Sim.GetHandleIndex(sttOptimizationHook.simHandle);
			num = Game.Instance.simData.elementChunks[handleIndex].temperature;
			sttOptimizationHook.deltaKJ = Game.Instance.simData.elementChunks[handleIndex].deltaKJ;
		}
		else
		{
			num = primary_element.InternalTemperature;
		}
		return num;
	}

	// Token: 0x06005375 RID: 21365 RVA: 0x001E6214 File Offset: 0x001E4414
	private unsafe static void OnSetTemperature(PrimaryElement primary_element, float temperature)
	{
		if (temperature <= 0f)
		{
			KCrashReporter.Assert(false, "STT.OnSetTemperature - Tried to set <= 0 degree temperature", null);
			temperature = 293f;
		}
		primary_element.InternalTemperature = temperature;
		SimTemperatureTransfer sttOptimizationHook = primary_element.sttOptimizationHook;
		if (Sim.IsValidHandle(sttOptimizationHook.simHandle))
		{
			float mass = primary_element.Mass;
			float num = ((mass >= 0.01f) ? (mass * primary_element.Element.specificHeatCapacity) : 0f);
			SimMessages.SetElementChunkData(sttOptimizationHook.simHandle, temperature, num);
			int handleIndex = Sim.GetHandleIndex(sttOptimizationHook.simHandle);
			Game.Instance.simData.elementChunks[handleIndex].temperature = temperature;
		}
	}

	// Token: 0x06005376 RID: 21366 RVA: 0x001E62B4 File Offset: 0x001E44B4
	private void OnDataChanged(PrimaryElement primary_element)
	{
		if (Sim.IsValidHandle(this.simHandle))
		{
			float num = ((primary_element.Mass >= 0.01f) ? (primary_element.Mass * primary_element.Element.specificHeatCapacity) : 0f);
			SimMessages.SetElementChunkData(this.simHandle, primary_element.Temperature, num);
			return;
		}
		this.forceDataSyncOnRegister = true;
	}

	// Token: 0x06005377 RID: 21367 RVA: 0x001E6310 File Offset: 0x001E4510
	protected void SimRegister()
	{
		if (base.isSpawned && this.simHandle == -1 && base.enabled && this.pe.Mass > 0f && !this.pe.Element.IsTemperatureInsulated)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.simHandle = -2;
			HandleVector<Game.ComplexCallbackInfo<int>>.Handle handle = Game.Instance.simComponentCallbackManager.Add(new Action<int, object>(SimTemperatureTransfer.OnSimRegisteredCallback), this, "SimTemperatureTransfer.SimRegister");
			float num2 = this.pe.InternalTemperature;
			if (num2 <= 0f)
			{
				this.pe.InternalTemperature = 293f;
				num2 = 293f;
			}
			this.forceDataSyncOnRegister = false;
			SimMessages.AddElementChunk(num, this.pe.ElementID, this.pe.Mass, num2, this.surfaceArea, this.thickness, this.groundTransferScale, handle.index);
		}
	}

	// Token: 0x06005378 RID: 21368 RVA: 0x001E640C File Offset: 0x001E460C
	protected unsafe void SimUnregister()
	{
		if (this.simHandle != -1 && !KMonoBehaviour.isLoadingScene)
		{
			if (Sim.IsValidHandle(this.simHandle))
			{
				int handleIndex = Sim.GetHandleIndex(this.simHandle);
				this.pe.InternalTemperature = Game.Instance.simData.elementChunks[handleIndex].temperature;
				SimMessages.RemoveElementChunk(this.simHandle, -1);
				SimTemperatureTransfer.handleInstanceMap.Remove(this.simHandle);
			}
			this.simHandle = -1;
		}
	}

	// Token: 0x06005379 RID: 21369 RVA: 0x001E648F File Offset: 0x001E468F
	private static void OnSimRegisteredCallback(int handle, object data)
	{
		((SimTemperatureTransfer)data).OnSimRegistered(handle);
	}

	// Token: 0x0600537A RID: 21370 RVA: 0x001E64A0 File Offset: 0x001E46A0
	private unsafe void OnSimRegistered(int handle)
	{
		if (this != null && this.simHandle == -2)
		{
			this.simHandle = handle;
			int handleIndex = Sim.GetHandleIndex(handle);
			float temperature = Game.Instance.simData.elementChunks[handleIndex].temperature;
			float internalTemperature = this.pe.InternalTemperature;
			if (temperature <= 0f)
			{
				KCrashReporter.Assert(false, "Bad temperature", null);
			}
			SimTemperatureTransfer.handleInstanceMap[this.simHandle] = this;
			if (this.forceDataSyncOnRegister || Mathf.Abs(temperature - internalTemperature) > 0.1f)
			{
				float num = ((this.pe.Mass >= 0.01f) ? (this.pe.Mass * this.pe.Element.specificHeatCapacity) : 0f);
				SimMessages.SetElementChunkData(this.simHandle, internalTemperature, num);
				SimMessages.MoveElementChunk(this.simHandle, Grid.PosToCell(this));
				Game.Instance.simData.elementChunks[handleIndex].temperature = internalTemperature;
			}
			if (this.onSimRegistered != null)
			{
				this.onSimRegistered(this);
			}
			if (!base.enabled)
			{
				this.OnCmpDisable();
				return;
			}
		}
		else
		{
			SimMessages.RemoveElementChunk(handle, -1);
		}
	}

	// Token: 0x04003811 RID: 14353
	[MyCmpReq]
	public PrimaryElement pe;

	// Token: 0x04003812 RID: 14354
	private const float SIM_FREEZE_SPAWN_ORE_PERCENT = 0.8f;

	// Token: 0x04003813 RID: 14355
	public const float MIN_MASS_FOR_TEMPERATURE_TRANSFER = 0.01f;

	// Token: 0x04003814 RID: 14356
	public float deltaKJ;

	// Token: 0x04003815 RID: 14357
	public Action<SimTemperatureTransfer> onSimRegistered;

	// Token: 0x04003816 RID: 14358
	protected int simHandle = -1;

	// Token: 0x04003817 RID: 14359
	protected bool forceDataSyncOnRegister;

	// Token: 0x04003818 RID: 14360
	[SerializeField]
	protected float surfaceArea = 10f;

	// Token: 0x04003819 RID: 14361
	[SerializeField]
	protected float thickness = 0.01f;

	// Token: 0x0400381A RID: 14362
	[SerializeField]
	protected float groundTransferScale = 0.0625f;

	// Token: 0x0400381B RID: 14363
	private static Dictionary<int, SimTemperatureTransfer> handleInstanceMap = new Dictionary<int, SimTemperatureTransfer>();
}
