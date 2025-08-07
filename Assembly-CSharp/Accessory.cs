using System;

// Token: 0x02000652 RID: 1618
public class Accessory : Resource
{
	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x0600277E RID: 10110 RVA: 0x000E1770 File Offset: 0x000DF970
	// (set) Token: 0x0600277F RID: 10111 RVA: 0x000E1778 File Offset: 0x000DF978
	public KAnim.Build.Symbol symbol { get; private set; }

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06002780 RID: 10112 RVA: 0x000E1781 File Offset: 0x000DF981
	// (set) Token: 0x06002781 RID: 10113 RVA: 0x000E1789 File Offset: 0x000DF989
	public HashedString batchSource { get; private set; }

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06002782 RID: 10114 RVA: 0x000E1792 File Offset: 0x000DF992
	// (set) Token: 0x06002783 RID: 10115 RVA: 0x000E179A File Offset: 0x000DF99A
	public AccessorySlot slot { get; private set; }

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06002784 RID: 10116 RVA: 0x000E17A3 File Offset: 0x000DF9A3
	// (set) Token: 0x06002785 RID: 10117 RVA: 0x000E17AB File Offset: 0x000DF9AB
	public KAnimFile animFile { get; private set; }

	// Token: 0x06002786 RID: 10118 RVA: 0x000E17B4 File Offset: 0x000DF9B4
	public Accessory(string id, ResourceSet parent, AccessorySlot slot, HashedString batchSource, KAnim.Build.Symbol symbol, KAnimFile animFile = null, KAnimFile defaultAnimFile = null)
		: base(id, parent, null)
	{
		this.slot = slot;
		this.symbol = symbol;
		this.batchSource = batchSource;
		this.animFile = animFile;
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x000E17DE File Offset: 0x000DF9DE
	public bool IsDefault()
	{
		return this.animFile == this.slot.defaultAnimFile;
	}
}
