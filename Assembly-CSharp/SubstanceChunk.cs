using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000BAC RID: 2988
[SkipSaveFileSerialization]
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/SubstanceChunk")]
public class SubstanceChunk : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x0600595B RID: 22875 RVA: 0x00204A98 File Offset: 0x00202C98
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Color color = base.GetComponent<PrimaryElement>().Element.substance.colour;
		color.a = 1f;
		base.GetComponent<KBatchedAnimController>().SetSymbolTint(SubstanceChunk.symbolToTint, color);
		base.GetComponent<KBatchedAnimController>().SetSymbolTint(SubstanceChunk.symbolToTint2, color);
	}

	// Token: 0x0600595C RID: 22876 RVA: 0x00204AF4 File Offset: 0x00202CF4
	private void OnRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_deconstruct", UI.USERMENUACTIONS.RELEASEELEMENT.NAME, new global::System.Action(this.OnRelease), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.RELEASEELEMENT.TOOLTIP, true), 1f);
	}

	// Token: 0x0600595D RID: 22877 RVA: 0x00204B50 File Offset: 0x00202D50
	private void OnRelease()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		if (component.Mass > 0f)
		{
			SimMessages.AddRemoveSubstance(num, component.ElementID, CellEventLogger.Instance.ExhaustSimUpdate, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount, true, -1);
		}
		base.gameObject.DeleteObject();
	}

	// Token: 0x04003B53 RID: 15187
	private static readonly KAnimHashedString symbolToTint = new KAnimHashedString("substance_tinter");

	// Token: 0x04003B54 RID: 15188
	private static readonly KAnimHashedString symbolToTint2 = new KAnimHashedString("substance_tinter_cap");
}
