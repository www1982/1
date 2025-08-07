using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000D03 RID: 3331
public class KleiItemDropScreen_PermitVis : KMonoBehaviour
{
	// Token: 0x060066D4 RID: 26324 RVA: 0x0026D9A8 File Offset: 0x0026BBA8
	public void ConfigureWith(DropScreenPresentationInfo info)
	{
		this.ResetState();
		this.equipmentVis.gameObject.SetActive(false);
		this.fallbackVis.gameObject.SetActive(false);
		if (info.UseEquipmentVis)
		{
			this.equipmentVis.gameObject.SetActive(true);
			this.equipmentVis.ConfigureWith(info);
			return;
		}
		this.fallbackVis.gameObject.SetActive(true);
		this.fallbackVis.ConfigureWith(info);
	}

	// Token: 0x060066D5 RID: 26325 RVA: 0x0026DA20 File Offset: 0x0026BC20
	public Promise AnimateIn()
	{
		return Updater.RunRoutine(this, this.AnimateInRoutine());
	}

	// Token: 0x060066D6 RID: 26326 RVA: 0x0026DA2E File Offset: 0x0026BC2E
	public Promise AnimateOut()
	{
		return Updater.RunRoutine(this, this.AnimateOutRoutine());
	}

	// Token: 0x060066D7 RID: 26327 RVA: 0x0026DA3C File Offset: 0x0026BC3C
	private IEnumerator AnimateInRoutine()
	{
		this.root.gameObject.SetActive(true);
		yield return Updater.Ease(delegate(Vector3 v3)
		{
			this.root.transform.localScale = v3;
		}, this.root.transform.localScale, Vector3.one, 0.5f, Easing.EaseOutBack, -1f);
		yield break;
	}

	// Token: 0x060066D8 RID: 26328 RVA: 0x0026DA4B File Offset: 0x0026BC4B
	private IEnumerator AnimateOutRoutine()
	{
		yield return Updater.Ease(delegate(Vector3 v3)
		{
			this.root.transform.localScale = v3;
		}, this.root.transform.localScale, Vector3.zero, 0.25f, null, -1f);
		this.root.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x060066D9 RID: 26329 RVA: 0x0026DA5A File Offset: 0x0026BC5A
	public void ResetState()
	{
		this.root.transform.localScale = Vector3.zero;
	}

	// Token: 0x04004685 RID: 18053
	[SerializeField]
	private RectTransform root;

	// Token: 0x04004686 RID: 18054
	[Header("Different Permit Visualizers")]
	[SerializeField]
	private KleiItemDropScreen_PermitVis_Fallback fallbackVis;

	// Token: 0x04004687 RID: 18055
	[SerializeField]
	private KleiItemDropScreen_PermitVis_DupeEquipment equipmentVis;
}
