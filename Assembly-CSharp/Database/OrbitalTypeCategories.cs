using System;

namespace Database
{
	// Token: 0x02000EF8 RID: 3832
	public class OrbitalTypeCategories : ResourceSet<OrbitalData>
	{
		// Token: 0x060079AA RID: 31146 RVA: 0x002FEF84 File Offset: 0x002FD184
		public OrbitalTypeCategories(ResourceSet parent)
			: base("OrbitalTypeCategories", parent)
		{
			this.backgroundEarth = new OrbitalData("backgroundEarth", this, "earth_kanim", "", OrbitalData.OrbitalType.world, 1f, 0.5f, 0.95f, 10f, 10f, 1.05f, true, 0.05f, 25f, 1f);
			this.backgroundEarth.GetRenderZ = () => Grid.GetLayerZ(Grid.SceneLayer.Background) + 0.9f;
			this.frozenOre = new OrbitalData("frozenOre", this, "starmap_frozen_ore_kanim", "", OrbitalData.OrbitalType.poi, 1f, 0.5f, 0.5f, -350f, 350f, 1f, true, 0.05f, 25f, 1f);
			this.heliumCloud = new OrbitalData("heliumCloud", this, "starmap_helium_cloud_kanim", "", OrbitalData.OrbitalType.poi, 1f, 0.5f, 0.5f, -350f, 350f, 1.05f, true, 0.05f, 25f, 1f);
			this.iceCloud = new OrbitalData("iceCloud", this, "starmap_ice_cloud_kanim", "", OrbitalData.OrbitalType.poi, 1f, 0.5f, 0.5f, -350f, 350f, 1.05f, true, 0.05f, 25f, 1f);
			this.iceRock = new OrbitalData("iceRock", this, "starmap_ice_kanim", "", OrbitalData.OrbitalType.poi, 1f, 0.5f, 0.5f, -350f, 350f, 1.05f, true, 0.05f, 25f, 1f);
			this.purpleGas = new OrbitalData("purpleGas", this, "starmap_purple_gas_kanim", "", OrbitalData.OrbitalType.poi, 1f, 0.5f, 0.5f, -350f, 350f, 1.05f, true, 0.05f, 25f, 1f);
			this.radioactiveGas = new OrbitalData("radioactiveGas", this, "starmap_radioactive_gas_kanim", "", OrbitalData.OrbitalType.poi, 1f, 0.5f, 0.5f, -350f, 350f, 1.05f, true, 0.05f, 25f, 1f);
			this.rocky = new OrbitalData("rocky", this, "starmap_rocky_kanim", "", OrbitalData.OrbitalType.poi, 1f, 0.5f, 0.5f, -350f, 350f, 1.05f, true, 0.05f, 25f, 1f);
			this.gravitas = new OrbitalData("gravitas", this, "starmap_space_junk_kanim", "", OrbitalData.OrbitalType.poi, 1f, 0.5f, 0.5f, -350f, 350f, 1.05f, true, 0.05f, 25f, 1f);
			this.orbit = new OrbitalData("orbit", this, "starmap_orbit_kanim", "", OrbitalData.OrbitalType.inOrbit, 1f, 0.25f, 0.5f, -350f, 350f, 1.05f, false, 0.05f, 4f, 1f);
			this.landed = new OrbitalData("landed", this, "starmap_landed_surface_kanim", "", OrbitalData.OrbitalType.landed, 0f, 0.5f, 0.35f, -350f, 350f, 1.05f, false, 0.05f, 4f, 1f);
		}

		// Token: 0x0400584F RID: 22607
		public OrbitalData backgroundEarth;

		// Token: 0x04005850 RID: 22608
		public OrbitalData frozenOre;

		// Token: 0x04005851 RID: 22609
		public OrbitalData heliumCloud;

		// Token: 0x04005852 RID: 22610
		public OrbitalData iceCloud;

		// Token: 0x04005853 RID: 22611
		public OrbitalData iceRock;

		// Token: 0x04005854 RID: 22612
		public OrbitalData purpleGas;

		// Token: 0x04005855 RID: 22613
		public OrbitalData radioactiveGas;

		// Token: 0x04005856 RID: 22614
		public OrbitalData rocky;

		// Token: 0x04005857 RID: 22615
		public OrbitalData gravitas;

		// Token: 0x04005858 RID: 22616
		public OrbitalData orbit;

		// Token: 0x04005859 RID: 22617
		public OrbitalData landed;
	}
}
