using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000955 RID: 2389
public class HighEnergyParticleStorage : KMonoBehaviour, IStorage
{
	// Token: 0x170004E8 RID: 1256
	// (get) Token: 0x06004493 RID: 17555 RVA: 0x0018A201 File Offset: 0x00188401
	public float Particles
	{
		get
		{
			return this.particles;
		}
	}

	// Token: 0x170004E9 RID: 1257
	// (get) Token: 0x06004494 RID: 17556 RVA: 0x0018A209 File Offset: 0x00188409
	// (set) Token: 0x06004495 RID: 17557 RVA: 0x0018A211 File Offset: 0x00188411
	public bool allowUIItemRemoval { get; set; }

	// Token: 0x06004496 RID: 17558 RVA: 0x0018A21C File Offset: 0x0018841C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.autoStore)
		{
			HighEnergyParticlePort component = base.gameObject.GetComponent<HighEnergyParticlePort>();
			component.onParticleCapture = (HighEnergyParticlePort.OnParticleCapture)Delegate.Combine(component.onParticleCapture, new HighEnergyParticlePort.OnParticleCapture(this.OnParticleCapture));
			component.onParticleCaptureAllowed = (HighEnergyParticlePort.OnParticleCaptureAllowed)Delegate.Combine(component.onParticleCaptureAllowed, new HighEnergyParticlePort.OnParticleCaptureAllowed(this.OnParticleCaptureAllowed));
		}
		this.SetupStorageStatusItems();
	}

	// Token: 0x06004497 RID: 17559 RVA: 0x0018A28B File Offset: 0x0018848B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateLogicPorts();
	}

	// Token: 0x06004498 RID: 17560 RVA: 0x0018A29C File Offset: 0x0018849C
	private void UpdateLogicPorts()
	{
		if (this._logicPorts != null)
		{
			bool flag = this.IsFull();
			this._logicPorts.SendSignal(this.PORT_ID, flag ? 1 : 0);
		}
	}

	// Token: 0x06004499 RID: 17561 RVA: 0x0018A2DB File Offset: 0x001884DB
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.autoStore)
		{
			HighEnergyParticlePort component = base.gameObject.GetComponent<HighEnergyParticlePort>();
			component.onParticleCapture = (HighEnergyParticlePort.OnParticleCapture)Delegate.Remove(component.onParticleCapture, new HighEnergyParticlePort.OnParticleCapture(this.OnParticleCapture));
		}
	}

	// Token: 0x0600449A RID: 17562 RVA: 0x0018A318 File Offset: 0x00188518
	private void OnParticleCapture(HighEnergyParticle particle)
	{
		float num = Mathf.Min(particle.payload, this.capacity - this.particles);
		this.Store(num);
		particle.payload -= num;
		if (particle.payload > 0f)
		{
			base.gameObject.GetComponent<HighEnergyParticlePort>().Uncapture(particle);
		}
	}

	// Token: 0x0600449B RID: 17563 RVA: 0x0018A372 File Offset: 0x00188572
	private bool OnParticleCaptureAllowed(HighEnergyParticle particle)
	{
		return this.particles < this.capacity && this.receiverOpen;
	}

	// Token: 0x0600449C RID: 17564 RVA: 0x0018A38C File Offset: 0x0018858C
	private void DeltaParticles(float delta)
	{
		this.particles += delta;
		if (this.particles <= 0f)
		{
			base.Trigger(155636535, base.transform.gameObject);
		}
		base.Trigger(-1837862626, base.transform.gameObject);
		this.UpdateLogicPorts();
	}

	// Token: 0x0600449D RID: 17565 RVA: 0x0018A3E8 File Offset: 0x001885E8
	public float Store(float amount)
	{
		float num = Mathf.Min(amount, this.RemainingCapacity());
		this.DeltaParticles(num);
		return num;
	}

	// Token: 0x0600449E RID: 17566 RVA: 0x0018A40A File Offset: 0x0018860A
	public float ConsumeAndGet(float amount)
	{
		amount = Mathf.Min(this.Particles, amount);
		this.DeltaParticles(-amount);
		return amount;
	}

	// Token: 0x0600449F RID: 17567 RVA: 0x0018A423 File Offset: 0x00188623
	[ContextMenu("Trigger Stored Event")]
	public void DEBUG_TriggerStorageEvent()
	{
		base.Trigger(-1837862626, base.transform.gameObject);
	}

	// Token: 0x060044A0 RID: 17568 RVA: 0x0018A43B File Offset: 0x0018863B
	[ContextMenu("Trigger Zero Event")]
	public void DEBUG_TriggerZeroEvent()
	{
		this.ConsumeAndGet(this.particles + 1f);
	}

	// Token: 0x060044A1 RID: 17569 RVA: 0x0018A450 File Offset: 0x00188650
	public float ConsumeAll()
	{
		return this.ConsumeAndGet(this.particles);
	}

	// Token: 0x060044A2 RID: 17570 RVA: 0x0018A45E File Offset: 0x0018865E
	public bool HasRadiation()
	{
		return this.Particles > 0f;
	}

	// Token: 0x060044A3 RID: 17571 RVA: 0x0018A46D File Offset: 0x0018866D
	public GameObject Drop(GameObject go, bool do_disease_transfer = true)
	{
		return null;
	}

	// Token: 0x060044A4 RID: 17572 RVA: 0x0018A470 File Offset: 0x00188670
	public List<GameObject> GetItems()
	{
		return new List<GameObject> { base.gameObject };
	}

	// Token: 0x060044A5 RID: 17573 RVA: 0x0018A483 File Offset: 0x00188683
	public bool IsFull()
	{
		return this.RemainingCapacity() <= 0f;
	}

	// Token: 0x060044A6 RID: 17574 RVA: 0x0018A495 File Offset: 0x00188695
	public bool IsEmpty()
	{
		return this.Particles == 0f;
	}

	// Token: 0x060044A7 RID: 17575 RVA: 0x0018A4A4 File Offset: 0x001886A4
	public float Capacity()
	{
		return this.capacity;
	}

	// Token: 0x060044A8 RID: 17576 RVA: 0x0018A4AC File Offset: 0x001886AC
	public float RemainingCapacity()
	{
		return Mathf.Max(this.capacity - this.Particles, 0f);
	}

	// Token: 0x060044A9 RID: 17577 RVA: 0x0018A4C5 File Offset: 0x001886C5
	public bool ShouldShowInUI()
	{
		return this.showInUI;
	}

	// Token: 0x060044AA RID: 17578 RVA: 0x0018A4CD File Offset: 0x001886CD
	public float GetAmountAvailable(Tag tag)
	{
		if (tag != GameTags.HighEnergyParticle)
		{
			return 0f;
		}
		return this.Particles;
	}

	// Token: 0x060044AB RID: 17579 RVA: 0x0018A4E8 File Offset: 0x001886E8
	public void ConsumeIgnoringDisease(Tag tag, float amount)
	{
		DebugUtil.DevAssert(tag == GameTags.HighEnergyParticle, "Consuming non-particle tag as amount", null);
		this.ConsumeAndGet(amount);
	}

	// Token: 0x060044AC RID: 17580 RVA: 0x0018A508 File Offset: 0x00188708
	private void SetupStorageStatusItems()
	{
		if (HighEnergyParticleStorage.capacityStatusItem == null)
		{
			HighEnergyParticleStorage.capacityStatusItem = new StatusItem("StorageLocker", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			HighEnergyParticleStorage.capacityStatusItem.resolveStringCallback = delegate(string str, object data)
			{
				HighEnergyParticleStorage highEnergyParticleStorage = (HighEnergyParticleStorage)data;
				string text = Util.FormatWholeNumber(highEnergyParticleStorage.particles);
				string text2 = Util.FormatWholeNumber(highEnergyParticleStorage.capacity);
				str = str.Replace("{Stored}", text);
				str = str.Replace("{Capacity}", text2);
				str = str.Replace("{Units}", UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES);
				return str;
			};
		}
		if (this.showCapacityStatusItem)
		{
			if (this.showCapacityAsMainStatus)
			{
				base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, HighEnergyParticleStorage.capacityStatusItem, this);
				return;
			}
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Stored, HighEnergyParticleStorage.capacityStatusItem, this);
		}
	}

	// Token: 0x04002DE8 RID: 11752
	[Serialize]
	[SerializeField]
	private float particles;

	// Token: 0x04002DE9 RID: 11753
	[Serialize]
	public float capacity = float.MaxValue;

	// Token: 0x04002DEA RID: 11754
	public bool showInUI = true;

	// Token: 0x04002DEB RID: 11755
	public bool showCapacityStatusItem;

	// Token: 0x04002DEC RID: 11756
	public bool showCapacityAsMainStatus;

	// Token: 0x04002DEE RID: 11758
	public bool autoStore;

	// Token: 0x04002DEF RID: 11759
	[Serialize]
	public bool receiverOpen = true;

	// Token: 0x04002DF0 RID: 11760
	[MyCmpGet]
	private LogicPorts _logicPorts;

	// Token: 0x04002DF1 RID: 11761
	public string PORT_ID = "";

	// Token: 0x04002DF2 RID: 11762
	private static StatusItem capacityStatusItem;
}
