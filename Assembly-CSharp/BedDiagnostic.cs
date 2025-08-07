using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020008A4 RID: 2212
public class BedDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003D7C RID: 15740 RVA: 0x00157214 File Offset: 0x00155414
	public BedDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "icon_action_region_bedroom";
		base.AddCriterion("CheckEnoughBeds", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.CRITERIA.CHECKENOUGHBEDS, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckEnoughBeds)));
		base.AddCriterion("CheckReachability", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.CRITERIA.CHECKREACHABILITY, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckReachability)));
		this.NO_MINIONS_WITH_STAMINA = (base.IsWorldModuleInterior ? UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.NO_MINIONS_ROCKET : UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.NO_MINIONS_PLANETOID);
	}

	// Token: 0x06003D7D RID: 15741 RVA: 0x001572A8 File Offset: 0x001554A8
	private ColonyDiagnostic.DiagnosticResult CheckEnoughBeds()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.NORMAL;
		if (this.minionsWithStamina.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = this.NO_MINIONS_WITH_STAMINA;
		}
		else if (Components.NormalBeds.GlobalCount < this.minionsWithStamina.Count)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.NOT_ENOUGH_BEDS;
		}
		return diagnosticResult;
	}

	// Token: 0x06003D7E RID: 15742 RVA: 0x00157338 File Offset: 0x00155538
	private ColonyDiagnostic.DiagnosticResult CheckReachability()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.NORMAL;
		if (this.minionsWithStamina.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = this.NO_MINIONS_WITH_STAMINA;
		}
		else
		{
			ListPool<Sleepable, BedDiagnostic>.PooledList pooledList = ListPool<Sleepable, BedDiagnostic>.Allocate();
			foreach (Sleepable sleepable in Components.NormalBeds.WorldItemsEnumerate(base.worldID, true))
			{
				if (sleepable.assignable != null && !sleepable.assignable.IsAssigned())
				{
					pooledList.Add(sleepable);
				}
			}
			foreach (MinionIdentity minionIdentity in this.minionsWithStamina)
			{
				Navigator component = minionIdentity.GetComponent<Navigator>();
				AssignableSlotInstance slot = minionIdentity.assignableProxy.Get().GetComponent<Ownables>().GetSlot(Db.Get().AssignableSlots.Bed);
				if (!slot.IsAssigned() && diagnosticResult.opinion == ColonyDiagnostic.DiagnosticResult.Opinion.Normal)
				{
					Sleepable sleepable2 = null;
					foreach (Sleepable sleepable3 in pooledList)
					{
						if (component.CanReach(sleepable3.approachable) && sleepable3.assignable.CanAutoAssignTo(minionIdentity))
						{
							sleepable2 = sleepable3;
							break;
						}
					}
					if (sleepable2 != null)
					{
						pooledList.Remove(sleepable2);
					}
					else
					{
						diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
						diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.MISSING_ASSIGNMENT;
						diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(minionIdentity.gameObject.transform.position, minionIdentity.gameObject);
					}
				}
				else if (slot.IsAssigned() && !component.CanReach(Grid.PosToCell(slot.assignable)))
				{
					diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
					diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.CANT_REACH;
					diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(minionIdentity.gameObject.transform.position, minionIdentity.gameObject);
					break;
				}
			}
			pooledList.Recycle();
		}
		return diagnosticResult;
	}

	// Token: 0x06003D7F RID: 15743 RVA: 0x001575D0 File Offset: 0x001557D0
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, this.NO_MINIONS_WITH_STAMINA, null);
		if (ColonyDiagnosticUtility.IgnoreRocketsWithNoCrewRequested(base.worldID, out diagnosticResult))
		{
			return diagnosticResult;
		}
		this.RefreshData();
		return base.Evaluate();
	}

	// Token: 0x06003D80 RID: 15744 RVA: 0x00157609 File Offset: 0x00155809
	private void RefreshData()
	{
		this.minionsWithStamina = Components.LiveMinionIdentities.GetWorldItems(base.worldID, true, new Func<MinionIdentity, bool>(this.MinionFilter));
	}

	// Token: 0x06003D81 RID: 15745 RVA: 0x0015762E File Offset: 0x0015582E
	private bool MinionFilter(MinionIdentity minion)
	{
		return minion.modifiers.amounts.Has(Db.Get().Amounts.Stamina);
	}

	// Token: 0x06003D82 RID: 15746 RVA: 0x00157650 File Offset: 0x00155850
	public override string GetAverageValueString()
	{
		if (this.minionsWithStamina == null)
		{
			this.RefreshData();
		}
		return Components.NormalBeds.CountWorldItems(base.worldID, true).ToString() + "/" + this.minionsWithStamina.Count.ToString();
	}

	// Token: 0x040025FD RID: 9725
	private List<MinionIdentity> minionsWithStamina;

	// Token: 0x040025FE RID: 9726
	private const bool INCLUDE_CHILD_WORLDS = true;

	// Token: 0x040025FF RID: 9727
	private readonly string NO_MINIONS_WITH_STAMINA;
}
