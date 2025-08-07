using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000007 RID: 7
[AddComponentMenu("KMonoBehaviour/scripts/UICurvePath")]
public class UICurvePath : KMonoBehaviour
{
	// Token: 0x06000019 RID: 25 RVA: 0x000024B0 File Offset: 0x000006B0
	protected override void OnSpawn()
	{
		this.Init();
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(this.OnResize));
		this.OnResize();
		this.startDelay = (float)global::UnityEngine.Random.Range(0, 8);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002500 File Offset: 0x00000700
	private void OnResize()
	{
		this.A = this.startPoint.position;
		this.B = this.controlPointStart.position;
		this.C = this.controlPointEnd.position;
		this.D = this.endPoint.position;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002551 File Offset: 0x00000751
	protected override void OnCleanUp()
	{
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (global::System.Action)Delegate.Remove(instance.OnResize, new global::System.Action(this.OnResize));
		base.OnCleanUp();
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002580 File Offset: 0x00000780
	private void Update()
	{
		this.startDelay -= Time.unscaledDeltaTime;
		this.sprite.gameObject.SetActive(this.startDelay < 0f);
		if (this.startDelay > 0f)
		{
			return;
		}
		this.tick += Time.unscaledDeltaTime * this.moveSpeed;
		this.sprite.transform.position = this.DeCasteljausAlgorithm(this.tick);
		this.sprite.SetAlpha(Mathf.Min(this.sprite.color.a + this.tick / 2f, 1f));
		if (this.animateScale)
		{
			float num = Mathf.Min(this.sprite.transform.localScale.x + Time.unscaledDeltaTime * this.moveSpeed, 1f);
			this.sprite.transform.localScale = new Vector3(num, num, 1f);
		}
		if (this.loop && this.tick > 1f)
		{
			this.Init();
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000026A0 File Offset: 0x000008A0
	private void Init()
	{
		this.sprite.transform.position = this.startPoint.position;
		this.tick = 0f;
		if (this.animateScale)
		{
			this.sprite.transform.localScale = this.initialScale;
		}
		this.sprite.SetAlpha(this.initialAlpha);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002704 File Offset: 0x00000904
	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
		{
			this.A = this.startPoint.position;
			this.B = this.controlPointStart.position;
			this.C = this.controlPointEnd.position;
			this.D = this.endPoint.position;
		}
		Gizmos.color = Color.white;
		Vector3 a = this.A;
		float num = 0.02f;
		int num2 = Mathf.FloorToInt(1f / num);
		for (int i = 1; i <= num2; i++)
		{
			float num3 = (float)i * num;
			this.DeCasteljausAlgorithm(num3);
		}
		Gizmos.color = Color.green;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000027A4 File Offset: 0x000009A4
	private Vector3 DeCasteljausAlgorithm(float t)
	{
		float num = 1f - t;
		Vector3 vector = num * this.A + t * this.B;
		Vector3 vector2 = num * this.B + t * this.C;
		Vector3 vector3 = num * this.C + t * this.D;
		Vector3 vector4 = num * vector + t * vector2;
		Vector3 vector5 = num * vector2 + t * vector3;
		return num * vector4 + t * vector5;
	}

	// Token: 0x04000015 RID: 21
	public Transform startPoint;

	// Token: 0x04000016 RID: 22
	public Transform endPoint;

	// Token: 0x04000017 RID: 23
	public Transform controlPointStart;

	// Token: 0x04000018 RID: 24
	public Transform controlPointEnd;

	// Token: 0x04000019 RID: 25
	public Image sprite;

	// Token: 0x0400001A RID: 26
	public bool loop = true;

	// Token: 0x0400001B RID: 27
	public bool animateScale;

	// Token: 0x0400001C RID: 28
	public Vector3 initialScale;

	// Token: 0x0400001D RID: 29
	private float startDelay;

	// Token: 0x0400001E RID: 30
	public float initialAlpha = 0.5f;

	// Token: 0x0400001F RID: 31
	public float moveSpeed = 0.1f;

	// Token: 0x04000020 RID: 32
	private float tick;

	// Token: 0x04000021 RID: 33
	private Vector3 A;

	// Token: 0x04000022 RID: 34
	private Vector3 B;

	// Token: 0x04000023 RID: 35
	private Vector3 C;

	// Token: 0x04000024 RID: 36
	private Vector3 D;
}
