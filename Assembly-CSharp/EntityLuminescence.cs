using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020008EE RID: 2286
public class EntityLuminescence : GameStateMachine<EntityLuminescence, EntityLuminescence.Instance, IStateMachineTarget, EntityLuminescence.Def>
{
	// Token: 0x06003FD9 RID: 16345 RVA: 0x0016680C File Offset: 0x00164A0C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.root;
	}

	// Token: 0x02001899 RID: 6297
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007964 RID: 31076
		public Color lightColor;

		// Token: 0x04007965 RID: 31077
		public float lightRange;

		// Token: 0x04007966 RID: 31078
		public float lightAngle;

		// Token: 0x04007967 RID: 31079
		public Vector2 lightOffset;

		// Token: 0x04007968 RID: 31080
		public Vector2 lightDirection;

		// Token: 0x04007969 RID: 31081
		public global::LightShape lightShape;
	}

	// Token: 0x0200189A RID: 6298
	public new class Instance : GameStateMachine<EntityLuminescence, EntityLuminescence.Instance, IStateMachineTarget, EntityLuminescence.Def>.GameInstance
	{
		// Token: 0x06009D27 RID: 40231 RVA: 0x00392354 File Offset: 0x00390554
		public Instance(IStateMachineTarget master, EntityLuminescence.Def def)
			: base(master, def)
		{
			this.light.Color = def.lightColor;
			this.light.Range = def.lightRange;
			this.light.Angle = def.lightAngle;
			this.light.Direction = def.lightDirection;
			this.light.Offset = def.lightOffset;
			this.light.shape = def.lightShape;
		}

		// Token: 0x06009D28 RID: 40232 RVA: 0x003923D0 File Offset: 0x003905D0
		public override void StartSM()
		{
			base.StartSM();
			this.luminescence = Db.Get().Attributes.Luminescence.Lookup(base.gameObject);
			AttributeInstance attributeInstance = this.luminescence;
			attributeInstance.OnDirty = (global::System.Action)Delegate.Combine(attributeInstance.OnDirty, new global::System.Action(this.OnLuminescenceChanged));
			this.RefreshLight();
		}

		// Token: 0x06009D29 RID: 40233 RVA: 0x00392430 File Offset: 0x00390630
		private void OnLuminescenceChanged()
		{
			this.RefreshLight();
		}

		// Token: 0x06009D2A RID: 40234 RVA: 0x00392438 File Offset: 0x00390638
		public void RefreshLight()
		{
			if (this.luminescence != null)
			{
				int num = (int)this.luminescence.GetTotalValue();
				this.light.Lux = num;
				bool flag = num > 0;
				if (this.light.enabled != flag)
				{
					this.light.enabled = flag;
				}
			}
		}

		// Token: 0x06009D2B RID: 40235 RVA: 0x00392485 File Offset: 0x00390685
		protected override void OnCleanUp()
		{
			if (this.luminescence != null)
			{
				AttributeInstance attributeInstance = this.luminescence;
				attributeInstance.OnDirty = (global::System.Action)Delegate.Remove(attributeInstance.OnDirty, new global::System.Action(this.OnLuminescenceChanged));
			}
			base.OnCleanUp();
		}

		// Token: 0x0400796A RID: 31082
		[MyCmpAdd]
		private Light2D light;

		// Token: 0x0400796B RID: 31083
		private AttributeInstance luminescence;
	}
}
