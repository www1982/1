using System;
using ImGuiNET;
using UnityEngine;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000ECB RID: 3787
	public class UnityObjectDrawer : PlainCSharpObjectDrawer
	{
		// Token: 0x0600790D RID: 30989 RVA: 0x002EC15C File Offset: 0x002EA35C
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is global::UnityEngine.Object;
		}

		// Token: 0x0600790E RID: 30990 RVA: 0x002EC16C File Offset: 0x002EA36C
		protected override void DrawCustom(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			global::UnityEngine.Object @object = (global::UnityEngine.Object)member.value;
			ImGuiTreeNodeFlags imGuiTreeNodeFlags = ImGuiTreeNodeFlags.None;
			if (context.default_open && depth <= 0)
			{
				imGuiTreeNodeFlags |= ImGuiTreeNodeFlags.DefaultOpen;
			}
			bool flag = ImGui.TreeNodeEx(member.name, imGuiTreeNodeFlags);
			DrawerUtil.Tooltip(member.type);
			if (flag)
			{
				base.DrawContents(in context, in member, depth);
				ImGui.TreePop();
			}
		}
	}
}
