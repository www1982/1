using System;
using UnityEngine;

// Token: 0x020008C1 RID: 2241
public struct DiseaseContainer
{
	// Token: 0x06003E12 RID: 15890 RVA: 0x0015B3BC File Offset: 0x001595BC
	public DiseaseContainer(GameObject go, ushort elemIdx)
	{
		this.elemIdx = elemIdx;
		this.isContainer = go.GetComponent<IUserControlledCapacity>() != null && go.GetComponent<Storage>() != null;
		Conduit component = go.GetComponent<Conduit>();
		if (component != null)
		{
			this.conduitType = component.type;
		}
		else
		{
			this.conduitType = ConduitType.None;
		}
		this.controller = go.GetComponent<KBatchedAnimController>();
		this.overpopulationCount = 1;
		this.instanceGrowthRate = 1f;
		this.accumulatedError = 0f;
		this.visualDiseaseProvider = null;
		this.autoDisinfectable = go.GetComponent<AutoDisinfectable>();
		if (this.autoDisinfectable != null)
		{
			AutoDisinfectableManager.Instance.AddAutoDisinfectable(this.autoDisinfectable);
		}
	}

	// Token: 0x06003E13 RID: 15891 RVA: 0x0015B46C File Offset: 0x0015966C
	public void Clear()
	{
		this.controller = null;
	}

	// Token: 0x0400262E RID: 9774
	public AutoDisinfectable autoDisinfectable;

	// Token: 0x0400262F RID: 9775
	public ushort elemIdx;

	// Token: 0x04002630 RID: 9776
	public bool isContainer;

	// Token: 0x04002631 RID: 9777
	public ConduitType conduitType;

	// Token: 0x04002632 RID: 9778
	public KBatchedAnimController controller;

	// Token: 0x04002633 RID: 9779
	public GameObject visualDiseaseProvider;

	// Token: 0x04002634 RID: 9780
	public int overpopulationCount;

	// Token: 0x04002635 RID: 9781
	public float instanceGrowthRate;

	// Token: 0x04002636 RID: 9782
	public float accumulatedError;
}
