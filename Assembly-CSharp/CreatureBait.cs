using System;
using KSerialization;

// Token: 0x02000CA4 RID: 3236
[SerializationConfig(MemberSerialization.OptIn)]
public class CreatureBait : StateMachineComponent<CreatureBait.StatesInstance>
{
	// Token: 0x0600638C RID: 25484 RVA: 0x002567E3 File Offset: 0x002549E3
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600638D RID: 25485 RVA: 0x002567EC File Offset: 0x002549EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Tag[] constructionElements = base.GetComponent<Deconstructable>().constructionElements;
		this.baitElement = ((constructionElements.Length > 1) ? constructionElements[1] : constructionElements[0]);
		base.gameObject.GetSMI<Lure.Instance>().SetActiveLures(new Tag[] { this.baitElement });
		base.smi.StartSM();
	}

	// Token: 0x040043B4 RID: 17332
	[Serialize]
	public Tag baitElement;

	// Token: 0x02001E70 RID: 7792
	public class StatesInstance : GameStateMachine<CreatureBait.States, CreatureBait.StatesInstance, CreatureBait, object>.GameInstance
	{
		// Token: 0x0600B06A RID: 45162 RVA: 0x003D293A File Offset: 0x003D0B3A
		public StatesInstance(CreatureBait master)
			: base(master)
		{
		}
	}

	// Token: 0x02001E71 RID: 7793
	public class States : GameStateMachine<CreatureBait.States, CreatureBait.StatesInstance, CreatureBait>
	{
		// Token: 0x0600B06B RID: 45163 RVA: 0x003D2944 File Offset: 0x003D0B44
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Baited, null).Enter(delegate(CreatureBait.StatesInstance smi)
			{
				KAnim.Build build = ElementLoader.FindElementByName(smi.master.baitElement.ToString()).substance.anim.GetData().build;
				KAnim.Build.Symbol symbol = build.GetSymbol(new KAnimHashedString(build.name));
				HashedString hashedString = "snapTo_bait";
				smi.GetComponent<SymbolOverrideController>().AddSymbolOverride(hashedString, symbol, 0);
			}).TagTransition(GameTags.LureUsed, this.destroy, false);
			this.destroy.PlayAnim("use").EventHandler(GameHashes.AnimQueueComplete, delegate(CreatureBait.StatesInstance smi)
			{
				Util.KDestroyGameObject(smi.master.gameObject);
			});
		}

		// Token: 0x04008D8E RID: 36238
		public GameStateMachine<CreatureBait.States, CreatureBait.StatesInstance, CreatureBait, object>.State idle;

		// Token: 0x04008D8F RID: 36239
		public GameStateMachine<CreatureBait.States, CreatureBait.StatesInstance, CreatureBait, object>.State destroy;
	}
}
