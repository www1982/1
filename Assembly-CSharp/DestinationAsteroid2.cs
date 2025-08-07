using System;
using ProcGen;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CB7 RID: 3255
[AddComponentMenu("KMonoBehaviour/scripts/DestinationAsteroid2")]
public class DestinationAsteroid2 : KMonoBehaviour
{
	// Token: 0x14000025 RID: 37
	// (add) Token: 0x06006428 RID: 25640 RVA: 0x00259CA8 File Offset: 0x00257EA8
	// (remove) Token: 0x06006429 RID: 25641 RVA: 0x00259CE0 File Offset: 0x00257EE0
	public event Action<ColonyDestinationAsteroidBeltData> OnClicked;

	// Token: 0x0600642A RID: 25642 RVA: 0x00259D15 File Offset: 0x00257F15
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.button.onClick += this.OnClickInternal;
	}

	// Token: 0x0600642B RID: 25643 RVA: 0x00259D34 File Offset: 0x00257F34
	public void SetAsteroid(ColonyDestinationAsteroidBeltData newAsteroidData)
	{
		if (this.asteroidData == null || newAsteroidData.beltPath != this.asteroidData.beltPath)
		{
			this.asteroidData = newAsteroidData;
			global::ProcGen.World getStartWorld = newAsteroidData.GetStartWorld;
			KAnimFile kanimFile;
			Assets.TryGetAnim(getStartWorld.asteroidIcon.IsNullOrWhiteSpace() ? AsteroidGridEntity.DEFAULT_ASTEROID_ICON_ANIM : getStartWorld.asteroidIcon, out kanimFile);
			if (kanimFile != null)
			{
				this.asteroidImage.gameObject.SetActive(false);
				this.animController.AnimFiles = new KAnimFile[] { kanimFile };
				this.animController.initialMode = KAnim.PlayMode.Loop;
				this.animController.initialAnim = "idle_loop";
				this.animController.gameObject.SetActive(true);
				if (this.animController.HasAnimation(this.animController.initialAnim))
				{
					this.animController.Play(this.animController.initialAnim, KAnim.PlayMode.Loop, 1f, 0f);
				}
			}
			else
			{
				this.animController.gameObject.SetActive(false);
				this.asteroidImage.gameObject.SetActive(true);
				this.asteroidImage.sprite = this.asteroidData.sprite;
				this.imageDlcFrom.gameObject.SetActive(false);
			}
			Sprite sprite = null;
			if (DlcManager.IsDlcId(this.asteroidData.Layout.dlcIdFrom))
			{
				sprite = Assets.GetSprite(DlcManager.GetDlcSmallLogo(this.asteroidData.Layout.dlcIdFrom));
			}
			if (sprite != null)
			{
				this.imageDlcFrom.gameObject.SetActive(true);
				this.imageDlcFrom.sprite = sprite;
				return;
			}
			this.imageDlcFrom.gameObject.SetActive(false);
			this.imageDlcFrom.sprite = sprite;
		}
	}

	// Token: 0x0600642C RID: 25644 RVA: 0x00259F03 File Offset: 0x00258103
	private void OnClickInternal()
	{
		DebugUtil.LogArgs(new object[]
		{
			"Clicked asteroid belt",
			this.asteroidData.beltPath
		});
		this.OnClicked(this.asteroidData);
	}

	// Token: 0x04004440 RID: 17472
	[SerializeField]
	private Image asteroidImage;

	// Token: 0x04004441 RID: 17473
	[SerializeField]
	private KButton button;

	// Token: 0x04004442 RID: 17474
	[SerializeField]
	private KBatchedAnimController animController;

	// Token: 0x04004443 RID: 17475
	[SerializeField]
	private Image imageDlcFrom;

	// Token: 0x04004445 RID: 17477
	private ColonyDestinationAsteroidBeltData asteroidData;
}
