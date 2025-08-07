using System;
using UnityEngine;

// Token: 0x0200044E RID: 1102
public class Easing
{
	// Token: 0x04000D63 RID: 3427
	public const Easing.EasingFn PARAM_DEFAULT = null;

	// Token: 0x04000D64 RID: 3428
	public static readonly Easing.EasingFn Linear = (float x) => x;

	// Token: 0x04000D65 RID: 3429
	public static readonly Easing.EasingFn SmoothStep = (float x) => Mathf.SmoothStep(0f, 1f, x);

	// Token: 0x04000D66 RID: 3430
	public static readonly Easing.EasingFn QuadIn = (float x) => x * x;

	// Token: 0x04000D67 RID: 3431
	public static readonly Easing.EasingFn QuadOut = (float x) => 1f - (1f - x) * (1f - x);

	// Token: 0x04000D68 RID: 3432
	public static readonly Easing.EasingFn QuadInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return 1f - Mathf.Pow(-2f * x + 2f, 2f) / 2f;
		}
		return 2f * x * x;
	};

	// Token: 0x04000D69 RID: 3433
	public static readonly Easing.EasingFn CubicIn = (float x) => x * x * x;

	// Token: 0x04000D6A RID: 3434
	public static readonly Easing.EasingFn CubicOut = (float x) => 1f - Mathf.Pow(1f - x, 3f);

	// Token: 0x04000D6B RID: 3435
	public static readonly Easing.EasingFn CubicInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
		}
		return 4f * x * x * x;
	};

	// Token: 0x04000D6C RID: 3436
	public static readonly Easing.EasingFn QuartIn = (float x) => x * x * x * x;

	// Token: 0x04000D6D RID: 3437
	public static readonly Easing.EasingFn QuartOut = (float x) => 1f - Mathf.Pow(1f - x, 4f);

	// Token: 0x04000D6E RID: 3438
	public static readonly Easing.EasingFn QuartInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return 1f - Mathf.Pow(-2f * x + 2f, 4f) / 2f;
		}
		return 8f * x * x * x * x;
	};

	// Token: 0x04000D6F RID: 3439
	public static readonly Easing.EasingFn QuintIn = (float x) => x * x * x * x * x;

	// Token: 0x04000D70 RID: 3440
	public static readonly Easing.EasingFn QuintOut = (float x) => 1f - Mathf.Pow(1f - x, 5f);

	// Token: 0x04000D71 RID: 3441
	public static readonly Easing.EasingFn QuintInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return 1f - Mathf.Pow(-2f * x + 2f, 5f) / 2f;
		}
		return 16f * x * x * x * x * x;
	};

	// Token: 0x04000D72 RID: 3442
	public static readonly Easing.EasingFn ExpoIn = delegate(float x)
	{
		if (x != 0f)
		{
			return Mathf.Pow(2f, 10f * x - 10f);
		}
		return 0f;
	};

	// Token: 0x04000D73 RID: 3443
	public static readonly Easing.EasingFn ExpoOut = delegate(float x)
	{
		if (x != 1f)
		{
			return 1f - Mathf.Pow(2f, -10f * x);
		}
		return 1f;
	};

	// Token: 0x04000D74 RID: 3444
	public static readonly Easing.EasingFn ExpoInOut = delegate(float x)
	{
		if (x == 0f)
		{
			return 0f;
		}
		if (x == 1f)
		{
			return 1f;
		}
		if ((double)x >= 0.5)
		{
			return (2f - Mathf.Pow(2f, -20f * x + 10f)) / 2f;
		}
		return Mathf.Pow(2f, 20f * x - 10f) / 2f;
	};

	// Token: 0x04000D75 RID: 3445
	public static readonly Easing.EasingFn SineIn = (float x) => 1f - Mathf.Cos(x * 3.1415927f / 2f);

	// Token: 0x04000D76 RID: 3446
	public static readonly Easing.EasingFn SineOut = (float x) => Mathf.Sin(x * 3.1415927f / 2f);

	// Token: 0x04000D77 RID: 3447
	public static readonly Easing.EasingFn SineInOut = (float x) => -(Mathf.Cos(3.1415927f * x) - 1f) / 2f;

	// Token: 0x04000D78 RID: 3448
	public static readonly Easing.EasingFn CircIn = (float x) => 1f - Mathf.Sqrt(1f - Mathf.Pow(x, 2f));

	// Token: 0x04000D79 RID: 3449
	public static readonly Easing.EasingFn CircOut = (float x) => Mathf.Sqrt(1f - Mathf.Pow(x - 1f, 2f));

	// Token: 0x04000D7A RID: 3450
	public static readonly Easing.EasingFn CircInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return (Mathf.Sqrt(1f - Mathf.Pow(-2f * x + 2f, 2f)) + 1f) / 2f;
		}
		return (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * x, 2f))) / 2f;
	};

	// Token: 0x04000D7B RID: 3451
	public static readonly Easing.EasingFn EaseOutBack = (float x) => 1f + 2.70158f * Mathf.Pow(x - 1f, 3f) + 1.70158f * Mathf.Pow(x - 1f, 2f);

	// Token: 0x04000D7C RID: 3452
	public static readonly Easing.EasingFn ElasticIn = delegate(float x)
	{
		if (x == 0f)
		{
			return 0f;
		}
		if (x != 1f)
		{
			return -Mathf.Pow(2f, 10f * x - 10f) * Mathf.Sin((x * 10f - 10.75f) * 2.0943952f);
		}
		return 1f;
	};

	// Token: 0x04000D7D RID: 3453
	public static readonly Easing.EasingFn ElasticOut = delegate(float x)
	{
		if (x == 0f)
		{
			return 0f;
		}
		if (x != 1f)
		{
			return Mathf.Pow(2f, -10f * x) * Mathf.Sin((x * 10f - 0.75f) * 2.0943952f) + 1f;
		}
		return 1f;
	};

	// Token: 0x04000D7E RID: 3454
	public static readonly Easing.EasingFn ElasticInOut = delegate(float x)
	{
		if (x == 0f)
		{
			return 0f;
		}
		if (x == 1f)
		{
			return 1f;
		}
		if ((double)x >= 0.5)
		{
			return Mathf.Pow(2f, -20f * x + 10f) * Mathf.Sin((20f * x - 11.125f) * 1.3962635f) / 2f + 1f;
		}
		return -(Mathf.Pow(2f, 20f * x - 10f) * Mathf.Sin((20f * x - 11.125f) * 1.3962635f)) / 2f;
	};

	// Token: 0x04000D7F RID: 3455
	public static readonly Easing.EasingFn BackIn = (float x) => 2.70158f * x * x * x - 1.70158f * x * x;

	// Token: 0x04000D80 RID: 3456
	public static readonly Easing.EasingFn BackOut = (float x) => 1f + 2.70158f * Mathf.Pow(x - 1f, 3f) + 1.70158f * Mathf.Pow(x - 1f, 2f);

	// Token: 0x04000D81 RID: 3457
	public static readonly Easing.EasingFn BackInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return (Mathf.Pow(2f * x - 2f, 2f) * (3.5949094f * (x * 2f - 2f) + 2.5949094f) + 2f) / 2f;
		}
		return Mathf.Pow(2f * x, 2f) * (7.189819f * x - 2.5949094f) / 2f;
	};

	// Token: 0x04000D82 RID: 3458
	public static readonly Easing.EasingFn BounceIn = (float x) => 1f - Easing.BounceOut(1f - x);

	// Token: 0x04000D83 RID: 3459
	public static readonly Easing.EasingFn BounceOut = delegate(float x)
	{
		if (x < 0.36363637f)
		{
			return 7.5625f * x * x;
		}
		if (x < 0.72727275f)
		{
			return 7.5625f * (x -= 0.54545456f) * x + 0.75f;
		}
		if ((double)x < 0.9090909090909091)
		{
			return 7.5625f * (x -= 0.8181818f) * x + 0.9375f;
		}
		return 7.5625f * (x -= 0.95454544f) * x + 0.984375f;
	};

	// Token: 0x04000D84 RID: 3460
	public static readonly Easing.EasingFn BounceInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return (1f + Easing.BounceOut(2f * x - 1f)) / 2f;
		}
		return (1f - Easing.BounceOut(1f - 2f * x)) / 2f;
	};

	// Token: 0x02001228 RID: 4648
	// (Invoke) Token: 0x06008533 RID: 34099
	public delegate float EasingFn(float f);
}
