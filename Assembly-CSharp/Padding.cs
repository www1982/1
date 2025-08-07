using System;

// Token: 0x02000457 RID: 1111
public readonly struct Padding
{
	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06001734 RID: 5940 RVA: 0x0008233D File Offset: 0x0008053D
	public float Width
	{
		get
		{
			return this.left + this.right;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06001735 RID: 5941 RVA: 0x0008234C File Offset: 0x0008054C
	public float Height
	{
		get
		{
			return this.top + this.bottom;
		}
	}

	// Token: 0x06001736 RID: 5942 RVA: 0x0008235B File Offset: 0x0008055B
	public Padding(float left, float right, float top, float bottom)
	{
		this.top = top;
		this.bottom = bottom;
		this.left = left;
		this.right = right;
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x0008237A File Offset: 0x0008057A
	public static Padding All(float padding)
	{
		return new Padding(padding, padding, padding, padding);
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x00082385 File Offset: 0x00080585
	public static Padding Symmetric(float horizontal, float vertical)
	{
		return new Padding(horizontal, horizontal, vertical, vertical);
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x00082390 File Offset: 0x00080590
	public static Padding Only(float left = 0f, float right = 0f, float top = 0f, float bottom = 0f)
	{
		return new Padding(left, right, top, bottom);
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x0008239B File Offset: 0x0008059B
	public static Padding Vertical(float vertical)
	{
		return new Padding(0f, 0f, vertical, vertical);
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x000823AE File Offset: 0x000805AE
	public static Padding Horizontal(float horizontal)
	{
		return new Padding(horizontal, horizontal, 0f, 0f);
	}

	// Token: 0x0600173C RID: 5948 RVA: 0x000823C1 File Offset: 0x000805C1
	public static Padding Top(float amount)
	{
		return new Padding(0f, 0f, amount, 0f);
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x000823D8 File Offset: 0x000805D8
	public static Padding Left(float amount)
	{
		return new Padding(amount, 0f, 0f, 0f);
	}

	// Token: 0x0600173E RID: 5950 RVA: 0x000823EF File Offset: 0x000805EF
	public static Padding Bottom(float amount)
	{
		return new Padding(0f, 0f, 0f, amount);
	}

	// Token: 0x0600173F RID: 5951 RVA: 0x00082406 File Offset: 0x00080606
	public static Padding Right(float amount)
	{
		return new Padding(0f, amount, 0f, 0f);
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x0008241D File Offset: 0x0008061D
	public static Padding operator +(Padding a, Padding b)
	{
		return new Padding(a.left + b.left, a.right + b.right, a.top + b.top, a.bottom + b.bottom);
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x00082458 File Offset: 0x00080658
	public static Padding operator -(Padding a, Padding b)
	{
		return new Padding(a.left - b.left, a.right - b.right, a.top - b.top, a.bottom - b.bottom);
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x00082493 File Offset: 0x00080693
	public static Padding operator *(float f, Padding p)
	{
		return p * f;
	}

	// Token: 0x06001743 RID: 5955 RVA: 0x0008249C File Offset: 0x0008069C
	public static Padding operator *(Padding p, float f)
	{
		return new Padding(p.left * f, p.right * f, p.top * f, p.bottom * f);
	}

	// Token: 0x06001744 RID: 5956 RVA: 0x000824C3 File Offset: 0x000806C3
	public static Padding operator /(Padding p, float f)
	{
		return new Padding(p.left / f, p.right / f, p.top / f, p.bottom / f);
	}

	// Token: 0x04000D93 RID: 3475
	public readonly float top;

	// Token: 0x04000D94 RID: 3476
	public readonly float bottom;

	// Token: 0x04000D95 RID: 3477
	public readonly float left;

	// Token: 0x04000D96 RID: 3478
	public readonly float right;
}
