using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020007C2 RID: 1986
[AddComponentMenu("KMonoBehaviour/Workable/SingleEntityReceptacle")]
public class SingleEntityReceptacle : Workable, IRender1000ms
{
	// Token: 0x17000368 RID: 872
	// (get) Token: 0x060034FF RID: 13567 RVA: 0x00129187 File Offset: 0x00127387
	public FetchChore GetActiveRequest
	{
		get
		{
			return this.fetchChore;
		}
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x06003500 RID: 13568 RVA: 0x0012918F File Offset: 0x0012738F
	// (set) Token: 0x06003501 RID: 13569 RVA: 0x001291B6 File Offset: 0x001273B6
	protected GameObject occupyingObject
	{
		get
		{
			if (this.occupyObjectRef.Get() != null)
			{
				return this.occupyObjectRef.Get().gameObject;
			}
			return null;
		}
		set
		{
			if (value == null)
			{
				this.occupyObjectRef.Set(null);
				return;
			}
			this.occupyObjectRef.Set(value.GetComponent<KSelectable>());
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06003502 RID: 13570 RVA: 0x001291DF File Offset: 0x001273DF
	public GameObject Occupant
	{
		get
		{
			return this.occupyingObject;
		}
	}

	// Token: 0x1700036B RID: 875
	// (get) Token: 0x06003503 RID: 13571 RVA: 0x001291E7 File Offset: 0x001273E7
	public IReadOnlyList<Tag> possibleDepositObjectTags
	{
		get
		{
			return this.possibleDepositTagsList;
		}
	}

	// Token: 0x06003504 RID: 13572 RVA: 0x001291EF File Offset: 0x001273EF
	public bool HasDepositTag(Tag tag)
	{
		return this.possibleDepositTagsList.Contains(tag);
	}

	// Token: 0x06003505 RID: 13573 RVA: 0x00129200 File Offset: 0x00127400
	public bool IsValidEntity(GameObject candidate)
	{
		if (!Game.IsCorrectDlcActiveForCurrentSave(candidate.GetComponent<KPrefabID>()))
		{
			return false;
		}
		IReceptacleDirection component = candidate.GetComponent<IReceptacleDirection>();
		bool flag = this.rotatable != null || component == null || component.Direction == this.Direction;
		int num = 0;
		while (flag && num < this.additionalCriteria.Count)
		{
			flag = this.additionalCriteria[num](candidate);
			num++;
		}
		return flag;
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06003506 RID: 13574 RVA: 0x00129273 File Offset: 0x00127473
	public SingleEntityReceptacle.ReceptacleDirection Direction
	{
		get
		{
			return this.direction;
		}
	}

	// Token: 0x06003507 RID: 13575 RVA: 0x0012927B File Offset: 0x0012747B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06003508 RID: 13576 RVA: 0x00129284 File Offset: 0x00127484
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.occupyingObject != null)
		{
			this.PositionOccupyingObject();
			this.SubscribeToOccupant();
		}
		this.UpdateStatusItem();
		if (this.occupyingObject == null && !this.requestedEntityTag.IsValid)
		{
			this.requestedEntityAdditionalFilterTag = null;
		}
		if (this.occupyingObject == null && this.requestedEntityTag.IsValid)
		{
			this.CreateOrder(this.requestedEntityTag, this.requestedEntityAdditionalFilterTag);
		}
		base.Subscribe<SingleEntityReceptacle>(-592767678, SingleEntityReceptacle.OnOperationalChangedDelegate);
	}

	// Token: 0x06003509 RID: 13577 RVA: 0x0012931C File Offset: 0x0012751C
	public void AddDepositTag(Tag t)
	{
		this.possibleDepositTagsList.Add(t);
	}

	// Token: 0x0600350A RID: 13578 RVA: 0x0012932A File Offset: 0x0012752A
	public void AddAdditionalCriteria(Func<GameObject, bool> criteria)
	{
		this.additionalCriteria.Add(criteria);
	}

	// Token: 0x0600350B RID: 13579 RVA: 0x00129338 File Offset: 0x00127538
	public void SetReceptacleDirection(SingleEntityReceptacle.ReceptacleDirection d)
	{
		this.direction = d;
	}

	// Token: 0x0600350C RID: 13580 RVA: 0x00129341 File Offset: 0x00127541
	public virtual void SetPreview(Tag entityTag, bool solid = false)
	{
	}

	// Token: 0x0600350D RID: 13581 RVA: 0x00129343 File Offset: 0x00127543
	public virtual void CreateOrder(Tag entityTag, Tag additionalFilterTag)
	{
		this.requestedEntityTag = entityTag;
		this.requestedEntityAdditionalFilterTag = additionalFilterTag;
		this.CreateFetchChore(this.requestedEntityTag, this.requestedEntityAdditionalFilterTag);
		this.SetPreview(entityTag, true);
		this.UpdateStatusItem();
	}

	// Token: 0x0600350E RID: 13582 RVA: 0x00129373 File Offset: 0x00127573
	public void Render1000ms(float dt)
	{
		this.UpdateStatusItem();
	}

	// Token: 0x0600350F RID: 13583 RVA: 0x0012937C File Offset: 0x0012757C
	protected virtual void UpdateStatusItem()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.Occupant != null)
		{
			component.SetStatusItem(Db.Get().StatusItemCategories.EntityReceptacle, null, null);
			return;
		}
		if (this.fetchChore == null)
		{
			component.SetStatusItem(Db.Get().StatusItemCategories.EntityReceptacle, this.statusItemNeed, null);
			return;
		}
		bool flag = this.fetchChore.fetcher != null;
		WorldContainer myWorld = this.GetMyWorld();
		if (!flag && myWorld != null)
		{
			foreach (Tag tag in this.fetchChore.tags)
			{
				if (myWorld.worldInventory.GetTotalAmount(tag, true) > 0f)
				{
					if (myWorld.worldInventory.GetTotalAmount(this.requestedEntityAdditionalFilterTag, true) > 0f || this.requestedEntityAdditionalFilterTag == Tag.Invalid)
					{
						flag = true;
						break;
					}
					break;
				}
			}
		}
		if (flag)
		{
			component.SetStatusItem(Db.Get().StatusItemCategories.EntityReceptacle, this.statusItemAwaitingDelivery, null);
			return;
		}
		component.SetStatusItem(Db.Get().StatusItemCategories.EntityReceptacle, this.statusItemNoneAvailable, null);
	}

