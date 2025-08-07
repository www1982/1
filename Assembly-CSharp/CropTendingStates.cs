using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class CropTendingStates : GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>
{
	// Token: 0x06000401 RID: 1025 RVA: 0x00021A38 File Offset: 0x0001FC38
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.findCrop;
		this.root.Exit(delegate(CropTendingStates.Instance smi)
		{
			this.UnreserveCrop(smi);
			if (!smi.tendedSucceeded)
			{
				this.RestoreSymbolsVisibility(smi);
			}
		});
		this.findCrop.Enter(delegate(CropTendingStates.Instance smi)
		{
			this.FindCrop(smi);
			if (smi.sm.targetCrop.Get(smi) == null)
			{
				smi.GoTo(this.behaviourcomplete);
				return;
			}
			this.ReserverCrop(smi);
			smi.GoTo(this.moveToCrop);
		});
		GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State state = this.moveToCrop;
		string text = CREATURES.STATUSITEMS.DIVERGENT_WILL_TEND.NAME;
		string text2 = CREATURES.STATUSITEMS.DIVERGENT_WILL_TEND.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory statusItemCategory = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, statusItemCategory).MoveTo((CropTendingStates.Instance smi) => smi.moveCell, this.tendCrop, this.behaviourcomplete, false).ParamTransition<GameObject>(this.targetCrop, this.behaviourcomplete, (CropTendingStates.Instance smi, GameObject p) => this.targetCrop.Get(smi) == null);
		GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State state2 = this.tendCrop.DefaultState(this.tendCrop.pre);
		string text4 = CREATURES.STATUSITEMS.DIVERGENT_TENDING.NAME;
		string text5 = CREATURES.STATUSITEMS.DIVERGENT_TENDING.TOOLTIP;
		string text6 = "";
		StatusItem.IconType iconType2 = StatusItem.IconType.Info;
		NotificationType notificationType2 = NotificationType.Neutral;
		bool flag2 = false;
		statusItemCategory = Db.Get().StatusItemCategories.Main;
		state2.ToggleStatusItem(text4, text5, text6, iconType2, notificationType2, flag2, default(HashedString), 129022, null, null, statusItemCategory).ParamTransition<GameObject>(this.targetCrop, this.behaviourcomplete, (CropTendingStates.Instance smi, GameObject p) => this.targetCrop.Get(smi) == null).Enter(delegate(CropTendingStates.Instance smi)
		{
			smi.animSet = this.GetCropTendingAnimSet(smi);
			this.StoreSymbolsVisibility(smi);
		});
		this.tendCrop.pre.Face(this.targetCrop, 0f).PlayAnim((CropTendingStates.Instance smi) => smi.animSet.crop_tending_pre, KAnim.PlayMode.Once).OnAnimQueueComplete(this.tendCrop.tend);
		this.tendCrop.tend.Enter(delegate(CropTendingStates.Instance smi)
		{
			this.SetSymbolsVisibility(smi, false);
		}).QueueAnim((CropTendingStates.Instance smi) => smi.animSet.crop_tending, false, null).OnAnimQueueComplete(this.tendCrop.pst);
		this.tendCrop.pst.QueueAnim((CropTendingStates.Instance smi) => smi.animSet.crop_tending_pst, false, null).OnAnimQueueComplete(this.behaviourcomplete).Exit(delegate(CropTendingStates.Instance smi)
		{
			GameObject gameObject = smi.sm.targetCrop.Get(smi);
			if (gameObject != null)
			{
				if (smi.effect != null)
				{
					gameObject.GetComponent<Effects>().Add(smi.effect, true);
				}
				smi.tendedSucceeded = true;
				CropTendingStates.CropTendingEventData cropTendingEventData = new CropTendingStates.CropTendingEventData
				{
					source = smi.gameObject,
					cropId = smi.sm.targetCrop.Get(smi).PrefabID()
				};
				smi.sm.targetCrop.Get(smi).Trigger(90606262, cropTendingEventData);
				smi.Trigger(90606262, cropTendingEventData);
			}
		});
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToTendCrops, false);
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00021CB4 File Offset: 0x0001FEB4
	private CropTendingStates.AnimSet GetCropTendingAnimSet(CropTendingStates.Instance smi)
	{
		CropTendingStates.AnimSet animSet;
		if (smi.def.animSetOverrides.TryGetValue(this.targetCrop.Get(smi).PrefabID(), out animSet))
		{
			return animSet;
		}
		return CropTendingStates.defaultAnimSet;
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x00021CF0 File Offset: 0x0001FEF0
	private void FindCrop(CropTendingStates.Instance smi)
	{
		Navigator component = smi.GetComponent<Navigator>();
		Crop crop = null;
		int num = Grid.InvalidCell;
		int num2 = 100;
		int num3 = -1;
		foreach (Crop crop2 in Components.Crops.GetWorldItems(smi.gameObject.GetMyWorldId(), false))
		{
			if (Vector2.SqrMagnitude(crop2.transform.position - smi.transform.position) <= 625f)
			{
				if (smi.effect != null)
				{
					Effects component2 = crop2.GetComponent<Effects>();
					if (component2 != null)
					{
						bool flag = false;
						for (int i = 0; i < smi.def.ignoreEffectGroup.Length; i++)
						{
							HashedString hashedString = smi.def.ignoreEffectGroup[i];
							if (component2.HasEffect(hashedString))
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							continue;
						}
					}
				}
				KPrefabID component3 = crop2.GetComponent<KPrefabID>();
				if (!component3.HasTag(GameTags.FullyGrown) && !component3.HasTag(GameTags.Creatures.ReservedByCreature))
				{
					int num4;
					smi.def.interests.TryGetValue(crop2.PrefabID(), out num4);
					if (num4 >= num3)
					{
						bool flag2 = num4 > num3;
						int num5 = Grid.PosToCell(crop2);
						int[] array = new int[]
						{
							Grid.CellLeft(num5),
							Grid.CellRight(num5)
						};
						if (component3.HasTag(GameTags.PlantedOnFloorVessel))
						{
							array = new int[]
							{
								Grid.CellLeft(num5),
								Grid.CellRight(num5),
								Grid.CellDownLeft(num5),
								Grid.CellDownRight(num5)
							};
						}
						int num6 = 100;
						int num7 = Grid.InvalidCell;
						for (int j = 0; j < array.Length; j++)
						{
							if (Grid.IsValidCell(array[j]))
							{
								int navigationCost = component.GetNavigationCost(array[j]);
								if (navigationCost != -1 && navigationCost < num6)
								{
									num6 = navigationCost;
									num7 = array[j];
								}
							}
						}
						if (num6 != -1 && num7 != Grid.InvalidCell && (flag2 || num6 < num2))
						{
							num = num7;
							num2 = num6;
							num3 = num4;
							crop = crop2;
						}
					}
				}
			}
		}
		GameObject gameObject = ((crop != null) ? crop.gameObject : null);
		smi.sm.targetCrop.Set(gameObject, smi, false);
		smi.moveCell = num;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x00021F64 File Offset: 0x00020164
	private void ReserverCrop(CropTendingStates.Instance smi)
	{
		GameObject gameObject = smi.sm.targetCrop.Get(smi);
		if (gameObject != null)
		{
			DebugUtil.Assert(!gameObject.HasTag(GameTags.Creatures.ReservedByCreature));
			gameObject.AddTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00021FAC File Offset: 0x000201AC
	private void UnreserveCrop(CropTendingStates.Instance smi)
	{
		GameObject gameObject = smi.sm.targetCrop.Get(smi);
		if (gameObject != null)
		{
			gameObject.RemoveTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00021FE0 File Offset: 0x000201E0
	private void SetSymbolsVisibility(CropTendingStates.Instance smi, bool isVisible)
	{
		if (this.targetCrop.Get(smi) != null)
		{
			string[] hide_symbols_after_pre = smi.animSet.hide_symbols_after_pre;
			if (hide_symbols_after_pre != null)
			{
				KAnimControllerBase component = this.targetCrop.Get(smi).GetComponent<KAnimControllerBase>();
				if (component != null)
				{
					foreach (string text in hide_symbols_after_pre)
					{
						component.SetSymbolVisiblity(text, isVisible);
					}
				}
			}
		}
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00022050 File Offset: 0x00020250
	private void StoreSymbolsVisibility(CropTendingStates.Instance smi)
	{
		if (this.targetCrop.Get(smi) != null)
		{
			string[] hide_symbols_after_pre = smi.animSet.hide_symbols_after_pre;
			if (hide_symbols_after_pre != null)
			{
				KAnimControllerBase component = this.targetCrop.Get(smi).GetComponent<KAnimControllerBase>();
				if (component != null)
				{
					smi.symbolStates = new bool[hide_symbols_after_pre.Length];
					for (int i = 0; i < hide_symbols_after_pre.Length; i++)
					{
						smi.symbolStates[i] = component.GetSymbolVisiblity(hide_symbols_after_pre[i]);
					}
				}
			}
		}
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x000220D0 File Offset: 0x000202D0
	private void RestoreSymbolsVisibility(CropTendingStates.Instance smi)
	{
		if (this.targetCrop.Get(smi) != null && smi.symbolStates != null)
		{
			string[] hide_symbols_after_pre = smi.animSet.hide_symbols_after_pre;
			if (hide_symbols_after_pre != null)
			{
				KAnimControllerBase component = this.targetCrop.Get(smi).GetComponent<KAnimControllerBase>();
				if (component != null)
				{
					for (int i = 0; i < hide_symbols_after_pre.Length; i++)
					{
						component.SetSymbolVisiblity(hide_symbols_after_pre[i], smi.symbolStates[i]);
					}
				}
			}
		}
	}

	// Token: 0x040002F6 RID: 758
	private const int MAX_NAVIGATE_DISTANCE = 100;

	// Token: 0x040002F7 RID: 759
	private const int MAX_SQR_EUCLIDEAN_DISTANCE = 625;

	// Token: 0x040002F8 RID: 760
	private static CropTendingStates.AnimSet defaultAnimSet = new CropTendingStates.AnimSet
	{
		crop_tending_pre = "crop_tending_pre",
		crop_tending = "crop_tending_loop",
		crop_tending_pst = "crop_tending_pst"
	};

	// Token: 0x040002F9 RID: 761
	public StateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.TargetParameter targetCrop;

	// Token: 0x040002FA RID: 762
	private GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State findCrop;

	// Token: 0x040002FB RID: 763
	private GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State moveToCrop;

	// Token: 0x040002FC RID: 764
	private CropTendingStates.TendingStates tendCrop;

	// Token: 0x040002FD RID: 765
	private GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State behaviourcomplete;

	// Token: 0x020010C0 RID: 4288
	public class AnimSet
	{
		// Token: 0x04006145 RID: 24901
		public string crop_tending_pre;

		// Token: 0x04006146 RID: 24902
		public string crop_tending;

		// Token: 0x04006147 RID: 24903
		public string crop_tending_pst;

		// Token: 0x04006148 RID: 24904
		public string[] hide_symbols_after_pre;
	}

	// Token: 0x020010C1 RID: 4289
	public class CropTendingEventData
	{
		// Token: 0x04006149 RID: 24905
		public GameObject source;

		// Token: 0x0400614A RID: 24906
		public Tag cropId;
	}

	// Token: 0x020010C2 RID: 4290
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400614B RID: 24907
		public string effectId;

		// Token: 0x0400614C RID: 24908
		public HashedString[] ignoreEffectGroup;

		// Token: 0x0400614D RID: 24909
		public Dictionary<Tag, int> interests = new Dictionary<Tag, int>();

		// Token: 0x0400614E RID: 24910
		public Dictionary<Tag, CropTendingStates.AnimSet> animSetOverrides = new Dictionary<Tag, CropTendingStates.AnimSet>();
	}

	// Token: 0x020010C3 RID: 4291
	public new class Instance : GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.GameInstance
	{
		// Token: 0x060080AB RID: 32939 RVA: 0x0032DBBC File Offset: 0x0032BDBC
		public Instance(Chore<CropTendingStates.Instance> chore, CropTendingStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToTendCrops);
			this.effect = Db.Get().effects.TryGet(base.smi.def.effectId);
		}

		// Token: 0x0400614F RID: 24911
		public Effect effect;

		// Token: 0x04006150 RID: 24912
		public int moveCell;

		// Token: 0x04006151 RID: 24913
		public CropTendingStates.AnimSet animSet;

		// Token: 0x04006152 RID: 24914
		public bool tendedSucceeded;

		// Token: 0x04006153 RID: 24915
		public bool[] symbolStates;
	}

	// Token: 0x020010C4 RID: 4292
	public class TendingStates : GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State
	{
		// Token: 0x04006154 RID: 24916
		public GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State pre;

		// Token: 0x04006155 RID: 24917
		public GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State tend;

		// Token: 0x04006156 RID: 24918
		public GameStateMachine<CropTendingStates, CropTendingStates.Instance, IStateMachineTarget, CropTendingStates.Def>.State pst;
	}
}
