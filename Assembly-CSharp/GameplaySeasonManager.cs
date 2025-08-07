using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using KSerialization;

// Token: 0x02000932 RID: 2354
public class GameplaySeasonManager : GameStateMachine<GameplaySeasonManager, GameplaySeasonManager.Instance, IStateMachineTarget, GameplaySeasonManager.Def>
{
	// Token: 0x060042C8 RID: 17096 RVA: 0x0017FB98 File Offset: 0x0017DD98
	public override void InitializeStates(out StateMachine.BaseState defaultState)
	{
		defaultState = this.root;
		this.root.Enter(delegate(GameplaySeasonManager.Instance smi)
		{
			smi.Initialize();
		}).Update(delegate(GameplaySeasonManager.Instance smi, float dt)
		{
			smi.Update(dt);
		}, UpdateRate.SIM_4000ms, false);
	}

	// Token: 0x0200190E RID: 6414
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200190F RID: 6415
	public new class Instance : GameStateMachine<GameplaySeasonManager, GameplaySeasonManager.Instance, IStateMachineTarget, GameplaySeasonManager.Def>.GameInstance
	{
		// Token: 0x06009E35 RID: 40501 RVA: 0x003959FD File Offset: 0x00393BFD
		public Instance(IStateMachineTarget master, GameplaySeasonManager.Def def)
			: base(master, def)
		{
			this.activeSeasons = new List<GameplaySeasonInstance>();
		}

		// Token: 0x06009E36 RID: 40502 RVA: 0x00395A14 File Offset: 0x00393C14
		public void Initialize()
		{
			this.activeSeasons.RemoveAll((GameplaySeasonInstance item) => item.Season == null);
			List<GameplaySeason> list = new List<GameplaySeason>();
			if (this.m_worldContainer != null)
			{
				ClusterGridEntity component = base.GetComponent<ClusterGridEntity>();
				using (List<string>.Enumerator enumerator = this.m_worldContainer.GetSeasonIds().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						GameplaySeason gameplaySeason = Db.Get().GameplaySeasons.TryGet(text);
						if (gameplaySeason == null)
						{
							Debug.LogWarning("world " + component.name + " has invalid season " + text);
						}
						else
						{
							if (gameplaySeason.type != GameplaySeason.Type.World)
							{
								Debug.LogWarning(string.Concat(new string[] { "world ", component.name, " has specified season ", text, ", which is not a world type season" }));
							}
							list.Add(gameplaySeason);
						}
					}
					goto IL_0146;
				}
			}
			Debug.Assert(base.GetComponent<SaveGame>() != null);
			list = Db.Get().GameplaySeasons.resources.Where((GameplaySeason season) => season.type == GameplaySeason.Type.Cluster).ToList<GameplaySeason>();
			IL_0146:
			foreach (GameplaySeason gameplaySeason2 in list)
			{
				if (Game.IsCorrectDlcActiveForCurrentSave(gameplaySeason2) && gameplaySeason2.startActive && !this.SeasonExists(gameplaySeason2) && gameplaySeason2.events.Count > 0)
				{
					this.activeSeasons.Add(gameplaySeason2.Instantiate(this.GetWorldId()));
				}
			}
			foreach (GameplaySeasonInstance gameplaySeasonInstance in new List<GameplaySeasonInstance>(this.activeSeasons))
			{
				if (!list.Contains(gameplaySeasonInstance.Season) || !Game.IsCorrectDlcActiveForCurrentSave(gameplaySeasonInstance.Season))
				{
					this.activeSeasons.Remove(gameplaySeasonInstance);
				}
			}
		}

		// Token: 0x06009E37 RID: 40503 RVA: 0x00395C60 File Offset: 0x00393E60
		private int GetWorldId()
		{
			if (this.m_worldContainer != null)
			{
				return this.m_worldContainer.id;
			}
			return -1;
		}

		// Token: 0x06009E38 RID: 40504 RVA: 0x00395C80 File Offset: 0x00393E80
		public void Update(float dt)
		{
			foreach (GameplaySeasonInstance gameplaySeasonInstance in this.activeSeasons)
			{
				if (gameplaySeasonInstance.ShouldGenerateEvents() && GameUtil.GetCurrentTimeInCycles() > gameplaySeasonInstance.NextEventTime)
				{
					int num = 0;
					while (num < gameplaySeasonInstance.Season.numEventsToStartEachPeriod && gameplaySeasonInstance.StartEvent(false))
					{
						num++;
					}
				}
			}
		}

		// Token: 0x06009E39 RID: 40505 RVA: 0x00395D00 File Offset: 0x00393F00
		public void StartNewSeason(GameplaySeason seasonType)
		{
			if (Game.IsCorrectDlcActiveForCurrentSave(seasonType))
			{
				this.activeSeasons.Add(seasonType.Instantiate(this.GetWorldId()));
			}
		}

		// Token: 0x06009E3A RID: 40506 RVA: 0x00395D24 File Offset: 0x00393F24
		public bool SeasonExists(GameplaySeason seasonType)
		{
			return this.activeSeasons.Find((GameplaySeasonInstance e) => e.Season.IdHash == seasonType.IdHash) != null;
		}

		// Token: 0x04007B10 RID: 31504
		[Serialize]
		public List<GameplaySeasonInstance> activeSeasons;

		// Token: 0x04007B11 RID: 31505
		[MyCmpGet]
		private WorldContainer m_worldContainer;
	}
}
