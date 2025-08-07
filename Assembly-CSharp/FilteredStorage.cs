using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000728 RID: 1832
public class FilteredStorage
{
	// Token: 0x06002E28 RID: 11816 RVA: 0x00108C6B File Offset: 0x00106E6B
	public void SetHasMeter(bool has_meter)
	{
		this.hasMeter = has_meter;
	}

	// Token: 0x06002E29 RID: 11817 RVA: 0x00108C74 File Offset: 0x00106E74
	public FilteredStorage(KMonoBehaviour root, Tag[] forbidden_tags, IUserControlledCapacity capacity_control, bool use_logic_meter, ChoreType fetch_chore_type)
	{
		this.root = root;
		this.forbiddenTags = forbidden_tags;
		this.capacityControl = capacity_control;
		this.useLogicMeter = use_logic_meter;
		this.choreType = fetch_chore_type;
		root.Subscribe(-1697596308, new Action<object>(this.OnStorageChanged));
		root.Subscribe(-543130682, new Action<object>(this.OnUserSettingsChanged));
		this.filterable = root.FindOrAdd<TreeFilterable>();
		TreeFilterable treeFilterable = this.filterable;
		treeFilterable.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Combine(treeFilterable.OnFilterChanged, new Action<HashSet<Tag>>(this.OnFilterChanged));
		this.storage = root.GetComponent<Storage>();
		this.storage.Subscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
		this.storage.Subscribe(-1852328367, new Action<object>(this.OnFunctionalChanged));
	}

	// Token: 0x06002E2A RID: 11818 RVA: 0x00108D67 File Offset: 0x00106F67
	private void OnOnlyFetchMarkedItemsSettingChanged(object data)
	{
		this.OnFilterChanged(this.filterable.GetTags());
	}

