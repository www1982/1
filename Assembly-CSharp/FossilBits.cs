using System;
using KSerialization;
using STRINGS;

// Token: 0x0200020C RID: 524
public class FossilBits : FossilExcavationWorkable, ISidescreenButtonControl
{
	// Token: 0x06000A76 RID: 2678 RVA: 0x0003F6FA File Offset: 0x0003D8FA
	protected override bool IsMarkedForExcavation()
	{
		return this.MarkedForDig;
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0003F702 File Offset: 0x0003D902
	public void SetEntombStatusItemVisibility(bool visible)
	{
		this.entombComponent.SetShowStatusItemOnEntombed(visible);
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0003F710 File Offset: 0x0003D910
	public void CreateWorkableChore()
	{
		if (this.chore == null && this.operational.IsOperational)
		{
			this.chore = new WorkChore<FossilBits>(Db.Get().ChoreTypes.ExcavateFossil, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0003F75E File Offset: 0x0003D95E
	public void CancelWorkChore()
	{
		if (this.chore != null)
		{
			this.chore.Cancel("FossilBits.CancelChore");
			this.chore = null;
		}
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0003F780 File Offset: 0x0003D980
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_sculpture_kanim") };
		base.Subscribe(-592767678, new Action<object>(this.OnOperationalChanged));
		base.SetWorkTime(30f);
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0003F7D4 File Offset: 0x0003D9D4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.SetEntombStatusItemVisibility(this.MarkedForDig);
		base.SetShouldShowSkillPerkStatusItem(this.IsMarkedForExcavation());
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
	private void OnOperationalChanged(object state)
	{
		if ((bool)state)
		{
			if (this.MarkedForDig)
			{
				this.CreateWorkableChore();
				return;
			}
		}
		else if (this.MarkedForDig)
		{
			this.CancelWorkChore();
		}
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0003F81C File Offset: 0x0003DA1C
	private void DropLoot()
	{
		PrimaryElement component = base.gameObject.GetComponent<PrimaryElement>();
		int num = Grid.PosToCell(base.transform.GetPosition());
		Element element = ElementLoader.GetElement(component.Element.tag);
		if (element != null)
		{
			float num2 = component.Mass;
			int num3 = 0;
			while ((float)num3 < component.Mass / 400f)
			{
				float num4 = num2;
				if (num2 > 400f)
				{
					num4 = 400f;
					num2 -= 400f;
				}
				int num5 = (int)((float)component.DiseaseCount * (num4 / component.Mass));
				element.substance.SpawnResource(Grid.CellToPosCBC(num, Grid.SceneLayer.Ore), num4, component.Temperature, component.DiseaseIdx, num5, false, false, false);
				num3++;
			}
		}
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0003F8D2 File Offset: 0x0003DAD2
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		this.DropLoot();
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0003F8EC File Offset: 0x0003DAEC
	public int HorizontalGroupID()
	{
		return -1;
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0003F8EF File Offset: 0x0003DAEF
	public string SidescreenButtonText
	{
		get
		{
			if (!this.MarkedForDig)
			{
				return CODEX.STORY_TRAITS.FOSSILHUNT.UISIDESCREENS.FOSSIL_BITS_EXCAVATE_BUTTON;
			}
			return CODEX.STORY_TRAITS.FOSSILHUNT.UISIDESCREENS.FOSSIL_BITS_CANCEL_EXCAVATION_BUTTON;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0003F90E File Offset: 0x0003DB0E
	public string SidescreenButtonTooltip
	{
		get
		{
			if (!this.MarkedForDig)
			{
				return CODEX.STORY_TRAITS.FOSSILHUNT.UISIDESCREENS.FOSSIL_BITS_EXCAVATE_BUTTON_TOOLTIP;
			}
			return CODEX.STORY_TRAITS.FOSSILHUNT.UISIDESCREENS.FOSSIL_BITS_CANCEL_EXCAVATION_BUTTON_TOOLTIP;
		}
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0003F92D File Offset: 0x0003DB2D
	public void SetButtonTextOverride(ButtonMenuTextOverride textOverride)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0003F934 File Offset: 0x0003DB34
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0003F937 File Offset: 0x0003DB37
	public bool SidescreenButtonInteractable()
	{
		return true;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0003F93C File Offset: 0x0003DB3C
	public void OnSidescreenButtonPressed()
	{
		this.MarkedForDig = !this.MarkedForDig;
		base.SetShouldShowSkillPerkStatusItem(this.MarkedForDig);
		this.SetEntombStatusItemVisibility(this.MarkedForDig);
		if (this.MarkedForDig)
		{
			this.CreateWorkableChore();
		}
		else
		{
			this.CancelWorkChore();
		}
		this.UpdateStatusItem(null);
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0003F98D File Offset: 0x0003DB8D
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x04000748 RID: 1864
	[Serialize]
	public bool MarkedForDig;

	// Token: 0x04000749 RID: 1865
	private Chore chore;

	// Token: 0x0400074A RID: 1866
	[MyCmpGet]
	private EntombVulnerable entombComponent;

	// Token: 0x0400074B RID: 1867
	[MyCmpGet]
	private Operational operational;
}
