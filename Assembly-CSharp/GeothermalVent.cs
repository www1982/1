using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200073E RID: 1854
public class GeothermalVent : StateMachineComponent<GeothermalVent.StatesInstance>, ISim200ms, ISaveLoadable
{
	// Token: 0x06002EE8 RID: 12008 RVA: 0x0010CE80 File Offset: 0x0010B080
	public bool IsQuestEntombed()
	{
		return this.progress == GeothermalVent.QuestProgress.Entombed;
	}

	// Token: 0x06002EE9 RID: 12009 RVA: 0x0010CE8C File Offset: 0x0010B08C
	public void SetQuestComplete()
	{
		this.progress = GeothermalVent.QuestProgress.Complete;
		this.connectedToggler.showButton = true;
		base.GetComponent<InfoDescription>().description = BUILDINGS.PREFABS.GEOTHERMALVENT.EFFECT + "\n\n" + BUILDINGS.PREFABS.GEOTHERMALVENT.DESC;
		base.Trigger(-1514841199, null);
	}

	// Token: 0x06002EEA RID: 12010 RVA: 0x0010CEE4 File Offset: 0x0010B0E4
	public static string GenerateName()
	{
		string text = "";
		for (int i = 0; i < 2; i++)
		{
			text += "0123456789"[global::UnityEngine.Random.Range(0, "0123456789".Length)].ToString();
		}
		return BUILDINGS.PREFABS.GEOTHERMALVENT.NAME_FMT.Replace("{ID}", text);
	}

