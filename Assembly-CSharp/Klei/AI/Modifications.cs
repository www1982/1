using System;
using System.Collections.Generic;
using System.IO;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02001000 RID: 4096
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Modifications<ModifierType, InstanceType> : ISaveLoadableDetails where ModifierType : Resource where InstanceType : ModifierInstance<ModifierType>
	{
		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06007E48 RID: 32328 RVA: 0x003288B7 File Offset: 0x00326AB7
		public int Count
		{
			get
			{
				return this.ModifierList.Count;
			}
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x003288C4 File Offset: 0x00326AC4
		public IEnumerator<InstanceType> GetEnumerator()
		{
			return this.ModifierList.GetEnumerator();
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06007E4A RID: 32330 RVA: 0x003288D6 File Offset: 0x00326AD6
		// (set) Token: 0x06007E4B RID: 32331 RVA: 0x003288DE File Offset: 0x00326ADE
		public GameObject gameObject { get; private set; }

		// Token: 0x170008EB RID: 2283
		public InstanceType this[int idx]
		{
			get
			{
				return this.ModifierList[idx];
			}
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x003288F5 File Offset: 0x00326AF5
		public ComponentType GetComponent<ComponentType>()
		{
			return this.gameObject.GetComponent<ComponentType>();
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x00328902 File Offset: 0x00326B02
		public void Trigger(GameHashes hash, object data = null)
		{
			this.gameObject.GetComponent<KPrefabID>().Trigger((int)hash, data);
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x00328918 File Offset: 0x00326B18
		public virtual InstanceType CreateInstance(ModifierType modifier)
		{
			return default(InstanceType);
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x0032892E File Offset: 0x00326B2E
		public Modifications(GameObject go, ResourceSet<ModifierType> resources = null)
		{
			this.resources = resources;
			this.gameObject = go;
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x0032894F File Offset: 0x00326B4F
		public virtual InstanceType Add(InstanceType instance)
		{
			this.ModifierList.Add(instance);
			return instance;
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x00328960 File Offset: 0x00326B60
		public virtual void Remove(InstanceType instance)
		{
			for (int i = 0; i < this.ModifierList.Count; i++)
			{
				if (this.ModifierList[i] == instance)
				{
					this.ModifierList.RemoveAt(i);
					instance.OnCleanUp();
					return;
				}
			}
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x003289B4 File Offset: 0x00326BB4
		public bool Has(ModifierType modifier)
		{
			return this.Get(modifier) != null;
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x003289C8 File Offset: 0x00326BC8
		public InstanceType Get(ModifierType modifier)
		{
			foreach (InstanceType instanceType in this.ModifierList)
			{
				if (instanceType.modifier == modifier)
				{
					return instanceType;
				}
			}
			return default(InstanceType);
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x00328A3C File Offset: 0x00326C3C
		public InstanceType Get(string id)
		{
			foreach (InstanceType instanceType in this.ModifierList)
			{
				if (instanceType.modifier.Id == id)
				{
					return instanceType;
				}
			}
			return default(InstanceType);
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x00328AB4 File Offset: 0x00326CB4
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.ModifierList.Count);
			foreach (InstanceType instanceType in this.ModifierList)
			{
				writer.WriteKleiString(instanceType.modifier.Id);
				long position = writer.BaseStream.Position;
				writer.Write(0);
				long position2 = writer.BaseStream.Position;
				Serializer.SerializeTypeless(instanceType, writer);
				long position3 = writer.BaseStream.Position;
				long num = position3 - position2;
				writer.BaseStream.Position = position;
				writer.Write((int)num);
				writer.BaseStream.Position = position3;
			}
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x00328B94 File Offset: 0x00326D94
		public void Deserialize(IReader reader)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				int num2 = reader.ReadInt32();
				int position = reader.Position;
				InstanceType instanceType = this.Get(text);
				if (instanceType == null && this.resources != null)
				{
					ModifierType modifierType = this.resources.TryGet(text);
					if (modifierType != null)
					{
						instanceType = this.CreateInstance(modifierType);
					}
				}
				if (instanceType == null)
				{
					if (text != "Condition")
					{
						DebugUtil.LogWarningArgs(new object[]
						{
							this.gameObject.name,
							"Missing modifier: " + text
						});
					}
					reader.SkipBytes(num2);
				}
				else if (!(instanceType is ISaveLoadable))
				{
					reader.SkipBytes(num2);
				}
				else
				{
					Deserializer.DeserializeTypeless(instanceType, reader);
					if (reader.Position != position + num2)
					{
						DebugUtil.LogWarningArgs(new object[]
						{
							"Expected to be at offset",
							position + num2,
							"but was only at offset",
							reader.Position,
							". Skipping to catch up."
						});
						reader.SkipBytes(position + num2 - reader.Position);
					}
				}
			}
		}

		// Token: 0x04005F53 RID: 24403
		public List<InstanceType> ModifierList = new List<InstanceType>();

		// Token: 0x04005F55 RID: 24405
		private ResourceSet<ModifierType> resources;
	}
}
