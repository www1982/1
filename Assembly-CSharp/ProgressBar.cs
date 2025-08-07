using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DA1 RID: 3489
[AddComponentMenu("KMonoBehaviour/scripts/ProgressBar")]
public class ProgressBar : KMonoBehaviour
{
	// Token: 0x170007A0 RID: 1952
	// (get) Token: 0x06006D46 RID: 27974 RVA: 0x0029612A File Offset: 0x0029432A
	// (set) Token: 0x06006D47 RID: 27975 RVA: 0x00296137 File Offset: 0x00294337
	public Color barColor
	{
		get
		{
			return this.bar.color;
		}
		set
		{
			this.bar.color = value;
		}
	}

	// Token: 0x170007A1 RID: 1953
	// (get) Token: 0x06006D48 RID: 27976 RVA: 0x00296145 File Offset: 0x00294345
	// (set) Token: 0x06006D49 RID: 27977 RVA: 0x00296152 File Offset: 0x00294352
	public float PercentFull
	{
		get
		{
			return this.bar.fillAmount;
		}
		set
		{
			this.bar.fillAmount = value;
		}
	}

	// Token: 0x06006D4A RID: 27978 RVA: 0x00296160 File Offset: 0x00294360
	public void SetVisibility(bool visible)
	{
		this.lastVisibilityValue = visible;
		this.RefreshVisibility();
	}

	// Token: 0x06006D4B RID: 27979 RVA: 0x00296170 File Offset: 0x00294370
	private void RefreshVisibility()
	{
		int myWorldId = base.gameObject.GetMyWorldId();
		bool flag = this.lastVisibilityValue;
		flag &= !this.hasBeenInitialize || myWorldId == ClusterManager.Instance.activeWorldId;
		flag &= !this.autoHide || SimDebugView.Instance == null || SimDebugView.Instance.GetMode() == OverlayModes.None.ID;
		base.gameObject.SetActive(flag);
		if (this.updatePercentFull == null || this.updatePercentFull.Target.IsNullOrDestroyed())
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06006D4C RID: 27980 RVA: 0x0029620C File Offset: 0x0029440C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.hasBeenInitialize = true;
		if (this.autoHide)
		{
			this.overlayUpdateHandle = Game.Instance.Subscribe(1798162660, new Action<object>(this.OnOverlayChanged));
			if (SimDebugView.Instance != null && SimDebugView.Instance.GetMode() != OverlayModes.None.ID)
			{
				base.gameObject.SetActive(false);
			}
		}
		Game.Instance.Subscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
		this.SetWorldActive(ClusterManager.Instance.activeWorldId);
		base.enabled = this.updatePercentFull != null;
		this.RefreshVisibility();
	}

	// Token: 0x06006D4D RID: 27981 RVA: 0x002962C0 File Offset: 0x002944C0
	private void OnActiveWorldChanged(object data)
	{
		global::Tuple<int, int> tuple = (global::Tuple<int, int>)data;
		this.SetWorldActive(tuple.first);
	}

	// Token: 0x06006D4E RID: 27982 RVA: 0x002962E0 File Offset: 0x002944E0
	private void SetWorldActive(int worldId)
	{
		this.RefreshVisibility();
	}

	// Token: 0x06006D4F RID: 27983 RVA: 0x002962E8 File Offset: 0x002944E8
	public void SetUpdateFunc(Func<float> func)
	{
		this.updatePercentFull = func;
		base.enabled = this.updatePercentFull != null;
	}

	// Token: 0x06006D50 RID: 27984 RVA: 0x00296300 File Offset: 0x00294500
	public virtual void Update()
	{
		if (this.updatePercentFull != null && !this.updatePercentFull.Target.IsNullOrDestroyed())
		{
			this.PercentFull = this.updatePercentFull();
		}
	}

	// Token: 0x06006D51 RID: 27985 RVA: 0x0029632D File Offset: 0x0029452D
	public virtual void OnOverlayChanged(object data = null)
	{
		this.RefreshVisibility();
	}

	// Token: 0x06006D52 RID: 27986 RVA: 0x00296338 File Offset: 0x00294538
	public void Retarget(GameObject entity)
	{
		Vector3 vector = entity.transform.GetPosition() + Vector3.down * 0.5f;
		Building component = entity.GetComponent<Building>();
		if (component != null)
		{
			vector -= Vector3.right * 0.5f * (float)(component.Def.WidthInCells % 2);
		}
		else
		{
			vector -= Vector3.right * 0.5f;
		}
		base.transform.SetPosition(vector);
	}

	// Token: 0x06006D53 RID: 27987 RVA: 0x002963C3 File Offset: 0x002945C3
	protected override void OnCleanUp()
	{
		if (this.overlayUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.overlayUpdateHandle);
		}
		Game.Instance.Unsubscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
		base.OnCleanUp();
	}

	// Token: 0x06006D54 RID: 27988 RVA: 0x002963FF File Offset: 0x002945FF
	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	// Token: 0x06006D55 RID: 27989 RVA: 0x00296408 File Offset: 0x00294608
	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	// Token: 0x06006D56 RID: 27990 RVA: 0x00296414 File Offset: 0x00294614
	public static ProgressBar CreateProgressBar(GameObject entity, Func<float> updateFunc)
	{
		ProgressBar progressBar = Util.KInstantiateUI<ProgressBar>(ProgressBarsConfig.Instance.progressBarPrefab, null, false);
		progressBar.SetUpdateFunc(updateFunc);
		progressBar.transform.SetParent(GameScreenManager.Instance.worldSpaceCanvas.transform);
		progressBar.name = ((entity != null) ? (entity.name + "_") : "") + " ProgressBar";
		progressBar.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("ProgressBar");
		progressBar.Update();
		progressBar.Retarget(entity);
		return progressBar;
	}

	// Token: 0x04004AA3 RID: 19107
	public Image bar;

	// Token: 0x04004AA4 RID: 19108
	private Func<float> updatePercentFull;

	// Token: 0x04004AA5 RID: 19109
	private int overlayUpdateHandle = -1;

	// Token: 0x04004AA6 RID: 19110
	public bool autoHide = true;

	// Token: 0x04004AA7 RID: 19111
	private bool lastVisibilityValue = true;

	// Token: 0x04004AA8 RID: 19112
	private bool hasBeenInitialize;
}
