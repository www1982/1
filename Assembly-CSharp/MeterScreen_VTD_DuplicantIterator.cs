using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000D67 RID: 3431
public abstract class MeterScreen_VTD_DuplicantIterator : MeterScreen_ValueTrackerDisplayer
{
	// Token: 0x06006A58 RID: 27224 RVA: 0x00281A10 File Offset: 0x0027FC10
	protected virtual void UpdateDisplayInfo(BaseEventData base_ev_data, IList<MinionIdentity> minions)
	{
		PointerEventData pointerEventData = base_ev_data as PointerEventData;
		if (pointerEventData == null)
		{
			return;
		}
		List<MinionIdentity> worldMinionIdentities = this.GetWorldMinionIdentities();
		PointerEventData.InputButton button = pointerEventData.button;
		if (button != PointerEventData.InputButton.Left)
		{
			if (button != PointerEventData.InputButton.Right)
			{
				return;
			}
			this.lastSelectedDuplicantIndex = -1;
		}
		else
		{
			if (worldMinionIdentities.Count < this.lastSelectedDuplicantIndex)
			{
				this.lastSelectedDuplicantIndex = -1;
			}
			if (worldMinionIdentities.Count > 0)
			{
				this.lastSelectedDuplicantIndex = (this.lastSelectedDuplicantIndex + 1) % worldMinionIdentities.Count;
				MinionIdentity minionIdentity = minions[this.lastSelectedDuplicantIndex];
				SelectTool.Instance.SelectAndFocus(minionIdentity.transform.GetPosition(), minionIdentity.GetComponent<KSelectable>(), Vector3.zero);
				return;
			}
		}
	}

	// Token: 0x06006A59 RID: 27225 RVA: 0x00281AA8 File Offset: 0x0027FCA8
	public override void OnClick(BaseEventData base_ev_data)
	{
		List<MinionIdentity> worldMinionIdentities = this.GetWorldMinionIdentities();
		this.UpdateDisplayInfo(base_ev_data, worldMinionIdentities);
		this.OnTooltip();
		this.Tooltip.forceRefresh = true;
	}

	// Token: 0x06006A5A RID: 27226 RVA: 0x00281AD7 File Offset: 0x0027FCD7
	protected void AddToolTipLine(string str, bool selected)
	{
		if (selected)
		{
			this.Tooltip.AddMultiStringTooltip("<color=#F0B310FF>" + str + "</color>", this.ToolTipStyle_Property);
			return;
		}
		this.Tooltip.AddMultiStringTooltip(str, this.ToolTipStyle_Property);
	}

	// Token: 0x06006A5B RID: 27227 RVA: 0x00281B10 File Offset: 0x0027FD10
	protected void AddToolTipAmountPercentLine(AmountInstance amount, MinionIdentity id, bool selected)
	{
		string text = id.GetComponent<KSelectable>().GetName() + ":  " + Mathf.Round(amount.value).ToString() + "%";
		this.AddToolTipLine(text, selected);
	}

	// Token: 0x04004881 RID: 18561
	protected int lastSelectedDuplicantIndex = -1;
}
