using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000DF9 RID: 3577
public class GeneShufflerSideScreen : SideScreenContent
{
	// Token: 0x060070E6 RID: 28902 RVA: 0x002AEFB1 File Offset: 0x002AD1B1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.button.onClick += this.OnButtonClick;
		this.Refresh();
	}

	// Token: 0x060070E7 RID: 28903 RVA: 0x002AEFD6 File Offset: 0x002AD1D6
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<GeneShuffler>() != null;
	}

	// Token: 0x060070E8 RID: 28904 RVA: 0x002AEFE4 File Offset: 0x002AD1E4
	public override void SetTarget(GameObject target)
	{
		GeneShuffler component = target.GetComponent<GeneShuffler>();
		if (component == null)
		{
			global::Debug.LogError("Target doesn't have a GeneShuffler associated with it.");
			return;
		}
		this.target = component;
		this.Refresh();
	}

	// Token: 0x060070E9 RID: 28905 RVA: 0x002AF01C File Offset: 0x002AD21C
	private void OnButtonClick()
	{
		if (this.target.WorkComplete)
		{
			this.target.SetWorkTime(0f);
			return;
		}
		if (this.target.IsConsumed)
		{
			this.target.RequestRecharge(!this.target.RechargeRequested);
			this.Refresh();
		}
	}

	// Token: 0x060070EA RID: 28906 RVA: 0x002AF074 File Offset: 0x002AD274
	private void Refresh()
	{
		if (!(this.target != null))
		{
			this.contents.SetActive(false);
			return;
		}
		if (this.target.WorkComplete)
		{
			this.contents.SetActive(true);
			this.label.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.COMPLETE;
			this.button.gameObject.SetActive(true);
			this.buttonLabel.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.BUTTON;
			return;
		}
		if (this.target.IsConsumed)
		{
			this.contents.SetActive(true);
			this.button.gameObject.SetActive(true);
			if (this.target.RechargeRequested)
			{
				this.label.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.CONSUMED_WAITING;
				this.buttonLabel.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.BUTTON_RECHARGE_CANCEL;
				return;
			}
			this.label.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.CONSUMED;
			this.buttonLabel.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.BUTTON_RECHARGE;
			return;
		}
		else
		{
			if (this.target.IsWorking)
			{
				this.contents.SetActive(true);
				this.label.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.UNDERWAY;
				this.button.gameObject.SetActive(false);
				return;
			}
			this.contents.SetActive(false);
			return;
		}
	}

	// Token: 0x04004DB0 RID: 19888
	[SerializeField]
	private LocText label;

	// Token: 0x04004DB1 RID: 19889
	[SerializeField]
	private KButton button;

	// Token: 0x04004DB2 RID: 19890
	[SerializeField]
	private LocText buttonLabel;

	// Token: 0x04004DB3 RID: 19891
	[SerializeField]
	private GeneShuffler target;

	// Token: 0x04004DB4 RID: 19892
	[SerializeField]
	private GameObject contents;
}
