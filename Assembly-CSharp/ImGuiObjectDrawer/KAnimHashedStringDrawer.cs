using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC3 RID: 3779
	public sealed class KAnimHashedStringDrawer : InlineDrawer
	{
		// Token: 0x060078ED RID: 30957 RVA: 0x002EBC8E File Offset: 0x002E9E8E
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is KAnimHashedString;
		}

		// Token: 0x060078EE RID: 30958 RVA: 0x002EBCA0 File Offset: 0x002E9EA0
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			KAnimHashedString kanimHashedString = (KAnimHashedString)member.value;
			string text = kanimHashedString.ToString();
			string text2 = "0x" + kanimHashedString.HashValue.ToString("X");
			ImGuiEx.SimpleField(member.name, text + " (" + text2 + ")");
		}
	}
}
