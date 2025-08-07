using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200063F RID: 1599
public class BipedTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x06002690 RID: 9872 RVA: 0x000DBABC File Offset: 0x000D9CBC
	public BipedTransitionLayer(Navigator navigator, float floor_speed, float ladder_speed)
		: base(navigator)
	{
		navigator.Subscribe(1773898642, delegate(object data)
		{
			this.isWalking = true;
		});
		navigator.Subscribe(1597112836, delegate(object data)
		{
			this.isWalking = false;
		});
		this.floorSpeed = floor_speed;
		this.ladderSpeed = ladder_speed;
		this.jetPackSpeed = floor_speed;
		this.movementSpeed = Db.Get().AttributeConverters.MovementSpeed.Lookup(navigator.gameObject);
		this.attributeLevels = navigator.GetComponent<AttributeLevels>();
	}

	// Token: 0x06002691 RID: 9873 RVA: 0x000DBB44 File Offset: 0x000D9D44
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		float num = 1f;
		bool flag = (transition.start == NavType.Pole || transition.end == NavType.Pole) && transition.y < 0 && transition.x == 0;
		bool flag2 = transition.start == NavType.Tube || transition.end == NavType.Tube;
		bool flag3 = transition.start == NavType.Hover || transition.end == NavType.Hover;
		if (!flag && !flag2 && !flag3)
		{
			if (this.isWalking)
			{
				return;
			}
			num = this.GetMovementSpeedMultiplier();
		}
		int num2 = Grid.PosToCell(navigator);
		float num3 = 1f;
		bool flag4 = (navigator.flags & PathFinder.PotentialPath.Flags.HasAtmoSuit) > PathFinder.PotentialPath.Flags.None;
		bool flag5 = (navigator.flags & PathFinder.PotentialPath.Flags.HasJetPack) > PathFinder.PotentialPath.Flags.None;
		bool flag6 = (navigator.flags & PathFinder.PotentialPath.Flags.HasLeadSuit) > PathFinder.PotentialPath.Flags.None;
		if (!flag5 && !flag4 && !flag6 && Grid.IsSubstantialLiquid(num2, 0.35f))
		{
			num3 = 0.5f;
		}
		num *= num3;
		if (transition.x == 0 && (transition.start == NavType.Ladder || transition.start == NavType.Pole) && transition.start == transition.end)
		{
			if (flag)
			{
				transition.speed = 15f * num3;
			}
			else
			{
				transition.speed = this.ladderSpeed * num;
				GameObject gameObject = Grid.Objects[num2, 1];
				if (gameObject != null)
				{
					Ladder component = gameObject.GetComponent<Ladder>();
					if (component != null)
					{
						float num4 = component.upwardsMovementSpeedMultiplier;
						if (transition.y < 0)
						{
							num4 = component.downwardsMovementSpeedMultiplier;
						}
						transition.speed *= num4;
						transition.animSpeed *= num4;
					}
				}
			}
		}
		else if (flag2)
		{
			transition.speed = this.GetTubeTravellingSpeedMultiplier(navigator);
		}
		else if (flag3)
		{
			transition.speed = this.jetPackSpeed;
		}
		else
		{
			transition.speed = this.floorSpeed * num;
		}
		float num5 = num - 1f;
		transition.animSpeed += transition.animSpeed * num5 / 2f;
		if (transition.start == NavType.Floor && transition.end == NavType.Floor)
		{
			int num6 = Grid.CellBelow(num2);
			if (Grid.Foundation[num6])
			{
				GameObject gameObject2 = Grid.Objects[num6, 1];
				if (gameObject2 != null)
				{
					SimCellOccupier component2 = gameObject2.GetComponent<SimCellOccupier>();
					if (component2 != null)
					{
						transition.speed *= component2.movementSpeedMultiplier;
						transition.animSpeed *= component2.movementSpeedMultiplier;
					}
				}
			}
		}
		this.startTime = Time.time;
	}

	// Token: 0x06002692 RID: 9874 RVA: 0x000DBDC4 File Offset: 0x000D9FC4
	public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.EndTransition(navigator, transition);
		bool flag = (transition.start == NavType.Pole || transition.end == NavType.Pole) && transition.y < 0 && transition.x == 0;
		bool flag2 = transition.start == NavType.Tube || transition.end == NavType.Tube;
		if (!this.isWalking && !flag && !flag2 && this.attributeLevels != null)
		{
			this.attributeLevels.AddExperience(Db.Get().Attributes.Athletics.Id, Time.time - this.startTime, DUPLICANTSTATS.ATTRIBUTE_LEVELING.ALL_DAY_EXPERIENCE);
		}
	}

	// Token: 0x06002693 RID: 9875 RVA: 0x000DBE64 File Offset: 0x000DA064
	public float GetTubeTravellingSpeedMultiplier(Navigator navigator)
	{
		AttributeInstance attributeInstance = Db.Get().Attributes.TransitTubeTravelSpeed.Lookup(navigator.gameObject);
		if (attributeInstance != null)
		{
			return attributeInstance.GetTotalValue();
		}
		return DUPLICANTSTATS.STANDARD.BaseStats.TRANSIT_TUBE_TRAVEL_SPEED;
	}

	// Token: 0x06002694 RID: 9876 RVA: 0x000DBEA8 File Offset: 0x000DA0A8
	public static float GetMovementSpeedMultiplier(AttributeConverterInstance movementSpeed)
	{
		float num = 1f;
		if (movementSpeed != null)
		{
			num += movementSpeed.Evaluate();
		}
		return Mathf.Max(0.1f, num);
	}

	// Token: 0x06002695 RID: 9877 RVA: 0x000DBED4 File Offset: 0x000DA0D4
	public float GetMovementSpeedMultiplier()
	{
		return BipedTransitionLayer.GetMovementSpeedMultiplier(this.movementSpeed);
	}

	// Token: 0x04001688 RID: 5768
	private bool isWalking;

	// Token: 0x04001689 RID: 5769
	private float floorSpeed;

	// Token: 0x0400168A RID: 5770
	private float ladderSpeed;

	// Token: 0x0400168B RID: 5771
	private float startTime;

	// Token: 0x0400168C RID: 5772
	private float jetPackSpeed;

	// Token: 0x0400168D RID: 5773
	private const float downPoleSpeed = 15f;

	// Token: 0x0400168E RID: 5774
	private const float WATER_SPEED_PENALTY = 0.5f;

	// Token: 0x0400168F RID: 5775
	private AttributeConverterInstance movementSpeed;

	// Token: 0x04001690 RID: 5776
	private AttributeLevels attributeLevels;
}
