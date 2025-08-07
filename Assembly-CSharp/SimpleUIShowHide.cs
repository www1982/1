using System;
using UnityEngine;

// Token: 0x02000E8B RID: 3723
[AddComponentMenu("KMonoBehaviour/scripts/SimpleUIShowHide")]
public class SimpleUIShowHide : KMonoBehaviour
{
	// Token: 0x0600769B RID: 30363 RVA: 0x002D62BC File Offset: 0x002D44BC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.toggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(this.OnClick));
		if (!this.saveStatePreferenceKey.IsNullOrWhiteSpace() && KPlayerPrefs.GetInt(this.saveStatePreferenceKey, 1) != 1 && this.toggle.CurrentState == 0)
		{
			this.OnClick();
		}
	}

	// Token: 0x0600769C RID: 30364 RVA: 0x002D6328 File Offset: 0x002D4528
	private void OnClick()
	{
		this.toggle.NextState();
		this.content.SetActive(this.toggle.CurrentState == 0);
		if (!this.saveStatePreferenceKey.IsNullOrWhiteSpace())
		{
			KPlayerPrefs.SetInt(this.saveStatePreferenceKey, (this.toggle.CurrentState == 0) ? 1 : 0);
		}
	}

	// Token: 0x04005290 RID: 21136
	[MyCmpReq]
	private MultiToggle toggle;

	// Token: 0x04005291 RID: 21137
	[SerializeField]
	public GameObject content;

	// Token: 0x04005292 RID: 21138
	[SerializeField]
	private string saveStatePreferenceKey;

	// Token: 0x04005293 RID: 21139
	private const int onState = 0;
}
