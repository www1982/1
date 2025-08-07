using System;

// Token: 0x02000656 RID: 1622
public class OrbitalData : Resource
{
	// Token: 0x060027A4 RID: 10148 RVA: 0x000E1C14 File Offset: 0x000DFE14
	public OrbitalData(string id, ResourceSet parent, string animFile = "earth_kanim", string initialAnim = "", OrbitalData.OrbitalType orbitalType = OrbitalData.OrbitalType.poi, float periodInCycles = 1f, float xGridPercent = 0.5f, float yGridPercent = 0.5f, float minAngle = -350f, float maxAngle = 350f, float radiusScale = 1.05f, bool rotatesBehind = true, float behindZ = 0.05f, float distance = 25f, float renderZ = 1f)
		: base(id, parent, null)
	{
		this.animFile = animFile;
		this.initialAnim = initialAnim;
		this.orbitalType = orbitalType;
		this.periodInCycles = periodInCycles;
		this.xGridPercent = xGridPercent;
		this.yGridPercent = yGridPercent;
		this.minAngle = minAngle;
		this.maxAngle = maxAngle;
		this.radiusScale = radiusScale;
		this.rotatesBehind = rotatesBehind;
		this.behindZ = behindZ;
		this.distance = distance;
		this.renderZ = renderZ;
	}

	// Token: 0x04001749 RID: 5961
	public string animFile;

	// Token: 0x0400174A RID: 5962
	public string initialAnim;

	// Token: 0x0400174B RID: 5963
	public float periodInCycles;

	// Token: 0x0400174C RID: 5964
	public float xGridPercent;

	// Token: 0x0400174D RID: 5965
	public float yGridPercent;

	// Token: 0x0400174E RID: 5966
	public float minAngle;

	// Token: 0x0400174F RID: 5967
	public float maxAngle;

	// Token: 0x04001750 RID: 5968
	public float radiusScale;

	// Token: 0x04001751 RID: 5969
	public bool rotatesBehind;

	// Token: 0x04001752 RID: 5970
	public float behindZ;

	// Token: 0x04001753 RID: 5971
	public float distance;

	// Token: 0x04001754 RID: 5972
	public float renderZ;

	// Token: 0x04001755 RID: 5973
	public OrbitalData.OrbitalType orbitalType;

	// Token: 0x04001756 RID: 5974
	public Func<float> GetRenderZ;

	// Token: 0x020014E7 RID: 5351
	public enum OrbitalType
	{
		// Token: 0x04006E40 RID: 28224
		world,
		// Token: 0x04006E41 RID: 28225
		poi,
		// Token: 0x04006E42 RID: 28226
		inOrbit,
		// Token: 0x04006E43 RID: 28227
		landed
	}
}