	// Token: 0x06002E2B RID: 11819 RVA: 0x00108D7C File Offset: 0x00106F7C
	private void CreateMeter()
	{
		if (!this.hasMeter)
		{
			return;
		}
		this.meter = new MeterController(this.root.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_frame", "meter_level" });
	}

	// Token: 0x06002E2C RID: 11820 RVA: 0x00108DCB File Offset: 0x00106FCB
	private void CreateLogicMeter()
	{
		if (!this.hasMeter)
		{
			return;
		}
		this.logicMeter = new MeterController(this.root.GetComponent<KBatchedAnimController>(), "logicmeter_target", "logicmeter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
	}

	// Token: 0x06002E2D RID: 11821 RVA: 0x00108DFE File Offset: 0x00106FFE
	public void SetMeter(MeterController meter)
	{
		this.hasMeter = true;
		this.meter = meter;
		this.UpdateMeter();
	}

	// Token: 0x06002E2E RID: 11822 RVA: 0x00108E14 File Offset: 0x00107014
	public void CleanUp()
	{
		if (this.filterable != null)
		{
			TreeFilterable treeFilterable = this.filterable;
			treeFilterable.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Remove(treeFilterable.OnFilterChanged, new Action<HashSet<Tag>>(this.OnFilterChanged));
		}
		if (this.fetchList != null)
		{
			this.fetchList.Cancel("Parent destroyed");
		}
	}

	// Token: 0x06002E2F RID: 11823 RVA: 0x00108E70 File Offset: 0x00107070
	public void FilterChanged()
	{
		if (this.hasMeter)
		{
			if (this.meter == null)
			{
				this.CreateMeter();
			}
			if (this.logicMeter == null && this.useLogicMeter)
			{
				this.CreateLogicMeter();
			}
		}
		this.OnFilterChanged(this.filterable.GetTags());
		this.UpdateMeter();
	}

	// Token: 0x06002E30 RID: 11824 RVA: 0x00108EC0 File Offset: 0x001070C0
	private void OnUserSettingsChanged(object data)
	{
		this.OnFilterChanged(this.filterable.GetTags());
		this.UpdateMeter();
	}

	// Token: 0x06002E31 RID: 11825 RVA: 0x00108ED9 File Offset: 0x001070D9
	private void OnStorageChanged(object data)
	{
		if (this.fetchList == null)
		{
			this.OnFilterChanged(this.filterable.GetTags());
		}
		this.UpdateMeter();
	}

	// Token: 0x06002E32 RID: 11826 RVA: 0x00108EFA File Offset: 0x001070FA
	private void OnFunctionalChanged(object data)
	{
		this.OnFilterChanged(this.filterable.GetTags());
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x00108F10 File Offset: 0x00107110
	private void UpdateMeter()
	{
		float maxCapacityMinusStorageMargin = this.GetMaxCapacityMinusStorageMargin();
		float num = Mathf.Clamp01(this.GetAmountStored() / maxCapacityMinusStorageMargin);
		if (this.meter != null)
		{
			this.meter.SetPositionPercent(num);
		}
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x00108F48 File Offset: 0x00107148
	public bool IsFull()
	{
		float maxCapacityMinusStorageMargin = this.GetMaxCapacityMinusStorageMargin();
		float num = Mathf.Clamp01(this.GetAmountStored() / maxCapacityMinusStorageMargin);
		if (this.meter != null)
		{
			this.meter.SetPositionPercent(num);
		}
		return num >= 1f;
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x00108F89 File Offset: 0x00107189
	private void OnFetchComplete()
	{
		this.OnFilterChanged(this.filterable.GetTags());
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x00108F9C File Offset: 0x0010719C
	private float GetMaxCapacity()
	{
		float num = this.storage.capacityKg;
		if (this.capacityControl != null)
		{
			num = Mathf.Min(num, this.capacityControl.UserMaxCapacity);
		}
		return num;
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x00108FD0 File Offset: 0x001071D0
	private float GetMaxCapacityMinusStorageMargin()
	{
		return this.GetMaxCapacity() - this.storage.storageFullMargin;
	}

	// Token: 0x06002E38 RID: 11832 RVA: 0x00108FE4 File Offset: 0x001071E4
	private float GetAmountStored()
	{
		float num = this.storage.MassStored();
		if (this.capacityControl != null)
		{
			num = this.capacityControl.AmountStored;
		}
		return num;
	}

	// Token: 0x06002E39 RID: 11833 RVA: 0x00109014 File Offset: 0x00107214
	private bool IsFunctional()
	{
		Operational component = this.storage.GetComponent<Operational>();
		return component == null || component.IsFunctional;
	}

	// Token: 0x06002E3A RID: 11834 RVA: 0x00109040 File Offset: 0x00107240
	private void OnFilterChanged(HashSet<Tag> tags)
	{
		bool flag = tags != null && tags.Count != 0;
		if (this.fetchList != null)
		{
			this.fetchList.Cancel("");
			this.fetchList = null;
		}
		float maxCapacityMinusStorageMargin = this.GetMaxCapacityMinusStorageMargin();
		float amountStored = this.GetAmountStored();
		float num = Mathf.Max(0f, maxCapacityMinusStorageMargin - amountStored);
		if (num > 0f && flag && this.IsFunctional())
		{
			num = Mathf.Max(0f, this.GetMaxCapacity() - amountStored);
			this.fetchList = new FetchList2(this.storage, this.choreType);
			this.fetchList.ShowStatusItem = false;
			this.fetchList.Add(tags, this.requiredTag, this.forbiddenTags, num, Operational.State.Functional);
			this.fetchList.Submit(new global::System.Action(this.OnFetchComplete), false);
		}
	}

	// Token: 0x06002E3B RID: 11835 RVA: 0x00109114 File Offset: 0x00107314
	public void SetLogicMeter(bool on)
	{
		if (this.logicMeter != null)
		{
			this.logicMeter.SetPositionPercent(on ? 1f : 0f);
		}
	}

	// Token: 0x06002E3C RID: 11836 RVA: 0x00109138 File Offset: 0x00107338
	public void SetRequiredTag(Tag tag)
	{
		if (this.requiredTag != tag)
		{
			this.requiredTag = tag;
			this.OnFilterChanged(this.filterable.GetTags());
		}
	}

	// Token: 0x06002E3D RID: 11837 RVA: 0x00109160 File Offset: 0x00107360
	public void AddForbiddenTag(Tag forbidden_tag)
	{
		if (this.forbiddenTags == null)
		{
			this.forbiddenTags = new Tag[0];
		}
		if (!this.forbiddenTags.Contains(forbidden_tag))
		{
			this.forbiddenTags = this.forbiddenTags.Append(forbidden_tag);
			this.OnFilterChanged(this.filterable.GetTags());
		}
	}

	// Token: 0x06002E3E RID: 11838 RVA: 0x001091B4 File Offset: 0x001073B4
	public void RemoveForbiddenTag(Tag forbidden_tag)
	{
		if (this.forbiddenTags != null)
		{
			List<Tag> list = new List<Tag>(this.forbiddenTags);
			list.Remove(forbidden_tag);
			this.forbiddenTags = list.ToArray();
			this.OnFilterChanged(this.filterable.GetTags());
		}
	}

	// Token: 0x04001B42 RID: 6978
	public static readonly HashedString FULL_PORT_ID = "FULL";

	// Token: 0x04001B43 RID: 6979
	private KMonoBehaviour root;

	// Token: 0x04001B44 RID: 6980
	private FetchList2 fetchList;

	// Token: 0x04001B45 RID: 6981
	private IUserControlledCapacity capacityControl;

	// Token: 0x04001B46 RID: 6982
	private TreeFilterable filterable;

	// Token: 0x04001B47 RID: 6983
	private Storage storage;

	// Token: 0x04001B48 RID: 6984
	private MeterController meter;

	// Token: 0x04001B49 RID: 6985
	private MeterController logicMeter;

	// Token: 0x04001B4A RID: 6986
	private Tag requiredTag = Tag.Invalid;

	// Token: 0x04001B4B RID: 6987
	private Tag[] forbiddenTags;

	// Token: 0x04001B4C RID: 6988
	private bool hasMeter = true;

	// Token: 0x04001B4D RID: 6989
	private bool useLogicMeter;

	// Token: 0x04001B4E RID: 6990
	private ChoreType choreType;
}
