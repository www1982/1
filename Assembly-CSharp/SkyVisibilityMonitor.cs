using System;
using Database;
using UnityEngine;

// Token: 0x02000B14 RID: 2836
public class SkyVisibilityMonitor : GameStateMachine<SkyVisibilityMonitor, SkyVisibilityMonitor.Instance, IStateMachineTarget, SkyVisibilityMonitor.Def>
{
	// Token: 0x06005394 RID: 21396 RVA: 0x001E6E31 File Offset: 0x001E5031
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update(new Action<SkyVisibilityMonitor.Instance, float>(SkyVisibilityMonitor.CheckSkyVisibility), UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x06005395 RID: 21397 RVA: 0x001E6E58 File Offset: 0x001E5058
	public static void CheckSkyVisibility(SkyVisibilityMonitor.Instance smi, float dt)
	{
		bool hasSkyVisibility = smi.HasSkyVisibility;
		ValueTuple<bool, float> visibilityOf = smi.def.skyVisibilityInfo.GetVisibilityOf(smi.gameObject);
		bool item = visibilityOf.Item1;
		float item2 = visibilityOf.Item2;
		smi.Internal_SetPercentClearSky(item2);
		KSelectable component = smi.GetComponent<KSelectable>();
		component.ToggleStatusItem(Db.Get().BuildingStatusItems.SkyVisNone, !item, smi);
		component.ToggleStatusItem(Db.Get().BuildingStatusItems.SkyVisLimited, item && item2 < 1f, smi);
		if (hasSkyVisibility == item)
		{
			return;
		}
		smi.TriggerVisibilityChange();
	}

	// Token: 0x02001C1A RID: 7194
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04008529 RID: 34089
		public SkyVisibilityInfo skyVisibilityInfo;
	}

	// Token: 0x02001C1B RID: 7195
	public new class Instance : GameStateMachine<SkyVisibilityMonitor, SkyVisibilityMonitor.Instance, IStateMachineTarget, SkyVisibilityMonitor.Def>.GameInstance, BuildingStatusItems.ISkyVisInfo
	{
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600A991 RID: 43409 RVA: 0x003B850B File Offset: 0x003B670B
		public bool HasSkyVisibility
		{
			get
			{
				return this.PercentClearSky > 0f && !Mathf.Approximately(0f, this.PercentClearSky);
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600A992 RID: 43410 RVA: 0x003B852F File Offset: 0x003B672F
		public float PercentClearSky
		{
			get
			{
				return this.percentClearSky01;
			}
		}

		// Token: 0x0600A993 RID: 43411 RVA: 0x003B8537 File Offset: 0x003B6737
		public void Internal_SetPercentClearSky(float percent01)
		{
			this.percentClearSky01 = percent01;
		}

		// Token: 0x0600A994 RID: 43412 RVA: 0x003B8540 File Offset: 0x003B6740
		float BuildingStatusItems.ISkyVisInfo.GetPercentVisible01()
		{
			return this.percentClearSky01;
		}

		// Token: 0x0600A995 RID: 43413 RVA: 0x003B8548 File Offset: 0x003B6748
		public Instance(IStateMachineTarget master, SkyVisibilityMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600A996 RID: 43414 RVA: 0x003B8552 File Offset: 0x003B6752
		public override void StartSM()
		{
			base.StartSM();
			SkyVisibilityMonitor.CheckSkyVisibility(this, 0f);
			this.TriggerVisibilityChange();
		}

		// Token: 0x0600A997 RID: 43415 RVA: 0x003B856C File Offset: 0x003B676C
		public void TriggerVisibilityChange()
		{
			if (this.visibilityStatusItem != null)
			{
				base.smi.GetComponent<KSelectable>().ToggleStatusItem(this.visibilityStatusItem, !this.HasSkyVisibility, this);
			}
			base.smi.GetComponent<Operational>().SetFlag(SkyVisibilityMonitor.Instance.skyVisibilityFlag, this.HasSkyVisibility);
			if (this.SkyVisibilityChanged != null)
			{
				this.SkyVisibilityChanged();
			}
		}

		// Token: 0x0400852A RID: 34090
		private float percentClearSky01;

		// Token: 0x0400852B RID: 34091
		public global::System.Action SkyVisibilityChanged;

		// Token: 0x0400852C RID: 34092
		private StatusItem visibilityStatusItem;

		// Token: 0x0400852D RID: 34093
		private static readonly Operational.Flag skyVisibilityFlag = new Operational.Flag("sky visibility", Operational.Flag.Type.Requirement);
	}
}
