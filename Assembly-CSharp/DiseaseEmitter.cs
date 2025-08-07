using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020008C3 RID: 2243
[AddComponentMenu("KMonoBehaviour/scripts/DiseaseEmitter")]
public class DiseaseEmitter : KMonoBehaviour
{
	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x06003E20 RID: 15904 RVA: 0x0015BC9A File Offset: 0x00159E9A
	public float EmitRate
	{
		get
		{
			return this.emitRate;
		}
	}

	// Token: 0x06003E21 RID: 15905 RVA: 0x0015BCA4 File Offset: 0x00159EA4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.emitDiseases != null)
		{
			this.simHandles = new int[this.emitDiseases.Length];
			for (int i = 0; i < this.simHandles.Length; i++)
			{
				this.simHandles[i] = -1;
			}
		}
		this.SimRegister();
	}

	// Token: 0x06003E22 RID: 15906 RVA: 0x0015BCF4 File Offset: 0x00159EF4
	protected override void OnCleanUp()
	{
		this.SimUnregister();
		base.OnCleanUp();
	}

	// Token: 0x06003E23 RID: 15907 RVA: 0x0015BD02 File Offset: 0x00159F02
	public void SetEnable(bool enable)
	{
		if (this.enableEmitter == enable)
		{
			return;
		}
		this.enableEmitter = enable;
		if (this.enableEmitter)
		{
			this.SimRegister();
			return;
		}
		this.SimUnregister();
	}

	// Token: 0x06003E24 RID: 15908 RVA: 0x0015BD2C File Offset: 0x00159F2C
	private void OnCellChanged()
	{
		if (this.simHandles == null || !this.enableEmitter)
		{
			return;
		}
		int num = Grid.PosToCell(this);
		if (Grid.IsValidCell(num))
		{
			for (int i = 0; i < this.emitDiseases.Length; i++)
			{
				if (Sim.IsValidHandle(this.simHandles[i]))
				{
					SimMessages.ModifyDiseaseEmitter(this.simHandles[i], num, this.emitRange, this.emitDiseases[i], this.emitRate, this.emitCount);
				}
			}
		}
	}

	// Token: 0x06003E25 RID: 15909 RVA: 0x0015BDA4 File Offset: 0x00159FA4
	private void SimRegister()
	{
		if (this.simHandles == null || !this.enableEmitter)
		{
			return;
		}
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChanged), "DiseaseEmitter.Modify");
		for (int i = 0; i < this.simHandles.Length; i++)
		{
			if (this.simHandles[i] == -1)
			{
				this.simHandles[i] = -2;
				SimMessages.AddDiseaseEmitter(Game.Instance.simComponentCallbackManager.Add(new Action<int, object>(DiseaseEmitter.OnSimRegisteredCallback), this, "DiseaseEmitter").index);
			}
		}
	}

	// Token: 0x06003E26 RID: 15910 RVA: 0x0015BE3C File Offset: 0x0015A03C
	private void SimUnregister()
	{
		if (this.simHandles == null)
		{
			return;
		}
		for (int i = 0; i < this.simHandles.Length; i++)
		{
			if (Sim.IsValidHandle(this.simHandles[i]))
			{
				SimMessages.RemoveDiseaseEmitter(-1, this.simHandles[i]);
			}
			this.simHandles[i] = -1;
		}
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChanged));
	}

	// Token: 0x06003E27 RID: 15911 RVA: 0x0015BEA7 File Offset: 0x0015A0A7
	private static void OnSimRegisteredCallback(int handle, object data)
	{
		((DiseaseEmitter)data).OnSimRegistered(handle);
	}

	// Token: 0x06003E28 RID: 15912 RVA: 0x0015BEB8 File Offset: 0x0015A0B8
	private void OnSimRegistered(int handle)
	{
		bool flag = false;
		if (this != null)
		{
			for (int i = 0; i < this.simHandles.Length; i++)
			{
				if (this.simHandles[i] == -2)
				{
					this.simHandles[i] = handle;
					flag = true;
					break;
				}
			}
			this.OnCellChanged();
		}
		if (!flag)
		{
			SimMessages.RemoveDiseaseEmitter(-1, handle);
		}
	}

	// Token: 0x06003E29 RID: 15913 RVA: 0x0015BF0C File Offset: 0x0015A10C
	public void SetDiseases(List<Disease> diseases)
	{
		this.emitDiseases = new byte[diseases.Count];
		for (int i = 0; i < diseases.Count; i++)
		{
			this.emitDiseases[i] = Db.Get().Diseases.GetIndex(diseases[i].id);
		}
	}

	// Token: 0x04002637 RID: 9783
	[Serialize]
	public float emitRate = 1f;

	// Token: 0x04002638 RID: 9784
	[Serialize]
	public byte emitRange;

	// Token: 0x04002639 RID: 9785
	[Serialize]
	public int emitCount;

	// Token: 0x0400263A RID: 9786
	[Serialize]
	public byte[] emitDiseases;

	// Token: 0x0400263B RID: 9787
	public int[] simHandles;

	// Token: 0x0400263C RID: 9788
	[Serialize]
	private bool enableEmitter;
}
