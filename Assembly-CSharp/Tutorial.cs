using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000648 RID: 1608
[AddComponentMenu("KMonoBehaviour/scripts/Tutorial")]
public class Tutorial : KMonoBehaviour, IRender1000ms
{
	// Token: 0x060026BC RID: 9916 RVA: 0x000DCEC8 File Offset: 0x000DB0C8
	public static void ResetHiddenTutorialMessages()
	{
		if (Tutorial.Instance != null)
		{
			Tutorial.Instance.tutorialMessagesSeen.Clear();
		}
		foreach (object obj in Enum.GetValues(typeof(Tutorial.TutorialMessages)))
		{
			Tutorial.TutorialMessages tutorialMessages = (Tutorial.TutorialMessages)obj;
			KPlayerPrefs.SetInt("HideTutorial_" + tutorialMessages.ToString(), 0);
			if (Tutorial.Instance != null)
			{
				Tutorial.Instance.hiddenTutorialMessages[tutorialMessages] = false;
			}
		}
		KPlayerPrefs.SetInt("HideTutorial_CheckState", 0);
	}

	// Token: 0x060026BD RID: 9917 RVA: 0x000DCF88 File Offset: 0x000DB188
	private void LoadHiddenTutorialMessages()
	{
		foreach (object obj in Enum.GetValues(typeof(Tutorial.TutorialMessages)))
		{
			Tutorial.TutorialMessages tutorialMessages = (Tutorial.TutorialMessages)obj;
			bool flag = KPlayerPrefs.GetInt("HideTutorial_" + tutorialMessages.ToString(), 0) != 0;
			this.hiddenTutorialMessages[tutorialMessages] = flag;
		}
	}

