using System;
using KSerialization;

// Token: 0x020008FD RID: 2301
public class EquippableFacade : KMonoBehaviour
{
	// Token: 0x06004030 RID: 16432 RVA: 0x00168590 File Offset: 0x00166790
	public static void AddFacadeToEquippable(Equippable equippable, string facadeID)
	{
		EquippableFacade equippableFacade = equippable.gameObject.AddOrGet<EquippableFacade>();
		equippableFacade.FacadeID = facadeID;
		equippableFacade.BuildOverride = Db.GetEquippableFacades().Get(facadeID).BuildOverride;
		equippableFacade.ApplyAnimOverride();
	}

	// Token: 0x06004031 RID: 16433 RVA: 0x001685BF File Offset: 0x001667BF
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.OverrideName();
		this.ApplyAnimOverride();
	}

	// Token: 0x1700049A RID: 1178
	// (get) Token: 0x06004032 RID: 16434 RVA: 0x001685D3 File Offset: 0x001667D3
	// (set) Token: 0x06004033 RID: 16435 RVA: 0x001685DB File Offset: 0x001667DB
	public string FacadeID
	{
		get
		{
			return this._facadeID;
		}
		private set
		{
			this._facadeID = value;
			this.OverrideName();
		}
	}

	// Token: 0x06004034 RID: 16436 RVA: 0x001685EA File Offset: 0x001667EA
	public void ApplyAnimOverride()
	{
		if (this.FacadeID.IsNullOrWhiteSpace())
		{
			return;
		}
		base.GetComponent<KBatchedAnimController>().SwapAnims(new KAnimFile[] { Db.GetEquippableFacades().Get(this.FacadeID).AnimFile });
	}

	// Token: 0x06004035 RID: 16437 RVA: 0x00168623 File Offset: 0x00166823
	private void OverrideName()
	{
		base.GetComponent<KSelectable>().SetName(EquippableFacade.GetNameOverride(base.GetComponent<Equippable>().def.Id, this.FacadeID));
	}

	// Token: 0x06004036 RID: 16438 RVA: 0x0016864B File Offset: 0x0016684B
	public static string GetNameOverride(string defID, string facadeID)
	{
		if (facadeID.IsNullOrWhiteSpace())
		{
			return Strings.Get("STRINGS.EQUIPMENT.PREFABS." + defID.ToUpper() + ".NAME");
		}
		return Db.GetEquippableFacades().Get(facadeID).Name;
	}

	// Token: 0x040027D9 RID: 10201
	[Serialize]
	private string _facadeID;

	// Token: 0x040027DA RID: 10202
	[Serialize]
	public string BuildOverride;
}
