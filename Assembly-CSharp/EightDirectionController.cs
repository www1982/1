using System;
using UnityEngine;

// Token: 0x0200071E RID: 1822
public class EightDirectionController
{
	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x001073A2 File Offset: 0x001055A2
	// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x001073AA File Offset: 0x001055AA
	public KBatchedAnimController controller { get; private set; }

	// Token: 0x06002DE8 RID: 11752 RVA: 0x001073B3 File Offset: 0x001055B3
	public EightDirectionController(KAnimControllerBase buildingController, string targetSymbol, string defaultAnim, EightDirectionController.Offset frontBank)
	{
		this.Initialize(buildingController, targetSymbol, defaultAnim, frontBank, Grid.SceneLayer.NoLayer);
	}

	// Token: 0x06002DE9 RID: 11753 RVA: 0x001073C8 File Offset: 0x001055C8
	private void Initialize(KAnimControllerBase buildingController, string targetSymbol, string defaultAnim, EightDirectionController.Offset frontBack, Grid.SceneLayer userSpecifiedRenderLayer)
	{
		string text = buildingController.name + ".eight_direction";
		this.gameObject = new GameObject(text);
		this.gameObject.SetActive(false);
		this.gameObject.transform.parent = buildingController.transform;
		this.gameObject.AddComponent<KPrefabID>().PrefabTag = new Tag(text);
		this.defaultAnim = defaultAnim;
		this.controller = this.gameObject.AddOrGet<KBatchedAnimController>();
		this.controller.AnimFiles = new KAnimFile[] { buildingController.AnimFiles[0] };
		this.controller.initialAnim = defaultAnim;
		this.controller.isMovable = true;
		this.controller.sceneLayer = Grid.SceneLayer.NoLayer;
		if (EightDirectionController.Offset.UserSpecified == frontBack)
		{
			this.controller.sceneLayer = userSpecifiedRenderLayer;
		}
		buildingController.SetSymbolVisiblity(targetSymbol, false);
		bool flag;
		Vector3 vector = buildingController.GetSymbolTransform(new HashedString(targetSymbol), out flag).GetColumn(3);
		switch (frontBack)
		{
		case EightDirectionController.Offset.Infront:
			vector.z = buildingController.transform.GetPosition().z - 0.1f;
			break;
		case EightDirectionController.Offset.Behind:
			vector.z = buildingController.transform.GetPosition().z + 0.1f;
			break;
		case EightDirectionController.Offset.UserSpecified:
			vector.z = Grid.GetLayerZ(userSpecifiedRenderLayer);
			break;
		}
		this.gameObject.transform.SetPosition(vector);
		this.gameObject.SetActive(true);
		this.link = new KAnimLink(buildingController, this.controller);
	}

	// Token: 0x06002DEA RID: 11754 RVA: 0x00107550 File Offset: 0x00105750
	public void SetPositionPercent(float percent_full)
	{
		if (this.controller == null)
		{
			return;
		}
		this.controller.SetPositionPercent(percent_full);
	}

	// Token: 0x06002DEB RID: 11755 RVA: 0x0010756D File Offset: 0x0010576D
	public void SetSymbolTint(KAnimHashedString symbol, Color32 colour)
	{
		if (this.controller != null)
		{
			this.controller.SetSymbolTint(symbol, colour);
		}
	}

	// Token: 0x06002DEC RID: 11756 RVA: 0x0010758F File Offset: 0x0010578F
	public void SetRotation(float rot)
	{
		if (this.controller == null)
		{
			return;
		}
		this.controller.Rotation = rot;
	}

	// Token: 0x06002DED RID: 11757 RVA: 0x001075AC File Offset: 0x001057AC
	public void PlayAnim(string anim, KAnim.PlayMode mode = KAnim.PlayMode.Once)
	{
		this.controller.Play(anim, mode, 1f, 0f);
	}

	// Token: 0x04001B0C RID: 6924
	public GameObject gameObject;

	// Token: 0x04001B0D RID: 6925
	private string defaultAnim;

	// Token: 0x04001B0E RID: 6926
	private KAnimLink link;

	// Token: 0x020015C0 RID: 5568
	public enum Offset
	{
		// Token: 0x040070C1 RID: 28865
		Infront,
		// Token: 0x040070C2 RID: 28866
		Behind,
		// Token: 0x040070C3 RID: 28867
		UserSpecified
	}
}