	// Token: 0x060026BE RID: 9918 RVA: 0x000DD014 File Offset: 0x000DB214
	public void HideTutorialMessage(Tutorial.TutorialMessages message)
	{
		this.hiddenTutorialMessages[message] = true;
		KPlayerPrefs.SetInt("HideTutorial_" + message.ToString(), 1);
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x060026BF RID: 9919 RVA: 0x000DD040 File Offset: 0x000DB240
	// (set) Token: 0x060026C0 RID: 9920 RVA: 0x000DD047 File Offset: 0x000DB247
	public static Tutorial Instance { get; private set; }

	// Token: 0x060026C1 RID: 9921 RVA: 0x000DD04F File Offset: 0x000DB24F
	public static void DestroyInstance()
	{
		Tutorial.Instance = null;
	}

	// Token: 0x060026C2 RID: 9922 RVA: 0x000DD058 File Offset: 0x000DB258
	private void UpdateNotifierPosition()
	{
		if (this.notifierPosition == Vector3.zero)
		{
			GameObject activeTelepad = GameUtil.GetActiveTelepad();
			if (activeTelepad != null)
			{
				this.notifierPosition = activeTelepad.transform.GetPosition();
			}
		}
		this.notifier.transform.SetPosition(this.notifierPosition);
	}

	// Token: 0x060026C3 RID: 9923 RVA: 0x000DD0AE File Offset: 0x000DB2AE
	protected override void OnPrefabInit()
	{
		Tutorial.Instance = this;
		this.LoadHiddenTutorialMessages();
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x000DD0BC File Offset: 0x000DB2BC
	protected override void OnSpawn()
	{
		if (this.tutorialMessagesRemaining.Count != 0)
		{
			this.tutorialMessagesRemaining.Clear();
			this.tutorialMessagesSeen.Add(0);
			this.tutorialMessagesSeen.Add(1);
			this.tutorialMessagesSeen.Add(15);
			this.tutorialMessagesSeen.Add(11);
			if (GameUtil.GetCurrentCycle() > 1)
			{
				this.tutorialMessagesSeen.Add(6);
			}
			if (GameUtil.GetCurrentCycle() > 10)
			{
				this.tutorialMessagesSeen.Add(5);
				this.tutorialMessagesSeen.Add(10);
				this.tutorialMessagesSeen.Add(4);
				this.tutorialMessagesSeen.Add(13);
				this.tutorialMessagesSeen.Add(18);
				this.tutorialMessagesSeen.Add(7);
				this.tutorialMessagesSeen.Add(2);
			}
			if (GameUtil.GetCurrentCycle() > 30)
			{
				this.tutorialMessagesSeen.Add(8);
				this.tutorialMessagesSeen.Add(14);
				this.tutorialMessagesSeen.Add(19);
			}
			if (GameUtil.GetCurrentCycle() > 100)
			{
				this.tutorialMessagesSeen.Add(9);
				this.tutorialMessagesSeen.Add(16);
				this.tutorialMessagesSeen.Add(17);
			}
			if (BuildingInventory.Instance.BuildingCount("SuitLocker") > 0)
			{
				this.tutorialMessagesSeen.Add(12);
			}
		}
		if (this.saved_TM_COUNT < 24)
		{
			global::Debug.Log("Upgraded tutorial messages");
			this.saved_TM_COUNT = 24;
		}
		List<Tutorial.Item> list = new List<Tutorial.Item>();
		List<Tutorial.Item> list2 = list;
		Tutorial.Item item = new Tutorial.Item();
		item.notification = new Notification(MISC.NOTIFICATIONS.NEEDTOILET.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.NEEDTOILET.TOOLTIP.text, null, true, 5f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Plumbing");
		}, null, null, true, false, false);
		item.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.ToiletExists);
		list2.Add(item);
		this.itemTree.Add(list);
		List<Tutorial.Item> list3 = new List<Tutorial.Item>();
		List<Tutorial.Item> list4 = list3;
		Tutorial.Item item2 = new Tutorial.Item();
		item2.notification = new Notification(MISC.NOTIFICATIONS.NEEDFOOD.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.NEEDFOOD.TOOLTIP.text, null, true, 20f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Food");
		}, null, null, true, false, false);
		item2.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.FoodSourceExistsOnStartingWorld);
		list4.Add(item2);
		List<Tutorial.Item> list5 = list3;
		Tutorial.Item item3 = new Tutorial.Item();
		item3.notification = new Notification(MISC.NOTIFICATIONS.THERMALCOMFORT.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.THERMALCOMFORT.TOOLTIP.text, null, true, 0f, null, null, null, true, false, false);
		list5.Add(item3);
		this.itemTree.Add(list3);
		List<Tutorial.Item> list6 = new List<Tutorial.Item>();
		List<Tutorial.Item> list7 = list6;
		Tutorial.Item item4 = new Tutorial.Item();
		item4.notification = new Notification(MISC.NOTIFICATIONS.HYGENE_NEEDED.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.HYGENE_NEEDED.TOOLTIP, null, true, 20f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Medicine");
		}, null, null, true, false, false);
		item4.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.HygeneExists);
		list7.Add(item4);
		this.itemTree.Add(list6);
		List<Tutorial.Item> list8 = this.warningItems;
		Tutorial.Item item5 = new Tutorial.Item();
		item5.notification = new Notification(MISC.NOTIFICATIONS.NO_OXYGEN_GENERATOR.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.NO_OXYGEN_GENERATOR.TOOLTIP, null, false, 0f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Oxygen");
		}, null, null, true, false, false);
		item5.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.OxygenGeneratorBuilt);
		item5.minTimeToNotify = 80f;
		item5.lastNotifyTime = 0f;
		list8.Add(item5);
		this.warningItems.Add(new Tutorial.Item
		{
			notification = new Notification(MISC.NOTIFICATIONS.INSUFFICIENTOXYGENLASTCYCLE.NAME, NotificationType.Tutorial, new Func<List<Notification>, object, string>(this.OnOxygenTooltip), null, false, 0f, delegate(object d)
			{
				this.ZoomToNextOxygenGenerator();
			}, null, null, true, false, false),
			hideCondition = new Tutorial.HideConditionDelegate(this.OxygenGeneratorNotBuilt),
			requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.SufficientOxygenLastCycleAndThisCycle),
			minTimeToNotify = 80f,
			lastNotifyTime = 0f
		});
		this.warningItems.Add(new Tutorial.Item
		{
			notification = new Notification(MISC.NOTIFICATIONS.UNREFRIGERATEDFOOD.NAME, NotificationType.Tutorial, new Func<List<Notification>, object, string>(this.UnrefrigeratedFoodTooltip), null, false, 0f, delegate(object d)
			{
				this.ZoomToNextUnrefrigeratedFood();
			}, null, null, true, false, false),
			requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.FoodIsRefrigerated),
			minTimeToNotify = 6f,
			lastNotifyTime = 0f
		});
		List<Tutorial.Item> list9 = this.warningItems;
		Tutorial.Item item6 = new Tutorial.Item();
		item6.notification = new Notification(MISC.NOTIFICATIONS.NO_MEDICAL_COTS.NAME, NotificationType.Bad, (List<Notification> n, object o) => MISC.NOTIFICATIONS.NO_MEDICAL_COTS.TOOLTIP, null, false, 0f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Medicine");
		}, null, null, true, false, false);
		item6.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.CanTreatSickDuplicant);
		item6.minTimeToNotify = 10f;
		item6.lastNotifyTime = 0f;
		list9.Add(item6);
		List<Tutorial.Item> list10 = this.warningItems;
		Tutorial.Item item7 = new Tutorial.Item();
		item7.notification = new Notification(string.Format(UI.ENDOFDAYREPORT.TRAVELTIMEWARNING.WARNING_TITLE, Array.Empty<object>()), NotificationType.BadMinor, (List<Notification> n, object d) => string.Format(UI.ENDOFDAYREPORT.TRAVELTIMEWARNING.WARNING_MESSAGE, GameUtil.GetFormattedPercent(40f, GameUtil.TimeSlice.None)), null, true, 0f, delegate(object d)
		{
			ManagementMenu.Instance.OpenReports(GameClock.Instance.GetCycle());
		}, null, null, true, false, false);
		item7.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.LongTravelTimes);
		item7.minTimeToNotify = 1f;
		item7.lastNotifyTime = 0f;
		list10.Add(item7);
		DiscoveredResources.Instance.OnDiscover += this.OnDiscover;
	}

	// Token: 0x060026C5 RID: 9925 RVA: 0x000DD701 File Offset: 0x000DB901
	protected override void OnCleanUp()
	{
		DiscoveredResources.Instance.OnDiscover -= this.OnDiscover;
	}

	// Token: 0x060026C6 RID: 9926 RVA: 0x000DD71C File Offset: 0x000DB91C
	private void OnDiscover(Tag category_tag, Tag tag)
	{
		Element element = ElementLoader.FindElementByHash(SimHashes.UraniumOre);
		if (element != null && tag == element.tag)
		{
			this.TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, true);
		}
	}

	// Token: 0x060026C7 RID: 9927 RVA: 0x000DD750 File Offset: 0x000DB950
	public Message TutorialMessage(Tutorial.TutorialMessages tm, bool queueMessage = true)
	{
		bool flag = false;
		Message message = null;
		switch (tm)
		{
		case Tutorial.TutorialMessages.TM_Basics:
			if (DistributionPlatform.Initialized && KInputManager.currentControllerIsGamepad)
			{
				message = new TutorialMessage(Tutorial.TutorialMessages.TM_Basics, MISC.NOTIFICATIONS.BASICCONTROLS.NAME, MISC.NOTIFICATIONS.BASICCONTROLS.MESSAGEBODYALT, MISC.NOTIFICATIONS.BASICCONTROLS.TOOLTIP, null, null, null, "", null, null);
			}
			else
			{
				message = new TutorialMessage(Tutorial.TutorialMessages.TM_Basics, MISC.NOTIFICATIONS.BASICCONTROLS.NAME, MISC.NOTIFICATIONS.BASICCONTROLS.MESSAGEBODY, MISC.NOTIFICATIONS.BASICCONTROLS.TOOLTIP, null, null, null, "", null, null);
			}
			break;
		case Tutorial.TutorialMessages.TM_Welcome:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Welcome, MISC.NOTIFICATIONS.WELCOMEMESSAGE.NAME, MISC.NOTIFICATIONS.WELCOMEMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.WELCOMEMESSAGE.TOOLTIP, null, null, null, "", null, null);
			break;
		case Tutorial.TutorialMessages.TM_StressManagement:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_StressManagement, MISC.NOTIFICATIONS.STRESSMANAGEMENTMESSAGE.NAME, MISC.NOTIFICATIONS.STRESSMANAGEMENTMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.STRESSMANAGEMENTMESSAGE.TOOLTIP, null, null, null, "hud_stress", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Scheduling:
			flag = true;
			break;
		case Tutorial.TutorialMessages.TM_Mopping:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Mopping, MISC.NOTIFICATIONS.MOPPINGMESSAGE.NAME, MISC.NOTIFICATIONS.MOPPINGMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.MOPPINGMESSAGE.TOOLTIP, null, null, null, "icon_action_mop", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Locomotion:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Locomotion, MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.NAME, MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.TOOLTIP, "tutorials\\Locomotion", "Tute_Locomotion", VIDEOS.LOCOMOTION, "action_navigable_regions", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Priorities:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Priorities, MISC.NOTIFICATIONS.PRIORITIESMESSAGE.NAME, MISC.NOTIFICATIONS.PRIORITIESMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.PRIORITIESMESSAGE.TOOLTIP, null, null, null, "icon_action_prioritize", null, null);
			break;
		case Tutorial.TutorialMessages.TM_FetchingWater:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_FetchingWater, MISC.NOTIFICATIONS.FETCHINGWATERMESSAGE.NAME, MISC.NOTIFICATIONS.FETCHINGWATERMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.FETCHINGWATERMESSAGE.TOOLTIP, null, null, null, "element_liquid", null, null);
			break;
		case Tutorial.TutorialMessages.TM_ThermalComfort:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_ThermalComfort, MISC.NOTIFICATIONS.THERMALCOMFORT.NAME, MISC.NOTIFICATIONS.THERMALCOMFORT.MESSAGEBODY, MISC.NOTIFICATIONS.THERMALCOMFORT.TOOLTIP, null, null, null, "temperature", null, null);
			break;
		case Tutorial.TutorialMessages.TM_OverheatingBuildings:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_OverheatingBuildings, MISC.NOTIFICATIONS.TUTORIAL_OVERHEATING.NAME, MISC.NOTIFICATIONS.TUTORIAL_OVERHEATING.MESSAGEBODY, MISC.NOTIFICATIONS.TUTORIAL_OVERHEATING.TOOLTIP, null, null, null, "temperature", null, null);
			break;
		case Tutorial.TutorialMessages.TM_LotsOfGerms:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_LotsOfGerms, MISC.NOTIFICATIONS.LOTS_OF_GERMS.NAME, MISC.NOTIFICATIONS.LOTS_OF_GERMS.MESSAGEBODY, MISC.NOTIFICATIONS.LOTS_OF_GERMS.TOOLTIP, null, null, null, "overlay_disease", null, null);
			break;
		case Tutorial.TutorialMessages.TM_DiseaseCooking:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_DiseaseCooking, MISC.NOTIFICATIONS.DISEASE_COOKING.NAME, MISC.NOTIFICATIONS.DISEASE_COOKING.MESSAGEBODY, MISC.NOTIFICATIONS.DISEASE_COOKING.TOOLTIP, null, null, null, "icon_category_food", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Suits:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Suits, MISC.NOTIFICATIONS.SUITS.NAME, MISC.NOTIFICATIONS.SUITS.MESSAGEBODY, MISC.NOTIFICATIONS.SUITS.TOOLTIP, null, null, null, "overlay_suit", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Morale:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Morale, MISC.NOTIFICATIONS.MORALE.NAME, MISC.NOTIFICATIONS.MORALE.MESSAGEBODY, MISC.NOTIFICATIONS.MORALE.TOOLTIP, "tutorials\\Morale", "Tute_Morale", VIDEOS.MORALE, "icon_category_morale", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Schedule:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, MISC.NOTIFICATIONS.SCHEDULEMESSAGE.NAME, MISC.NOTIFICATIONS.SCHEDULEMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.SCHEDULEMESSAGE.TOOLTIP, null, null, null, "OverviewUI_schedule2_icon", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Digging:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Digging, MISC.NOTIFICATIONS.DIGGING.NAME, MISC.NOTIFICATIONS.DIGGING.MESSAGEBODY, MISC.NOTIFICATIONS.DIGGING.TOOLTIP, "tutorials\\Digging", "Tute_Digging", VIDEOS.DIGGING, "icon_action_dig", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Power:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Power, MISC.NOTIFICATIONS.POWER.NAME, MISC.NOTIFICATIONS.POWER.MESSAGEBODY, MISC.NOTIFICATIONS.POWER.TOOLTIP, "tutorials\\Power", "Tute_Power", VIDEOS.POWER, "overlay_power", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Insulation:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, MISC.NOTIFICATIONS.INSULATION.NAME, MISC.NOTIFICATIONS.INSULATION.MESSAGEBODY, MISC.NOTIFICATIONS.INSULATION.TOOLTIP, "tutorials\\Insulation", "Tute_Insulation", VIDEOS.INSULATION, "icon_thermal_conductivity", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Plumbing:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Plumbing, MISC.NOTIFICATIONS.PLUMBING.NAME, MISC.NOTIFICATIONS.PLUMBING.MESSAGEBODY, MISC.NOTIFICATIONS.PLUMBING.TOOLTIP, "tutorials\\Piping", "Tute_Plumbing", VIDEOS.PLUMBING, "icon_category_plumbing", null, null);
			break;
		case Tutorial.TutorialMessages.TM_Radiation:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, MISC.NOTIFICATIONS.RADIATION.NAME, MISC.NOTIFICATIONS.RADIATION.MESSAGEBODY, MISC.NOTIFICATIONS.RADIATION.TOOLTIP, null, null, null, "icon_category_radiation", DlcManager.EXPANSION1, null);
			break;
		case Tutorial.TutorialMessages.TM_BionicBattery:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_BionicBattery, MISC.NOTIFICATIONS.BIONICBATTERY.NAME, MISC.NOTIFICATIONS.BIONICBATTERY.MESSAGEBODY, MISC.NOTIFICATIONS.BIONICBATTERY.TOOLTIP, null, null, null, "electrobank_large", DlcManager.DLC3, null);
			break;
		case Tutorial.TutorialMessages.TM_GunkedToilet:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_GunkedToilet, MISC.NOTIFICATIONS.GUNKEDTOILET.NAME, MISC.NOTIFICATIONS.GUNKEDTOILET.MESSAGEBODY, MISC.NOTIFICATIONS.GUNKEDTOILET.TOOLTIP, null, null, null, "icon_plunger", DlcManager.DLC3, null);
			break;
		case Tutorial.TutorialMessages.TM_SlipperySurface:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_SlipperySurface, MISC.NOTIFICATIONS.SLIPPERYSURFACE.NAME, MISC.NOTIFICATIONS.SLIPPERYSURFACE.MESSAGEBODY, MISC.NOTIFICATIONS.SLIPPERYSURFACE.TOOLTIP, null, null, null, "icon_action_mop", null, null);
			break;
		case Tutorial.TutorialMessages.TM_BionicOil:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_BionicOil, MISC.NOTIFICATIONS.BIONICOIL.NAME, MISC.NOTIFICATIONS.BIONICOIL.MESSAGEBODY, MISC.NOTIFICATIONS.BIONICOIL.TOOLTIP, null, null, null, "icon_oil", DlcManager.DLC3, null);
			break;
		}
		DebugUtil.AssertArgs(message != null || flag, new object[] { "No tutorial message:", tm });
		if (queueMessage)
		{
			DebugUtil.AssertArgs(!flag, new object[] { "Attempted to queue deprecated Tutorial Message", tm });
			if (this.tutorialMessagesSeen.Contains((int)tm))
			{
				return null;
			}
			if (this.hiddenTutorialMessages.ContainsKey(tm) && this.hiddenTutorialMessages[tm])
			{
				return null;
			}
			this.tutorialMessagesSeen.Add((int)tm);
			Messenger.Instance.QueueMessage(message);
		}
		return message;
	}

	// Token: 0x060026C8 RID: 9928 RVA: 0x000DDDC8 File Offset: 0x000DBFC8
	private string OnOxygenTooltip(List<Notification> notifications, object data)
	{
		ReportManager.ReportEntry entry = ReportManager.Instance.YesterdaysReport.GetEntry(ReportManager.ReportType.OxygenCreated);
		return MISC.NOTIFICATIONS.INSUFFICIENTOXYGENLASTCYCLE.TOOLTIP.Replace("{EmittingRate}", GameUtil.GetFormattedMass(entry.Positive, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")).Replace("{ConsumptionRate}", GameUtil.GetFormattedMass(Mathf.Abs(entry.Negative), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x060026C9 RID: 9929 RVA: 0x000DDE30 File Offset: 0x000DC030
	private string UnrefrigeratedFoodTooltip(List<Notification> notifications, object data)
	{
		string text = MISC.NOTIFICATIONS.UNREFRIGERATEDFOOD.TOOLTIP;
		ListPool<Pickupable, Tutorial>.PooledList pooledList = ListPool<Pickupable, Tutorial>.Allocate();
		this.GetUnrefrigeratedFood(pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			text = text + "\n" + pooledList[i].GetProperName();
		}
		pooledList.Recycle();
		return text;
	}

	// Token: 0x060026CA RID: 9930 RVA: 0x000DDE88 File Offset: 0x000DC088
	private string OnLowFoodTooltip(List<Notification> notifications, object data)
	{
		global::Debug.Assert(((WorldContainer)data).id == ClusterManager.Instance.activeWorldId);
		float num = WorldResourceAmountTracker<RationTracker>.Get().CountAmount(null, ((WorldContainer)data).worldInventory, true);
		float num2 = (float)Components.LiveMinionIdentities.GetWorldItems(((WorldContainer)data).id, false).Count * DUPLICANTSTATS.STANDARD.BaseStats.CALORIES_BURNED_PER_CYCLE;
		return string.Format(MISC.NOTIFICATIONS.FOODLOW.TOOLTIP, GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories(Mathf.Abs(num2), GameUtil.TimeSlice.None, true));
	}

	// Token: 0x060026CB RID: 9931 RVA: 0x000DDF1C File Offset: 0x000DC11C
	public void DebugNotification()
	{
		NotificationType notificationType;
		string text;
		if (this.debugMessageCount % 3 == 0)
		{
			notificationType = NotificationType.Tutorial;
			text = "Warning message e.g. \"not enough oxygen\" uses Warning Color";
		}
		else if (this.debugMessageCount % 3 == 1)
		{
			notificationType = NotificationType.BadMinor;
			text = "Normal message e.g. Idle. Uses Normal Color BG";
		}
		else
		{
			notificationType = NotificationType.Bad;
			text = "Urgent important message. Uses Bad Color BG";
		}
		string text2 = "{0} ({1})";
		object obj = text;
		int num = this.debugMessageCount;
		this.debugMessageCount = num + 1;
		Notification notification = new Notification(string.Format(text2, obj, num.ToString()), notificationType, (List<Notification> n, object d) => MISC.NOTIFICATIONS.NEEDTOILET.TOOLTIP.text, null, true, 0f, null, null, null, true, false, false);
		this.notifier.Add(notification, "");
	}

	// Token: 0x060026CC RID: 9932 RVA: 0x000DDFC8 File Offset: 0x000DC1C8
	public void DebugNotificationMessage()
	{
		string text = "This is a message notification. ";
		int num = this.debugMessageCount;
		this.debugMessageCount = num + 1;
		Message message = new GenericMessage(text + num.ToString(), MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.TOOLTIP, null);
		Messenger.Instance.QueueMessage(message);
	}

	// Token: 0x060026CD RID: 9933 RVA: 0x000DE01C File Offset: 0x000DC21C
	public void Render1000ms(float dt)
	{
		if (App.isLoading)
		{
			return;
		}
		if (Components.LiveMinionIdentities.Count == 0)
		{
			return;
		}
		if (this.itemTree.Count > 0)
		{
			List<Tutorial.Item> list = this.itemTree[0];
			for (int i = list.Count - 1; i >= 0; i--)
			{
				Tutorial.Item item = list[i];
				if (item != null)
				{
					if (item.requirementSatisfied == null || item.requirementSatisfied())
					{
						item.notification.Clear();
						list.RemoveAt(i);
					}
					else if (item.hideCondition != null && item.hideCondition())
					{
						item.notification.Clear();
						list.RemoveAt(i);
					}
					else
					{
						this.UpdateNotifierPosition();
						this.notifier.Add(item.notification, "");
					}
				}
			}
			if (list.Count == 0)
			{
				this.itemTree.RemoveAt(0);
			}
		}
		foreach (Tutorial.Item item2 in this.warningItems)
		{
			if (item2.requirementSatisfied())
			{
				item2.notification.Clear();
				item2.lastNotifyTime = Time.time;
			}
			else if (item2.hideCondition != null && item2.hideCondition())
			{
				item2.notification.Clear();
				item2.lastNotifyTime = Time.time;
			}
			else if (item2.lastNotifyTime == 0f || Time.time - item2.lastNotifyTime > item2.minTimeToNotify)
			{
				this.notifier.Add(item2.notification, "");
				item2.lastNotifyTime = Time.time;
			}
		}
		if (GameClock.Instance.GetCycle() > 0 && !this.tutorialMessagesSeen.Contains(6) && !this.queuedPrioritiesMessage)
		{
			this.queuedPrioritiesMessage = true;
			GameScheduler.Instance.Schedule("PrioritiesTutorial", 2f, delegate(object obj)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Priorities, true);
			}, null, null);
		}
	}

	// Token: 0x060026CE RID: 9934 RVA: 0x000DE248 File Offset: 0x000DC448
	private bool OxygenGeneratorBuilt()
	{
		return this.oxygenGenerators.Count > 0;
	}

	// Token: 0x060026CF RID: 9935 RVA: 0x000DE258 File Offset: 0x000DC458
	private bool OxygenGeneratorNotBuilt()
	{
		return this.oxygenGenerators.Count == 0;
	}

	// Token: 0x060026D0 RID: 9936 RVA: 0x000DE268 File Offset: 0x000DC468
	private bool SufficientOxygenLastCycleAndThisCycle()
	{
		if (ReportManager.Instance.YesterdaysReport == null)
		{
			return true;
		}
		ReportManager.ReportEntry entry = ReportManager.Instance.YesterdaysReport.GetEntry(ReportManager.ReportType.OxygenCreated);
		return ReportManager.Instance.TodaysReport.GetEntry(ReportManager.ReportType.OxygenCreated).Net > 0.0001f || entry.Net > 0.0001f || (GameClock.Instance.GetCycle() < 1 && !GameClock.Instance.IsNighttime());
	}

	// Token: 0x060026D1 RID: 9937 RVA: 0x000DE2DD File Offset: 0x000DC4DD
	private bool FoodIsRefrigerated()
	{
		return this.GetUnrefrigeratedFood(null) <= 0;
	}

	// Token: 0x060026D2 RID: 9938 RVA: 0x000DE2EC File Offset: 0x000DC4EC
	private int GetUnrefrigeratedFood(List<Pickupable> foods)
	{
		int num = 0;
		if (ClusterManager.Instance.activeWorld.worldInventory != null)
		{
			ICollection<Pickupable> pickupables = ClusterManager.Instance.activeWorld.worldInventory.GetPickupables(GameTags.Edible, false);
			if (pickupables == null)
			{
				return 0;
			}
			foreach (Pickupable pickupable in pickupables)
			{
				if (pickupable.storage != null && (pickupable.storage.GetComponent<RationBox>() != null || pickupable.storage.GetComponent<Refrigerator>() != null))
				{
					Rottable.Instance smi = pickupable.GetSMI<Rottable.Instance>();
					if (smi != null && Rottable.RefrigerationLevel(smi) == Rottable.RotRefrigerationLevel.Normal && Rottable.AtmosphereQuality(smi) != Rottable.RotAtmosphereQuality.Sterilizing && smi != null && smi.RotConstitutionPercentage < 0.8f)
					{
						num++;
						if (foods != null)
						{
							foods.Add(pickupable);
						}
					}
				}
			}
		}
		return num;
	}

	// Token: 0x060026D3 RID: 9939 RVA: 0x000DE3E0 File Offset: 0x000DC5E0
	private bool EnergySourceExists()
	{
		return Game.Instance.circuitManager.HasGenerators();
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x000DE3F1 File Offset: 0x000DC5F1
	private bool BedExists()
	{
		return Components.NormalBeds.GlobalCount > 0;
	}

	// Token: 0x060026D5 RID: 9941 RVA: 0x000DE400 File Offset: 0x000DC600
	private bool EnoughFood()
	{
		int count = Components.LiveMinionIdentities.GetWorldItems(ClusterManager.Instance.activeWorldId, false).Count;
		float num = WorldResourceAmountTracker<RationTracker>.Get().CountAmount(null, ClusterManager.Instance.activeWorld.worldInventory, true);
		float num2 = (float)count * FOOD.FOOD_CALORIES_PER_CYCLE;
		return num / num2 >= 1f;
	}

	// Token: 0x060026D6 RID: 9942 RVA: 0x000DE458 File Offset: 0x000DC658
	private bool CanTreatSickDuplicant()
	{
		bool flag = Components.Clinics.Count >= 1;
		bool flag2 = false;
		for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
		{
			using (IEnumerator<SicknessInstance> enumerator = Components.LiveMinionIdentities[i].GetSicknesses().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Sickness.severity >= Sickness.Severity.Major)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (flag2)
			{
				break;
			}
		}
		return !flag2 || flag;
	}

	// Token: 0x060026D7 RID: 9943 RVA: 0x000DE4EC File Offset: 0x000DC6EC
	private bool LongTravelTimes()
	{
		if (ReportManager.Instance.reports.Count < 3)
		{
			return true;
		}
		float num = 0f;
		float num2 = 0f;
		for (int i = ReportManager.Instance.reports.Count - 1; i >= ReportManager.Instance.reports.Count - 3; i--)
		{
			ReportManager.ReportEntry entry = ReportManager.Instance.reports[i].GetEntry(ReportManager.ReportType.TravelTime);
			num += entry.Net;
			num2 += 600f * (float)entry.contextEntries.Count;
		}
		return num / num2 <= 0.4f;
	}

	// Token: 0x060026D8 RID: 9944 RVA: 0x000DE588 File Offset: 0x000DC788
	private bool FoodSourceExistsOnStartingWorld()
	{
		if (Components.GetMinionIdentitiesByModel(MinionConfig.MODEL).Count <= 0)
		{
			return true;
		}
		using (List<ComplexFabricator>.Enumerator enumerator = Components.ComplexFabricators.Items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetType() == typeof(MicrobeMusher))
				{
					return true;
				}
			}
		}
		return Components.PlantablePlots.GetItems(ClusterManager.Instance.GetStartWorld().id).Count > 0;
	}

	// Token: 0x060026D9 RID: 9945 RVA: 0x000DE628 File Offset: 0x000DC828
	private bool HygeneExists()
	{
		return Components.HandSanitizers.Count > 0;
	}

	// Token: 0x060026DA RID: 9946 RVA: 0x000DE637 File Offset: 0x000DC837
	private bool ToiletExists()
	{
		return Components.Toilets.Count > 0 || Components.GunkExtractors.Count > 0;
	}

	// Token: 0x060026DB RID: 9947 RVA: 0x000DE658 File Offset: 0x000DC858
	private void ZoomToNextOxygenGenerator()
	{
		if (this.oxygenGenerators.Count == 0)
		{
			return;
		}
		this.focusedOxygenGenerator %= this.oxygenGenerators.Count;
		GameObject gameObject = this.oxygenGenerators[this.focusedOxygenGenerator];
		if (gameObject != null)
		{
			Vector3 position = gameObject.transform.GetPosition();
			CameraController.Instance.SetTargetPos(position, 8f, true);
		}
		else
		{
			DebugUtil.DevLogErrorFormat("ZoomToNextOxygenGenerator generator was null: {0}", new object[] { gameObject });
		}
		this.focusedOxygenGenerator++;
	}

	// Token: 0x060026DC RID: 9948 RVA: 0x000DE6E8 File Offset: 0x000DC8E8
	private void ZoomToNextUnrefrigeratedFood()
	{
		ListPool<Pickupable, Tutorial>.PooledList pooledList = ListPool<Pickupable, Tutorial>.Allocate();
		int unrefrigeratedFood = this.GetUnrefrigeratedFood(pooledList);
		if (pooledList.Count == 0)
		{
			return;
		}
		this.focusedUnrefrigFood++;
		if (this.focusedUnrefrigFood >= unrefrigeratedFood)
		{
			this.focusedUnrefrigFood = 0;
		}
		Pickupable pickupable = pooledList[this.focusedUnrefrigFood];
		if (pickupable != null)
		{
			CameraController.Instance.SetTargetPos(pickupable.transform.GetPosition(), 8f, true);
		}
		pooledList.Recycle();
	}

	// Token: 0x040016A2 RID: 5794
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x040016A3 RID: 5795
	[Serialize]
	private int saved_TM_COUNT = 24;

	// Token: 0x040016A4 RID: 5796
	[Serialize]
	private List<int> tutorialMessagesSeen = new List<int>();

	// Token: 0x040016A5 RID: 5797
	[Obsolete("Contains invalid data")]
	[Serialize]
	private SerializedList<Tutorial.TutorialMessages> tutorialMessagesRemaining = new SerializedList<Tutorial.TutorialMessages>();

	// Token: 0x040016A6 RID: 5798
	private const string HIDDEN_TUTORIAL_PREF_KEY_PREFIX = "HideTutorial_";

	// Token: 0x040016A7 RID: 5799
	public const string HIDDEN_TUTORIAL_PREF_BUTTON_KEY = "HideTutorial_CheckState";

	// Token: 0x040016A8 RID: 5800
	private Dictionary<Tutorial.TutorialMessages, bool> hiddenTutorialMessages = new Dictionary<Tutorial.TutorialMessages, bool>();

	// Token: 0x040016A9 RID: 5801
	private int debugMessageCount;

	// Token: 0x040016AA RID: 5802
	private bool queuedPrioritiesMessage;

	// Token: 0x040016AB RID: 5803
	private const float LOW_RATION_AMOUNT = 1f;

	// Token: 0x040016AD RID: 5805
	private List<List<Tutorial.Item>> itemTree = new List<List<Tutorial.Item>>();

	// Token: 0x040016AE RID: 5806
	private List<Tutorial.Item> warningItems = new List<Tutorial.Item>();

	// Token: 0x040016AF RID: 5807
	private Vector3 notifierPosition;

	// Token: 0x040016B0 RID: 5808
	public List<GameObject> oxygenGenerators = new List<GameObject>();

	// Token: 0x040016B1 RID: 5809
	private int focusedOxygenGenerator;

	// Token: 0x040016B2 RID: 5810
	private int focusedUnrefrigFood = -1;

	// Token: 0x020014CB RID: 5323
	public enum TutorialMessages
	{
		// Token: 0x04006DC9 RID: 28105
		TM_Basics,
		// Token: 0x04006DCA RID: 28106
		TM_Welcome,
		// Token: 0x04006DCB RID: 28107
		TM_StressManagement,
		// Token: 0x04006DCC RID: 28108
		TM_Scheduling,
		// Token: 0x04006DCD RID: 28109
		TM_Mopping,
		// Token: 0x04006DCE RID: 28110
		TM_Locomotion,
		// Token: 0x04006DCF RID: 28111
		TM_Priorities,
		// Token: 0x04006DD0 RID: 28112
		TM_FetchingWater,
		// Token: 0x04006DD1 RID: 28113
		TM_ThermalComfort,
		// Token: 0x04006DD2 RID: 28114
		TM_OverheatingBuildings,
		// Token: 0x04006DD3 RID: 28115
		TM_LotsOfGerms,
		// Token: 0x04006DD4 RID: 28116
		TM_DiseaseCooking,
		// Token: 0x04006DD5 RID: 28117
		TM_Suits,
		// Token: 0x04006DD6 RID: 28118
		TM_Morale,
		// Token: 0x04006DD7 RID: 28119
		TM_Schedule,
		// Token: 0x04006DD8 RID: 28120
		TM_Digging,
		// Token: 0x04006DD9 RID: 28121
		TM_Power,
		// Token: 0x04006DDA RID: 28122
		TM_Insulation,
		// Token: 0x04006DDB RID: 28123
		TM_Plumbing,
		// Token: 0x04006DDC RID: 28124
		TM_Radiation,
		// Token: 0x04006DDD RID: 28125
		TM_BionicBattery,
		// Token: 0x04006DDE RID: 28126
		TM_GunkedToilet,
		// Token: 0x04006DDF RID: 28127
		TM_SlipperySurface,
		// Token: 0x04006DE0 RID: 28128
		TM_BionicOil,
		// Token: 0x04006DE1 RID: 28129
		TM_COUNT
	}

	// Token: 0x020014CC RID: 5324
	// (Invoke) Token: 0x06008EF0 RID: 36592
	private delegate bool HideConditionDelegate();

	// Token: 0x020014CD RID: 5325
	// (Invoke) Token: 0x06008EF4 RID: 36596
	private delegate bool RequirementSatisfiedDelegate();

	// Token: 0x020014CE RID: 5326
	private class Item
	{
		// Token: 0x04006DE2 RID: 28130
		public Notification notification;

		// Token: 0x04006DE3 RID: 28131
		public Tutorial.HideConditionDelegate hideCondition;

		// Token: 0x04006DE4 RID: 28132
		public Tutorial.RequirementSatisfiedDelegate requirementSatisfied;

		// Token: 0x04006DE5 RID: 28133
		public float minTimeToNotify;

		// Token: 0x04006DE6 RID: 28134
		public float lastNotifyTime;
	}
}
