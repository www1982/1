using System;
using STRINGS;

// Token: 0x02000B39 RID: 2873
public class ClustercraftInteriorDoor : KMonoBehaviour, ISidescreenButtonControl
{
	// Token: 0x17000619 RID: 1561
	// (get) Token: 0x06005535 RID: 21813 RVA: 0x001EF1BA File Offset: 0x001ED3BA
	public string SidescreenButtonText
	{
		get
		{
			return UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWEXTERIOR.LABEL;
		}
	}

	// Token: 0x1700061A RID: 1562
	// (get) Token: 0x06005536 RID: 21814 RVA: 0x001EF1C6 File Offset: 0x001ED3C6
	public string SidescreenButtonTooltip
	{
		get
		{
			return this.SidescreenButtonInteractable() ? UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWEXTERIOR.LABEL : UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWEXTERIOR.INVALID;
		}
	}

	// Token: 0x06005537 RID: 21815 RVA: 0x001EF1E1 File Offset: 0x001ED3E1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.ClusterCraftInteriorDoors.Add(this);
	}

	// Token: 0x06005538 RID: 21816 RVA: 0x001EF1F4 File Offset: 0x001ED3F4
	protected override void OnCleanUp()
	{
		Components.ClusterCraftInteriorDoors.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06005539 RID: 21817 RVA: 0x001EF207 File Offset: 0x001ED407
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x0600553A RID: 21818 RVA: 0x001EF20C File Offset: 0x001ED40C
	public bool SidescreenButtonInteractable()
	{
		WorldContainer myWorld = base.gameObject.GetMyWorld();
		return myWorld.ParentWorldId != 255 && myWorld.ParentWorldId != myWorld.id;
	}

	// Token: 0x0600553B RID: 21819 RVA: 0x001EF245 File Offset: 0x001ED445
	public void OnSidescreenButtonPressed()
	{
		ClusterManager.Instance.SetActiveWorld(base.gameObject.GetMyWorld().ParentWorldId);
	}

	// Token: 0x0600553C RID: 21820 RVA: 0x001EF261 File Offset: 0x001ED461
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x0600553D RID: 21821 RVA: 0x001EF265 File Offset: 0x001ED465
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600553E RID: 21822 RVA: 0x001EF26C File Offset: 0x001ED46C
	public int HorizontalGroupID()
	{
		return -1;
	}
}
