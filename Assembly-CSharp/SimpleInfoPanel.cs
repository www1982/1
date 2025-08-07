using System;
using UnityEngine;

// Token: 0x02000CBF RID: 3263
public class SimpleInfoPanel
{
	// Token: 0x0600646E RID: 25710 RVA: 0x0025B81B File Offset: 0x00259A1B
	public SimpleInfoPanel(SimpleInfoScreen simpleInfoRoot)
	{
		this.simpleInfoRoot = simpleInfoRoot;
	}

	// Token: 0x0600646F RID: 25711 RVA: 0x0025B82A File Offset: 0x00259A2A
	public virtual void Refresh(CollapsibleDetailContentPanel panel, GameObject selectedTarget)
	{
	}

	// Token: 0x04004484 RID: 17540
	protected SimpleInfoScreen simpleInfoRoot;
}
