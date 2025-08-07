using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020009DF RID: 2527
public class ConversationMonitor : GameStateMachine<ConversationMonitor, ConversationMonitor.Instance, IStateMachineTarget, ConversationMonitor.Def>
{
	// Token: 0x06004A05 RID: 18949 RVA: 0x001ACC38 File Offset: 0x001AAE38
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventHandler(GameHashes.TopicDiscussed, delegate(ConversationMonitor.Instance smi, object obj)
		{
			smi.OnTopicDiscussed(obj);
		}).EventHandler(GameHashes.TopicDiscovered, delegate(ConversationMonitor.Instance smi, object obj)
		{
			smi.OnTopicDiscovered(obj);
		});
	}

	// Token: 0x040030CC RID: 12492
	private const int MAX_RECENT_TOPICS = 5;

	// Token: 0x040030CD RID: 12493
	private const int MAX_FAVOURITE_TOPICS = 5;

	// Token: 0x040030CE RID: 12494
	private const float FAVOURITE_CHANCE = 0.033333335f;

	// Token: 0x040030CF RID: 12495
	private const float LEARN_CHANCE = 0.33333334f;

	// Token: 0x02001A25 RID: 6693
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A26 RID: 6694
	[SerializationConfig(MemberSerialization.OptIn)]
	public new class Instance : GameStateMachine<ConversationMonitor, ConversationMonitor.Instance, IStateMachineTarget, ConversationMonitor.Def>.GameInstance
	{
		// Token: 0x0600A260 RID: 41568 RVA: 0x003A0F60 File Offset: 0x0039F160
		public Instance(IStateMachineTarget master, ConversationMonitor.Def def)
			: base(master, def)
		{
			this.recentTopics = new Queue<string>();
			this.favouriteTopics = new List<string> { ConversationMonitor.Instance.randomTopics[global::UnityEngine.Random.Range(0, ConversationMonitor.Instance.randomTopics.Count)] };
			this.personalTopics = new List<string>();
		}

		// Token: 0x0600A261 RID: 41569 RVA: 0x003A0FB8 File Offset: 0x0039F1B8
		public string GetATopic()
		{
			int num = this.recentTopics.Count + this.favouriteTopics.Count * 2 + this.personalTopics.Count;
			int num2 = global::UnityEngine.Random.Range(0, num);
			if (num2 < this.recentTopics.Count)
			{
				return this.recentTopics.Dequeue();
			}
			num2 -= this.recentTopics.Count;
			if (num2 < this.favouriteTopics.Count)
			{
				return this.favouriteTopics[num2];
			}
			num2 -= this.favouriteTopics.Count;
			if (num2 < this.favouriteTopics.Count)
			{
				return this.favouriteTopics[num2];
			}
			num2 -= this.favouriteTopics.Count;
			if (num2 < this.personalTopics.Count)
			{
				return this.personalTopics[num2];
			}
			return "";
		}

		// Token: 0x0600A262 RID: 41570 RVA: 0x003A1090 File Offset: 0x0039F290
		public void OnTopicDiscovered(object data)
		{
			string text = (string)data;
			if (!this.recentTopics.Contains(text))
			{
				this.recentTopics.Enqueue(text);
				if (this.recentTopics.Count > 5)
				{
					string text2 = this.recentTopics.Dequeue();
					this.TryMakeFavouriteTopic(text2);
				}
			}
		}

		// Token: 0x0600A263 RID: 41571 RVA: 0x003A10E0 File Offset: 0x0039F2E0
		public void OnTopicDiscussed(object data)
		{
			string text = (string)data;
			if (global::UnityEngine.Random.value < 0.33333334f)
			{
				this.OnTopicDiscovered(text);
			}
		}

		// Token: 0x0600A264 RID: 41572 RVA: 0x003A1108 File Offset: 0x0039F308
		private void TryMakeFavouriteTopic(string topic)
		{
			if (global::UnityEngine.Random.value < 0.033333335f)
			{
				if (this.favouriteTopics.Count < 5)
				{
					this.favouriteTopics.Add(topic);
					return;
				}
				this.favouriteTopics[global::UnityEngine.Random.Range(0, this.favouriteTopics.Count)] = topic;
			}
		}

		// Token: 0x04007ECF RID: 32463
		[Serialize]
		private Queue<string> recentTopics;

		// Token: 0x04007ED0 RID: 32464
		[Serialize]
		private List<string> favouriteTopics;

		// Token: 0x04007ED1 RID: 32465
		private List<string> personalTopics;

		// Token: 0x04007ED2 RID: 32466
		private static readonly List<string> randomTopics = new List<string> { "Headquarters" };
	}
}
