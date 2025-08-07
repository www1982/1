using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EBD RID: 3773
	public class NullDrawer : InlineDrawer
	{
		// Token: 0x060078D9 RID: 30937 RVA: 0x002EBB39 File Offset: 0x002E9D39
		public override bool CanDrawAtDepth(int depth)
		{
			return true;
		}

		// Token: 0x060078DA RID: 30938 RVA: 0x002EBB3C File Offset: 0x002E9D3C
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value == null;
		}

		// Token: 0x060078DB RID: 30939 RVA: 0x002EBB47 File Offset: 0x002E9D47
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			ImGuiEx.SimpleField(member.name, "null");
		}
	}
}
