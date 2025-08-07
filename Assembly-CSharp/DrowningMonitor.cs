using System;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000862 RID: 2146
[AddComponentMenu("KMonoBehaviour/scripts/DrowningMonitor")]
public class DrowningMonitor : KMonoBehaviour, IWiltCause, ISlicedSim1000ms
{
	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x001474A3 File Offset: 0x001456A3
	private OccupyArea occupyArea
	{
		get
		{
			if (this._occupyArea == null)
			{
				this._occupyArea = base.GetComponent<OccupyArea>();
			}
			return this._occupyArea;
		}
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x001474C5 File Offset: 0x001456C5
	public bool Drowning
	{
		get
		{
			return this.drowning;
		}
	}

	// Token: 0x06003AE9 RID: 15081 RVA: 0x001474D0 File Offset: 0x001456D0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.timeToDrown = 75f;
		if (DrowningMonitor.drowningEffect == null)
		{
			DrowningMonitor.drowningEffect = new Effect("Drowning", CREATURES.STATUSITEMS.DROWNING.NAME, CREATURES.STATUSITEMS.DROWNING.TOOLTIP, 0f, false, false, true, null, -1f, 0f, null, "");
			DrowningMonitor.drowningEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -100f, CREATURES.STATUSITEMS.DROWNING.NAME, false, false, true));
		}
		if (DrowningMonitor.saturatedEffect == null)
		{
			DrowningMonitor.saturatedEffect = new Effect("Saturated", CREATURES.STATUSITEMS.SATURATED.NAME, CREATURES.STATUSITEMS.SATURATED.TOOLTIP, 0f, false, false, true, null, -1f, 0f, null, "");
			DrowningMonitor.saturatedEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -100f, CREATURES.STATUSITEMS.SATURATED.NAME, false, false, true));
		}
	}

	// Token: 0x06003AEA RID: 15082 RVA: 0x001475E0 File Offset: 0x001457E0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SlicedUpdaterSim1000ms<DrowningMonitor>.instance.RegisterUpdate1000ms(this);
		this.OnMove();
		this.CheckDrowning(null);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnMove), "DrowningMonitor.OnSpawn");
	}

	// Token: 0x06003AEB RID: 15083 RVA: 0x00147630 File Offset: 0x00145830
	private void OnMove()
	{
		if (this.partitionerEntry.IsValid())
		{
			Extents extents = this.occupyArea.GetExtents();
			GameScenePartitioner.Instance.UpdatePosition(this.partitionerEntry, extents);
		}
		else
		{
			this.partitionerEntry = GameScenePartitioner.Instance.Add("DrowningMonitor.OnSpawn", base.gameObject, this.occupyArea.GetExtents(), GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnLiquidChanged));
		}
		this.CheckDrowning(null);
	}

	// Token: 0x06003AEC RID: 15084 RVA: 0x001476AC File Offset: 0x001458AC
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnMove));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		SlicedUpdaterSim1000ms<DrowningMonitor>.instance.UnregisterUpdate1000ms(this);
		base.OnCleanUp();
	}

	// Token: 0x06003AED RID: 15085 RVA: 0x001476EC File Offset: 0x001458EC
	private void CheckDrowning(object data = null)
	{
		if (this.drowned)
		{
			return;
		}
		int num = Grid.PosToCell(base.gameObject.transform.GetPosition());
		if (!this.IsCellSafe(num))
		{
			if (!this.drowning)
			{
				this.drowning = true;
				base.GetComponent<KPrefabID>().AddTag(GameTags.Creatures.Drowning, false);
				base.Trigger(1949704522, null);
			}
			if (this.timeToDrown <= 0f && this.canDrownToDeath)
			{
				DeathMonitor.Instance smi = this.GetSMI<DeathMonitor.Instance>();
				if (smi != null)
				{
					smi.Kill(Db.Get().Deaths.Drowned);
				}
				base.Trigger(-750750377, null);
				this.drowned = true;
			}
		}
		else if (this.drowning)
		{
			this.drowning = false;
			base.GetComponent<KPrefabID>().RemoveTag(GameTags.Creatures.Drowning);
			base.Trigger(99949694, null);
		}
		if (this.livesUnderWater)
		{
			this.saturatedStatusGuid = this.selectable.ToggleStatusItem(Db.Get().CreatureStatusItems.Saturated, this.saturatedStatusGuid, this.drowning, this);
		}
		else
		{
			this.drowningStatusGuid = this.selectable.ToggleStatusItem(Db.Get().CreatureStatusItems.Drowning, this.drowningStatusGuid, this.drowning, this);
		}
		if (this.effects != null)
		{
			if (this.drowning)
			{
				if (this.livesUnderWater)
				{
					this.effects.Add(DrowningMonitor.saturatedEffect, false);
					return;
				}
				this.effects.Add(DrowningMonitor.drowningEffect, false);
				return;
			}
			else
			{
				if (this.livesUnderWater)
				{
					this.effects.Remove(DrowningMonitor.saturatedEffect);
					return;
				}
				this.effects.Remove(DrowningMonitor.drowningEffect);
			}
		}
	}

	// Token: 0x06003AEE RID: 15086 RVA: 0x00147892 File Offset: 0x00145A92
	private static bool CellSafeTest(int testCell, object data)
	{
		return !Grid.IsNavigatableLiquid(testCell);
	}

	// Token: 0x06003AEF RID: 15087 RVA: 0x0014789D File Offset: 0x00145A9D
	public bool IsCellSafe(int cell)
	{
		return this.occupyArea.TestArea(cell, this, DrowningMonitor.CellSafeTestDelegate);
	}

	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x001478B1 File Offset: 0x00145AB1
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[] { WiltCondition.Condition.Drowning };
		}
	}

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x001478BD File Offset: 0x00145ABD
	public string WiltStateString
	{
		get
		{
			if (this.livesUnderWater)
			{
				return "    • " + CREATURES.STATUSITEMS.SATURATED.NAME;
			}
			return "    • " + CREATURES.STATUSITEMS.DROWNING.NAME;
		}
	}

	// Token: 0x06003AF2 RID: 15090 RVA: 0x001478F0 File Offset: 0x00145AF0
	private void OnLiquidChanged(object data)
	{
		this.CheckDrowning(null);
	}

	// Token: 0x06003AF3 RID: 15091 RVA: 0x001478FC File Offset: 0x00145AFC
	public void SlicedSim1000ms(float dt)
	{
		this.CheckDrowning(null);
		if (this.drowning)
		{
			if (!this.drowned)
			{
				this.timeToDrown -= dt;
				if (this.timeToDrown <= 0f)
				{
					this.CheckDrowning(null);
					return;
				}
			}
		}
		else
		{
			this.timeToDrown += dt * 5f;
			this.timeToDrown = Mathf.Clamp(this.timeToDrown, 0f, 75f);
		}
	}

	// Token: 0x0400241A RID: 9242
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x0400241B RID: 9243
	[MyCmpGet]
	private Effects effects;

	// Token: 0x0400241C RID: 9244
	private OccupyArea _occupyArea;

	// Token: 0x0400241D RID: 9245
	[Serialize]
	[SerializeField]
	private float timeToDrown;

	// Token: 0x0400241E RID: 9246
	[Serialize]
	private bool drowned;

	// Token: 0x0400241F RID: 9247
	private bool drowning;

	// Token: 0x04002420 RID: 9248
	protected const float MaxDrownTime = 75f;

	// Token: 0x04002421 RID: 9249
	protected const float RegenRate = 5f;

	// Token: 0x04002422 RID: 9250
	protected const float CellLiquidThreshold = 0.95f;

	// Token: 0x04002423 RID: 9251
	public bool canDrownToDeath = true;

	// Token: 0x04002424 RID: 9252
	public bool livesUnderWater;

	// Token: 0x04002425 RID: 9253
	private Guid drowningStatusGuid;

	// Token: 0x04002426 RID: 9254
	private Guid saturatedStatusGuid;

	// Token: 0x04002427 RID: 9255
	private Extents extents;

	// Token: 0x04002428 RID: 9256
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04002429 RID: 9257
	public static Effect drowningEffect;

	// Token: 0x0400242A RID: 9258
	public static Effect saturatedEffect;

	// Token: 0x0400242B RID: 9259
	private static readonly Func<int, object, bool> CellSafeTestDelegate = (int testCell, object data) => DrowningMonitor.CellSafeTest(testCell, data);
}
