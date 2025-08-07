using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC2 RID: 3778
	public sealed class HashedStringDrawer : InlineDrawer
	{
		// Token: 0x060078EA RID: 30954 RVA: 0x002EBC13 File Offset: 0x002E9E13
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is HashedString;
		}

		// Token: 0x060078EB RID: 30955 RVA: 0x002EBC24 File Offset: 0x002E9E24
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			HashedString hashedString = (HashedString)member.value;
			string text = hashedString.ToString();
			string text2 = "0x" + hashedString.HashValue.ToString("X");
			ImGuiEx.SimpleField(member.name, text + " (" + text2 + ")");
		}
	}
}
