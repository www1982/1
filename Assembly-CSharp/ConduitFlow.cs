using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Klei;
using KSerialization;
using UnityEngine;

// Token: 0x02000841 RID: 2113
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{conduitType}")]
public class ConduitFlow : IConduitFlow
{
	// Token: 0x14000014 RID: 20
	// (add) Token: 0x060039DA RID: 14810 RVA: 0x00141278 File Offset: 0x0013F478
	// (remove) Token: 0x060039DB RID: 14811 RVA: 0x001412B0 File Offset: 0x0013F4B0
	public event global::System.Action onConduitsRebuilt;

	// Token: 0x060039DC RID: 14812 RVA: 0x001412E8 File Offset: 0x0013F4E8
	public void AddConduitUpdater(Action<float> callback, ConduitFlowPriority priority = ConduitFlowPriority.Default)
	{
		this.conduitUpdaters.Add(new ConduitFlow.ConduitUpdater
		{
			priority = priority,
			callback = callback
		});
		this.dirtyConduitUpdaters = true;
	}

	// Token: 0x060039DD RID: 14813 RVA: 0x00141320 File Offset: 0x0013F520
	public void RemoveConduitUpdater(Action<float> callback)
	{
		for (int i = 0; i < this.conduitUpdaters.Count; i++)
		{
			if (this.conduitUpdaters[i].callback == callback)
			{
				this.conduitUpdaters.RemoveAt(i);
				this.dirtyConduitUpdaters = true;
				return;
			}
		}
	}

	// Token: 0x060039DE RID: 14814 RVA: 0x00141370 File Offset: 0x0013F570
	private static ConduitFlow.FlowDirections ComputeNextFlowDirection(ConduitFlow.FlowDirections current)
	{
		switch (current)
		{
		case ConduitFlow.FlowDirections.None:
		case ConduitFlow.FlowDirections.Up:
			return ConduitFlow.FlowDirections.Down;
		case ConduitFlow.FlowDirections.Down:
			return ConduitFlow.FlowDirections.Left;
		case ConduitFlow.FlowDirections.Left:
			return ConduitFlow.FlowDirections.Right;
		case ConduitFlow.FlowDirections.Right:
			return ConduitFlow.FlowDirections.Up;
		}
		global::Debug.Assert(false, "multiple bits are set in 'FlowDirections'...can't compute next direction");
		return ConduitFlow.FlowDirections.None;
	}

	// Token: 0x060039DF RID: 14815 RVA: 0x001413BD File Offset: 0x0013F5BD
	public static ConduitFlow.FlowDirections Invert(ConduitFlow.FlowDirections directions)
	{
		return ConduitFlow.FlowDirections.All & ~directions;
	}

	// Token: 0x060039E0 RID: 14816 RVA: 0x001413C8 File Offset: 0x0013F5C8
	public static ConduitFlow.FlowDirections Opposite(ConduitFlow.FlowDirections directions)
	{
		ConduitFlow.FlowDirections flowDirections = ConduitFlow.FlowDirections.None;
		if ((directions & ConduitFlow.FlowDirections.Left) != ConduitFlow.FlowDirections.None)
		{
			flowDirections = ConduitFlow.FlowDirections.Right;
		}
		else if ((directions & ConduitFlow.FlowDirections.Right) != ConduitFlow.FlowDirections.None)
		{
			flowDirections = ConduitFlow.FlowDirections.Left;
		}
		else if ((directions & ConduitFlow.FlowDirections.Up) != ConduitFlow.FlowDirections.None)
		{
			flowDirections = ConduitFlow.FlowDirections.Down;
		}
		else if ((directions & ConduitFlow.FlowDirections.Down) != ConduitFlow.FlowDirections.None)
		{
			flowDirections = ConduitFlow.FlowDirections.Up;
		}
		return flowDirections;
	}

	// Token: 0x060039E1 RID: 14817 RVA: 0x001413FC File Offset: 0x0013F5FC
	public ConduitFlow(ConduitType conduit_type, int num_cells, IUtilityNetworkMgr network_mgr, float max_conduit_mass, float initial_elapsed_time)
	{
		this.elapsedTime = initial_elapsed_time;
		this.conduitType = conduit_type;
		this.networkMgr = network_mgr;
		this.MaxMass = max_conduit_mass;
		this.Initialize(num_cells);
		network_mgr.AddNetworksRebuiltListener(new Action<IList<UtilityNetwork>, ICollection<int>>(this.OnUtilityNetworksRebuilt));
	}

	// Token: 0x060039E2 RID: 14818 RVA: 0x001414AC File Offset: 0x0013F6AC
	public void Initialize(int num_cells)
	{
		this.grid = new ConduitFlow.GridNode[num_cells];
		for (int i = 0; i < num_cells; i++)
		{
			this.grid[i].conduitIdx = -1;
			this.grid[i].contents.element = SimHashes.Vacuum;
			this.grid[i].contents.diseaseIdx = byte.MaxValue;
		}
	}

	// Token: 0x060039E3 RID: 14819 RVA: 0x0014151C File Offset: 0x0013F71C
	private void OnUtilityNetworksRebuilt(IList<UtilityNetwork> networks, ICollection<int> root_nodes)
	{
		this.RebuildConnections(root_nodes);
		int num = this.networks.Count - networks.Count;
		if (0 < this.networks.Count - networks.Count)
		{
			this.networks.RemoveRange(networks.Count, num);
		}
		global::Debug.Assert(this.networks.Count <= networks.Count);
		for (int num2 = 0; num2 != networks.Count; num2++)
		{
			if (num2 < this.networks.Count)
			{
				this.networks[num2] = new ConduitFlow.Network
				{
					network = (FlowUtilityNetwork)networks[num2],
					cells = this.networks[num2].cells
				};
				this.networks[num2].cells.Clear();
			}
			else
			{
				this.networks.Add(new ConduitFlow.Network
				{
					network = (FlowUtilityNetwork)networks[num2],
					cells = new List<int>()
				});
			}
		}
		this.build_network_job.Reset(this);
		foreach (ConduitFlow.Network network in this.networks)
		{
			this.build_network_job.Add(new ConduitFlow.BuildNetworkTask(network, this.soaInfo.NumEntries));
		}
		GlobalJobManager.Run(this.build_network_job);
		for (int num3 = 0; num3 != this.build_network_job.Count; num3++)
		{
			this.build_network_job.GetWorkItem(num3).Finish();
		}
	}

	// Token: 0x060039E4 RID: 14820 RVA: 0x001416DC File Offset: 0x0013F8DC
	private void RebuildConnections(IEnumerable<int> root_nodes)
	{
		ConduitFlow.ConnectContext connectContext = new ConduitFlow.ConnectContext(this);
		this.soaInfo.Clear(this);
		this.replacements.ExceptWith(root_nodes);
		ObjectLayer objectLayer = ((this.conduitType == ConduitType.Gas) ? ObjectLayer.GasConduit : ObjectLayer.LiquidConduit);
		foreach (int num in root_nodes)
		{
			GameObject gameObject = Grid.Objects[num, (int)objectLayer];
			if (!(gameObject == null))
			{
				global::Conduit component = gameObject.GetComponent<global::Conduit>();
				if (!(component != null) || !component.IsDisconnected())
				{
					int num2 = this.soaInfo.AddConduit(this, gameObject, num);
					this.grid[num].conduitIdx = num2;
					connectContext.cells.Add(num);
				}
			}
		}
		Game.Instance.conduitTemperatureManager.Sim200ms(0f);
		this.connect_job.Reset(connectContext);
		int num3 = 256;
		for (int i = 0; i < connectContext.cells.Count; i += num3)
		{
			this.connect_job.Add(new ConduitFlow.ConnectTask(i, Mathf.Min(i + num3, connectContext.cells.Count)));
		}
		GlobalJobManager.Run(this.connect_job);
		connectContext.Finish();
		if (this.onConduitsRebuilt != null)
		{
			this.onConduitsRebuilt();
		}
	}

	// Token: 0x060039E5 RID: 14821 RVA: 0x00141844 File Offset: 0x0013FA44
	private ConduitFlow.FlowDirections GetDirection(ConduitFlow.Conduit conduit, ConduitFlow.Conduit target_conduit)
	{
		global::Debug.Assert(conduit.idx != -1);
		global::Debug.Assert(target_conduit.idx != -1);
		ConduitFlow.ConduitConnections conduitConnections = this.soaInfo.GetConduitConnections(conduit.idx);
		if (conduitConnections.up == target_conduit.idx)
		{
			return ConduitFlow.FlowDirections.Up;
		}
		if (conduitConnections.down == target_conduit.idx)
		{
			return ConduitFlow.FlowDirections.Down;
		}
		if (conduitConnections.left == target_conduit.idx)
		{
			return ConduitFlow.FlowDirections.Left;
		}
		if (conduitConnections.right == target_conduit.idx)
		{
			return ConduitFlow.FlowDirections.Right;
		}
		return ConduitFlow.FlowDirections.None;
	}

	// Token: 0x060039E6 RID: 14822 RVA: 0x001418C8 File Offset: 0x0013FAC8
	public int ComputeUpdateOrder(int cell)
	{
		foreach (ConduitFlow.Network network in this.networks)
		{
			int num = network.cells.IndexOf(cell);
			if (num != -1)
			{
				return num;
			}
		}
		return -1;
	}

	// Token: 0x060039E7 RID: 14823 RVA: 0x0014192C File Offset: 0x0013FB2C
	public ConduitFlow.ConduitContents GetContents(int cell)
	{
		ConduitFlow.ConduitContents conduitContents = this.grid[cell].contents;
		ConduitFlow.GridNode gridNode = this.grid[cell];
		if (gridNode.conduitIdx != -1)
		{
			conduitContents = this.soaInfo.GetConduit(gridNode.conduitIdx).GetContents(this);
		}
		if (conduitContents.mass > 0f && conduitContents.temperature <= 0f)
		{
			global::Debug.LogError(string.Format("unexpected temperature {0}", conduitContents.temperature));
		}
		return conduitContents;
	}

	// Token: 0x060039E8 RID: 14824 RVA: 0x001419B4 File Offset: 0x0013FBB4
	public void SetContents(int cell, ConduitFlow.ConduitContents contents)
	{
		ConduitFlow.GridNode gridNode = this.grid[cell];
		if (gridNode.conduitIdx != -1)
		{
			this.soaInfo.GetConduit(gridNode.conduitIdx).SetContents(this, contents);
			return;
		}
		this.grid[cell].contents = contents;
	}

	// Token: 0x060039E9 RID: 14825 RVA: 0x00141A05 File Offset: 0x0013FC05
	public static int GetCellFromDirection(int cell, ConduitFlow.FlowDirections direction)
	{
		switch (direction)
		{
		case ConduitFlow.FlowDirections.Down:
			return Grid.CellBelow(cell);
		case ConduitFlow.FlowDirections.Left:
			return Grid.CellLeft(cell);
		case ConduitFlow.FlowDirections.Down | ConduitFlow.FlowDirections.Left:
			break;
		case ConduitFlow.FlowDirections.Right:
			return Grid.CellRight(cell);
		default:
			if (direction == ConduitFlow.FlowDirections.Up)
			{
				return Grid.CellAbove(cell);
			}
			break;
		}
		return -1;
	}

	// Token: 0x060039EA RID: 14826 RVA: 0x00141A44 File Offset: 0x0013FC44
	public void Sim200ms(float dt)
	{
		if (dt <= 0f)
		{
			return;
		}
		this.elapsedTime += dt;
		if (this.elapsedTime < 1f)
		{
			return;
		}
		this.elapsedTime -= 1f;
		float num = 1f;
		this.lastUpdateTime = Time.time;
		this.soaInfo.BeginFrame(this);
		ListPool<ConduitFlow.UpdateNetworkTask, ConduitFlow>.PooledList pooledList = ListPool<ConduitFlow.UpdateNetworkTask, ConduitFlow>.Allocate();
		pooledList.Capacity = Mathf.Max(pooledList.Capacity, this.networks.Count);
		foreach (ConduitFlow.Network network in this.networks)
		{
			pooledList.Add(new ConduitFlow.UpdateNetworkTask(network));
		}
		int num2 = 0;
		while (num2 != 4 && pooledList.Count != 0)
		{
			this.update_networks_job.Reset(this);
			foreach (ConduitFlow.UpdateNetworkTask updateNetworkTask in pooledList)
			{
				this.update_networks_job.Add(updateNetworkTask);
			}
			GlobalJobManager.Run(this.update_networks_job);
			pooledList.Clear();
			for (int num3 = 0; num3 != this.update_networks_job.Count; num3++)
			{
				ConduitFlow.UpdateNetworkTask workItem = this.update_networks_job.GetWorkItem(num3);
				if (workItem.continue_updating && num2 != 3)
				{
					pooledList.Add(workItem);
				}
				else
				{
					workItem.Finish(this);
				}
			}
			num2++;
		}
		pooledList.Recycle();
		if (this.dirtyConduitUpdaters)
		{
			this.conduitUpdaters.Sort((ConduitFlow.ConduitUpdater a, ConduitFlow.ConduitUpdater b) => a.priority - b.priority);
		}
		this.soaInfo.EndFrame(this);
		for (int i = 0; i < this.conduitUpdaters.Count; i++)
		{
			this.conduitUpdaters[i].callback(num);
		}
	}

	// Token: 0x060039EB RID: 14827 RVA: 0x00141C54 File Offset: 0x0013FE54
	private float ComputeMovableMass(ConduitFlow.GridNode grid_node)
	{
		ConduitFlow.ConduitContents contents = grid_node.contents;
		if (contents.element == SimHashes.Vacuum)
		{
			return 0f;
		}
		return contents.movable_mass;
	}

