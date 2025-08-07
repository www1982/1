using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CF6 RID: 3318
[AddComponentMenu("KMonoBehaviour/scripts/KCanvasScaler")]
public class KCanvasScaler : KMonoBehaviour
{
	// Token: 0x06006631 RID: 26161 RVA: 0x0026981C File Offset: 0x00267A1C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (KPlayerPrefs.HasKey(KCanvasScaler.UIScalePrefKey))
		{
			this.SetUserScale(KPlayerPrefs.GetFloat(KCanvasScaler.UIScalePrefKey) / 100f);
		}
		else
		{
			this.SetUserScale(1f);
		}
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(this.OnResize));
	}

	// Token: 0x06006632 RID: 26162 RVA: 0x00269884 File Offset: 0x00267A84
	private void OnResize()
	{
		this.SetUserScale(this.userScale);
	}

	// Token: 0x06006633 RID: 26163 RVA: 0x00269892 File Offset: 0x00267A92
	public void SetUserScale(float scale)
	{
		if (this.canvasScaler == null)
		{
			this.canvasScaler = base.GetComponent<CanvasScaler>();
		}
		this.userScale = scale;
		this.canvasScaler.scaleFactor = this.GetCanvasScale();
	}

	// Token: 0x06006634 RID: 26164 RVA: 0x002698C6 File Offset: 0x00267AC6
	public float GetUserScale()
	{
		return this.userScale;
	}

	// Token: 0x06006635 RID: 26165 RVA: 0x002698CE File Offset: 0x00267ACE
	public float GetCanvasScale()
	{
		return this.userScale * this.ScreenRelativeScale();
	}

	// Token: 0x06006636 RID: 26166 RVA: 0x002698E0 File Offset: 0x00267AE0
	private float ScreenRelativeScale()
	{
		float dpi = Screen.dpi;
		Camera camera = Camera.main;
		if (camera == null)
		{
			camera = global::UnityEngine.Object.FindObjectOfType<Camera>();
		}
		camera != null;
		float num = (float)Screen.width / (float)Screen.height;
		if ((float)Screen.height <= this.scaleSteps[0].maxRes_y || num < 1.6f)
		{
			return this.scaleSteps[0].scale;
		}
		if ((float)Screen.height > this.scaleSteps[this.scaleSteps.Length - 1].maxRes_y)
		{
			return this.scaleSteps[this.scaleSteps.Length - 1].scale;
		}
		for (int i = 0; i < this.scaleSteps.Length; i++)
		{
			if ((float)Screen.height > this.scaleSteps[i].maxRes_y && (float)Screen.height <= this.scaleSteps[i + 1].maxRes_y)
			{
				float num2 = ((float)Screen.height - this.scaleSteps[i].maxRes_y) / (this.scaleSteps[i + 1].maxRes_y - this.scaleSteps[i].maxRes_y);
				return Mathf.Lerp(this.scaleSteps[i].scale, this.scaleSteps[i + 1].scale, num2);
			}
		}
		return 1f;
	}

	// Token: 0x04004605 RID: 17925
	[MyCmpReq]
	private CanvasScaler canvasScaler;

	// Token: 0x04004606 RID: 17926
	public static string UIScalePrefKey = "UIScalePref";

	// Token: 0x04004607 RID: 17927
	private float userScale = 1f;

	// Token: 0x04004608 RID: 17928
	[Range(0.75f, 2f)]
	private KCanvasScaler.ScaleStep[] scaleSteps = new KCanvasScaler.ScaleStep[]
	{
		new KCanvasScaler.ScaleStep(720f, 0.86f),
		new KCanvasScaler.ScaleStep(1080f, 1f),
		new KCanvasScaler.ScaleStep(2160f, 1.33f)
	};

	// Token: 0x02001EBE RID: 7870
	[Serializable]
	public struct ScaleStep
	{
		// Token: 0x0600B140 RID: 45376 RVA: 0x003D45A1 File Offset: 0x003D27A1
		public ScaleStep(float maxRes_y, float scale)
		{
			this.maxRes_y = maxRes_y;
			this.scale = scale;
		}

		// Token: 0x04008EB8 RID: 36536
		public float scale;

		// Token: 0x04008EB9 RID: 36537
		public float maxRes_y;
	}
}
