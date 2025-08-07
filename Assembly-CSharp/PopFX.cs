using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D9B RID: 3483
[AddComponentMenu("KMonoBehaviour/scripts/PopFX")]
public class PopFX : KMonoBehaviour
{
	// Token: 0x06006D0A RID: 27914 RVA: 0x00294698 File Offset: 0x00292898
	public void Recycle()
	{
		this.icon = null;
		this.text = "";
		this.targetTransform = null;
		this.lifeElapsed = 0f;
		this.trackTarget = false;
		this.startPos = Vector3.zero;
		this.IconDisplay.color = Color.white;
		this.TextDisplay.color = Color.white;
		PopFXManager.Instance.RecycleFX(this);
		this.canvasGroup.alpha = 0f;
		base.gameObject.SetActive(false);
		this.isLive = false;
		this.isActiveWorld = false;
		Game.Instance.Unsubscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
	}

	// Token: 0x06006D0B RID: 27915 RVA: 0x0029474C File Offset: 0x0029294C
	public void Spawn(Sprite Icon, string Text, Transform TargetTransform, Vector3 Offset, float LifeTime = 1.5f, bool TrackTarget = false)
	{
		this.icon = Icon;
		this.text = Text;
		this.targetTransform = TargetTransform;
		this.trackTarget = TrackTarget;
		this.lifetime = LifeTime;
		this.offset = Offset;
		if (this.targetTransform != null)
		{
			this.startPos = this.targetTransform.GetPosition();
			int num;
			int num2;
			Grid.PosToXY(this.startPos, out num, out num2);
			if (num2 % 2 != 0)
			{
				this.startPos.x = this.startPos.x + 0.5f;
			}
		}
		this.TextDisplay.text = this.text;
		this.IconDisplay.sprite = this.icon;
		this.canvasGroup.alpha = 1f;
		this.isLive = true;
		Game.Instance.Subscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
		this.SetWorldActive(ClusterManager.Instance.activeWorldId);
		this.Update();
	}

	// Token: 0x06006D0C RID: 27916 RVA: 0x00294838 File Offset: 0x00292A38
	private void OnActiveWorldChanged(object data)
	{
		global::Tuple<int, int> tuple = (global::Tuple<int, int>)data;
		if (this.isLive)
		{
			this.SetWorldActive(tuple.first);
		}
	}

	// Token: 0x06006D0D RID: 27917 RVA: 0x00294860 File Offset: 0x00292A60
	private void SetWorldActive(int worldId)
	{
		int num = Grid.PosToCell((this.trackTarget && this.targetTransform != null) ? this.targetTransform.position : (this.startPos + this.offset));
		this.isActiveWorld = !Grid.IsValidCell(num) || (int)Grid.WorldIdx[num] == worldId;
	}

	// Token: 0x06006D0E RID: 27918 RVA: 0x002948C4 File Offset: 0x00292AC4
	private void Update()
	{
		if (!this.isLive)
		{
			return;
		}
		if (!PopFXManager.Instance.Ready())
		{
			return;
		}
		this.lifeElapsed += Time.unscaledDeltaTime;
		if (this.lifeElapsed >= this.lifetime)
		{
			this.Recycle();
		}
		if (this.trackTarget && this.targetTransform != null)
		{
			Vector3 vector = PopFXManager.Instance.WorldToScreen(this.targetTransform.GetPosition() + this.offset + Vector3.up * this.lifeElapsed * (this.Speed * this.lifeElapsed));
			vector.z = 0f;
			base.gameObject.rectTransform().anchoredPosition = vector;
		}
		else
		{
			Vector3 vector2 = PopFXManager.Instance.WorldToScreen(this.startPos + this.offset + Vector3.up * this.lifeElapsed * (this.Speed * (this.lifeElapsed / 2f)));
			vector2.z = 0f;
			base.gameObject.rectTransform().anchoredPosition = vector2;
		}
		this.canvasGroup.alpha = (this.isActiveWorld ? (1.5f * ((this.lifetime - this.lifeElapsed) / this.lifetime)) : 0f);
	}

	// Token: 0x04004A60 RID: 19040
	private float Speed = 2f;

	// Token: 0x04004A61 RID: 19041
	private Sprite icon;

	// Token: 0x04004A62 RID: 19042
	private string text;

	// Token: 0x04004A63 RID: 19043
	private Transform targetTransform;

	// Token: 0x04004A64 RID: 19044
	private Vector3 offset;

	// Token: 0x04004A65 RID: 19045
	public Image IconDisplay;

	// Token: 0x04004A66 RID: 19046
	public LocText TextDisplay;

	// Token: 0x04004A67 RID: 19047
	public CanvasGroup canvasGroup;

	// Token: 0x04004A68 RID: 19048
	private Camera uiCamera;

	// Token: 0x04004A69 RID: 19049
	private float lifetime;

	// Token: 0x04004A6A RID: 19050
	private float lifeElapsed;

	// Token: 0x04004A6B RID: 19051
	private bool trackTarget;

	// Token: 0x04004A6C RID: 19052
	private Vector3 startPos;

	// Token: 0x04004A6D RID: 19053
	private bool isLive;

	// Token: 0x04004A6E RID: 19054
	private bool isActiveWorld;
}
