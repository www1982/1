using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200064A RID: 1610
[AddComponentMenu("KMonoBehaviour/Workable/Unsealable")]
public class Unsealable : Workable
{
	// Token: 0x060026EA RID: 9962 RVA: 0x000DE998 File Offset: 0x000DCB98
	private Unsealable()
	{
	}

	// Token: 0x060026EB RID: 9963 RVA: 0x000DE9A0 File Offset: 0x000DCBA0
	public override CellOffset[] GetOffsets(int cell)
	{
		if (this.facingRight)
		{
			return OffsetGroups.RightOnly;
		}
		return OffsetGroups.LeftOnly;
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x000DE9B5 File Offset: 0x000DCBB5
	protected override void OnPrefabInit()
	{
		this.faceTargetWhenWorking = true;
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_door_poi_kanim") };
	}

	// Token: 0x060026ED RID: 9965 RVA: 0x000DE9E4 File Offset: 0x000DCBE4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(3f);
		if (this.unsealed)
		{
			Deconstructable component = base.GetComponent<Deconstructable>();
			if (component != null)
			{
				component.allowDeconstruction = true;
			}
		}
	}

	// Token: 0x060026EE RID: 9966 RVA: 0x000DEA21 File Offset: 0x000DCC21
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
	}

	// Token: 0x060026EF RID: 9967 RVA: 0x000DEA2C File Offset: 0x000DCC2C
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.unsealed = true;
		base.OnCompleteWork(worker);
		Deconstructable component = base.GetComponent<Deconstructable>();
		if (component != null)
		{
			component.allowDeconstruction = true;
			Game.Instance.Trigger(1980521255, base.gameObject);
		}
	}

	// Token: 0x040016B7 RID: 5815
	[Serialize]
	public bool facingRight;

	// Token: 0x040016B8 RID: 5816
	[Serialize]
	public bool unsealed;
}
