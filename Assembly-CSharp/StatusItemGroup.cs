using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200061A RID: 1562
public class StatusItemGroup
{
	// Token: 0x0600253D RID: 9533 RVA: 0x000D4C83 File Offset: 0x000D2E83
	public IEnumerator<StatusItemGroup.Entry> GetEnumerator()
	{
		return this.items.GetEnumerator();
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x0600253E RID: 9534 RVA: 0x000D4C95 File Offset: 0x000D2E95
	// (set) Token: 0x0600253F RID: 9535 RVA: 0x000D4C9D File Offset: 0x000D2E9D
	public GameObject gameObject { get; private set; }

	// Token: 0x06002540 RID: 9536 RVA: 0x000D4CA6 File Offset: 0x000D2EA6
	public StatusItemGroup(GameObject go)
	{
		this.gameObject = go;
	}

	// Token: 0x06002541 RID: 9537 RVA: 0x000D4CDA File Offset: 0x000D2EDA
	public void SetOffset(Vector3 offset)
	{
		this.offset = offset;
		Game.Instance.SetStatusItemOffset(this.gameObject.transform, offset);
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x000D4CFC File Offset: 0x000D2EFC
	public StatusItemGroup.Entry GetStatusItem(StatusItemCategory category)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].category == category)
			{
				return this.items[i];
			}
		}
		return StatusItemGroup.Entry.EmptyEntry;
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x000D4D48 File Offset: 0x000D2F48
	public Guid SetStatusItem(StatusItemCategory category, StatusItem item, object data = null)
	{
		if (item != null && item.allowMultiples)
		{
			throw new ArgumentException(item.Name + " allows multiple instances of itself to be active so you must access it via its handle");
		}
		if (category == null)
		{
			throw new ArgumentException("SetStatusItem requires a category.");
		}
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].category == category)
			{
				if (this.items[i].item == item)
				{
					this.Log("Set (exists in category)", item, this.items[i].id, category);
					return this.items[i].id;
				}
				this.Log("Set->Remove existing in category", item, this.items[i].id, category);
				this.RemoveStatusItem(this.items[i].id, false);
			}
		}
		if (item != null)
		{
			Guid guid = this.AddStatusItem(item, data, category);
			this.Log("Set (new)", item, guid, category);
			return guid;
		}
		this.Log("Set (failed)", item, Guid.Empty, category);
		return Guid.Empty;
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x000D4E63 File Offset: 0x000D3063
	public void SetStatusItem(Guid guid, StatusItemCategory category, StatusItem new_item, object data = null)
	{
		this.RemoveStatusItem(guid, false);
		if (new_item != null)
		{
			this.AddStatusItem(new_item, data, category);
		}
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x000D4E7C File Offset: 0x000D307C
	public bool HasStatusItem(StatusItem status_item)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].item.Id == status_item.Id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002546 RID: 9542 RVA: 0x000D4EC8 File Offset: 0x000D30C8
	public bool HasStatusItemID(string status_item_id)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].item.Id == status_item_id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002547 RID: 9543 RVA: 0x000D4F0C File Offset: 0x000D310C
	public Guid AddStatusItem(StatusItem item, object data = null, StatusItemCategory category = null)
	{
		if (this.gameObject == null || (!item.allowMultiples && this.HasStatusItem(item)))
		{
			return Guid.Empty;
		}
		if (!item.allowMultiples)
		{
			using (List<StatusItemGroup.Entry>.Enumerator enumerator = this.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.item.Id == item.Id)
					{
						throw new ArgumentException("Tried to add " + item.Id + " multiples times which is not permitted.");
					}
				}
			}
		}
		StatusItemGroup.Entry entry = new StatusItemGroup.Entry(item, category, data);
		if (item.shouldNotify)
		{
			entry.notification = new Notification(item.notificationText, item.notificationType, new Func<List<Notification>, object, string>(StatusItemGroup.OnToolTip), item, false, 0f, item.notificationClickCallback, data, null, true, false, false);
			this.gameObject.AddOrGet<Notifier>().Add(entry.notification, "");
		}
		if (item.ShouldShowIcon())
		{
			Game.Instance.AddStatusItem(this.gameObject.transform, item);
			Game.Instance.SetStatusItemOffset(this.gameObject.transform, this.offset);
		}
		this.items.Add(entry);
		if (this.OnAddStatusItem != null)
		{
			this.OnAddStatusItem(entry, category);
		}
		return entry.id;
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x000D507C File Offset: 0x000D327C
	public Guid RemoveStatusItem(StatusItem status_item, bool immediate = false)
	{
		if (status_item.allowMultiples)
		{
			throw new ArgumentException(status_item.Name + " allows multiple instances of itself to be active so it must be released via an instance handle");
		}
		int i = 0;
		while (i < this.items.Count)
		{
			if (this.items[i].item.Id == status_item.Id)
			{
				Guid id = this.items[i].id;
				if (id == Guid.Empty)
				{
					return id;
				}
				this.RemoveStatusItemInternal(id, i, immediate);
				return id;
			}
			else
			{
				i++;
			}
		}
		return Guid.Empty;
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x000D5114 File Offset: 0x000D3314
	public Guid RemoveStatusItem(Guid guid, bool immediate = false)
	{
		if (guid == Guid.Empty)
		{
			return guid;
		}
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].id == guid)
			{
				this.RemoveStatusItemInternal(guid, i, immediate);
				return guid;
			}
		}
		return Guid.Empty;
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x000D5170 File Offset: 0x000D3370
	private void RemoveStatusItemInternal(Guid guid, int itemIdx, bool immediate)
	{
		StatusItemGroup.Entry entry = this.items[itemIdx];
		this.items.RemoveAt(itemIdx);
		if (entry.notification != null)
		{
			this.gameObject.GetComponent<Notifier>().Remove(entry.notification);
		}
		if (entry.item.ShouldShowIcon() && Game.Instance != null)
		{
			Game.Instance.RemoveStatusItem(this.gameObject.transform, entry.item);
		}
		if (this.OnRemoveStatusItem != null)
		{
			this.OnRemoveStatusItem(entry, immediate);
		}
	}

	// Token: 0x0600254B RID: 9547 RVA: 0x000D51FE File Offset: 0x000D33FE
	private static string OnToolTip(List<Notification> notifications, object data)
	{
		return ((StatusItem)data).notificationTooltipText + notifications.ReduceMessages(true);
	}

	// Token: 0x0600254C RID: 9548 RVA: 0x000D5217 File Offset: 0x000D3417
	public void Destroy()
	{
		if (Game.IsQuitting())
		{
			return;
		}
		while (this.items.Count > 0)
		{
			this.RemoveStatusItem(this.items[0].id, false);
		}
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x000D5248 File Offset: 0x000D3448
	[Conditional("ENABLE_LOGGER")]
	private void Log(string action, StatusItem item, Guid guid)
	{
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x000D524A File Offset: 0x000D344A
	private void Log(string action, StatusItem item, Guid guid, StatusItemCategory category)
	{
	}

	// Token: 0x040015E7 RID: 5607
	private List<StatusItemGroup.Entry> items = new List<StatusItemGroup.Entry>();

	// Token: 0x040015E8 RID: 5608
	public Action<StatusItemGroup.Entry, StatusItemCategory> OnAddStatusItem;

	// Token: 0x040015E9 RID: 5609
	public Action<StatusItemGroup.Entry, bool> OnRemoveStatusItem;

	// Token: 0x040015EB RID: 5611
	private Vector3 offset = new Vector3(0f, 0f, 0f);

	// Token: 0x020014B0 RID: 5296
	public struct Entry : IComparable<StatusItemGroup.Entry>, IEquatable<StatusItemGroup.Entry>
	{
		// Token: 0x06008E90 RID: 36496 RVA: 0x0035B9E3 File Offset: 0x00359BE3
		public Entry(StatusItem item, StatusItemCategory category, object data)
		{
			this.id = Guid.NewGuid();
			this.item = item;
			this.data = data;
			this.category = category;
			this.notification = null;
		}

		// Token: 0x06008E91 RID: 36497 RVA: 0x0035BA0C File Offset: 0x00359C0C
		public string GetName()
		{
			return this.item.GetName(this.data);
		}

		// Token: 0x06008E92 RID: 36498 RVA: 0x0035BA1F File Offset: 0x00359C1F
		public void ShowToolTip(ToolTip tooltip_widget, TextStyleSetting property_style)
		{
			this.item.ShowToolTip(tooltip_widget, this.data, property_style);
		}

		// Token: 0x06008E93 RID: 36499 RVA: 0x0035BA34 File Offset: 0x00359C34
		public void SetIcon(Image image)
		{
			this.item.SetIcon(image, this.data);
		}

		// Token: 0x06008E94 RID: 36500 RVA: 0x0035BA48 File Offset: 0x00359C48
		public int CompareTo(StatusItemGroup.Entry other)
		{
			return this.id.CompareTo(other.id);
		}

		// Token: 0x06008E95 RID: 36501 RVA: 0x0035BA5B File Offset: 0x00359C5B
		public bool Equals(StatusItemGroup.Entry other)
		{
			return this.id == other.id;
		}

		// Token: 0x06008E96 RID: 36502 RVA: 0x0035BA6E File Offset: 0x00359C6E
		public void OnClick()
		{
			this.item.OnClick(this.data);
		}

		// Token: 0x04006D7C RID: 28028
		public static StatusItemGroup.Entry EmptyEntry = new StatusItemGroup.Entry
		{
			id = Guid.Empty
		};

		// Token: 0x04006D7D RID: 28029
		public Guid id;

		// Token: 0x04006D7E RID: 28030
		public StatusItem item;

		// Token: 0x04006D7F RID: 28031
		public object data;

		// Token: 0x04006D80 RID: 28032
		public Notification notification;

		// Token: 0x04006D81 RID: 28033
		public StatusItemCategory category;
	}
}