	// Token: 0x060039EC RID: 14828 RVA: 0x00141C84 File Offset: 0x0013FE84
	private bool UpdateConduit(ConduitFlow.Conduit conduit)
	{
		bool flag = false;
		int cell = this.soaInfo.GetCell(conduit.idx);
		ConduitFlow.GridNode gridNode = this.grid[cell];
		float num = this.ComputeMovableMass(gridNode);
		ConduitFlow.FlowDirections permittedFlowDirections = this.soaInfo.GetPermittedFlowDirections(conduit.idx);
		ConduitFlow.FlowDirections flowDirections = this.soaInfo.GetTargetFlowDirection(conduit.idx);
		if (num <= 0f)
		{
			for (int num2 = 0; num2 != 4; num2++)
			{
				flowDirections = ConduitFlow.ComputeNextFlowDirection(flowDirections);
				if ((permittedFlowDirections & flowDirections) != ConduitFlow.FlowDirections.None)
				{
					ConduitFlow.Conduit conduitFromDirection = this.soaInfo.GetConduitFromDirection(conduit.idx, flowDirections);
					global::Debug.Assert(conduitFromDirection.idx != -1);
					if ((this.soaInfo.GetSrcFlowDirection(conduitFromDirection.idx) & ConduitFlow.Opposite(flowDirections)) > ConduitFlow.FlowDirections.None)
					{
						this.soaInfo.SetPullDirection(conduitFromDirection.idx, flowDirections);
					}
				}
			}
		}
		else
		{
			for (int num3 = 0; num3 != 4; num3++)
			{
				flowDirections = ConduitFlow.ComputeNextFlowDirection(flowDirections);
				if ((permittedFlowDirections & flowDirections) != ConduitFlow.FlowDirections.None)
				{
					ConduitFlow.Conduit conduitFromDirection2 = this.soaInfo.GetConduitFromDirection(conduit.idx, flowDirections);
					global::Debug.Assert(conduitFromDirection2.idx != -1);
					ConduitFlow.FlowDirections srcFlowDirection = this.soaInfo.GetSrcFlowDirection(conduitFromDirection2.idx);
					bool flag2 = (srcFlowDirection & ConduitFlow.Opposite(flowDirections)) > ConduitFlow.FlowDirections.None;
					if (srcFlowDirection != ConduitFlow.FlowDirections.None && !flag2)
					{
						flag = true;
					}
					else
					{
						int cell2 = this.soaInfo.GetCell(conduitFromDirection2.idx);
						global::Debug.Assert(cell2 != -1);
						ConduitFlow.ConduitContents contents = this.grid[cell2].contents;
						bool flag3 = contents.element == SimHashes.Vacuum || contents.element == gridNode.contents.element;
						float effectiveCapacity = contents.GetEffectiveCapacity(this.MaxMass);
						bool flag4 = flag3 && effectiveCapacity > 0f;
						float num4 = Mathf.Min(num, effectiveCapacity);
						if (flag2 && flag4)
						{
							this.soaInfo.SetPullDirection(conduitFromDirection2.idx, flowDirections);
						}
						if (num4 > 0f && flag4)
						{
							this.soaInfo.SetTargetFlowDirection(conduit.idx, flowDirections);
							global::Debug.Assert(gridNode.contents.temperature > 0f);
							contents.temperature = GameUtil.GetFinalTemperature(gridNode.contents.temperature, num4, contents.temperature, contents.mass);
							contents.AddMass(num4);
							contents.element = gridNode.contents.element;
							int num5 = (int)(num4 / gridNode.contents.mass * (float)gridNode.contents.diseaseCount);
							if (num5 != 0)
							{
								SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(gridNode.contents.diseaseIdx, num5, contents.diseaseIdx, contents.diseaseCount);
								contents.diseaseIdx = diseaseInfo.idx;
								contents.diseaseCount = diseaseInfo.count;
							}
							this.grid[cell2].contents = contents;
							global::Debug.Assert(num4 <= gridNode.contents.mass);
							float num6 = gridNode.contents.mass - num4;
							num -= num4;
							if (num6 <= 0f)
							{
								global::Debug.Assert(num <= 0f);
								this.soaInfo.SetLastFlowInfo(conduit.idx, flowDirections, ref gridNode.contents);
								gridNode.contents = ConduitFlow.ConduitContents.Empty;
							}
							else
							{
								int num7 = (int)(num6 / gridNode.contents.mass * (float)gridNode.contents.diseaseCount);
								global::Debug.Assert(num7 >= 0);
								ConduitFlow.ConduitContents contents2 = gridNode.contents;
								contents2.RemoveMass(num6);
								contents2.diseaseCount -= num7;
								gridNode.contents.RemoveMass(num4);
								gridNode.contents.diseaseCount = num7;
								if (num7 == 0)
								{
									gridNode.contents.diseaseIdx = byte.MaxValue;
								}
								this.soaInfo.SetLastFlowInfo(conduit.idx, flowDirections, ref contents2);
							}
							this.grid[cell].contents = gridNode.contents;
							flag = 0f < this.ComputeMovableMass(gridNode);
							break;
						}
					}
				}
			}
		}
		ConduitFlow.FlowDirections srcFlowDirection2 = this.soaInfo.GetSrcFlowDirection(conduit.idx);
		ConduitFlow.FlowDirections pullDirection = this.soaInfo.GetPullDirection(conduit.idx);
		if (srcFlowDirection2 == ConduitFlow.FlowDirections.None || (ConduitFlow.Opposite(srcFlowDirection2) & pullDirection) != ConduitFlow.FlowDirections.None)
		{
			this.soaInfo.SetPullDirection(conduit.idx, ConduitFlow.FlowDirections.None);
			this.soaInfo.SetSrcFlowDirection(conduit.idx, ConduitFlow.FlowDirections.None);
			for (int num8 = 0; num8 != 2; num8++)
			{
				ConduitFlow.FlowDirections flowDirections2 = srcFlowDirection2;
				for (int num9 = 0; num9 != 4; num9++)
				{
					flowDirections2 = ConduitFlow.ComputeNextFlowDirection(flowDirections2);
					ConduitFlow.Conduit conduitFromDirection3 = this.soaInfo.GetConduitFromDirection(conduit.idx, flowDirections2);
					if (conduitFromDirection3.idx != -1 && (this.soaInfo.GetPermittedFlowDirections(conduitFromDirection3.idx) & ConduitFlow.Opposite(flowDirections2)) != ConduitFlow.FlowDirections.None)
					{
						int cell3 = this.soaInfo.GetCell(conduitFromDirection3.idx);
						ConduitFlow.ConduitContents contents3 = this.grid[cell3].contents;
						float num10 = ((num8 == 0) ? contents3.movable_mass : contents3.mass);
						if (0f < num10)
						{
							this.soaInfo.SetSrcFlowDirection(conduit.idx, flowDirections2);
							break;
						}
					}
				}
				if (this.soaInfo.GetSrcFlowDirection(conduit.idx) != ConduitFlow.FlowDirections.None)
				{
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x060039ED RID: 14829 RVA: 0x001421E3 File Offset: 0x001403E3
	public float ContinuousLerpPercent
	{
		get
		{
			return Mathf.Clamp01((Time.time - this.lastUpdateTime) / 1f);
		}
	}

	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x060039EE RID: 14830 RVA: 0x001421FC File Offset: 0x001403FC
	public float DiscreteLerpPercent
	{
		get
		{
			return Mathf.Clamp01(this.elapsedTime / 1f);
		}
	}

	// Token: 0x060039EF RID: 14831 RVA: 0x0014220F File Offset: 0x0014040F
	public float GetAmountAllowedForMerging(ConduitFlow.ConduitContents from, ConduitFlow.ConduitContents to, float massDesiredtoBeMoved)
	{
		return Mathf.Min(massDesiredtoBeMoved, this.MaxMass - to.mass);
	}

	// Token: 0x060039F0 RID: 14832 RVA: 0x00142225 File Offset: 0x00140425
	public bool CanMergeContents(ConduitFlow.ConduitContents from, ConduitFlow.ConduitContents to, float massToMove)
	{
		return (from.element == to.element || to.element == SimHashes.Vacuum || massToMove <= 0f) && this.GetAmountAllowedForMerging(from, to, massToMove) > 0f;
	}

	// Token: 0x060039F1 RID: 14833 RVA: 0x00142260 File Offset: 0x00140460
	public float AddElement(int cell_idx, SimHashes element, float mass, float temperature, byte disease_idx, int disease_count)
	{
		if (this.grid[cell_idx].conduitIdx == -1)
		{
			return 0f;
		}
		ConduitFlow.ConduitContents contents = this.GetConduit(cell_idx).GetContents(this);
		if (contents.element != element && contents.element != SimHashes.Vacuum && mass > 0f)
		{
			return 0f;
		}
		float num = Mathf.Min(mass, this.MaxMass - contents.mass);
		float num2 = num / mass;
		if (num <= 0f)
		{
			return 0f;
		}
		contents.temperature = GameUtil.GetFinalTemperature(temperature, num, contents.temperature, contents.mass);
		contents.AddMass(num);
		contents.element = element;
		contents.ConsolidateMass();
		int num3 = (int)(num2 * (float)disease_count);
		if (num3 > 0)
		{
			SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(disease_idx, num3, contents.diseaseIdx, contents.diseaseCount);
			contents.diseaseIdx = diseaseInfo.idx;
			contents.diseaseCount = diseaseInfo.count;
		}
		this.SetContents(cell_idx, contents);
		return num;
	}

	// Token: 0x060039F2 RID: 14834 RVA: 0x00142360 File Offset: 0x00140560
	public ConduitFlow.ConduitContents RemoveElement(int cell, float delta)
	{
		ConduitFlow.Conduit conduit = this.GetConduit(cell);
		if (conduit.idx == -1)
		{
			return ConduitFlow.ConduitContents.Empty;
		}
		return this.RemoveElement(conduit, delta);
	}

	// Token: 0x060039F3 RID: 14835 RVA: 0x0014238C File Offset: 0x0014058C
	public ConduitFlow.ConduitContents RemoveElement(ConduitFlow.Conduit conduit, float delta)
	{
		ConduitFlow.ConduitContents contents = conduit.GetContents(this);
		float num = Mathf.Min(contents.mass, delta);
		float num2 = contents.mass - num;
		if (num2 <= 0f)
		{
			conduit.SetContents(this, ConduitFlow.ConduitContents.Empty);
			return contents;
		}
		ConduitFlow.ConduitContents conduitContents = contents;
		conduitContents.RemoveMass(num2);
		int num3 = (int)(num2 / contents.mass * (float)contents.diseaseCount);
		conduitContents.diseaseCount = contents.diseaseCount - num3;
		ConduitFlow.ConduitContents conduitContents2 = contents;
		conduitContents2.RemoveMass(num);
		conduitContents2.diseaseCount = num3;
		if (num3 <= 0)
		{
			conduitContents2.diseaseIdx = byte.MaxValue;
			conduitContents2.diseaseCount = 0;
		}
		conduit.SetContents(this, conduitContents2);
		return conduitContents;
	}

	// Token: 0x060039F4 RID: 14836 RVA: 0x0014243C File Offset: 0x0014063C
	public ConduitFlow.FlowDirections GetPermittedFlow(int cell)
	{
		ConduitFlow.Conduit conduit = this.GetConduit(cell);
		if (conduit.idx == -1)
		{
			return ConduitFlow.FlowDirections.None;
		}
		return this.soaInfo.GetPermittedFlowDirections(conduit.idx);
	}

	// Token: 0x060039F5 RID: 14837 RVA: 0x0014246D File Offset: 0x0014066D
	public bool HasConduit(int cell)
	{
		return this.grid[cell].conduitIdx != -1;
	}

	// Token: 0x060039F6 RID: 14838 RVA: 0x00142488 File Offset: 0x00140688
	public ConduitFlow.Conduit GetConduit(int cell)
	{
		int conduitIdx = this.grid[cell].conduitIdx;
		if (conduitIdx == -1)
		{
			return ConduitFlow.Conduit.Invalid;
		}
		return this.soaInfo.GetConduit(conduitIdx);
	}

	// Token: 0x060039F7 RID: 14839 RVA: 0x001424C0 File Offset: 0x001406C0
	private void DumpPipeContents(int cell, ConduitFlow.ConduitContents contents)
	{
		if (contents.element != SimHashes.Vacuum && contents.mass > 0f)
		{
			SimMessages.AddRemoveSubstance(cell, contents.element, CellEventLogger.Instance.ConduitFlowEmptyConduit, contents.mass, contents.temperature, contents.diseaseIdx, contents.diseaseCount, true, -1);
			this.SetContents(cell, ConduitFlow.ConduitContents.Empty);
		}
	}

	// Token: 0x060039F8 RID: 14840 RVA: 0x00142525 File Offset: 0x00140725
	public void EmptyConduit(int cell)
	{
		if (this.replacements.Contains(cell))
		{
			return;
		}
		this.DumpPipeContents(cell, this.grid[cell].contents);
	}

	// Token: 0x060039F9 RID: 14841 RVA: 0x0014254E File Offset: 0x0014074E
	public void MarkForReplacement(int cell)
	{
		this.replacements.Add(cell);
	}

	// Token: 0x060039FA RID: 14842 RVA: 0x0014255D File Offset: 0x0014075D
	public void DeactivateCell(int cell)
	{
		this.grid[cell].conduitIdx = -1;
		this.SetContents(cell, ConduitFlow.ConduitContents.Empty);
	}

	// Token: 0x060039FB RID: 14843 RVA: 0x0014257D File Offset: 0x0014077D
	[Conditional("CHECK_NAN")]
	private void Validate(ConduitFlow.ConduitContents contents)
	{
		if (contents.mass > 0f && contents.temperature <= 0f)
		{
			global::Debug.LogError("zero degree pipe contents");
		}
	}

	// Token: 0x060039FC RID: 14844 RVA: 0x001425A4 File Offset: 0x001407A4
	[OnSerializing]
	private void OnSerializing()
	{
		int numEntries = this.soaInfo.NumEntries;
		if (numEntries > 0)
		{
			this.versionedSerializedContents = new ConduitFlow.SerializedContents[numEntries];
			this.serializedIdx = new int[numEntries];
			for (int i = 0; i < numEntries; i++)
			{
				ConduitFlow.Conduit conduit = this.soaInfo.GetConduit(i);
				ConduitFlow.ConduitContents contents = conduit.GetContents(this);
				this.serializedIdx[i] = this.soaInfo.GetCell(conduit.idx);
				this.versionedSerializedContents[i] = new ConduitFlow.SerializedContents(contents);
			}
			return;
		}
		this.serializedContents = null;
		this.versionedSerializedContents = null;
		this.serializedIdx = null;
	}

	// Token: 0x060039FD RID: 14845 RVA: 0x0014263C File Offset: 0x0014083C
	[OnSerialized]
	private void OnSerialized()
	{
		this.versionedSerializedContents = null;
		this.serializedContents = null;
		this.serializedIdx = null;
	}

	// Token: 0x060039FE RID: 14846 RVA: 0x00142654 File Offset: 0x00140854
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.serializedContents != null)
		{
			this.versionedSerializedContents = new ConduitFlow.SerializedContents[this.serializedContents.Length];
			for (int i = 0; i < this.serializedContents.Length; i++)
			{
				this.versionedSerializedContents[i] = new ConduitFlow.SerializedContents(this.serializedContents[i]);
			}
			this.serializedContents = null;
		}
		if (this.versionedSerializedContents == null)
		{
			return;
		}
		for (int j = 0; j < this.versionedSerializedContents.Length; j++)
		{
			int num = this.serializedIdx[j];
			ConduitFlow.SerializedContents serializedContents = this.versionedSerializedContents[j];
			ConduitFlow.ConduitContents conduitContents = ((serializedContents.mass <= 0f) ? ConduitFlow.ConduitContents.Empty : new ConduitFlow.ConduitContents(serializedContents.element, Math.Min(this.MaxMass, serializedContents.mass), serializedContents.temperature, byte.MaxValue, 0));
			if (0 < serializedContents.diseaseCount || serializedContents.diseaseHash != 0)
			{
				conduitContents.diseaseIdx = Db.Get().Diseases.GetIndex(serializedContents.diseaseHash);
				conduitContents.diseaseCount = ((conduitContents.diseaseIdx == byte.MaxValue) ? 0 : serializedContents.diseaseCount);
			}
			if (float.IsNaN(conduitContents.temperature) || (conduitContents.temperature <= 0f && conduitContents.element != SimHashes.Vacuum) || 10000f < conduitContents.temperature)
			{
				Vector2I vector2I = Grid.CellToXY(num);
				DeserializeWarnings.Instance.PipeContentsTemperatureIsNan.Warn(string.Format("Invalid pipe content temperature of {0} detected. Resetting temperature. (x={1}, y={2}, cell={3})", new object[] { conduitContents.temperature, vector2I.x, vector2I.y, num }), null);
				conduitContents.temperature = ElementLoader.FindElementByHash(conduitContents.element).defaultValues.temperature;
			}
			this.SetContents(num, conduitContents);
		}
		this.versionedSerializedContents = null;
		this.serializedContents = null;
		this.serializedIdx = null;
	}

	// Token: 0x060039FF RID: 14847 RVA: 0x00142848 File Offset: 0x00140A48
	public UtilityNetwork GetNetwork(ConduitFlow.Conduit conduit)
	{
		int cell = this.soaInfo.GetCell(conduit.idx);
		return this.networkMgr.GetNetworkForCell(cell);
	}

	// Token: 0x06003A00 RID: 14848 RVA: 0x00142873 File Offset: 0x00140A73
	public void ForceRebuildNetworks()
	{
		this.networkMgr.ForceRebuildNetworks();
	}

	// Token: 0x06003A01 RID: 14849 RVA: 0x00142880 File Offset: 0x00140A80
	public bool IsConduitFull(int cell_idx)
	{
		ConduitFlow.ConduitContents contents = this.grid[cell_idx].contents;
		return this.MaxMass - contents.mass <= 0f;
	}

	// Token: 0x06003A02 RID: 14850 RVA: 0x001428B8 File Offset: 0x00140AB8
	public bool IsConduitEmpty(int cell_idx)
	{
		ConduitFlow.ConduitContents contents = this.grid[cell_idx].contents;
		return contents.mass <= 0f;
	}

	// Token: 0x06003A03 RID: 14851 RVA: 0x001428E8 File Offset: 0x00140AE8
	public void FreezeConduitContents(int conduit_idx)
	{
		GameObject conduitGO = this.soaInfo.GetConduitGO(conduit_idx);
		if (conduitGO != null && this.soaInfo.GetConduit(conduit_idx).GetContents(this).mass > this.MaxMass * 0.1f)
		{
			conduitGO.Trigger(-700727624, null);
		}
	}

	// Token: 0x06003A04 RID: 14852 RVA: 0x00142944 File Offset: 0x00140B44
	public void MeltConduitContents(int conduit_idx)
	{
		GameObject conduitGO = this.soaInfo.GetConduitGO(conduit_idx);
		if (conduitGO != null && this.soaInfo.GetConduit(conduit_idx).GetContents(this).mass > this.MaxMass * 0.1f)
		{
			conduitGO.Trigger(-1152799878, null);
		}
	}

	// Token: 0x04002398 RID: 9112
	public const float MAX_LIQUID_MASS = 10f;

	// Token: 0x04002399 RID: 9113
	public const float MAX_GAS_MASS = 1f;

	// Token: 0x0400239A RID: 9114
	public ConduitType conduitType;

	// Token: 0x0400239B RID: 9115
	private float MaxMass = 10f;

	// Token: 0x0400239C RID: 9116
	private const float PERCENT_MAX_MASS_FOR_STATE_CHANGE_DAMAGE = 0.1f;

	// Token: 0x0400239D RID: 9117
	public const float TickRate = 1f;

	// Token: 0x0400239E RID: 9118
	public const float WaitTime = 1f;

	// Token: 0x0400239F RID: 9119
	private float elapsedTime;

	// Token: 0x040023A0 RID: 9120
	private float lastUpdateTime = float.NegativeInfinity;

	// Token: 0x040023A1 RID: 9121
	public ConduitFlow.SOAInfo soaInfo = new ConduitFlow.SOAInfo();

	// Token: 0x040023A3 RID: 9123
	private bool dirtyConduitUpdaters;

	// Token: 0x040023A4 RID: 9124
	private List<ConduitFlow.ConduitUpdater> conduitUpdaters = new List<ConduitFlow.ConduitUpdater>();

	// Token: 0x040023A5 RID: 9125
	private ConduitFlow.GridNode[] grid;

	// Token: 0x040023A6 RID: 9126
	[Serialize]
	public int[] serializedIdx;

	// Token: 0x040023A7 RID: 9127
	[Serialize]
	public ConduitFlow.ConduitContents[] serializedContents;

	// Token: 0x040023A8 RID: 9128
	[Serialize]
	public ConduitFlow.SerializedContents[] versionedSerializedContents;

	// Token: 0x040023A9 RID: 9129
	private IUtilityNetworkMgr networkMgr;

	// Token: 0x040023AA RID: 9130
	private HashSet<int> replacements = new HashSet<int>();

	// Token: 0x040023AB RID: 9131
	private const int FLOW_DIRECTION_COUNT = 4;

	// Token: 0x040023AC RID: 9132
	private List<ConduitFlow.Network> networks = new List<ConduitFlow.Network>();

	// Token: 0x040023AD RID: 9133
	private WorkItemCollection<ConduitFlow.BuildNetworkTask, ConduitFlow> build_network_job = new WorkItemCollection<ConduitFlow.BuildNetworkTask, ConduitFlow>();

	// Token: 0x040023AE RID: 9134
	private WorkItemCollection<ConduitFlow.ConnectTask, ConduitFlow.ConnectContext> connect_job = new WorkItemCollection<ConduitFlow.ConnectTask, ConduitFlow.ConnectContext>();

	// Token: 0x040023AF RID: 9135
	private WorkItemCollection<ConduitFlow.UpdateNetworkTask, ConduitFlow> update_networks_job = new WorkItemCollection<ConduitFlow.UpdateNetworkTask, ConduitFlow>();

	// Token: 0x020017AD RID: 6061
	[DebuggerDisplay("{NumEntries}")]
	public class SOAInfo
	{
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06009A0D RID: 39437 RVA: 0x00388CD9 File Offset: 0x00386ED9
		public int NumEntries
		{
			get
			{
				return this.conduits.Count;
			}
		}

		// Token: 0x06009A0E RID: 39438 RVA: 0x00388CE8 File Offset: 0x00386EE8
		public int AddConduit(ConduitFlow manager, GameObject conduit_go, int cell)
		{
			int count = this.conduitConnections.Count;
			ConduitFlow.Conduit conduit = new ConduitFlow.Conduit(count);
			this.conduits.Add(conduit);
			this.conduitConnections.Add(new ConduitFlow.ConduitConnections
			{
				left = -1,
				right = -1,
				up = -1,
				down = -1
			});
			ConduitFlow.ConduitContents contents = manager.grid[cell].contents;
			this.initialContents.Add(contents);
			this.lastFlowInfo.Add(ConduitFlow.ConduitFlowInfo.DEFAULT);
			HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(conduit_go);
			HandleVector<int>.Handle handle2 = Game.Instance.conduitTemperatureManager.Allocate(manager.conduitType, count, handle, ref contents);
			HandleVector<int>.Handle handle3 = Game.Instance.conduitDiseaseManager.Allocate(handle2, ref contents);
			this.cells.Add(cell);
			this.diseaseContentsVisible.Add(false);
			this.structureTemperatureHandles.Add(handle);
			this.temperatureHandles.Add(handle2);
			this.diseaseHandles.Add(handle3);
			this.conduitGOs.Add(conduit_go);
			this.permittedFlowDirections.Add(ConduitFlow.FlowDirections.None);
			this.srcFlowDirections.Add(ConduitFlow.FlowDirections.None);
			this.pullDirections.Add(ConduitFlow.FlowDirections.None);
			this.targetFlowDirections.Add(ConduitFlow.FlowDirections.None);
			return count;
		}

		// Token: 0x06009A0F RID: 39439 RVA: 0x00388E30 File Offset: 0x00387030
		public void Clear(ConduitFlow manager)
		{
			if (this.clearJob.Count == 0)
			{
				this.clearJob.Reset(this);
				this.clearJob.Add<ConduitFlow.SOAInfo.PublishTemperatureToSim>(this.publishTemperatureToSim);
				this.clearJob.Add<ConduitFlow.SOAInfo.PublishDiseaseToSim>(this.publishDiseaseToSim);
				this.clearJob.Add<ConduitFlow.SOAInfo.ResetConduit>(this.resetConduit);
			}
			this.clearPermanentDiseaseContainer.Initialize(this.conduits.Count, manager);
			this.publishTemperatureToSim.Initialize(this.conduits.Count, manager);
			this.publishDiseaseToSim.Initialize(this.conduits.Count, manager);
			this.resetConduit.Initialize(this.conduits.Count, manager);
			this.clearPermanentDiseaseContainer.Run(this, 0);
			GlobalJobManager.Run(this.clearJob);
			for (int num = 0; num != this.conduits.Count; num++)
			{
				Game.Instance.conduitDiseaseManager.Free(this.diseaseHandles[num]);
			}
			for (int num2 = 0; num2 != this.conduits.Count; num2++)
			{
				Game.Instance.conduitTemperatureManager.Free(this.temperatureHandles[num2]);
			}
			this.cells.Clear();
			this.diseaseContentsVisible.Clear();
			this.permittedFlowDirections.Clear();
			this.srcFlowDirections.Clear();
			this.pullDirections.Clear();
			this.targetFlowDirections.Clear();
			this.conduitGOs.Clear();
			this.diseaseHandles.Clear();
			this.temperatureHandles.Clear();
			this.structureTemperatureHandles.Clear();
			this.initialContents.Clear();
			this.lastFlowInfo.Clear();
			this.conduitConnections.Clear();
			this.conduits.Clear();
		}

		// Token: 0x06009A10 RID: 39440 RVA: 0x00388FFA File Offset: 0x003871FA
		public ConduitFlow.Conduit GetConduit(int idx)
		{
			return this.conduits[idx];
		}

		// Token: 0x06009A11 RID: 39441 RVA: 0x00389008 File Offset: 0x00387208
		public ConduitFlow.ConduitConnections GetConduitConnections(int idx)
		{
			return this.conduitConnections[idx];
		}

		// Token: 0x06009A12 RID: 39442 RVA: 0x00389016 File Offset: 0x00387216
		public void SetConduitConnections(int idx, ConduitFlow.ConduitConnections data)
		{
			this.conduitConnections[idx] = data;
		}

		// Token: 0x06009A13 RID: 39443 RVA: 0x00389028 File Offset: 0x00387228
		public float GetConduitTemperature(int idx)
		{
			HandleVector<int>.Handle handle = this.temperatureHandles[idx];
			float temperature = Game.Instance.conduitTemperatureManager.GetTemperature(handle);
			global::Debug.Assert(!float.IsNaN(temperature));
			return temperature;
		}

		// Token: 0x06009A14 RID: 39444 RVA: 0x00389060 File Offset: 0x00387260
		public void SetConduitTemperatureData(int idx, ref ConduitFlow.ConduitContents contents)
		{
			HandleVector<int>.Handle handle = this.temperatureHandles[idx];
			Game.Instance.conduitTemperatureManager.SetData(handle, ref contents);
		}

		// Token: 0x06009A15 RID: 39445 RVA: 0x0038908C File Offset: 0x0038728C
		public ConduitDiseaseManager.Data GetDiseaseData(int idx)
		{
			HandleVector<int>.Handle handle = this.diseaseHandles[idx];
			return Game.Instance.conduitDiseaseManager.GetData(handle);
		}

		// Token: 0x06009A16 RID: 39446 RVA: 0x003890B8 File Offset: 0x003872B8
		public void SetDiseaseData(int idx, ref ConduitFlow.ConduitContents contents)
		{
			HandleVector<int>.Handle handle = this.diseaseHandles[idx];
			Game.Instance.conduitDiseaseManager.SetData(handle, ref contents);
		}

		// Token: 0x06009A17 RID: 39447 RVA: 0x003890E3 File Offset: 0x003872E3
		public GameObject GetConduitGO(int idx)
		{
			return this.conduitGOs[idx];
		}

		// Token: 0x06009A18 RID: 39448 RVA: 0x003890F4 File Offset: 0x003872F4
		public void ForcePermanentDiseaseContainer(int idx, bool force_on)
		{
			if (this.diseaseContentsVisible[idx] != force_on)
			{
				this.diseaseContentsVisible[idx] = force_on;
				GameObject gameObject = this.conduitGOs[idx];
				if (gameObject == null)
				{
					return;
				}
				gameObject.GetComponent<PrimaryElement>().ForcePermanentDiseaseContainer(force_on);
			}
		}

		// Token: 0x06009A19 RID: 39449 RVA: 0x00389140 File Offset: 0x00387340
		public ConduitFlow.Conduit GetConduitFromDirection(int idx, ConduitFlow.FlowDirections direction)
		{
			ConduitFlow.ConduitConnections conduitConnections = this.conduitConnections[idx];
			switch (direction)
			{
			case ConduitFlow.FlowDirections.Down:
				if (conduitConnections.down == Grid.InvalidCell)
				{
					return ConduitFlow.Conduit.Invalid;
				}
				return this.conduits[conduitConnections.down];
			case ConduitFlow.FlowDirections.Left:
				if (conduitConnections.left == Grid.InvalidCell)
				{
					return ConduitFlow.Conduit.Invalid;
				}
				return this.conduits[conduitConnections.left];
			case ConduitFlow.FlowDirections.Down | ConduitFlow.FlowDirections.Left:
				break;
			case ConduitFlow.FlowDirections.Right:
				if (conduitConnections.right == Grid.InvalidCell)
				{
					return ConduitFlow.Conduit.Invalid;
				}
				return this.conduits[conduitConnections.right];
			default:
				if (direction == ConduitFlow.FlowDirections.Up)
				{
					if (conduitConnections.up == Grid.InvalidCell)
					{
						return ConduitFlow.Conduit.Invalid;
					}
					return this.conduits[conduitConnections.up];
				}
				break;
			}
			return ConduitFlow.Conduit.Invalid;
		}

		// Token: 0x06009A1A RID: 39450 RVA: 0x00389214 File Offset: 0x00387414
		public void BeginFrame(ConduitFlow manager)
		{
			if (this.beginFrameJob.Count == 0)
			{
				this.beginFrameJob.Reset(this);
				this.beginFrameJob.Add<ConduitFlow.SOAInfo.InitializeContentsTask>(this.initializeContents);
				this.beginFrameJob.Add<ConduitFlow.SOAInfo.InvalidateLastFlow>(this.invalidateLastFlow);
			}
			this.initializeContents.Initialize(this.conduits.Count, manager);
			this.invalidateLastFlow.Initialize(this.conduits.Count, manager);
			GlobalJobManager.Run(this.beginFrameJob);
		}

		// Token: 0x06009A1B RID: 39451 RVA: 0x00389298 File Offset: 0x00387498
		public void EndFrame(ConduitFlow manager)
		{
			if (this.endFrameJob.Count == 0)
			{
				this.endFrameJob.Reset(this);
				this.endFrameJob.Add<ConduitFlow.SOAInfo.PublishDiseaseToGame>(this.publishDiseaseToGame);
			}
			this.publishTemperatureToGame.Initialize(this.conduits.Count, manager);
			this.publishDiseaseToGame.Initialize(this.conduits.Count, manager);
			this.publishTemperatureToGame.Run(this, 0);
			GlobalJobManager.Run(this.endFrameJob);
		}

		// Token: 0x06009A1C RID: 39452 RVA: 0x00389318 File Offset: 0x00387518
		public void UpdateFlowDirection(ConduitFlow manager)
		{
			if (this.updateFlowDirectionJob.Count == 0)
			{
				this.updateFlowDirectionJob.Reset(this);
				this.updateFlowDirectionJob.Add<ConduitFlow.SOAInfo.FlowThroughVacuum>(this.flowThroughVacuum);
			}
			this.flowThroughVacuum.Initialize(this.conduits.Count, manager);
			GlobalJobManager.Run(this.updateFlowDirectionJob);
		}

		// Token: 0x06009A1D RID: 39453 RVA: 0x00389371 File Offset: 0x00387571
		public void ResetLastFlowInfo(int idx)
		{
			this.lastFlowInfo[idx] = ConduitFlow.ConduitFlowInfo.DEFAULT;
		}

		// Token: 0x06009A1E RID: 39454 RVA: 0x00389384 File Offset: 0x00387584
		public void SetLastFlowInfo(int idx, ConduitFlow.FlowDirections direction, ref ConduitFlow.ConduitContents contents)
		{
			if (this.lastFlowInfo[idx].direction == ConduitFlow.FlowDirections.None)
			{
				this.lastFlowInfo[idx] = new ConduitFlow.ConduitFlowInfo
				{
					direction = direction,
					contents = contents
				};
			}
		}

		// Token: 0x06009A1F RID: 39455 RVA: 0x003893CE File Offset: 0x003875CE
		public ConduitFlow.ConduitContents GetInitialContents(int idx)
		{
			return this.initialContents[idx];
		}

		// Token: 0x06009A20 RID: 39456 RVA: 0x003893DC File Offset: 0x003875DC
		public ConduitFlow.ConduitFlowInfo GetLastFlowInfo(int idx)
		{
			return this.lastFlowInfo[idx];
		}

		// Token: 0x06009A21 RID: 39457 RVA: 0x003893EA File Offset: 0x003875EA
		public ConduitFlow.FlowDirections GetPermittedFlowDirections(int idx)
		{
			return this.permittedFlowDirections[idx];
		}

		// Token: 0x06009A22 RID: 39458 RVA: 0x003893F8 File Offset: 0x003875F8
		public void SetPermittedFlowDirections(int idx, ConduitFlow.FlowDirections permitted)
		{
			this.permittedFlowDirections[idx] = permitted;
		}

		// Token: 0x06009A23 RID: 39459 RVA: 0x00389408 File Offset: 0x00387608
		public ConduitFlow.FlowDirections AddPermittedFlowDirections(int idx, ConduitFlow.FlowDirections delta)
		{
			List<ConduitFlow.FlowDirections> list = this.permittedFlowDirections;
			return list[idx] |= delta;
		}

		// Token: 0x06009A24 RID: 39460 RVA: 0x00389434 File Offset: 0x00387634
		public ConduitFlow.FlowDirections RemovePermittedFlowDirections(int idx, ConduitFlow.FlowDirections delta)
		{
			List<ConduitFlow.FlowDirections> list = this.permittedFlowDirections;
			return list[idx] &= ~delta;
		}

		// Token: 0x06009A25 RID: 39461 RVA: 0x0038945F File Offset: 0x0038765F
		public ConduitFlow.FlowDirections GetTargetFlowDirection(int idx)
		{
			return this.targetFlowDirections[idx];
		}

		// Token: 0x06009A26 RID: 39462 RVA: 0x0038946D File Offset: 0x0038766D
		public void SetTargetFlowDirection(int idx, ConduitFlow.FlowDirections directions)
		{
			this.targetFlowDirections[idx] = directions;
		}

		// Token: 0x06009A27 RID: 39463 RVA: 0x0038947C File Offset: 0x0038767C
		public ConduitFlow.FlowDirections GetSrcFlowDirection(int idx)
		{
			return this.srcFlowDirections[idx];
		}

		// Token: 0x06009A28 RID: 39464 RVA: 0x0038948A File Offset: 0x0038768A
		public void SetSrcFlowDirection(int idx, ConduitFlow.FlowDirections directions)
		{
			this.srcFlowDirections[idx] = directions;
		}

		// Token: 0x06009A29 RID: 39465 RVA: 0x00389499 File Offset: 0x00387699
		public ConduitFlow.FlowDirections GetPullDirection(int idx)
		{
			return this.pullDirections[idx];
		}

		// Token: 0x06009A2A RID: 39466 RVA: 0x003894A7 File Offset: 0x003876A7
		public void SetPullDirection(int idx, ConduitFlow.FlowDirections directions)
		{
			this.pullDirections[idx] = directions;
		}

		// Token: 0x06009A2B RID: 39467 RVA: 0x003894B6 File Offset: 0x003876B6
		public int GetCell(int idx)
		{
			return this.cells[idx];
		}

		// Token: 0x06009A2C RID: 39468 RVA: 0x003894C4 File Offset: 0x003876C4
		public void SetCell(int idx, int cell)
		{
			this.cells[idx] = cell;
		}

		// Token: 0x04007682 RID: 30338
		private List<ConduitFlow.Conduit> conduits = new List<ConduitFlow.Conduit>();

		// Token: 0x04007683 RID: 30339
		private List<ConduitFlow.ConduitConnections> conduitConnections = new List<ConduitFlow.ConduitConnections>();

		// Token: 0x04007684 RID: 30340
		private List<ConduitFlow.ConduitFlowInfo> lastFlowInfo = new List<ConduitFlow.ConduitFlowInfo>();

		// Token: 0x04007685 RID: 30341
		private List<ConduitFlow.ConduitContents> initialContents = new List<ConduitFlow.ConduitContents>();

		// Token: 0x04007686 RID: 30342
		private List<GameObject> conduitGOs = new List<GameObject>();

		// Token: 0x04007687 RID: 30343
		private List<bool> diseaseContentsVisible = new List<bool>();

		// Token: 0x04007688 RID: 30344
		private List<int> cells = new List<int>();

		// Token: 0x04007689 RID: 30345
		private List<ConduitFlow.FlowDirections> permittedFlowDirections = new List<ConduitFlow.FlowDirections>();

		// Token: 0x0400768A RID: 30346
		private List<ConduitFlow.FlowDirections> srcFlowDirections = new List<ConduitFlow.FlowDirections>();

		// Token: 0x0400768B RID: 30347
		private List<ConduitFlow.FlowDirections> pullDirections = new List<ConduitFlow.FlowDirections>();

		// Token: 0x0400768C RID: 30348
		private List<ConduitFlow.FlowDirections> targetFlowDirections = new List<ConduitFlow.FlowDirections>();

		// Token: 0x0400768D RID: 30349
		private List<HandleVector<int>.Handle> structureTemperatureHandles = new List<HandleVector<int>.Handle>();

		// Token: 0x0400768E RID: 30350
		private List<HandleVector<int>.Handle> temperatureHandles = new List<HandleVector<int>.Handle>();

		// Token: 0x0400768F RID: 30351
		private List<HandleVector<int>.Handle> diseaseHandles = new List<HandleVector<int>.Handle>();

		// Token: 0x04007690 RID: 30352
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.ClearPermanentDiseaseContainer> clearPermanentDiseaseContainer = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.ClearPermanentDiseaseContainer>();

		// Token: 0x04007691 RID: 30353
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishTemperatureToSim> publishTemperatureToSim = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishTemperatureToSim>();

		// Token: 0x04007692 RID: 30354
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishDiseaseToSim> publishDiseaseToSim = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishDiseaseToSim>();

		// Token: 0x04007693 RID: 30355
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.ResetConduit> resetConduit = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.ResetConduit>();

		// Token: 0x04007694 RID: 30356
		private ConduitFlow.SOAInfo.ConduitJob clearJob = new ConduitFlow.SOAInfo.ConduitJob();

		// Token: 0x04007695 RID: 30357
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.InitializeContentsTask> initializeContents = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.InitializeContentsTask>();

		// Token: 0x04007696 RID: 30358
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.InvalidateLastFlow> invalidateLastFlow = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.InvalidateLastFlow>();

		// Token: 0x04007697 RID: 30359
		private ConduitFlow.SOAInfo.ConduitJob beginFrameJob = new ConduitFlow.SOAInfo.ConduitJob();

		// Token: 0x04007698 RID: 30360
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishTemperatureToGame> publishTemperatureToGame = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishTemperatureToGame>();

		// Token: 0x04007699 RID: 30361
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishDiseaseToGame> publishDiseaseToGame = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishDiseaseToGame>();

		// Token: 0x0400769A RID: 30362
		private ConduitFlow.SOAInfo.ConduitJob endFrameJob = new ConduitFlow.SOAInfo.ConduitJob();

		// Token: 0x0400769B RID: 30363
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.FlowThroughVacuum> flowThroughVacuum = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.FlowThroughVacuum>();

		// Token: 0x0400769C RID: 30364
		private ConduitFlow.SOAInfo.ConduitJob updateFlowDirectionJob = new ConduitFlow.SOAInfo.ConduitJob();

		// Token: 0x0200280B RID: 10251
		private abstract class ConduitTask : DivisibleTask<ConduitFlow.SOAInfo>
		{
			// Token: 0x0600CA8C RID: 51852 RVA: 0x00417E57 File Offset: 0x00416057
			public ConduitTask(string name)
				: base(name)
			{
			}

			// Token: 0x0400B1B9 RID: 45497
			public ConduitFlow manager;
		}

		// Token: 0x0200280C RID: 10252
		private class ConduitTaskDivision<Task> : TaskDivision<Task, ConduitFlow.SOAInfo> where Task : ConduitFlow.SOAInfo.ConduitTask, new()
		{
			// Token: 0x0600CA8D RID: 51853 RVA: 0x00417E60 File Offset: 0x00416060
			public void Initialize(int conduitCount, ConduitFlow manager)
			{
				base.Initialize(conduitCount);
				Task[] tasks = this.tasks;
				for (int i = 0; i < tasks.Length; i++)
				{
					tasks[i].manager = manager;
				}
			}
		}

		// Token: 0x0200280D RID: 10253
		private class ConduitJob : WorkItemCollection<ConduitFlow.SOAInfo.ConduitTask, ConduitFlow.SOAInfo>
		{
			// Token: 0x0600CA8F RID: 51855 RVA: 0x00417EA4 File Offset: 0x004160A4
			public void Add<Task>(ConduitFlow.SOAInfo.ConduitTaskDivision<Task> taskDivision) where Task : ConduitFlow.SOAInfo.ConduitTask, new()
			{
				foreach (Task task in taskDivision.tasks)
				{
					base.Add(task);
				}
			}
		}

		// Token: 0x0200280E RID: 10254
		private class ClearPermanentDiseaseContainer : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600CA91 RID: 51857 RVA: 0x00417EE2 File Offset: 0x004160E2
			public ClearPermanentDiseaseContainer()
				: base("ClearPermanentDiseaseContainer")
			{
			}

			// Token: 0x0600CA92 RID: 51858 RVA: 0x00417EF0 File Offset: 0x004160F0
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					soaInfo.ForcePermanentDiseaseContainer(num, false);
				}
			}
		}

