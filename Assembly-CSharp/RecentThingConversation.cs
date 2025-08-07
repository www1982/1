using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000848 RID: 2120
public class RecentThingConversation : ConversationType
{
	// Token: 0x06003A38 RID: 14904 RVA: 0x00143CAB File Offset: 0x00141EAB
	public RecentThingConversation()
	{
		this.id = "RecentThingConversation";
	}

	// Token: 0x06003A39 RID: 14905 RVA: 0x00143CC0 File Offset: 0x00141EC0
	public override void NewTarget(MinionIdentity speaker)
	{
		ConversationMonitor.Instance smi = speaker.GetSMI<ConversationMonitor.Instance>();
		this.target = smi.GetATopic();
	}

	// Token: 0x06003A3A RID: 14906 RVA: 0x00143CE0 File Offset: 0x00141EE0
	public override Conversation.Topic GetNextTopic(MinionIdentity speaker, Conversation.Topic lastTopic)
	{
		if (string.IsNullOrEmpty(this.target))
		{
			return null;
		}
		List<Conversation.ModeType> list;
		if (lastTopic == null)
		{
			list = new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.Statement,
				Conversation.ModeType.Musing
			};
		}
		else
		{
			list = RecentThingConversation.transitions[lastTopic.mode];
		}
		Conversation.ModeType modeType = list[global::UnityEngine.Random.Range(0, list.Count)];
		return new Conversation.Topic(this.target, modeType);
	}

	// Token: 0x06003A3B RID: 14907 RVA: 0x00143D4C File Offset: 0x00141F4C
	public override Sprite GetSprite(string topic)
	{
		global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(topic, "ui", true);
		if (uisprite != null)
		{
			return uisprite.first;
		}
		return null;
	}

	// Token: 0x040023C5 RID: 9157
	public static Dictionary<Conversation.ModeType, List<Conversation.ModeType>> transitions = new Dictionary<Conversation.ModeType, List<Conversation.ModeType>>
	{
		{
			Conversation.ModeType.Query,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Agreement,
				Conversation.ModeType.Disagreement,
				Conversation.ModeType.Musing
			}
		},
		{
			Conversation.ModeType.Statement,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Agreement,
				Conversation.ModeType.Disagreement,
				Conversation.ModeType.Query,
				Conversation.ModeType.Segue
			}
		},
		{
			Conversation.ModeType.Agreement,
			new List<Conversation.ModeType> { Conversation.ModeType.Satisfaction }
		},
		{
			Conversation.ModeType.Disagreement,
			new List<Conversation.ModeType> { Conversation.ModeType.Dissatisfaction }
		},
		{
			Conversation.ModeType.Musing,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.Statement,
				Conversation.ModeType.Segue
			}
		},
		{
			Conversation.ModeType.Satisfaction,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Segue,
				Conversation.ModeType.End
			}
		},
		{
			Conversation.ModeType.Nominal,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Segue,
				Conversation.ModeType.End
			}
		},
		{
			Conversation.ModeType.Dissatisfaction,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Segue,
				Conversation.ModeType.End
			}
		}
	};
}
