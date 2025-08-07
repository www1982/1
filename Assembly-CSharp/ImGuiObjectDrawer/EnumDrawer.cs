using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC1 RID: 3777
	public sealed class EnumDrawer : InlineDrawer
	{
		// Token: 0x060078E7 RID: 30951 RVA: 0x002EBBE6 File Offset: 0x002E9DE6
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.type.IsEnum;
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x002EBBF3 File Offset: 0x002E9DF3
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			ImGuiEx.SimpleField(member.name, member.value.ToString());
		}
	}
}
