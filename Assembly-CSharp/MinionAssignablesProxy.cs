using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020006B0 RID: 1712
[AddComponentMenu("KMonoBehaviour/scripts/MinionAssignablesProxy")]
public class MinionAssignablesProxy : KMonoBehaviour, IAssignableIdentity
{
	// Token: 0x170001FB RID: 507
	// (get) Token: 0x060029FA RID: 10746 RVA: 0x000F385F File Offset: 0x000F1A5F
	// (set) Token: 0x060029FB RID: 10747 RVA: 0x000F3867 File Offset: 0x000F1A67
	public IAssignableIdentity target { get; private set; }

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x060029FC RID: 10748 RVA: 0x000F3870 File Offset: 0x000F1A70
	public bool IsConfigured
	{
		get
		{
			return this.slotsConfigured;
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x060029FD RID: 10749 RVA: 0x000F3878 File Offset: 0x000F1A78
	public int TargetInstanceID
	{
		get
		{
			return this.target_instance_id;
		}
	}

	// Token: 0x060029FE RID: 10750 RVA: 0x000F3880 File Offset: 0x000F1A80
	public GameObject GetTargetGameObject()
	{
		if (this.target == null && this.target_instance_id != -1)
		{
			this.RestoreTargetFromInstanceID();
		}
		KMonoBehaviour kmonoBehaviour = (KMonoBehaviour)this.target;
		if (kmonoBehaviour != null)
		{
			return kmonoBehaviour.gameObject;
		}
		return null;
	}

	// Token: 0x060029FF RID: 10751 RVA: 0x000F38C4 File Offset: 0x000F1AC4
	public float GetArrivalTime()
	{
		if (this.GetTargetGameObject().GetComponent<MinionIdentity>() != null)
		{
			return this.GetTargetGameObject().GetComponent<MinionIdentity>().arrivalTime;
		}
		if (this.GetTargetGameObject().GetComponent<StoredMinionIdentity>() != null)
		{
			return this.GetTargetGameObject().GetComponent<StoredMinionIdentity>().arrivalTime;
		}
		global::Debug.LogError("Could not get minion arrival time");
		return -1f;
	}

	// Token: 0x06002A00 RID: 10752 RVA: 0x000F3928 File Offset: 0x000F1B28
	public int GetTotalSkillpoints()
	{
		if (this.GetTargetGameObject().GetComponent<MinionIdentity>() != null)
		{
			return this.GetTargetGameObject().GetComponent<MinionResume>().TotalSkillPointsGained;
		}
		if (this.GetTargetGameObject().GetComponent<StoredMinionIdentity>() != null)
		{
			return MinionResume.CalculateTotalSkillPointsGained(this.GetTargetGameObject().GetComponent<StoredMinionIdentity>().TotalExperienceGained);
		}
		global::Debug.LogError("Could not get minion skill points time");
		return -1;
	}

	// Token: 0x06002A01 RID: 10753 RVA: 0x000F3990 File Offset: 0x000F1B90
	public Tag GetMinionModel()
	{
		MinionIdentity component = this.GetTargetGameObject().GetComponent<MinionIdentity>();
		if (component != null)
		{
			return component.model;
		}
		StoredMinionIdentity component2 = this.GetTargetGameObject().GetComponent<StoredMinionIdentity>();
		if (component2 != null)
		{
			return component2.model;
		}
		global::Debug.LogError("Could not get minion model");
		return Tag.Invalid;
	}

	// Token: 0x06002A02 RID: 10754 RVA: 0x000F39E4 File Offset: 0x000F1BE4
	public void SetTarget(IAssignableIdentity target, GameObject targetGO)
	{
		global::Debug.Assert(target != null, "target was null");
		if (targetGO == null)
		{
			global::Debug.LogWarningFormat("{0} MinionAssignablesProxy.SetTarget {1}, {2}, {3}. DESTROYING", new object[]
			{
				base.GetInstanceID(),
				this.target_instance_id,
				target,
				targetGO
			});
			Util.KDestroyGameObject(base.gameObject);
		}
		this.target = target;
		this.target_instance_id = targetGO.GetComponent<KPrefabID>().InstanceID;
		base.gameObject.name = "Minion Assignables Proxy : " + targetGO.name;
	}

	// Token: 0x06002A03 RID: 10755 RVA: 0x000F3A7C File Offset: 0x000F1C7C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.ownables = new List<Ownables> { base.gameObject.AddOrGet<Ownables>() };
		Components.MinionAssignablesProxy.Add(this);
		base.Subscribe<MinionAssignablesProxy>(1502190696, MinionAssignablesProxy.OnQueueDestroyObjectDelegate);
		this.ConfigureAssignableSlots();
	}

	// Token: 0x06002A04 RID: 10756 RVA: 0x000F3ACD File Offset: 0x000F1CCD
	[OnDeserialized]
	private void OnDeserialized()
	{
	}

	// Token: 0x06002A05 RID: 10757 RVA: 0x000F3AD0 File Offset: 0x000F1CD0
	public void ConfigureAssignableSlots()
	{
		if (this.slotsConfigured)
		{
			return;
		}
		Ownables component = base.GetComponent<Ownables>();
		Equipment component2 = base.GetComponent<Equipment>();
		if (component2 != null)
		{
			foreach (AssignableSlot assignableSlot in Db.Get().AssignableSlots.resources)
			{
				if (assignableSlot is OwnableSlot)
				{
					OwnableSlotInstance ownableSlotInstance = new OwnableSlotInstance(component, (OwnableSlot)assignableSlot);
					component.Add(ownableSlotInstance);
				}
				else if (assignableSlot is EquipmentSlot)
				{
					EquipmentSlotInstance equipmentSlotInstance = new EquipmentSlotInstance(component2, (EquipmentSlot)assignableSlot);
					component2.Add(equipmentSlotInstance);
				}
			}
			BionicUpgradesMonitor.CreateAssignableSlots(this);
		}
		this.slotsConfigured = true;
	}

	// Token: 0x06002A06 RID: 10758 RVA: 0x000F3B90 File Offset: 0x000F1D90
	public void RestoreTargetFromInstanceID()
	{
		if (this.target_instance_id != -1 && this.target == null)
		{
			KPrefabID instance = KPrefabIDTracker.Get().GetInstance(this.target_instance_id);
			if (instance)
			{
				IAssignableIdentity component = instance.GetComponent<IAssignableIdentity>();
				if (component != null)
				{
					this.SetTarget(component, instance.gameObject);
					return;
				}
				global::Debug.LogWarningFormat("RestoreTargetFromInstanceID target ID {0} was found but it wasn't an IAssignableIdentity, destroying proxy object.", new object[] { this.target_instance_id });
				Util.KDestroyGameObject(base.gameObject);
				return;
			}
			else
			{
				global::Debug.LogWarningFormat("RestoreTargetFromInstanceID target ID {0} was not found, destroying proxy object.", new object[] { this.target_instance_id });
				Util.KDestroyGameObject(base.gameObject);
			}
		}
	}

	// Token: 0x06002A07 RID: 10759 RVA: 0x000F3C38 File Offset: 0x000F1E38
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RestoreTargetFromInstanceID();
		if (this.target != null)
		{
			base.Subscribe<MinionAssignablesProxy>(-1585839766, MinionAssignablesProxy.OnAssignablesChangedDelegate);
			Game.Instance.assignmentManager.AddToAssignmentGroup("public", this);
		}
	}

