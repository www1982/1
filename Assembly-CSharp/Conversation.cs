using System;
using System.Collections.Generic;

// Token: 0x02000845 RID: 2117
public class Conversation
{
	// Token: 0x040023BC RID: 9148
	public List<MinionIdentity> minions = new List<MinionIdentity>();

	// Token: 0x040023BD RID: 9149
	public MinionIdentity lastTalked;

	// Token: 0x040023BE RID: 9150
	public ConversationType conversationType;

	// Token: 0x040023BF RID: 9151
	public float lastTalkedTime;

	// Token: 0x040023C0 RID: 9152
	public Conversation.Topic lastTopic;

	// Token: 0x040023C1 RID: 9153
	public int numUtterances;

	// Token: 0x020017C1 RID: 6081
	public enum ModeType
	{
		// Token: 0x040076E5 RID: 30437
		Query,
		// Token: 0x040076E6 RID: 30438
		Statement,
		// Token: 0x040076E7 RID: 30439
		Agreement,
		// Token: 0x040076E8 RID: 30440
		Disagreement,
		// Token: 0x040076E9 RID: 30441
		Musing,
		// Token: 0x040076EA RID: 30442
		Satisfaction,
		// Token: 0x040076EB RID: 30443
		Nominal,
		// Token: 0x040076EC RID: 30444
		Dissatisfaction,
		// Token: 0x040076ED RID: 30445
		Stressing,
		// Token: 0x040076EE RID: 30446
		Segue,
		// Token: 0x040076EF RID: 30447
		End
	}

	// Token: 0x020017C2 RID: 6082
	public class Mode
	{
		// Token: 0x06009A5F RID: 39519 RVA: 0x0038A3DB File Offset: 0x003885DB
		public Mode(Conversation.ModeType type, string voice, string icon, string mouth, string anim, bool newTopic = false)
		{
			this.type = type;
			this.voice = voice;
			this.mouth = mouth;
			this.anim = anim;
			this.icon = icon;
			this.newTopic = newTopic;
		}

		// Token: 0x040076F0 RID: 30448
		public Conversation.ModeType type;

		// Token: 0x040076F1 RID: 30449
		public string voice;

		// Token: 0x040076F2 RID: 30450
		public string mouth;

		// Token: 0x040076F3 RID: 30451
		public string anim;

		// Token: 0x040076F4 RID: 30452
		public string icon;

		// Token: 0x040076F5 RID: 30453
		public bool newTopic;
	}

	// Token: 0x020017C3 RID: 6083
	public class Topic
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06009A60 RID: 39520 RVA: 0x0038A410 File Offset: 0x00388610
		public static Dictionary<int, Conversation.Mode> Modes
		{
			get
			{
				if (Conversation.Topic._modes == null)
				{
					Conversation.Topic._modes = new Dictionary<int, Conversation.Mode>();
					foreach (Conversation.Mode mode in Conversation.Topic.modeList)
					{
						Conversation.Topic._modes[(int)mode.type] = mode;
					}
				}
				return Conversation.Topic._modes;
			}
		}

		// Token: 0x06009A61 RID: 39521 RVA: 0x0038A484 File Offset: 0x00388684
		public Topic(string topic, Conversation.ModeType mode)
		{
			this.topic = topic;
			this.mode = mode;
		}

		// Token: 0x040076F6 RID: 30454
		public static List<Conversation.Mode> modeList = new List<Conversation.Mode>
		{
			new Conversation.Mode(Conversation.ModeType.Query, "conversation_question", "mode_query", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Statement, "conversation_answer", "mode_statement", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Agreement, "conversation_answer", "mode_agreement", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Disagreement, "conversation_answer", "mode_disagreement", SpeechMonitor.PREFIX_SAD, "unhappy", false),
			new Conversation.Mode(Conversation.ModeType.Musing, "conversation_short", "mode_musing", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Satisfaction, "conversation_short", "mode_satisfaction", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Nominal, "conversation_short", "mode_nominal", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Dissatisfaction, "conversation_short", "mode_dissatisfaction", SpeechMonitor.PREFIX_SAD, "unhappy", false),
			new Conversation.Mode(Conversation.ModeType.Stressing, "conversation_short", "mode_stressing", SpeechMonitor.PREFIX_SAD, "unhappy", false),
			new Conversation.Mode(Conversation.ModeType.Segue, "conversation_question", "mode_segue", SpeechMonitor.PREFIX_HAPPY, "happy", true)
		};

		// Token: 0x040076F7 RID: 30455
		private static Dictionary<int, Conversation.Mode> _modes;

		// Token: 0x040076F8 RID: 30456
		public string topic;

		// Token: 0x040076F9 RID: 30457
		public Conversation.ModeType mode;
	}
}
