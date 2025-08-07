using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EBE RID: 3774
	public class SimpleDrawer : InlineDrawer
	{
		// Token: 0x060078DD RID: 30941 RVA: 0x002EBB61 File Offset: 0x002E9D61
		public override bool CanDrawAtDepth(int depth)
		{
			return true;
		}

		// Token: 0x060078DE RID: 30942 RVA: 0x002EBB64 File Offset: 0x002E9D64
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.type.IsPrimitive || member.CanAssignToType<string>();
		}

		// Token: 0x060078DF RID: 30943 RVA: 0x002EBB7B File Offset: 0x002E9D7B
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			ImGuiEx.SimpleField(member.name, member.value.ToString());
		}
	}
}
