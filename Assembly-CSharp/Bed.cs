using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x020006DF RID: 1759
[AddComponentMenu("KMonoBehaviour/Workable/Bed")]
public class Bed : Workable, IGameObjectEffectDescriptor, IBasicBuilding
{
	// Token: 0x06002B9D RID: 11165 RVA: 0x000FB49A File Offset: 0x000F969A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = false;
	}

	// Token: 0x06002B9E RID: 11166 RVA: 0x000FB4AC File Offset: 0x000F96AC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.BasicBuildings.Add(this);
		this.sleepable = base.GetComponent<Sleepable>();
		Sleepable sleepable = this.sleepable;
		sleepable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(sleepable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkableEvent));
	}

	// Token: 0x06002B9F RID: 11167 RVA: 0x000FB4FD File Offset: 0x000F96FD
	private void OnWorkableEvent(Workable workable, Workable.WorkableEvent workable_event)
	{
		if (workable_event == Workable.WorkableEvent.WorkStarted)
		{
			this.AddEffects();
			return;
		}
		if (workable_event == Workable.WorkableEvent.WorkStopped)
		{
			this.RemoveEffects();
		}
	}

	// Token: 0x06002BA0 RID: 11168 RVA: 0x000FB514 File Offset: 0x000F9714
	private void AddEffects()
	{
		this.targetWorker = this.sleepable.worker;
		if (this.effects != null)
		{
			foreach (string text in this.effects)
			{
				this.targetWorker.GetComponent<Effects>().Add(text, false);
			}
		}
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject == null)
		{
			return;
		}
		RoomType roomType = roomOfGameObject.roomType;
		foreach (KeyValuePair<string, string> keyValuePair in Bed.roomSleepingEffects)
		{
			if (keyValuePair.Key == roomType.Id)
			{
				this.targetWorker.GetComponent<Effects>().Add(keyValuePair.Value, false);
			}
		}
		roomType.TriggerRoomEffects(base.GetComponent<KPrefabID>(), this.targetWorker.GetComponent<Effects>());
	}

	// Token: 0x06002BA1 RID: 11169 RVA: 0x000FB610 File Offset: 0x000F9810
	private void RemoveEffects()
	{
		if (this.targetWorker == null)
		{
			return;
		}
		if (this.effects != null)
		{
			foreach (string text in this.effects)
			{
				this.targetWorker.GetComponent<Effects>().Remove(text);
			}
		}
		foreach (KeyValuePair<string, string> keyValuePair in Bed.roomSleepingEffects)
		{
			this.targetWorker.GetComponent<Effects>().Remove(keyValuePair.Value);
		}
		this.targetWorker = null;
	}

	// Token: 0x06002BA2 RID: 11170 RVA: 0x000FB6BC File Offset: 0x000F98BC
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.effects != null)
		{
			foreach (string text in this.effects)
			{
				if (text != null && text != "")
				{
					Effect.AddModifierDescriptions(base.gameObject, list, text, false);
				}
			}
		}
		return list;
	}

	// Token: 0x06002BA3 RID: 11171 RVA: 0x000FB710 File Offset: 0x000F9910
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.BasicBuildings.Remove(this);
		if (this.sleepable != null)
		{
			Sleepable sleepable = this.sleepable;
			sleepable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(sleepable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkableEvent));
		}
	}

	// Token: 0x040019CC RID: 6604
	[MyCmpReq]
	private Sleepable sleepable;

	// Token: 0x040019CD RID: 6605
	private WorkerBase targetWorker;

	// Token: 0x040019CE RID: 6606
	public string[] effects;

	// Token: 0x040019CF RID: 6607
	public static readonly Dictionary<string, string> roomSleepingEffects = new Dictionary<string, string>
	{
		{ "Barracks", "BarracksStamina" },
		{ "Luxury Barracks", "BarracksStamina" },
		{ "Private Bedroom", "BedroomStamina" }
	};
}
