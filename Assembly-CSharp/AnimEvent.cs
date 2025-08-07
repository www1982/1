using System;
using UnityEngine;

// Token: 0x0200051B RID: 1307
[Serializable]
public class AnimEvent
{
	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0009839F File Offset: 0x0009659F
	// (set) Token: 0x06001C04 RID: 7172 RVA: 0x000983A7 File Offset: 0x000965A7
	[SerializeField]
	public string name { get; private set; }

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000983B0 File Offset: 0x000965B0
	// (set) Token: 0x06001C06 RID: 7174 RVA: 0x000983B8 File Offset: 0x000965B8
	[SerializeField]
	public string file { get; private set; }

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06001C07 RID: 7175 RVA: 0x000983C1 File Offset: 0x000965C1
	// (set) Token: 0x06001C08 RID: 7176 RVA: 0x000983C9 File Offset: 0x000965C9
	[SerializeField]
	public int frame { get; private set; }

	// Token: 0x06001C09 RID: 7177 RVA: 0x000983D2 File Offset: 0x000965D2
	public AnimEvent()
	{
	}

	// Token: 0x06001C0A RID: 7178 RVA: 0x000983DC File Offset: 0x000965DC
	public AnimEvent(string file, string name, int frame)
	{
		this.file = ((file == "") ? null : file);
		if (this.file != null)
		{
			this.fileHash = new KAnimHashedString(this.file);
		}
		this.name = name;
		this.frame = frame;
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x00098430 File Offset: 0x00096630
	public void Play(AnimEventManager.EventPlayerData behaviour)
	{
		if (this.IsFilteredOut(behaviour))
		{
			return;
		}
		if (behaviour.previousFrame < behaviour.currentFrame)
		{
			if (behaviour.previousFrame < this.frame && behaviour.currentFrame >= this.frame)
			{
				this.OnPlay(behaviour);
				return;
			}
		}
		else if (behaviour.previousFrame > behaviour.currentFrame && (behaviour.previousFrame < this.frame || this.frame <= behaviour.currentFrame))
		{
			this.OnPlay(behaviour);
		}
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x000984B2 File Offset: 0x000966B2
	private void DebugAnimEvent(string ev_name, AnimEventManager.EventPlayerData behaviour)
	{
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x000984B4 File Offset: 0x000966B4
	public virtual void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x000984B6 File Offset: 0x000966B6
	public virtual void OnUpdate(AnimEventManager.EventPlayerData behaviour)
	{
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x000984B8 File Offset: 0x000966B8
	public virtual void Stop(AnimEventManager.EventPlayerData behaviour)
	{
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x000984BA File Offset: 0x000966BA
	protected bool IsFilteredOut(AnimEventManager.EventPlayerData behaviour)
	{
		return this.file != null && !behaviour.controller.HasAnimationFile(this.fileHash);
	}

	// Token: 0x04001071 RID: 4209
	[SerializeField]
	private KAnimHashedString fileHash;

	// Token: 0x04001073 RID: 4211
	public bool OnExit;
}
