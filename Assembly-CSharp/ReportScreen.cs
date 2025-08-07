using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000DA6 RID: 3494
public class ReportScreen : KScreen
{
	// Token: 0x170007A3 RID: 1955
	// (get) Token: 0x06006D7E RID: 28030 RVA: 0x00296FDC File Offset: 0x002951DC
	// (set) Token: 0x06006D7F RID: 28031 RVA: 0x00296FE3 File Offset: 0x002951E3
	public static ReportScreen Instance { get; private set; }

	// Token: 0x06006D80 RID: 28032 RVA: 0x00296FEB File Offset: 0x002951EB
	public static void DestroyInstance()
	{
		ReportScreen.Instance = null;
	}

	// Token: 0x06006D81 RID: 28033 RVA: 0x00296FF4 File Offset: 0x002951F4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ReportScreen.Instance = this;
		this.closeButton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
		this.prevButton.onClick += delegate
		{
			this.ShowReport(this.currentReport.day - 1);
		};
		this.nextButton.onClick += delegate
		{
			this.ShowReport(this.currentReport.day + 1);
		};
		this.summaryButton.onClick += delegate
		{
			RetiredColonyData currentColonyRetiredColonyData = RetireColonyUtility.GetCurrentColonyRetiredColonyData();
			MainMenu.ActivateRetiredColoniesScreenFromData(PauseScreen.Instance.transform.parent.gameObject, currentColonyRetiredColonyData);
		};
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06006D82 RID: 28034 RVA: 0x00297096 File Offset: 0x00295296
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06006D83 RID: 28035 RVA: 0x0029709E File Offset: 0x0029529E
	protected override void OnShow(bool bShow)
	{
		base.OnShow(bShow);
		if (ReportManager.Instance != null)
		{
			this.currentReport = ReportManager.Instance.TodaysReport;
		}
	}

	// Token: 0x06006D84 RID: 28036 RVA: 0x002970C4 File Offset: 0x002952C4
	public void SetTitle(string title)
	{
		this.title.text = title;
	}

	// Token: 0x06006D85 RID: 28037 RVA: 0x002970D2 File Offset: 0x002952D2
	public override void ScreenUpdate(bool b)
	{
		base.ScreenUpdate(b);
		this.Refresh();
	}

	// Token: 0x06006D86 RID: 28038 RVA: 0x002970E4 File Offset: 0x002952E4
	private void Refresh()
	{
		global::Debug.Assert(this.currentReport != null);
		if (this.currentReport.day == ReportManager.Instance.TodaysReport.day)
		{
			this.SetTitle(string.Format(UI.ENDOFDAYREPORT.DAY_TITLE_TODAY, this.currentReport.day));
		}
		else if (this.currentReport.day == ReportManager.Instance.TodaysReport.day - 1)
		{
			this.SetTitle(string.Format(UI.ENDOFDAYREPORT.DAY_TITLE_YESTERDAY, this.currentReport.day));
		}
		else
		{
			this.SetTitle(string.Format(UI.ENDOFDAYREPORT.DAY_TITLE, this.currentReport.day));
		}
		bool flag = this.currentReport.day < ReportManager.Instance.TodaysReport.day;
		this.nextButton.isInteractable = flag;
		if (flag)
		{
			this.nextButton.GetComponent<ToolTip>().toolTip = string.Format(UI.ENDOFDAYREPORT.DAY_TITLE, this.currentReport.day + 1);
			this.nextButton.GetComponent<ToolTip>().enabled = true;
		}
		else
		{
			this.nextButton.GetComponent<ToolTip>().enabled = false;
		}
		flag = this.currentReport.day > 1;
		this.prevButton.isInteractable = flag;
		if (flag)
		{
			this.prevButton.GetComponent<ToolTip>().toolTip = string.Format(UI.ENDOFDAYREPORT.DAY_TITLE, this.currentReport.day - 1);
			this.prevButton.GetComponent<ToolTip>().enabled = true;
		}
		else
		{
			this.prevButton.GetComponent<ToolTip>().enabled = false;
		}
		this.AddSpacer(0);
		int num = 1;
		foreach (KeyValuePair<ReportManager.ReportType, ReportManager.ReportGroup> keyValuePair in ReportManager.Instance.ReportGroups)
		{
			ReportManager.ReportEntry entry = this.currentReport.GetEntry(keyValuePair.Key);
			if (num != keyValuePair.Value.group)
			{
				num = keyValuePair.Value.group;
				this.AddSpacer(num);
			}
			bool flag2 = entry.accumulate != 0f || keyValuePair.Value.reportIfZero;
			if (keyValuePair.Value.isHeader)
			{
				this.CreateHeader(keyValuePair.Value);
			}
			else if (flag2)
			{
				this.CreateOrUpdateLine(entry, keyValuePair.Value, flag2);
			}
		}
	}

