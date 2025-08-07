using System;

// Token: 0x020004E5 RID: 1253
public class SafetyChecker
{
	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00093E17 File Offset: 0x00092017
	// (set) Token: 0x06001ACE RID: 6862 RVA: 0x00093E1F File Offset: 0x0009201F
	public SafetyChecker.Condition[] conditions { get; private set; }

	// Token: 0x06001ACF RID: 6863 RVA: 0x00093E28 File Offset: 0x00092028
	public SafetyChecker(SafetyChecker.Condition[] conditions)
	{
		this.conditions = conditions;
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x00093E38 File Offset: 0x00092038
	public int GetSafetyConditions(int cell, int cost, SafetyChecker.Context context, out bool all_conditions_met)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.conditions.Length; i++)
		{
			SafetyChecker.Condition condition = this.conditions[i];
			if (condition.callback(cell, cost, context))
			{
				num |= condition.mask;
				num2++;
			}
		}
		all_conditions_met = num2 == this.conditions.Length;
		return num;
	}

	// Token: 0x02001336 RID: 4918
	public struct Condition
	{
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06008909 RID: 35081 RVA: 0x0034AB50 File Offset: 0x00348D50
		// (set) Token: 0x0600890A RID: 35082 RVA: 0x0034AB58 File Offset: 0x00348D58
		public SafetyChecker.Condition.Callback callback { readonly get; private set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600890B RID: 35083 RVA: 0x0034AB61 File Offset: 0x00348D61
		// (set) Token: 0x0600890C RID: 35084 RVA: 0x0034AB69 File Offset: 0x00348D69
		public int mask { readonly get; private set; }

		// Token: 0x0600890D RID: 35085 RVA: 0x0034AB72 File Offset: 0x00348D72
		public Condition(string id, int condition_mask, SafetyChecker.Condition.Callback condition_callback)
		{
			this = default(SafetyChecker.Condition);
			this.callback = condition_callback;
			this.mask = condition_mask;
		}

		// Token: 0x0200269C RID: 9884
		// (Invoke) Token: 0x0600C457 RID: 50263
		public delegate bool Callback(int cell, int cost, SafetyChecker.Context context);
	}

	// Token: 0x02001337 RID: 4919
	public struct Context
	{
		// Token: 0x0600890E RID: 35086 RVA: 0x0034AB8C File Offset: 0x00348D8C
		public Context(KMonoBehaviour cmp)
		{
			this.cell = Grid.PosToCell(cmp);
			this.navigator = cmp.GetComponent<Navigator>();
			this.oxygenBreather = cmp.GetComponent<OxygenBreather>();
			this.minionBrain = cmp.GetComponent<MinionBrain>();
			this.temperatureTransferer = cmp.GetComponent<SimTemperatureTransfer>();
			this.primaryElement = cmp.GetComponent<PrimaryElement>();
			this.worldID = this.navigator.GetMyWorldId();
		}

		// Token: 0x040068C7 RID: 26823
		public Navigator navigator;

		// Token: 0x040068C8 RID: 26824
		public OxygenBreather oxygenBreather;

		// Token: 0x040068C9 RID: 26825
		public SimTemperatureTransfer temperatureTransferer;

		// Token: 0x040068CA RID: 26826
		public PrimaryElement primaryElement;

		// Token: 0x040068CB RID: 26827
		public MinionBrain minionBrain;

		// Token: 0x040068CC RID: 26828
		public int worldID;

		// Token: 0x040068CD RID: 26829
		public int cell;
	}
}
