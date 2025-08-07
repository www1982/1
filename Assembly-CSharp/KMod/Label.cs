using System;
using System.Diagnostics;
using System.IO;
using Klei;
using Newtonsoft.Json;

namespace KMod
{
	// Token: 0x02000F74 RID: 3956
	[JsonObject(MemberSerialization.Fields)]
	[DebuggerDisplay("{title}")]
	public struct Label
	{
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06007BA0 RID: 31648 RVA: 0x00312041 File Offset: 0x00310241
		[JsonIgnore]
		private string distribution_platform_name
		{
			get
			{
				return this.distribution_platform.ToString();
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06007BA1 RID: 31649 RVA: 0x00312054 File Offset: 0x00310254
		[JsonIgnore]
		public string install_path
		{
			get
			{
				return FileSystem.Normalize(Path.Combine(Manager.GetDirectory(), this.distribution_platform_name, this.id));
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06007BA2 RID: 31650 RVA: 0x00312071 File Offset: 0x00310271
		[JsonIgnore]
		public string defaultStaticID
		{
			get
			{
				return this.id + "." + this.distribution_platform.ToString();
			}
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x00312094 File Offset: 0x00310294
		public override string ToString()
		{
			return this.title;
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x0031209C File Offset: 0x0031029C
		public bool Match(Label rhs)
		{
			return this.id == rhs.id && this.distribution_platform == rhs.distribution_platform;
		}

		// Token: 0x04005AF2 RID: 23282
		public Label.DistributionPlatform distribution_platform;

		// Token: 0x04005AF3 RID: 23283
		public string id;

		// Token: 0x04005AF4 RID: 23284
		public string title;

		// Token: 0x04005AF5 RID: 23285
		public long version;

		// Token: 0x0200210A RID: 8458
		public enum DistributionPlatform
		{
			// Token: 0x04009721 RID: 38689
			Local,
			// Token: 0x04009722 RID: 38690
			Steam,
			// Token: 0x04009723 RID: 38691
			Epic,
			// Token: 0x04009724 RID: 38692
			Rail,
			// Token: 0x04009725 RID: 38693
			Dev
		}
	}
}
