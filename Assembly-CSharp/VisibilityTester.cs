using System;
using UnityEngine;

// Token: 0x02000BD9 RID: 3033
[AddComponentMenu("KMonoBehaviour/scripts/VisibilityTester")]
public class VisibilityTester : KMonoBehaviour
{
	// Token: 0x06005AED RID: 23277 RVA: 0x0020DB43 File Offset: 0x0020BD43
	public static void DestroyInstance()
	{
		VisibilityTester.Instance = null;
	}

	// Token: 0x06005AEE RID: 23278 RVA: 0x0020DB4B File Offset: 0x0020BD4B
	protected override void OnPrefabInit()
	{
		VisibilityTester.Instance = this;
	}

	// Token: 0x06005AEF RID: 23279 RVA: 0x0020DB54 File Offset: 0x0020BD54
	private void Update()
	{
		if (SelectTool.Instance == null || SelectTool.Instance.selected == null || !this.enableTesting)
		{
			return;
		}
		int num = Grid.PosToCell(SelectTool.Instance.selected);
		int mouseCell = DebugHandler.GetMouseCell();
		string text = "";
		text = text + "Source Cell: " + num.ToString() + "\n";
		text = text + "Target Cell: " + mouseCell.ToString() + "\n";
		text = text + "Visible: " + Grid.VisibilityTest(num, mouseCell, false).ToString();
		for (int i = 0; i < 10000; i++)
		{
			Grid.VisibilityTest(num, mouseCell, false);
		}
		DebugText.Instance.Draw(text, Grid.CellToPosCCC(mouseCell, Grid.SceneLayer.Move), Color.white);
	}

	// Token: 0x04003C53 RID: 15443
	public static VisibilityTester Instance;

	// Token: 0x04003C54 RID: 15444
	public bool enableTesting;
}
