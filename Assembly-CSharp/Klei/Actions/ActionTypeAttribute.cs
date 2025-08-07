using System;

namespace Klei.Actions
{
	// Token: 0x0200101E RID: 4126
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class ActionTypeAttribute : Attribute
	{
		// Token: 0x06007F0F RID: 32527 RVA: 0x0032B744 File Offset: 0x00329944
		public ActionTypeAttribute(string groupName, string typeName, bool generateConfig = true)
		{
			this.TypeName = typeName;
			this.GroupName = groupName;
			this.GenerateConfig = generateConfig;
		}

		// Token: 0x06007F10 RID: 32528 RVA: 0x0032B764 File Offset: 0x00329964
		public static bool operator ==(ActionTypeAttribute lhs, ActionTypeAttribute rhs)
		{
			bool flag = object.Equals(lhs, null);
			bool flag2 = object.Equals(rhs, null);
			if (flag || flag2)
			{
				return flag == flag2;
			}
			return lhs.TypeName == rhs.TypeName && lhs.GroupName == rhs.GroupName;
		}

		// Token: 0x06007F11 RID: 32529 RVA: 0x0032B7B1 File Offset: 0x003299B1
		public static bool operator !=(ActionTypeAttribute lhs, ActionTypeAttribute rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x0032B7BD File Offset: 0x003299BD
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06007F13 RID: 32531 RVA: 0x0032B7C6 File Offset: 0x003299C6
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04005FB8 RID: 24504
		public readonly string TypeName;

		// Token: 0x04005FB9 RID: 24505
		public readonly string GroupName;

		// Token: 0x04005FBA RID: 24506
		public readonly bool GenerateConfig;
	}
}
