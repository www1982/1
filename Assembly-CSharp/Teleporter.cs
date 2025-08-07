using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020007DD RID: 2013
public class Teleporter : KMonoBehaviour
{
	// Token: 0x17000399 RID: 921
	// (get) Token: 0x0600365D RID: 13917 RVA: 0x0012EB85 File Offset: 0x0012CD85
	// (set) Token: 0x0600365E RID: 13918 RVA: 0x0012EB8D File Offset: 0x0012CD8D
	[Serialize]
	public int teleporterID { get; private set; }

	// Token: 0x0600365F RID: 13919 RVA: 0x0012EB96 File Offset: 0x0012CD96
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06003660 RID: 13920 RVA: 0x0012EB9E File Offset: 0x0012CD9E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Teleporters.Add(this);
		this.SetTeleporterID(0);
		base.Subscribe<Teleporter>(-801688580, Teleporter.OnLogicValueChangedDelegate);
	}

	// Token: 0x06003661 RID: 13921 RVA: 0x0012EBCC File Offset: 0x0012CDCC
	private void OnLogicValueChanged(object data)
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		List<int> list = new List<int>();
		int num = 0;
		int num2 = Mathf.Min(this.ID_LENGTH, component.inputPorts.Count);
		for (int i = 0; i < num2; i++)
		{
			int logicUICell = component.inputPorts[i].GetLogicUICell();
			LogicCircuitNetwork networkForCell = logicCircuitManager.GetNetworkForCell(logicUICell);
			int num3 = ((networkForCell != null) ? networkForCell.OutputValue : 1);
			list.Add(num3);
		}
		foreach (int num4 in list)
		{
			num = (num << 1) | num4;
		}
		this.SetTeleporterID(num);
	}

	// Token: 0x06003662 RID: 13922 RVA: 0x0012EC9C File Offset: 0x0012CE9C
	protected override void OnCleanUp()
	{
		Components.Teleporters.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003663 RID: 13923 RVA: 0x0012ECAF File Offset: 0x0012CEAF
	public bool HasTeleporterTarget()
	{
		return this.FindTeleportTarget() != null;
	}

	// Token: 0x06003664 RID: 13924 RVA: 0x0012ECBD File Offset: 0x0012CEBD
	public bool IsValidTeleportTarget(Teleporter from_tele)
	{
		return from_tele.teleporterID == this.teleporterID && this.operational.IsOperational;
	}

	// Token: 0x06003665 RID: 13925 RVA: 0x0012ECDC File Offset: 0x0012CEDC
	public Teleporter FindTeleportTarget()
	{
		List<Teleporter> list = new List<Teleporter>();
		foreach (object obj in Components.Teleporters)
		{
			Teleporter teleporter = (Teleporter)obj;
			if (teleporter.IsValidTeleportTarget(this) && teleporter != this)
			{
				list.Add(teleporter);
			}
		}
		Teleporter teleporter2 = null;
		if (list.Count > 0)
		{
			teleporter2 = list.GetRandom<Teleporter>();
		}
		return teleporter2;
	}

	// Token: 0x06003666 RID: 13926 RVA: 0x0012ED64 File Offset: 0x0012CF64
	public void SetTeleporterID(int ID)
	{
		this.teleporterID = ID;
		foreach (object obj in Components.Teleporters)
		{
			((Teleporter)obj).Trigger(-1266722732, null);
		}
	}

	// Token: 0x06003667 RID: 13927 RVA: 0x0012EDC8 File Offset: 0x0012CFC8
	public void SetTeleportTarget(Teleporter target)
	{
		this.teleportTarget.Set(target);
	}

	// Token: 0x06003668 RID: 13928 RVA: 0x0012EDD8 File Offset: 0x0012CFD8
	public void TeleportObjects()
	{
		Teleporter teleporter = this.teleportTarget.Get();
		int widthInCells = base.GetComponent<Building>().Def.WidthInCells;
		int num = base.GetComponent<Building>().Def.HeightInCells - 1;
		Vector3 position = base.transform.GetPosition();
		if (teleporter != null)
		{
			ListPool<ScenePartitionerEntry, Teleporter>.PooledList pooledList = ListPool<ScenePartitionerEntry, Teleporter>.Allocate();
			GameScenePartitioner.Instance.GatherEntries((int)position.x - widthInCells / 2 + 1, (int)position.y - num / 2 + 1, widthInCells, num, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
			int num2 = Grid.PosToCell(teleporter);
			foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
			{
				GameObject gameObject = (scenePartitionerEntry.obj as Pickupable).gameObject;
				Vector3 vector = gameObject.transform.GetPosition() - position;
				MinionIdentity component = gameObject.GetComponent<MinionIdentity>();
				if (component != null)
				{
					new EmoteChore(component.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, "anim_interacts_portal_kanim", Telepad.PortalBirthAnim, null);
				}
				else
				{
					vector += Vector3.up;
				}
				gameObject.transform.SetLocalPosition(Grid.CellToPosCBC(num2, Grid.SceneLayer.Move) + vector);
			}
			pooledList.Recycle();
		}
		TeleportalPad.StatesInstance smi = this.teleportTarget.Get().GetSMI<TeleportalPad.StatesInstance>();
		smi.sm.doTeleport.Trigger(smi);
		this.teleportTarget.Set(null);
	}

	// Token: 0x040020D5 RID: 8405
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040020D7 RID: 8407
	[Serialize]
	public Ref<Teleporter> teleportTarget = new Ref<Teleporter>();

	// Token: 0x040020D8 RID: 8408
	public int ID_LENGTH = 4;

	// Token: 0x040020D9 RID: 8409
	private static readonly EventSystem.IntraObjectHandler<Teleporter> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<Teleporter>(delegate(Teleporter component, object data)
	{
		component.OnLogicValueChanged(data);
	});
}
