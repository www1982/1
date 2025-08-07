using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Klei;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000F72 RID: 3954
	internal struct Directory : IFileSource
	{
		// Token: 0x06007B8C RID: 31628 RVA: 0x00311924 File Offset: 0x0030FB24
		public Directory(string root)
		{
			this.root = root;
			this.file_system = new AliasDirectory(root, root, Application.streamingAssetsPath, true);
		}

		// Token: 0x06007B8D RID: 31629 RVA: 0x00311940 File Offset: 0x0030FB40
		public string GetRoot()
		{
			return this.root;
		}

		// Token: 0x06007B8E RID: 31630 RVA: 0x00311948 File Offset: 0x0030FB48
		public bool Exists()
		{
			return Directory.Exists(this.GetRoot());
		}

		// Token: 0x06007B8F RID: 31631 RVA: 0x00311955 File Offset: 0x0030FB55
		public bool Exists(string relative_path)
		{
			return this.Exists() && new DirectoryInfo(FileSystem.Normalize(Path.Combine(this.root, relative_path))).Exists;
		}

		// Token: 0x06007B90 RID: 31632 RVA: 0x0031197C File Offset: 0x0030FB7C
		public void GetTopLevelItems(List<FileSystemItem> file_system_items, string relative_root)
		{
			relative_root = relative_root ?? "";
			string text = FileSystem.Normalize(Path.Combine(this.root, relative_root));
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			if (!directoryInfo.Exists)
			{
				global::Debug.LogError("Cannot iterate over $" + text + ", this directory does not exist");
				return;
			}
			foreach (FileSystemInfo fileSystemInfo in directoryInfo.GetFileSystemInfos())
			{
				file_system_items.Add(new FileSystemItem
				{
					name = fileSystemInfo.Name,
					type = ((fileSystemInfo is DirectoryInfo) ? FileSystemItem.ItemType.Directory : FileSystemItem.ItemType.File)
				});
			}
		}

		// Token: 0x06007B91 RID: 31633 RVA: 0x00311A18 File Offset: 0x0030FC18
		public IFileDirectory GetFileSystem()
		{
			return this.file_system;
		}

		// Token: 0x06007B92 RID: 31634 RVA: 0x00311A20 File Offset: 0x0030FC20
		public void CopyTo(string path, List<string> extensions = null)
		{
			try
			{
				Directory.CopyDirectory(this.root, path, extensions);
			}
			catch (UnauthorizedAccessException)
			{
				FileUtil.ErrorDialog(FileUtil.ErrorType.UnauthorizedAccess, path, null, null);
			}
			catch (IOException)
			{
				FileUtil.ErrorDialog(FileUtil.ErrorType.IOError, path, null, null);
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06007B93 RID: 31635 RVA: 0x00311A80 File Offset: 0x0030FC80
		public string Read(string relative_path)
		{
			string text;
			try
			{
				using (FileStream fileStream = File.OpenRead(Path.Combine(this.root, relative_path)))
				{
					byte[] array = new byte[fileStream.Length];
					fileStream.Read(array, 0, (int)fileStream.Length);
					text = Encoding.UTF8.GetString(array);
				}
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06007B94 RID: 31636 RVA: 0x00311AFC File Offset: 0x0030FCFC
		private static int CopyDirectory(string sourceDirName, string destDirName, List<string> extensions)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirName);
			if (!directoryInfo.Exists)
			{
				return 0;
			}
			if (!FileUtil.CreateDirectory(destDirName, 0))
			{
				return 0;
			}
			FileInfo[] files = directoryInfo.GetFiles();
			int num = 0;
			foreach (FileInfo fileInfo in files)
			{
				bool flag = extensions == null || extensions.Count == 0;
				if (extensions != null)
				{
					using (List<string>.Enumerator enumerator = extensions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == Path.GetExtension(fileInfo.Name).ToLower())
							{
								flag = true;
								break;
							}
						}
					}
				}
				if (flag)
				{
					string text = Path.Combine(destDirName, fileInfo.Name);
					fileInfo.CopyTo(text, false);
					num++;
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				string text2 = Path.Combine(destDirName, directoryInfo2.Name);
				num += Directory.CopyDirectory(directoryInfo2.FullName, text2, extensions);
			}
			if (num == 0)
			{
				FileUtil.DeleteDirectory(destDirName, 0);
			}
			return num;
		}

		// Token: 0x06007B95 RID: 31637 RVA: 0x00311C20 File Offset: 0x0030FE20
		public void Dispose()
		{
		}

		// Token: 0x04005AED RID: 23277
		private AliasDirectory file_system;

		// Token: 0x04005AEE RID: 23278
		private string root;
	}
}
