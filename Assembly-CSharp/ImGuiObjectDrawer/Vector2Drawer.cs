using System;
using UnityEngine;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC4 RID: 3780
	public sealed class Vector2Drawer : InlineDrawer
	{
		// Token: 0x060078F0 RID: 30960 RVA: 0x002EBD0A File Offset: 0x002E9F0A
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is Vector2;
		}

		// Token: 0x060078F1 RID: 30961 RVA: 0x002EBD1C File Offset: 0x002E9F1C
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			Vector2 vector = (Vector2)member.value;
			ImGuiEx.SimpleField(member.name, string.Format("( {0}, {1} )", vector.x, vector.y));
		}
	}
}
