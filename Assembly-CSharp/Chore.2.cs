using System;
using UnityEngine;

// Token: 0x020004AB RID: 1195
public class Chore<StateMachineInstanceType> : StandardChoreBase, IStateMachineTarget where StateMachineInstanceType : StateMachine.Instance
{
	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06001939 RID: 6457 RVA: 0x0008B748 File Offset: 0x00089948
	// (set) Token: 0x0600193A RID: 6458 RVA: 0x0008B750 File Offset: 0x00089950
	public StateMachineInstanceType smi { get; protected set; }

	// Token: 0x0600193B RID: 6459 RVA: 0x0008B759 File Offset: 0x00089959
	protected override StateMachine.Instance GetSMI()
	{
		return this.smi;
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x0008B766 File Offset: 0x00089966
	public int Subscribe(int hash, Action<object> handler)
	{
		return this.GetComponent<KPrefabID>().Subscribe(hash, handler);
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x0008B775 File Offset: 0x00089975
	public void Unsubscribe(int hash, Action<object> handler)
	{
		this.GetComponent<KPrefabID>().Unsubscribe(hash, handler);
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x0008B784 File Offset: 0x00089984
	public void Unsubscribe(int id)
	{
		this.GetComponent<KPrefabID>().Unsubscribe(id);
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x0008B792 File Offset: 0x00089992
	public void Trigger(int hash, object data = null)
	{
		this.GetComponent<KPrefabID>().Trigger(hash, data);
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x0008B7A1 File Offset: 0x000899A1
	public ComponentType GetComponent<ComponentType>()
	{
		return this.target.GetComponent<ComponentType>();
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06001941 RID: 6465 RVA: 0x0008B7AE File Offset: 0x000899AE
	public override GameObject gameObject
	{
		get
		{
			return this.target.gameObject;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06001942 RID: 6466 RVA: 0x0008B7BB File Offset: 0x000899BB
	public Transform transform
	{
		get
		{
			return this.target.gameObject.transform;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06001943 RID: 6467 RVA: 0x0008B7CD File Offset: 0x000899CD
	public string name
	{
		get
		{
			return this.gameObject.name;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06001944 RID: 6468 RVA: 0x0008B7DA File Offset: 0x000899DA
	public override bool isNull
	{
		get
		{
			return this.target.isNull;
		}
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x0008B7E8 File Offset: 0x000899E8
	public Chore(ChoreType chore_type, IStateMachineTarget target, ChoreProvider chore_provider, bool run_until_complete = true, Action<Chore> on_complete = null, Action<Chore> on_begin = null, Action<Chore> on_end = null, PriorityScreen.PriorityClass master_priority_class = PriorityScreen.PriorityClass.basic, int master_priority_value = 5, bool is_preemptable = false, bool allow_in_context_menu = true, int priority_mod = 0, bool add_to_daily_report = false, ReportManager.ReportType report_type = ReportManager.ReportType.WorkTime)
		: base(chore_type, target, chore_provider, run_until_complete, on_complete, on_begin, on_end, master_priority_class, master_priority_value, is_preemptable, allow_in_context_menu, priority_mod, add_to_daily_report, report_type)
	{
		target.Subscribe(1969584890, new Action<object>(this.OnTargetDestroyed));
		this.reportType = report_type;
		this.addToDailyReport = add_to_daily_report;
		if (this.addToDailyReport)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, 1f, chore_type.Name, GameUtil.GetChoreName(this, null));
		}
	}

	// Token: 0x06001946 RID: 6470 RVA: 0x0008B861 File Offset: 0x00089A61
	public override string ResolveString(string str)
	{
		if (!this.target.isNull)
		{
			str = str.Replace("{Target}", this.target.gameObject.GetProperName());
		}
		return base.ResolveString(str);
	}

	// Token: 0x06001947 RID: 6471 RVA: 0x0008B894 File Offset: 0x00089A94
	public override void Cleanup()
	{
		base.Cleanup();
		if (this.target != null)
		{
			this.target.Unsubscribe(1969584890, new Action<object>(this.OnTargetDestroyed));
		}
		if (this.onCleanup != null)
		{
			this.onCleanup(this);
		}
	}

	// Token: 0x06001948 RID: 6472 RVA: 0x0008B8D4 File Offset: 0x00089AD4
	private void OnTargetDestroyed(object data)
	{
		this.Cancel("Target Destroyed");
	}

	// Token: 0x06001949 RID: 6473 RVA: 0x0008B8E1 File Offset: 0x00089AE1
	public override bool CanPreempt(Chore.Precondition.Context context)
	{
		return base.CanPreempt(context);
	}
}