		// Token: 0x0200280F RID: 10255
		private class PublishTemperatureToSim : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600CA93 RID: 51859 RVA: 0x00417F1B File Offset: 0x0041611B
			public PublishTemperatureToSim()
				: base("PublishTemperatureToSim")
			{
			}

			// Token: 0x0600CA94 RID: 51860 RVA: 0x00417F28 File Offset: 0x00416128
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					HandleVector<int>.Handle handle = soaInfo.temperatureHandles[num];
					if (handle.IsValid())
					{
						float temperature = Game.Instance.conduitTemperatureManager.GetTemperature(handle);
						this.manager.grid[soaInfo.cells[num]].contents.temperature = temperature;
					}
				}
			}
		}

		// Token: 0x02002810 RID: 10256
		private class PublishDiseaseToSim : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600CA95 RID: 51861 RVA: 0x00417F99 File Offset: 0x00416199
			public PublishDiseaseToSim()
				: base("PublishDiseaseToSim")
			{
			}

			// Token: 0x0600CA96 RID: 51862 RVA: 0x00417FA8 File Offset: 0x004161A8
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					HandleVector<int>.Handle handle = soaInfo.diseaseHandles[num];
					if (handle.IsValid())
					{
						ConduitDiseaseManager.Data data = Game.Instance.conduitDiseaseManager.GetData(handle);
						int num2 = soaInfo.cells[num];
						this.manager.grid[num2].contents.diseaseIdx = data.diseaseIdx;
						this.manager.grid[num2].contents.diseaseCount = data.diseaseCount;
					}
				}
			}
		}

		// Token: 0x02002811 RID: 10257
		private class ResetConduit : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600CA97 RID: 51863 RVA: 0x00418044 File Offset: 0x00416244
			public ResetConduit()
				: base("ResetConduitTask")
			{
			}

			// Token: 0x0600CA98 RID: 51864 RVA: 0x00418054 File Offset: 0x00416254
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					this.manager.grid[soaInfo.cells[num]].conduitIdx = -1;
				}
			}
		}

		// Token: 0x02002812 RID: 10258
		private class InitializeContentsTask : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600CA99 RID: 51865 RVA: 0x00418099 File Offset: 0x00416299
			public InitializeContentsTask()
				: base("SetInitialContents")
			{
			}

			// Token: 0x0600CA9A RID: 51866 RVA: 0x004180A8 File Offset: 0x004162A8
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					int num2 = soaInfo.cells[num];
					ConduitFlow.ConduitContents conduitContents = soaInfo.conduits[num].GetContents(this.manager);
					if (conduitContents.mass <= 0f)
					{
						conduitContents = ConduitFlow.ConduitContents.Empty;
					}
					soaInfo.initialContents[num] = conduitContents;
					this.manager.grid[num2].contents = conduitContents;
				}
			}
		}

		// Token: 0x02002813 RID: 10259
		private class InvalidateLastFlow : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600CA9B RID: 51867 RVA: 0x0041812B File Offset: 0x0041632B
			public InvalidateLastFlow()
				: base("InvalidateLastFlow")
			{
			}

			// Token: 0x0600CA9C RID: 51868 RVA: 0x00418138 File Offset: 0x00416338
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					soaInfo.lastFlowInfo[num] = ConduitFlow.ConduitFlowInfo.DEFAULT;
				}
			}
		}

		// Token: 0x02002814 RID: 10260
		private class PublishTemperatureToGame : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600CA9D RID: 51869 RVA: 0x0041816C File Offset: 0x0041636C
			public PublishTemperatureToGame()
				: base("PublishTemperatureToGame")
			{
			}

			// Token: 0x0600CA9E RID: 51870 RVA: 0x0041817C File Offset: 0x0041637C
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					Game.Instance.conduitTemperatureManager.SetData(soaInfo.temperatureHandles[num], ref this.manager.grid[soaInfo.cells[num]].contents);
				}
			}
		}

		// Token: 0x02002815 RID: 10261
		private class PublishDiseaseToGame : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600CA9F RID: 51871 RVA: 0x004181DB File Offset: 0x004163DB
			public PublishDiseaseToGame()
				: base("PublishDiseaseToGame")
			{
			}

			// Token: 0x0600CAA0 RID: 51872 RVA: 0x004181E8 File Offset: 0x004163E8
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					Game.Instance.conduitDiseaseManager.SetData(soaInfo.diseaseHandles[num], ref this.manager.grid[soaInfo.cells[num]].contents);
				}
			}
		}

		// Token: 0x02002816 RID: 10262
		private class FlowThroughVacuum : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600CAA1 RID: 51873 RVA: 0x00418247 File Offset: 0x00416447
			public FlowThroughVacuum()
				: base("FlowThroughVacuum")
			{
			}

			// Token: 0x0600CAA2 RID: 51874 RVA: 0x00418254 File Offset: 0x00416454
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					ConduitFlow.Conduit conduit = soaInfo.conduits[num];
					int cell = conduit.GetCell(this.manager);
					if (this.manager.grid[cell].contents.element == SimHashes.Vacuum)
					{
						soaInfo.srcFlowDirections[conduit.idx] = ConduitFlow.FlowDirections.None;
					}
				}
			}
		}
	}

	// Token: 0x020017AE RID: 6062
	[DebuggerDisplay("{priority} {callback.Target.name} {callback.Target} {callback.Method}")]
	public struct ConduitUpdater
	{
		// Token: 0x0400769D RID: 30365
		public ConduitFlowPriority priority;

		// Token: 0x0400769E RID: 30366
		public Action<float> callback;
	}

	// Token: 0x020017AF RID: 6063
	[DebuggerDisplay("conduit {conduitIdx}:{contents.element}")]
	public struct GridNode
	{
		// Token: 0x0400769F RID: 30367
		public int conduitIdx;

		// Token: 0x040076A0 RID: 30368
		public ConduitFlow.ConduitContents contents;
	}

	// Token: 0x020017B0 RID: 6064
	public struct SerializedContents
	{
		// Token: 0x06009A2E RID: 39470 RVA: 0x00389610 File Offset: 0x00387810
		public SerializedContents(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count)
		{
			this.element = element;
			this.mass = mass;
			this.temperature = temperature;
			this.diseaseHash = ((disease_idx != byte.MaxValue) ? Db.Get().Diseases[(int)disease_idx].id.GetHashCode() : 0);
			this.diseaseCount = disease_count;
			if (this.diseaseCount <= 0)
			{
				this.diseaseHash = 0;
			}
		}

		// Token: 0x06009A2F RID: 39471 RVA: 0x0038967D File Offset: 0x0038787D
		public SerializedContents(ConduitFlow.ConduitContents src)
		{
			this = new ConduitFlow.SerializedContents(src.element, src.mass, src.temperature, src.diseaseIdx, src.diseaseCount);
		}

		// Token: 0x040076A1 RID: 30369
		public SimHashes element;

		// Token: 0x040076A2 RID: 30370
		public float mass;

		// Token: 0x040076A3 RID: 30371
		public float temperature;

		// Token: 0x040076A4 RID: 30372
		public int diseaseHash;

		// Token: 0x040076A5 RID: 30373
		public int diseaseCount;
	}

	// Token: 0x020017B1 RID: 6065
	[Flags]
	public enum FlowDirections : byte
	{
		// Token: 0x040076A7 RID: 30375
		None = 0,
		// Token: 0x040076A8 RID: 30376
		Down = 1,
		// Token: 0x040076A9 RID: 30377
		Left = 2,
		// Token: 0x040076AA RID: 30378
		Right = 4,
		// Token: 0x040076AB RID: 30379
		Up = 8,
		// Token: 0x040076AC RID: 30380
		All = 15
	}

	// Token: 0x020017B2 RID: 6066
	[DebuggerDisplay("conduits l:{left}, r:{right}, u:{up}, d:{down}")]
	public struct ConduitConnections
	{
		// Token: 0x06009A30 RID: 39472 RVA: 0x003896A4 File Offset: 0x003878A4
		public int GetConnection(ConduitFlow.FlowDirections dir)
		{
			switch (dir)
			{
			case ConduitFlow.FlowDirections.Down:
				return this.down;
			case ConduitFlow.FlowDirections.Left:
				return this.left;
			case ConduitFlow.FlowDirections.Down | ConduitFlow.FlowDirections.Left:
				break;
			case ConduitFlow.FlowDirections.Right:
				return this.right;
			default:
				if (dir == ConduitFlow.FlowDirections.Up)
				{
					return this.up;
				}
				break;
			}
			return -1;
		}

		// Token: 0x040076AD RID: 30381
		public int left;

		// Token: 0x040076AE RID: 30382
		public int right;

		// Token: 0x040076AF RID: 30383
		public int up;

		// Token: 0x040076B0 RID: 30384
		public int down;

		// Token: 0x040076B1 RID: 30385
		public static readonly ConduitFlow.ConduitConnections DEFAULT = new ConduitFlow.ConduitConnections
		{
			left = -1,
			right = -1,
			up = -1,
			down = -1
		};
	}

	// Token: 0x020017B3 RID: 6067
	[DebuggerDisplay("{direction}:{contents.element}")]
	public struct ConduitFlowInfo
	{
		// Token: 0x040076B2 RID: 30386
		public ConduitFlow.FlowDirections direction;

		// Token: 0x040076B3 RID: 30387
		public ConduitFlow.ConduitContents contents;

		// Token: 0x040076B4 RID: 30388
		public static readonly ConduitFlow.ConduitFlowInfo DEFAULT = new ConduitFlow.ConduitFlowInfo
		{
			direction = ConduitFlow.FlowDirections.None,
			contents = ConduitFlow.ConduitContents.Empty
		};
	}

	// Token: 0x020017B4 RID: 6068
	[DebuggerDisplay("conduit {idx}")]
	[Serializable]
	public struct Conduit : IEquatable<ConduitFlow.Conduit>
	{
		// Token: 0x06009A33 RID: 39475 RVA: 0x0038974F File Offset: 0x0038794F
		public Conduit(int idx)
		{
			this.idx = idx;
		}

		// Token: 0x06009A34 RID: 39476 RVA: 0x00389758 File Offset: 0x00387958
		public ConduitFlow.FlowDirections GetPermittedFlowDirections(ConduitFlow manager)
		{
			return manager.soaInfo.GetPermittedFlowDirections(this.idx);
		}

		// Token: 0x06009A35 RID: 39477 RVA: 0x0038976B File Offset: 0x0038796B
		public void SetPermittedFlowDirections(ConduitFlow.FlowDirections permitted, ConduitFlow manager)
		{
			manager.soaInfo.SetPermittedFlowDirections(this.idx, permitted);
		}

		// Token: 0x06009A36 RID: 39478 RVA: 0x0038977F File Offset: 0x0038797F
		public ConduitFlow.FlowDirections GetTargetFlowDirection(ConduitFlow manager)
		{
			return manager.soaInfo.GetTargetFlowDirection(this.idx);
		}

		// Token: 0x06009A37 RID: 39479 RVA: 0x00389792 File Offset: 0x00387992
		public void SetTargetFlowDirection(ConduitFlow.FlowDirections directions, ConduitFlow manager)
		{
			manager.soaInfo.SetTargetFlowDirection(this.idx, directions);
		}

		// Token: 0x06009A38 RID: 39480 RVA: 0x003897A8 File Offset: 0x003879A8
		public ConduitFlow.ConduitContents GetContents(ConduitFlow manager)
		{
			int cell = manager.soaInfo.GetCell(this.idx);
			ConduitFlow.ConduitContents contents = manager.grid[cell].contents;
			ConduitFlow.SOAInfo soaInfo = manager.soaInfo;
			contents.temperature = soaInfo.GetConduitTemperature(this.idx);
			ConduitDiseaseManager.Data diseaseData = soaInfo.GetDiseaseData(this.idx);
			contents.diseaseIdx = diseaseData.diseaseIdx;
			contents.diseaseCount = diseaseData.diseaseCount;
			return contents;
		}

		// Token: 0x06009A39 RID: 39481 RVA: 0x0038981C File Offset: 0x00387A1C
		public void SetContents(ConduitFlow manager, ConduitFlow.ConduitContents contents)
		{
			int cell = manager.soaInfo.GetCell(this.idx);
			manager.grid[cell].contents = contents;
			ConduitFlow.SOAInfo soaInfo = manager.soaInfo;
			soaInfo.SetConduitTemperatureData(this.idx, ref contents);
			soaInfo.ForcePermanentDiseaseContainer(this.idx, contents.diseaseIdx != byte.MaxValue);
			soaInfo.SetDiseaseData(this.idx, ref contents);
		}

		// Token: 0x06009A3A RID: 39482 RVA: 0x0038988A File Offset: 0x00387A8A
		public ConduitFlow.ConduitFlowInfo GetLastFlowInfo(ConduitFlow manager)
		{
			return manager.soaInfo.GetLastFlowInfo(this.idx);
		}

		// Token: 0x06009A3B RID: 39483 RVA: 0x0038989D File Offset: 0x00387A9D
		public ConduitFlow.ConduitContents GetInitialContents(ConduitFlow manager)
		{
			return manager.soaInfo.GetInitialContents(this.idx);
		}

		// Token: 0x06009A3C RID: 39484 RVA: 0x003898B0 File Offset: 0x00387AB0
		public int GetCell(ConduitFlow manager)
		{
			return manager.soaInfo.GetCell(this.idx);
		}

		// Token: 0x06009A3D RID: 39485 RVA: 0x003898C3 File Offset: 0x00387AC3
		public bool Equals(ConduitFlow.Conduit other)
		{
			return this.idx == other.idx;
		}

		// Token: 0x040076B5 RID: 30389
		public static readonly ConduitFlow.Conduit Invalid = new ConduitFlow.Conduit(-1);

		// Token: 0x040076B6 RID: 30390
		public readonly int idx;
	}

	// Token: 0x020017B5 RID: 6069
	[DebuggerDisplay("{element} M:{mass} T:{temperature}")]
	public struct ConduitContents
	{
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06009A3F RID: 39487 RVA: 0x003898E0 File Offset: 0x00387AE0
		public float mass
		{
			get
			{
				return this.initial_mass + this.added_mass - this.removed_mass;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06009A40 RID: 39488 RVA: 0x003898F6 File Offset: 0x00387AF6
		public float movable_mass
		{
			get
			{
				return this.initial_mass - this.removed_mass;
			}
		}

		// Token: 0x06009A41 RID: 39489 RVA: 0x00389908 File Offset: 0x00387B08
		public ConduitContents(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count)
		{
			global::Debug.Assert(!float.IsNaN(temperature));
			this.element = element;
			this.initial_mass = mass;
			this.added_mass = 0f;
			this.removed_mass = 0f;
			this.temperature = temperature;
			this.diseaseIdx = disease_idx;
			this.diseaseCount = disease_count;
		}

		// Token: 0x06009A42 RID: 39490 RVA: 0x0038995E File Offset: 0x00387B5E
		public void ConsolidateMass()
		{
			this.initial_mass += this.added_mass;
			this.added_mass = 0f;
			this.initial_mass -= this.removed_mass;
			this.removed_mass = 0f;
		}

		// Token: 0x06009A43 RID: 39491 RVA: 0x0038999C File Offset: 0x00387B9C
		public float GetEffectiveCapacity(float maximum_capacity)
		{
			float mass = this.mass;
			return Mathf.Max(0f, maximum_capacity - mass);
		}

		// Token: 0x06009A44 RID: 39492 RVA: 0x003899BD File Offset: 0x00387BBD
		public void AddMass(float amount)
		{
			global::Debug.Assert(0f <= amount);
			this.added_mass += amount;
		}

		// Token: 0x06009A45 RID: 39493 RVA: 0x003899E0 File Offset: 0x00387BE0
		public float RemoveMass(float amount)
		{
			global::Debug.Assert(0f <= amount);
			float num = 0f;
			float num2 = this.mass - amount;
			if (num2 < 0f)
			{
				amount += num2;
				num = -num2;
				global::Debug.Assert(false);
			}
			this.removed_mass += amount;
			return num;
		}

		// Token: 0x040076B7 RID: 30391
		public SimHashes element;

		// Token: 0x040076B8 RID: 30392
		private float initial_mass;

		// Token: 0x040076B9 RID: 30393
		private float added_mass;

		// Token: 0x040076BA RID: 30394
		private float removed_mass;

		// Token: 0x040076BB RID: 30395
		public float temperature;

		// Token: 0x040076BC RID: 30396
		public byte diseaseIdx;

		// Token: 0x040076BD RID: 30397
		public int diseaseCount;

		// Token: 0x040076BE RID: 30398
		public static readonly ConduitFlow.ConduitContents Empty = new ConduitFlow.ConduitContents
		{
			element = SimHashes.Vacuum,
			initial_mass = 0f,
			added_mass = 0f,
			removed_mass = 0f,
			temperature = 0f,
			diseaseIdx = byte.MaxValue,
			diseaseCount = 0
		};
	}

	// Token: 0x020017B6 RID: 6070
	[DebuggerDisplay("{network.ConduitType}:{cells.Count}")]
	private struct Network
	{
		// Token: 0x040076BF RID: 30399
		public List<int> cells;

		// Token: 0x040076C0 RID: 30400
		public FlowUtilityNetwork network;
	}

	// Token: 0x020017B7 RID: 6071
	private struct BuildNetworkTask : IWorkItem<ConduitFlow>
	{
		// Token: 0x06009A47 RID: 39495 RVA: 0x00389AA0 File Offset: 0x00387CA0
		public BuildNetworkTask(ConduitFlow.Network network, int conduit_count)
		{
			this.network = network;
			this.order_dfs_stack = StackPool<ConduitFlow.BuildNetworkTask.OrderNode, ConduitFlow>.Allocate();
			this.visited = HashSetPool<int, ConduitFlow>.Allocate();
			this.from_sources = ListPool<KeyValuePair<int, int>, ConduitFlow>.Allocate();
			this.from_sinks = ListPool<KeyValuePair<int, int>, ConduitFlow>.Allocate();
			this.from_sources_graph = new ConduitFlow.BuildNetworkTask.Graph(network.network);
			this.from_sinks_graph = new ConduitFlow.BuildNetworkTask.Graph(network.network);
		}

		// Token: 0x06009A48 RID: 39496 RVA: 0x00389B04 File Offset: 0x00387D04
		public void Finish()
		{
			this.order_dfs_stack.Recycle();
			this.visited.Recycle();
			this.from_sources.Recycle();
			this.from_sinks.Recycle();
			this.from_sources_graph.Recycle();
			this.from_sinks_graph.Recycle();
		}

		// Token: 0x06009A49 RID: 39497 RVA: 0x00389B54 File Offset: 0x00387D54
		private void ComputeFlow(ConduitFlow outer)
		{
			this.from_sources_graph.Build(outer, this.network.network.sources, this.network.network.sinks, true);
			this.from_sinks_graph.Build(outer, this.network.network.sinks, this.network.network.sources, false);
			this.from_sources_graph.Merge(this.from_sinks_graph);
			this.from_sources_graph.BreakCycles();
			this.from_sources_graph.WriteFlow(false);
			this.from_sinks_graph.WriteFlow(true);
		}

		// Token: 0x06009A4A RID: 39498 RVA: 0x00389BF0 File Offset: 0x00387DF0
		private void ReverseTopologicalOrderingPush(ConduitFlow outer, List<int> result, int start_cell)
		{
			global::Debug.Assert(this.order_dfs_stack.Count == 0);
			ConduitFlow.BuildNetworkTask.OrderNode orderNode = default(ConduitFlow.BuildNetworkTask.OrderNode);
			orderNode.idx = outer.grid[start_cell].conduitIdx;
			orderNode.direction = ConduitFlow.FlowDirections.Down;
			orderNode.permited = outer.soaInfo.GetPermittedFlowDirections(orderNode.idx);
			if (orderNode.idx == -1 || !this.visited.Add(orderNode.idx))
			{
				return;
			}
			this.order_dfs_stack.Push(orderNode);
			while (this.order_dfs_stack.Count > 0)
			{
				ConduitFlow.BuildNetworkTask.OrderNode orderNode2 = this.order_dfs_stack.Pop();
				if ((orderNode2.direction & ConduitFlow.FlowDirections.All) != ConduitFlow.FlowDirections.None)
				{
					ConduitFlow.FlowDirections direction = orderNode2.direction;
					orderNode2.direction = direction << 1;
					this.order_dfs_stack.Push(orderNode2);
					if ((orderNode2.permited & direction) != ConduitFlow.FlowDirections.None)
					{
						orderNode.idx = outer.soaInfo.GetConduitConnections(orderNode2.idx).GetConnection(direction);
						orderNode.direction = ConduitFlow.FlowDirections.Down;
						orderNode.permited = outer.soaInfo.GetPermittedFlowDirections(orderNode.idx);
						if (orderNode.idx != -1 && this.visited.Add(orderNode.idx))
						{
							this.order_dfs_stack.Push(orderNode);
						}
					}
				}
				else
				{
					result.Add(outer.soaInfo.GetCell(orderNode2.idx));
				}
			}
		}

		// Token: 0x06009A4B RID: 39499 RVA: 0x00389D58 File Offset: 0x00387F58
		private void ReverseTopologicalOrderingPull(ConduitFlow outer, List<int> result, int start_cell)
		{
			global::Debug.Assert(this.order_dfs_stack.Count == 0);
			ConduitFlow.BuildNetworkTask.OrderNode orderNode = default(ConduitFlow.BuildNetworkTask.OrderNode);
			orderNode.idx = outer.grid[start_cell].conduitIdx;
			orderNode.direction = ConduitFlow.FlowDirections.Down;
			orderNode.permited = outer.soaInfo.GetPermittedFlowDirections(orderNode.idx);
			if (orderNode.idx == -1 || !this.visited.Add(orderNode.idx))
			{
				return;
			}
			this.order_dfs_stack.Push(orderNode);
			while (this.order_dfs_stack.Count > 0)
			{
				ConduitFlow.BuildNetworkTask.OrderNode orderNode2 = this.order_dfs_stack.Pop();
				if ((orderNode2.direction & ConduitFlow.FlowDirections.All) != ConduitFlow.FlowDirections.None)
				{
					ConduitFlow.FlowDirections direction = orderNode2.direction;
					orderNode2.direction = direction << 1;
					this.order_dfs_stack.Push(orderNode2);
					orderNode.idx = outer.soaInfo.GetConduitConnections(orderNode2.idx).GetConnection(ConduitFlow.Opposite(direction));
					if (orderNode.idx != -1)
					{
						orderNode.direction = ConduitFlow.FlowDirections.Down;
						orderNode.permited = outer.soaInfo.GetPermittedFlowDirections(orderNode.idx);
						if ((orderNode.permited & direction) != ConduitFlow.FlowDirections.None && this.visited.Add(orderNode.idx))
						{
							this.order_dfs_stack.Push(orderNode);
						}
					}
				}
				else
				{
					result.Add(outer.soaInfo.GetCell(orderNode2.idx));
				}
			}
		}

		// Token: 0x06009A4C RID: 39500 RVA: 0x00389EC0 File Offset: 0x003880C0
		private void ComputeOrder(ConduitFlow outer)
		{
			this.network.cells.Capacity = Math.Max(this.network.cells.Capacity, outer.soaInfo.NumEntries);
			foreach (int num in this.from_sources_graph.sources)
			{
				this.ReverseTopologicalOrderingPush(outer, this.network.cells, num);
			}
			foreach (int num2 in this.from_sources_graph.dead_ends)
			{
				this.ReverseTopologicalOrderingPush(outer, this.network.cells, num2);
			}
			int count = this.network.cells.Count;
			foreach (int num3 in this.from_sinks_graph.sources)
			{
				this.ReverseTopologicalOrderingPull(outer, this.network.cells, num3);
			}
			foreach (int num4 in this.from_sinks_graph.dead_ends)
			{
				this.ReverseTopologicalOrderingPull(outer, this.network.cells, num4);
			}
			if (count != this.network.cells.Count)
			{
				this.network.cells.Reverse(count, this.network.cells.Count - count);
			}
		}

		// Token: 0x06009A4D RID: 39501 RVA: 0x0038A098 File Offset: 0x00388298
		public void Run(ConduitFlow outer, int threadIndex)
		{
			this.ComputeFlow(outer);
			this.ComputeOrder(outer);
		}

		// Token: 0x040076C1 RID: 30401
		private ConduitFlow.Network network;

		// Token: 0x040076C2 RID: 30402
		private StackPool<ConduitFlow.BuildNetworkTask.OrderNode, ConduitFlow>.PooledStack order_dfs_stack;

		// Token: 0x040076C3 RID: 30403
		private HashSetPool<int, ConduitFlow>.PooledHashSet visited;

		// Token: 0x040076C4 RID: 30404
		private ListPool<KeyValuePair<int, int>, ConduitFlow>.PooledList from_sources;

		// Token: 0x040076C5 RID: 30405
		private ListPool<KeyValuePair<int, int>, ConduitFlow>.PooledList from_sinks;

		// Token: 0x040076C6 RID: 30406
		private ConduitFlow.BuildNetworkTask.Graph from_sources_graph;

		// Token: 0x040076C7 RID: 30407
		private ConduitFlow.BuildNetworkTask.Graph from_sinks_graph;

		// Token: 0x02002817 RID: 10263
		[DebuggerDisplay("cell {cell}:{distance}")]
		private struct OrderNode
		{
			// Token: 0x0400B1BA RID: 45498
			public int idx;

			// Token: 0x0400B1BB RID: 45499
			public ConduitFlow.FlowDirections direction;

			// Token: 0x0400B1BC RID: 45500
			public ConduitFlow.FlowDirections permited;
		}

		// Token: 0x02002818 RID: 10264
		[DebuggerDisplay("vertices:{vertex_cells.Count}, edges:{edges.Count}")]
		private struct Graph
		{
			// Token: 0x0600CAA3 RID: 51875 RVA: 0x004182C8 File Offset: 0x004164C8
			public Graph(FlowUtilityNetwork network)
			{
				this.conduit_flow = null;
				this.vertex_cells = HashSetPool<int, ConduitFlow>.Allocate();
				this.edges = ListPool<ConduitFlow.BuildNetworkTask.Graph.Edge, ConduitFlow>.Allocate();
				this.cycles = ListPool<ConduitFlow.BuildNetworkTask.Graph.Edge, ConduitFlow>.Allocate();
				this.bfs_traversal = QueuePool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.Allocate();
				this.visited = HashSetPool<int, ConduitFlow>.Allocate();
				this.pseudo_sources = ListPool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.Allocate();
				this.sources = HashSetPool<int, ConduitFlow>.Allocate();
				this.sinks = HashSetPool<int, ConduitFlow>.Allocate();
				this.dfs_path = HashSetPool<ConduitFlow.BuildNetworkTask.Graph.DFSNode, ConduitFlow>.Allocate();
				this.dfs_traversal = ListPool<ConduitFlow.BuildNetworkTask.Graph.DFSNode, ConduitFlow>.Allocate();
				this.dead_ends = HashSetPool<int, ConduitFlow>.Allocate();
				this.cycle_vertices = ListPool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.Allocate();
			}

			// Token: 0x0600CAA4 RID: 51876 RVA: 0x00418360 File Offset: 0x00416560
			public void Recycle()
			{
				this.vertex_cells.Recycle();
				this.edges.Recycle();
				this.cycles.Recycle();
				this.bfs_traversal.Recycle();
				this.visited.Recycle();
				this.pseudo_sources.Recycle();
				this.sources.Recycle();
				this.sinks.Recycle();
				this.dfs_path.Recycle();
				this.dfs_traversal.Recycle();
				this.dead_ends.Recycle();
				this.cycle_vertices.Recycle();
			}

			// Token: 0x0600CAA5 RID: 51877 RVA: 0x004183F4 File Offset: 0x004165F4
			public void Build(ConduitFlow conduit_flow, List<FlowUtilityNetwork.IItem> sources, List<FlowUtilityNetwork.IItem> sinks, bool are_dead_ends_pseudo_sources)
			{
				this.conduit_flow = conduit_flow;
				this.sources.Clear();
				for (int i = 0; i < sources.Count; i++)
				{
					int cell = sources[i].Cell;
					if (conduit_flow.grid[cell].conduitIdx != -1)
					{
						this.sources.Add(cell);
					}
				}
				this.sinks.Clear();
				for (int j = 0; j < sinks.Count; j++)
				{
					int cell2 = sinks[j].Cell;
					if (conduit_flow.grid[cell2].conduitIdx != -1)
					{
						this.sinks.Add(cell2);
					}
				}
				global::Debug.Assert(this.bfs_traversal.Count == 0);
				this.visited.Clear();
				foreach (int num in this.sources)
				{
					this.bfs_traversal.Enqueue(new ConduitFlow.BuildNetworkTask.Graph.Vertex
					{
						cell = num,
						direction = ConduitFlow.FlowDirections.None
					});
					this.visited.Add(num);
				}
				this.pseudo_sources.Clear();
				this.dead_ends.Clear();
				this.cycles.Clear();
				while (this.bfs_traversal.Count != 0)
				{
					ConduitFlow.BuildNetworkTask.Graph.Vertex node = this.bfs_traversal.Dequeue();
					this.vertex_cells.Add(node.cell);
					ConduitFlow.FlowDirections flowDirections = ConduitFlow.FlowDirections.None;
					int num2 = 4;
					if (node.direction != ConduitFlow.FlowDirections.None)
					{
						flowDirections = ConduitFlow.Opposite(node.direction);
						num2 = 3;
					}
					int conduitIdx = conduit_flow.grid[node.cell].conduitIdx;
					for (int num3 = 0; num3 != num2; num3++)
					{
						flowDirections = ConduitFlow.ComputeNextFlowDirection(flowDirections);
						ConduitFlow.Conduit conduitFromDirection = conduit_flow.soaInfo.GetConduitFromDirection(conduitIdx, flowDirections);
						ConduitFlow.BuildNetworkTask.Graph.Vertex new_node = this.WalkPath(conduitIdx, conduitFromDirection.idx, flowDirections, are_dead_ends_pseudo_sources);
						if (new_node.is_valid)
						{
							ConduitFlow.BuildNetworkTask.Graph.Edge edge2 = new ConduitFlow.BuildNetworkTask.Graph.Edge
							{
								vertices = new ConduitFlow.BuildNetworkTask.Graph.Vertex[]
								{
									new ConduitFlow.BuildNetworkTask.Graph.Vertex
									{
										cell = node.cell,
										direction = flowDirections
									},
									new_node
								}
							};
							if (new_node.cell == node.cell)
							{
								this.cycles.Add(edge2);
							}
							else if (!this.edges.Any((ConduitFlow.BuildNetworkTask.Graph.Edge edge) => edge.vertices[0].cell == new_node.cell && edge.vertices[1].cell == node.cell) && !this.edges.Contains(edge2))
							{
								this.edges.Add(edge2);
								if (this.visited.Add(new_node.cell))
								{
									if (this.IsSink(new_node.cell))
									{
										this.pseudo_sources.Add(new_node);
									}
									else
									{
										this.bfs_traversal.Enqueue(new_node);
									}
								}
							}
						}
					}
					if (this.bfs_traversal.Count == 0)
					{
						foreach (ConduitFlow.BuildNetworkTask.Graph.Vertex vertex in this.pseudo_sources)
						{
							this.bfs_traversal.Enqueue(vertex);
						}
						this.pseudo_sources.Clear();
					}
				}
			}

			// Token: 0x0600CAA6 RID: 51878 RVA: 0x004187C0 File Offset: 0x004169C0
			private bool IsEndpoint(int cell)
			{
				global::Debug.Assert(cell != -1);
				return this.conduit_flow.grid[cell].conduitIdx == -1 || this.sources.Contains(cell) || this.sinks.Contains(cell) || this.dead_ends.Contains(cell);
			}

			// Token: 0x0600CAA7 RID: 51879 RVA: 0x0041881C File Offset: 0x00416A1C
			private bool IsSink(int cell)
			{
				return this.sinks.Contains(cell);
			}

			// Token: 0x0600CAA8 RID: 51880 RVA: 0x0041882C File Offset: 0x00416A2C
			private bool IsJunction(int cell)
			{
				global::Debug.Assert(cell != -1);
				ConduitFlow.GridNode gridNode = this.conduit_flow.grid[cell];
				global::Debug.Assert(gridNode.conduitIdx != -1);
				ConduitFlow.ConduitConnections conduitConnections = this.conduit_flow.soaInfo.GetConduitConnections(gridNode.conduitIdx);
				return 2 < this.JunctionValue(conduitConnections.down) + this.JunctionValue(conduitConnections.left) + this.JunctionValue(conduitConnections.up) + this.JunctionValue(conduitConnections.right);
			}

			// Token: 0x0600CAA9 RID: 51881 RVA: 0x004188B5 File Offset: 0x00416AB5
			private int JunctionValue(int conduit)
			{
				if (conduit != -1)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x0600CAAA RID: 51882 RVA: 0x004188C0 File Offset: 0x00416AC0
			private ConduitFlow.BuildNetworkTask.Graph.Vertex WalkPath(int root_conduit, int conduit, ConduitFlow.FlowDirections direction, bool are_dead_ends_pseudo_sources)
			{
				if (conduit == -1)
				{
					return ConduitFlow.BuildNetworkTask.Graph.Vertex.INVALID;
				}
				int cell;
				for (;;)
				{
					cell = this.conduit_flow.soaInfo.GetCell(conduit);
					if (this.IsEndpoint(cell) || this.IsJunction(cell))
					{
						break;
					}
					direction = ConduitFlow.Opposite(direction);
					bool flag = true;
					for (int num = 0; num != 3; num++)
					{
						direction = ConduitFlow.ComputeNextFlowDirection(direction);
						ConduitFlow.Conduit conduitFromDirection = this.conduit_flow.soaInfo.GetConduitFromDirection(conduit, direction);
						if (conduitFromDirection.idx != -1)
						{
							conduit = conduitFromDirection.idx;
							flag = false;
							break;
						}
					}
					if (flag)
					{
						goto Block_4;
					}
				}
				return new ConduitFlow.BuildNetworkTask.Graph.Vertex
				{
					cell = cell,
					direction = direction
				};
				Block_4:
				if (are_dead_ends_pseudo_sources)
				{
					this.dead_ends.Add(cell);
					this.pseudo_sources.Add(new ConduitFlow.BuildNetworkTask.Graph.Vertex
					{
						cell = cell,
						direction = ConduitFlow.ComputeNextFlowDirection(direction)
					});
					return ConduitFlow.BuildNetworkTask.Graph.Vertex.INVALID;
				}
				ConduitFlow.BuildNetworkTask.Graph.Vertex vertex = default(ConduitFlow.BuildNetworkTask.Graph.Vertex);
				vertex.cell = cell;
				direction = (vertex.direction = ConduitFlow.Opposite(ConduitFlow.ComputeNextFlowDirection(direction)));
				return vertex;
			}

			// Token: 0x0600CAAB RID: 51883 RVA: 0x004189CC File Offset: 0x00416BCC
			public void Merge(ConduitFlow.BuildNetworkTask.Graph inverted_graph)
			{
				using (List<ConduitFlow.BuildNetworkTask.Graph.Edge>.Enumerator enumerator = inverted_graph.edges.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ConduitFlow.BuildNetworkTask.Graph.Edge inverted_edge2 = enumerator.Current;
						ConduitFlow.BuildNetworkTask.Graph.Edge candidate = inverted_edge2.Invert();
						if (!this.edges.Any((ConduitFlow.BuildNetworkTask.Graph.Edge edge) => edge.Equals(inverted_edge2) || edge.Equals(candidate)))
						{
							this.edges.Add(candidate);
							this.vertex_cells.Add(candidate.vertices[0].cell);
							this.vertex_cells.Add(candidate.vertices[1].cell);
						}
					}
				}
				int num = 1000;
				for (int num2 = 0; num2 != num; num2++)
				{
					global::Debug.Assert(num2 != num - 1);
					bool flag = false;
					using (HashSet<int>.Enumerator enumerator2 = this.vertex_cells.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int cell = enumerator2.Current;
							if (!this.IsSink(cell) && !this.edges.Any((ConduitFlow.BuildNetworkTask.Graph.Edge edge) => edge.vertices[0].cell == cell))
							{
								int num3 = inverted_graph.edges.FindIndex((ConduitFlow.BuildNetworkTask.Graph.Edge inverted_edge) => inverted_edge.vertices[1].cell == cell);
								if (num3 != -1)
								{
									ConduitFlow.BuildNetworkTask.Graph.Edge edge3 = inverted_graph.edges[num3];
									for (int num4 = 0; num4 != this.edges.Count; num4++)
									{
										ConduitFlow.BuildNetworkTask.Graph.Edge edge2 = this.edges[num4];
										if (edge2.vertices[0].cell == edge3.vertices[0].cell && edge2.vertices[1].cell == edge3.vertices[1].cell)
										{
											this.edges[num4] = edge2.Invert();
										}
									}
									flag = true;
									break;
								}
							}
						}
					}
					if (!flag)
					{
						break;
					}
				}
			}

			// Token: 0x0600CAAC RID: 51884 RVA: 0x00418C30 File Offset: 0x00416E30
			public void BreakCycles()
			{
				this.visited.Clear();
				foreach (int num in this.vertex_cells)
				{
					if (!this.visited.Contains(num))
					{
						this.dfs_path.Clear();
						this.dfs_traversal.Clear();
						this.dfs_traversal.Add(new ConduitFlow.BuildNetworkTask.Graph.DFSNode
						{
							cell = num,
							parent = null
						});
						while (this.dfs_traversal.Count != 0)
						{
							ConduitFlow.BuildNetworkTask.Graph.DFSNode dfsnode = this.dfs_traversal[this.dfs_traversal.Count - 1];
							this.dfs_traversal.RemoveAt(this.dfs_traversal.Count - 1);
							bool flag = false;
							for (ConduitFlow.BuildNetworkTask.Graph.DFSNode dfsnode2 = dfsnode.parent; dfsnode2 != null; dfsnode2 = dfsnode2.parent)
							{
								if (dfsnode2.cell == dfsnode.cell)
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								for (int num2 = this.edges.Count - 1; num2 != -1; num2--)
								{
									ConduitFlow.BuildNetworkTask.Graph.Edge edge = this.edges[num2];
									if (edge.vertices[0].cell == dfsnode.parent.cell && edge.vertices[1].cell == dfsnode.cell)
									{
										this.cycles.Add(edge);
										this.edges.RemoveAt(num2);
									}
								}
							}
							else if (this.visited.Add(dfsnode.cell))
							{
								foreach (ConduitFlow.BuildNetworkTask.Graph.Edge edge2 in this.edges)
								{
									if (edge2.vertices[0].cell == dfsnode.cell)
									{
										this.dfs_traversal.Add(new ConduitFlow.BuildNetworkTask.Graph.DFSNode
										{
											cell = edge2.vertices[1].cell,
											parent = dfsnode
										});
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x0600CAAD RID: 51885 RVA: 0x00418E80 File Offset: 0x00417080
			public void WriteFlow(bool cycles_only = false)
			{
				if (!cycles_only)
				{
					foreach (ConduitFlow.BuildNetworkTask.Graph.Edge edge in this.edges)
					{
						ConduitFlow.BuildNetworkTask.Graph.Edge.VertexIterator vertexIterator = edge.Iter(this.conduit_flow);
						while (vertexIterator.IsValid())
						{
							this.conduit_flow.soaInfo.AddPermittedFlowDirections(this.conduit_flow.grid[vertexIterator.cell].conduitIdx, vertexIterator.direction);
							vertexIterator.Next();
						}
					}
				}
				foreach (ConduitFlow.BuildNetworkTask.Graph.Edge edge2 in this.cycles)
				{
					this.cycle_vertices.Clear();
					ConduitFlow.BuildNetworkTask.Graph.Edge.VertexIterator vertexIterator2 = edge2.Iter(this.conduit_flow);
					vertexIterator2.Next();
					while (vertexIterator2.IsValid())
					{
						this.cycle_vertices.Add(new ConduitFlow.BuildNetworkTask.Graph.Vertex
						{
							cell = vertexIterator2.cell,
							direction = vertexIterator2.direction
						});
						vertexIterator2.Next();
					}
					if (this.cycle_vertices.Count > 1)
					{
						int i = 0;
						int num = this.cycle_vertices.Count - 1;
						ConduitFlow.FlowDirections flowDirections = edge2.vertices[0].direction;
						while (i <= num)
						{
							ConduitFlow.BuildNetworkTask.Graph.Vertex vertex = this.cycle_vertices[i];
							this.conduit_flow.soaInfo.AddPermittedFlowDirections(this.conduit_flow.grid[vertex.cell].conduitIdx, ConduitFlow.Opposite(flowDirections));
							flowDirections = vertex.direction;
							i++;
							ConduitFlow.BuildNetworkTask.Graph.Vertex vertex2 = this.cycle_vertices[num];
							this.conduit_flow.soaInfo.AddPermittedFlowDirections(this.conduit_flow.grid[vertex2.cell].conduitIdx, vertex2.direction);
							num--;
						}
						this.dead_ends.Add(this.cycle_vertices[i].cell);
						this.dead_ends.Add(this.cycle_vertices[num].cell);
					}
				}
			}

			// Token: 0x0400B1BD RID: 45501
			private ConduitFlow conduit_flow;

			// Token: 0x0400B1BE RID: 45502
			private HashSetPool<int, ConduitFlow>.PooledHashSet vertex_cells;

			// Token: 0x0400B1BF RID: 45503
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.Edge, ConduitFlow>.PooledList edges;

			// Token: 0x0400B1C0 RID: 45504
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.Edge, ConduitFlow>.PooledList cycles;

			// Token: 0x0400B1C1 RID: 45505
			private QueuePool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.PooledQueue bfs_traversal;

			// Token: 0x0400B1C2 RID: 45506
			private HashSetPool<int, ConduitFlow>.PooledHashSet visited;

			// Token: 0x0400B1C3 RID: 45507
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.PooledList pseudo_sources;

			// Token: 0x0400B1C4 RID: 45508
			public HashSetPool<int, ConduitFlow>.PooledHashSet sources;

			// Token: 0x0400B1C5 RID: 45509
			private HashSetPool<int, ConduitFlow>.PooledHashSet sinks;

			// Token: 0x0400B1C6 RID: 45510
			private HashSetPool<ConduitFlow.BuildNetworkTask.Graph.DFSNode, ConduitFlow>.PooledHashSet dfs_path;

			// Token: 0x0400B1C7 RID: 45511
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.DFSNode, ConduitFlow>.PooledList dfs_traversal;

			// Token: 0x0400B1C8 RID: 45512
			public HashSetPool<int, ConduitFlow>.PooledHashSet dead_ends;

			// Token: 0x0400B1C9 RID: 45513
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.PooledList cycle_vertices;

			// Token: 0x02003886 RID: 14470
			[DebuggerDisplay("{cell}:{direction}")]
			public struct Vertex : IEquatable<ConduitFlow.BuildNetworkTask.Graph.Vertex>
			{
				// Token: 0x17000CF6 RID: 3318
				// (get) Token: 0x0600EC72 RID: 60530 RVA: 0x0047455C File Offset: 0x0047275C
				public bool is_valid
				{
					get
					{
						return this.cell != -1;
					}
				}

				// Token: 0x0600EC73 RID: 60531 RVA: 0x0047456A File Offset: 0x0047276A
				public bool Equals(ConduitFlow.BuildNetworkTask.Graph.Vertex rhs)
				{
					return this.direction == rhs.direction && this.cell == rhs.cell;
				}

				// Token: 0x0400E475 RID: 58485
				public ConduitFlow.FlowDirections direction;

				// Token: 0x0400E476 RID: 58486
				public int cell;

				// Token: 0x0400E477 RID: 58487
				public static ConduitFlow.BuildNetworkTask.Graph.Vertex INVALID = new ConduitFlow.BuildNetworkTask.Graph.Vertex
				{
					direction = ConduitFlow.FlowDirections.None,
					cell = -1
				};
			}

			// Token: 0x02003887 RID: 14471
			[DebuggerDisplay("{vertices[0].cell}:{vertices[0].direction} -> {vertices[1].cell}:{vertices[1].direction}")]
			public struct Edge : IEquatable<ConduitFlow.BuildNetworkTask.Graph.Edge>
			{
				// Token: 0x17000CF7 RID: 3319
				// (get) Token: 0x0600EC75 RID: 60533 RVA: 0x004745B7 File Offset: 0x004727B7
				public bool is_valid
				{
					get
					{
						return this.vertices != null;
					}
				}

				// Token: 0x0600EC76 RID: 60534 RVA: 0x004745C4 File Offset: 0x004727C4
				public bool Equals(ConduitFlow.BuildNetworkTask.Graph.Edge rhs)
				{
					if (this.vertices == null)
					{
						return rhs.vertices == null;
					}
					return rhs.vertices != null && (this.vertices.Length == rhs.vertices.Length && this.vertices.Length == 2 && this.vertices[0].Equals(rhs.vertices[0])) && this.vertices[1].Equals(rhs.vertices[1]);
				}

				// Token: 0x0600EC77 RID: 60535 RVA: 0x00474648 File Offset: 0x00472848
				public ConduitFlow.BuildNetworkTask.Graph.Edge Invert()
				{
					return new ConduitFlow.BuildNetworkTask.Graph.Edge
					{
						vertices = new ConduitFlow.BuildNetworkTask.Graph.Vertex[]
						{
							new ConduitFlow.BuildNetworkTask.Graph.Vertex
							{
								cell = this.vertices[1].cell,
								direction = ConduitFlow.Opposite(this.vertices[1].direction)
							},
							new ConduitFlow.BuildNetworkTask.Graph.Vertex
							{
								cell = this.vertices[0].cell,
								direction = ConduitFlow.Opposite(this.vertices[0].direction)
							}
						}
					};
				}

				// Token: 0x0600EC78 RID: 60536 RVA: 0x004746F5 File Offset: 0x004728F5
				public ConduitFlow.BuildNetworkTask.Graph.Edge.VertexIterator Iter(ConduitFlow conduit_flow)
				{
					return new ConduitFlow.BuildNetworkTask.Graph.Edge.VertexIterator(conduit_flow, this);
				}

				// Token: 0x0400E478 RID: 58488
				public ConduitFlow.BuildNetworkTask.Graph.Vertex[] vertices;

				// Token: 0x0400E479 RID: 58489
				public static readonly ConduitFlow.BuildNetworkTask.Graph.Edge INVALID = new ConduitFlow.BuildNetworkTask.Graph.Edge
				{
					vertices = null
				};

				// Token: 0x02003BD8 RID: 15320
				[DebuggerDisplay("{cell}:{direction}")]
				public struct VertexIterator
				{
					// Token: 0x0600F297 RID: 62103 RVA: 0x00483623 File Offset: 0x00481823
					public VertexIterator(ConduitFlow conduit_flow, ConduitFlow.BuildNetworkTask.Graph.Edge edge)
					{
						this.conduit_flow = conduit_flow;
						this.edge = edge;
						this.cell = edge.vertices[0].cell;
						this.direction = edge.vertices[0].direction;
					}

					// Token: 0x0600F298 RID: 62104 RVA: 0x00483664 File Offset: 0x00481864
					public void Next()
					{
						int conduitIdx = this.conduit_flow.grid[this.cell].conduitIdx;
						ConduitFlow.Conduit conduitFromDirection = this.conduit_flow.soaInfo.GetConduitFromDirection(conduitIdx, this.direction);
						global::Debug.Assert(conduitFromDirection.idx != -1);
						this.cell = conduitFromDirection.GetCell(this.conduit_flow);
						if (this.cell == this.edge.vertices[1].cell)
						{
							return;
						}
						this.direction = ConduitFlow.Opposite(this.direction);
						bool flag = false;
						for (int num = 0; num != 3; num++)
						{
							this.direction = ConduitFlow.ComputeNextFlowDirection(this.direction);
							if (this.conduit_flow.soaInfo.GetConduitFromDirection(conduitFromDirection.idx, this.direction).idx != -1)
							{
								flag = true;
								break;
							}
						}
						global::Debug.Assert(flag);
						if (!flag)
						{
							this.cell = this.edge.vertices[1].cell;
						}
					}

					// Token: 0x0600F299 RID: 62105 RVA: 0x00483765 File Offset: 0x00481965
					public bool IsValid()
					{
						return this.cell != this.edge.vertices[1].cell;
					}

					// Token: 0x0400ECE6 RID: 60646
					public int cell;

					// Token: 0x0400ECE7 RID: 60647
					public ConduitFlow.FlowDirections direction;

					// Token: 0x0400ECE8 RID: 60648
					private ConduitFlow conduit_flow;

					// Token: 0x0400ECE9 RID: 60649
					private ConduitFlow.BuildNetworkTask.Graph.Edge edge;
				}
			}

			// Token: 0x02003888 RID: 14472
			[DebuggerDisplay("cell:{cell}, parent:{parent == null ? -1 : parent.cell}")]
			private class DFSNode
			{
				// Token: 0x0400E47A RID: 58490
				public int cell;

				// Token: 0x0400E47B RID: 58491
				public ConduitFlow.BuildNetworkTask.Graph.DFSNode parent;
			}
		}
	}

	// Token: 0x020017B8 RID: 6072
	private struct ConnectContext
	{
		// Token: 0x06009A4E RID: 39502 RVA: 0x0038A0A8 File Offset: 0x003882A8
		public ConnectContext(ConduitFlow outer)
		{
			this.outer = outer;
			this.cells = ListPool<int, ConduitFlow>.Allocate();
			this.cells.Capacity = Mathf.Max(this.cells.Capacity, outer.soaInfo.NumEntries);
		}

		// Token: 0x06009A4F RID: 39503 RVA: 0x0038A0E2 File Offset: 0x003882E2
		public void Finish()
		{
			this.cells.Recycle();
		}

		// Token: 0x040076C8 RID: 30408
		public ListPool<int, ConduitFlow>.PooledList cells;

		// Token: 0x040076C9 RID: 30409
		public ConduitFlow outer;
	}

	// Token: 0x020017B9 RID: 6073
	private struct ConnectTask : IWorkItem<ConduitFlow.ConnectContext>
	{
		// Token: 0x06009A50 RID: 39504 RVA: 0x0038A0EF File Offset: 0x003882EF
		public ConnectTask(int start, int end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x06009A51 RID: 39505 RVA: 0x0038A100 File Offset: 0x00388300
		public void Run(ConduitFlow.ConnectContext context, int threadIndex)
		{
			for (int num = this.start; num != this.end; num++)
			{
				int num2 = context.cells[num];
				int conduitIdx = context.outer.grid[num2].conduitIdx;
				if (conduitIdx != -1)
				{
					UtilityConnections connections = context.outer.networkMgr.GetConnections(num2, true);
					if (connections != (UtilityConnections)0)
					{
						ConduitFlow.ConduitConnections @default = ConduitFlow.ConduitConnections.DEFAULT;
						int num3 = Grid.CellLeft(num2);
						if (Grid.IsValidCell(num3) && (connections & UtilityConnections.Left) != (UtilityConnections)0)
						{
							@default.left = context.outer.grid[num3].conduitIdx;
						}
						num3 = Grid.CellRight(num2);
						if (Grid.IsValidCell(num3) && (connections & UtilityConnections.Right) != (UtilityConnections)0)
						{
							@default.right = context.outer.grid[num3].conduitIdx;
						}
						num3 = Grid.CellBelow(num2);
						if (Grid.IsValidCell(num3) && (connections & UtilityConnections.Down) != (UtilityConnections)0)
						{
							@default.down = context.outer.grid[num3].conduitIdx;
						}
						num3 = Grid.CellAbove(num2);
						if (Grid.IsValidCell(num3) && (connections & UtilityConnections.Up) != (UtilityConnections)0)
						{
							@default.up = context.outer.grid[num3].conduitIdx;
						}
						context.outer.soaInfo.SetConduitConnections(conduitIdx, @default);
					}
				}
			}
		}

		// Token: 0x040076CA RID: 30410
		private int start;

		// Token: 0x040076CB RID: 30411
		private int end;
	}

	// Token: 0x020017BA RID: 6074
	private struct UpdateNetworkTask : IWorkItem<ConduitFlow>
	{
		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06009A52 RID: 39506 RVA: 0x0038A257 File Offset: 0x00388457
		// (set) Token: 0x06009A53 RID: 39507 RVA: 0x0038A25F File Offset: 0x0038845F
		public bool continue_updating { readonly get; private set; }

		// Token: 0x06009A54 RID: 39508 RVA: 0x0038A268 File Offset: 0x00388468
		public UpdateNetworkTask(ConduitFlow.Network network)
		{
			this.continue_updating = true;
			this.network = network;
		}

		// Token: 0x06009A55 RID: 39509 RVA: 0x0038A278 File Offset: 0x00388478
		public void Run(ConduitFlow conduit_flow, int threadIndex)
		{
			global::Debug.Assert(this.continue_updating);
			this.continue_updating = false;
			foreach (int num in this.network.cells)
			{
				int conduitIdx = conduit_flow.grid[num].conduitIdx;
				if (conduit_flow.UpdateConduit(conduit_flow.soaInfo.GetConduit(conduitIdx)))
				{
					this.continue_updating = true;
				}
			}
		}

		// Token: 0x06009A56 RID: 39510 RVA: 0x0038A308 File Offset: 0x00388508
		public void Finish(ConduitFlow conduit_flow)
		{
			foreach (int num in this.network.cells)
			{
				ConduitFlow.ConduitContents contents = conduit_flow.grid[num].contents;
				contents.ConsolidateMass();
				conduit_flow.grid[num].contents = contents;
			}
		}

		// Token: 0x040076CC RID: 30412
		private ConduitFlow.Network network;
	}
}
