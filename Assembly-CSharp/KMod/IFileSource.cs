using System;
using System.Collections.Generic;
using Klei;

namespace KMod
{
	// Token: 0x02000F71 RID: 3953
	public interface IFileSource
	{
		// Token: 0x06007B84 RID: 31620
		string GetRoot();

		// Token: 0x06007B85 RID: 31621
		bool Exists();

		// Token: 0x06007B86 RID: 31622
		bool Exists(string relative_path);

		// Token: 0x06007B87 RID: 31623
		void GetTopLevelItems(List<FileSystemItem> file_system_items, string relative_root = "");

		// Token: 0x06007B88 RID: 31624
		IFileDirectory GetFileSystem();

		// Token: 0x06007B89 RID: 31625
		void CopyTo(string path, List<string> extensions = null);

		// Token: 0x06007B8A RID: 31626
		string Read(string relative_path);

		// Token: 0x06007B8B RID: 31627
		void Dispose();
	}
}
