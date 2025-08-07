using System;
using UnityEngine;

// Token: 0x02000DA9 RID: 3497
[AddComponentMenu("KMonoBehaviour/scripts/ReportScreenHeader")]
public class ReportScreenHeader : KMonoBehaviour
{
	// Token: 0x06006DA1 RID: 28065 RVA: 0x00297F8E File Offset: 0x0029618E
	public void SetMainEntry(ReportManager.ReportGroup reportGroup)
	{
		if (this.mainRow == null)
		{
			this.mainRow = Util.KInstantiateUI(this.rowTemplate.gameObject, base.gameObject, true).GetComponent<ReportScreenHeaderRow>();
		}
		this.mainRow.SetLine(reportGroup);
	}

	// Token: 0x04004AF2 RID: 19186
	[SerializeField]
	private ReportScreenHeaderRow rowTemplate;

	// Token: 0x04004AF3 RID: 19187
	private ReportScreenHeaderRow mainRow;
}
