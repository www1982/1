using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020005E2 RID: 1506
[SerializationConfig(MemberSerialization.OptIn)]
public class MinionStorageDataHolder : KMonoBehaviour, StoredMinionIdentity.IStoredMinionExtension
{
	// Token: 0x060022F7 RID: 8951 RVA: 0x000C8EF4 File Offset: 0x000C70F4
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060022F8 RID: 8952 RVA: 0x000C8EFC File Offset: 0x000C70FC
	public MinionStorageDataHolder.DataPack Internal_GetDataPack(string ID)
	{
		if (this.storedDataPacks != null)
		{
			MinionStorageDataHolder.DataPack dataPack = this.storedDataPacks.Find((MinionStorageDataHolder.DataPack d) => d.ID == ID);
			if (dataPack != null)
			{
				return dataPack;
			}
		}
		return null;
	}

	// Token: 0x060022F9 RID: 8953 RVA: 0x000C8F3C File Offset: 0x000C713C
	public void Internal_UpdateData(string ID, MinionStorageDataHolder.DataPackData data)
	{
		this.SetData(ID, data, false);
	}

	// Token: 0x060022FA RID: 8954 RVA: 0x000C8F48 File Offset: 0x000C7148
	private void SetData(string ID, MinionStorageDataHolder.DataPackData data, bool markAsNewDataToRead)
	{
		if (this.storedDataPacks == null)
		{
			this.storedDataPacks = new List<MinionStorageDataHolder.DataPack>();
		}
		MinionStorageDataHolder.DataPack dataPack = this.storedDataPacks.Find((MinionStorageDataHolder.DataPack d) => d.ID == ID);
		if (dataPack == null)
		{
			dataPack = new MinionStorageDataHolder.DataPack(ID);
			this.storedDataPacks.Add(dataPack);
		}
		dataPack.SetData(data, markAsNewDataToRead);
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x000C8FB0 File Offset: 0x000C71B0
	public void PullFrom(StoredMinionIdentity source)
	{
		MinionStorageDataHolder component = source.GetComponent<MinionStorageDataHolder>();
		if (component != null && component.storedDataPacks != null)
		{
			for (int i = 0; i < component.storedDataPacks.Count; i++)
			{
				MinionStorageDataHolder.DataPack dataPack = component.storedDataPacks[i];
				if (dataPack != null)
				{
					this.SetData(dataPack.ID, dataPack.ReadData(), true);
				}
			}
		}
	}

	// Token: 0x060022FC RID: 8956 RVA: 0x000C9010 File Offset: 0x000C7210
	public void PushTo(StoredMinionIdentity destination)
	{
		Action<StoredMinionIdentity> onCopyBegins = this.OnCopyBegins;
		if (onCopyBegins != null)
		{
			onCopyBegins(destination);
		}
		this.AddStoredMinionGameObjectRequirements(destination.gameObject);
		MinionStorageDataHolder component = destination.gameObject.GetComponent<MinionStorageDataHolder>();
		if (this.storedDataPacks != null)
		{
			for (int i = 0; i < this.storedDataPacks.Count; i++)
			{
				MinionStorageDataHolder.DataPack dataPack = this.storedDataPacks[i];
				if (dataPack != null)
				{
					component.SetData(dataPack.ID, dataPack.ReadData(), true);
				}
			}
		}
	}

	// Token: 0x060022FD RID: 8957 RVA: 0x000C9088 File Offset: 0x000C7288
	public void AddStoredMinionGameObjectRequirements(GameObject storedMinionGameObject)
	{
		storedMinionGameObject.AddOrGet<MinionStorageDataHolder>();
	}

	// Token: 0x04001451 RID: 5201
	public Action<StoredMinionIdentity> OnCopyBegins;

	// Token: 0x04001452 RID: 5202
	[Serialize]
	private List<MinionStorageDataHolder.DataPack> storedDataPacks;

	// Token: 0x02001471 RID: 5233
	[SerializationConfig(MemberSerialization.OptIn)]
	public class DataPackData
	{
		// Token: 0x04006C9E RID: 27806
		[Serialize]
		public bool[] Bools;

		// Token: 0x04006C9F RID: 27807
		[Serialize]
		public Tag[] Tags;
	}

	// Token: 0x02001472 RID: 5234
	[SerializationConfig(MemberSerialization.OptIn)]
	public class DataPack
	{
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06008DC1 RID: 36289 RVA: 0x0035973E File Offset: 0x0035793E
		public bool IsStoringNewData
		{
			get
			{
				return this.isStoringNewData;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06008DC2 RID: 36290 RVA: 0x00359746 File Offset: 0x00357946
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06008DC3 RID: 36291 RVA: 0x0035974E File Offset: 0x0035794E
		public DataPack(string id)
		{
			this.id = id;
		}

		// Token: 0x06008DC4 RID: 36292 RVA: 0x0035975D File Offset: 0x0035795D
		public void SetData(MinionStorageDataHolder.DataPackData data, bool markAsNewDataToRead)
		{
			this.data = data;
			if (markAsNewDataToRead)
			{
				this.isStoringNewData = markAsNewDataToRead;
			}
		}

		// Token: 0x06008DC5 RID: 36293 RVA: 0x00359770 File Offset: 0x00357970
		public MinionStorageDataHolder.DataPackData ReadData()
		{
			this.isStoringNewData = false;
			return this.data;
		}

		// Token: 0x06008DC6 RID: 36294 RVA: 0x0035977F File Offset: 0x0035797F
		public MinionStorageDataHolder.DataPackData PeekData()
		{
			return this.data;
		}

		// Token: 0x04006CA0 RID: 27808
		[Serialize]
		private string id;

		// Token: 0x04006CA1 RID: 27809
		[Serialize]
		private bool isStoringNewData;

		// Token: 0x04006CA2 RID: 27810
		[Serialize]
		private MinionStorageDataHolder.DataPackData data;
	}
}
