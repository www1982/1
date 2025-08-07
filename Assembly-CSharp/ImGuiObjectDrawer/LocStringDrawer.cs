using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC0 RID: 3776
	public sealed class LocStringDrawer : InlineDrawer
	{
		// Token: 0x060078E4 RID: 30948 RVA: 0x002EBBA9 File Offset: 0x002E9DA9
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.CanAssignToType<LocString>();
		}

		// Token: 0x060078E5 RID: 30949 RVA: 0x002EBBB1 File Offset: 0x002E9DB1
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			ImGuiEx.SimpleField(member.name, string.Format("{0}({1})", member.value, ((LocString)member.value).text));
		}
	}
}
