using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000886 RID: 2182
[AddComponentMenu("KMonoBehaviour/scripts/SubmersionMonitor")]
public class SubmersionMonitor : KMonoBehaviour, IGameObjectEffectDescriptor, IWiltCause, ISim1000ms
{
	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x06003BFC RID: 15356 RVA: 0x0014CAD7 File Offset: 0x0014ACD7
	public bool Dry
	{
		get
		{
			return this.dry;
		}
	}

	// Token: 0x06003BFD RID: 15357 RVA: 0x0014CADF File Offset: 0x0014ACDF
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.OnMove();
		this.CheckDry();
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnMove), "SubmersionMonitor.OnSpawn");
	}

	// Token: 0x06003BFE RID: 15358 RVA: 0x0014CB18 File Offset: 0x0014AD18
	private void OnMove()
	{
		this.position = Grid.PosToCell(base.gameObject);
		if (this.partitionerEntry.IsValid())
		{
			GameScenePartitioner.Instance.UpdatePosition(this.partitionerEntry, this.position);
		}
		else
		{
			Vector2I vector2I = Grid.PosToXY(base.transform.GetPosition());
			Extents extents = new Extents(vector2I.x, vector2I.y, 1, 2);
			this.partitionerEntry = GameScenePartitioner.Instance.Add("DrowningMonitor.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnLiquidChanged));
		}
		this.CheckDry();
	}

	// Token: 0x06003BFF RID: 15359 RVA: 0x0014CBB9 File Offset: 0x0014ADB9
	private void OnDrawGizmosSelected()
	{
	}

	// Token: 0x06003C00 RID: 15360 RVA: 0x0014CBBB File Offset: 0x0014ADBB
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnMove));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06003C01 RID: 15361 RVA: 0x0014CBEF File Offset: 0x0014ADEF
	public void Configure(float _maxStamina, float _staminaRegenRate, float _cellLiquidThreshold = 0.95f)
	{
		this.cellLiquidThreshold = _cellLiquidThreshold;
	}

	// Token: 0x06003C02 RID: 15362 RVA: 0x0014CBF8 File Offset: 0x0014ADF8
	public void Sim1000ms(float dt)
	{
		this.CheckDry();
	}

	// Token: 0x06003C03 RID: 15363 RVA: 0x0014CC00 File Offset: 0x0014AE00
	private void CheckDry()
	{
		if (!this.IsCellSafe())
		{
			if (!this.dry)
			{
				this.dry = true;
				base.Trigger(-2057657673, null);
				return;
			}
		}
		else if (this.dry)
		{
			this.dry = false;
			base.Trigger(1555379996, null);
		}
	}

	// Token: 0x06003C04 RID: 15364 RVA: 0x0014CC4C File Offset: 0x0014AE4C
	public bool IsCellSafe()
	{
		int num = Grid.PosToCell(base.gameObject);
		return Grid.IsValidCell(num) && Grid.IsSubstantialLiquid(num, this.cellLiquidThreshold);
	}

	// Token: 0x06003C05 RID: 15365 RVA: 0x0014CC80 File Offset: 0x0014AE80
	private void OnLiquidChanged(object data)
	{
		this.CheckDry();
	}

	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x06003C06 RID: 15366 RVA: 0x0014CC88 File Offset: 0x0014AE88
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[] { WiltCondition.Condition.DryingOut };
		}
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x06003C07 RID: 15367 RVA: 0x0014CC94 File Offset: 0x0014AE94
	public string WiltStateString
	{
		get
		{
			if (this.Dry)
			{
				return Db.Get().CreatureStatusItems.DryingOut.resolveStringCallback(CREATURES.STATUSITEMS.DRYINGOUT.NAME, this);
			}
			return "";
		}
	}

	// Token: 0x06003C08 RID: 15368 RVA: 0x0014CCC8 File Offset: 0x0014AEC8
	public void SetIncapacitated(bool state)
	{
	}

	// Token: 0x06003C09 RID: 15369 RVA: 0x0014CCCA File Offset: 0x0014AECA
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_SUBMERSION, UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_SUBMERSION, Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x040024CA RID: 9418
	private int position;

	// Token: 0x040024CB RID: 9419
	private bool dry;

	// Token: 0x040024CC RID: 9420
	protected float cellLiquidThreshold = 0.2f;

	// Token: 0x040024CD RID: 9421
	private Extents extents;

	// Token: 0x040024CE RID: 9422
	private HandleVector<int>.Handle partitionerEntry;
}
