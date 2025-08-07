using System;
using UnityEngine;

// Token: 0x02000DD5 RID: 3541
public class AutoPlumberSideScreen : SideScreenContent
{
	// Token: 0x06006FC2 RID: 28610 RVA: 0x002A8C60 File Offset: 0x002A6E60
	protected override void OnSpawn()
	{
		this.activateButton.onClick += delegate
		{
			DevAutoPlumber.AutoPlumbBuilding(this.building);
		};
		this.powerButton.onClick += delegate
		{
			DevAutoPlumber.DoElectricalPlumbing(this.building);
		};
		this.pipesButton.onClick += delegate
		{
			DevAutoPlumber.DoLiquidAndGasPlumbing(this.building);
		};
		this.solidsButton.onClick += delegate
		{
			DevAutoPlumber.SetupSolidOreDelivery(this.building);
		};
		this.minionButton.onClick += delegate
		{
			this.SpawnMinion();
		};
	}

	// Token: 0x06006FC3 RID: 28611 RVA: 0x002A8CE0 File Offset: 0x002A6EE0
	private void SpawnMinion()
	{
		MinionStartingStats minionStartingStats = new MinionStartingStats(false, null, null, true);
		GameObject prefab = Assets.GetPrefab(BaseMinionConfig.GetMinionIDForModel(minionStartingStats.personality.model));
		GameObject gameObject = Util.KInstantiate(prefab, null, null);
		gameObject.name = prefab.name;
		Immigration.Instance.ApplyDefaultPersonalPriorities(gameObject);
		Vector3 vector = Grid.CellToPos(Grid.PosToCell(this.building), CellAlignment.Bottom, Grid.SceneLayer.Move);
		gameObject.transform.SetLocalPosition(vector);
		gameObject.SetActive(true);
		minionStartingStats.Apply(gameObject);
	}

	// Token: 0x06006FC4 RID: 28612 RVA: 0x002A8D5F File Offset: 0x002A6F5F
	public override int GetSideScreenSortOrder()
	{
		return -150;
	}

	// Token: 0x06006FC5 RID: 28613 RVA: 0x002A8D66 File Offset: 0x002A6F66
	public override bool IsValidForTarget(GameObject target)
	{
		return DebugHandler.InstantBuildMode && target.GetComponent<Building>() != null;
	}

	// Token: 0x06006FC6 RID: 28614 RVA: 0x002A8D7D File Offset: 0x002A6F7D
	public override void SetTarget(GameObject target)
	{
		this.building = target.GetComponent<Building>();
	}

	// Token: 0x06006FC7 RID: 28615 RVA: 0x002A8D8B File Offset: 0x002A6F8B
	public override void ClearTarget()
	{
	}

	// Token: 0x04004CDF RID: 19679
	public KButton activateButton;

	// Token: 0x04004CE0 RID: 19680
	public KButton powerButton;

	// Token: 0x04004CE1 RID: 19681
	public KButton pipesButton;

	// Token: 0x04004CE2 RID: 19682
	public KButton solidsButton;

	// Token: 0x04004CE3 RID: 19683
	public KButton minionButton;

	// Token: 0x04004CE4 RID: 19684
	private Building building;
}
