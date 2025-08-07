using System;
using ImGuiNET;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000ECC RID: 3788
	public class PlainCSharpObjectDrawer : MemberDrawer
	{
		// Token: 0x06007910 RID: 30992 RVA: 0x002EC1C7 File Offset: 0x002EA3C7
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return true;
		}

		// Token: 0x06007911 RID: 30993 RVA: 0x002EC1CA File Offset: 0x002EA3CA
		public override MemberDrawType GetDrawType(in MemberDrawContext context, in MemberDetails member)
		{
			return MemberDrawType.Custom;
		}

		// Token: 0x06007912 RID: 30994 RVA: 0x002EC1CD File Offset: 0x002EA3CD
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06007913 RID: 30995 RVA: 0x002EC1D4 File Offset: 0x002EA3D4
		protected override void DrawCustom(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			ImGuiTreeNodeFlags imGuiTreeNodeFlags = ImGuiTreeNodeFlags.None;
			if (context.default_open && depth <= 0)
			{
				imGuiTreeNodeFlags |= ImGuiTreeNodeFlags.DefaultOpen;
			}
			bool flag = ImGui.TreeNodeEx(member.name, imGuiTreeNodeFlags);
			DrawerUtil.Tooltip(member.type);
			if (flag)
			{
				this.DrawContents(in context, in member, depth);
				ImGui.TreePop();
			}
		}

		// Token: 0x06007914 RID: 30996 RVA: 0x002EC21B File Offset: 0x002EA41B
		protected virtual void DrawContents(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			DrawerUtil.DrawObjectContents(member.value, in context, depth + 1);
		}
	}
}
