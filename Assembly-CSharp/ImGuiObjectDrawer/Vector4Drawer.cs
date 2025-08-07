using System;
using UnityEngine;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC6 RID: 3782
	public sealed class Vector4Drawer : InlineDrawer
	{
		// Token: 0x060078F6 RID: 30966 RVA: 0x002EBDCF File Offset: 0x002E9FCF
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is Vector4;
		}

		// Token: 0x060078F7 RID: 30967 RVA: 0x002EBDE0 File Offset: 0x002E9FE0
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			Vector4 vector = (Vector4)member.value;
			ImGuiEx.SimpleField(member.name, string.Format("( {0}, {1}, {2}, {3} )", new object[] { vector.x, vector.y, vector.z, vector.w }));
		}
	}
}