	// Token: 0x06002A08 RID: 10760 RVA: 0x000F3C74 File Offset: 0x000F1E74
	private void OnQueueDestroyObject(object data)
	{
		Components.MinionAssignablesProxy.Remove(this);
	}

	// Token: 0x06002A09 RID: 10761 RVA: 0x000F3C81 File Offset: 0x000F1E81
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Game.Instance.assignmentManager.RemoveFromAllGroups(this);
		base.GetComponent<Ownables>().UnassignAll();
		base.GetComponent<Equipment>().UnequipAll();
	}

	// Token: 0x06002A0A RID: 10762 RVA: 0x000F3CAF File Offset: 0x000F1EAF
	private void OnAssignablesChanged(object data)
	{
		if (!this.target.IsNull())
		{
			((KMonoBehaviour)this.target).Trigger(-1585839766, data);
		}
	}

	// Token: 0x06002A0B RID: 10763 RVA: 0x000F3CD4 File Offset: 0x000F1ED4
	private void CheckTarget()
	{
		if (this.target == null)
		{
			KPrefabID instance = KPrefabIDTracker.Get().GetInstance(this.target_instance_id);
			if (instance != null)
			{
				this.target = instance.GetComponent<IAssignableIdentity>();
				if (this.target != null)
				{
					MinionIdentity minionIdentity = this.target as MinionIdentity;
					if (minionIdentity)
					{
						minionIdentity.ValidateProxy();
						return;
					}
					StoredMinionIdentity storedMinionIdentity = this.target as StoredMinionIdentity;
					if (storedMinionIdentity)
					{
						storedMinionIdentity.ValidateProxy();
					}
				}
			}
		}
	}

	// Token: 0x06002A0C RID: 10764 RVA: 0x000F3D4C File Offset: 0x000F1F4C
	public List<Ownables> GetOwners()
	{
		this.CheckTarget();
		return this.target.GetOwners();
	}

	// Token: 0x06002A0D RID: 10765 RVA: 0x000F3D5F File Offset: 0x000F1F5F
	public string GetProperName()
	{
		this.CheckTarget();
		return this.target.GetProperName();
	}

	// Token: 0x06002A0E RID: 10766 RVA: 0x000F3D72 File Offset: 0x000F1F72
	public Ownables GetSoleOwner()
	{
		this.CheckTarget();
		return this.target.GetSoleOwner();
	}

	// Token: 0x06002A0F RID: 10767 RVA: 0x000F3D85 File Offset: 0x000F1F85
	public bool HasOwner(Assignables owner)
	{
		this.CheckTarget();
		return this.target.HasOwner(owner);
	}

	// Token: 0x06002A10 RID: 10768 RVA: 0x000F3D99 File Offset: 0x000F1F99
	public int NumOwners()
	{
		this.CheckTarget();
		return this.target.NumOwners();
	}

	// Token: 0x06002A11 RID: 10769 RVA: 0x000F3DAC File Offset: 0x000F1FAC
	public bool IsNull()
	{
		this.CheckTarget();
		return this.target.IsNull();
	}

	// Token: 0x06002A12 RID: 10770 RVA: 0x000F3DC0 File Offset: 0x000F1FC0
	public static Ref<MinionAssignablesProxy> InitAssignableProxy(Ref<MinionAssignablesProxy> assignableProxyRef, IAssignableIdentity source)
	{
		if (assignableProxyRef == null)
		{
			assignableProxyRef = new Ref<MinionAssignablesProxy>();
		}
		GameObject gameObject = ((KMonoBehaviour)source).gameObject;
		MinionAssignablesProxy minionAssignablesProxy = assignableProxyRef.Get();
		if (minionAssignablesProxy == null)
		{
			GameObject gameObject2 = GameUtil.KInstantiate(Assets.GetPrefab(MinionAssignablesProxyConfig.ID), Grid.SceneLayer.NoLayer, null, 0);
			minionAssignablesProxy = gameObject2.GetComponent<MinionAssignablesProxy>();
			minionAssignablesProxy.SetTarget(source, gameObject);
			gameObject2.SetActive(true);
			assignableProxyRef.Set(minionAssignablesProxy);
		}
		else
		{
			minionAssignablesProxy.SetTarget(source, gameObject);
		}
		return assignableProxyRef;
	}

	// Token: 0x040018EB RID: 6379
	public List<Ownables> ownables;

	// Token: 0x040018ED RID: 6381
	[Serialize]
	private int target_instance_id = -1;

	// Token: 0x040018EE RID: 6382
	private bool slotsConfigured;

	// Token: 0x040018EF RID: 6383
	private static readonly EventSystem.IntraObjectHandler<MinionAssignablesProxy> OnAssignablesChangedDelegate = new EventSystem.IntraObjectHandler<MinionAssignablesProxy>(delegate(MinionAssignablesProxy component, object data)
	{
		component.OnAssignablesChanged(data);
	});

	// Token: 0x040018F0 RID: 6384
	private static readonly EventSystem.IntraObjectHandler<MinionAssignablesProxy> OnQueueDestroyObjectDelegate = new EventSystem.IntraObjectHandler<MinionAssignablesProxy>(delegate(MinionAssignablesProxy component, object data)
	{
		component.OnQueueDestroyObject(data);
	});
}
