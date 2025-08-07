using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000BBD RID: 3005
[AddComponentMenu("KMonoBehaviour/scripts/Trappable")]
public class Trappable : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x060059F2 RID: 23026 RVA: 0x00207F50 File Offset: 0x00206150
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Register();
		this.OnCellChange();
	}

	// Token: 0x060059F3 RID: 23027 RVA: 0x00207F64 File Offset: 0x00206164
	protected override void OnCleanUp()
	{
		this.Unregister();
		base.OnCleanUp();
	}

	// Token: 0x060059F4 RID: 23028 RVA: 0x00207F74 File Offset: 0x00206174
	private void OnCellChange()
	{
		int num = Grid.PosToCell(this);
		GameScenePartitioner.Instance.TriggerEvent(num, GameScenePartitioner.Instance.trapsLayer, this);
	}

	// Token: 0x060059F5 RID: 23029 RVA: 0x00207F9E File Offset: 0x0020619E
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.Register();
	}

	// Token: 0x060059F6 RID: 23030 RVA: 0x00207FAC File Offset: 0x002061AC
	protected override void OnCmpDisable()
	{
		this.Unregister();
		base.OnCmpDisable();
	}

	// Token: 0x060059F7 RID: 23031 RVA: 0x00207FBC File Offset: 0x002061BC
	private void Register()
	{
		if (this.registered)
		{
			return;
		}
		base.Subscribe<Trappable>(856640610, Trappable.OnStoreDelegate);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange), "Trappable.Register");
		this.registered = true;
	}

	// Token: 0x060059F8 RID: 23032 RVA: 0x0020800C File Offset: 0x0020620C
	private void Unregister()
	{
		if (!this.registered)
		{
			return;
		}
		base.Unsubscribe<Trappable>(856640610, Trappable.OnStoreDelegate, false);
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
		this.registered = false;
	}

	// Token: 0x060059F9 RID: 23033 RVA: 0x0020804B File Offset: 0x0020624B
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.BUILDINGEFFECTS.CAPTURE_METHOD_LAND_TRAP, UI.BUILDINGEFFECTS.TOOLTIPS.CAPTURE_METHOD_TRAP, Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x060059FA RID: 23034 RVA: 0x00208074 File Offset: 0x00206274
	public void OnStore(object data)
	{
		Storage storage = data as Storage;
		if (storage && (storage.GetComponent<Trap>() != null || storage.GetSMI<ReusableTrap.Instance>() != null))
		{
			base.gameObject.AddTag(GameTags.Trapped);
			Navigator component = base.gameObject.GetComponent<Navigator>();
			if (component != null)
			{
				component.Stop(false, true);
			}
			Brain component2 = base.gameObject.GetComponent<Brain>();
			if (component2 != null)
			{
				Game.BrainScheduler.PrioritizeBrain(component2);
				return;
			}
		}
		else
		{
			base.gameObject.RemoveTag(GameTags.Trapped);
		}
	}

	// Token: 0x04003BC3 RID: 15299
	private bool registered;

	// Token: 0x04003BC4 RID: 15300
	private static readonly EventSystem.IntraObjectHandler<Trappable> OnStoreDelegate = new EventSystem.IntraObjectHandler<Trappable>(delegate(Trappable component, object data)
	{
		component.OnStore(data);
	});
}
