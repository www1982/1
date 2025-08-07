using System;

namespace Database
{
	// Token: 0x02000F61 RID: 3937
	public class SimpleSkillPerk : SkillPerk
	{
		// Token: 0x06007B51 RID: 31569 RVA: 0x0030D2F2 File Offset: 0x0030B4F2
		public SimpleSkillPerk(string id, string description)
			: base(id, description, null, null, null, false)
		{
		}

		// Token: 0x06007B52 RID: 31570 RVA: 0x0030D300 File Offset: 0x0030B500
		public SimpleSkillPerk(string id, string description, string[] requiredDlcIds)
			: base(id, description, null, null, null, requiredDlcIds, false)
		{
		}
	}
}
