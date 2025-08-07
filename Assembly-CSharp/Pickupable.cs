using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using FMOD.Studio;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005F8 RID: 1528
[AddComponentMenu("KMonoBehaviour/Workable/Pickupable")]
public class Pickupable : Workable, IHasSortOrder
{
	// Token: 0x17000184 RID: 388
	// (get) Token: 0x060023E3 RID: 9187 RVA: 0x000CC769 File Offset: 0x000CA969
	public PrimaryElement PrimaryElement
	{
		get
		{
			return this.primaryElement;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x060023E4 RID: 9188 RVA: 0x000CC771 File Offset: 0x000CA971
	// (set) Token: 0x060023E5 RID: 9189 RVA: 0x000CC779 File Offset: 0x000CA979
	public int sortOrder
	{
		get
		{
			return this._sortOrder;
		}
		set
		{
			this._sortOrder = value;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x060023E6 RID: 9190 RVA: 0x000CC782 File Offset: 0x000CA982
	// (set) Token: 0x060023E7 RID: 9191 RVA: 0x000CC78A File Offset: 0x000CA98A
	public Storage storage { get; set; }

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000CC793 File Offset: 0x000CA993
	public float MinTakeAmount
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x060023E9 RID: 9193 RVA: 0x000CC79A File Offset: 0x000CA99A
	public bool isChoreAllowedToPickup(ChoreType choreType)
	{
		return this.allowedChoreTypes == null || this.allowedChoreTypes.Contains(choreType);
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x060023EA RID: 9194 RVA: 0x000CC7B2 File Offset: 0x000CA9B2
	// (set) Token: 0x060023EB RID: 9195 RVA: 0x000CC7BA File Offset: 0x000CA9BA
	public bool prevent_absorb_until_stored { get; set; }

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x060023EC RID: 9196 RVA: 0x000CC7C3 File Offset: 0x000CA9C3
	// (set) Token: 0x060023ED RID: 9197 RVA: 0x000CC7CB File Offset: 0x000CA9CB
	public bool isKinematic { get; set; }

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x060023EE RID: 9198 RVA: 0x000CC7D4 File Offset: 0x000CA9D4
	// (set) Token: 0x060023EF RID: 9199 RVA: 0x000CC7DC File Offset: 0x000CA9DC
	public bool wasAbsorbed { get; private set; }

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000CC7E5 File Offset: 0x000CA9E5
	// (set) Token: 0x060023F1 RID: 9201 RVA: 0x000CC7ED File Offset: 0x000CA9ED
	public int cachedCell { get; private set; }

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000CC7F6 File Offset: 0x000CA9F6
	// (set) Token: 0x060023F3 RID: 9203 RVA: 0x000CC800 File Offset: 0x000CAA00
	public bool IsEntombed
	{
		get
		{
			return this.isEntombed;
		}
		set
		{
			if (value != this.isEntombed)
			{
				this.isEntombed = value;
				if (this.isEntombed)
				{
					base.GetComponent<KPrefabID>().AddTag(GameTags.Entombed, false);
				}
				else
				{
					base.GetComponent<KPrefabID>().RemoveTag(GameTags.Entombed);
				}
				base.Trigger(-1089732772, null);
				this.UpdateEntombedVisualizer();
			}
		}
	}

	// Token: 0x060023F4 RID: 9204 RVA: 0x000CC85A File Offset: 0x000CAA5A
	[Obsolete("Use Instance ID")]
	private bool CouldBePickedUpCommon(GameObject carrier)
	{
		return this.CouldBePickedUpCommon(carrier.GetComponent<KPrefabID>().InstanceID);
	}

	// Token: 0x060023F5 RID: 9205 RVA: 0x000CC86D File Offset: 0x000CAA6D
	private bool CouldBePickedUpCommon(int carrierID)
	{
		return this.UnreservedFetchAmount >= this.MinTakeAmount && (this.UnreservedFetchAmount > 0f || this.FindReservedAmount(carrierID) > 0f);
	}

	// Token: 0x060023F6 RID: 9206 RVA: 0x000CC89C File Offset: 0x000CAA9C
	[Obsolete("Use Instance ID")]
	public bool CouldBePickedUpByMinion(GameObject carrier)
	{
		return this.CouldBePickedUpByMinion(carrier.GetComponent<KPrefabID>().InstanceID);
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x000CC8B0 File Offset: 0x000CAAB0
	public bool CouldBePickedUpByMinion(int carrierID)
	{
		return this.CouldBePickedUpCommon(carrierID) && (this.storage == null || !this.storage.automatable || !this.storage.automatable.GetAutomationOnly());
	}

	// Token: 0x060023F8 RID: 9208 RVA: 0x000CC8FD File Offset: 0x000CAAFD
	[Obsolete("Use Instance ID")]
	public bool CouldBePickedUpByTransferArm(GameObject carrier)
	{
		return this.CouldBePickedUpByTransferArm(carrier.GetComponent<KPrefabID>().InstanceID);
	}

	// Token: 0x060023F9 RID: 9209 RVA: 0x000CC910 File Offset: 0x000CAB10
	public bool CouldBePickedUpByTransferArm(int carrierID)
	{
		return this.CouldBePickedUpCommon(carrierID) && (this.fetchable_monitor == null || this.fetchable_monitor.IsFetchable());
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x000CC932 File Offset: 0x000CAB32
	[Obsolete("Use Instance ID")]
	public float FindReservedAmount(GameObject reserver)
	{
		return this.FindReservedAmount(reserver.GetComponent<KPrefabID>().InstanceID);
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x000CC948 File Offset: 0x000CAB48
	public float FindReservedAmount(int reserverID)
	{
		for (int i = 0; i < this.reservations.Count; i++)
		{
			if (this.reservations[i].reserverID == reserverID)
			{
				return this.reservations[i].amount;
			}
		}
		return 0f;
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x060023FC RID: 9212 RVA: 0x000CC996 File Offset: 0x000CAB96
	public float UnreservedAmount
	{
		get
		{
			return this.TotalAmount - this.ReservedAmount;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x060023FD RID: 9213 RVA: 0x000CC9A5 File Offset: 0x000CABA5
	// (set) Token: 0x060023FE RID: 9214 RVA: 0x000CC9AD File Offset: 0x000CABAD
	public float ReservedAmount { get; private set; }

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x060023FF RID: 9215 RVA: 0x000CC9B6 File Offset: 0x000CABB6
	public float FetchTotalAmount
	{
		get
		{
			return this.primaryElement.MassPerUnit * this.primaryElement.Units;
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06002400 RID: 9216 RVA: 0x000CC9CF File Offset: 0x000CABCF
	public float UnreservedFetchAmount
	{
		get
		{
			return this.FetchTotalAmount - this.ReservedAmount;
		}
	}

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06002401 RID: 9217 RVA: 0x000CC9DE File Offset: 0x000CABDE
	// (set) Token: 0x06002402 RID: 9218 RVA: 0x000CC9EC File Offset: 0x000CABEC
	public float TotalAmount
	{
		get
		{
			return this.primaryElement.Units;
		}
		set
		{
			DebugUtil.Assert(this.primaryElement != null);
			this.primaryElement.Units = value;
			if (value < PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT && !this.primaryElement.KeepZeroMassObject)
			{
				base.gameObject.DeleteObject();
			}
			this.NotifyChanged(Grid.PosToCell(this));
		}
	}

	// Token: 0x06002403 RID: 9219 RVA: 0x000CCA44 File Offset: 0x000CAC44
	private void RefreshReservedAmount()
	{
		this.ReservedAmount = 0f;
		for (int i = 0; i < this.reservations.Count; i++)
		{
			this.ReservedAmount += this.reservations[i].amount;
		}
	}

	// Token: 0x06002404 RID: 9220 RVA: 0x000CCA90 File Offset: 0x000CAC90
	[Conditional("UNITY_EDITOR")]
	private void Log(string evt, string param, float value)
	{
	}

	// Token: 0x06002405 RID: 9221 RVA: 0x000CCA92 File Offset: 0x000CAC92
	public void ClearReservations()
	{
		this.reservations.Clear();
		this.RefreshReservedAmount();
	}

	// Token: 0x06002406 RID: 9222 RVA: 0x000CCAA8 File Offset: 0x000CACA8
	[ContextMenu("Print Reservations")]
	public void PrintReservations()
	{
		foreach (Pickupable.Reservation reservation in this.reservations)
		{
			global::Debug.Log(reservation.ToString());
		}
	}

	// Token: 0x06002407 RID: 9223 RVA: 0x000CCB08 File Offset: 0x000CAD08
	public int Reserve(string context, int reserverID, float amount)
	{
		int num = this.nextTicketNumber;
		this.nextTicketNumber = num + 1;
		int num2 = num;
		Pickupable.Reservation reservation = new Pickupable.Reservation(reserverID, amount, num2);
		this.reservations.Add(reservation);
		this.RefreshReservedAmount();
		if (this.OnReservationsChanged != null)
		{
			this.OnReservationsChanged(this, true, reservation);
		}
		return num2;
	}

	// Token: 0x06002408 RID: 9224 RVA: 0x000CCB5C File Offset: 0x000CAD5C
	public void Unreserve(string context, int ticket)
	{
		int i = 0;
		while (i < this.reservations.Count)
		{
			if (this.reservations[i].ticket == ticket)
			{
				Pickupable.Reservation reservation = this.reservations[i];
				this.reservations.RemoveAt(i);
				this.RefreshReservedAmount();
				if (this.OnReservationsChanged != null)
				{
					this.OnReservationsChanged(this, false, reservation);
					return;
				}
				break;
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x06002409 RID: 9225 RVA: 0x000CCBCC File Offset: 0x000CADCC
	private Pickupable()
	{
		this.showProgressBar = false;
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.shouldTransferDiseaseWithWorker = false;
	}

	// Token: 0x0600240A RID: 9226 RVA: 0x000CCC4C File Offset: 0x000CAE4C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		this.log = new LoggerFSSF("Pickupable");
		this.workerStatusItem = Db.Get().DuplicantStatusItems.PickingUp;
		base.SetWorkTime(1.5f);
		this.targetWorkable = this;
		this.resetProgressOnStop = true;
		base.gameObject.layer = Game.PickupableLayer;
		Vector3 position = base.transform.GetPosition();
		this.UpdateCachedCell(Grid.PosToCell(position));
		base.Subscribe<Pickupable>(856640610, Pickupable.OnStoreDelegate);
		base.Subscribe<Pickupable>(1188683690, Pickupable.OnLandedDelegate);
		base.Subscribe<Pickupable>(1807976145, Pickupable.OnOreSizeChangedDelegate);
		base.Subscribe<Pickupable>(-1432940121, Pickupable.OnReachableChangedDelegate);
		base.Subscribe<Pickupable>(-778359855, Pickupable.RefreshStorageTagsDelegate);
		base.Subscribe<Pickupable>(580035959, Pickupable.OnWorkableEntombOffset);
		this.KPrefabID.AddTag(GameTags.Pickupable, false);
		Components.Pickupables.Add(this);
	}

	// Token: 0x0600240B RID: 9227 RVA: 0x000CCD55 File Offset: 0x000CAF55
	protected override void OnLoadLevel()
	{
		base.OnLoadLevel();
	}

	// Token: 0x0600240C RID: 9228 RVA: 0x000CCD60 File Offset: 0x000CAF60
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = Grid.PosToCell(this);
		if (!Grid.IsValidCell(num) && this.deleteOffGrid)
		{
			base.gameObject.DeleteObject();
			return;
		}
		if (base.GetComponent<Health>() != null)
		{
			this.handleFallerComponents = false;
		}
		this.UpdateCachedCell(num);
		new ReachabilityMonitor.Instance(this).StartSM();
		this.fetchable_monitor = new FetchableMonitor.Instance(this);
		this.fetchable_monitor.StartSM();
		base.SetWorkTime(1.5f);
		this.faceTargetWhenWorking = true;
		KSelectable component = base.GetComponent<KSelectable>();
		if (component != null)
		{
			component.SetStatusIndicatorOffset(new Vector3(0f, -0.65f, 0f));
		}
		this.OnTagsChanged(null);
		this.TryToOffsetIfBuried(CellOffset.none);
		DecorProvider component2 = base.GetComponent<DecorProvider>();
		if (component2 != null && string.IsNullOrEmpty(component2.overrideName))
		{
			component2.overrideName = UI.OVERLAYS.DECOR.CLUTTER;
		}
		this.UpdateEntombedVisualizer();
		base.Subscribe<Pickupable>(-1582839653, Pickupable.OnTagsChangedDelegate);
		this.NotifyChanged(num);
	}

	// Token: 0x0600240D RID: 9229 RVA: 0x000CCE70 File Offset: 0x000CB070
	[OnDeserialized]
	public void OnDeserialize()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 28) && base.transform.position.z == 0f)
		{
			KBatchedAnimController component = base.transform.GetComponent<KBatchedAnimController>();
			component.SetSceneLayer(component.sceneLayer);
		}
	}

	// Token: 0x0600240E RID: 9230 RVA: 0x000CCEC4 File Offset: 0x000CB0C4
	public void UpdateListeners(bool worldSpace)
	{
		if (this.cleaningUp)
		{
			return;
		}
		int num = Grid.PosToCell(this);
		if (worldSpace)
		{
			if (this.solidPartitionerEntry.IsValid())
			{
				return;
			}
			GameScenePartitioner.Instance.Free(ref this.storedPartitionerEntry);
			this.objectLayerListItem = new ObjectLayerListItem(base.gameObject, ObjectLayer.Pickupables, num);
			this.solidPartitionerEntry = GameScenePartitioner.Instance.Add("Pickupable.RegisterSolidListener", base.gameObject, num, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
			this.worldPartitionerEntry = GameScenePartitioner.Instance.Add("Pickupable.RegisterPickupable", this, num, GameScenePartitioner.Instance.pickupablesLayer, null);
			Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange), "Pickupable.OnCellChange");
			Singleton<CellChangeMonitor>.Instance.MarkDirty(base.transform);
			Singleton<CellChangeMonitor>.Instance.ClearLastKnownCell(base.transform);
			return;
		}
		else
		{
			if (this.storedPartitionerEntry.IsValid())
			{
				return;
			}
			this.storedPartitionerEntry = GameScenePartitioner.Instance.Add("Pickupable.RegisterStoredPickupable", this, num, GameScenePartitioner.Instance.storedPickupablesLayer, null);
			if (this.objectLayerListItem != null)
			{
				this.objectLayerListItem.Clear();
				this.objectLayerListItem = null;
			}
			GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
			GameScenePartitioner.Instance.Free(ref this.worldPartitionerEntry);
			Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
			return;
		}
	}

	// Token: 0x0600240F RID: 9231 RVA: 0x000CD035 File Offset: 0x000CB235
	public void RegisterListeners()
	{
		this.UpdateListeners(true);
	}

	// Token: 0x06002410 RID: 9232 RVA: 0x000CD040 File Offset: 0x000CB240
	public void UnregisterListeners()
	{
		if (this.objectLayerListItem != null)
		{
			this.objectLayerListItem.Clear();
			this.objectLayerListItem = null;
		}
		GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.worldPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.storedPartitionerEntry);
		base.Unsubscribe<Pickupable>(856640610, Pickupable.OnStoreDelegate, false);
		base.Unsubscribe<Pickupable>(1188683690, Pickupable.OnLandedDelegate, false);
		base.Unsubscribe<Pickupable>(1807976145, Pickupable.OnOreSizeChangedDelegate, false);
		base.Unsubscribe<Pickupable>(-1432940121, Pickupable.OnReachableChangedDelegate, false);
		base.Unsubscribe<Pickupable>(-778359855, Pickupable.RefreshStorageTagsDelegate, false);
		base.Unsubscribe<Pickupable>(580035959, Pickupable.OnWorkableEntombOffset, false);
		if (base.isSpawned)
		{
			base.Unsubscribe<Pickupable>(-1582839653, Pickupable.OnTagsChangedDelegate, false);
		}
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
	}

	// Token: 0x06002411 RID: 9233 RVA: 0x000CD132 File Offset: 0x000CB332
	private void OnSolidChanged(object data)
	{
		this.TryToOffsetIfBuried(CellOffset.none);
	}

	// Token: 0x06002412 RID: 9234 RVA: 0x000CD140 File Offset: 0x000CB340
	private void SetWorkableOffset(object data)
	{
		CellOffset cellOffset = CellOffset.none;
		WorkerBase workerBase = data as WorkerBase;
		if (workerBase != null)
		{
			int num = Grid.PosToCell(workerBase);
			int num2 = Grid.PosToCell(this);
			cellOffset = (Grid.IsValidCell(num) ? Grid.GetCellOffsetDirection(num2, num) : CellOffset.none);
		}
		this.TryToOffsetIfBuried(cellOffset);
	}

	// Token: 0x06002413 RID: 9235 RVA: 0x000CD190 File Offset: 0x000CB390
	private CellOffset[] GetPreferedOffsets(CellOffset preferedDirectionOffset)
	{
		if (preferedDirectionOffset == CellOffset.left || preferedDirectionOffset == CellOffset.leftup)
		{
			return new CellOffset[]
			{
				CellOffset.up,
				CellOffset.left,
				CellOffset.leftup
			};
		}
		if (preferedDirectionOffset == CellOffset.right || preferedDirectionOffset == CellOffset.rightup)
		{
			return new CellOffset[]
			{
				CellOffset.up,
				CellOffset.right,
				CellOffset.rightup
			};
		}
		if (preferedDirectionOffset == CellOffset.up)
		{
			return new CellOffset[]
			{
				CellOffset.up,
				CellOffset.rightup,
				CellOffset.leftup
			};
		}
		if (preferedDirectionOffset == CellOffset.leftdown)
		{
			return new CellOffset[]
			{
				CellOffset.down,
				CellOffset.leftdown,
				CellOffset.left
			};
		}
		if (preferedDirectionOffset == CellOffset.rightdown)
		{
			return new CellOffset[]
			{
				CellOffset.down,
				CellOffset.rightdown,
				CellOffset.right
			};
		}
		if (preferedDirectionOffset == CellOffset.down)
		{
			return new CellOffset[]
			{
				CellOffset.down,
				CellOffset.leftdown,
				CellOffset.rightdown
			};
		}
		return new CellOffset[0];
	}

	// Token: 0x06002414 RID: 9236 RVA: 0x000CD310 File Offset: 0x000CB510
	public void TryToOffsetIfBuried(CellOffset offset)
	{
		if (this.KPrefabID.HasTag(GameTags.Stored) || this.KPrefabID.HasTag(GameTags.Equipped))
		{
			return;
		}
		int num = Grid.PosToCell(this);
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		DeathMonitor.Instance smi = base.gameObject.GetSMI<DeathMonitor.Instance>();
		if ((smi == null || smi.IsDead()) && ((Grid.Solid[num] && Grid.Foundation[num]) || Grid.Properties[num] != 0))
		{
			CellOffset[] array = this.GetPreferedOffsets(offset).Concat(Pickupable.displacementOffsets);
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = Grid.OffsetCell(num, array[i]);
				if (Grid.IsValidCell(num2) && !Grid.Solid[num2])
				{
					Vector3 vector = Grid.CellToPosCBC(num2, Grid.SceneLayer.Move);
					KCollider2D component = base.GetComponent<KCollider2D>();
					if (component != null)
					{
						vector.y += base.transform.GetPosition().y - component.bounds.min.y;
					}
					base.transform.SetPosition(vector);
					num = num2;
					this.RemoveFaller();
					this.AddFaller(Vector2.zero);
					break;
				}
			}
		}
		this.HandleSolidCell(num);
	}

	// Token: 0x06002415 RID: 9237 RVA: 0x000CD464 File Offset: 0x000CB664
	private bool HandleSolidCell(int cell)
	{
		bool flag = this.IsEntombed;
		bool flag2 = false;
		if (Grid.IsValidCell(cell) && Grid.Solid[cell])
		{
			DeathMonitor.Instance smi = base.gameObject.GetSMI<DeathMonitor.Instance>();
			if (smi == null || smi.IsDead())
			{
				this.Clearable.CancelClearing();
				flag2 = true;
			}
		}
		if (flag2 != flag && !this.KPrefabID.HasTag(GameTags.Stored))
		{
			this.IsEntombed = flag2;
			base.GetComponent<KSelectable>().IsSelectable = !this.IsEntombed;
		}
		this.UpdateEntombedVisualizer();
		return this.IsEntombed;
	}

	// Token: 0x06002416 RID: 9238 RVA: 0x000CD4F4 File Offset: 0x000CB6F4
	private void OnCellChange()
	{
		Vector3 position = base.transform.GetPosition();
		int num = Grid.PosToCell(position);
		if (!Grid.IsValidCell(num))
		{
			Vector2 vector = new Vector2(-0.1f * (float)Grid.WidthInCells, 1.1f * (float)Grid.WidthInCells);
			Vector2 vector2 = new Vector2(-0.1f * (float)Grid.HeightInCells, 1.1f * (float)Grid.HeightInCells);
			if (this.deleteOffGrid && (position.x < vector.x || vector.y < position.x || position.y < vector2.x || vector2.y < position.y))
			{
				this.DeleteObject();
				return;
			}
		}
		else
		{
			this.ReleaseEntombedVisualizerAndAddFaller(true);
			if (this.HandleSolidCell(num))
			{
				return;
			}
			this.objectLayerListItem.Update(num);
			bool flag = false;
			if (this.absorbable && !this.KPrefabID.HasTag(GameTags.Stored))
			{
				int num2 = Grid.CellBelow(num);
				if (Grid.IsValidCell(num2) && Grid.Solid[num2])
				{
					ObjectLayerListItem objectLayerListItem = this.objectLayerListItem.nextItem;
					while (objectLayerListItem != null)
					{
						GameObject gameObject = objectLayerListItem.gameObject;
						objectLayerListItem = objectLayerListItem.nextItem;
						Pickupable component = gameObject.GetComponent<Pickupable>();
						if (component != null)
						{
							flag = component.TryAbsorb(this, false, false);
							if (flag)
							{
								break;
							}
						}
					}
				}
			}
			GameScenePartitioner.Instance.UpdatePosition(this.solidPartitionerEntry, num);
			GameScenePartitioner.Instance.UpdatePosition(this.worldPartitionerEntry, num);
			int cachedCell = this.cachedCell;
			this.UpdateCachedCell(num);
			if (!flag)
			{
				this.NotifyChanged(num);
			}
			if (Grid.IsValidCell(cachedCell) && num != cachedCell)
			{
				this.NotifyChanged(cachedCell);
			}
		}
	}

	// Token: 0x06002417 RID: 9239 RVA: 0x000CD6A0 File Offset: 0x000CB8A0
	private void OnTagsChanged(object data)
	{
		if (!this.KPrefabID.HasTag(GameTags.Stored) && !this.KPrefabID.HasTag(GameTags.Equipped))
		{
			this.UpdateListeners(true);
			this.AddFaller(Vector2.zero);
			return;
		}
		this.UpdateListeners(false);
		this.RemoveFaller();
	}

	// Token: 0x06002418 RID: 9240 RVA: 0x000CD6F1 File Offset: 0x000CB8F1
	private void NotifyChanged(int new_cell)
	{
		GameScenePartitioner.Instance.TriggerEvent(new_cell, GameScenePartitioner.Instance.pickupablesChangedLayer, this);
	}

	// Token: 0x06002419 RID: 9241 RVA: 0x000CD70C File Offset: 0x000CB90C
	public bool TryAbsorb(Pickupable other, bool hide_effects, bool allow_cross_storage = false)
	{
		if (other == null)
		{
			return false;
		}
		if (other.wasAbsorbed)
		{
			return false;
		}
		if (this.wasAbsorbed)
		{
			return false;
		}
		if (!other.CanAbsorb(this))
		{
			return false;
		}
		if (this.prevent_absorb_until_stored)
		{
			return false;
		}
		if (!allow_cross_storage && this.storage == null != (other.storage == null))
		{
			return false;
		}
		this.Absorb(other);
		if (!hide_effects && EffectPrefabs.Instance != null && !this.storage)
		{
			Vector3 position = base.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Front);
			global::Util.KInstantiate(Assets.GetPrefab(EffectConfigs.OreAbsorbId), position, Quaternion.identity, null, null, true, 0).SetActive(true);
		}
		return true;
	}

	// Token: 0x0600241A RID: 9242 RVA: 0x000CD7D4 File Offset: 0x000CB9D4
	protected override void OnCleanUp()
	{
		this.cleaningUp = true;
		this.ReleaseEntombedVisualizerAndAddFaller(false);
		this.RemoveFaller();
		if (this.storage)
		{
			this.storage.Remove(base.gameObject, true);
		}
		this.UnregisterListeners();
		this.fetchable_monitor = null;
		Components.Pickupables.Remove(this);
		if (this.reservations.Count > 0)
		{
			Pickupable.Reservation[] array = this.reservations.ToArray();
			this.reservations.Clear();
			if (this.OnReservationsChanged != null)
			{
				foreach (Pickupable.Reservation reservation in array)
				{
					this.OnReservationsChanged(this, false, reservation);
				}
			}
		}
		if (Grid.IsValidCell(this.cachedCell))
		{
			this.NotifyChanged(this.cachedCell);
		}
		base.OnCleanUp();
	}

	// Token: 0x0600241B RID: 9243 RVA: 0x000CD89E File Offset: 0x000CBA9E
	public Pickupable TakeUnit(float units)
	{
		return this.Take(units * this.primaryElement.MassPerUnit);
	}

	// Token: 0x0600241C RID: 9244 RVA: 0x000CD8B4 File Offset: 0x000CBAB4
	public Pickupable Take(float amount)
	{
		if (amount <= 0f)
		{
			return null;
		}
		if (this.OnTake == null)
		{
			if (this.storage != null)
			{
				this.storage.Remove(base.gameObject, true);
			}
			return this;
		}
		float num = this.TotalAmount * this.primaryElement.MassPerUnit;
		if (amount >= num && this.storage != null && !this.primaryElement.KeepZeroMassObject)
		{
			this.storage.Remove(base.gameObject, true);
		}
		float num2 = Math.Min(num, amount) / this.primaryElement.MassPerUnit;
		if (num2 <= 0f)
		{
			return null;
		}
		return this.OnTake(this, num2);
	}

	// Token: 0x0600241D RID: 9245 RVA: 0x000CD964 File Offset: 0x000CBB64
	private void Absorb(Pickupable pickupable)
	{
		global::Debug.Assert(!this.wasAbsorbed);
		global::Debug.Assert(!pickupable.wasAbsorbed);
		base.Trigger(-2064133523, pickupable);
		pickupable.Trigger(-1940207677, base.gameObject);
		pickupable.wasAbsorbed = true;
		KSelectable component = base.GetComponent<KSelectable>();
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null && SelectTool.Instance.selected == pickupable.GetComponent<KSelectable>())
		{
			SelectTool.Instance.Select(component, false);
		}
		pickupable.gameObject.DeleteObject();
		this.NotifyChanged(Grid.PosToCell(this));
	}

	// Token: 0x0600241E RID: 9246 RVA: 0x000CDA14 File Offset: 0x000CBC14
	private void RefreshStorageTags(object data = null)
	{
		bool flag = data is Storage || (data != null && (bool)data);
		if (flag && data is Storage && ((Storage)data).gameObject == base.gameObject)
		{
			return;
		}
		if (!flag)
		{
			this.KPrefabID.RemoveTag(GameTags.Stored);
			this.KPrefabID.RemoveTag(GameTags.StoredPrivate);
			return;
		}
		this.KPrefabID.AddTag(GameTags.Stored, false);
		if (this.storage == null || !this.storage.allowItemRemoval)
		{
			this.KPrefabID.AddTag(GameTags.StoredPrivate, false);
			return;
		}
		this.KPrefabID.RemoveTag(GameTags.StoredPrivate);
	}

	// Token: 0x0600241F RID: 9247 RVA: 0x000CDAD0 File Offset: 0x000CBCD0
	public void OnStore(object data)
	{
		this.storage = data as Storage;
		bool flag = data is Storage || (data != null && (bool)data);
		SaveLoadRoot component = base.GetComponent<SaveLoadRoot>();
		if (this.carryAnimOverride != null && this.lastCarrier != null)
		{
			this.lastCarrier.RemoveAnimOverrides(this.carryAnimOverride);
			this.lastCarrier = null;
		}
		KSelectable component2 = base.GetComponent<KSelectable>();
		if (component2)
		{
			component2.IsSelectable = !flag;
		}
		if (flag)
		{
			int cachedCell = this.cachedCell;
			this.RefreshStorageTags(data);
			this.RemoveFaller();
			if (this.storage != null)
			{
				if (this.carryAnimOverride != null && this.storage.GetComponent<Navigator>() != null)
				{
					this.lastCarrier = this.storage.GetComponent<KBatchedAnimController>();
					if (this.lastCarrier != null && this.lastCarrier.HasTag(GameTags.BaseMinion))
					{
						this.lastCarrier.AddAnimOverrides(this.carryAnimOverride, 0f);
					}
				}
				this.UpdateCachedCell(Grid.PosToCell(this.storage));
			}
			this.NotifyChanged(cachedCell);
			if (component != null)
			{
				component.SetRegistered(false);
				return;
			}
		}
		else
		{
			if (component != null)
			{
				component.SetRegistered(true);
			}
			this.RemovedFromStorage();
		}
	}

	// Token: 0x06002420 RID: 9248 RVA: 0x000CDC20 File Offset: 0x000CBE20
	private void RemovedFromStorage()
	{
		this.storage = null;
		this.UpdateCachedCell(Grid.PosToCell(this));
		this.RefreshStorageTags(null);
		this.AddFaller(Vector2.zero);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		component.enabled = true;
		base.gameObject.transform.rotation = Quaternion.identity;
		this.UpdateListeners(true);
		component.GetBatchInstanceData().ClearOverrideTransformMatrix();
	}

	// Token: 0x06002421 RID: 9249 RVA: 0x000CDC85 File Offset: 0x000CBE85
	public void UpdateCachedCellFromStoragePosition()
	{
		global::Debug.Assert(this.storage != null, "Only call UpdateCachedCellFromStoragePosition on pickupables in storage!");
		this.UpdateCachedCell(Grid.PosToCell(this.storage));
	}

	// Token: 0x06002422 RID: 9250 RVA: 0x000CDCB0 File Offset: 0x000CBEB0
	public void UpdateCachedCell(int cell)
	{
		if (this.cachedCell != cell && this.storedPartitionerEntry.IsValid())
		{
			GameScenePartitioner.Instance.UpdatePosition(this.storedPartitionerEntry, cell);
		}
		this.cachedCell = cell;
		this.GetOffsets(this.cachedCell);
		if (this.KPrefabID.HasTag(GameTags.PickupableStorage))
		{
			base.GetComponent<Storage>().UpdateStoredItemCachedCells();
		}
	}

	// Token: 0x06002423 RID: 9251 RVA: 0x000CDD15 File Offset: 0x000CBF15
	public override int GetCell()
	{
		return this.cachedCell;
	}

	// Token: 0x06002424 RID: 9252 RVA: 0x000CDD20 File Offset: 0x000CBF20
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		if (this.useGunforPickup && worker.UsesMultiTool())
		{
			Workable.AnimInfo anim = base.GetAnim(worker);
			anim.smi = new MultitoolController.Instance(this, worker, "pickup", Assets.GetPrefab(EffectConfigs.OreAbsorbId));
			return anim;
		}
		return base.GetAnim(worker);
	}

	// Token: 0x06002425 RID: 9253 RVA: 0x000CDD78 File Offset: 0x000CBF78
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Storage component = worker.GetComponent<Storage>();
		Pickupable.PickupableStartWorkInfo pickupableStartWorkInfo = (Pickupable.PickupableStartWorkInfo)worker.GetStartWorkInfo();
		float amount = pickupableStartWorkInfo.amount;
		if (!(this != null))
		{
			pickupableStartWorkInfo.setResultCb(null);
			return;
		}
		Pickupable pickupable = this.Take(amount);
		if (pickupable != null)
		{
			component.Store(pickupable.gameObject, false, false, true, false);
			worker.SetWorkCompleteData(pickupable);
			pickupableStartWorkInfo.setResultCb(pickupable.gameObject);
			return;
		}
		pickupableStartWorkInfo.setResultCb(null);
	}

	// Token: 0x06002426 RID: 9254 RVA: 0x000CDE03 File Offset: 0x000CC003
	public override bool InstantlyFinish(WorkerBase worker)
	{
		return false;
	}

	// Token: 0x06002427 RID: 9255 RVA: 0x000CDE06 File Offset: 0x000CC006
	public override Vector3 GetTargetPoint()
	{
		return base.transform.GetPosition();
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x000CDE13 File Offset: 0x000CC013
	public bool IsReachable()
	{
		return this.isReachable;
	}

	// Token: 0x06002429 RID: 9257 RVA: 0x000CDE1C File Offset: 0x000CC01C
	private void OnReachableChanged(object data)
	{
		this.isReachable = (bool)data;
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.isReachable)
		{
			component.RemoveStatusItem(Db.Get().MiscStatusItems.PickupableUnreachable, false);
			return;
		}
		component.AddStatusItem(Db.Get().MiscStatusItems.PickupableUnreachable, this);
	}

	// Token: 0x0600242A RID: 9258 RVA: 0x000CDE73 File Offset: 0x000CC073
	private void AddFaller(Vector2 initial_velocity)
	{
		if (!this.handleFallerComponents)
		{
			return;
		}
		if (!GameComps.Fallers.Has(base.gameObject))
		{
			GameComps.Fallers.Add(base.gameObject, initial_velocity);
		}
	}

	// Token: 0x0600242B RID: 9259 RVA: 0x000CDEA2 File Offset: 0x000CC0A2
	private void RemoveFaller()
	{
		if (!this.handleFallerComponents)
		{
			return;
		}
		if (GameComps.Fallers.Has(base.gameObject))
		{
			GameComps.Fallers.Remove(base.gameObject);
		}
	}

	// Token: 0x0600242C RID: 9260 RVA: 0x000CDED0 File Offset: 0x000CC0D0
	private void OnOreSizeChanged(object data)
	{
		Vector3 vector = Vector3.zero;
		HandleVector<int>.Handle handle = GameComps.Gravities.GetHandle(base.gameObject);
		if (handle.IsValid())
		{
			vector = GameComps.Gravities.GetData(handle).velocity;
		}
		this.RemoveFaller();
		if (!this.KPrefabID.HasTag(GameTags.Stored))
		{
			this.AddFaller(vector);
		}
	}

	// Token: 0x0600242D RID: 9261 RVA: 0x000CDF38 File Offset: 0x000CC138
	private void OnLanded(object data)
	{
		if (CameraController.Instance == null)
		{
			return;
		}
		Vector3 position = base.transform.GetPosition();
		Vector2I vector2I = Grid.PosToXY(position);
		if (vector2I.x < 0 || Grid.WidthInCells <= vector2I.x || vector2I.y < 0 || Grid.HeightInCells <= vector2I.y)
		{
			this.DeleteObject();
			return;
		}
		Vector2 vector = (Vector2)data;
		if (vector.sqrMagnitude <= 0.2f || SpeedControlScreen.Instance.IsPaused)
		{
			return;
		}
		Element element = this.primaryElement.Element;
		if (element.substance != null)
		{
			string text = element.substance.GetOreBumpSound();
			if (text == null)
			{
				if (element.HasTag(GameTags.RefinedMetal))
				{
					text = "RefinedMetal";
				}
				else if (element.HasTag(GameTags.Metal))
				{
					text = "RawMetal";
				}
				else
				{
					text = "Rock";
				}
			}
			if (element.tag.ToString() == "Creature" && !base.gameObject.HasTag(GameTags.Seed))
			{
				text = "Bodyfall_rock";
			}
			else
			{
				text = "Ore_bump_" + text;
			}
			string text2 = GlobalAssets.GetSound(text, true);
			text2 = ((text2 != null) ? text2 : GlobalAssets.GetSound("Ore_bump_rock", false));
			if (CameraController.Instance.IsAudibleSound(base.transform.GetPosition(), text2))
			{
				int num = Grid.PosToCell(position);
				bool isLiquid = Grid.Element[num].IsLiquid;
				float num2 = 0f;
				if (isLiquid)
				{
					num2 = SoundUtil.GetLiquidDepth(num);
				}
				FMOD.Studio.EventInstance eventInstance = KFMOD.BeginOneShot(text2, CameraController.Instance.GetVerticallyScaledPosition(base.transform.GetPosition(), false), 1f);
				eventInstance.setParameterByName("velocity", vector.magnitude, false);
				eventInstance.setParameterByName("liquidDepth", num2, false);
				KFMOD.EndOneShot(eventInstance);
			}
		}
	}

	// Token: 0x0600242E RID: 9262 RVA: 0x000CE114 File Offset: 0x000CC314
	private void UpdateEntombedVisualizer()
	{
		if (this.IsEntombed)
		{
			if (this.entombedCell == -1)
			{
				int num = Grid.PosToCell(this);
				if (EntombedItemManager.CanEntomb(this))
				{
					SaveGame.Instance.entombedItemManager.Add(this);
				}
				if (Grid.Objects[num, 1] == null)
				{
					KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
					if (component != null && Game.Instance.GetComponent<EntombedItemVisualizer>().AddItem(num))
					{
						this.entombedCell = num;
						component.enabled = false;
						this.RemoveFaller();
						return;
					}
				}
			}
		}
		else
		{
			this.ReleaseEntombedVisualizerAndAddFaller(true);
		}
	}

	// Token: 0x0600242F RID: 9263 RVA: 0x000CE1A4 File Offset: 0x000CC3A4
	private void ReleaseEntombedVisualizerAndAddFaller(bool add_faller_if_necessary)
	{
		if (this.entombedCell != -1)
		{
			Game.Instance.GetComponent<EntombedItemVisualizer>().RemoveItem(this.entombedCell);
			this.entombedCell = -1;
			base.GetComponent<KBatchedAnimController>().enabled = true;
			if (add_faller_if_necessary)
			{
				this.AddFaller(Vector2.zero);
			}
		}
	}

	// Token: 0x040014DF RID: 5343
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x040014E0 RID: 5344
	public const float WorkTime = 1.5f;

	// Token: 0x040014E1 RID: 5345
	[SerializeField]
	private int _sortOrder;

	// Token: 0x040014E3 RID: 5347
	[MyCmpReq]
	[NonSerialized]
	public KPrefabID KPrefabID;

	// Token: 0x040014E4 RID: 5348
	[MyCmpAdd]
	[NonSerialized]
	public Clearable Clearable;

	// Token: 0x040014E5 RID: 5349
	[MyCmpAdd]
	[NonSerialized]
	public Prioritizable prioritizable;

	// Token: 0x040014E6 RID: 5350
	[SerializeField]
	public List<ChoreType> allowedChoreTypes;

	// Token: 0x040014E7 RID: 5351
	public bool absorbable;

	// Token: 0x040014E9 RID: 5353
	public Func<Pickupable, bool> CanAbsorb = (Pickupable other) => false;

	// Token: 0x040014EA RID: 5354
	public Func<Pickupable, float, Pickupable> OnTake;

	// Token: 0x040014EB RID: 5355
	public Action<Pickupable, bool, Pickupable.Reservation> OnReservationsChanged;

	// Token: 0x040014EC RID: 5356
	public ObjectLayerListItem objectLayerListItem;

	// Token: 0x040014ED RID: 5357
	public Workable targetWorkable;

	// Token: 0x040014EE RID: 5358
	public KAnimFile carryAnimOverride;

	// Token: 0x040014EF RID: 5359
	private KBatchedAnimController lastCarrier;

	// Token: 0x040014F0 RID: 5360
	public bool useGunforPickup = true;

	// Token: 0x040014F2 RID: 5362
	private static CellOffset[] displacementOffsets = new CellOffset[]
	{
		new CellOffset(0, 1),
		new CellOffset(0, -1),
		new CellOffset(1, 0),
		new CellOffset(-1, 0),
		new CellOffset(1, 1),
		new CellOffset(1, -1),
		new CellOffset(-1, 1),
		new CellOffset(-1, -1)
	};

	// Token: 0x040014F3 RID: 5363
	private bool isReachable;

	// Token: 0x040014F4 RID: 5364
	private bool isEntombed;

	// Token: 0x040014F5 RID: 5365
	private bool cleaningUp;

	// Token: 0x040014F7 RID: 5367
	public bool trackOnPickup = true;

	// Token: 0x040014F9 RID: 5369
	private int nextTicketNumber;

	// Token: 0x040014FA RID: 5370
	[Serialize]
	public bool deleteOffGrid = true;

	// Token: 0x040014FB RID: 5371
	private List<Pickupable.Reservation> reservations = new List<Pickupable.Reservation>();

	// Token: 0x040014FC RID: 5372
	private HandleVector<int>.Handle solidPartitionerEntry;

	// Token: 0x040014FD RID: 5373
	private HandleVector<int>.Handle worldPartitionerEntry;

	// Token: 0x040014FE RID: 5374
	private HandleVector<int>.Handle storedPartitionerEntry;

	// Token: 0x040014FF RID: 5375
	private FetchableMonitor.Instance fetchable_monitor;

	// Token: 0x04001500 RID: 5376
	public bool handleFallerComponents = true;

	// Token: 0x04001501 RID: 5377
	private LoggerFSSF log;

	// Token: 0x04001503 RID: 5379
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnStoreDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnStore(data);
	});

	// Token: 0x04001504 RID: 5380
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnLandedDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnLanded(data);
	});

