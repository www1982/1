using System;
using UnityEngine;

// Token: 0x02000887 RID: 2183
[AddComponentMenu("KMonoBehaviour/scripts/TemperatureCookable")]
public class TemperatureCookable : KMonoBehaviour, ISim1000ms
{
	// Token: 0x06003C0B RID: 15371 RVA: 0x0014CD05 File Offset: 0x0014AF05
	public void Sim1000ms(float dt)
	{
		if (this.element.Temperature > this.cookTemperature && this.cookedID != null)
		{
			this.Cook();
		}
	}

	// Token: 0x06003C0C RID: 15372 RVA: 0x0014CD28 File Offset: 0x0014AF28
	private void Cook()
	{
		Vector3 position = base.transform.GetPosition();
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(this.cookedID), position);
		gameObject.SetActive(true);
		KSelectable component = base.gameObject.GetComponent<KSelectable>();
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null && SelectTool.Instance.selected == component)
		{
			SelectTool.Instance.Select(gameObject.GetComponent<KSelectable>(), false);
		}
		PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
		component2.Temperature = this.element.Temperature;
		component2.Mass = this.element.Mass;
		base.gameObject.DeleteObject();
	}

	// Token: 0x040024CF RID: 9423
	[MyCmpReq]
	private PrimaryElement element;

	// Token: 0x040024D0 RID: 9424
	public float cookTemperature = 273150f;

	// Token: 0x040024D1 RID: 9425
	public string cookedID;
}
