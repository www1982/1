using System;

namespace Database
{
	// Token: 0x02000ECF RID: 3791
	public class AccessorySlots : ResourceSet<AccessorySlot>
	{
		// Token: 0x0600791B RID: 31003 RVA: 0x002EC434 File Offset: 0x002EA634
		public AccessorySlots(ResourceSet parent)
			: base("AccessorySlots", parent)
		{
			parent = Db.Get().Accessories;
			KAnimFile anim = Assets.GetAnim("head_swap_kanim");
			KAnimFile anim2 = Assets.GetAnim("body_comp_default_kanim");
			KAnimFile anim3 = Assets.GetAnim("body_swap_kanim");
			KAnimFile anim4 = Assets.GetAnim("hair_swap_kanim");
			KAnimFile anim5 = Assets.GetAnim("hat_swap_kanim");
			this.Eyes = new AccessorySlot("Eyes", this, anim, 0);
			this.Hair = new AccessorySlot("Hair", this, anim4, 0);
			this.HeadShape = new AccessorySlot("HeadShape", this, anim, 0);
			this.Mouth = new AccessorySlot("Mouth", this, anim, 0);
			this.Hat = new AccessorySlot("Hat", this, anim5, 4);
			this.HatHair = new AccessorySlot("Hat_Hair", this, anim4, 0);
			this.HeadEffects = new AccessorySlot("HeadFX", this, anim, 0);
			this.Body = new AccessorySlot("Torso", this, new KAnimHashedString("torso"), anim3, null, 0);
			this.Arm = new AccessorySlot("Arm_Sleeve", this, new KAnimHashedString("arm_sleeve"), anim3, null, 0);
			this.ArmLower = new AccessorySlot("Arm_Lower_Sleeve", this, new KAnimHashedString("arm_lower_sleeve"), anim3, null, 0);
			this.Belt = new AccessorySlot("Belt", this, new KAnimHashedString("belt"), anim2, null, 0);
			this.Neck = new AccessorySlot("Neck", this, new KAnimHashedString("neck"), anim2, null, 0);
			this.Pelvis = new AccessorySlot("Pelvis", this, new KAnimHashedString("pelvis"), anim2, null, 0);
			this.Foot = new AccessorySlot("Foot", this, new KAnimHashedString("foot"), anim2, Assets.GetAnim("shoes_basic_black_kanim"), 0);
			this.Leg = new AccessorySlot("Leg", this, new KAnimHashedString("leg"), anim2, null, 0);
			this.Necklace = new AccessorySlot("Necklace", this, new KAnimHashedString("necklace"), anim2, null, 0);
			this.Cuff = new AccessorySlot("Cuff", this, new KAnimHashedString("cuff"), anim2, null, 0);
			this.Hand = new AccessorySlot("Hand", this, new KAnimHashedString("hand_paint"), anim2, null, 0);
			this.Skirt = new AccessorySlot("Skirt", this, new KAnimHashedString("skirt"), anim3, null, 0);
			this.ArmLowerSkin = new AccessorySlot("Arm_Lower", this, new KAnimHashedString("arm_lower"), anim3, null, 0);
			this.ArmUpperSkin = new AccessorySlot("Arm_Upper", this, new KAnimHashedString("arm_upper"), anim3, null, 0);
			this.LegSkin = new AccessorySlot("Leg_Skin", this, new KAnimHashedString("leg_skin"), anim3, null, 0);
			foreach (AccessorySlot accessorySlot in this.resources)
			{
				accessorySlot.AddAccessories(accessorySlot.AnimFile, parent);
			}
			Db.Get().Accessories.AddCustomAccessories(Assets.GetAnim("body_lonelyminion_kanim"), parent, this);
		}

		// Token: 0x0600791C RID: 31004 RVA: 0x002EC76C File Offset: 0x002EA96C
		public AccessorySlot Find(KAnimHashedString symbol_name)
		{
			foreach (AccessorySlot accessorySlot in Db.Get().AccessorySlots.resources)
			{
				if (symbol_name == accessorySlot.targetSymbolId)
				{
					return accessorySlot;
				}
			}
			return null;
		}

		// Token: 0x040053E1 RID: 21473
		public AccessorySlot Eyes;

		// Token: 0x040053E2 RID: 21474
		public AccessorySlot Hair;

		// Token: 0x040053E3 RID: 21475
		public AccessorySlot HeadShape;

		// Token: 0x040053E4 RID: 21476
		public AccessorySlot Mouth;

		// Token: 0x040053E5 RID: 21477
		public AccessorySlot Body;

		// Token: 0x040053E6 RID: 21478
		public AccessorySlot Arm;

		// Token: 0x040053E7 RID: 21479
		public AccessorySlot ArmLower;

		// Token: 0x040053E8 RID: 21480
		public AccessorySlot Hat;

		// Token: 0x040053E9 RID: 21481
		public AccessorySlot HatHair;

		// Token: 0x040053EA RID: 21482
		public AccessorySlot HeadEffects;

		// Token: 0x040053EB RID: 21483
		public AccessorySlot Belt;

		// Token: 0x040053EC RID: 21484
		public AccessorySlot Neck;

		// Token: 0x040053ED RID: 21485
		public AccessorySlot Pelvis;

		// Token: 0x040053EE RID: 21486
		public AccessorySlot Leg;

		// Token: 0x040053EF RID: 21487
		public AccessorySlot Foot;

		// Token: 0x040053F0 RID: 21488
		public AccessorySlot Skirt;

		// Token: 0x040053F1 RID: 21489
		public AccessorySlot Necklace;

		// Token: 0x040053F2 RID: 21490
		public AccessorySlot Cuff;

		// Token: 0x040053F3 RID: 21491
		public AccessorySlot Hand;

		// Token: 0x040053F4 RID: 21492
		public AccessorySlot ArmLowerSkin;

		// Token: 0x040053F5 RID: 21493
		public AccessorySlot ArmUpperSkin;

		// Token: 0x040053F6 RID: 21494
		public AccessorySlot LegSkin;
	}
}
