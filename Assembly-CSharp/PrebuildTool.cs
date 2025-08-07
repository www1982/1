using System;
using UnityEngine;

// Token: 0x0200097C RID: 2428
public class PrebuildTool : InterfaceTool
{
	// Token: 0x06004646 RID: 17990 RVA: 0x00194489 File Offset: 0x00192689
	public static void DestroyInstance()
	{
		PrebuildTool.Instance = null;
	}

	// Token: 0x06004647 RID: 17991 RVA: 0x00194491 File Offset: 0x00192691
	protected override void OnPrefabInit()
	{
		PrebuildTool.Instance = this;
	}

	// Token: 0x06004648 RID: 17992 RVA: 0x00194499 File Offset: 0x00192699
	protected override void OnActivateTool()
	{
		this.viewMode = this.def.ViewMode;
		base.OnActivateTool();
	}

	// Token: 0x06004649 RID: 17993 RVA: 0x001944B2 File Offset: 0x001926B2
	public void Activate(BuildingDef def, string errorMessage)
	{
		this.def = def;
		PlayerController.Instance.ActivateTool(this);
		PrebuildToolHoverTextCard component = base.GetComponent<PrebuildToolHoverTextCard>();
		component.errorMessage = errorMessage;
		component.currentDef = def;
	}

	// Token: 0x0600464A RID: 17994 RVA: 0x001944D9 File Offset: 0x001926D9
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		UISounds.PlaySound(UISounds.Sound.Negative);
		base.OnLeftClickDown(cursor_pos);
	}

	// Token: 0x04002EB4 RID: 11956
	public static PrebuildTool Instance;

	// Token: 0x04002EB5 RID: 11957
	private BuildingDef def;
}
