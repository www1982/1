using System;
using KSerialization;

// Token: 0x02000623 RID: 1571
[SerializationConfig(MemberSerialization.OptIn)]
public abstract class SubstanceSource : KMonoBehaviour
{
	// Token: 0x0600261A RID: 9754 RVA: 0x000D999E File Offset: 0x000D7B9E
	protected override void OnPrefabInit()
	{
		this.pickupable.SetWorkTime(SubstanceSource.MaxPickupTime);
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x000D99B0 File Offset: 0x000D7BB0
	protected override void OnSpawn()
	{
		this.pickupable.SetWorkTime(10f);
	}

	// Token: 0x0600261C RID: 9756
	protected abstract CellOffset[] GetOffsetGroup();

	// Token: 0x0600261D RID: 9757
	protected abstract IChunkManager GetChunkManager();

	// Token: 0x0600261E RID: 9758 RVA: 0x000D99C2 File Offset: 0x000D7BC2
	public SimHashes GetElementID()
	{
		return this.primaryElement.ElementID;
	}

	// Token: 0x0600261F RID: 9759 RVA: 0x000D99D0 File Offset: 0x000D7BD0
	public Tag GetElementTag()
	{
		Tag tag = Tag.Invalid;
		if (base.gameObject != null && this.primaryElement != null && this.primaryElement.Element != null)
		{
			tag = this.primaryElement.Element.tag;
		}
		return tag;
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x000D9A20 File Offset: 0x000D7C20
	public Tag GetMaterialCategoryTag()
	{
		Tag tag = Tag.Invalid;
		if (base.gameObject != null && this.primaryElement != null && this.primaryElement.Element != null)
		{
			tag = this.primaryElement.Element.GetMaterialCategoryTag();
		}
		return tag;
	}

	// Token: 0x04001668 RID: 5736
	private bool enableRefresh;

	// Token: 0x04001669 RID: 5737
	private static readonly float MaxPickupTime = 8f;

	// Token: 0x0400166A RID: 5738
	[MyCmpReq]
	public Pickupable pickupable;

	// Token: 0x0400166B RID: 5739
	[MyCmpReq]
	private PrimaryElement primaryElement;
}
