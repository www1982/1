using System;
using System.Collections.Generic;

// Token: 0x020004F8 RID: 1272
public class ClosestEdibleSensor : Sensor
{
	// Token: 0x06001B45 RID: 6981 RVA: 0x00095EBC File Offset: 0x000940BC
	public ClosestEdibleSensor(Sensors sensors)
		: base(sensors)
	{
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x00095EC8 File Offset: 0x000940C8
	public override void Update()
	{
		HashSet<Tag> forbiddenTagSet = base.GetComponent<ConsumableConsumer>().forbiddenTagSet;
		Pickupable pickupable = Game.Instance.fetchManager.FindEdibleFetchTarget(base.GetComponent<Storage>(), forbiddenTagSet, ClosestEdibleSensor.requiredSearchTags);
		bool flag = this.edibleInReachButNotPermitted;
		Edible edible = null;
		bool flag2 = false;
		if (pickupable != null)
		{
			edible = pickupable.GetComponent<Edible>();
			flag2 = true;
			flag = false;
		}
		else
		{
			flag = Game.Instance.fetchManager.FindEdibleFetchTarget(base.GetComponent<Storage>(), new HashSet<Tag>(), ClosestEdibleSensor.requiredSearchTags) != null;
		}
		if (edible != this.edible || this.hasEdible != flag2)
		{
			this.edible = edible;
			this.hasEdible = flag2;
			this.edibleInReachButNotPermitted = flag;
			base.Trigger(86328522, this.edible);
		}
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x00095F85 File Offset: 0x00094185
	public Edible GetEdible()
	{
		return this.edible;
	}

	// Token: 0x0400100D RID: 4109
	private Edible edible;

	// Token: 0x0400100E RID: 4110
	private bool hasEdible;

	// Token: 0x0400100F RID: 4111
	public bool edibleInReachButNotPermitted;

	// Token: 0x04001010 RID: 4112
	public static Tag[] requiredSearchTags = new Tag[] { GameTags.Edible };
}