	// Token: 0x06003510 RID: 13584 RVA: 0x001294D0 File Offset: 0x001276D0
	protected void CreateFetchChore(Tag entityTag, Tag additionalRequiredTag)
	{
		if (this.fetchChore == null && entityTag.IsValid && entityTag != GameTags.Empty)
		{
			this.fetchChore = new FetchChore(this.choreType, this.storage, this.GetPrefabFetchMass(entityTag), new HashSet<Tag> { entityTag }, FetchChore.MatchCriteria.MatchID, (additionalRequiredTag.IsValid && additionalRequiredTag != GameTags.Empty) ? additionalRequiredTag : Tag.Invalid, null, null, true, new Action<Chore>(this.OnFetchComplete), delegate(Chore chore)
			{
				this.UpdateStatusItem();
			}, delegate(Chore chore)
			{
				this.UpdateStatusItem();
			}, Operational.State.Functional, 0);
			MaterialNeeds.UpdateNeed(this.requestedEntityTag, 1f, base.gameObject.GetMyWorldId());
			this.UpdateStatusItem();
		}
	}

	// Token: 0x06003511 RID: 13585 RVA: 0x00129598 File Offset: 0x00127798
	private float GetPrefabFetchMass(Tag entityTag)
	{
		GameObject prefab = Assets.GetPrefab(entityTag);
		if (prefab != null)
		{
			PrimaryElement component = prefab.GetComponent<PrimaryElement>();
			if (component != null)
			{
				return component.MassPerUnit;
			}
		}
		KCrashReporter.ReportDevNotification(string.Concat(new string[] { "SingleEntityReceptacle ", base.name, " is requesting ", entityTag.Name, " which is not an entity" }), Environment.StackTrace, "", false, null);
		return 1f;
	}

	// Token: 0x06003512 RID: 13586 RVA: 0x00129618 File Offset: 0x00127818
	public virtual void OrderRemoveOccupant()
	{
		this.ClearOccupant();
	}

	// Token: 0x06003513 RID: 13587 RVA: 0x00129620 File Offset: 0x00127820
	protected virtual void ClearOccupant()
	{
		if (this.occupyingObject)
		{
			this.UnsubscribeFromOccupant();
			this.storage.DropAll(false, false, default(Vector3), true, null);
		}
		this.occupyingObject = null;
		this.UpdateActive();
		this.UpdateStatusItem();
		base.Trigger(-731304873, this.occupyingObject);
	}

