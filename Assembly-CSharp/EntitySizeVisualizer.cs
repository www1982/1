using System;

// Token: 0x020008F0 RID: 2288
public class EntitySizeVisualizer : KMonoBehaviour
{
	// Token: 0x06003FE0 RID: 16352 RVA: 0x0016684C File Offset: 0x00164A4C
	protected override void OnPrefabInit()
	{
		OreSizeVisualizerData oreSizeVisualizerData = new OreSizeVisualizerData(base.gameObject);
		oreSizeVisualizerData.tierSetType = this.TierSetType;
		GameComps.OreSizeVisualizers.Add(base.gameObject, oreSizeVisualizerData);
		base.OnPrefabInit();
	}

	// Token: 0x06003FE1 RID: 16353 RVA: 0x0016688B File Offset: 0x00164A8B
	protected override void OnCleanUp()
	{
		GameComps.OreSizeVisualizers.Remove(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x040027A3 RID: 10147
	public OreSizeVisualizerComponents.TiersSetType TierSetType;
}
