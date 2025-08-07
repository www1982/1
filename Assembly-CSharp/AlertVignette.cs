using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C4B RID: 3147
public class AlertVignette : KMonoBehaviour
{
	// Token: 0x0600602E RID: 24622 RVA: 0x00238485 File Offset: 0x00236685
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x0600602F RID: 24623 RVA: 0x00238490 File Offset: 0x00236690
	private void Update()
	{
		Color color = this.image.color;
		if (ClusterManager.Instance.GetWorld(this.worldID) == null)
		{
			color = Color.clear;
			this.image.color = color;
			return;
		}
		if (ClusterManager.Instance.GetWorld(this.worldID).IsRedAlert())
		{
			if (color.r != Vignette.Instance.redAlertColor.r || color.g != Vignette.Instance.redAlertColor.g || color.b != Vignette.Instance.redAlertColor.b)
			{
				color = Vignette.Instance.redAlertColor;
			}
		}
		else if (ClusterManager.Instance.GetWorld(this.worldID).IsYellowAlert())
		{
			if (color.r != Vignette.Instance.yellowAlertColor.r || color.g != Vignette.Instance.yellowAlertColor.g || color.b != Vignette.Instance.yellowAlertColor.b)
			{
				color = Vignette.Instance.yellowAlertColor;
			}
		}
		else
		{
			color = Color.clear;
		}
		if (color != Color.clear)
		{
			color.a = 0.2f + (0.5f + Mathf.Sin(Time.unscaledTime * 4f - 1f) / 2f) * 0.5f;
		}
		if (this.image.color != color)
		{
			this.image.color = color;
		}
	}

	// Token: 0x0400412E RID: 16686
	public Image image;

	// Token: 0x0400412F RID: 16687
	public int worldID;
}
