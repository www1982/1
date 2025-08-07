using System;
using System.Collections.Generic;

// Token: 0x02000653 RID: 1619
public class AccessorySlot : Resource
{
	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06002788 RID: 10120 RVA: 0x000E17F6 File Offset: 0x000DF9F6
	// (set) Token: 0x06002789 RID: 10121 RVA: 0x000E17FE File Offset: 0x000DF9FE
	public KAnimHashedString targetSymbolId { get; private set; }

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x0600278A RID: 10122 RVA: 0x000E1807 File Offset: 0x000DFA07
	// (set) Token: 0x0600278B RID: 10123 RVA: 0x000E180F File Offset: 0x000DFA0F
	public List<Accessory> accessories { get; private set; }

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x0600278C RID: 10124 RVA: 0x000E1818 File Offset: 0x000DFA18
	public KAnimFile AnimFile
	{
		get
		{
			return this.file;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x0600278D RID: 10125 RVA: 0x000E1820 File Offset: 0x000DFA20
	// (set) Token: 0x0600278E RID: 10126 RVA: 0x000E1828 File Offset: 0x000DFA28
	public KAnimFile defaultAnimFile { get; private set; }

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x0600278F RID: 10127 RVA: 0x000E1831 File Offset: 0x000DFA31
	// (set) Token: 0x06002790 RID: 10128 RVA: 0x000E1839 File Offset: 0x000DFA39
	public int overrideLayer { get; private set; }

	// Token: 0x06002791 RID: 10129 RVA: 0x000E1844 File Offset: 0x000DFA44
	public AccessorySlot(string id, ResourceSet parent, KAnimFile swap_build, int overrideLayer = 0)
		: base(id, parent, null)
	{
		if (swap_build == null)
		{
			Debug.LogErrorFormat("AccessorySlot {0} missing swap_build", new object[] { id });
		}
		this.targetSymbolId = new KAnimHashedString("snapTo_" + id.ToLower());
		this.accessories = new List<Accessory>();
		this.file = swap_build;
		this.overrideLayer = overrideLayer;
		this.defaultAnimFile = swap_build;
	}

	// Token: 0x06002792 RID: 10130 RVA: 0x000E18B4 File Offset: 0x000DFAB4
	public AccessorySlot(string id, ResourceSet parent, KAnimHashedString target_symbol_id, KAnimFile swap_build, KAnimFile defaultAnimFile = null, int overrideLayer = 0)
		: base(id, parent, null)
	{
		if (swap_build == null)
		{
			Debug.LogErrorFormat("AccessorySlot {0} missing swap_build", new object[] { id });
		}
		this.targetSymbolId = target_symbol_id;
		this.accessories = new List<Accessory>();
		this.file = swap_build;
		this.defaultAnimFile = ((defaultAnimFile != null) ? defaultAnimFile : swap_build);
		this.overrideLayer = overrideLayer;
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x000E1920 File Offset: 0x000DFB20
	public void AddAccessories(KAnimFile default_build, ResourceSet parent)
	{
		KAnim.Build build = default_build.GetData().build;
		default_build.GetData().build.GetSymbol(this.targetSymbolId);
		string text = this.Id.ToLower();
		for (int i = 0; i < build.symbols.Length; i++)
		{
			string text2 = HashCache.Get().Get(build.symbols[i].hash);
			if (text2.StartsWith(text))
			{
				Accessory accessory = new Accessory(text2, parent, this, this.file.batchTag, build.symbols[i], default_build, null);
				this.accessories.Add(accessory);
				HashCache.Get().Add(accessory.IdHash.HashValue, accessory.Id);
			}
		}
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x000E19D9 File Offset: 0x000DFBD9
	public Accessory Lookup(string id)
	{
		return this.Lookup(new HashedString(id));
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x000E19E8 File Offset: 0x000DFBE8
	public Accessory Lookup(HashedString full_id)
	{
		if (!full_id.IsValid)
		{
			return null;
		}
		return this.accessories.Find((Accessory a) => a.IdHash == full_id);
	}

	// Token: 0x04001737 RID: 5943
	private KAnimFile file;
}
