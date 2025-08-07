using System;

namespace Klei.Actions
{
	// Token: 0x0200101F RID: 4127
	[AttributeUsage(AttributeTargets.Class)]
	public class ActionAttribute : Attribute
	{
		// Token: 0x06007F14 RID: 32532 RVA: 0x0032B7CE File Offset: 0x003299CE
		public ActionAttribute(string actionName)
		{
			this.ActionName = actionName;
		}

		// Token: 0x04005FBB RID: 24507
		public readonly string ActionName;
	}
}
