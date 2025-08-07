using System;
using System.Collections.Generic;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000EBB RID: 3771
	public class PrimaryMemberDrawerProvider : IMemberDrawerProvider
	{
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060078D3 RID: 30931 RVA: 0x002EBA80 File Offset: 0x002E9C80
		public int Priority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x060078D4 RID: 30932 RVA: 0x002EBA84 File Offset: 0x002E9C84
		public void AppendDrawersTo(List<MemberDrawer> drawers)
		{
			drawers.AddRange(new MemberDrawer[]
			{
				new NullDrawer(),
				new SimpleDrawer(),
				new LocStringDrawer(),
				new EnumDrawer(),
				new HashedStringDrawer(),
				new KAnimHashedStringDrawer(),
				new Vector2Drawer(),
				new Vector3Drawer(),
				new Vector4Drawer(),
				new UnityObjectDrawer(),
				new ArrayDrawer(),
				new IDictionaryDrawer(),
				new IEnumerableDrawer(),
				new PlainCSharpObjectDrawer(),
				new FallbackDrawer()
			});
		}
	}
}
