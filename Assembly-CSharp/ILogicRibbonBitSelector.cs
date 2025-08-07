using System;

// Token: 0x02000E06 RID: 3590
public interface ILogicRibbonBitSelector
{
	// Token: 0x0600714A RID: 29002
	void SetBitSelection(int bit);

	// Token: 0x0600714B RID: 29003
	int GetBitSelection();

	// Token: 0x0600714C RID: 29004
	int GetBitDepth();

	// Token: 0x170007C6 RID: 1990
	// (get) Token: 0x0600714D RID: 29005
	string SideScreenTitle { get; }

	// Token: 0x170007C7 RID: 1991
	// (get) Token: 0x0600714E RID: 29006
	string SideScreenDescription { get; }

	// Token: 0x0600714F RID: 29007
	bool SideScreenDisplayWriterDescription();

	// Token: 0x06007150 RID: 29008
	bool SideScreenDisplayReaderDescription();

	// Token: 0x06007151 RID: 29009
	bool IsBitActive(int bit);

	// Token: 0x06007152 RID: 29010
	int GetOutputValue();

	// Token: 0x06007153 RID: 29011
	int GetInputValue();

	// Token: 0x06007154 RID: 29012
	void UpdateVisuals();
}
