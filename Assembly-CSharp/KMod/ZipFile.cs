using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;
using Klei;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000F73 RID: 3955
	internal struct ZipFile : IFileSource
	{
		// Token: 0x06007B96 RID: 31638 RVA: 0x00311C22 File Offset: 0x0030FE22
		public ZipFile(string filename)
		{
			this.filename = filename;
			this.zipfile = ZipFile.Read(filename);
			this.file_system = new ZipFileDirectory(this.zipfile.Name, this.zipfile, Application.streamingAssetsPath, true);
		}

		// Token: 0x06007B97 RID: 31639 RVA: 0x00311C59 File Offset: 0x0030FE59
		public string GetRoot()
		{
			return this.filename;
		}

		// Token: 0x06007B98 RID: 31640 RVA: 0x00311C61 File Offset: 0x0030FE61
		public bool Exists()
		{
			return File.Exists(this.GetRoot());
		}

		// Token: 0x06007B99 RID: 31641 RVA: 0x00311C70 File Offset: 0x0030FE70
		public bool Exists(string relative_path)
		{
			if (!this.Exists())
			{
				return false;
			}
			using (IEnumerator<ZipEntry> enumerator = this.zipfile.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (FileSystem.Normalize(enumerator.Current.FileName).StartsWith(relative_path))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007B9A RID: 31642 RVA: 0x00311CD8 File Offset: 0x0030FED8
		public void GetTopLevelItems(List<FileSystemItem> file_system_items, string relative_root)
		{
			HashSetPool<string, ZipFile>.PooledHashSet pooledHashSet = HashSetPool<string, ZipFile>.Allocate();
			string[] array;
			if (!string.IsNullOrEmpty(relative_root))
			{
				relative_root = relative_root ?? "";
				relative_root = FileSystem.Normalize(relative_root);
				array = relative_root.Split('/', StringSplitOptions.None);
			}
			else
			{
				array = new string[0];
			}
			foreach (ZipEntry zipEntry in this.zipfile)
			{
				List<string> list = (from part in FileSystem.Normalize(zipEntry.FileName).Split('/', StringSplitOptions.None)
					where !string.IsNullOrEmpty(part)
					select part).ToList<string>();
				if (this.IsSharedRoot(array, list))
				{
					list = list.GetRange(array.Length, list.Count - array.Length);
					if (list.Count != 0)
					{
						string text = list[0];
						if (pooledHashSet.Add(text))
						{
							file_system_items.Add(new FileSystemItem
							{
								name = text,
								type = ((1 < list.Count) ? FileSystemItem.ItemType.Directory : FileSystemItem.ItemType.File)
							});
						}
					}
				}
			}
			pooledHashSet.Recycle();
		}

		// Token: 0x06007B9B RID: 31643 RVA: 0x00311E00 File Offset: 0x00310000
		private bool IsSharedRoot(string[] root_path, List<string> check_path)
		{
			for (int i = 0; i < root_path.Length; i++)
			{
				if (i >= check_path.Count || root_path[i] != check_path[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007B9C RID: 31644 RVA: 0x00311E38 File Offset: 0x00310038
		public IFileDirectory GetFileSystem()
		{
			return this.file_system;
		}

		// Token: 0x06007B9D RID: 31645 RVA: 0x00311E40 File Offset: 0x00310040
		public void CopyTo(string path, List<string> extensions = null)
		{
			foreach (ZipEntry zipEntry in this.zipfile.Entries)
			{
				bool flag = extensions == null || extensions.Count == 0;
				if (extensions != null)
				{
					foreach (string text in extensions)
					{
						if (zipEntry.FileName.ToLower().EndsWith(text))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					string text2 = FileSystem.Normalize(Path.Combine(path, zipEntry.FileName));
					string directoryName = Path.GetDirectoryName(text2);
					if (string.IsNullOrEmpty(directoryName) || FileUtil.CreateDirectory(directoryName, 0))
					{
						using (MemoryStream memoryStream = new MemoryStream((int)zipEntry.UncompressedSize))
						{
							zipEntry.Extract(memoryStream);
							using (FileStream fileStream = FileUtil.Create(text2, 0))
							{
								fileStream.Write(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
							}
						}
					}
				}
			}
		}

		// Token: 0x06007B9E RID: 31646 RVA: 0x00311F90 File Offset: 0x00310190
		public string Read(string relative_path)
		{
			ICollection<ZipEntry> collection = this.zipfile.SelectEntries(relative_path);
			if (collection.Count == 0)
			{
				return string.Empty;
			}
			foreach (ZipEntry zipEntry in collection)
			{
				using (MemoryStream memoryStream = new MemoryStream((int)zipEntry.UncompressedSize))
				{
					zipEntry.Extract(memoryStream);
					return Encoding.UTF8.GetString(memoryStream.GetBuffer());
				}
			}
			return string.Empty;
		}

		// Token: 0x06007B9F RID: 31647 RVA: 0x00312034 File Offset: 0x00310234
		public void Dispose()
		{
			this.zipfile.Dispose();
		}

		// Token: 0x04005AEF RID: 23279
		private string filename;

		// Token: 0x04005AF0 RID: 23280
		private ZipFile zipfile;

		// Token: 0x04005AF1 RID: 23281
		private ZipFileDirectory file_system;
	}
}
