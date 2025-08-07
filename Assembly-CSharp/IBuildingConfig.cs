using System;
using UnityEngine;

// Token: 0x020006E4 RID: 1764
public abstract class IBuildingConfig : IHasDlcRestrictions
{
	// Token: 0x06002BC8 RID: 11208
	public abstract BuildingDef CreateBuildingDef();

	// Token: 0x06002BC9 RID: 11209 RVA: 0x000FC3C5 File Offset: 0x000FA5C5
	public virtual void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
	}

	// Token: 0x06002BCA RID: 11210
	public abstract void DoPostConfigureComplete(GameObject go);

	// Token: 0x06002BCB RID: 11211 RVA: 0x000FC3C7 File Offset: 0x000FA5C7
	public virtual void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06002BCC RID: 11212 RVA: 0x000FC3C9 File Offset: 0x000FA5C9
	public virtual void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06002BCD RID: 11213 RVA: 0x000FC3CB File Offset: 0x000FA5CB
	public virtual void ConfigurePost(BuildingDef def)
	{
	}

	// Token: 0x06002BCE RID: 11214 RVA: 0x000FC3CD File Offset: 0x000FA5CD
	[Obsolete("Implement GetRequiredDlcIds and/or GetForbiddenDlcIds instead")]
	public virtual string[] GetDlcIds()
	{
		return null;
	}

	// Token: 0x06002BCF RID: 11215 RVA: 0x000FC3D0 File Offset: 0x000FA5D0
	public virtual string[] GetRequiredDlcIds()
	{
		return null;
	}

	// Token: 0x06002BD0 RID: 11216 RVA: 0x000FC3D3 File Offset: 0x000FA5D3
	public virtual string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06002BD1 RID: 11217 RVA: 0x000FC3D6 File Offset: 0x000FA5D6
	public virtual bool ForbidFromLoading()
	{
		return false;
	}
}
