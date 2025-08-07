using System;
using UnityEngine;

// Token: 0x02000534 RID: 1332
public class NativeAnimBatchLoader : MonoBehaviour
{
	// Token: 0x06001D63 RID: 7523 RVA: 0x0009F6D4 File Offset: 0x0009D8D4
	private void Start()
	{
		if (this.generateObjects)
		{
			for (int i = 0; i < this.enableObjects.Length; i++)
			{
				if (this.enableObjects[i] != null)
				{
					this.enableObjects[i].GetComponent<KBatchedAnimController>().visibilityType = KAnimControllerBase.VisibilityType.Always;
					this.enableObjects[i].SetActive(true);
				}
			}
		}
		if (this.setTimeScale)
		{
			Time.timeScale = 1f;
		}
		if (this.destroySelf)
		{
			global::UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x0009F750 File Offset: 0x0009D950
	private void LateUpdate()
	{
		if (this.destroySelf)
		{
			return;
		}
		if (this.performUpdate)
		{
			KAnimBatchManager.Instance().UpdateActiveArea(new Vector2I(0, 0), new Vector2I(9999, 9999));
			KAnimBatchManager.Instance().UpdateDirty(Time.frameCount);
		}
		if (this.performRender)
		{
			KAnimBatchManager.Instance().Render();
		}
	}

	// Token: 0x04001127 RID: 4391
	public bool performTimeUpdate;

	// Token: 0x04001128 RID: 4392
	public bool performUpdate;

	// Token: 0x04001129 RID: 4393
	public bool performRender;

	// Token: 0x0400112A RID: 4394
	public bool setTimeScale;

	// Token: 0x0400112B RID: 4395
	public bool destroySelf;

	// Token: 0x0400112C RID: 4396
	public bool generateObjects;

	// Token: 0x0400112D RID: 4397
	public GameObject[] enableObjects;
}
