using System;

namespace Database
{
	// Token: 0x02000F10 RID: 3856
	public class StateMachineCategories : ResourceSet<StateMachine.Category>
	{
		// Token: 0x060079FD RID: 31229 RVA: 0x00303C60 File Offset: 0x00301E60
		public StateMachineCategories()
		{
			this.Ai = base.Add(new StateMachine.Category("Ai"));
			this.Monitor = base.Add(new StateMachine.Category("Monitor"));
			this.Chore = base.Add(new StateMachine.Category("Chore"));
			this.Misc = base.Add(new StateMachine.Category("Misc"));
		}

		// Token: 0x04005920 RID: 22816
		public StateMachine.Category Ai;

		// Token: 0x04005921 RID: 22817
		public StateMachine.Category Monitor;

		// Token: 0x04005922 RID: 22818
		public StateMachine.Category Chore;

		// Token: 0x04005923 RID: 22819
		public StateMachine.Category Misc;
	}
}
