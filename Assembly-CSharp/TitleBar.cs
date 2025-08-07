using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E67 RID: 3687
[AddComponentMenu("KMonoBehaviour/scripts/TitleBar")]
public class TitleBar : KMonoBehaviour
{
	// Token: 0x060075A8 RID: 30120 RVA: 0x002D0C22 File Offset: 0x002CEE22
	public void SetTitle(string Name)
	{
		this.titleText.text = Name;
	}

	// Token: 0x060075A9 RID: 30121 RVA: 0x002D0C30 File Offset: 0x002CEE30
	public void SetSubText(string subtext, string tooltip = "")
	{
		this.subtextText.text = subtext;
		this.subtextText.GetComponent<ToolTip>().toolTip = tooltip;
	}

	// Token: 0x060075AA RID: 30122 RVA: 0x002D0C4F File Offset: 0x002CEE4F
	public void SetWarningActve(bool state)
	{
		this.WarningNotification.SetActive(state);
	}

	// Token: 0x060075AB RID: 30123 RVA: 0x002D0C5D File Offset: 0x002CEE5D
	public void SetWarning(Sprite icon, string label)
	{
		this.SetWarningActve(true);
		this.NotificationIcon.sprite = icon;
		this.NotificationText.text = label;
	}

	// Token: 0x060075AC RID: 30124 RVA: 0x002D0C7E File Offset: 0x002CEE7E
	public void SetPortrait(GameObject target)
	{
		this.portrait.SetPortrait(target);
	}

	// Token: 0x04005193 RID: 20883
	public LocText titleText;

	// Token: 0x04005194 RID: 20884
	public LocText subtextText;

	// Token: 0x04005195 RID: 20885
	public GameObject WarningNotification;

	// Token: 0x04005196 RID: 20886
	public Text NotificationText;

	// Token: 0x04005197 RID: 20887
	public Image NotificationIcon;

	// Token: 0x04005198 RID: 20888
	public Sprite techIcon;

	// Token: 0x04005199 RID: 20889
	public Sprite materialIcon;

	// Token: 0x0400519A RID: 20890
	public TitleBarPortrait portrait;

	// Token: 0x0400519B RID: 20891
	public bool userEditable;

	// Token: 0x0400519C RID: 20892
	public bool setCameraControllerState = true;
}
