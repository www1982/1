using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000D50 RID: 3408
public class GenericMessage : Message
{
	// Token: 0x060069AF RID: 27055 RVA: 0x0027FB7A File Offset: 0x0027DD7A
	public GenericMessage(string _title, string _body, string _tooltip, KMonoBehaviour click_focus = null)
	{
		this.title = _title;
		this.body = _body;
		this.tooltip = _tooltip;
		this.clickFocus.Set(click_focus);
	}

	// Token: 0x060069B0 RID: 27056 RVA: 0x0027FBAF File Offset: 0x0027DDAF
	public GenericMessage()
	{
	}

	// Token: 0x060069B1 RID: 27057 RVA: 0x0027FBC2 File Offset: 0x0027DDC2
	public override string GetSound()
	{
		return null;
	}

	// Token: 0x060069B2 RID: 27058 RVA: 0x0027FBC5 File Offset: 0x0027DDC5
	public override string GetMessageBody()
	{
		return this.body;
	}

	// Token: 0x060069B3 RID: 27059 RVA: 0x0027FBCD File Offset: 0x0027DDCD
	public override string GetTooltip()
	{
		return this.tooltip;
	}

	// Token: 0x060069B4 RID: 27060 RVA: 0x0027FBD5 File Offset: 0x0027DDD5
	public override string GetTitle()
	{
		return this.title;
	}

	// Token: 0x060069B5 RID: 27061 RVA: 0x0027FBE0 File Offset: 0x0027DDE0
	public override void OnClick()
	{
		KMonoBehaviour kmonoBehaviour = this.clickFocus.Get();
		if (kmonoBehaviour == null)
		{
			return;
		}
		Transform transform = kmonoBehaviour.transform;
		if (transform == null)
		{
			return;
		}
		Vector3 position = transform.GetPosition();
		position.z = -40f;
		CameraController.Instance.SetTargetPos(position, 8f, true);
		if (transform.GetComponent<KSelectable>() != null)
		{
			SelectTool.Instance.Select(transform.GetComponent<KSelectable>(), false);
		}
	}

	// Token: 0x0400483E RID: 18494
	[Serialize]
	private string title;

	// Token: 0x0400483F RID: 18495
	[Serialize]
	private string tooltip;

	// Token: 0x04004840 RID: 18496
	[Serialize]
	private string body;

	// Token: 0x04004841 RID: 18497
	[Serialize]
	private Ref<KMonoBehaviour> clickFocus = new Ref<KMonoBehaviour>();
}
