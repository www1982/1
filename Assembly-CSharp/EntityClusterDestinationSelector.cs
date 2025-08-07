using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000B3D RID: 2877
public class EntityClusterDestinationSelector : ClusterDestinationSelector
{
	// Token: 0x17000629 RID: 1577
	// (get) Token: 0x0600558D RID: 21901 RVA: 0x001F15A1 File Offset: 0x001EF7A1
	private ClusterGridEntity DestinationEntity
	{
		get
		{
			if (this.m_DestinationEntity == null)
			{
				return null;
			}
			return this.m_DestinationEntity.Get();
		}
	}

	// Token: 0x0600558E RID: 21902 RVA: 0x001F15B8 File Offset: 0x001EF7B8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		global::Debug.Assert(this.requiredEntityLayer != EntityLayer.None, "EnityClusterDestinationSelector must specify an EntityLayer");
	}

	// Token: 0x0600558F RID: 21903 RVA: 0x001F15D6 File Offset: 0x001EF7D6
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
	}

	// Token: 0x06005590 RID: 21904 RVA: 0x001F15F8 File Offset: 0x001EF7F8
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject != null)
		{
			EntityClusterDestinationSelector component = gameObject.GetComponent<EntityClusterDestinationSelector>();
			if (component != null && component.DestinationEntity != null)
			{
				this.m_DestinationEntity = new Ref<ClusterGridEntity>(component.DestinationEntity);
				this.SetDestination(this.m_DestinationEntity.Get().Location);
			}
		}
	}

	// Token: 0x06005591 RID: 21905 RVA: 0x001F165A File Offset: 0x001EF85A
	public override ClusterGridEntity GetClusterEntityTarget()
	{
		return this.DestinationEntity;
	}

	// Token: 0x06005592 RID: 21906 RVA: 0x001F1662 File Offset: 0x001EF862
	public override AxialI GetDestination()
	{
		if (this.DestinationEntity != null)
		{
			return this.DestinationEntity.Location;
		}
		return base.GetDestination();
	}

	// Token: 0x06005593 RID: 21907 RVA: 0x001F1684 File Offset: 0x001EF884
	public override void SetDestination(AxialI location)
	{
		ClusterGridEntity visibleEntityOfLayerAtCell = ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(location, this.requiredEntityLayer);
		this.m_DestinationEntity.Set(visibleEntityOfLayerAtCell);
		base.SetDestination(location);
	}

	// Token: 0x0400392A RID: 14634
	[Serialize]
	protected Ref<ClusterGridEntity> m_DestinationEntity = new Ref<ClusterGridEntity>();
}