	// Token: 0x06002EEB RID: 12011 RVA: 0x0010CF3C File Offset: 0x0010B13C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.entombVulnerable.SetStatusItem(Db.Get().BuildingStatusItems.Entombed);
		base.GetComponent<PrimaryElement>().SetElement(SimHashes.Katairite, true);
		this.emitterInfo = default(GeothermalVent.EmitterInfo);
		this.emitterInfo.cell = Grid.PosToCell(base.gameObject) + Grid.WidthInCells * 3;
		this.emitterInfo.element = default(GeothermalVent.ElementInfo);
		this.emitterInfo.simHandle = -1;
		Components.GeothermalVents.Add(base.gameObject.GetMyWorldId(), this);
		if (this.progress == GeothermalVent.QuestProgress.Uninitialized)
		{
			if (Components.GeothermalVents.GetItems(base.gameObject.GetMyWorldId()).Count == 3)
			{
				this.progress = GeothermalVent.QuestProgress.Entombed;
			}
			else
			{
				this.progress = GeothermalVent.QuestProgress.Complete;
			}
		}
		if (this.progress == GeothermalVent.QuestProgress.Complete)
		{
			this.connectedToggler.showButton = true;
		}
		else
		{
			base.GetComponent<InfoDescription>().description = BUILDINGS.PREFABS.GEOTHERMALVENT.EFFECT + "\n\n" + BUILDINGS.PREFABS.GEOTHERMALVENT.BLOCKED_DESC;
			base.Trigger(-1514841199, null);
		}
		this.massMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.NoChange, Grid.SceneLayer.NoLayer, GeothermalVentConfig.BAROMETER_SYMBOLS);
		UserNameable component = base.GetComponent<UserNameable>();
		if (component.savedName == "" || component.savedName == BUILDINGS.PREFABS.GEOTHERMALVENT.NAME)
		{
			component.SetName(GeothermalVent.GenerateName());
		}
		this.SimRegister();
		base.smi.StartSM();
	}

	// Token: 0x06002EEC RID: 12012 RVA: 0x0010D0C8 File Offset: 0x0010B2C8
	[OnDeserialized]
	internal void OnDeserializedMethod()
	{
		bool flag = false;
		for (int i = 0; i < this.availableMaterial.Count; i++)
		{
			GeothermalVent.ElementInfo elementInfo = this.availableMaterial[i];
			Element element = ElementLoader.FindElementByHash(elementInfo.elementHash);
			if (element == null)
			{
				element = ElementLoader.FindElementByHash(SimHashes.Steam);
				elementInfo.elementHash = SimHashes.Steam;
				elementInfo.isSolid = false;
			}
			elementInfo.elementIdx = element.idx;
			this.availableMaterial[i] = elementInfo;
		}
		if (flag)
		{
			global::Debug.LogWarning("Invalid geothermal vent content in save was converted to steam on load.");
		}
	}

	// Token: 0x06002EED RID: 12013 RVA: 0x0010D150 File Offset: 0x0010B350
	protected void SimRegister()
	{
		this.onBlockedHandle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(new global::System.Action(this.OnSimBlockedCallback), true));
		this.onUnblockedHandle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(new global::System.Action(this.OnSimUnblockedCallback), true));
		SimMessages.AddElementEmitter(float.MaxValue, Game.Instance.simComponentCallbackManager.Add(new Action<int, object>(GeothermalVent.OnSimRegisteredCallback), this, "GeothermalVentElementEmitter").index, this.onBlockedHandle.index, this.onUnblockedHandle.index);
	}

	// Token: 0x06002EEE RID: 12014 RVA: 0x0010D1F4 File Offset: 0x0010B3F4
	protected void OnSimBlockedCallback()
	{
		this.overpressure = true;
	}

	// Token: 0x06002EEF RID: 12015 RVA: 0x0010D1FD File Offset: 0x0010B3FD
	protected void OnSimUnblockedCallback()
	{
		this.overpressure = false;
	}

	// Token: 0x06002EF0 RID: 12016 RVA: 0x0010D206 File Offset: 0x0010B406
	protected static void OnSimRegisteredCallback(int handle, object data)
	{
		((GeothermalVent)data).OnSimRegisteredImpl(handle);
	}

	// Token: 0x06002EF1 RID: 12017 RVA: 0x0010D214 File Offset: 0x0010B414
	protected void OnSimRegisteredImpl(int handle)
	{
		global::Debug.Assert(this.emitterInfo.simHandle == -1, "?! too many handles registered");
		this.emitterInfo.simHandle = handle;
	}

	// Token: 0x06002EF2 RID: 12018 RVA: 0x0010D23A File Offset: 0x0010B43A
	protected void SimUnregister()
	{
		if (Sim.IsValidHandle(this.emitterInfo.simHandle))
		{
			SimMessages.RemoveElementEmitter(-1, this.emitterInfo.simHandle);
		}
		this.emitterInfo.simHandle = -1;
	}

	// Token: 0x06002EF3 RID: 12019 RVA: 0x0010D26B File Offset: 0x0010B46B
	protected override void OnCleanUp()
	{
		Game.Instance.ManualReleaseHandle(this.onBlockedHandle);
		Game.Instance.ManualReleaseHandle(this.onUnblockedHandle);
		Components.GeothermalVents.Remove(base.gameObject.GetMyWorldId(), this);
		base.OnCleanUp();
	}

	// Token: 0x06002EF4 RID: 12020 RVA: 0x0010D2AC File Offset: 0x0010B4AC
	protected void OnMassEmitted(ushort element, float mass)
	{
		bool flag = false;
		for (int i = 0; i < this.availableMaterial.Count; i++)
		{
			if (this.availableMaterial[i].elementIdx == element)
			{
				GeothermalVent.ElementInfo elementInfo = this.availableMaterial[i];
				elementInfo.mass -= mass;
				flag |= elementInfo.mass <= 0f;
				this.availableMaterial[i] = elementInfo;
				break;
			}
		}
		if (flag)
		{
			this.RecomputeEmissions();
		}
	}

	// Token: 0x06002EF5 RID: 12021 RVA: 0x0010D32C File Offset: 0x0010B52C
	public void SpawnKeepsake()
	{
		GameObject keepsakePrefab = Assets.GetPrefab("keepsake_geothermalplant");
		if (keepsakePrefab != null)
		{
			base.GetComponent<KBatchedAnimController>().Play("pooped", KAnim.PlayMode.Once, 1f, 0f);
			GameScheduler.Instance.Schedule("UncorkPoopAnim", 1.5f, delegate(object data)
			{
				this.GetComponent<KBatchedAnimController>().Play("uncork", KAnim.PlayMode.Once, 1f, 0f);
			}, null, null);
			GameScheduler.Instance.Schedule("UncorkPoopFX", 2f, delegate(object data)
			{
				Game.Instance.SpawnFX(SpawnFXHashes.MissileExplosion, this.transform.GetPosition() + Vector3.up * 3f, 0f);
			}, null, null);
			GameScheduler.Instance.Schedule("SpawnGeothermalKeepsake", 3.75f, delegate(object data)
			{
				Vector3 position = this.transform.GetPosition();
				position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingFront);
				GameObject gameObject = Util.KInstantiate(keepsakePrefab, position);
				gameObject.SetActive(true);
				new UpgradeFX.Instance(gameObject.GetComponent<KMonoBehaviour>(), new Vector3(0f, -0.5f, -0.1f)).StartSM();
			}, null, null);
		}
	}

	// Token: 0x06002EF6 RID: 12022 RVA: 0x0010D3F5 File Offset: 0x0010B5F5
	public bool IsOverPressure()
	{
		return this.overpressure;
	}

	// Token: 0x06002EF7 RID: 12023 RVA: 0x0010D400 File Offset: 0x0010B600
	protected void RecomputeEmissions()
	{
		this.availableMaterial.Sort();
		while (this.availableMaterial.Count > 0 && this.availableMaterial[this.availableMaterial.Count - 1].mass <= 0f)
		{
			this.availableMaterial.RemoveAt(this.availableMaterial.Count - 1);
		}
		int num = 0;
		using (List<GeothermalVent.ElementInfo>.Enumerator enumerator = this.availableMaterial.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.isSolid)
				{
					num++;
				}
			}
		}
		if (num > 0)
		{
			int num2 = global::UnityEngine.Random.Range(0, this.availableMaterial.Count);
			while (this.availableMaterial[num2].isSolid)
			{
				num2 = (num2 + 1) % this.availableMaterial.Count;
			}
			this.emitterInfo.element = this.availableMaterial[num2];
			this.emitterInfo.element.diseaseCount = (int)((float)this.availableMaterial[num2].diseaseCount * this.emitterInfo.element.mass / this.availableMaterial[num2].mass);
		}
		else
		{
			this.emitterInfo.element.elementIdx = 0;
			this.emitterInfo.element.mass = 0f;
		}
		this.emitterInfo.dirty = true;
	}

	// Token: 0x06002EF8 RID: 12024 RVA: 0x0010D580 File Offset: 0x0010B780
	public void addMaterial(GeothermalVent.ElementInfo info)
	{
		this.availableMaterial.Add(info);
		this.recentMass = this.MaterialAvailable();
	}

	// Token: 0x06002EF9 RID: 12025 RVA: 0x0010D59C File Offset: 0x0010B79C
	public bool HasMaterial()
	{
		bool flag = this.availableMaterial.Count != 0;
		if (flag != this.logicPorts.GetOutputValue("GEOTHERMAL_VENT_STATUS_PORT") > 0)
		{
			this.logicPorts.SendSignal("GEOTHERMAL_VENT_STATUS_PORT", flag ? 1 : 0);
		}
		return flag;
	}

	// Token: 0x06002EFA RID: 12026 RVA: 0x0010D5F0 File Offset: 0x0010B7F0
	public float MaterialAvailable()
	{
		float num = 0f;
		foreach (GeothermalVent.ElementInfo elementInfo in this.availableMaterial)
		{
			num += elementInfo.mass;
		}
		return num;
	}

	// Token: 0x06002EFB RID: 12027 RVA: 0x0010D64C File Offset: 0x0010B84C
	public bool IsEntombed()
	{
		return this.entombVulnerable.GetEntombed;
	}

	// Token: 0x06002EFC RID: 12028 RVA: 0x0010D659 File Offset: 0x0010B859
	public bool CanVent()
	{
		return !this.HasMaterial() && !this.IsEntombed();
	}

	// Token: 0x06002EFD RID: 12029 RVA: 0x0010D66E File Offset: 0x0010B86E
	public bool IsVentConnected()
	{
		return !(this.connectedToggler == null) && this.connectedToggler.IsConnected;
	}

	// Token: 0x06002EFE RID: 12030 RVA: 0x0010D68C File Offset: 0x0010B88C
	public void EmitSolidChunk()
	{
		int num = 0;
		foreach (GeothermalVent.ElementInfo elementInfo in this.availableMaterial)
		{
			if (elementInfo.isSolid && elementInfo.mass > 0f)
			{
				num++;
			}
		}
		if (num == 0)
		{
			return;
		}
		int num2 = global::UnityEngine.Random.Range(0, this.availableMaterial.Count);
		while (!this.availableMaterial[num2].isSolid)
		{
			num2 = (num2 + 1) % this.availableMaterial.Count;
		}
		GeothermalVent.ElementInfo elementInfo2 = this.availableMaterial[num2];
		if (ElementLoader.elements[(int)this.availableMaterial[num2].elementIdx] == null)
		{
			return;
		}
		bool flag = global::UnityEngine.Random.value >= 0.5f;
		float num3 = GeothermalVentConfig.INITIAL_DEBRIS_ANGLE.Get() * 3.1415927f / 180f;
		Vector2 normalized = new Vector2(-Mathf.Cos(num3), Mathf.Sin(num3));
		if (flag)
		{
			normalized.x = -normalized.x;
		}
		normalized = normalized.normalized;
		normalized * GeothermalVentConfig.INITIAL_DEBRIS_VELOCIOTY.Get();
		float num4 = Math.Min(GeothermalVentConfig.DEBRIS_MASS_KG.Get(), elementInfo2.mass);
		if (elementInfo2.mass - num4 < GeothermalVentConfig.DEBRIS_MASS_KG.min)
		{
			num4 = elementInfo2.mass;
		}
		if (num4 < 0.01f)
		{
			elementInfo2.mass = 0f;
			this.availableMaterial[num2] = elementInfo2;
			return;
		}
		int num5 = (int)((float)elementInfo2.diseaseCount * num4 / elementInfo2.mass);
		Vector3 vector = Grid.CellToPos(this.emitterInfo.cell, CellAlignment.Top, Grid.SceneLayer.BuildingFront);
		Game.Instance.SpawnFX(SpawnFXHashes.MeteorImpactDust, vector, 0f);
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(MiniCometConfig.ID), vector);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(ElementLoader.elements[(int)elementInfo2.elementIdx].id, true);
		component.Mass = num4;
		component.Temperature = elementInfo2.temperature;
		MiniComet component2 = gameObject.GetComponent<MiniComet>();
		component2.diseaseIdx = elementInfo2.diseaseIdx;
		component2.addDiseaseCount = num5;
		gameObject.SetActive(true);
		elementInfo2.diseaseCount -= num5;
		elementInfo2.mass -= num4;
		this.availableMaterial[num2] = elementInfo2;
	}

	// Token: 0x06002EFF RID: 12031 RVA: 0x0010D8F4 File Offset: 0x0010BAF4
	public void Sim200ms(float dt)
	{
		if (dt > 0f)
		{
			this.unsafeSim200ms(dt);
		}
	}

	// Token: 0x06002F00 RID: 12032 RVA: 0x0010D908 File Offset: 0x0010BB08
	private unsafe void unsafeSim200ms(float dt)
	{
		if (Sim.IsValidHandle(this.emitterInfo.simHandle))
		{
			if (this.emitterInfo.dirty)
			{
				SimMessages.ModifyElementEmitter(this.emitterInfo.simHandle, this.emitterInfo.cell, 1, ElementLoader.elements[(int)this.emitterInfo.element.elementIdx].id, 0.2f, Math.Min(3f, this.emitterInfo.element.mass), this.emitterInfo.element.temperature, 120f, this.emitterInfo.element.diseaseIdx, this.emitterInfo.element.diseaseCount);
				this.emitterInfo.dirty = false;
			}
			int handleIndex = Sim.GetHandleIndex(this.emitterInfo.simHandle);
			Sim.EmittedMassInfo emittedMassInfo = Game.Instance.simData.emittedMassEntries[handleIndex];
			if (emittedMassInfo.mass > 0f)
			{
				this.OnMassEmitted(emittedMassInfo.elemIdx, emittedMassInfo.mass);
			}
		}
		this.massMeter.SetPositionPercent(this.MaterialAvailable() / this.recentMass);
	}

	// Token: 0x06002F01 RID: 12033 RVA: 0x0010DA3C File Offset: 0x0010BC3C
	protected static bool HasProblem(GeothermalVent.StatesInstance smi)
	{
		return smi.master.IsEntombed() || smi.master.IsOverPressure();
	}

	// Token: 0x04001BBC RID: 7100
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001BBD RID: 7101
	[MyCmpAdd]
	private ConnectionManager connectedToggler;

	// Token: 0x04001BBE RID: 7102
	[MyCmpAdd]
	private EntombVulnerable entombVulnerable;

	// Token: 0x04001BBF RID: 7103
	[MyCmpReq]
	private LogicPorts logicPorts;

	// Token: 0x04001BC0 RID: 7104
	[Serialize]
	private float recentMass = 1f;

	// Token: 0x04001BC1 RID: 7105
	private MeterController massMeter;

	// Token: 0x04001BC2 RID: 7106
	[Serialize]
	private GeothermalVent.QuestProgress progress;

	// Token: 0x04001BC3 RID: 7107
	protected GeothermalVent.EmitterInfo emitterInfo;

	// Token: 0x04001BC4 RID: 7108
	[Serialize]
	protected List<GeothermalVent.ElementInfo> availableMaterial = new List<GeothermalVent.ElementInfo>();

	// Token: 0x04001BC5 RID: 7109
	protected bool overpressure;

	// Token: 0x04001BC6 RID: 7110
	protected int debrisEmissionCell;

	// Token: 0x04001BC7 RID: 7111
	private HandleVector<Game.CallbackInfo>.Handle onBlockedHandle = HandleVector<Game.CallbackInfo>.InvalidHandle;

	// Token: 0x04001BC8 RID: 7112
	private HandleVector<Game.CallbackInfo>.Handle onUnblockedHandle = HandleVector<Game.CallbackInfo>.InvalidHandle;

	// Token: 0x020015FB RID: 5627
	private enum QuestProgress
	{
		// Token: 0x0400718E RID: 29070
		Uninitialized,
		// Token: 0x0400718F RID: 29071
		Entombed,
		// Token: 0x04007190 RID: 29072
		Complete
	}

	// Token: 0x020015FC RID: 5628
	public struct ElementInfo : IComparable
	{
		// Token: 0x0600937E RID: 37758 RVA: 0x0036B5BD File Offset: 0x003697BD
		public int CompareTo(object obj)
		{
			return -this.mass.CompareTo(((GeothermalVent.ElementInfo)obj).mass);
		}

		// Token: 0x04007191 RID: 29073
		public bool isSolid;

		// Token: 0x04007192 RID: 29074
		public SimHashes elementHash;

		// Token: 0x04007193 RID: 29075
		public ushort elementIdx;

		// Token: 0x04007194 RID: 29076
		public float mass;

		// Token: 0x04007195 RID: 29077
		public float temperature;

		// Token: 0x04007196 RID: 29078
		public byte diseaseIdx;

		// Token: 0x04007197 RID: 29079
		public int diseaseCount;
	}

	// Token: 0x020015FD RID: 5629
	public struct EmitterInfo
	{
		// Token: 0x04007198 RID: 29080
		public int simHandle;

		// Token: 0x04007199 RID: 29081
		public int cell;

		// Token: 0x0400719A RID: 29082
		public GeothermalVent.ElementInfo element;

		// Token: 0x0400719B RID: 29083
		public bool dirty;
	}

	// Token: 0x020015FE RID: 5630
	public class States : GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent>
	{
		// Token: 0x0600937F RID: 37759 RVA: 0x0036B5D8 File Offset: 0x003697D8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.EnterTransition(this.questEntombed, (GeothermalVent.StatesInstance smi) => smi.master.IsQuestEntombed()).EnterTransition(this.online, (GeothermalVent.StatesInstance smi) => !smi.master.IsQuestEntombed());
			this.questEntombed.PlayAnim("pooped").ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoVentQuestBlockage, (GeothermalVent.StatesInstance smi) => smi.master).Transition(this.online, (GeothermalVent.StatesInstance smi) => smi.master.progress == GeothermalVent.QuestProgress.Complete, UpdateRate.SIM_200ms);
			this.online.PlayAnim("on", KAnim.PlayMode.Once).defaultState = this.online.identify;
			this.online.identify.EnterTransition(this.online.inactive, new StateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.Transition.ConditionCallback(GeothermalVent.HasProblem)).EnterTransition(this.online.active, (GeothermalVent.StatesInstance smi) => !GeothermalVent.HasProblem(smi) && smi.master.HasMaterial()).EnterTransition(this.online.ready, (GeothermalVent.StatesInstance smi) => !GeothermalVent.HasProblem(smi) && !smi.master.HasMaterial() && smi.master.IsVentConnected())
				.EnterTransition(this.online.disconnected, (GeothermalVent.StatesInstance smi) => !GeothermalVent.HasProblem(smi) && !smi.master.HasMaterial() && !smi.master.IsVentConnected());
			this.online.active.defaultState = this.online.active.preVent;
			this.online.active.preVent.PlayAnim("working_pre").OnAnimQueueComplete(this.online.active.loopVent);
			this.online.active.loopVent.Enter(delegate(GeothermalVent.StatesInstance smi)
			{
				smi.master.RecomputeEmissions();
			}).Exit(delegate(GeothermalVent.StatesInstance smi)
			{
				smi.master.RecomputeEmissions();
			}).Transition(this.online.active.postVent, (GeothermalVent.StatesInstance smi) => !smi.master.HasMaterial(), UpdateRate.SIM_200ms)
				.Transition(this.online.inactive.identify, new StateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.Transition.ConditionCallback(GeothermalVent.HasProblem), UpdateRate.SIM_200ms)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoVentsVenting, (GeothermalVent.StatesInstance smi) => smi.master)
				.Update(delegate(GeothermalVent.StatesInstance smi, float dt)
				{
					if (dt > 0f)
					{
						smi.master.RecomputeEmissions();
					}
				}, UpdateRate.SIM_4000ms, false)
				.defaultState = this.online.active.loopVent.start;
			this.online.active.loopVent.start.PlayAnim("working1").OnAnimQueueComplete(this.online.active.loopVent.finish);
			this.online.active.loopVent.finish.Enter(delegate(GeothermalVent.StatesInstance smi)
			{
				smi.master.EmitSolidChunk();
			}).PlayAnim("working2").OnAnimQueueComplete(this.online.active.loopVent.start);
			this.online.active.postVent.QueueAnim("working_pst", false, null).OnAnimQueueComplete(this.online.ready);
			this.online.ready.PlayAnim("on", KAnim.PlayMode.Once).Transition(this.online.active, (GeothermalVent.StatesInstance smi) => smi.master.HasMaterial(), UpdateRate.SIM_200ms).Transition(this.online.inactive, new StateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.Transition.ConditionCallback(GeothermalVent.HasProblem), UpdateRate.SIM_200ms)
				.Transition(this.online.disconnected, (GeothermalVent.StatesInstance smi) => !smi.master.IsVentConnected(), UpdateRate.SIM_200ms)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoVentsReady, (GeothermalVent.StatesInstance smi) => smi.master);
			this.online.disconnected.PlayAnim("on", KAnim.PlayMode.Once).Transition(this.online.active, (GeothermalVent.StatesInstance smi) => smi.master.HasMaterial(), UpdateRate.SIM_200ms).Transition(this.online.inactive, new StateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.Transition.ConditionCallback(GeothermalVent.HasProblem), UpdateRate.SIM_200ms)
				.Transition(this.online.ready, (GeothermalVent.StatesInstance smi) => smi.master.IsVentConnected(), UpdateRate.SIM_200ms)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoVentsDisconnected, (GeothermalVent.StatesInstance smi) => smi.master);
			this.online.inactive.PlayAnim("over_pressure", KAnim.PlayMode.Once).Transition(this.online.identify, (GeothermalVent.StatesInstance smi) => !GeothermalVent.HasProblem(smi), UpdateRate.SIM_200ms).defaultState = this.online.inactive.identify;
			this.online.inactive.identify.EnterTransition(this.online.inactive.entombed, (GeothermalVent.StatesInstance smi) => smi.master.IsEntombed()).EnterTransition(this.online.inactive.overpressure, (GeothermalVent.StatesInstance smi) => smi.master.IsOverPressure());
			this.online.inactive.entombed.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Entombed, null).Transition(this.online.inactive.identify, (GeothermalVent.StatesInstance smi) => !smi.master.IsEntombed(), UpdateRate.SIM_200ms);
			this.online.inactive.overpressure.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoVentsOverpressure, null).EnterTransition(this.online.inactive.identify, (GeothermalVent.StatesInstance smi) => !smi.master.IsOverPressure());
		}

		// Token: 0x0400719C RID: 29084
		public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State questEntombed;

		// Token: 0x0400719D RID: 29085
		public GeothermalVent.States.OnlineStates online;

		// Token: 0x02002789 RID: 10121
		public class ActiveStates : GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State
		{
			// Token: 0x0400AF00 RID: 44800
			public GeothermalVent.States.ActiveStates.LoopStates loopVent;

			// Token: 0x0400AF01 RID: 44801
			public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State preVent;

			// Token: 0x0400AF02 RID: 44802
			public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State postVent;

			// Token: 0x02003883 RID: 14467
			public class LoopStates : GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State
			{
				// Token: 0x0400E470 RID: 58480
				public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State start;

				// Token: 0x0400E471 RID: 58481
				public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State finish;
			}
		}

		// Token: 0x0200278A RID: 10122
		public class ProblemStates : GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State
		{
			// Token: 0x0400AF03 RID: 44803
			public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State identify;

			// Token: 0x0400AF04 RID: 44804
			public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State entombed;

			// Token: 0x0400AF05 RID: 44805
			public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State overpressure;
		}

		// Token: 0x0200278B RID: 10123
		public class OnlineStates : GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State
		{
			// Token: 0x0400AF06 RID: 44806
			public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State identify;

			// Token: 0x0400AF07 RID: 44807
			public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State ready;

			// Token: 0x0400AF08 RID: 44808
			public GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.State disconnected;

			// Token: 0x0400AF09 RID: 44809
			public GeothermalVent.States.ActiveStates active;

			// Token: 0x0400AF0A RID: 44810
			public GeothermalVent.States.ProblemStates inactive;
		}
	}

	// Token: 0x020015FF RID: 5631
	public class StatesInstance : GameStateMachine<GeothermalVent.States, GeothermalVent.StatesInstance, GeothermalVent, object>.GameInstance
	{
		// Token: 0x06009381 RID: 37761 RVA: 0x0036BCE3 File Offset: 0x00369EE3
		public StatesInstance(GeothermalVent smi)
			: base(smi)
		{
		}
	}
}
