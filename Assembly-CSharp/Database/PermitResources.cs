using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000F00 RID: 3840
	public class PermitResources : ResourceSet<PermitResource>
	{
		// Token: 0x060079C0 RID: 31168 RVA: 0x002FFA60 File Offset: 0x002FDC60
		public PermitResources(ResourceSet parent)
			: base("PermitResources", parent)
		{
			this.Root = new ResourceSet<Resource>("Root", null);
			this.Permits = new Dictionary<string, IEnumerable<PermitResource>>();
			this.BuildingFacades = new BuildingFacades(this.Root);
			this.Permits.Add(this.BuildingFacades.Id, this.BuildingFacades.resources);
			this.EquippableFacades = new EquippableFacades(this.Root);
			this.Permits.Add(this.EquippableFacades.Id, this.EquippableFacades.resources);
			this.ArtableStages = new ArtableStages(this.Root);
			this.Permits.Add(this.ArtableStages.Id, this.ArtableStages.resources);
			this.StickerBombs = new StickerBombs(this.Root);
			this.Permits.Add(this.StickerBombs.Id, this.StickerBombs.resources);
			this.ClothingItems = new ClothingItems(this.Root);
			this.ClothingOutfits = new ClothingOutfits(this.Root, this.ClothingItems);
			this.Permits.Add(this.ClothingItems.Id, this.ClothingItems.resources);
			this.BalloonArtistFacades = new BalloonArtistFacades(this.Root);
			this.Permits.Add(this.BalloonArtistFacades.Id, this.BalloonArtistFacades.resources);
			this.MonumentParts = new MonumentParts(this.Root);
			this.Permits.Add(this.MonumentParts.Id, this.MonumentParts.resources);
			foreach (IEnumerable<PermitResource> enumerable in this.Permits.Values)
			{
				this.resources.AddRange(enumerable);
			}
		}

		// Token: 0x060079C1 RID: 31169 RVA: 0x002FFC5C File Offset: 0x002FDE5C
		public void PostProcess()
		{
			this.BuildingFacades.PostProcess();
		}

		// Token: 0x0400588E RID: 22670
		public ResourceSet Root;

		// Token: 0x0400588F RID: 22671
		public BuildingFacades BuildingFacades;

		// Token: 0x04005890 RID: 22672
		public EquippableFacades EquippableFacades;

		// Token: 0x04005891 RID: 22673
		public ArtableStages ArtableStages;

		// Token: 0x04005892 RID: 22674
		public StickerBombs StickerBombs;

		// Token: 0x04005893 RID: 22675
		public ClothingItems ClothingItems;

		// Token: 0x04005894 RID: 22676
		public ClothingOutfits ClothingOutfits;

		// Token: 0x04005895 RID: 22677
		public MonumentParts MonumentParts;

		// Token: 0x04005896 RID: 22678
		public BalloonArtistFacades BalloonArtistFacades;

		// Token: 0x04005897 RID: 22679
		public Dictionary<string, IEnumerable<PermitResource>> Permits;
	}
}
