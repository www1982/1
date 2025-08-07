using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000752 RID: 1874
public class LadderBed : GameStateMachine<LadderBed, LadderBed.Instance, IStateMachineTarget, LadderBed.Def>
{
	// Token: 0x06002FAD RID: 12205 RVA: 0x00111146 File Offset: 0x0010F346
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
	}

	// Token: 0x04001C5B RID: 7259
	public static string lightBedShakeSoundPath = GlobalAssets.GetSound("LadderBed_LightShake", false);

	// Token: 0x04001C5C RID: 7260
	public static string noDupeBedShakeSoundPath = GlobalAssets.GetSound("LadderBed_Shake", false);

	// Token: 0x04001C5D RID: 7261
	public static string LADDER_BED_COUNT_BELOW_PARAMETER = "bed_count";

	// Token: 0x0200162A RID: 5674
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400720E RID: 29198
		public CellOffset[] offsets;
	}

	// Token: 0x0200162B RID: 5675
	public new class Instance : GameStateMachine<LadderBed, LadderBed.Instance, IStateMachineTarget, LadderBed.Def>.GameInstance
	{
		// Token: 0x0600941F RID: 37919 RVA: 0x0036E7EC File Offset: 0x0036C9EC
		public Instance(IStateMachineTarget master, LadderBed.Def def)
			: base(master, def)
		{
			ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[40];
			this.m_cell = Grid.PosToCell(master.gameObject);
			foreach (CellOffset cellOffset in def.offsets)
			{
				int num = Grid.OffsetCell(this.m_cell, cellOffset);
				if (Grid.IsValidCell(this.m_cell) && Grid.IsValidCell(num))
				{
					this.m_partitionEntires.Add(GameScenePartitioner.Instance.Add("LadderBed.Constructor", base.gameObject, num, GameScenePartitioner.Instance.pickupablesChangedLayer, new Action<object>(this.OnMoverChanged)));
					this.OnMoverChanged(null);
				}
			}
			AttachableBuilding attachable = this.m_attachable;
			attachable.onAttachmentNetworkChanged = (Action<object>)Delegate.Combine(attachable.onAttachmentNetworkChanged, new Action<object>(this.OnAttachmentChanged));
			this.OnAttachmentChanged(null);
			base.Subscribe(-717201811, new Action<object>(this.OnSleepDisturbedByMovement));
			master.GetComponent<KAnimControllerBase>().GetLayering().GetLink()
				.syncTint = false;
		}

		// Token: 0x06009420 RID: 37920 RVA: 0x0036E904 File Offset: 0x0036CB04
		private void OnSleepDisturbedByMovement(object obj)
		{
			base.GetComponent<KAnimControllerBase>().Play("interrupt_light", KAnim.PlayMode.Once, 1f, 0f);
			EventInstance eventInstance = SoundEvent.BeginOneShot(LadderBed.lightBedShakeSoundPath, base.smi.transform.GetPosition(), 1f, false);
			eventInstance.setParameterByName(LadderBed.LADDER_BED_COUNT_BELOW_PARAMETER, (float)this.numBelow, false);
			SoundEvent.EndOneShot(eventInstance);
		}

		// Token: 0x06009421 RID: 37921 RVA: 0x0036E96E File Offset: 0x0036CB6E
		private void OnAttachmentChanged(object data)
		{
			this.numBelow = AttachableBuilding.CountAttachedBelow(this.m_attachable);
		}

		// Token: 0x06009422 RID: 37922 RVA: 0x0036E984 File Offset: 0x0036CB84
		private void OnMoverChanged(object obj)
		{
			Pickupable pickupable = obj as Pickupable;
			if (pickupable != null && pickupable.gameObject != null && pickupable.KPrefabID.HasTag(GameTags.BaseMinion) && pickupable.GetComponent<Navigator>().CurrentNavType == NavType.Ladder)
			{
				if (this.m_sleepable.worker == null)
				{
					base.GetComponent<KAnimControllerBase>().Play("interrupt_light_nodupe", KAnim.PlayMode.Once, 1f, 0f);
					EventInstance eventInstance = SoundEvent.BeginOneShot(LadderBed.noDupeBedShakeSoundPath, base.smi.transform.GetPosition(), 1f, false);
					eventInstance.setParameterByName(LadderBed.LADDER_BED_COUNT_BELOW_PARAMETER, (float)this.numBelow, false);
					SoundEvent.EndOneShot(eventInstance);
					return;
				}
				if (pickupable.gameObject != this.m_sleepable.worker.gameObject)
				{
					this.m_sleepable.worker.Trigger(-717201811, null);
				}
			}
		}

		// Token: 0x06009423 RID: 37923 RVA: 0x0036EA80 File Offset: 0x0036CC80
		protected override void OnCleanUp()
		{
			foreach (HandleVector<int>.Handle handle in this.m_partitionEntires)
			{
				GameScenePartitioner.Instance.Free(ref handle);
			}
			AttachableBuilding attachable = this.m_attachable;
			attachable.onAttachmentNetworkChanged = (Action<object>)Delegate.Remove(attachable.onAttachmentNetworkChanged, new Action<object>(this.OnAttachmentChanged));
			base.OnCleanUp();
		}

		// Token: 0x0400720F RID: 29199
		private List<HandleVector<int>.Handle> m_partitionEntires = new List<HandleVector<int>.Handle>();

		// Token: 0x04007210 RID: 29200
		private int m_cell;

		// Token: 0x04007211 RID: 29201
		[MyCmpGet]
		private Ownable m_ownable;

		// Token: 0x04007212 RID: 29202
		[MyCmpGet]
		private Sleepable m_sleepable;

		// Token: 0x04007213 RID: 29203
		[MyCmpGet]
		private AttachableBuilding m_attachable;

		// Token: 0x04007214 RID: 29204
		private int numBelow;
	}
}