	// Token: 0x06003514 RID: 13588 RVA: 0x0012967C File Offset: 0x0012787C
	public void CancelActiveRequest()
	{
		if (this.fetchChore != null)
		{
			MaterialNeeds.UpdateNeed(this.requestedEntityTag, -1f, base.gameObject.GetMyWorldId());
			this.fetchChore.Cancel("User canceled");
			this.fetchChore = null;
		}
		this.requestedEntityTag = Tag.Invalid;
		this.requestedEntityAdditionalFilterTag = Tag.Invalid;
		this.UpdateStatusItem();
		this.SetPreview(Tag.Invalid, false);
	}

	// Token: 0x06003515 RID: 13589 RVA: 0x001296EC File Offset: 0x001278EC
	private void OnOccupantDestroyed(object data)
	{
		this.occupyingObject = null;
		this.ClearOccupant();
		if (this.autoReplaceEntity && this.requestedEntityTag.IsValid && this.requestedEntityTag != GameTags.Empty)
		{
			this.CreateOrder(this.requestedEntityTag, this.requestedEntityAdditionalFilterTag);
		}
	}

	// Token: 0x06003516 RID: 13590 RVA: 0x0012973F File Offset: 0x0012793F
	protected virtual void SubscribeToOccupant()
	{
		if (this.occupyingObject != null)
		{
			base.Subscribe(this.occupyingObject, 1969584890, new Action<object>(this.OnOccupantDestroyed));
		}
	}

	// Token: 0x06003517 RID: 13591 RVA: 0x0012976D File Offset: 0x0012796D
	protected virtual void UnsubscribeFromOccupant()
	{
		if (this.occupyingObject != null)
		{
			base.Unsubscribe(this.occupyingObject, 1969584890, new Action<object>(this.OnOccupantDestroyed));
		}
	}

	// Token: 0x06003518 RID: 13592 RVA: 0x0012979C File Offset: 0x0012799C
	private void OnFetchComplete(Chore chore)
	{
		if (this.fetchChore == null)
		{
			global::Debug.LogWarningFormat(base.gameObject, "{0} OnFetchComplete fetchChore null", new object[] { base.gameObject });
			return;
		}
		if (this.fetchChore.fetchTarget == null)
		{
			global::Debug.LogWarningFormat(base.gameObject, "{0} OnFetchComplete fetchChore.fetchTarget null", new object[] { base.gameObject });
			return;
		}
		this.OnDepositObject(this.fetchChore.fetchTarget.gameObject);
	}

	// Token: 0x06003519 RID: 13593 RVA: 0x0012981A File Offset: 0x00127A1A
	public void ForceDeposit(GameObject depositedObject)
	{
		if (this.occupyingObject != null)
		{
			this.ClearOccupant();
		}
		this.OnDepositObject(depositedObject);
	}

