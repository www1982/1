using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x0200084C RID: 2124
[SerializationConfig(MemberSerialization.OptIn)]
public class RoverModifiers : Modifiers, ISaveLoadable
{
	// Token: 0x06003A5A RID: 14938 RVA: 0x00144D64 File Offset: 0x00142F64
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributes.Add(Db.Get().Attributes.Construction);
		this.attributes.Add(Db.Get().Attributes.Digging);
		this.attributes.Add(Db.Get().Attributes.Strength);
	}

	// Token: 0x06003A5B RID: 14939 RVA: 0x00144DC8 File Offset: 0x00142FC8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (base.GetComponent<ChoreConsumer>() != null)
		{
			base.Subscribe<RoverModifiers>(-1988963660, RoverModifiers.OnBeginChoreDelegate);
			Vector3 position = base.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			base.transform.SetPosition(position);
			base.gameObject.layer = LayerMask.NameToLayer("Default");
			this.SetupDependentAttribute(Db.Get().Attributes.CarryAmount, Db.Get().AttributeConverters.CarryAmountFromStrength);
		}
	}

	// Token: 0x06003A5C RID: 14940 RVA: 0x00144E5C File Offset: 0x0014305C
	private void SetupDependentAttribute(Klei.AI.Attribute targetAttribute, AttributeConverter attributeConverter)
	{
		Klei.AI.Attribute attribute = attributeConverter.attribute;
		AttributeInstance attributeInstance = attribute.Lookup(this);
		AttributeModifier target_modifier = new AttributeModifier(targetAttribute.Id, attributeConverter.Lookup(this).Evaluate(), attribute.Name, false, false, false);
		this.GetAttributes().Add(target_modifier);
		attributeInstance.OnDirty = (global::System.Action)Delegate.Combine(attributeInstance.OnDirty, new global::System.Action(delegate
		{
			target_modifier.SetValue(attributeConverter.Lookup(this).Evaluate());
		}));
	}

	// Token: 0x06003A5D RID: 14941 RVA: 0x00144EF0 File Offset: 0x001430F0
	private void OnBeginChore(object data)
	{
		Storage component = base.GetComponent<Storage>();
		if (component != null)
		{
			component.DropAll(false, false, default(Vector3), true, null);
		}
	}

	// Token: 0x040023CD RID: 9165
	private static readonly EventSystem.IntraObjectHandler<RoverModifiers> OnBeginChoreDelegate = new EventSystem.IntraObjectHandler<RoverModifiers>(delegate(RoverModifiers component, object data)
	{
		component.OnBeginChore(data);
	});
}
