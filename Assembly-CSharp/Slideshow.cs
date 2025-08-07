using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E52 RID: 3666
[AddComponentMenu("KMonoBehaviour/scripts/Slideshow")]
public class Slideshow : KMonoBehaviour
{
	// Token: 0x060074AA RID: 29866 RVA: 0x002C8720 File Offset: 0x002C6920
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.timeUntilNextSlide = this.timePerSlide;
		if (this.transparentIfEmpty && this.sprites != null && this.sprites.Length == 0)
		{
			this.imageTarget.color = Color.clear;
		}
		if (this.isExpandable)
		{
			this.button = base.GetComponent<KButton>();
			this.button.onClick += delegate
			{
				if (this.onBeforePlay != null)
				{
					this.onBeforePlay();
				}
				SlideshowUpdateType slideshowUpdateType = this.updateType;
				if (slideshowUpdateType == SlideshowUpdateType.preloadedSprites)
				{
					VideoScreen.Instance.PlaySlideShow(this.sprites);
					return;
				}
				if (slideshowUpdateType != SlideshowUpdateType.loadOnDemand)
				{
					return;
				}
				VideoScreen.Instance.PlaySlideShow(this.files);
			};
		}
		if (this.nextButton != null)
		{
			this.nextButton.onClick += delegate
			{
				this.nextSlide();
			};
		}
		if (this.prevButton != null)
		{
			this.prevButton.onClick += delegate
			{
				this.prevSlide();
			};
		}
		if (this.pauseButton != null)
		{
			this.pauseButton.onClick += delegate
			{
				this.SetPaused(!this.paused);
			};
		}
		if (this.closeButton != null)
		{
			this.closeButton.onClick += delegate
			{
				VideoScreen.Instance.Stop();
				if (this.onEndingPlay != null)
				{
					this.onEndingPlay();
				}
			};
		}
	}

	// Token: 0x060074AB RID: 29867 RVA: 0x002C8828 File Offset: 0x002C6A28
	public void SetPaused(bool state)
	{
		this.paused = state;
		if (this.pauseIcon != null)
		{
			this.pauseIcon.gameObject.SetActive(!this.paused);
		}
		if (this.unpauseIcon != null)
		{
			this.unpauseIcon.gameObject.SetActive(this.paused);
		}
		if (this.prevButton != null)
		{
			this.prevButton.gameObject.SetActive(this.paused);
		}
		if (this.nextButton != null)
		{
			this.nextButton.gameObject.SetActive(this.paused);
		}
	}

	// Token: 0x060074AC RID: 29868 RVA: 0x002C88D0 File Offset: 0x002C6AD0
	private void resetSlide(bool enable)
	{
		this.timeUntilNextSlide = this.timePerSlide;
		this.currentSlide = 0;
		if (enable)
		{
			this.imageTarget.color = Color.white;
			return;
		}
		if (this.transparentIfEmpty)
		{
			this.imageTarget.color = Color.clear;
		}
	}

	// Token: 0x060074AD RID: 29869 RVA: 0x002C891C File Offset: 0x002C6B1C
	private Sprite loadSlide(string file)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		Texture2D texture2D = new Texture2D(512, 768);
		texture2D.filterMode = FilterMode.Point;
		texture2D.LoadImage(File.ReadAllBytes(file));
		return Sprite.Create(texture2D, new Rect(Vector2.zero, new Vector2((float)texture2D.width, (float)texture2D.height)), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect);
	}

	// Token: 0x060074AE RID: 29870 RVA: 0x002C898C File Offset: 0x002C6B8C
	public void SetFiles(string[] files, int loadFrame = -1)
	{
		if (files == null)
		{
			return;
		}
		this.files = files;
		bool flag = files.Length != 0 && files[0] != null;
		this.resetSlide(flag);
		if (flag)
		{
			int num = ((loadFrame != -1) ? loadFrame : (files.Length - 1));
			string text = files[num];
			Sprite sprite = this.loadSlide(text);
			this.setSlide(sprite);
			this.currentSlideImage = sprite;
		}
	}

	// Token: 0x060074AF RID: 29871 RVA: 0x002C89E4 File Offset: 0x002C6BE4
	public void updateSize(Sprite sprite)
	{
		Vector2 fittedSize = this.GetFittedSize(sprite, 960f, 960f);
		base.GetComponent<RectTransform>().sizeDelta = fittedSize;
	}

	// Token: 0x060074B0 RID: 29872 RVA: 0x002C8A0F File Offset: 0x002C6C0F
	public void SetSprites(Sprite[] sprites)
	{
		if (sprites == null)
		{
			return;
		}
		this.sprites = sprites;
		this.resetSlide(sprites.Length != 0 && sprites[0] != null);
		if (sprites.Length != 0 && sprites[0] != null)
		{
			this.setSlide(sprites[0]);
		}
	}

	// Token: 0x060074B1 RID: 29873 RVA: 0x002C8A4C File Offset: 0x002C6C4C
	public Vector2 GetFittedSize(Sprite sprite, float maxWidth, float maxHeight)
	{
		if (sprite == null || sprite.texture == null)
		{
			return Vector2.zero;
		}
		int width = sprite.texture.width;
		int height = sprite.texture.height;
		float num = maxWidth / (float)width;
		float num2 = maxHeight / (float)height;
		if (num < num2)
		{
			return new Vector2((float)width * num, (float)height * num);
		}
		return new Vector2((float)width * num2, (float)height * num2);
	}

	// Token: 0x060074B2 RID: 29874 RVA: 0x002C8AB7 File Offset: 0x002C6CB7
	public void setSlide(Sprite slide)
	{
		if (slide == null)
		{
			return;
		}
		this.imageTarget.texture = slide.texture;
		this.updateSize(slide);
	}

	// Token: 0x060074B3 RID: 29875 RVA: 0x002C8ADB File Offset: 0x002C6CDB
	public void nextSlide()
	{
		this.setSlideIndex(this.currentSlide + 1);
	}

	// Token: 0x060074B4 RID: 29876 RVA: 0x002C8AEB File Offset: 0x002C6CEB
	public void prevSlide()
	{
		this.setSlideIndex(this.currentSlide - 1);
	}

	// Token: 0x060074B5 RID: 29877 RVA: 0x002C8AFC File Offset: 0x002C6CFC
	private void setSlideIndex(int slideIndex)
	{
		this.timeUntilNextSlide = this.timePerSlide;
		SlideshowUpdateType slideshowUpdateType = this.updateType;
		if (slideshowUpdateType != SlideshowUpdateType.preloadedSprites)
		{
			if (slideshowUpdateType != SlideshowUpdateType.loadOnDemand)
			{
				return;
			}
			if (slideIndex < 0)
			{
				slideIndex = this.files.Length + slideIndex;
			}
			this.currentSlide = slideIndex % this.files.Length;
			if (this.currentSlide == this.files.Length - 1)
			{
				this.timeUntilNextSlide *= this.timeFactorForLastSlide;
			}
			if (this.playInThumbnail)
			{
				if (this.currentSlideImage != null)
				{
					global::UnityEngine.Object.Destroy(this.currentSlideImage.texture);
					global::UnityEngine.Object.Destroy(this.currentSlideImage);
					GC.Collect();
				}
				this.currentSlideImage = this.loadSlide(this.files[this.currentSlide]);
				this.setSlide(this.currentSlideImage);
			}
		}
		else
		{
			if (slideIndex < 0)
			{
				slideIndex = this.sprites.Length + slideIndex;
			}
			this.currentSlide = slideIndex % this.sprites.Length;
			if (this.currentSlide == this.sprites.Length - 1)
			{
				this.timeUntilNextSlide *= this.timeFactorForLastSlide;
			}
			if (this.playInThumbnail)
			{
				this.setSlide(this.sprites[this.currentSlide]);
				return;
			}
		}
	}

	// Token: 0x060074B6 RID: 29878 RVA: 0x002C8C28 File Offset: 0x002C6E28
	private void Update()
	{
		if (this.updateType == SlideshowUpdateType.preloadedSprites && (this.sprites == null || this.sprites.Length == 0))
		{
			return;
		}
		if (this.updateType == SlideshowUpdateType.loadOnDemand && (this.files == null || this.files.Length == 0))
		{
			return;
		}
		if (this.paused)
		{
			return;
		}
		this.timeUntilNextSlide -= Time.unscaledDeltaTime;
		if (this.timeUntilNextSlide <= 0f)
		{
			this.nextSlide();
		}
	}

	// Token: 0x040050A6 RID: 20646
	public RawImage imageTarget;

	// Token: 0x040050A7 RID: 20647
	private string[] files;

	// Token: 0x040050A8 RID: 20648
	private Sprite currentSlideImage;

	// Token: 0x040050A9 RID: 20649
	private Sprite[] sprites;

	// Token: 0x040050AA RID: 20650
	public float timePerSlide = 1f;

	// Token: 0x040050AB RID: 20651
	public float timeFactorForLastSlide = 3f;

	// Token: 0x040050AC RID: 20652
	private int currentSlide;

	// Token: 0x040050AD RID: 20653
	private float timeUntilNextSlide;

	// Token: 0x040050AE RID: 20654
	private bool paused;

	// Token: 0x040050AF RID: 20655
	public bool playInThumbnail;

	// Token: 0x040050B0 RID: 20656
	public SlideshowUpdateType updateType;

	// Token: 0x040050B1 RID: 20657
	[SerializeField]
	private bool isExpandable;

	// Token: 0x040050B2 RID: 20658
	[SerializeField]
	private KButton button;

	// Token: 0x040050B3 RID: 20659
	[SerializeField]
	private bool transparentIfEmpty = true;

	// Token: 0x040050B4 RID: 20660
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040050B5 RID: 20661
	[SerializeField]
	private KButton prevButton;

	// Token: 0x040050B6 RID: 20662
	[SerializeField]
	private KButton nextButton;

	// Token: 0x040050B7 RID: 20663
	[SerializeField]
	private KButton pauseButton;

	// Token: 0x040050B8 RID: 20664
	[SerializeField]
	private Image pauseIcon;

	// Token: 0x040050B9 RID: 20665
	[SerializeField]
	private Image unpauseIcon;

	// Token: 0x040050BA RID: 20666
	public Slideshow.onBeforeAndEndPlayDelegate onBeforePlay;

	// Token: 0x040050BB RID: 20667
	public Slideshow.onBeforeAndEndPlayDelegate onEndingPlay;

	// Token: 0x0200204F RID: 8271
	// (Invoke) Token: 0x0600B601 RID: 46593
	public delegate void onBeforeAndEndPlayDelegate();
}
