using System;
using ImGuiNET;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC7 RID: 3783
	public abstract class CollectionDrawer : MemberDrawer
	{
		// Token: 0x060078F9 RID: 30969
		public abstract bool IsEmpty(in MemberDrawContext context, in MemberDetails member);

		// Token: 0x060078FA RID: 30970 RVA: 0x002EBE54 File Offset: 0x002EA054
		public override MemberDrawType GetDrawType(in MemberDrawContext context, in MemberDetails member)
		{
			if (this.IsEmpty(in context, in member))
			{
				return MemberDrawType.Inline;
			}
			return MemberDrawType.Custom;
		}

		// Token: 0x060078FB RID: 30971 RVA: 0x002EBE63 File Offset: 0x002EA063
		protected sealed override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			Debug.Assert(this.IsEmpty(in context, in member));
			this.DrawEmpty(in context, in member);
		}

		// Token: 0x060078FC RID: 30972 RVA: 0x002EBE7A File Offset: 0x002EA07A
		protected sealed override void DrawCustom(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			Debug.Assert(!this.IsEmpty(in context, in member));
			this.DrawWithContents(in context, in member, depth);
		}

		// Token: 0x060078FD RID: 30973 RVA: 0x002EBE95 File Offset: 0x002EA095
		private void DrawEmpty(in MemberDrawContext context, in MemberDetails member)
		{
			ImGui.Text(member.name + "(empty)");
		}

		// Token: 0x060078FE RID: 30974 RVA: 0x002EBEAC File Offset: 0x002EA0AC
		private void DrawWithContents(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			CollectionDrawer.<>c__DisplayClass5_0 CS$<>8__locals1 = new CollectionDrawer.<>c__DisplayClass5_0();
			CS$<>8__locals1.depth = depth;
			ImGuiTreeNodeFlags imGuiTreeNodeFlags = ImGuiTreeNodeFlags.None;
			if (context.default_open && CS$<>8__locals1.depth <= 0)
			{
				imGuiTreeNodeFlags |= ImGuiTreeNodeFlags.DefaultOpen;
			}
			bool flag = ImGui.TreeNodeEx(member.name, imGuiTreeNodeFlags);
			DrawerUtil.Tooltip(member.type);
			if (flag)
			{
				this.VisitElements(new CollectionDrawer.ElementVisitor(CS$<>8__locals1.<DrawWithContents>g__Visitor|0), in context, in member);
				ImGui.TreePop();
			}
		}

		// Token: 0x060078FF RID: 30975
		protected abstract void VisitElements(CollectionDrawer.ElementVisitor visit, in MemberDrawContext context, in MemberDetails member);

		// Token: 0x020020CD RID: 8397
		// (Invoke) Token: 0x0600B73D RID: 46909
		protected delegate void ElementVisitor(in MemberDrawContext context, CollectionDrawer.Element element);

		// Token: 0x020020CE RID: 8398
		protected struct Element
		{
			// Token: 0x0600B740 RID: 46912 RVA: 0x003E4154 File Offset: 0x003E2354
			public Element(string node_name, global::System.Action draw_tooltip, Func<object> get_object_to_inspect)
			{
				this.node_name = node_name;
				this.draw_tooltip = draw_tooltip;
				this.get_object_to_inspect = get_object_to_inspect;
			}

			// Token: 0x0600B741 RID: 46913 RVA: 0x003E416B File Offset: 0x003E236B
			public Element(int index, global::System.Action draw_tooltip, Func<object> get_object_to_inspect)
			{
				this = new CollectionDrawer.Element(string.Format("[{0}]", index), draw_tooltip, get_object_to_inspect);
			}

			// Token: 0x04009556 RID: 38230
			public readonly string node_name;

			// Token: 0x04009557 RID: 38231
			public readonly global::System.Action draw_tooltip;

			// Token: 0x04009558 RID: 38232
			public readonly Func<object> get_object_to_inspect;
		}
	}
}
