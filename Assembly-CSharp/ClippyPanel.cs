using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E80 RID: 3712
public class ClippyPanel : KScreen
{
	// Token: 0x06007659 RID: 30297 RVA: 0x002D45F4 File Offset: 0x002D27F4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600765A RID: 30298 RVA: 0x002D45FC File Offset: 0x002D27FC
	protected override void OnActivate()
	{
		base.OnActivate();
		SpeedControlScreen.Instance.Pause(true, false);
		Game.Instance.Trigger(1634669191, null);
	}

	// Token: 0x0600765B RID: 30299 RVA: 0x002D4620 File Offset: 0x002D2820
	public void OnOk()
	{
		SpeedControlScreen.Instance.Unpause(true);
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400522C RID: 21036
	public Text title;

	// Token: 0x0400522D RID: 21037
	public Text detailText;

	// Token: 0x0400522E RID: 21038
	public Text flavorText;

	// Token: 0x0400522F RID: 21039
	public Image topicIcon;

	// Token: 0x04005230 RID: 21040
	private KButton okButton;
}
