using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using KSerialization;

// Token: 0x02000D5A RID: 3418
[SerializationConfig(MemberSerialization.OptIn)]
public class SerializedList<ItemType>
{
	// Token: 0x17000783 RID: 1923
	// (get) Token: 0x06006A00 RID: 27136 RVA: 0x002805D2 File Offset: 0x0027E7D2
	public int Count
	{
		get
		{
			return this.items.Count;
		}
	}

	// Token: 0x06006A01 RID: 27137 RVA: 0x002805DF File Offset: 0x0027E7DF
	public IEnumerator<ItemType> GetEnumerator()
	{
		return this.items.GetEnumerator();
	}

	// Token: 0x17000784 RID: 1924
	public ItemType this[int idx]
	{
		get
		{
			return this.items[idx];
		}
	}

	// Token: 0x06006A03 RID: 27139 RVA: 0x002805FF File Offset: 0x0027E7FF
	public void Add(ItemType item)
	{
		this.items.Add(item);
	}

	// Token: 0x06006A04 RID: 27140 RVA: 0x0028060D File Offset: 0x0027E80D
	public void Remove(ItemType item)
	{
		this.items.Remove(item);
	}

	// Token: 0x06006A05 RID: 27141 RVA: 0x0028061C File Offset: 0x0027E81C
	public void RemoveAt(int idx)
	{
		this.items.RemoveAt(idx);
	}

	// Token: 0x06006A06 RID: 27142 RVA: 0x0028062A File Offset: 0x0027E82A
	public bool Contains(ItemType item)
	{
		return this.items.Contains(item);
	}

	// Token: 0x06006A07 RID: 27143 RVA: 0x00280638 File Offset: 0x0027E838
	public void Clear()
	{
		this.items.Clear();
	}

	// Token: 0x06006A08 RID: 27144 RVA: 0x00280648 File Offset: 0x0027E848
	[OnSerializing]
	private void OnSerializing()
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(this.items.Count);
		foreach (ItemType itemType in this.items)
		{
			binaryWriter.WriteKleiString(itemType.GetType().FullName);
			long position = binaryWriter.BaseStream.Position;
			binaryWriter.Write(0);
			long position2 = binaryWriter.BaseStream.Position;
			Serializer.SerializeTypeless(itemType, binaryWriter);
			long position3 = binaryWriter.BaseStream.Position;
			long num = position3 - position2;
			binaryWriter.BaseStream.Position = position;
			binaryWriter.Write((int)num);
			binaryWriter.BaseStream.Position = position3;
		}
		memoryStream.Flush();
		this.serializationBuffer = memoryStream.ToArray();
	}

	// Token: 0x06006A09 RID: 27145 RVA: 0x00280748 File Offset: 0x0027E948
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.serializationBuffer == null)
		{
			return;
		}
		FastReader fastReader = new FastReader(this.serializationBuffer);
		int num = fastReader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			string text = fastReader.ReadKleiString();
			int num2 = fastReader.ReadInt32();
			int position = fastReader.Position;
			Type type = Type.GetType(text);
			if (type == null)
			{
				DebugUtil.LogWarningArgs(new object[] { "Type no longer exists: " + text });
				fastReader.SkipBytes(num2);
			}
			else
			{
				ItemType itemType;
				if (typeof(ItemType) != type)
				{
					itemType = (ItemType)((object)Activator.CreateInstance(type));
				}
				else
				{
					itemType = default(ItemType);
				}
				Deserializer.DeserializeTypeless(itemType, fastReader);
				if (fastReader.Position != position + num2)
				{
					DebugUtil.LogWarningArgs(new object[]
					{
						"Expected to be at offset",
						position + num2,
						"but was only at offset",
						fastReader.Position,
						". Skipping to catch up."
					});
					fastReader.SkipBytes(position + num2 - fastReader.Position);
				}
				this.items.Add(itemType);
			}
		}
	}

	// Token: 0x0400485C RID: 18524
	[Serialize]
	private byte[] serializationBuffer;

	// Token: 0x0400485D RID: 18525
	private List<ItemType> items = new List<ItemType>();
}