	// Token: 0x0600351A RID: 13594 RVA: 0x00129838 File Offset: 0x00127A38
	private void OnDepositObject(GameObject depositedObject)
	{
		this.SetPreview(Tag.Invalid, false);
		MaterialNeeds.UpdateNeed(this.requestedEntityTag, -1f, base.gameObject.GetMyWorldId());
		KBatchedAnimController component = depositedObject.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.GetBatchInstanceData().ClearOverrideTransformMatrix();
		}
		this.occupyingObject = this.SpawnOccupyingObject(depositedObject);
		if (this.occupyingObject != null)
		{
			this.ConfigureOccupyingObject(this.occupyingObject);
			this.occupyingObject.SetActive(true);
			this.PositionOccupyingObject();
			this.SubscribeToOccupant();
		}
		else
		{
			global::Debug.LogWarning(base.gameObject.name + " EntityReceptacle did not spawn occupying entity.");
		}
		if (this.fetchChore != null)
		{
			this.fetchChore.Cancel("receptacle filled");
			this.fetchChore = null;
		}
		if (!this.autoReplaceEntity)
		{
			this.requestedEntityTag = Tag.Invalid;
			this.requestedEntityAdditionalFilterTag = Tag.Invalid;
		}
		this.UpdateActive();
		this.UpdateStatusItem();
		if (this.destroyEntityOnDeposit)
		{
			Util.KDestroyGameObject(depositedObject);
		}
		base.Trigger(-731304873, this.occupyingObject);
	}

	// Token: 0x0600351B RID: 13595 RVA: 0x0012994A File Offset: 0x00127B4A
	protected virtual GameObject SpawnOccupyingObject(GameObject depositedEntity)
	{
		return depositedEntity;
	}

	// Token: 0x0600351C RID: 13596 RVA: 0x0012994D File Offset: 0x00127B4D
	protected virtual void ConfigureOccupyingObject(GameObject source)
	{
	}

	// Token: 0x0600351D RID: 13597 RVA: 0x00129950 File Offset: 0x00127B50
	protected virtual void PositionOccupyingObject()
	{
		if (this.rotatable != null)
		{
			this.occupyingObject.transform.SetPosition(base.gameObject.transform.GetPosition() + this.rotatable.GetRotatedOffset(this.occupyingObjectRelativePosition));
		}
		else
		{
			this.occupyingObject.transform.SetPosition(base.gameObject.transform.GetPosition() + this.occupyingObjectRelativePosition);
		}
		KBatchedAnimController component = this.occupyingObject.GetComponent<KBatchedAnimController>();
		component.enabled = false;
		component.enabled = true;
	}

	// Token: 0x0600351E RID: 13598 RVA: 0x001299E8 File Offset: 0x00127BE8
	protected void UpdateActive()
	{
		if (this.Equals(null) || this == null || base.gameObject.Equals(null) || base.gameObject == null)
		{
			return;
		}
		if (this.operational != null)
		{
			this.operational.SetActive(this.operational.IsOperational && this.occupyingObject != null, false);
		}
	}

	// Token: 0x0600351F RID: 13599 RVA: 0x00129A5A File Offset: 0x00127C5A
	protected override void OnCleanUp()
	{
		this.CancelActiveRequest();
		this.UnsubscribeFromOccupant();
		base.OnCleanUp();
	}

	// Token: 0x06003520 RID: 13600 RVA: 0x00129A6E File Offset: 0x00127C6E
	private void OnOperationalChanged(object data)
	{
		this.UpdateActive();
		if (this.occupyingObject)
		{
			this.occupyingObject.Trigger(this.operational.IsOperational ? 1628751838 : 960378201, null);
		}
	}

	// Token: 0x04001FFE RID: 8190
	[MyCmpGet]
	protected Operational operational;

	// Token: 0x04001FFF RID: 8191
	[MyCmpReq]
	protected Storage storage;

	// Token: 0x04002000 RID: 8192
	[MyCmpGet]
	public Rotatable rotatable;

	// Token: 0x04002001 RID: 8193
	protected FetchChore fetchChore;

	// Token: 0x04002002 RID: 8194
	public ChoreType choreType = Db.Get().ChoreTypes.Fetch;

	// Token: 0x04002003 RID: 8195
	[Serialize]
	public bool autoReplaceEntity;

	// Token: 0x04002004 RID: 8196
	[Serialize]
	public Tag requestedEntityTag;

	// Token: 0x04002005 RID: 8197
	[Serialize]
	public Tag requestedEntityAdditionalFilterTag;

	// Token: 0x04002006 RID: 8198
	[Serialize]
	protected Ref<KSelectable> occupyObjectRef = new Ref<KSelectable>();

	// Token: 0x04002007 RID: 8199
	[SerializeField]
	private List<Tag> possibleDepositTagsList = new List<Tag>();

	// Token: 0x04002008 RID: 8200
	[SerializeField]
	private List<Func<GameObject, bool>> additionalCriteria = new List<Func<GameObject, bool>>();

	// Token: 0x04002009 RID: 8201
	[SerializeField]
	protected bool destroyEntityOnDeposit;

	// Token: 0x0400200A RID: 8202
	[SerializeField]
	protected SingleEntityReceptacle.ReceptacleDirection direction;

	// Token: 0x0400200B RID: 8203
	public Vector3 occupyingObjectRelativePosition = new Vector3(0f, 1f, 3f);

	// Token: 0x0400200C RID: 8204
	protected StatusItem statusItemAwaitingDelivery;

	// Token: 0x0400200D RID: 8205
	protected StatusItem statusItemNeed;

	// Token: 0x0400200E RID: 8206
	protected StatusItem statusItemNoneAvailable;

	// Token: 0x0400200F RID: 8207
	private static readonly EventSystem.IntraObjectHandler<SingleEntityReceptacle> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<SingleEntityReceptacle>(delegate(SingleEntityReceptacle component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x020016F2 RID: 5874
	public enum ReceptacleDirection
	{
		// Token: 0x04007443 RID: 29763
		Top,
		// Token: 0x04007444 RID: 29764
		Side,
		// Token: 0x04007445 RID: 29765
		Bottom
	}
}
