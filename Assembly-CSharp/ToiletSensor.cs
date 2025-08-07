using System;

// Token: 0x02000504 RID: 1284
public class ToiletSensor : Sensor
{
	// Token: 0x06001B7F RID: 7039 RVA: 0x00096AE3 File Offset: 0x00094CE3
	public ToiletSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = base.GetComponent<Navigator>();
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x00096AF8 File Offset: 0x00094CF8
	public override void Update()
	{
		IUsable usable = null;
		int num = int.MaxValue;
		bool flag = false;
		foreach (IUsable usable2 in Components.Toilets.Items)
		{
			if (usable2.IsUsable())
			{
				flag = true;
				int navigationCost = this.navigator.GetNavigationCost(Grid.PosToCell(usable2.transform.GetPosition()));
				if (navigationCost != -1 && navigationCost < num)
				{
					usable = usable2;
					num = navigationCost;
				}
			}
		}
		bool flag2 = Components.Toilets.Count > 0;
		if (usable != this.toilet || flag2 != this.areThereAnyToilets || this.areThereAnyUsableToilets != flag)
		{
			this.toilet = usable;
			this.areThereAnyToilets = flag2;
			this.areThereAnyUsableToilets = flag;
			base.Trigger(-752545459, null);
		}
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x00096BD8 File Offset: 0x00094DD8
	public bool AreThereAnyToilets()
	{
		return this.areThereAnyToilets;
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x00096BE0 File Offset: 0x00094DE0
	public bool AreThereAnyUsableToilets()
	{
		return this.areThereAnyUsableToilets;
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x00096BE8 File Offset: 0x00094DE8
	public IUsable GetNearestUsableToilet()
	{
		return this.toilet;
	}

	// Token: 0x04001032 RID: 4146
	private Navigator navigator;

	// Token: 0x04001033 RID: 4147
	private IUsable toilet;

	// Token: 0x04001034 RID: 4148
	private bool areThereAnyToilets;

	// Token: 0x04001035 RID: 4149
	private bool areThereAnyUsableToilets;
}
