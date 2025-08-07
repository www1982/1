using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000D56 RID: 3414
[AddComponentMenu("KMonoBehaviour/scripts/Messenger")]
public class Messenger : KMonoBehaviour
{
	// Token: 0x17000781 RID: 1921
	// (get) Token: 0x060069DF RID: 27103 RVA: 0x00280131 File Offset: 0x0027E331
	public int Count
	{
		get
		{
			return this.messages.Count;
		}
	}

	// Token: 0x060069E0 RID: 27104 RVA: 0x0028013E File Offset: 0x0027E33E
	public IEnumerator<Message> GetEnumerator()
	{
		return this.messages.GetEnumerator();
	}

	// Token: 0x060069E1 RID: 27105 RVA: 0x0028014B File Offset: 0x0027E34B
	public static void DestroyInstance()
	{
		Messenger.Instance = null;
	}

	// Token: 0x17000782 RID: 1922
	// (get) Token: 0x060069E2 RID: 27106 RVA: 0x00280153 File Offset: 0x0027E353
	public SerializedList<Message> Messages
	{
		get
		{
			return this.messages;
		}
	}

	// Token: 0x060069E3 RID: 27107 RVA: 0x0028015B File Offset: 0x0027E35B
	protected override void OnPrefabInit()
	{
		Messenger.Instance = this;
	}

	// Token: 0x060069E4 RID: 27108 RVA: 0x00280164 File Offset: 0x0027E364
	protected override void OnSpawn()
	{
		int i = 0;
		while (i < this.messages.Count)
		{
			if (this.messages[i].IsValid())
			{
				i++;
			}
			else
			{
				this.messages.RemoveAt(i);
			}
		}
		base.Trigger(-599791736, null);
	}

	// Token: 0x060069E5 RID: 27109 RVA: 0x002801B4 File Offset: 0x0027E3B4
	public void QueueMessage(Message message)
	{
		this.messages.Add(message);
		base.Trigger(1558809273, message);
	}

	// Token: 0x060069E6 RID: 27110 RVA: 0x002801D0 File Offset: 0x0027E3D0
	public Message DequeueMessage()
	{
		Message message = null;
		if (this.messages.Count > 0)
		{
			message = this.messages[0];
			this.messages.RemoveAt(0);
		}
		return message;
	}

	// Token: 0x060069E7 RID: 27111 RVA: 0x00280208 File Offset: 0x0027E408
	public void ClearAllMessages()
	{
		for (int i = this.messages.Count - 1; i >= 0; i--)
		{
			this.messages.RemoveAt(i);
		}
	}

	// Token: 0x060069E8 RID: 27112 RVA: 0x00280239 File Offset: 0x0027E439
	public void RemoveMessage(Message m)
	{
		this.messages.Remove(m);
		base.Trigger(-599791736, null);
	}

	// Token: 0x0400484D RID: 18509
	[Serialize]
	private SerializedList<Message> messages = new SerializedList<Message>();

	// Token: 0x0400484E RID: 18510
	public static Messenger Instance;
}
