using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FFD RID: 4093
	public class SimpleEvent : GameplayEvent<SimpleEvent.StatesInstance>
	{
		// Token: 0x06007E42 RID: 32322 RVA: 0x003287EF File Offset: 0x003269EF
		public SimpleEvent(string id, string title, string description, string animFileName, string buttonText = null, string buttonTooltip = null)
			: base(id, 0, 0, null, null)
		{
			this.title = title;
			this.description = description;
			this.buttonText = buttonText;
			this.buttonTooltip = buttonTooltip;
			this.animFileName = animFileName;
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x00328827 File Offset: 0x00326A27
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new SimpleEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x04005F4B RID: 24395
		private string buttonText;

		// Token: 0x04005F4C RID: 24396
		private string buttonTooltip;

		// Token: 0x020025E5 RID: 9701
		public class States : GameplayEventStateMachine<SimpleEvent.States, SimpleEvent.StatesInstance, GameplayEventManager, SimpleEvent>
		{
			// Token: 0x0600C207 RID: 49671 RVA: 0x00409AC6 File Offset: 0x00407CC6
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.root;
				this.ending.ReturnSuccess();
			}

			// Token: 0x0600C208 RID: 49672 RVA: 0x00409ADC File Offset: 0x00407CDC
			public override EventInfoData GenerateEventPopupData(SimpleEvent.StatesInstance smi)
			{
				EventInfoData eventInfoData = new EventInfoData(smi.gameplayEvent.title, smi.gameplayEvent.description, smi.gameplayEvent.animFileName);
				eventInfoData.minions = smi.minions;
				eventInfoData.artifact = smi.artifact;
				EventInfoData.Option option = eventInfoData.AddOption(smi.gameplayEvent.buttonText, null);
				option.callback = delegate
				{
					if (smi.callback != null)
					{
						smi.callback();
					}
					smi.StopSM("SimpleEvent Finished");
				};
				option.tooltip = smi.gameplayEvent.buttonTooltip;
				if (smi.textParameters != null)
				{
					foreach (global::Tuple<string, string> tuple in smi.textParameters)
					{
						eventInfoData.SetTextParameter(tuple.first, tuple.second);
					}
				}
				return eventInfoData;
			}

			// Token: 0x0400A920 RID: 43296
			public GameStateMachine<SimpleEvent.States, SimpleEvent.StatesInstance, GameplayEventManager, object>.State ending;
		}

		// Token: 0x020025E6 RID: 9702
		public class StatesInstance : GameplayEventStateMachine<SimpleEvent.States, SimpleEvent.StatesInstance, GameplayEventManager, SimpleEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x0600C20A RID: 49674 RVA: 0x00409BF8 File Offset: 0x00407DF8
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, SimpleEvent simpleEvent)
				: base(master, eventInstance, simpleEvent)
			{
			}

			// Token: 0x0600C20B RID: 49675 RVA: 0x00409C03 File Offset: 0x00407E03
			public void SetTextParameter(string key, string value)
			{
				if (this.textParameters == null)
				{
					this.textParameters = new List<global::Tuple<string, string>>();
				}
				this.textParameters.Add(new global::Tuple<string, string>(key, value));
			}

			// Token: 0x0600C20C RID: 49676 RVA: 0x00409C2A File Offset: 0x00407E2A
			public void ShowEventPopup()
			{
				EventInfoScreen.ShowPopup(base.smi.sm.GenerateEventPopupData(base.smi));
			}

			// Token: 0x0400A921 RID: 43297
			public GameObject[] minions;

			// Token: 0x0400A922 RID: 43298
			public GameObject artifact;

			// Token: 0x0400A923 RID: 43299
			public List<global::Tuple<string, string>> textParameters;

			// Token: 0x0400A924 RID: 43300
			public global::System.Action callback;
		}
	}
}
