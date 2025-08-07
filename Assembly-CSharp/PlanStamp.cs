using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D99 RID: 3481
[AddComponentMenu("KMonoBehaviour/scripts/PlanStamp")]
public class PlanStamp : KMonoBehaviour
{
	// Token: 0x06006D05 RID: 27909 RVA: 0x002945F8 File Offset: 0x002927F8
	public void SetStamp(Sprite sprite, string Text)
	{
		this.StampImage.sprite = sprite;
		this.StampText.text = Text.ToUpper();
	}

	// Token: 0x04004A5A RID: 19034
	public PlanStamp.StampArt stampSprites;

	// Token: 0x04004A5B RID: 19035
	[SerializeField]
	private Image StampImage;

	// Token: 0x04004A5C RID: 19036
	[SerializeField]
	private Text StampText;

	// Token: 0x02001FA1 RID: 8097
	[Serializable]
	public struct StampArt
	{
		// Token: 0x0400918F RID: 37263
		public Sprite UnderConstruction;

		// Token: 0x04009190 RID: 37264
		public Sprite NeedsResearch;

		// Token: 0x04009191 RID: 37265
		public Sprite SelectResource;

		// Token: 0x04009192 RID: 37266
		public Sprite NeedsRepair;

		// Token: 0x04009193 RID: 37267
		public Sprite NeedsPower;

		// Token: 0x04009194 RID: 37268
		public Sprite NeedsResource;

		// Token: 0x04009195 RID: 37269
		public Sprite NeedsGasPipe;

		// Token: 0x04009196 RID: 37270
		public Sprite NeedsLiquidPipe;
	}
}
