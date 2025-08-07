using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EBF RID: 3775
	public sealed class FallbackDrawer : SimpleDrawer
	{
		// Token: 0x060078E1 RID: 30945 RVA: 0x002EBB9B File Offset: 0x002E9D9B
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return true;
		}

		// Token: 0x060078E2 RID: 30946 RVA: 0x002EBB9E File Offset: 0x002E9D9E
		public override bool CanDrawAtDepth(int depth)
		{
			return true;
		}
	}
}