	// Token: 0x06006D87 RID: 28039 RVA: 0x00297380 File Offset: 0x00295580
	public void ShowReport(int day)
	{
		this.currentReport = ReportManager.Instance.FindReport(day);
		global::Debug.Assert(this.currentReport != null, "Can't find report for day: " + day.ToString());
		this.Refresh();
	}

	// Token: 0x06006D88 RID: 28040 RVA: 0x002973B8 File Offset: 0x002955B8
	private GameObject AddSpacer(int group)
	{
		GameObject gameObject;
		if (this.lineItems.ContainsKey(group.ToString()))
		{
			gameObject = this.lineItems[group.ToString()];
		}
		else
		{
			gameObject = Util.KInstantiateUI(this.lineItemSpacer, this.contentFolder, false);
			gameObject.name = "Spacer" + group.ToString();
			this.lineItems[group.ToString()] = gameObject;
		}
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06006D89 RID: 28041 RVA: 0x00297438 File Offset: 0x00295638
	private GameObject CreateHeader(ReportManager.ReportGroup reportGroup)
	{
		GameObject gameObject = null;
		this.lineItems.TryGetValue(reportGroup.stringKey, out gameObject);
		if (gameObject == null)
		{
			gameObject = Util.KInstantiateUI(this.lineItemHeader, this.contentFolder, true);
			gameObject.name = "LineItemHeader" + this.lineItems.Count.ToString();
			this.lineItems[reportGroup.stringKey] = gameObject;
		}
		gameObject.SetActive(true);
		gameObject.GetComponent<ReportScreenHeader>().SetMainEntry(reportGroup);
		return gameObject;
	}

	// Token: 0x06006D8A RID: 28042 RVA: 0x002974C0 File Offset: 0x002956C0
	private GameObject CreateOrUpdateLine(ReportManager.ReportEntry entry, ReportManager.ReportGroup reportGroup, bool is_line_active)
	{
		GameObject gameObject = null;
		this.lineItems.TryGetValue(reportGroup.stringKey, out gameObject);
		if (!is_line_active)
		{
			if (gameObject != null && gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
		}
		else
		{
			if (gameObject == null)
			{
				gameObject = Util.KInstantiateUI(this.lineItem, this.contentFolder, true);
				gameObject.name = "LineItem" + this.lineItems.Count.ToString();
				this.lineItems[reportGroup.stringKey] = gameObject;
			}
			gameObject.SetActive(true);
			gameObject.GetComponent<ReportScreenEntry>().SetMainEntry(entry, reportGroup);
		}
		return gameObject;
	}

	// Token: 0x06006D8B RID: 28043 RVA: 0x00297566 File Offset: 0x00295766
	private void OnClickClose()
	{
		base.PlaySound3D(GlobalAssets.GetSound("HUD_Click_Close", false));
		this.Show(false);
	}

	// Token: 0x04004AD0 RID: 19152
	[SerializeField]
	private LocText title;

	// Token: 0x04004AD1 RID: 19153
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04004AD2 RID: 19154
	[SerializeField]
	private KButton prevButton;

	// Token: 0x04004AD3 RID: 19155
	[SerializeField]
	private KButton nextButton;

	// Token: 0x04004AD4 RID: 19156
	[SerializeField]
	private KButton summaryButton;

	// Token: 0x04004AD5 RID: 19157
	[SerializeField]
	private GameObject lineItem;

	// Token: 0x04004AD6 RID: 19158
	[SerializeField]
	private GameObject lineItemSpacer;

	// Token: 0x04004AD7 RID: 19159
	[SerializeField]
	private GameObject lineItemHeader;

	// Token: 0x04004AD8 RID: 19160
	[SerializeField]
	private GameObject contentFolder;

	// Token: 0x04004AD9 RID: 19161
	private Dictionary<string, GameObject> lineItems = new Dictionary<string, GameObject>();

	// Token: 0x04004ADA RID: 19162
	private ReportManager.DailyReport currentReport;
}