	// Token: 0x04001505 RID: 5381
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnOreSizeChangedDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnOreSizeChanged(data);
	});

	// Token: 0x04001506 RID: 5382
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnReachableChanged(data);
	});

	// Token: 0x04001507 RID: 5383
	private static readonly EventSystem.IntraObjectHandler<Pickupable> RefreshStorageTagsDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.RefreshStorageTags(data);
	});

	// Token: 0x04001508 RID: 5384
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnWorkableEntombOffset = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.SetWorkableOffset(data);
	});

	// Token: 0x04001509 RID: 5385
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnTagsChangedDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnTagsChanged(data);
	});

	// Token: 0x0400150A RID: 5386
	private int entombedCell = -1;

	// Token: 0x02001486 RID: 5254
	public struct Reservation
	{
		// Token: 0x06008DF9 RID: 36345 RVA: 0x0035A18D File Offset: 0x0035838D
		public Reservation(int reserverID, float amount, int ticket)
		{
			this.reserverID = reserverID;
			this.amount = amount;
			this.ticket = ticket;
		}

		// Token: 0x06008DFA RID: 36346 RVA: 0x0035A1A4 File Offset: 0x003583A4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.reserverID.ToString(),
				", ",
				this.amount.ToString(),
				", ",
				this.ticket.ToString()
			});
		}

		// Token: 0x04006CD5 RID: 27861
		public int reserverID;

		// Token: 0x04006CD6 RID: 27862
		public float amount;

		// Token: 0x04006CD7 RID: 27863
		public int ticket;
	}

	// Token: 0x02001487 RID: 5255
	public class PickupableStartWorkInfo : WorkerBase.StartWorkInfo
	{
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06008DFB RID: 36347 RVA: 0x0035A1F6 File Offset: 0x003583F6
		// (set) Token: 0x06008DFC RID: 36348 RVA: 0x0035A1FE File Offset: 0x003583FE
		public float amount { get; private set; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06008DFD RID: 36349 RVA: 0x0035A207 File Offset: 0x00358407
		// (set) Token: 0x06008DFE RID: 36350 RVA: 0x0035A20F File Offset: 0x0035840F
		public Pickupable originalPickupable { get; private set; }

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06008DFF RID: 36351 RVA: 0x0035A218 File Offset: 0x00358418
		// (set) Token: 0x06008E00 RID: 36352 RVA: 0x0035A220 File Offset: 0x00358420
		public Action<GameObject> setResultCb { get; private set; }

		// Token: 0x06008E01 RID: 36353 RVA: 0x0035A229 File Offset: 0x00358429
		public PickupableStartWorkInfo(Pickupable pickupable, float amount, Action<GameObject> set_result_cb)
			: base(pickupable.targetWorkable)
		{
			this.originalPickupable = pickupable;
			this.amount = amount;
			this.setResultCb = set_result_cb;
		}
	}
}
