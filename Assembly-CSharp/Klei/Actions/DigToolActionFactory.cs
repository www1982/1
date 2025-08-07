using System;
using Klei.Input;

namespace Klei.Actions
{
	// Token: 0x02001021 RID: 4129
	public class DigToolActionFactory : ActionFactory<DigToolActionFactory, DigAction, DigToolActionFactory.Actions>
	{
		// Token: 0x06007F1A RID: 32538 RVA: 0x0032B877 File Offset: 0x00329A77
		protected override DigAction CreateAction(DigToolActionFactory.Actions action)
		{
			if (action == DigToolActionFactory.Actions.Immediate)
			{
				return new ImmediateDigAction();
			}
			if (action == DigToolActionFactory.Actions.ClearCell)
			{
				return new ClearCellDigAction();
			}
			if (action == DigToolActionFactory.Actions.MarkCell)
			{
				return new MarkCellDigAction();
			}
			throw new InvalidOperationException("Can not create DigAction 'Count'. Please provide a valid action.");
		}

		// Token: 0x020025FD RID: 9725
		public enum Actions
		{
			// Token: 0x0400A97F RID: 43391
			MarkCell = 145163119,
			// Token: 0x0400A980 RID: 43392
			Immediate = -1044758767,
			// Token: 0x0400A981 RID: 43393
			ClearCell = -1011242513,
			// Token: 0x0400A982 RID: 43394
			Count = -1427607121
		}
	}
}
