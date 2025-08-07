using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D1F RID: 3359
public class KScrollbarVisibility : MonoBehaviour
{
	// Token: 0x0600678F RID: 26511 RVA: 0x00270A12 File Offset: 0x0026EC12
	private void Start()
	{
		this.Update();
	}

	// Token: 0x06006790 RID: 26512 RVA: 0x00270A1C File Offset: 0x0026EC1C
	private void Update()
	{
		if (this.content.content == null)
		{
			return;
		}
		bool flag = false;
		Vector2 vector = new Vector2(this.parent.rect.width, this.parent.rect.height);
		Vector2 sizeDelta = this.content.content.GetComponent<RectTransform>().sizeDelta;
		if ((sizeDelta.x >= vector.x && this.checkWidth) || (sizeDelta.y >= vector.y && this.checkHeight))
		{
			flag = true;
		}
		if (this.scrollbar.gameObject.activeSelf != flag)
		{
			this.scrollbar.gameObject.SetActive(flag);
			if (this.others != null)
			{
				GameObject[] array = this.others;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(flag);
				}
			}
		}
	}

	// Token: 0x040046F0 RID: 18160
	[SerializeField]
	private ScrollRect content;

	// Token: 0x040046F1 RID: 18161
	[SerializeField]
	private RectTransform parent;

	// Token: 0x040046F2 RID: 18162
	[SerializeField]
	private bool checkWidth = true;

	// Token: 0x040046F3 RID: 18163
	[SerializeField]
	private bool checkHeight = true;

	// Token: 0x040046F4 RID: 18164
	[SerializeField]
	private Scrollbar scrollbar;

	// Token: 0x040046F5 RID: 18165
	[SerializeField]
	private GameObject[] others;
}
