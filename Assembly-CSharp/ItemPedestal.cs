using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200074F RID: 1871
[AddComponentMenu("KMonoBehaviour/scripts/ItemPedestal")]
public class ItemPedestal : KMonoBehaviour
{
	// Token: 0x06002F96 RID: 12182 RVA: 0x0011090C File Offset: 0x0010EB0C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<ItemPedestal>(-731304873, ItemPedestal.OnOccupantChangedDelegate);
		if (this.receptacle.Occupant)
		{
			KBatchedAnimController component = this.receptacle.Occupant.GetComponent<KBatchedAnimController>();
			if (component)
			{
				component.enabled = true;
				component.sceneLayer = Grid.SceneLayer.Move;
			}
			this.OnOccupantChanged(this.receptacle.Occupant);
		}
	}

	// Token: 0x06002F97 RID: 12183 RVA: 0x0011097C File Offset: 0x0010EB7C
	private void OnOccupantChanged(object data)
	{
		Attributes attributes = this.GetAttributes();
		if (this.decorModifier != null)
		{
			attributes.Remove(this.decorModifier);
			attributes.Remove(this.decorRadiusModifier);
			this.decorModifier = null;
			this.decorRadiusModifier = null;
		}
		if (data != null)
		{
			GameObject gameObject = (GameObject)data;
			global::UnityEngine.Object component = gameObject.GetComponent<DecorProvider>();
			float num = 5f;
			float num2 = 3f;
			if (component != null)
			{
				num = Mathf.Max(Db.Get().BuildingAttributes.Decor.Lookup(gameObject).GetTotalValue() * 2f, 5f);
				num2 = Db.Get().BuildingAttributes.DecorRadius.Lookup(gameObject).GetTotalValue() + 2f;
			}
			string text = string.Format(BUILDINGS.PREFABS.ITEMPEDESTAL.DISPLAYED_ITEM_FMT, gameObject.GetComponent<KPrefabID>().PrefabTag.ProperName());
			this.decorModifier = new AttributeModifier(Db.Get().BuildingAttributes.Decor.Id, num, text, false, false, true);
			this.decorRadiusModifier = new AttributeModifier(Db.Get().BuildingAttributes.DecorRadius.Id, num2, text, false, false, true);
			attributes.Add(this.decorModifier);
			attributes.Add(this.decorRadiusModifier);
		}
	}

	// Token: 0x04001C43 RID: 7235
	[MyCmpReq]
	protected SingleEntityReceptacle receptacle;

	// Token: 0x04001C44 RID: 7236
	[MyCmpReq]
	private DecorProvider decorProvider;

	// Token: 0x04001C45 RID: 7237
	private const float MINIMUM_DECOR = 5f;

	// Token: 0x04001C46 RID: 7238
	private const float STORED_DECOR_MODIFIER = 2f;

	// Token: 0x04001C47 RID: 7239
	private const int RADIUS_BONUS = 2;

	// Token: 0x04001C48 RID: 7240
	private AttributeModifier decorModifier;

	// Token: 0x04001C49 RID: 7241
	private AttributeModifier decorRadiusModifier;

	// Token: 0x04001C4A RID: 7242
	private static readonly EventSystem.IntraObjectHandler<ItemPedestal> OnOccupantChangedDelegate = new EventSystem.IntraObjectHandler<ItemPedestal>(delegate(ItemPedestal component, object data)
	{
		component.OnOccupantChanged(data);
	});
}
