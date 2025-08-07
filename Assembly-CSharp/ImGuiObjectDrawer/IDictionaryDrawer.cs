using System;
using System.Collections;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EC9 RID: 3785
	public sealed class IDictionaryDrawer : CollectionDrawer
	{
		// Token: 0x06007905 RID: 30981 RVA: 0x002EBFF2 File Offset: 0x002EA1F2
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.CanAssignToType<IDictionary>();
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x002EBFFA File Offset: 0x002EA1FA
		public override bool IsEmpty(in MemberDrawContext context, in MemberDetails member)
		{
			return ((IDictionary)member.value).Count == 0;
		}

		// Token: 0x06007907 RID: 30983 RVA: 0x002EC010 File Offset: 0x002EA210
		protected override void VisitElements(CollectionDrawer.ElementVisitor visit, in MemberDrawContext context, in MemberDetails member)
		{
			IDictionary dictionary = (IDictionary)member.value;
			int num = 0;
			using (IDictionaryEnumerator enumerator = dictionary.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DictionaryEntry kvp = (DictionaryEntry)enumerator.Current;
					visit(in context, new CollectionDrawer.Element(num, delegate
					{
						DrawerUtil.Tooltip(string.Format("{0} -> {1}", kvp.Key.GetType(), kvp.Value.GetType()));
					}, () => new
					{
						key = kvp.Key,
						value = kvp.Value
					}));
					num++;
				}
			}
		}
	}
}
