using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007C4 RID: 1988
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/SolidConduit")]
public class SolidConduit : KMonoBehaviour, IFirstFrameCallback, IHaveUtilityNetworkMgr
{
	// Token: 0x0600352D RID: 13613 RVA: 0x00129F30 File Offset: 0x00128130
	public void SetFirstFrameCallback(global::System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x0600352E RID: 13614 RVA: 0x00129F46 File Offset: 0x00128146
	private IEnumerator RunCallback()
	{
		yield return null;
		if (this.firstFrameCallback != null)
		{
			this.firstFrameCallback();
			this.firstFrameCallback = null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x0600352F RID: 13615 RVA: 0x00129F55 File Offset: 0x00128155
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.solidConduitSystem;
	}

	// Token: 0x06003530 RID: 13616 RVA: 0x00129F61 File Offset: 0x00128161
	public UtilityNetwork GetNetwork()
	{
		return this.GetNetworkManager().GetNetworkForCell(Grid.PosToCell(this));
	}

	// Token: 0x06003531 RID: 13617 RVA: 0x00129F74 File Offset: 0x00128174
	public static SolidConduitFlow GetFlowManager()
	{
		return Game.Instance.solidConduitFlow;
	}

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x06003532 RID: 13618 RVA: 0x00129F80 File Offset: 0x00128180
	public Vector3 Position
	{
		get
		{
			return base.transform.GetPosition();
		}
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x00129F8D File Offset: 0x0012818D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Conveyor, this);
	}

	// Token: 0x06003534 RID: 13620 RVA: 0x00129FC0 File Offset: 0x001281C0
	protected override void OnCleanUp()
	{
		int num = Grid.PosToCell(this);
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.Def.ReplacementLayer == ObjectLayer.NumLayers || Grid.Objects[num, (int)component.Def.ReplacementLayer] == null)
		{
			this.GetNetworkManager().RemoveFromNetworks(num, this, false);
			SolidConduit.GetFlowManager().EmptyConduit(num);
		}
		base.OnCleanUp();
	}

	// Token: 0x04002016 RID: 8214
	[MyCmpReq]
	private KAnimGraphTileVisualizer graphTileDependency;

	// Token: 0x04002017 RID: 8215
	private global::System.Action firstFrameCallback;
}
