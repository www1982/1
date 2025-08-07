using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E7C RID: 3708
public class Vignette : KMonoBehaviour
{
	// Token: 0x0600763A RID: 30266 RVA: 0x002D3B87 File Offset: 0x002D1D87
	public static void DestroyInstance()
	{
		Vignette.Instance = null;
	}

	// Token: 0x0600763B RID: 30267 RVA: 0x002D3B90 File Offset: 0x002D1D90
	protected override void OnSpawn()
	{
		this.looping_sounds = base.GetComponent<LoopingSounds>();
		base.OnSpawn();
		Vignette.Instance = this;
		this.defaultColor = this.image.color;
		Game.Instance.Subscribe(1983128072, new Action<object>(this.Refresh));
		Game.Instance.Subscribe(1585324898, new Action<object>(this.Refresh));
		Game.Instance.Subscribe(-1393151672, new Action<object>(this.Refresh));
		Game.Instance.Subscribe(-741654735, new Action<object>(this.Refresh));
		Game.Instance.Subscribe(-2062778933, new Action<object>(this.Refresh));
	}

	// Token: 0x0600763C RID: 30268 RVA: 0x002D3C52 File Offset: 0x002D1E52
	public void SetColor(Color color)
	{
		this.image.color = color;
	}

	// Token: 0x0600763D RID: 30269 RVA: 0x002D3C60 File Offset: 0x002D1E60
	public void Refresh(object data)
	{
		AlertStateManager.Instance alertManager = ClusterManager.Instance.activeWorld.AlertManager;
		if (alertManager == null)
		{
			return;
		}
		if (alertManager.IsYellowAlert())
		{
			this.SetColor(this.yellowAlertColor);
			if (!this.showingYellowAlert)
			{
				this.looping_sounds.StartSound(GlobalAssets.GetSound("YellowAlert_LP", false), true, false, true);
				this.showingYellowAlert = true;
			}
		}
		else
		{
			this.showingYellowAlert = false;
			this.looping_sounds.StopSound(GlobalAssets.GetSound("YellowAlert_LP", false));
		}
		if (alertManager.IsRedAlert())
		{
			this.SetColor(this.redAlertColor);
			if (!this.showingRedAlert)
			{
				this.looping_sounds.StartSound(GlobalAssets.GetSound("RedAlert_LP", false), true, false, true);
				this.showingRedAlert = true;
			}
		}
		else
		{
			this.showingRedAlert = false;
			this.looping_sounds.StopSound(GlobalAssets.GetSound("RedAlert_LP", false));
		}
		if (!this.showingRedAlert && !this.showingYellowAlert)
		{
			this.Reset();
		}
	}

	// Token: 0x0600763E RID: 30270 RVA: 0x002D3D50 File Offset: 0x002D1F50
	public void Reset()
	{
		this.SetColor(this.defaultColor);
		this.showingRedAlert = false;
		this.showingYellowAlert = false;
		this.looping_sounds.StopSound(GlobalAssets.GetSound("RedAlert_LP", false));
		this.looping_sounds.StopSound(GlobalAssets.GetSound("YellowAlert_LP", false));
	}

	// Token: 0x04005208 RID: 21000
	[SerializeField]
	private Image image;

	// Token: 0x04005209 RID: 21001
	public Color defaultColor;

	// Token: 0x0400520A RID: 21002
	public Color redAlertColor = new Color(1f, 0f, 0f, 0.3f);

	// Token: 0x0400520B RID: 21003
	public Color yellowAlertColor = new Color(1f, 1f, 0f, 0.3f);

	// Token: 0x0400520C RID: 21004
	public static Vignette Instance;

	// Token: 0x0400520D RID: 21005
	private LoopingSounds looping_sounds;

	// Token: 0x0400520E RID: 21006
	private bool showingRedAlert;

	// Token: 0x0400520F RID: 21007
	private bool showingYellowAlert;
}
