using System;
using UnityEngine;

// Token: 0x02000A86 RID: 2694
public class RadiationEmitter : SimComponent
{
	// Token: 0x06004E2B RID: 20011 RVA: 0x001C4F13 File Offset: 0x001C3113
	protected override void OnSpawn()
	{
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange), "RadiationEmitter.OnSpawn");
		base.OnSpawn();
	}

	// Token: 0x06004E2C RID: 20012 RVA: 0x001C4F3D File Offset: 0x001C313D
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
		base.OnCleanUp();
	}

	// Token: 0x06004E2D RID: 20013 RVA: 0x001C4F61 File Offset: 0x001C3161
	public void SetEmitting(bool emitting)
	{
		base.SetSimActive(emitting);
	}

	// Token: 0x06004E2E RID: 20014 RVA: 0x001C4F6A File Offset: 0x001C316A
	public int GetEmissionCell()
	{
		return Grid.PosToCell(base.transform.GetPosition() + this.emissionOffset);
	}

	// Token: 0x06004E2F RID: 20015 RVA: 0x001C4F88 File Offset: 0x001C3188
	public void Refresh()
	{
		int emissionCell = this.GetEmissionCell();
		if (this.radiusProportionalToRads)
		{
			this.SetRadiusProportionalToRads();
		}
		SimMessages.ModifyRadiationEmitter(this.simHandle, emissionCell, this.emitRadiusX, this.emitRadiusY, this.emitRads, this.emitRate, this.emitSpeed, this.emitDirection, this.emitAngle, this.emitType);
	}

	// Token: 0x06004E30 RID: 20016 RVA: 0x001C4FE6 File Offset: 0x001C31E6
	private void OnCellChange()
	{
		this.Refresh();
	}

	// Token: 0x06004E31 RID: 20017 RVA: 0x001C4FF0 File Offset: 0x001C31F0
	private void SetRadiusProportionalToRads()
	{
		this.emitRadiusX = (short)Mathf.Clamp(Mathf.RoundToInt(this.emitRads * 1f), 1, 128);
		this.emitRadiusY = (short)Mathf.Clamp(Mathf.RoundToInt(this.emitRads * 1f), 1, 128);
	}

	// Token: 0x06004E32 RID: 20018 RVA: 0x001C5044 File Offset: 0x001C3244
	protected override void OnSimActivate()
	{
		int emissionCell = this.GetEmissionCell();
		if (this.radiusProportionalToRads)
		{
			this.SetRadiusProportionalToRads();
		}
		SimMessages.ModifyRadiationEmitter(this.simHandle, emissionCell, this.emitRadiusX, this.emitRadiusY, this.emitRads, this.emitRate, this.emitSpeed, this.emitDirection, this.emitAngle, this.emitType);
	}

	// Token: 0x06004E33 RID: 20019 RVA: 0x001C50A4 File Offset: 0x001C32A4
	protected override void OnSimDeactivate()
	{
		int emissionCell = this.GetEmissionCell();
		SimMessages.ModifyRadiationEmitter(this.simHandle, emissionCell, 0, 0, 0f, 0f, 0f, 0f, 0f, this.emitType);
	}

	// Token: 0x06004E34 RID: 20020 RVA: 0x001C50E8 File Offset: 0x001C32E8
	protected override void OnSimRegister(HandleVector<Game.ComplexCallbackInfo<int>>.Handle cb_handle)
	{
		Game.Instance.simComponentCallbackManager.GetItem(cb_handle);
		int emissionCell = this.GetEmissionCell();
		SimMessages.AddRadiationEmitter(cb_handle.index, emissionCell, 0, 0, 0f, 0f, 0f, 0f, 0f, this.emitType);
	}

	// Token: 0x06004E35 RID: 20021 RVA: 0x001C513B File Offset: 0x001C333B
	protected override void OnSimUnregister()
	{
		RadiationEmitter.StaticUnregister(this.simHandle);
	}

	// Token: 0x06004E36 RID: 20022 RVA: 0x001C5148 File Offset: 0x001C3348
	private static void StaticUnregister(int sim_handle)
	{
		global::Debug.Assert(Sim.IsValidHandle(sim_handle));
		SimMessages.RemoveRadiationEmitter(-1, sim_handle);
	}

	// Token: 0x06004E37 RID: 20023 RVA: 0x001C515C File Offset: 0x001C335C
	private void OnDrawGizmosSelected()
	{
		int emissionCell = this.GetEmissionCell();
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(Grid.CellToPos(emissionCell) + Vector3.right / 2f + Vector3.up / 2f, 0.2f);
	}

	// Token: 0x06004E38 RID: 20024 RVA: 0x001C51B0 File Offset: 0x001C33B0
	protected override Action<int> GetStaticUnregister()
	{
		return new Action<int>(RadiationEmitter.StaticUnregister);
	}

	// Token: 0x040033EA RID: 13290
	public bool radiusProportionalToRads;

	// Token: 0x040033EB RID: 13291
	[SerializeField]
	public short emitRadiusX = 4;

	// Token: 0x040033EC RID: 13292
	[SerializeField]
	public short emitRadiusY = 4;

	// Token: 0x040033ED RID: 13293
	[SerializeField]
	public float emitRads = 10f;

	// Token: 0x040033EE RID: 13294
	[SerializeField]
	public float emitRate = 1f;

	// Token: 0x040033EF RID: 13295
	[SerializeField]
	public float emitSpeed = 1f;

	// Token: 0x040033F0 RID: 13296
	[SerializeField]
	public float emitDirection;

	// Token: 0x040033F1 RID: 13297
	[SerializeField]
	public float emitAngle = 360f;

	// Token: 0x040033F2 RID: 13298
	[SerializeField]
	public RadiationEmitter.RadiationEmitterType emitType;

	// Token: 0x040033F3 RID: 13299
	[SerializeField]
	public Vector3 emissionOffset = Vector3.zero;

	// Token: 0x02001B6C RID: 7020
	public enum RadiationEmitterType
	{
		// Token: 0x040082E2 RID: 33506
		Constant,
		// Token: 0x040082E3 RID: 33507
		Pulsing,
		// Token: 0x040082E4 RID: 33508
		PulsingAveraged,
		// Token: 0x040082E5 RID: 33509
		SimplePulse,
		// Token: 0x040082E6 RID: 33510
		RadialBeams,
		// Token: 0x040082E7 RID: 33511
		Attractor
	}
}
