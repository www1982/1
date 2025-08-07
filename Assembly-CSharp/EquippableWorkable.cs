using System;
using KSerialization;
using UnityEngine;

// Token: 0x020008FE RID: 2302
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/EquippableWorkable")]
public class EquippableWorkable : Workable, ISaveLoadable
{
	// Token: 0x06004038 RID: 16440 RVA: 0x00168690 File Offset: 0x00166890
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Equipping;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_equip_clothing_kanim") };
		this.synchronizeAnims = false;
	}

	// Token: 0x06004039 RID: 16441 RVA: 0x001686DD File Offset: 0x001668DD
	public global::QualityLevel GetQuality()
	{
		return this.quality;
	}

	// Token: 0x0600403A RID: 16442 RVA: 0x001686E5 File Offset: 0x001668E5
	public void SetQuality(global::QualityLevel level)
	{
		this.quality = level;
	}

	// Token: 0x0600403B RID: 16443 RVA: 0x001686EE File Offset: 0x001668EE
	protected override void OnSpawn()
	{
		base.SetWorkTime(1.5f);
		this.equippable.OnAssign += this.RefreshChore;
	}

	// Token: 0x0600403C RID: 16444 RVA: 0x00168714 File Offset: 0x00166914
	private void CreateChore()
	{
		global::Debug.Assert(this.chore == null, "chore should be null");
		this.chore = new EquipChore(this);
		Chore chore = this.chore;
		chore.onExit = (Action<Chore>)Delegate.Combine(chore.onExit, new Action<Chore>(this.OnChoreExit));
	}

	// Token: 0x0600403D RID: 16445 RVA: 0x00168767 File Offset: 0x00166967
	private void OnChoreExit(Chore chore)
	{
		if (!chore.isComplete)
		{
			this.RefreshChore(this.currentTarget);
		}
	}

	// Token: 0x0600403E RID: 16446 RVA: 0x0016877D File Offset: 0x0016697D
	public void CancelChore(string reason = "")
	{
		if (this.chore != null)
		{
			this.chore.Cancel(reason);
			Prioritizable.RemoveRef(this.equippable.gameObject);
			this.chore = null;
		}
	}

	// Token: 0x0600403F RID: 16447 RVA: 0x001687AA File Offset: 0x001669AA
	private void RefreshChore(IAssignableIdentity target)
	{
		if (this.chore != null)
		{
			this.CancelChore("Equipment Reassigned");
		}
		this.currentTarget = target;
		if (target != null && !target.GetSoleOwner().GetComponent<Equipment>().IsEquipped(this.equippable))
		{
			this.CreateChore();
		}
	}

	// Token: 0x06004040 RID: 16448 RVA: 0x001687E8 File Offset: 0x001669E8
	protected override void OnCompleteWork(WorkerBase worker)
	{
		if (this.equippable.assignee != null)
		{
			Ownables soleOwner = this.equippable.assignee.GetSoleOwner();
			if (soleOwner)
			{
				soleOwner.GetComponent<Equipment>().Equip(this.equippable);
				Prioritizable.RemoveRef(this.equippable.gameObject);
				this.chore = null;
			}
		}
	}

	// Token: 0x06004041 RID: 16449 RVA: 0x00168843 File Offset: 0x00166A43
	protected override void OnStopWork(WorkerBase worker)
	{
		this.workTimeRemaining = this.GetWorkTime();
		base.OnStopWork(worker);
	}

	// Token: 0x040027DB RID: 10203
	[MyCmpReq]
	private Equippable equippable;

	// Token: 0x040027DC RID: 10204
	private Chore chore;

	// Token: 0x040027DD RID: 10205
	private IAssignableIdentity currentTarget;

	// Token: 0x040027DE RID: 10206
	private global::QualityLevel quality;
}
