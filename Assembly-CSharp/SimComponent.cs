using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000B0D RID: 2829
public abstract class SimComponent : KMonoBehaviour, ISim200ms
{
	// Token: 0x170005D2 RID: 1490
	// (get) Token: 0x06005315 RID: 21269 RVA: 0x001E3BB0 File Offset: 0x001E1DB0
	public bool IsSimActive
	{
		get
		{
			return this.simActive;
		}
	}

	// Token: 0x06005316 RID: 21270 RVA: 0x001E3BB8 File Offset: 0x001E1DB8
	protected virtual void OnSimRegister(HandleVector<Game.ComplexCallbackInfo<int>>.Handle cb_handle)
	{
	}

	// Token: 0x06005317 RID: 21271 RVA: 0x001E3BBA File Offset: 0x001E1DBA
	protected virtual void OnSimRegistered()
	{
	}

	// Token: 0x06005318 RID: 21272 RVA: 0x001E3BBC File Offset: 0x001E1DBC
	protected virtual void OnSimActivate()
	{
	}

	// Token: 0x06005319 RID: 21273 RVA: 0x001E3BBE File Offset: 0x001E1DBE
	protected virtual void OnSimDeactivate()
	{
	}

	// Token: 0x0600531A RID: 21274 RVA: 0x001E3BC0 File Offset: 0x001E1DC0
	protected virtual void OnSimUnregister()
	{
	}

	// Token: 0x0600531B RID: 21275
	protected abstract Action<int> GetStaticUnregister();

	// Token: 0x0600531C RID: 21276 RVA: 0x001E3BC2 File Offset: 0x001E1DC2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600531D RID: 21277 RVA: 0x001E3BCA File Offset: 0x001E1DCA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.SimRegister();
	}

	// Token: 0x0600531E RID: 21278 RVA: 0x001E3BD8 File Offset: 0x001E1DD8
	protected override void OnCleanUp()
	{
		this.SimUnregister();
		base.OnCleanUp();
	}

	// Token: 0x0600531F RID: 21279 RVA: 0x001E3BE6 File Offset: 0x001E1DE6
	public void SetSimActive(bool active)
	{
		this.simActive = active;
		this.dirty = true;
	}

	// Token: 0x06005320 RID: 21280 RVA: 0x001E3BF6 File Offset: 0x001E1DF6
	public void Sim200ms(float dt)
	{
		if (!Sim.IsValidHandle(this.simHandle))
		{
			return;
		}
		this.UpdateSimState();
	}

	// Token: 0x06005321 RID: 21281 RVA: 0x001E3C0C File Offset: 0x001E1E0C
	private void UpdateSimState()
	{
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		if (this.simActive)
		{
			this.OnSimActivate();
			return;
		}
		this.OnSimDeactivate();
	}

	// Token: 0x06005322 RID: 21282 RVA: 0x001E3C34 File Offset: 0x001E1E34
	private void SimRegister()
	{
		if (base.isSpawned && this.simHandle == -1)
		{
			this.simHandle = -2;
			Action<int> static_unregister = this.GetStaticUnregister();
			HandleVector<Game.ComplexCallbackInfo<int>>.Handle handle2 = Game.Instance.simComponentCallbackManager.Add(delegate(int handle, object data)
			{
				SimComponent.OnSimRegistered(this, handle, static_unregister);
			}, this, "SimComponent.SimRegister");
			this.OnSimRegister(handle2);
		}
	}

	// Token: 0x06005323 RID: 21283 RVA: 0x001E3C9C File Offset: 0x001E1E9C
	private void SimUnregister()
	{
		if (Sim.IsValidHandle(this.simHandle))
		{
			this.OnSimUnregister();
		}
		this.simHandle = -1;
	}

	// Token: 0x06005324 RID: 21284 RVA: 0x001E3CB8 File Offset: 0x001E1EB8
	private static void OnSimRegistered(SimComponent instance, int handle, Action<int> static_unregister)
	{
		if (instance != null)
		{
			instance.simHandle = handle;
			instance.OnSimRegistered();
			return;
		}
		static_unregister(handle);
	}

	// Token: 0x06005325 RID: 21285 RVA: 0x001E3CD8 File Offset: 0x001E1ED8
	[Conditional("ENABLE_LOGGER")]
	protected void Log(string msg)
	{
	}

	// Token: 0x040037E0 RID: 14304
	[SerializeField]
	protected int simHandle = -1;

	// Token: 0x040037E1 RID: 14305
	private bool simActive = true;

	// Token: 0x040037E2 RID: 14306
	private bool dirty = true;
}
