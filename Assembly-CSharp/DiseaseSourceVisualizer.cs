using System;
using Klei.AI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020008C4 RID: 2244
[AddComponentMenu("KMonoBehaviour/scripts/DiseaseSourceVisualizer")]
public class DiseaseSourceVisualizer : KMonoBehaviour
{
	// Token: 0x06003E2B RID: 15915 RVA: 0x0015BF71 File Offset: 0x0015A171
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateVisibility();
		Components.DiseaseSourceVisualizers.Add(this);
	}

	// Token: 0x06003E2C RID: 15916 RVA: 0x0015BF8C File Offset: 0x0015A18C
	protected override void OnCleanUp()
	{
		OverlayScreen instance = OverlayScreen.Instance;
		instance.OnOverlayChanged = (Action<HashedString>)Delegate.Remove(instance.OnOverlayChanged, new Action<HashedString>(this.OnViewModeChanged));
		base.OnCleanUp();
		Components.DiseaseSourceVisualizers.Remove(this);
		if (this.visualizer != null)
		{
			global::UnityEngine.Object.Destroy(this.visualizer);
			this.visualizer = null;
		}
	}

	// Token: 0x06003E2D RID: 15917 RVA: 0x0015BFF0 File Offset: 0x0015A1F0
	private void CreateVisualizer()
	{
		if (this.visualizer != null)
		{
			return;
		}
		if (GameScreenManager.Instance.worldSpaceCanvas == null)
		{
			return;
		}
		this.visualizer = Util.KInstantiate(Assets.UIPrefabs.ResourceVisualizer, GameScreenManager.Instance.worldSpaceCanvas, null);
	}

	// Token: 0x06003E2E RID: 15918 RVA: 0x0015C040 File Offset: 0x0015A240
	public void UpdateVisibility()
	{
		this.CreateVisualizer();
		if (string.IsNullOrEmpty(this.alwaysShowDisease))
		{
			this.visible = false;
		}
		else
		{
			Disease disease = Db.Get().Diseases.Get(this.alwaysShowDisease);
			if (disease != null)
			{
				this.SetVisibleDisease(disease);
			}
		}
		if (OverlayScreen.Instance != null)
		{
			this.Show(OverlayScreen.Instance.GetMode());
		}
	}

	// Token: 0x06003E2F RID: 15919 RVA: 0x0015C0A8 File Offset: 0x0015A2A8
	private void SetVisibleDisease(Disease disease)
	{
		Sprite overlaySprite = Assets.instance.DiseaseVisualization.overlaySprite;
		Color32 colorByName = GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName);
		Image component = this.visualizer.transform.GetChild(0).GetComponent<Image>();
		component.sprite = overlaySprite;
		component.color = colorByName;
		this.visible = true;
	}

	// Token: 0x06003E30 RID: 15920 RVA: 0x0015C10A File Offset: 0x0015A30A
	private void Update()
	{
		if (this.visualizer == null)
		{
			return;
		}
		this.visualizer.transform.SetPosition(base.transform.GetPosition() + this.offset);
	}

	// Token: 0x06003E31 RID: 15921 RVA: 0x0015C142 File Offset: 0x0015A342
	private void OnViewModeChanged(HashedString mode)
	{
		this.Show(mode);
	}

	// Token: 0x06003E32 RID: 15922 RVA: 0x0015C14B File Offset: 0x0015A34B
	public void Show(HashedString mode)
	{
		base.enabled = this.visible && mode == OverlayModes.Disease.ID;
		if (this.visualizer != null)
		{
			this.visualizer.SetActive(base.enabled);
		}
	}

	// Token: 0x0400263D RID: 9789
	[SerializeField]
	private Vector3 offset;

	// Token: 0x0400263E RID: 9790
	private GameObject visualizer;

	// Token: 0x0400263F RID: 9791
	private bool visible;

	// Token: 0x04002640 RID: 9792
	public string alwaysShowDisease;
}
