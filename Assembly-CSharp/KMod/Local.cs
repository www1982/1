using System;
using System.IO;
using Klei;
using STRINGS;

namespace KMod
{
	// Token: 0x02000F6E RID: 3950
	public class Local : IDistributionPlatform
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06007B7A RID: 31610 RVA: 0x0031142F File Offset: 0x0030F62F
		// (set) Token: 0x06007B7B RID: 31611 RVA: 0x00311437 File Offset: 0x0030F637
		public string folder { get; private set; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06007B7C RID: 31612 RVA: 0x00311440 File Offset: 0x0030F640
		// (set) Token: 0x06007B7D RID: 31613 RVA: 0x00311448 File Offset: 0x0030F648
		public Label.DistributionPlatform distribution_platform { get; private set; }

		// Token: 0x06007B7E RID: 31614 RVA: 0x00311451 File Offset: 0x0030F651
		public string GetDirectory()
		{
			return FileSystem.Normalize(Path.Combine(Manager.GetDirectory(), this.folder));
		}

		// Token: 0x06007B7F RID: 31615 RVA: 0x00311468 File Offset: 0x0030F668
		private void Subscribe(string directoryName, long timestamp, IFileSource file_source, bool isDevMod)
		{
			Label label = new Label
			{
				id = directoryName,
				distribution_platform = this.distribution_platform,
				version = (long)directoryName.GetHashCode(),
				title = directoryName
			};
			KModHeader header = KModUtil.GetHeader(file_source, label.defaultStaticID, directoryName, directoryName, isDevMod);
			label.title = header.title;
			Mod mod = new Mod(label, header.staticID, header.description, file_source, UI.FRONTEND.MODS.TOOLTIPS.MANAGE_LOCAL_MOD, delegate
			{
				App.OpenWebURL("file://" + file_source.GetRoot());
			});
			if (file_source.GetType() == typeof(Directory))
			{
				mod.status = Mod.Status.Installed;
			}
			Global.Instance.modManager.Subscribe(mod, this);
		}

		// Token: 0x06007B80 RID: 31616 RVA: 0x0031153C File Offset: 0x0030F73C
		public Local(string folder, Label.DistributionPlatform distribution_platform, bool isDevFolder)
		{
			this.folder = folder;
			this.distribution_platform = distribution_platform;
			DirectoryInfo directoryInfo = new DirectoryInfo(this.GetDirectory());
			if (!directoryInfo.Exists)
			{
				return;
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				string name = directoryInfo2.Name;
				this.Subscribe(name, directoryInfo2.LastWriteTime.ToFileTime(), new Directory(directoryInfo2.FullName), isDevFolder);
			}
		}
	}
}
