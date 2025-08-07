using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000CB6 RID: 3254
[AddComponentMenu("KMonoBehaviour/scripts/DescriptorPanel")]
public class DescriptorPanel : KMonoBehaviour
{
	// Token: 0x06006425 RID: 25637 RVA: 0x00259B8B File Offset: 0x00257D8B
	public bool HasDescriptors()
	{
		return this.labels.Count > 0;
	}

	// Token: 0x06006426 RID: 25638 RVA: 0x00259B9C File Offset: 0x00257D9C
	public void SetDescriptors(IList<Descriptor> descriptors)
	{
		int i;
		for (i = 0; i < descriptors.Count; i++)
		{
			GameObject gameObject;
			if (i >= this.labels.Count)
			{
				gameObject = Util.KInstantiate((this.customLabelPrefab != null) ? this.customLabelPrefab : ScreenPrefabs.Instance.DescriptionLabel, base.gameObject, null);
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				this.labels.Add(gameObject);
			}
			else
			{
				gameObject = this.labels[i];
			}
			gameObject.GetComponent<LocText>().text = descriptors[i].IndentedText();
			gameObject.GetComponent<ToolTip>().toolTip = descriptors[i].tooltipText;
			gameObject.SetActive(true);
		}
		while (i < this.labels.Count)
		{
			this.labels[i].SetActive(false);
			i++;
		}
	}

	// Token: 0x0400443E RID: 17470
	[SerializeField]
	private GameObject customLabelPrefab;

	// Token: 0x0400443F RID: 17471
	private List<GameObject> labels = new List<GameObject>();
}
