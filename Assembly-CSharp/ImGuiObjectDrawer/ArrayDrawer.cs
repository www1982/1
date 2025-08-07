using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC8 RID: 3784
	public sealed class ArrayDrawer : CollectionDrawer
	{
		// Token: 0x06007901 RID: 30977 RVA: 0x002EBF18 File Offset: 0x002EA118
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.type.IsArray;
		}

		// Token: 0x06007902 RID: 30978 RVA: 0x002EBF25 File Offset: 0x002EA125
		public override bool IsEmpty(in MemberDrawContext context, in MemberDetails member)
		{
			return ((Array)member.value).Length == 0;
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x002EBF3C File Offset: 0x002EA13C
		protected override void VisitElements(CollectionDrawer.ElementVisitor visit, in MemberDrawContext context, in MemberDetails member)
		{
			ArrayDrawer.<>c__DisplayClass2_0 CS$<>8__locals1 = new ArrayDrawer.<>c__DisplayClass2_0();
			CS$<>8__locals1.array = (Array)member.value;
			int i;
			int num;
			for (i = 0; i < CS$<>8__locals1.array.Length; i = num)
			{
				int j = i;
				global::System.Action action;
				if ((action = CS$<>8__locals1.<>9__0) == null)
				{
					action = (CS$<>8__locals1.<>9__0 = delegate
					{
						DrawerUtil.Tooltip(CS$<>8__locals1.array.GetType().GetElementType());
					});
				}
				visit(in context, new CollectionDrawer.Element(j, action, () => new
				{
					value = CS$<>8__locals1.array.GetValue(i)
				}));
				num = i + 1;
			}
		}
	}
}
