using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000602 RID: 1538
[AddComponentMenu("KMonoBehaviour/scripts/ReportManager")]
public class ReportManager : KMonoBehaviour
{
	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06002478 RID: 9336 RVA: 0x000CFC21 File Offset: 0x000CDE21
	public List<ReportManager.DailyReport> reports
	{
		get
		{
			return this.dailyReports;
		}
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x000CFC29 File Offset: 0x000CDE29
	public static void DestroyInstance()
	{
		ReportManager.Instance = null;
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x0600247A RID: 9338 RVA: 0x000CFC31 File Offset: 0x000CDE31
	// (set) Token: 0x0600247B RID: 9339 RVA: 0x000CFC38 File Offset: 0x000CDE38
	public static ReportManager Instance { get; private set; }

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x0600247C RID: 9340 RVA: 0x000CFC40 File Offset: 0x000CDE40
	public ReportManager.DailyReport TodaysReport
	{
		get
		{
			return this.todaysReport;
		}
	}

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x0600247D RID: 9341 RVA: 0x000CFC48 File Offset: 0x000CDE48
	public ReportManager.DailyReport YesterdaysReport
	{
		get
		{
			if (this.dailyReports.Count <= 1)
			{
				return null;
			}
			return this.dailyReports[this.dailyReports.Count - 1];
		}
	}

	// Token: 0x0600247E RID: 9342 RVA: 0x000CFC72 File Offset: 0x000CDE72
	protected override void OnPrefabInit()
	{
		ReportManager.Instance = this;
		base.Subscribe(Game.Instance.gameObject, -1917495436, new Action<object>(this.OnSaveGameReady));
		this.noteStorage = new ReportManager.NoteStorage();
	}

	// Token: 0x0600247F RID: 9343 RVA: 0x000CFCA7 File Offset: 0x000CDEA7
	protected override void OnCleanUp()
	{
		ReportManager.Instance = null;
	}

	// Token: 0x06002480 RID: 9344 RVA: 0x000CFCAF File Offset: 0x000CDEAF
	[CustomSerialize]
	private void CustomSerialize(BinaryWriter writer)
	{
		writer.Write(0);
		this.noteStorage.Serialize(writer);
	}

	// Token: 0x06002481 RID: 9345 RVA: 0x000CFCC4 File Offset: 0x000CDEC4
	[CustomDeserialize]
	private void CustomDeserialize(IReader reader)
	{
		if (this.noteStorageBytes == null)
		{
			global::Debug.Assert(reader.ReadInt32() == 0);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(reader.RawBytes()));
			binaryReader.BaseStream.Position = (long)reader.Position;
			this.noteStorage.Deserialize(binaryReader);
			reader.SkipBytes((int)binaryReader.BaseStream.Position - reader.Position);
		}
	}

	// Token: 0x06002482 RID: 9346 RVA: 0x000CFD2F File Offset: 0x000CDF2F
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.noteStorageBytes != null)
		{
			this.noteStorage.Deserialize(new BinaryReader(new MemoryStream(this.noteStorageBytes)));
			this.noteStorageBytes = null;
		}
	}

	// Token: 0x06002483 RID: 9347 RVA: 0x000CFD5C File Offset: 0x000CDF5C
	private void OnSaveGameReady(object data)
	{
		base.Subscribe(GameClock.Instance.gameObject, -722330267, new Action<object>(this.OnNightTime));
		if (this.todaysReport == null)
		{
			this.todaysReport = new ReportManager.DailyReport(this);
			this.todaysReport.day = GameUtil.GetCurrentCycle();
		}
	}

	// Token: 0x06002484 RID: 9348 RVA: 0x000CFDAF File Offset: 0x000CDFAF
	public void ReportValue(ReportManager.ReportType reportType, float value, string note = null, string context = null)
	{
		this.TodaysReport.AddData(reportType, value, note, context);
	}

	// Token: 0x06002485 RID: 9349 RVA: 0x000CFDC4 File Offset: 0x000CDFC4
	private void OnNightTime(object data)
	{
		this.dailyReports.Add(this.todaysReport);
		int day = this.todaysReport.day;
		ManagementMenuNotification managementMenuNotification = new ManagementMenuNotification(global::Action.ManageReport, NotificationValence.Good, null, string.Format(UI.ENDOFDAYREPORT.NOTIFICATION_TITLE, day), NotificationType.Good, (List<Notification> n, object d) => string.Format(UI.ENDOFDAYREPORT.NOTIFICATION_TOOLTIP, day), null, true, 0f, delegate(object d)
		{
			ManagementMenu.Instance.OpenReports(day);
		}, null, null, true);
		if (this.notifier == null)
		{
			global::Debug.LogError("Cant notify, null notifier");
		}
		else
		{
			this.notifier.Add(managementMenuNotification, "");
		}
		this.todaysReport = new ReportManager.DailyReport(this);
		this.todaysReport.day = GameUtil.GetCurrentCycle() + 1;
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x000CFE8C File Offset: 0x000CE08C
	public ReportManager.DailyReport FindReport(int day)
	{
		foreach (ReportManager.DailyReport dailyReport in this.dailyReports)
		{
			if (dailyReport.day == day)
			{
				return dailyReport;
			}
		}
		if (this.todaysReport.day == day)
		{
			return this.todaysReport;
		}
		return null;
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x000CFF00 File Offset: 0x000CE100
	public ReportManager()
	{
		Dictionary<ReportManager.ReportType, ReportManager.ReportGroup> dictionary = new Dictionary<ReportManager.ReportType, ReportManager.ReportGroup>();
		dictionary.Add(ReportManager.ReportType.DuplicantHeader, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.DUPLICANT_DETAILS_HEADER, "", "", ReportManager.ReportEntry.Order.Unordered, ReportManager.ReportEntry.Order.Unordered, true, null));
		dictionary.Add(ReportManager.ReportType.CaloriesCreated, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedCalories(v, GameUtil.TimeSlice.None, true), true, 1, UI.ENDOFDAYREPORT.CALORIES_CREATED.NAME, UI.ENDOFDAYREPORT.CALORIES_CREATED.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.CALORIES_CREATED.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.StressDelta, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v, GameUtil.TimeSlice.None), true, 1, UI.ENDOFDAYREPORT.STRESS_DELTA.NAME, UI.ENDOFDAYREPORT.STRESS_DELTA.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.STRESS_DELTA.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.DiseaseAdded, new ReportManager.ReportGroup(null, false, 1, UI.ENDOFDAYREPORT.DISEASE_ADDED.NAME, UI.ENDOFDAYREPORT.DISEASE_ADDED.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.DISEASE_ADDED.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.DiseaseStatus, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedDiseaseAmount((int)v, GameUtil.TimeSlice.None), true, 1, UI.ENDOFDAYREPORT.DISEASE_STATUS.NAME, UI.ENDOFDAYREPORT.DISEASE_STATUS.TOOLTIP, UI.ENDOFDAYREPORT.DISEASE_STATUS.TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.LevelUp, new ReportManager.ReportGroup(null, false, 1, UI.ENDOFDAYREPORT.LEVEL_UP.NAME, UI.ENDOFDAYREPORT.LEVEL_UP.TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.ToiletIncident, new ReportManager.ReportGroup(null, false, 1, UI.ENDOFDAYREPORT.TOILET_INCIDENT.NAME, UI.ENDOFDAYREPORT.TOILET_INCIDENT.TOOLTIP, UI.ENDOFDAYREPORT.TOILET_INCIDENT.TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.ChoreStatus, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.CHORE_STATUS.NAME, UI.ENDOFDAYREPORT.CHORE_STATUS.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.CHORE_STATUS.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.DomesticatedCritters, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.NUMBER_OF_DOMESTICATED_CRITTERS.NAME, UI.ENDOFDAYREPORT.NUMBER_OF_DOMESTICATED_CRITTERS.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NUMBER_OF_DOMESTICATED_CRITTERS.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.WildCritters, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.NUMBER_OF_WILD_CRITTERS.NAME, UI.ENDOFDAYREPORT.NUMBER_OF_WILD_CRITTERS.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NUMBER_OF_WILD_CRITTERS.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.RocketsInFlight, new ReportManager.ReportGroup(null, true, 1, UI.ENDOFDAYREPORT.ROCKETS_IN_FLIGHT.NAME, UI.ENDOFDAYREPORT.ROCKETS_IN_FLIGHT.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.ROCKETS_IN_FLIGHT.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.TimeSpentHeader, new ReportManager.ReportGroup(null, true, 2, UI.ENDOFDAYREPORT.TIME_DETAILS_HEADER, "", "", ReportManager.ReportEntry.Order.Unordered, ReportManager.ReportEntry.Order.Unordered, true, null));
		dictionary.Add(ReportManager.ReportType.WorkTime, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v / 600f * 100f, GameUtil.TimeSlice.None), true, 2, UI.ENDOFDAYREPORT.WORK_TIME.NAME, UI.ENDOFDAYREPORT.WORK_TIME.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, (float v, float num_entries) => GameUtil.GetFormattedPercent(v / 600f * 100f / num_entries, GameUtil.TimeSlice.None)));
		dictionary.Add(ReportManager.ReportType.TravelTime, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v / 600f * 100f, GameUtil.TimeSlice.None), true, 2, UI.ENDOFDAYREPORT.TRAVEL_TIME.NAME, UI.ENDOFDAYREPORT.TRAVEL_TIME.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, (float v, float num_entries) => GameUtil.GetFormattedPercent(v / 600f * 100f / num_entries, GameUtil.TimeSlice.None)));
		dictionary.Add(ReportManager.ReportType.PersonalTime, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v / 600f * 100f, GameUtil.TimeSlice.None), true, 2, UI.ENDOFDAYREPORT.PERSONAL_TIME.NAME, UI.ENDOFDAYREPORT.PERSONAL_TIME.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, (float v, float num_entries) => GameUtil.GetFormattedPercent(v / 600f * 100f / num_entries, GameUtil.TimeSlice.None)));
		dictionary.Add(ReportManager.ReportType.IdleTime, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedPercent(v / 600f * 100f, GameUtil.TimeSlice.None), true, 2, UI.ENDOFDAYREPORT.IDLE_TIME.NAME, UI.ENDOFDAYREPORT.IDLE_TIME.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.NONE, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, (float v, float num_entries) => GameUtil.GetFormattedPercent(v / 600f * 100f / num_entries, GameUtil.TimeSlice.None)));
		dictionary.Add(ReportManager.ReportType.BaseHeader, new ReportManager.ReportGroup(null, true, 3, UI.ENDOFDAYREPORT.BASE_DETAILS_HEADER, "", "", ReportManager.ReportEntry.Order.Unordered, ReportManager.ReportEntry.Order.Unordered, true, null));
		dictionary.Add(ReportManager.ReportType.OxygenCreated, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedMass(v, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), true, 3, UI.ENDOFDAYREPORT.OXYGEN_CREATED.NAME, UI.ENDOFDAYREPORT.OXYGEN_CREATED.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.OXYGEN_CREATED.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.EnergyCreated, new ReportManager.ReportGroup(new ReportManager.FormattingFn(GameUtil.GetFormattedRoundedJoules), true, 3, UI.ENDOFDAYREPORT.ENERGY_USAGE.NAME, UI.ENDOFDAYREPORT.ENERGY_USAGE.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.ENERGY_USAGE.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.EnergyWasted, new ReportManager.ReportGroup(new ReportManager.FormattingFn(GameUtil.GetFormattedRoundedJoules), true, 3, UI.ENDOFDAYREPORT.ENERGY_WASTED.NAME, UI.ENDOFDAYREPORT.NONE, UI.ENDOFDAYREPORT.ENERGY_WASTED.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.ContaminatedOxygenToilet, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedMass(v, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), false, 3, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_TOILET.NAME, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_TOILET.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_TOILET.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		dictionary.Add(ReportManager.ReportType.ContaminatedOxygenSublimation, new ReportManager.ReportGroup((float v) => GameUtil.GetFormattedMass(v, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), false, 3, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_SUBLIMATION.NAME, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_SUBLIMATION.POSITIVE_TOOLTIP, UI.ENDOFDAYREPORT.CONTAMINATED_OXYGEN_SUBLIMATION.NEGATIVE_TOOLTIP, ReportManager.ReportEntry.Order.Descending, ReportManager.ReportEntry.Order.Descending, false, null));
		this.ReportGroups = dictionary;
		this.dailyReports = new List<ReportManager.DailyReport>();
		base..ctor();
	}

	// Token: 0x04001543 RID: 5443
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x04001544 RID: 5444
	private ReportManager.NoteStorage noteStorage;

	// Token: 0x04001545 RID: 5445
	public Dictionary<ReportManager.ReportType, ReportManager.ReportGroup> ReportGroups;

	// Token: 0x04001546 RID: 5446
	[Serialize]
	private List<ReportManager.DailyReport> dailyReports;

	// Token: 0x04001547 RID: 5447
	[Serialize]
	private ReportManager.DailyReport todaysReport;

	// Token: 0x04001548 RID: 5448
	[Serialize]
	private byte[] noteStorageBytes;

	// Token: 0x02001492 RID: 5266
	// (Invoke) Token: 0x06008E29 RID: 36393
	public delegate string FormattingFn(float v);

	// Token: 0x02001493 RID: 5267
	// (Invoke) Token: 0x06008E2D RID: 36397
	public delegate string GroupFormattingFn(float v, float numEntries);

	// Token: 0x02001494 RID: 5268
	public enum ReportType
	{
		// Token: 0x04006CF4 RID: 27892
		DuplicantHeader,
		// Token: 0x04006CF5 RID: 27893
		CaloriesCreated,
		// Token: 0x04006CF6 RID: 27894
		StressDelta,
		// Token: 0x04006CF7 RID: 27895
		LevelUp,
		// Token: 0x04006CF8 RID: 27896
		DiseaseStatus,
		// Token: 0x04006CF9 RID: 27897
		DiseaseAdded,
		// Token: 0x04006CFA RID: 27898
		ToiletIncident,
		// Token: 0x04006CFB RID: 27899
		ChoreStatus,
		// Token: 0x04006CFC RID: 27900
		TimeSpentHeader,
		// Token: 0x04006CFD RID: 27901
		TimeSpent,
		// Token: 0x04006CFE RID: 27902
		WorkTime,
		// Token: 0x04006CFF RID: 27903
		TravelTime,
		// Token: 0x04006D00 RID: 27904
		PersonalTime,
		// Token: 0x04006D01 RID: 27905
		IdleTime,
		// Token: 0x04006D02 RID: 27906
		BaseHeader,
		// Token: 0x04006D03 RID: 27907
		ContaminatedOxygenFlatulence,
		// Token: 0x04006D04 RID: 27908
		ContaminatedOxygenToilet,
		// Token: 0x04006D05 RID: 27909
		ContaminatedOxygenSublimation,
		// Token: 0x04006D06 RID: 27910
		OxygenCreated,
		// Token: 0x04006D07 RID: 27911
		EnergyCreated,
		// Token: 0x04006D08 RID: 27912
		EnergyWasted,
		// Token: 0x04006D09 RID: 27913
		DomesticatedCritters,
		// Token: 0x04006D0A RID: 27914
		WildCritters,
		// Token: 0x04006D0B RID: 27915
		RocketsInFlight
	}

	// Token: 0x02001495 RID: 5269
	public struct ReportGroup
	{
		// Token: 0x06008E30 RID: 36400 RVA: 0x0035AA10 File Offset: 0x00358C10
		public ReportGroup(ReportManager.FormattingFn formatfn, bool reportIfZero, int group, string stringKey, string positiveTooltip, string negativeTooltip, ReportManager.ReportEntry.Order pos_note_order = ReportManager.ReportEntry.Order.Unordered, ReportManager.ReportEntry.Order neg_note_order = ReportManager.ReportEntry.Order.Unordered, bool is_header = false, ReportManager.GroupFormattingFn group_format_fn = null)
		{
			ReportManager.FormattingFn formattingFn;
			if (formatfn == null)
			{
				formattingFn = (float v) => v.ToString();
			}
			else
			{
				formattingFn = formatfn;
			}
			this.formatfn = formattingFn;
			this.groupFormatfn = group_format_fn;
			this.stringKey = stringKey;
			this.positiveTooltip = positiveTooltip;
			this.negativeTooltip = negativeTooltip;
			this.reportIfZero = reportIfZero;
			this.group = group;
			this.posNoteOrder = pos_note_order;
			this.negNoteOrder = neg_note_order;
			this.isHeader = is_header;
		}

		// Token: 0x04006D0C RID: 27916
		public ReportManager.FormattingFn formatfn;

		// Token: 0x04006D0D RID: 27917
		public ReportManager.GroupFormattingFn groupFormatfn;

		// Token: 0x04006D0E RID: 27918
		public string stringKey;

		// Token: 0x04006D0F RID: 27919
		public string positiveTooltip;

		// Token: 0x04006D10 RID: 27920
		public string negativeTooltip;

		// Token: 0x04006D11 RID: 27921
		public bool reportIfZero;

		// Token: 0x04006D12 RID: 27922
		public int group;

		// Token: 0x04006D13 RID: 27923
		public bool isHeader;

		// Token: 0x04006D14 RID: 27924
		public ReportManager.ReportEntry.Order posNoteOrder;

		// Token: 0x04006D15 RID: 27925
		public ReportManager.ReportEntry.Order negNoteOrder;
	}

	// Token: 0x02001496 RID: 5270
	[SerializationConfig(MemberSerialization.OptIn)]
	public class ReportEntry
	{
		// Token: 0x06008E31 RID: 36401 RVA: 0x0035AA90 File Offset: 0x00358C90
		public ReportEntry(ReportManager.ReportType reportType, int note_storage_id, string context, bool is_child = false)
		{
			this.reportType = reportType;
			this.context = context;
			this.isChild = is_child;
			this.accumulate = 0f;
			this.accPositive = 0f;
			this.accNegative = 0f;
			this.noteStorageId = note_storage_id;
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06008E32 RID: 36402 RVA: 0x0035AAE8 File Offset: 0x00358CE8
		public float Positive
		{
			get
			{
				return this.accPositive;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06008E33 RID: 36403 RVA: 0x0035AAF0 File Offset: 0x00358CF0
		public float Negative
		{
			get
			{
				return this.accNegative;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06008E34 RID: 36404 RVA: 0x0035AAF8 File Offset: 0x00358CF8
		public float Net
		{
			get
			{
				return this.accPositive + this.accNegative;
			}
		}

		// Token: 0x06008E35 RID: 36405 RVA: 0x0035AB07 File Offset: 0x00358D07
		[OnDeserializing]
		private void OnDeserialize()
		{
			this.contextEntries.Clear();
		}

		// Token: 0x06008E36 RID: 36406 RVA: 0x0035AB14 File Offset: 0x00358D14
		public void IterateNotes(Action<ReportManager.ReportEntry.Note> callback)
		{
			ReportManager.Instance.noteStorage.IterateNotes(this.noteStorageId, callback);
		}

		// Token: 0x06008E37 RID: 36407 RVA: 0x0035AB2C File Offset: 0x00358D2C
		[OnDeserialized]
		private void OnDeserialized()
		{
			if (this.gameHash != -1)
			{
				this.reportType = (ReportManager.ReportType)this.gameHash;
				this.gameHash = -1;
			}
		}

		// Token: 0x06008E38 RID: 36408 RVA: 0x0035AB4C File Offset: 0x00358D4C
		public void AddData(ReportManager.NoteStorage note_storage, float value, string note = null, string dataContext = null)
		{
			this.AddActualData(note_storage, value, note);
			if (dataContext != null)
			{
				ReportManager.ReportEntry reportEntry = null;
				for (int i = 0; i < this.contextEntries.Count; i++)
				{
					if (this.contextEntries[i].context == dataContext)
					{
						reportEntry = this.contextEntries[i];
						break;
					}
				}
				if (reportEntry == null)
				{
					reportEntry = new ReportManager.ReportEntry(this.reportType, note_storage.GetNewNoteId(), dataContext, true);
					this.contextEntries.Add(reportEntry);
				}
				reportEntry.AddActualData(note_storage, value, note);
			}
		}

		// Token: 0x06008E39 RID: 36409 RVA: 0x0035ABD8 File Offset: 0x00358DD8
		private void AddActualData(ReportManager.NoteStorage note_storage, float value, string note = null)
		{
			this.accumulate += value;
			if (value > 0f)
			{
				this.accPositive += value;
			}
			else
			{
				this.accNegative += value;
			}
			if (note != null)
			{
				note_storage.Add(this.noteStorageId, value, note);
			}
		}

		// Token: 0x06008E3A RID: 36410 RVA: 0x0035AC2A File Offset: 0x00358E2A
		public bool HasContextEntries()
		{
			return this.contextEntries.Count > 0;
		}

		// Token: 0x04006D16 RID: 27926
		[Serialize]
		public int noteStorageId;

		// Token: 0x04006D17 RID: 27927
		[Serialize]
		public int gameHash = -1;

		// Token: 0x04006D18 RID: 27928
		[Serialize]
		public ReportManager.ReportType reportType;

		// Token: 0x04006D19 RID: 27929
		[Serialize]
		public string context;

		// Token: 0x04006D1A RID: 27930
		[Serialize]
		public float accumulate;

		// Token: 0x04006D1B RID: 27931
		[Serialize]
		public float accPositive;

		// Token: 0x04006D1C RID: 27932
		[Serialize]
		public float accNegative;

		// Token: 0x04006D1D RID: 27933
		[Serialize]
		public ArrayRef<ReportManager.ReportEntry> contextEntries;

		// Token: 0x04006D1E RID: 27934
		public bool isChild;

		// Token: 0x02002746 RID: 10054
		public struct Note
		{
			// Token: 0x0600C64F RID: 50767 RVA: 0x00411CE3 File Offset: 0x0040FEE3
			public Note(float value, string note)
			{
				this.value = value;
				this.note = note;
			}

			// Token: 0x0400AD5E RID: 44382
			public float value;

			// Token: 0x0400AD5F RID: 44383
			public string note;
		}

		// Token: 0x02002747 RID: 10055
		public enum Order
		{
			// Token: 0x0400AD61 RID: 44385
			Unordered,
			// Token: 0x0400AD62 RID: 44386
			Ascending,
			// Token: 0x0400AD63 RID: 44387
			Descending
		}
	}

	// Token: 0x02001497 RID: 5271
	public class DailyReport
	{
		// Token: 0x06008E3B RID: 36411 RVA: 0x0035AC3C File Offset: 0x00358E3C
		public DailyReport(ReportManager manager)
		{
			foreach (KeyValuePair<ReportManager.ReportType, ReportManager.ReportGroup> keyValuePair in manager.ReportGroups)
			{
				this.reportEntries.Add(new ReportManager.ReportEntry(keyValuePair.Key, this.noteStorage.GetNewNoteId(), null, false));
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06008E3C RID: 36412 RVA: 0x0035ACC0 File Offset: 0x00358EC0
		private ReportManager.NoteStorage noteStorage
		{
			get
			{
				return ReportManager.Instance.noteStorage;
			}
		}

		// Token: 0x06008E3D RID: 36413 RVA: 0x0035ACCC File Offset: 0x00358ECC
		public ReportManager.ReportEntry GetEntry(ReportManager.ReportType reportType)
		{
			for (int i = 0; i < this.reportEntries.Count; i++)
			{
				ReportManager.ReportEntry reportEntry = this.reportEntries[i];
				if (reportEntry.reportType == reportType)
				{
					return reportEntry;
				}
			}
			ReportManager.ReportEntry reportEntry2 = new ReportManager.ReportEntry(reportType, this.noteStorage.GetNewNoteId(), null, false);
			this.reportEntries.Add(reportEntry2);
			return reportEntry2;
		}

		// Token: 0x06008E3E RID: 36414 RVA: 0x0035AD28 File Offset: 0x00358F28
		public void AddData(ReportManager.ReportType reportType, float value, string note = null, string context = null)
		{
			this.GetEntry(reportType).AddData(this.noteStorage, value, note, context);
		}

		// Token: 0x04006D1F RID: 27935
		[Serialize]
		public int day;

		// Token: 0x04006D20 RID: 27936
		[Serialize]
		public List<ReportManager.ReportEntry> reportEntries = new List<ReportManager.ReportEntry>();
	}

	// Token: 0x02001498 RID: 5272
	public class NoteStorage
	{
		// Token: 0x06008E3F RID: 36415 RVA: 0x0035AD40 File Offset: 0x00358F40
		public NoteStorage()
		{
			this.noteEntries = new ReportManager.NoteStorage.NoteEntries();
			this.stringTable = new ReportManager.NoteStorage.StringTable();
		}

		// Token: 0x06008E40 RID: 36416 RVA: 0x0035AD60 File Offset: 0x00358F60
		public void Add(int report_entry_id, float value, string note)
		{
			int num = this.stringTable.AddString(note, 6);
			this.noteEntries.Add(report_entry_id, value, num);
		}

		// Token: 0x06008E41 RID: 36417 RVA: 0x0035AD8C File Offset: 0x00358F8C
		public int GetNewNoteId()
		{
			int num = this.nextNoteId + 1;
			this.nextNoteId = num;
			return num;
		}

		// Token: 0x06008E42 RID: 36418 RVA: 0x0035ADAA File Offset: 0x00358FAA
		public void IterateNotes(int report_entry_id, Action<ReportManager.ReportEntry.Note> callback)
		{
			this.noteEntries.IterateNotes(this.stringTable, report_entry_id, callback);
		}

		// Token: 0x06008E43 RID: 36419 RVA: 0x0035ADBF File Offset: 0x00358FBF
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(6);
			writer.Write(this.nextNoteId);
			this.stringTable.Serialize(writer);
			this.noteEntries.Serialize(writer);
		}

		// Token: 0x06008E44 RID: 36420 RVA: 0x0035ADEC File Offset: 0x00358FEC
		public void Deserialize(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if (num < 5)
			{
				return;
			}
			this.nextNoteId = reader.ReadInt32();
			this.stringTable.Deserialize(reader, num);
			this.noteEntries.Deserialize(reader, num);
		}

		// Token: 0x04006D21 RID: 27937
		public const int SERIALIZATION_VERSION = 6;

		// Token: 0x04006D22 RID: 27938
		private int nextNoteId;

		// Token: 0x04006D23 RID: 27939
		private ReportManager.NoteStorage.NoteEntries noteEntries;

		// Token: 0x04006D24 RID: 27940
		private ReportManager.NoteStorage.StringTable stringTable;

		// Token: 0x02002748 RID: 10056
		private class StringTable
		{
			// Token: 0x0600C650 RID: 50768 RVA: 0x00411CF4 File Offset: 0x0040FEF4
			public int AddString(string str, int version = 6)
			{
				int num = Hash.SDBMLower(str);
				this.strings[num] = str;
				return num;
			}

			// Token: 0x0600C651 RID: 50769 RVA: 0x00411D18 File Offset: 0x0040FF18
			public string GetStringByHash(int hash)
			{
				string text = "";
				this.strings.TryGetValue(hash, out text);
				return text;
			}

			// Token: 0x0600C652 RID: 50770 RVA: 0x00411D3C File Offset: 0x0040FF3C
			public void Serialize(BinaryWriter writer)
			{
				writer.Write(this.strings.Count);
				foreach (KeyValuePair<int, string> keyValuePair in this.strings)
				{
					writer.Write(keyValuePair.Value);
				}
			}

			// Token: 0x0600C653 RID: 50771 RVA: 0x00411DA8 File Offset: 0x0040FFA8
			public void Deserialize(BinaryReader reader, int version)
			{
				int num = reader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					string text = reader.ReadString();
					this.AddString(text, version);
				}
			}

			// Token: 0x0400AD64 RID: 44388
			private Dictionary<int, string> strings = new Dictionary<int, string>();
		}

		// Token: 0x02002749 RID: 10057
		private class NoteEntries
		{
			// Token: 0x0600C655 RID: 50773 RVA: 0x00411DEC File Offset: 0x0040FFEC
			public void Add(int report_entry_id, float value, int note_id)
			{
				Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> dictionary;
				if (!this.entries.TryGetValue(report_entry_id, out dictionary))
				{
					dictionary = new Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>(ReportManager.NoteStorage.NoteEntries.sKeyComparer);
					this.entries[report_entry_id] = dictionary;
				}
				ReportManager.NoteStorage.NoteEntries.NoteEntryKey noteEntryKey = new ReportManager.NoteStorage.NoteEntries.NoteEntryKey
				{
					noteHash = note_id,
					isPositive = (value > 0f)
				};
				if (dictionary.ContainsKey(noteEntryKey))
				{
					Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> dictionary2 = dictionary;
					ReportManager.NoteStorage.NoteEntries.NoteEntryKey noteEntryKey2 = noteEntryKey;
					dictionary2[noteEntryKey2] += value;
					return;
				}
				dictionary[noteEntryKey] = value;
			}

			// Token: 0x0600C656 RID: 50774 RVA: 0x00411E68 File Offset: 0x00410068
			public void Serialize(BinaryWriter writer)
			{
				writer.Write(this.entries.Count);
				foreach (KeyValuePair<int, Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>> keyValuePair in this.entries)
				{
					writer.Write(keyValuePair.Key);
					writer.Write(keyValuePair.Value.Count);
					foreach (KeyValuePair<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> keyValuePair2 in keyValuePair.Value)
					{
						writer.Write(keyValuePair2.Key.noteHash);
						writer.Write(keyValuePair2.Key.isPositive);
						writer.WriteSingleFast(keyValuePair2.Value);
					}
				}
			}

			// Token: 0x0600C657 RID: 50775 RVA: 0x00411F58 File Offset: 0x00410158
			public void Deserialize(BinaryReader reader, int version)
			{
				if (version < 6)
				{
					OldNoteEntriesV5 oldNoteEntriesV = new OldNoteEntriesV5();
					oldNoteEntriesV.Deserialize(reader);
					foreach (OldNoteEntriesV5.NoteStorageBlock noteStorageBlock in oldNoteEntriesV.storageBlocks)
					{
						for (int i = 0; i < noteStorageBlock.entryCount; i++)
						{
							OldNoteEntriesV5.NoteEntry noteEntry = noteStorageBlock.entries.structs[i];
							this.Add(noteEntry.reportEntryId, noteEntry.value, noteEntry.noteHash);
						}
					}
					return;
				}
				int num = reader.ReadInt32();
				this.entries = new Dictionary<int, Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>>(num);
				for (int j = 0; j < num; j++)
				{
					int num2 = reader.ReadInt32();
					int num3 = reader.ReadInt32();
					Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> dictionary = new Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>(num3, ReportManager.NoteStorage.NoteEntries.sKeyComparer);
					this.entries[num2] = dictionary;
					for (int k = 0; k < num3; k++)
					{
						ReportManager.NoteStorage.NoteEntries.NoteEntryKey noteEntryKey = new ReportManager.NoteStorage.NoteEntries.NoteEntryKey
						{
							noteHash = reader.ReadInt32(),
							isPositive = reader.ReadBoolean()
						};
						dictionary[noteEntryKey] = reader.ReadSingle();
					}
				}
			}

			// Token: 0x0600C658 RID: 50776 RVA: 0x0041208C File Offset: 0x0041028C
			public void IterateNotes(ReportManager.NoteStorage.StringTable string_table, int report_entry_id, Action<ReportManager.ReportEntry.Note> callback)
			{
				Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> dictionary;
				if (this.entries.TryGetValue(report_entry_id, out dictionary))
				{
					foreach (KeyValuePair<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float> keyValuePair in dictionary)
					{
						string stringByHash = string_table.GetStringByHash(keyValuePair.Key.noteHash);
						ReportManager.ReportEntry.Note note = new ReportManager.ReportEntry.Note(keyValuePair.Value, stringByHash);
						callback(note);
					}
				}
			}

			// Token: 0x0400AD65 RID: 44389
			private static ReportManager.NoteStorage.NoteEntries.NoteEntryKeyComparer sKeyComparer = new ReportManager.NoteStorage.NoteEntries.NoteEntryKeyComparer();

			// Token: 0x0400AD66 RID: 44390
			private Dictionary<int, Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>> entries = new Dictionary<int, Dictionary<ReportManager.NoteStorage.NoteEntries.NoteEntryKey, float>>();

			// Token: 0x0200387E RID: 14462
			public struct NoteEntryKey
			{
				// Token: 0x0400E467 RID: 58471
				public int noteHash;

				// Token: 0x0400E468 RID: 58472
				public bool isPositive;
			}

			// Token: 0x0200387F RID: 14463
			public class NoteEntryKeyComparer : IEqualityComparer<ReportManager.NoteStorage.NoteEntries.NoteEntryKey>
			{
				// Token: 0x0600EC68 RID: 60520 RVA: 0x004744B7 File Offset: 0x004726B7
				public bool Equals(ReportManager.NoteStorage.NoteEntries.NoteEntryKey a, ReportManager.NoteStorage.NoteEntries.NoteEntryKey b)
				{
					return a.noteHash == b.noteHash && a.isPositive == b.isPositive;
				}

				// Token: 0x0600EC69 RID: 60521 RVA: 0x004744D7 File Offset: 0x004726D7
				public int GetHashCode(ReportManager.NoteStorage.NoteEntries.NoteEntryKey a)
				{
					return a.noteHash * (a.isPositive ? 1 : (-1));
				}
			}
		}
	}
}
