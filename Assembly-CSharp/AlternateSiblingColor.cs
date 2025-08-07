using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C4E RID: 3150
public class AlternateSiblingColor : KMonoBehaviour
{
	// Token: 0x0600606C RID: 24684 RVA: 0x0023ABFC File Offset: 0x00238DFC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int siblingIndex = base.transform.GetSiblingIndex();
		this.RefreshColor(siblingIndex % 2 == 0);
	}

	// Token: 0x0600606D RID: 24685 RVA: 0x0023AC27 File Offset: 0x00238E27
	private void RefreshColor(bool evenIndex)
	{
		if (this.image == null)
		{
			return;
		}
		this.image.color = (evenIndex ? this.evenColor : this.oddColor);
	}

	// Token: 0x0600606E RID: 24686 RVA: 0x0023AC54 File Offset: 0x00238E54
	private void Update()
	{
		if (this.mySiblingIndex != base.transform.GetSiblingIndex())
		{
			this.mySiblingIndex = base.transform.GetSiblingIndex();
			this.RefreshColor(this.mySiblingIndex % 2 == 0);
		}
	}

	// Token: 0x0400414B RID: 16715
	public Color evenColor;

	// Token: 0x0400414C RID: 16716
	public Color oddColor;

	// Token: 0x0400414D RID: 16717
	public Image image;

	// Token: 0x0400414E RID: 16718
	private int mySiblingIndex;
}
