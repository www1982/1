using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020005EF RID: 1519
public class Notification
{
	// Token: 0x17000176 RID: 374
	// (get) Token: 0x0600238D RID: 9101 RVA: 0x000CB607 File Offset: 0x000C9807
	// (set) Token: 0x0600238E RID: 9102 RVA: 0x000CB60F File Offset: 0x000C980F
	public NotificationType Type { get; set; }

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x0600238F RID: 9103 RVA: 0x000CB618 File Offset: 0x000C9818
	// (set) Token: 0x06002390 RID: 9104 RVA: 0x000CB620 File Offset: 0x000C9820
	public Notifier Notifier { get; set; }

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06002391 RID: 9105 RVA: 0x000CB629 File Offset: 0x000C9829
	// (set) Token: 0x06002392 RID: 9106 RVA: 0x000CB631 File Offset: 0x000C9831
	public Transform clickFocus { get; set; }

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x06002393 RID: 9107 RVA: 0x000CB63A File Offset: 0x000C983A
	// (set) Token: 0x06002394 RID: 9108 RVA: 0x000CB642 File Offset: 0x000C9842
	public float Time { get; set; }

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06002395 RID: 9109 RVA: 0x000CB64B File Offset: 0x000C984B
	// (set) Token: 0x06002396 RID: 9110 RVA: 0x000CB653 File Offset: 0x000C9853
	public float GameTime { get; set; }

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06002397 RID: 9111 RVA: 0x000CB65C File Offset: 0x000C985C
	// (set) Token: 0x06002398 RID: 9112 RVA: 0x000CB664 File Offset: 0x000C9864
	public float Delay { get; set; }

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06002399 RID: 9113 RVA: 0x000CB66D File Offset: 0x000C986D
	// (set) Token: 0x0600239A RID: 9114 RVA: 0x000CB675 File Offset: 0x000C9875
	public int Idx { get; set; }

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x0600239B RID: 9115 RVA: 0x000CB67E File Offset: 0x000C987E
	// (set) Token: 0x0600239C RID: 9116 RVA: 0x000CB686 File Offset: 0x000C9886
	public Func<List<Notification>, object, string> ToolTip { get; set; }

	// Token: 0x0600239D RID: 9117 RVA: 0x000CB68F File Offset: 0x000C988F
	public bool IsReady()
	{
		return global::UnityEngine.Time.time >= this.GameTime + this.Delay;
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x0600239E RID: 9118 RVA: 0x000CB6A8 File Offset: 0x000C98A8
	// (set) Token: 0x0600239F RID: 9119 RVA: 0x000CB6B0 File Offset: 0x000C98B0
	public string titleText { get; private set; }

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x060023A0 RID: 9120 RVA: 0x000CB6B9 File Offset: 0x000C98B9
	// (set) Token: 0x060023A1 RID: 9121 RVA: 0x000CB6C1 File Offset: 0x000C98C1
	public string NotifierName
	{
		get
		{
			return this.notifierName;
		}
		set
		{
			this.notifierName = value;
			this.titleText = this.ReplaceTags(this.titleText);
		}
	}

	// Token: 0x060023A2 RID: 9122 RVA: 0x000CB6DC File Offset: 0x000C98DC
	public Notification(string title, NotificationType type, Func<List<Notification>, object, string> tooltip = null, object tooltip_data = null, bool expires = true, float delay = 0f, Notification.ClickCallback custom_click_callback = null, object custom_click_data = null, Transform click_focus = null, bool volume_attenuation = true, bool clear_on_click = false, bool show_dismiss_button = false)
	{
		this.titleText = title;
		this.Type = type;
		this.ToolTip = tooltip;
		this.tooltipData = tooltip_data;
		this.expires = expires;
		this.Delay = delay;
		this.customClickCallback = custom_click_callback;
		this.customClickData = custom_click_data;
		this.clickFocus = click_focus;
		this.volume_attenuation = volume_attenuation;
		this.clearOnClick = clear_on_click;
		this.showDismissButton = show_dismiss_button;
		int num = this.notificationIncrement;
		this.notificationIncrement = num + 1;
		this.Idx = num;
	}

	// Token: 0x060023A3 RID: 9123 RVA: 0x000CB778 File Offset: 0x000C9978
	public void Clear()
	{
		if (this.Notifier != null)
		{
			this.Notifier.Remove(this);
			return;
		}
		NotificationManager.Instance.RemoveNotification(this);
	}

	// Token: 0x060023A4 RID: 9124 RVA: 0x000CB7A0 File Offset: 0x000C99A0
	private string ReplaceTags(string text)
	{
		DebugUtil.Assert(text != null);
		int num = text.IndexOf('{');
		int num2 = text.IndexOf('}');
		if (0 <= num && num < num2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num3 = 0;
			while (0 <= num)
			{
				string text2 = text.Substring(num3, num - num3);
				stringBuilder.Append(text2);
				num2 = text.IndexOf('}', num);
				if (num >= num2)
				{
					break;
				}
				string text3 = text.Substring(num + 1, num2 - num - 1);
				string tagDescription = this.GetTagDescription(text3);
				stringBuilder.Append(tagDescription);
				num3 = num2 + 1;
				num = text.IndexOf('{', num2);
			}
			stringBuilder.Append(text.Substring(num3, text.Length - num3));
			return stringBuilder.ToString();
		}
		return text;
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x000CB854 File Offset: 0x000C9A54
	private string GetTagDescription(string tag)
	{
		string text;
		if (tag == "NotifierName")
		{
			text = this.notifierName;
		}
		else
		{
			text = "UNKNOWN TAG: " + tag;
		}
		return text;
	}

	// Token: 0x040014BA RID: 5306
	public object tooltipData;

	// Token: 0x040014BB RID: 5307
	public bool expires = true;

	// Token: 0x040014BC RID: 5308
	public bool playSound = true;

	// Token: 0x040014BD RID: 5309
	public bool volume_attenuation = true;

	// Token: 0x040014BE RID: 5310
	public Notification.ClickCallback customClickCallback;

	// Token: 0x040014BF RID: 5311
	public bool clearOnClick;

	// Token: 0x040014C0 RID: 5312
	public bool showDismissButton;

	// Token: 0x040014C1 RID: 5313
	public object customClickData;

	// Token: 0x040014C2 RID: 5314
	public string customNotificationID;

	// Token: 0x040014C3 RID: 5315
	private int notificationIncrement;

	// Token: 0x040014C5 RID: 5317
	private string notifierName;

	// Token: 0x0200147C RID: 5244
	// (Invoke) Token: 0x06008DE8 RID: 36328
	public delegate void ClickCallback(object data);
}
