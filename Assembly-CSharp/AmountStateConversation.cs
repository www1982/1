using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x02000849 RID: 2121
public class AmountStateConversation : ConversationType
{
	// Token: 0x06003A3D RID: 14909 RVA: 0x00143E71 File Offset: 0x00142071
	public AmountStateConversation()
	{
		this.id = "AmountStateConversation";
	}

	// Token: 0x06003A3E RID: 14910 RVA: 0x00143E84 File Offset: 0x00142084
	public override void NewTarget(MinionIdentity speaker)
	{
		this.target = AmountStateConversation.targets[global::UnityEngine.Random.Range(0, AmountStateConversation.targets.Count)];
	}

	// Token: 0x06003A3F RID: 14911 RVA: 0x00143EA8 File Offset: 0x001420A8
	public override Conversation.Topic GetNextTopic(MinionIdentity speaker, Conversation.Topic lastTopic)
	{
		if (lastTopic == null)
		{
			return new Conversation.Topic(this.target, Conversation.ModeType.Query);
		}
		List<Conversation.ModeType> list = AmountStateConversation.transitions[lastTopic.mode];
		Conversation.ModeType modeType = list[global::UnityEngine.Random.Range(0, list.Count)];
		if (modeType == Conversation.ModeType.Statement)
		{
			Conversation.ModeType modeForAmount = this.GetModeForAmount(speaker, this.target);
			return new Conversation.Topic(this.target, modeForAmount);
		}
		return new Conversation.Topic(this.target, modeType);
	}

	// Token: 0x06003A40 RID: 14912 RVA: 0x00143F14 File Offset: 0x00142114
	public override Sprite GetSprite(string topic)
	{
		if (Db.Get().Amounts.Exists(topic))
		{
			return Assets.GetSprite(Db.Get().Amounts.Get(topic).thoughtSprite);
		}
		if (Db.Get().Attributes.Exists(topic))
		{
			return Assets.GetSprite(Db.Get().Attributes.Get(topic).thoughtSprite);
		}
		return null;
	}

	// Token: 0x06003A41 RID: 14913 RVA: 0x00143F88 File Offset: 0x00142188
	private Conversation.ModeType GetModeForAmount(MinionIdentity speaker, string target)
	{
		if (target == Db.Get().Amounts.Stress.Id)
		{
			AmountInstance amountInstance = Db.Get().Amounts.Stress.Lookup(speaker);
			float num = amountInstance.value / amountInstance.GetMax();
			if (num < 0.1f)
			{
				return Conversation.ModeType.Satisfaction;
			}
			if (num > 0.6f)
			{
				return Conversation.ModeType.Dissatisfaction;
			}
		}
		else if (target == Db.Get().Attributes.QualityOfLife.Id)
		{
			AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup(speaker);
			AttributeInstance attributeInstance2 = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(speaker);
			float num2 = attributeInstance.GetTotalValue() - attributeInstance2.GetTotalValue();
			if (num2 > 0f)
			{
				return Conversation.ModeType.Satisfaction;
			}
			if (num2 < 0f)
			{
				return Conversation.ModeType.Dissatisfaction;
			}
		}
		else if (target == Db.Get().Amounts.HitPoints.Id)
		{
			AmountInstance amountInstance2 = Db.Get().Amounts.HitPoints.Lookup(speaker);
			float num3 = amountInstance2.value / amountInstance2.GetMax();
			if (num3 >= 1f)
			{
				return Conversation.ModeType.Satisfaction;
			}
			if (num3 < 0.8f)
			{
				return Conversation.ModeType.Dissatisfaction;
			}
		}
		else if (target == Db.Get().Amounts.Calories.Id)
		{
			AmountInstance amountInstance3 = Db.Get().Amounts.Calories.Lookup(speaker);
			if (amountInstance3 == null)
			{
				return Conversation.ModeType.Query;
			}
			float num4 = amountInstance3.value / amountInstance3.GetMax();
			if (num4 > 0.85f)
			{
				return Conversation.ModeType.Satisfaction;
			}
			if (num4 < 0.5f)
			{
				return Conversation.ModeType.Dissatisfaction;
			}
		}
		else if (target == Db.Get().Amounts.Stamina.Id)
		{
			AmountInstance amountInstance4 = Db.Get().Amounts.Stamina.Lookup(speaker);
			if (amountInstance4 == null)
			{
				return Conversation.ModeType.Query;
			}
			float num5 = amountInstance4.value / amountInstance4.GetMax();
			if (num5 > 0.5f)
			{
				return Conversation.ModeType.Satisfaction;
			}
			if (num5 < 0.2f)
			{
				return Conversation.ModeType.Dissatisfaction;
			}
		}
		else if (target == Db.Get().Amounts.ImmuneLevel.Id)
		{
			AmountInstance amountInstance5 = Db.Get().Amounts.ImmuneLevel.Lookup(speaker);
			float num6 = amountInstance5.value / amountInstance5.GetMax();
			if (num6 > 0.9f)
			{
				return Conversation.ModeType.Satisfaction;
			}
			if (num6 < 0.5f)
			{
				return Conversation.ModeType.Dissatisfaction;
			}
		}
		return Conversation.ModeType.Nominal;
	}

	// Token: 0x040023C6 RID: 9158
	public static Dictionary<Conversation.ModeType, List<Conversation.ModeType>> transitions = new Dictionary<Conversation.ModeType, List<Conversation.ModeType>>
	{
		{
			Conversation.ModeType.Query,
			new List<Conversation.ModeType> { Conversation.ModeType.Statement }
		},
		{
			Conversation.ModeType.Satisfaction,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Agreement,
				Conversation.ModeType.Statement
			}
		},
		{
			Conversation.ModeType.Nominal,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Musing,
				Conversation.ModeType.Statement
			}
		},
		{
			Conversation.ModeType.Dissatisfaction,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Disagreement,
				Conversation.ModeType.Statement
			}
		},
		{
			Conversation.ModeType.Agreement,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.End
			}
		},
		{
			Conversation.ModeType.Disagreement,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.End
			}
		},
		{
			Conversation.ModeType.Musing,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.End
			}
		}
	};

	// Token: 0x040023C7 RID: 9159
	public static List<string> targets = new List<string> { "Stress", "QualityOfLife", "HitPoints", "Calories", "Stamina", "ImmuneLevel" };
}
