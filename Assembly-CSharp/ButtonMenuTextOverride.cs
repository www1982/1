using System;

// Token: 0x02000DDB RID: 3547
[Serializable]
public struct ButtonMenuTextOverride
{
	// Token: 0x170007B7 RID: 1975
	// (get) Token: 0x06006FFF RID: 28671 RVA: 0x002A9AC9 File Offset: 0x002A7CC9
	public bool IsValid
	{
		get
		{
			return !string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this.ToolTip);
		}
	}

	// Token: 0x170007B8 RID: 1976
	// (get) Token: 0x06007000 RID: 28672 RVA: 0x002A9AF2 File Offset: 0x002A7CF2
	public bool HasCancelText
	{
		get
		{
			return !string.IsNullOrEmpty(this.CancelText) && !string.IsNullOrEmpty(this.CancelToolTip);
		}
	}

	// Token: 0x04004D0E RID: 19726
	public LocString Text;

	// Token: 0x04004D0F RID: 19727
	public LocString CancelText;

	// Token: 0x04004D10 RID: 19728
	public LocString ToolTip;

	// Token: 0x04004D11 RID: 19729
	public LocString CancelToolTip;
}
