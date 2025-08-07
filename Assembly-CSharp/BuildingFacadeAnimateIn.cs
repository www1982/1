using System;
using UnityEngine;

// Token: 0x020006E7 RID: 1767
public class BuildingFacadeAnimateIn : MonoBehaviour
{
	// Token: 0x06002BE9 RID: 11241 RVA: 0x000FCCB8 File Offset: 0x000FAEB8
	private void Awake()
	{
		this.placeAnimController.TintColour = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 1);
		this.colorAnimController.TintColour = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 1);
		this.updater = Updater.Series(new Updater[]
		{
			KleiPermitBuildingAnimateIn.MakeAnimInUpdater(this.sourceAnimController, this.placeAnimController, this.colorAnimController),
			Updater.Do(delegate
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			})
		});
	}

	// Token: 0x06002BEA RID: 11242 RVA: 0x000FCD4C File Offset: 0x000FAF4C
	private void Update()
	{
		if (this.sourceAnimController.IsNullOrDestroyed())
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		BuildingFacadeAnimateIn.SetVisibilityOn(this.sourceAnimController, false);
		this.updater.Internal_Update(Time.unscaledDeltaTime);
	}

	// Token: 0x06002BEB RID: 11243 RVA: 0x000FCD84 File Offset: 0x000FAF84
	private void OnDisable()
	{
		if (!this.sourceAnimController.IsNullOrDestroyed())
		{
			BuildingFacadeAnimateIn.SetVisibilityOn(this.sourceAnimController, true);
		}
		global::UnityEngine.Object.Destroy(this.placeAnimController.gameObject);
		global::UnityEngine.Object.Destroy(this.colorAnimController.gameObject);
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06002BEC RID: 11244 RVA: 0x000FCDD8 File Offset: 0x000FAFD8
	public static BuildingFacadeAnimateIn MakeFor(KBatchedAnimController sourceAnimController)
	{
		BuildingFacadeAnimateIn.SetVisibilityOn(sourceAnimController, false);
		KBatchedAnimController kbatchedAnimController = BuildingFacadeAnimateIn.SpawnAnimFrom(sourceAnimController);
		kbatchedAnimController.gameObject.name = "BuildingFacadeAnimateIn.placeAnimController";
		kbatchedAnimController.initialAnim = "place";
		KBatchedAnimController kbatchedAnimController2 = BuildingFacadeAnimateIn.SpawnAnimFrom(sourceAnimController);
		kbatchedAnimController2.gameObject.name = "BuildingFacadeAnimateIn.colorAnimController";
		kbatchedAnimController2.initialAnim = ((sourceAnimController.CurrentAnim != null) ? sourceAnimController.CurrentAnim.name : sourceAnimController.AnimFiles[0].GetData().GetAnim(0).name);
		GameObject gameObject = new GameObject("BuildingFacadeAnimateIn");
		gameObject.SetActive(false);
		gameObject.transform.SetParent(sourceAnimController.transform.parent, false);
		BuildingFacadeAnimateIn buildingFacadeAnimateIn = gameObject.AddComponent<BuildingFacadeAnimateIn>();
		buildingFacadeAnimateIn.sourceAnimController = sourceAnimController;
		buildingFacadeAnimateIn.placeAnimController = kbatchedAnimController;
		buildingFacadeAnimateIn.colorAnimController = kbatchedAnimController2;
		kbatchedAnimController.gameObject.SetActive(true);
		kbatchedAnimController2.gameObject.SetActive(true);
		gameObject.SetActive(true);
		return buildingFacadeAnimateIn;
	}

	// Token: 0x06002BED RID: 11245 RVA: 0x000FCEBC File Offset: 0x000FB0BC
	private static void SetVisibilityOn(KBatchedAnimController animController, bool isVisible)
	{
		animController.SetVisiblity(isVisible);
		foreach (KBatchedAnimController kbatchedAnimController in animController.GetComponentsInChildren<KBatchedAnimController>(true))
		{
			if (kbatchedAnimController.batchGroupID == animController.batchGroupID)
			{
				kbatchedAnimController.SetVisiblity(isVisible);
			}
		}
	}

	// Token: 0x06002BEE RID: 11246 RVA: 0x000FCF04 File Offset: 0x000FB104
	private static KBatchedAnimController SpawnAnimFrom(KBatchedAnimController sourceAnimController)
	{
		GameObject gameObject = new GameObject();
		gameObject.SetActive(false);
		gameObject.transform.SetParent(sourceAnimController.transform.parent, false);
		gameObject.transform.localPosition = sourceAnimController.transform.localPosition;
		gameObject.transform.localRotation = sourceAnimController.transform.localRotation;
		gameObject.transform.localScale = sourceAnimController.transform.localScale;
		gameObject.layer = sourceAnimController.gameObject.layer;
		KBatchedAnimController kbatchedAnimController = gameObject.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.materialType = sourceAnimController.materialType;
		kbatchedAnimController.initialMode = sourceAnimController.initialMode;
		kbatchedAnimController.AnimFiles = sourceAnimController.AnimFiles;
		kbatchedAnimController.Offset = sourceAnimController.Offset;
		kbatchedAnimController.animWidth = sourceAnimController.animWidth;
		kbatchedAnimController.animHeight = sourceAnimController.animHeight;
		kbatchedAnimController.animScale = sourceAnimController.animScale;
		kbatchedAnimController.sceneLayer = sourceAnimController.sceneLayer;
		kbatchedAnimController.fgLayer = sourceAnimController.fgLayer;
		kbatchedAnimController.FlipX = sourceAnimController.FlipX;
		kbatchedAnimController.FlipY = sourceAnimController.FlipY;
		kbatchedAnimController.Rotation = sourceAnimController.Rotation;
		kbatchedAnimController.Pivot = sourceAnimController.Pivot;
		return kbatchedAnimController;
	}

	// Token: 0x040019EF RID: 6639
	private KBatchedAnimController sourceAnimController;

	// Token: 0x040019F0 RID: 6640
	private KBatchedAnimController placeAnimController;

	// Token: 0x040019F1 RID: 6641
	private KBatchedAnimController colorAnimController;

	// Token: 0x040019F2 RID: 6642
	private Updater updater;
}
