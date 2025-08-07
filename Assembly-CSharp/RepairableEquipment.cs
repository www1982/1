using System;
using KSerialization;

// Token: 0x02000ABC RID: 2748
public class RepairableEquipment : KMonoBehaviour
{
	// Token: 0x17000589 RID: 1417
	// (get) Token: 0x06004FC0 RID: 20416 RVA: 0x001CDBBA File Offset: 0x001CBDBA
	// (set) Token: 0x06004FC1 RID: 20417 RVA: 0x001CDBC7 File Offset: 0x001CBDC7
	public EquipmentDef def
	{
		get
		{
			return this.defHandle.Get<EquipmentDef>();
		}
		set
		{
			this.defHandle.Set<EquipmentDef>(value);
		}
	}

	// Token: 0x06004FC2 RID: 20418 RVA: 0x001CDBD8 File Offset: 0x001CBDD8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.def.AdditionalTags != null)
		{
			foreach (Tag tag in this.def.AdditionalTags)
			{
				base.GetComponent<KPrefabID>().AddTag(tag, false);
			}
		}
	}

	// Token: 0x06004FC3 RID: 20419 RVA: 0x001CDC28 File Offset: 0x001CBE28
	protected override void OnSpawn()
	{
		if (!this.facadeID.IsNullOrWhiteSpace())
		{
			KAnim.Build.Symbol symbol = Db.GetEquippableFacades().Get(this.facadeID).AnimFile.GetData().build.GetSymbol("object");
			SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
			component.TryRemoveSymbolOverride("object", 0);
			component.AddSymbolOverride("object", symbol, 0);
		}
	}

	// Token: 0x040035B5 RID: 13749
	public DefHandle defHandle;

	// Token: 0x040035B6 RID: 13750
	[Serialize]
	public string facadeID;
}
