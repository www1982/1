using System;
using UnityEngine;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC5 RID: 3781
	public sealed class Vector3Drawer : InlineDrawer
	{
		// Token: 0x060078F3 RID: 30963 RVA: 0x002EBD68 File Offset: 0x002E9F68
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is Vector3;
		}

		// Token: 0x060078F4 RID: 30964 RVA: 0x002EBD78 File Offset: 0x002E9F78
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			Vector3 vector = (Vector3)member.value;
			ImGuiEx.SimpleField(member.name, string.Format("( {0}, {1}, {2} )", vector.x, vector.y, vector.z));
		}
	}
}
