using System;
using UnityEngine;

// Token: 0x02000D54 RID: 3412
public class MessageDialogFrame : KScreen
{
	// Token: 0x060069CF RID: 27087 RVA: 0x0027FD59 File Offset: 0x0027DF59
	public override float GetSortKey()
	{
		return 15f;
	}

	// Token: 0x060069D0 RID: 27088 RVA: 0x0027FD60 File Offset: 0x0027DF60
	protected override void OnActivate()
	{
		this.closeButton.onClick += this.OnClickClose;
		this.nextMessageButton.onClick += this.OnClickNextMessage;
		MultiToggle multiToggle = this.dontShowAgainButton;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(this.OnClickDontShowAgain));
		bool flag = KPlayerPrefs.GetInt("HideTutorial_CheckState", 0) == 1;
		this.dontShowAgainButton.ChangeState(flag ? 0 : 1);
		base.Subscribe(Messenger.Instance.gameObject, -599791736, new Action<object>(this.OnMessagesChanged));
		this.OnMessagesChanged(null);
	}

	// Token: 0x060069D1 RID: 27089 RVA: 0x0027FE0C File Offset: 0x0027E00C
	protected override void OnDeactivate()
	{
		base.Unsubscribe(Messenger.Instance.gameObject, -599791736, new Action<object>(this.OnMessagesChanged));
	}

	// Token: 0x060069D2 RID: 27090 RVA: 0x0027FE2F File Offset: 0x0027E02F
	private void OnClickClose()
	{
		this.TryDontShowAgain();
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060069D3 RID: 27091 RVA: 0x0027FE42 File Offset: 0x0027E042
	private void OnClickNextMessage()
	{
		this.TryDontShowAgain();
		global::UnityEngine.Object.Destroy(base.gameObject);
		NotificationScreen.Instance.OnClickNextMessage();
	}

	// Token: 0x060069D4 RID: 27092 RVA: 0x0027FE60 File Offset: 0x0027E060
	private void OnClickDontShowAgain()
	{
		this.dontShowAgainButton.NextState();
		bool flag = this.dontShowAgainButton.CurrentState == 0;
		KPlayerPrefs.SetInt("HideTutorial_CheckState", flag ? 1 : 0);
	}

	// Token: 0x060069D5 RID: 27093 RVA: 0x0027FE98 File Offset: 0x0027E098
	private void OnMessagesChanged(object data)
	{
		this.nextMessageButton.gameObject.SetActive(Messenger.Instance.Count != 0);
	}

	// Token: 0x060069D6 RID: 27094 RVA: 0x0027FEB8 File Offset: 0x0027E0B8
	public void SetMessage(MessageDialog dialog, Message message)
	{
		this.title.text = message.GetTitle().ToUpper();
		dialog.GetComponent<RectTransform>().SetParent(this.body.GetComponent<RectTransform>());
		RectTransform component = dialog.GetComponent<RectTransform>();
		component.offsetMin = Vector2.zero;
		component.offsetMax = Vector2.zero;
		dialog.transform.SetLocalPosition(Vector3.zero);
		dialog.SetMessage(message);
		dialog.OnClickAction();
		if (dialog.CanDontShowAgain)
		{
			this.dontShowAgainElement.SetActive(true);
			this.dontShowAgainDelegate = new global::System.Action(dialog.OnDontShowAgain);
			return;
		}
		this.dontShowAgainElement.SetActive(false);
		this.dontShowAgainDelegate = null;
	}

	// Token: 0x060069D7 RID: 27095 RVA: 0x0027FF65 File Offset: 0x0027E165
	private void TryDontShowAgain()
	{
		if (this.dontShowAgainDelegate != null && this.dontShowAgainButton.CurrentState == 0)
		{
			this.dontShowAgainDelegate();
		}
	}

	// Token: 0x04004843 RID: 18499
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04004844 RID: 18500
	[SerializeField]
	private KToggle nextMessageButton;

	// Token: 0x04004845 RID: 18501
	[SerializeField]
	private GameObject dontShowAgainElement;

	// Token: 0x04004846 RID: 18502
	[SerializeField]
	private MultiToggle dontShowAgainButton;

	// Token: 0x04004847 RID: 18503
	[SerializeField]
	private LocText title;

	// Token: 0x04004848 RID: 18504
	[SerializeField]
	private RectTransform body;

	// Token: 0x04004849 RID: 18505
	private global::System.Action dontShowAgainDelegate;
}
