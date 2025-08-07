using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

// Token: 0x020005F2 RID: 1522
public class OldNoteEntriesV5
{
	// Token: 0x060023AC RID: 9132 RVA: 0x000CBAF8 File Offset: 0x000C9CF8
	public void Deserialize(BinaryReader reader)
	{
		int num = reader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			OldNoteEntriesV5.NoteStorageBlock noteStorageBlock = default(OldNoteEntriesV5.NoteStorageBlock);
			noteStorageBlock.Deserialize(reader);
			this.storageBlocks.Add(noteStorageBlock);
		}
	}

	// Token: 0x040014C9 RID: 5321
	public List<OldNoteEntriesV5.NoteStorageBlock> storageBlocks = new List<OldNoteEntriesV5.NoteStorageBlock>();

	// Token: 0x0200147D RID: 5245
	[StructLayout(LayoutKind.Explicit)]
	public struct NoteEntry
	{
		// Token: 0x04006CC0 RID: 27840
		[FieldOffset(0)]
		public int reportEntryId;

		// Token: 0x04006CC1 RID: 27841
		[FieldOffset(4)]
		public int noteHash;

		// Token: 0x04006CC2 RID: 27842
		[FieldOffset(8)]
		public float value;
	}

	// Token: 0x0200147E RID: 5246
	[StructLayout(LayoutKind.Explicit)]
	public struct NoteEntryArray
	{
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06008DEB RID: 36331 RVA: 0x00359EC3 File Offset: 0x003580C3
		public int StructSizeInBytes
		{
			get
			{
				return Marshal.SizeOf(typeof(OldNoteEntriesV5.NoteEntry));
			}
		}

		// Token: 0x04006CC3 RID: 27843
		[FieldOffset(0)]
		public byte[] bytes;

		// Token: 0x04006CC4 RID: 27844
		[FieldOffset(0)]
		public OldNoteEntriesV5.NoteEntry[] structs;
	}

	// Token: 0x0200147F RID: 5247
	public struct NoteStorageBlock
	{
		// Token: 0x06008DEC RID: 36332 RVA: 0x00359ED4 File Offset: 0x003580D4
		public void Deserialize(BinaryReader reader)
		{
			this.entryCount = reader.ReadInt32();
			this.entries.bytes = reader.ReadBytes(this.entries.StructSizeInBytes * this.entryCount);
		}

		// Token: 0x04006CC5 RID: 27845
		public int entryCount;

		// Token: 0x04006CC6 RID: 27846
		public OldNoteEntriesV5.NoteEntryArray entries;
	}
}
