using System;
using System.Collections.Generic;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x02000814 RID: 2068
public class ClusterFogOfWarManager : GameStateMachine<ClusterFogOfWarManager, ClusterFogOfWarManager.Instance, IStateMachineTarget, ClusterFogOfWarManager.Def>
{
	// Token: 0x0600388D RID: 14477 RVA: 0x00139BA4 File Offset: 0x00137DA4
	public override void InitializeStates(out StateMachine.BaseState defaultState)
	{
		defaultState = this.root;
		this.root.Enter(delegate(ClusterFogOfWarManager.Instance smi)
		{
			smi.Initialize();
		}).EventHandler(GameHashes.DiscoveredWorldsChanged, (ClusterFogOfWarManager.Instance smi) => Game.Instance, delegate(ClusterFogOfWarManager.Instance smi)
		{
			smi.UpdateRevealedCellsFromDiscoveredWorlds();
		});
	}

	// Token: 0x04002240 RID: 8768
	public const int AUTOMATIC_PEEK_RADIUS = 2;

	// Token: 0x0200177A RID: 6010
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200177B RID: 6011
	public new class Instance : GameStateMachine<ClusterFogOfWarManager, ClusterFogOfWarManager.Instance, IStateMachineTarget, ClusterFogOfWarManager.Def>.GameInstance
	{
		// Token: 0x06009940 RID: 39232 RVA: 0x00383BB0 File Offset: 0x00381DB0
		public Instance(IStateMachineTarget master, ClusterFogOfWarManager.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009941 RID: 39233 RVA: 0x00383BC5 File Offset: 0x00381DC5
		public void Initialize()
		{
			this.UpdateRevealedCellsFromDiscoveredWorlds();
			this.EnsureRevealedTilesHavePeek();
		}

		// Token: 0x06009942 RID: 39234 RVA: 0x00383BD3 File Offset: 0x00381DD3
		public ClusterRevealLevel GetCellRevealLevel(AxialI location)
		{
			if (this.GetRevealCompleteFraction(location) >= 1f)
			{
				return ClusterRevealLevel.Visible;
			}
			if (this.GetRevealCompleteFraction(location) > 0f)
			{
				return ClusterRevealLevel.Peeked;
			}
			return ClusterRevealLevel.Hidden;
		}

		// Token: 0x06009943 RID: 39235 RVA: 0x00383BF6 File Offset: 0x00381DF6
		public void DEBUG_REVEAL_ENTIRE_MAP()
		{
			this.RevealLocation(AxialI.ZERO, 100, 2);
		}

		// Token: 0x06009944 RID: 39236 RVA: 0x00383C06 File Offset: 0x00381E06
		public bool IsLocationRevealed(AxialI location)
		{
			return this.GetRevealCompleteFraction(location) >= 1f;
		}

		// Token: 0x06009945 RID: 39237 RVA: 0x00383C1C File Offset: 0x00381E1C
		private void EnsureRevealedTilesHavePeek()
		{
			foreach (KeyValuePair<AxialI, List<ClusterGridEntity>> keyValuePair in ClusterGrid.Instance.cellContents)
			{
				if (this.IsLocationRevealed(keyValuePair.Key))
				{
					this.PeekLocation(keyValuePair.Key, 2);
				}
			}
		}

		// Token: 0x06009946 RID: 39238 RVA: 0x00383C8C File Offset: 0x00381E8C
		public void PeekLocation(AxialI location, int radius)
		{
			foreach (AxialI axialI in AxialUtil.GetAllPointsWithinRadius(location, radius))
			{
				if (this.m_revealPointsByCell.ContainsKey(axialI))
				{
					this.m_revealPointsByCell[axialI] = Mathf.Max(this.m_revealPointsByCell[axialI], 0.01f);
				}
				else
				{
					this.m_revealPointsByCell[axialI] = 0.01f;
				}
			}
		}

		// Token: 0x06009947 RID: 39239 RVA: 0x00383D1C File Offset: 0x00381F1C
		public void RevealLocation(AxialI location, int radius = 0, int peekRadius = 2)
		{
			if (ClusterGrid.Instance.GetHiddenEntitiesOfLayerAtCell(location, EntityLayer.Asteroid).Count > 0 || ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(location, EntityLayer.Asteroid) != null)
			{
				radius = Mathf.Max(radius, 1);
			}
			bool flag = false;
			foreach (AxialI axialI in AxialUtil.GetAllPointsWithinRadius(location, radius))
			{
				flag |= this.RevealCellIfValid(axialI, peekRadius);
			}
			if (flag)
			{
				Game.Instance.Trigger(-1991583975, location);
			}
		}

		// Token: 0x06009948 RID: 39240 RVA: 0x00383DC0 File Offset: 0x00381FC0
		public void EarnRevealPointsForLocation(AxialI location, float points)
		{
			global::Debug.Assert(ClusterGrid.Instance.IsValidCell(location), string.Format("EarnRevealPointsForLocation called with invalid location: {0}", location));
			if (this.IsLocationRevealed(location))
			{
				return;
			}
			if (this.m_revealPointsByCell.ContainsKey(location))
			{
				Dictionary<AxialI, float> revealPointsByCell = this.m_revealPointsByCell;
				revealPointsByCell[location] += points;
			}
			else
			{
				this.m_revealPointsByCell[location] = points;
				Game.Instance.Trigger(-1554423969, location);
			}
			if (this.IsLocationRevealed(location))
			{
				this.RevealLocation(location, 0, 2);
				this.PeekLocation(location, 2);
				Game.Instance.Trigger(-1991583975, location);
			}
		}

		// Token: 0x06009949 RID: 39241 RVA: 0x00383E74 File Offset: 0x00382074
		public float GetRevealCompleteFraction(AxialI location)
		{
			if (!ClusterGrid.Instance.IsValidCell(location))
			{
				global::Debug.LogError(string.Format("GetRevealCompleteFraction called with invalid location: {0}, {1}", location.r, location.q));
			}
			if (DebugHandler.RevealFogOfWar)
			{
				return 1f;
			}
			float num;
			if (this.m_revealPointsByCell.TryGetValue(location, out num))
			{
				return Mathf.Min(num / ROCKETRY.CLUSTER_FOW.POINTS_TO_REVEAL, 1f);
			}
			return 0f;
		}

		// Token: 0x0600994A RID: 39242 RVA: 0x00383EE7 File Offset: 0x003820E7
		private bool RevealCellIfValid(AxialI cell, int peekRadius = 2)
		{
			if (!ClusterGrid.Instance.IsValidCell(cell))
			{
				return false;
			}
			if (this.IsLocationRevealed(cell))
			{
				return false;
			}
			this.m_revealPointsByCell[cell] = ROCKETRY.CLUSTER_FOW.POINTS_TO_REVEAL;
			this.PeekLocation(cell, peekRadius);
			return true;
		}

		// Token: 0x0600994B RID: 39243 RVA: 0x00383F20 File Offset: 0x00382120
		public bool GetUnrevealedLocationWithinRadius(AxialI center, int radius, out AxialI result)
		{
			for (int i = 0; i <= radius; i++)
			{
				foreach (AxialI axialI in AxialUtil.GetRing(center, i))
				{
					if (ClusterGrid.Instance.IsValidCell(axialI) && !this.IsLocationRevealed(axialI))
					{
						result = axialI;
						return true;
					}
				}
			}
			result = AxialI.ZERO;
			return false;
		}

		// Token: 0x0600994C RID: 39244 RVA: 0x00383FA8 File Offset: 0x003821A8
		public void UpdateRevealedCellsFromDiscoveredWorlds()
		{
			int num = (DlcManager.IsExpansion1Active() ? 0 : 2);
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				if (worldContainer.IsDiscovered && !DebugHandler.RevealFogOfWar)
				{
					this.RevealLocation(worldContainer.GetComponent<ClusterGridEntity>().Location, num, 2);
				}
			}
		}

		// Token: 0x040075D1 RID: 30161
		[Serialize]
		private Dictionary<AxialI, float> m_revealPointsByCell = new Dictionary<AxialI, float>();
	}
}
