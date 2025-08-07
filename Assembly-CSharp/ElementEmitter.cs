using System;
using UnityEngine;

// Token: 0x020008E0 RID: 2272
public class ElementEmitter : SimComponent
{
	// Token: 0x1700046C RID: 1132
	// (get) Token: 0x06003F30 RID: 16176 RVA: 0x00163EAF File Offset: 0x001620AF
	// (set) Token: 0x06003F31 RID: 16177 RVA: 0x00163EB7 File Offset: 0x001620B7
	public bool isEmitterBlocked { get; private set; }

	// Token: 0x06003F32 RID: 16178 RVA: 0x00163EC0 File Offset: 0x001620C0
	protected override void OnSpawn()
	{
		this.onBlockedHandle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(new global::System.Action(this.OnEmitterBlocked), true));
		this.onUnblockedHandle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(new global::System.Action(this.OnEmitterUnblocked), true));
		base.OnSpawn();
	}

	// Token: 0x06003F33 RID: 16179 RVA: 0x00163F21 File Offset: 0x00162121
	protected override void OnCleanUp()
	{
		Game.Instance.ManualReleaseHandle(this.onBlockedHandle);
		Game.Instance.ManualReleaseHandle(this.onUnblockedHandle);
		base.OnCleanUp();
	}

	// Token: 0x06003F34 RID: 16180 RVA: 0x00163F49 File Offset: 0x00162149
	public void SetEmitting(bool emitting)
	{
		base.SetSimActive(emitting);
	}

	// Token: 0x06003F35 RID: 16181 RVA: 0x00163F54 File Offset: 0x00162154
	protected override void OnSimActivate()
	{
		int num = Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), (int)this.outputElement.outputElementOffset.x, (int)this.outputElement.outputElementOffset.y);
		if (this.outputElement.elementHash != (SimHashes)0 && this.outputElement.massGenerationRate > 0f && this.emissionFrequency > 0f)
		{
			float num2 = ((this.outputElement.minOutputTemperature == 0f) ? base.GetComponent<PrimaryElement>().Temperature : this.outputElement.minOutputTemperature);
			SimMessages.ModifyElementEmitter(this.simHandle, num, (int)this.emitRange, this.outputElement.elementHash, this.emissionFrequency, this.outputElement.massGenerationRate, num2, this.maxPressure, this.outputElement.addedDiseaseIdx, this.outputElement.addedDiseaseCount);
		}
		if (this.showDescriptor)
		{
			this.statusHandle = base.GetComponent<KSelectable>().ReplaceStatusItem(this.statusHandle, Db.Get().BuildingStatusItems.ElementEmitterOutput, this);
		}
	}

	// Token: 0x06003F36 RID: 16182 RVA: 0x00164070 File Offset: 0x00162270
	protected override void OnSimDeactivate()
	{
		int num = Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), (int)this.outputElement.outputElementOffset.x, (int)this.outputElement.outputElementOffset.y);
		SimMessages.ModifyElementEmitter(this.simHandle, num, (int)this.emitRange, SimHashes.Vacuum, 0f, 0f, 0f, 0f, byte.MaxValue, 0);
		if (this.showDescriptor)
		{
			this.statusHandle = base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
		}
	}

	// Token: 0x06003F37 RID: 16183 RVA: 0x00164108 File Offset: 0x00162308
	public void ForceEmit(float mass, byte disease_idx, int disease_count, float temperature = -1f)
	{
		if (mass <= 0f)
		{
			return;
		}
		float num = ((temperature > 0f) ? temperature : this.outputElement.minOutputTemperature);
		Element element = ElementLoader.FindElementByHash(this.outputElement.elementHash);
		if (element.IsGas || element.IsLiquid)
		{
			SimMessages.AddRemoveSubstance(Grid.PosToCell(base.transform.GetPosition()), this.outputElement.elementHash, CellEventLogger.Instance.ElementConsumerSimUpdate, mass, num, disease_idx, disease_count, true, -1);
		}
		else if (element.IsSolid)
		{
			element.substance.SpawnResource(base.transform.GetPosition() + new Vector3(0f, 0.5f, 0f), mass, num, disease_idx, disease_count, false, true, false);
		}
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, ElementLoader.FindElementByHash(this.outputElement.elementHash).name, base.gameObject.transform, 1.5f, false);
	}

	// Token: 0x06003F38 RID: 16184 RVA: 0x00164204 File Offset: 0x00162404
	private void OnEmitterBlocked()
	{
		this.isEmitterBlocked = true;
		base.Trigger(1615168894, this);
	}

	// Token: 0x06003F39 RID: 16185 RVA: 0x00164219 File Offset: 0x00162419
	private void OnEmitterUnblocked()
	{
		this.isEmitterBlocked = false;
		base.Trigger(-657992955, this);
	}

	// Token: 0x06003F3A RID: 16186 RVA: 0x0016422E File Offset: 0x0016242E
	protected override void OnSimRegister(HandleVector<Game.ComplexCallbackInfo<int>>.Handle cb_handle)
	{
		Game.Instance.simComponentCallbackManager.GetItem(cb_handle);
		SimMessages.AddElementEmitter(this.maxPressure, cb_handle.index, this.onBlockedHandle.index, this.onUnblockedHandle.index);
	}

	// Token: 0x06003F3B RID: 16187 RVA: 0x00164269 File Offset: 0x00162469
	protected override void OnSimUnregister()
	{
		ElementEmitter.StaticUnregister(this.simHandle);
	}

	// Token: 0x06003F3C RID: 16188 RVA: 0x00164276 File Offset: 0x00162476
	private static void StaticUnregister(int sim_handle)
	{
		global::Debug.Assert(Sim.IsValidHandle(sim_handle));
		SimMessages.RemoveElementEmitter(-1, sim_handle);
	}

	// Token: 0x06003F3D RID: 16189 RVA: 0x0016428C File Offset: 0x0016248C
	private void OnDrawGizmosSelected()
	{
		int num = Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), (int)this.outputElement.outputElementOffset.x, (int)this.outputElement.outputElementOffset.y);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(Grid.CellToPos(num) + Vector3.right / 2f + Vector3.up / 2f, 0.2f);
	}

	// Token: 0x06003F3E RID: 16190 RVA: 0x00164311 File Offset: 0x00162511
	protected override Action<int> GetStaticUnregister()
	{
		return new Action<int>(ElementEmitter.StaticUnregister);
	}

	// Token: 0x0400274C RID: 10060
	[SerializeField]
	public ElementConverter.OutputElement outputElement;

	// Token: 0x0400274D RID: 10061
	[SerializeField]
	public float emissionFrequency = 1f;

	// Token: 0x0400274E RID: 10062
	[SerializeField]
	public byte emitRange = 1;

	// Token: 0x0400274F RID: 10063
	[SerializeField]
	public float maxPressure = 1f;

	// Token: 0x04002750 RID: 10064
	private Guid statusHandle = Guid.Empty;

	// Token: 0x04002751 RID: 10065
	public bool showDescriptor = true;

	// Token: 0x04002752 RID: 10066
	private HandleVector<Game.CallbackInfo>.Handle onBlockedHandle = HandleVector<Game.CallbackInfo>.InvalidHandle;

	// Token: 0x04002753 RID: 10067
	private HandleVector<Game.CallbackInfo>.Handle onUnblockedHandle = HandleVector<Game.CallbackInfo>.InvalidHandle;
}
