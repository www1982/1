using System;
using UnityEngine.UI;

// Token: 0x02000CA2 RID: 3234
public class ControlsScreen : KScreen
{
	// Token: 0x06006382 RID: 25474 RVA: 0x0025612C File Offset: 0x0025432C
	protected override void OnPrefabInit()
	{
		BindingEntry[] bindingEntries = GameInputMapping.GetBindingEntries();
		string text = "";
		foreach (BindingEntry bindingEntry in bindingEntries)
		{
			text += bindingEntry.mAction.ToString();
			text += ": ";
			text += bindingEntry.mKeyCode.ToString();
			text += "\n";
		}
		this.controlLabel.text = text;
	}

	// Token: 0x06006383 RID: 25475 RVA: 0x002561B1 File Offset: 0x002543B1
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Help) || e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
		}
	}

	// Token: 0x040043A8 RID: 17320
	public Text controlLabel;
}
