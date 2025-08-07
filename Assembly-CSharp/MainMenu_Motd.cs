using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000D28 RID: 3368
[Serializable]
public class MainMenu_Motd
{
	// Token: 0x060067F9 RID: 26617 RVA: 0x0027347C File Offset: 0x0027167C
	public void Setup()
	{
		this.CleanUp();
		this.boxA.gameObject.SetActive(false);
		this.boxB.gameObject.SetActive(false);
		this.boxC.gameObject.SetActive(false);
		this.motdDataFetchRequest = new MotdDataFetchRequest();
		this.motdDataFetchRequest.Fetch(MotdDataFetchRequest.BuildUrl());
		this.motdDataFetchRequest.OnComplete(delegate(MotdData motdData)
		{
			this.RecieveMotdData(motdData);
		});
	}

	// Token: 0x060067FA RID: 26618 RVA: 0x002734F4 File Offset: 0x002716F4
	public void CleanUp()
	{
		if (this.motdDataFetchRequest != null)
		{
			this.motdDataFetchRequest.Dispose();
			this.motdDataFetchRequest = null;
		}
	}

	// Token: 0x060067FB RID: 26619 RVA: 0x00273510 File Offset: 0x00271710
	private void RecieveMotdData(MotdData motdData)
	{
		MainMenu_Motd.<>c__DisplayClass6_0 CS$<>8__locals1 = new MainMenu_Motd.<>c__DisplayClass6_0();
		CS$<>8__locals1.<>4__this = this;
		if (motdData == null || motdData.boxesLive == null || motdData.boxesLive.Count == 0)
		{
			global::Debug.LogWarning("MOTD Error: failed to get valid motd data, hiding ui.");
			this.boxA.gameObject.SetActive(false);
			this.boxB.gameObject.SetActive(false);
			this.boxC.gameObject.SetActive(false);
			return;
		}
		CS$<>8__locals1.boxes = motdData.boxesLive.StableSort((MotdData_Box a, MotdData_Box b) => CS$<>8__locals1.<>4__this.CalcScore(a).CompareTo(CS$<>8__locals1.<>4__this.CalcScore(b))).ToList<MotdData_Box>();
		MotdData_Box motdData_Box = CS$<>8__locals1.<RecieveMotdData>g__ConsumeBox|1("PatchNotes");
		MotdData_Box motdData_Box2 = CS$<>8__locals1.<RecieveMotdData>g__ConsumeBox|1("News");
		MotdData_Box motdData_Box3 = CS$<>8__locals1.<RecieveMotdData>g__ConsumeBox|1("Skins");
		if (motdData_Box != null)
		{
			this.boxA.Config(new MotdBox.PageData[] { this.ConvertToPageData(motdData_Box) });
			this.boxA.gameObject.SetActive(true);
		}
		if (motdData_Box2 != null)
		{
			this.boxB.Config(new MotdBox.PageData[] { this.ConvertToPageData(motdData_Box2) });
			this.boxB.gameObject.SetActive(true);
		}
		if (motdData_Box3 != null)
		{
			this.boxC.Config(new MotdBox.PageData[] { this.ConvertToPageData(motdData_Box3) });
			this.boxC.gameObject.SetActive(true);
		}
	}

	// Token: 0x060067FC RID: 26620 RVA: 0x00273653 File Offset: 0x00271853
	private int CalcScore(MotdData_Box box)
	{
		return 0;
	}

	// Token: 0x060067FD RID: 26621 RVA: 0x00273656 File Offset: 0x00271856
	private MotdBox.PageData ConvertToPageData(MotdData_Box box)
	{
		return new MotdBox.PageData
		{
			Texture = box.resolvedImage,
			HeaderText = box.title,
			ImageText = box.text,
			URL = box.href
		};
	}

	// Token: 0x04004754 RID: 18260
	[SerializeField]
	private MotdBox boxA;

	// Token: 0x04004755 RID: 18261
	[SerializeField]
	private MotdBox boxB;

	// Token: 0x04004756 RID: 18262
	[SerializeField]
	private MotdBox boxC;

	// Token: 0x04004757 RID: 18263
	private MotdDataFetchRequest motdDataFetchRequest;
}
