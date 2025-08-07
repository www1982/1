using System;
using System.Collections;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000ECA RID: 3786
	public sealed class IEnumerableDrawer : CollectionDrawer
	{
		// Token: 0x06007909 RID: 30985 RVA: 0x002EC0A8 File Offset: 0x002EA2A8
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.CanAssignToType<IEnumerable>();
		}

		// Token: 0x0600790A RID: 30986 RVA: 0x002EC0B0 File Offset: 0x002EA2B0
		public override bool IsEmpty(in MemberDrawContext context, in MemberDetails member)
		{
			return !((IEnumerable)member.value).GetEnumerator().MoveNext();
		}

		// Token: 0x0600790B RID: 30987 RVA: 0x002EC0CC File Offset: 0x002EA2CC
		protected override void VisitElements(CollectionDrawer.ElementVisitor visit, in MemberDrawContext context, in MemberDetails member)
		{
			IEnumerable enumerable = (IEnumerable)member.value;
			int num = 0;
			using (IEnumerator enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object el = enumerator.Current;
					visit(in context, new CollectionDrawer.Element(num, delegate
					{
						DrawerUtil.Tooltip(el.GetType());
					}, () => new
					{
						value = el
					}));
					num++;
				}
			}
		}
	}
}
