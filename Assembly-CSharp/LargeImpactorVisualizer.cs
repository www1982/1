using System;
using UnityEngine;

// Token: 0x020005D1 RID: 1489
public class LargeImpactorVisualizer : KMonoBehaviour
{
	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06002269 RID: 8809 RVA: 0x000C57D2 File Offset: 0x000C39D2
	public bool Visible
	{
		get
		{
			return this.Active && !this.Folded;
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x0600226B RID: 8811 RVA: 0x000C57F0 File Offset: 0x000C39F0
	// (set) Token: 0x0600226A RID: 8810 RVA: 0x000C57E7 File Offset: 0x000C39E7
	public bool Folded { get; private set; } = true;

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x0600226D RID: 8813 RVA: 0x000C5801 File Offset: 0x000C3A01
	// (set) Token: 0x0600226C RID: 8812 RVA: 0x000C57F8 File Offset: 0x000C39F8
	public float LastTimeSetToFolded { get; private set; }

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x0600226F RID: 8815 RVA: 0x000C5812 File Offset: 0x000C3A12
	// (set) Token: 0x0600226E RID: 8814 RVA: 0x000C5809 File Offset: 0x000C3A09
	public bool ShouldResetEntryEffect { get; private set; }

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06002271 RID: 8817 RVA: 0x000C5823 File Offset: 0x000C3A23
	// (set) Token: 0x06002270 RID: 8816 RVA: 0x000C581A File Offset: 0x000C3A1A
	public float EntryEffectDuration { get; private set; } = 3f;

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06002273 RID: 8819 RVA: 0x000C5834 File Offset: 0x000C3A34
	// (set) Token: 0x06002272 RID: 8818 RVA: 0x000C582B File Offset: 0x000C3A2B
	public float FoldEffectDuration { get; private set; } = 1f;

	// Token: 0x06002274 RID: 8820 RVA: 0x000C583C File Offset: 0x000C3A3C
	public void BeginEntryEffect(float duration)
	{
		this.EntryEffectDuration = duration;
		this.SetShouldResetEntryEffect(true);
	}

	// Token: 0x06002275 RID: 8821 RVA: 0x000C584C File Offset: 0x000C3A4C
	public void SetShouldResetEntryEffect(bool shouldIt)
	{
		this.ShouldResetEntryEffect = shouldIt;
	}

	// Token: 0x06002276 RID: 8822 RVA: 0x000C5858 File Offset: 0x000C3A58
	public void SetFoldedState(bool shouldBeFolded)
	{
		if (!this.Folded && shouldBeFolded)
		{
			this.LastTimeSetToFolded = Time.unscaledTime;
			if (this.Active)
			{
				KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Demolior_LandingZone_close_fx", false));
			}
		}
		this.Folded = shouldBeFolded;
		if (!shouldBeFolded)
		{
			this.LastTimeSetToFolded = float.MaxValue;
		}
	}

	// Token: 0x040013FC RID: 5116
	public bool Active;

	// Token: 0x04001402 RID: 5122
	private const string SFX_Fold = "HUD_Demolior_LandingZone_close_fx";

	// Token: 0x04001403 RID: 5123
	public Vector2I OriginOffset;

	// Token: 0x04001404 RID: 5124
	public Vector2 ScreenSpaceNotificationTogglePosition = Vector2.zero;

	// Token: 0x04001405 RID: 5125
	public Vector2I RangeMin;

	// Token: 0x04001406 RID: 5126
	public Vector2I RangeMax;

	// Token: 0x04001407 RID: 5127
	public Vector2I TexSize = new Vector2I(64, 64);

	// Token: 0x04001408 RID: 5128
	public bool TestLineOfSight;

	// Token: 0x04001409 RID: 5129
	public bool BlockingTileVisible;

	// Token: 0x0400140A RID: 5130
	public Func<int, bool> BlockingVisibleCb;

	// Token: 0x0400140B RID: 5131
	public Func<int, bool> BlockingCb = new Func<int, bool>(Grid.IsSolidCell);

	// Token: 0x0400140C RID: 5132
	public bool AllowLineOfSightInvalidCells;
}
