using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EBC RID: 3772
	public abstract class InlineDrawer : MemberDrawer
	{
		// Token: 0x060078D6 RID: 30934 RVA: 0x002EBB24 File Offset: 0x002E9D24
		public sealed override MemberDrawType GetDrawType(in MemberDrawContext context, in MemberDetails member)
		{
			return MemberDrawType.Inline;
		}

		// Token: 0x060078D7 RID: 30935 RVA: 0x002EBB27 File Offset: 0x002E9D27
		protected sealed override void DrawCustom(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			this.DrawInline(in context, in member);
		}
	}
}
