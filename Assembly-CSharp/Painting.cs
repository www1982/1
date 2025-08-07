using System;

// Token: 0x020005F5 RID: 1525
public class Painting : Artable
{
	// Token: 0x060023CF RID: 9167 RVA: 0x000CC3C8 File Offset: 0x000CA5C8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.multitoolContext = "paint";
		this.multitoolHitEffectTag = "fx_paint_splash";
	}

	// Token: 0x060023D0 RID: 9168 RVA: 0x000CC3FB File Offset: 0x000CA5FB
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Paintings.Add(this);
	}

	// Token: 0x060023D1 RID: 9169 RVA: 0x000CC40E File Offset: 0x000CA60E
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Paintings.Remove(this);
	}

	// Token: 0x060023D2 RID: 9170 RVA: 0x000CC421 File Offset: 0x000CA621
	public override void SetStage(string stage_id, bool skip_effect)
	{
		base.SetStage(stage_id, skip_effect);
		if (Db.GetArtableStages().Get(stage_id) == null)
		{
			Debug.LogError("Missing stage: " + stage_id);
		}
	}
}
