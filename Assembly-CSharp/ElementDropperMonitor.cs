using System;
using UnityEngine;

// Token: 0x02000865 RID: 2149
public class ElementDropperMonitor : GameStateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>
{
	// Token: 0x06003AFB RID: 15099 RVA: 0x00147BA8 File Offset: 0x00145DA8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.EventHandler(GameHashes.DeathAnimComplete, delegate(ElementDropperMonitor.Instance smi)
		{
			smi.DropDeathElement();
		});
		this.satisfied.OnSignal(this.cellChangedSignal, this.readytodrop, (ElementDropperMonitor.Instance smi) => smi.ShouldDropElement());
		this.readytodrop.ToggleBehaviour(GameTags.Creatures.WantsToDropElements, (ElementDropperMonitor.Instance smi) => true, delegate(ElementDropperMonitor.Instance smi)
		{
			smi.GoTo(this.satisfied);
		}).EventHandler(GameHashes.ObjectMovementStateChanged, delegate(ElementDropperMonitor.Instance smi, object d)
		{
			if ((GameHashes)d == GameHashes.ObjectMovementWakeUp)
			{
				smi.GoTo(this.satisfied);
			}
		});
	}

	// Token: 0x0400242F RID: 9263
	public GameStateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>.State satisfied;

	// Token: 0x04002430 RID: 9264
	public GameStateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>.State readytodrop;

	// Token: 0x04002431 RID: 9265
	public StateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>.Signal cellChangedSignal;

	// Token: 0x020017FD RID: 6141
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007786 RID: 30598
		public SimHashes dirtyEmitElement;

		// Token: 0x04007787 RID: 30599
		public float dirtyProbabilityPercent;

		// Token: 0x04007788 RID: 30600
		public float dirtyCellToTargetMass;

		// Token: 0x04007789 RID: 30601
		public float dirtyMassPerDirty;

		// Token: 0x0400778A RID: 30602
		public float dirtyMassReleaseOnDeath;

		// Token: 0x0400778B RID: 30603
		public byte emitDiseaseIdx = byte.MaxValue;

		// Token: 0x0400778C RID: 30604
		public float emitDiseasePerKg;
	}

	// Token: 0x020017FE RID: 6142
	public new class Instance : GameStateMachine<ElementDropperMonitor, ElementDropperMonitor.Instance, IStateMachineTarget, ElementDropperMonitor.Def>.GameInstance
	{
		// Token: 0x06009B12 RID: 39698 RVA: 0x0038C1A9 File Offset: 0x0038A3A9
		public Instance(IStateMachineTarget master, ElementDropperMonitor.Def def)
			: base(master, def)
		{
			Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange), "ElementDropperMonitor.Instance");
		}

		// Token: 0x06009B13 RID: 39699 RVA: 0x0038C1D5 File Offset: 0x0038A3D5
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
		}

		// Token: 0x06009B14 RID: 39700 RVA: 0x0038C1FA File Offset: 0x0038A3FA
		private void OnCellChange()
		{
			base.sm.cellChangedSignal.Trigger(this);
		}

		// Token: 0x06009B15 RID: 39701 RVA: 0x0038C20D File Offset: 0x0038A40D
		public bool ShouldDropElement()
		{
			return this.IsValidDropCell() && global::UnityEngine.Random.Range(0f, 100f) < base.def.dirtyProbabilityPercent;
		}

		// Token: 0x06009B16 RID: 39702 RVA: 0x0038C238 File Offset: 0x0038A438
		public void DropDeathElement()
		{
			this.DropElement(base.def.dirtyMassReleaseOnDeath, base.def.dirtyEmitElement, base.def.emitDiseaseIdx, Mathf.RoundToInt(base.def.dirtyMassReleaseOnDeath * base.def.dirtyMassPerDirty));
		}

		// Token: 0x06009B17 RID: 39703 RVA: 0x0038C288 File Offset: 0x0038A488
		public void DropPeriodicElement()
		{
			this.DropElement(base.def.dirtyMassPerDirty, base.def.dirtyEmitElement, base.def.emitDiseaseIdx, Mathf.RoundToInt(base.def.emitDiseasePerKg * base.def.dirtyMassPerDirty));
		}

		// Token: 0x06009B18 RID: 39704 RVA: 0x0038C2D8 File Offset: 0x0038A4D8
		public void DropElement(float mass, SimHashes element_id, byte disease_idx, int disease_count)
		{
			if (mass <= 0f)
			{
				return;
			}
			Element element = ElementLoader.FindElementByHash(element_id);
			float temperature = base.GetComponent<PrimaryElement>().Temperature;
			if (element.IsGas || element.IsLiquid)
			{
				SimMessages.AddRemoveSubstance(Grid.PosToCell(base.transform.GetPosition()), element_id, CellEventLogger.Instance.ElementConsumerSimUpdate, mass, temperature, disease_idx, disease_count, true, -1);
			}
			else if (element.IsSolid)
			{
				element.substance.SpawnResource(base.transform.GetPosition() + new Vector3(0f, 0.5f, 0f), mass, temperature, disease_idx, disease_count, false, true, false);
			}
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, element.name, base.gameObject.transform, 1.5f, false);
		}

		// Token: 0x06009B19 RID: 39705 RVA: 0x0038C3A8 File Offset: 0x0038A5A8
		public bool IsValidDropCell()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			return Grid.IsValidCell(num) && Grid.IsGas(num) && Grid.Mass[num] <= 1f;
		}
	}
}
