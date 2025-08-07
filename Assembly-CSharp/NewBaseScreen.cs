using System;
using FMODUnity;
using ProcGenGame;
using UnityEngine;

// Token: 0x02000C2A RID: 3114
public class NewBaseScreen : KScreen
{
	// Token: 0x06005E9F RID: 24223 RVA: 0x0022B2AE File Offset: 0x002294AE
	public override float GetSortKey()
	{
		return 1f;
	}

	// Token: 0x06005EA0 RID: 24224 RVA: 0x0022B2B5 File Offset: 0x002294B5
	protected override void OnPrefabInit()
	{
		NewBaseScreen.Instance = this;
		base.OnPrefabInit();
		TimeOfDay.Instance.SetScale(0f);
	}

	// Token: 0x06005EA1 RID: 24225 RVA: 0x0022B2D2 File Offset: 0x002294D2
	protected override void OnForcedCleanUp()
	{
		NewBaseScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06005EA2 RID: 24226 RVA: 0x0022B2E0 File Offset: 0x002294E0
	public static Vector2I SetInitialCamera()
	{
		Vector2I vector2I = SaveLoader.Instance.cachedGSD.baseStartPos;
		vector2I += ClusterManager.Instance.GetStartWorld().WorldOffset;
		Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(Grid.OffsetCell(0, vector2I.x, vector2I.y), 0, -2), Grid.SceneLayer.Background);
		CameraController.Instance.SetMaxOrthographicSize(40f);
		CameraController.Instance.SnapTo(vector);
		CameraController.Instance.SetTargetPos(vector, 20f, false);
		CameraController.Instance.OrthographicSize = 40f;
		CameraSaveData.valid = false;
		return vector2I;
	}

	// Token: 0x06005EA3 RID: 24227 RVA: 0x0022B378 File Offset: 0x00229578
	protected override void OnActivate()
	{
		if (this.disabledUIElements != null)
		{
			foreach (CanvasGroup canvasGroup in this.disabledUIElements)
			{
				if (canvasGroup != null)
				{
					canvasGroup.interactable = false;
				}
			}
		}
		NewBaseScreen.SetInitialCamera();
		if (SpeedControlScreen.Instance.IsPaused)
		{
			SpeedControlScreen.Instance.Unpause(false);
		}
		this.Final();
	}

	// Token: 0x06005EA4 RID: 24228 RVA: 0x0022B3D9 File Offset: 0x002295D9
	public void Init(Cluster clusterLayout, ITelepadDeliverable[] startingMinionStats)
	{
		this.m_clusterLayout = clusterLayout;
		this.m_minionStartingStats = startingMinionStats;
	}

	// Token: 0x06005EA5 RID: 24229 RVA: 0x0022B3EC File Offset: 0x002295EC
	protected override void OnDeactivate()
	{
		Game.Instance.Trigger(-122303817, null);
		if (this.disabledUIElements != null)
		{
			foreach (CanvasGroup canvasGroup in this.disabledUIElements)
			{
				if (canvasGroup != null)
				{
					canvasGroup.interactable = true;
				}
			}
		}
	}

	// Token: 0x06005EA6 RID: 24230 RVA: 0x0022B43C File Offset: 0x0022963C
	public override void OnKeyDown(KButtonEvent e)
	{
		global::Action[] array = new global::Action[]
		{
			global::Action.SpeedUp,
			global::Action.SlowDown,
			global::Action.TogglePause,
			global::Action.CycleSpeed
		};
		if (!e.Consumed)
		{
			int num = 0;
			while (num < array.Length && !e.TryConsume(array[num]))
			{
				num++;
			}
		}
	}

	// Token: 0x06005EA7 RID: 24231 RVA: 0x0022B47C File Offset: 0x0022967C
	private void Final()
	{
		SpeedControlScreen.Instance.Unpause(false);
		GameObject telepad = GameUtil.GetTelepad(ClusterManager.Instance.GetStartWorld().id);
		if (telepad)
		{
			this.SpawnMinions(telepad);
		}
		Game.Instance.baseAlreadyCreated = true;
		this.Deactivate();
	}

	// Token: 0x06005EA8 RID: 24232 RVA: 0x0022B4CC File Offset: 0x002296CC
	private void SpawnMinions(GameObject start_pad)
	{
		int num = Grid.PosToCell(start_pad);
		if (num == -1)
		{
			global::Debug.LogWarning("No headquarters in saved base template. Cannot place minions. Confirm there is a headquarters saved to the base template, or consider creating a new one.");
			return;
		}
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		if (Grid.WidthInCells < 64)
		{
			return;
		}
		int baseLeft = this.m_clusterLayout.currentWorld.BaseLeft;
		int baseRight = this.m_clusterLayout.currentWorld.BaseRight;
		Db.Get().effects.Get("AnewHope");
		Telepad component = start_pad.GetComponent<Telepad>();
		for (int i = 0; i < this.m_minionStartingStats.Length; i++)
		{
			MinionStartingStats minionStartingStats = (MinionStartingStats)this.m_minionStartingStats[i];
			int num4 = num2 + i % (baseRight - baseLeft) + 1;
			int num5 = num3;
			int num6 = Grid.XYToCell(num4, num5);
			GameObject prefab = Assets.GetPrefab(BaseMinionConfig.GetMinionIDForModel(minionStartingStats.personality.model));
			GameObject gameObject = Util.KInstantiate(prefab, null, null);
			gameObject.name = prefab.name;
			Immigration.Instance.ApplyDefaultPersonalPriorities(gameObject);
			gameObject.transform.SetLocalPosition(Grid.CellToPosCBC(num6, Grid.SceneLayer.Move));
			gameObject.SetActive(true);
			minionStartingStats.Apply(gameObject);
			if (component != null)
			{
				component.AddNewBaseMinion(gameObject, minionStartingStats.personality.model == GameTags.Minions.Models.Bionic);
			}
		}
		component.ScheduleNewBaseEvents();
		ClusterManager.Instance.activeWorld.SetDupeVisited();
	}

	// Token: 0x04003EFC RID: 16124
	public static NewBaseScreen Instance;

	// Token: 0x04003EFD RID: 16125
	[SerializeField]
	private CanvasGroup[] disabledUIElements;

	// Token: 0x04003EFE RID: 16126
	public EventReference ScanSoundMigrated;

	// Token: 0x04003EFF RID: 16127
	public EventReference BuildBaseSoundMigrated;

	// Token: 0x04003F00 RID: 16128
	private ITelepadDeliverable[] m_minionStartingStats;

	// Token: 0x04003F01 RID: 16129
	private Cluster m_clusterLayout;
}
