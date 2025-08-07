using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000896 RID: 2198
[AddComponentMenu("KMonoBehaviour/scripts/DebugText")]
public class DebugText : KMonoBehaviour
{
	// Token: 0x06003CCB RID: 15563 RVA: 0x00152336 File Offset: 0x00150536
	public static void DestroyInstance()
	{
		DebugText.Instance = null;
	}

	// Token: 0x06003CCC RID: 15564 RVA: 0x0015233E File Offset: 0x0015053E
	protected override void OnPrefabInit()
	{
		DebugText.Instance = this;
	}

	// Token: 0x06003CCD RID: 15565 RVA: 0x00152348 File Offset: 0x00150548
	public void Draw(string text, Vector3 pos, Color color)
	{
		DebugText.Entry entry = new DebugText.Entry
		{
			text = text,
			pos = pos,
			color = color
		};
		this.entries.Add(entry);
	}

	// Token: 0x06003CCE RID: 15566 RVA: 0x00152384 File Offset: 0x00150584
	private void LateUpdate()
	{
		foreach (Text text in this.texts)
		{
			global::UnityEngine.Object.Destroy(text.gameObject);
		}
		this.texts.Clear();
		foreach (DebugText.Entry entry in this.entries)
		{
			GameObject gameObject = new GameObject();
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(GameScreenManager.Instance.worldSpaceCanvas.GetComponent<RectTransform>());
			gameObject.transform.SetPosition(entry.pos);
			rectTransform.localScale = new Vector3(0.02f, 0.02f, 1f);
			Text text2 = gameObject.AddComponent<Text>();
			text2.font = Assets.DebugFont;
			text2.text = entry.text;
			text2.color = entry.color;
			text2.horizontalOverflow = HorizontalWrapMode.Overflow;
			text2.verticalOverflow = VerticalWrapMode.Overflow;
			text2.alignment = TextAnchor.MiddleCenter;
			this.texts.Add(text2);
		}
		this.entries.Clear();
	}

	// Token: 0x04002541 RID: 9537
	public static DebugText Instance;

	// Token: 0x04002542 RID: 9538
	private List<DebugText.Entry> entries = new List<DebugText.Entry>();

	// Token: 0x04002543 RID: 9539
	private List<Text> texts = new List<Text>();

	// Token: 0x02001863 RID: 6243
	private struct Entry
	{
		// Token: 0x040078D1 RID: 30929
		public string text;

		// Token: 0x040078D2 RID: 30930
		public Vector3 pos;

		// Token: 0x040078D3 RID: 30931
		public Color color;
	}
}
