using System;

namespace Database
{
	// Token: 0x02000EF1 RID: 3825
	public class Expressions : ResourceSet<Expression>
	{
		// Token: 0x0600798D RID: 31117 RVA: 0x002FBD14 File Offset: 0x002F9F14
		public Expressions(ResourceSet parent)
			: base("Expressions", parent)
		{
			Faces faces = Db.Get().Faces;
			this.Angry = new Expression("Angry", this, faces.Angry);
			this.Suffocate = new Expression("Suffocate", this, faces.Suffocate);
			this.RecoverBreath = new Expression("RecoverBreath", this, faces.Uncomfortable);
			this.RedAlert = new Expression("RedAlert", this, faces.Hot);
			this.Hungry = new Expression("Hungry", this, faces.Hungry);
			this.Radiation1 = new Expression("Radiation1", this, faces.Radiation1);
			this.Radiation2 = new Expression("Radiation2", this, faces.Radiation2);
			this.Radiation3 = new Expression("Radiation3", this, faces.Radiation3);
			this.Radiation4 = new Expression("Radiation4", this, faces.Radiation4);
			this.SickSpores = new Expression("SickSpores", this, faces.SickSpores);
			this.Zombie = new Expression("Zombie", this, faces.Zombie);
			this.SickFierySkin = new Expression("SickFierySkin", this, faces.SickFierySkin);
			this.SickCold = new Expression("SickCold", this, faces.SickCold);
			this.Pollen = new Expression("Pollen", this, faces.Pollen);
			this.Sick = new Expression("Sick", this, faces.Sick);
			this.Cold = new Expression("Cold", this, faces.Cold);
			this.Hot = new Expression("Hot", this, faces.Hot);
			this.FullBladder = new Expression("FullBladder", this, faces.Uncomfortable);
			this.Tired = new Expression("Tired", this, faces.Tired);
			this.Unhappy = new Expression("Unhappy", this, faces.Uncomfortable);
			this.Uncomfortable = new Expression("Uncomfortable", this, faces.Uncomfortable);
			this.Productive = new Expression("Productive", this, faces.Productive);
			this.Determined = new Expression("Determined", this, faces.Determined);
			this.Sticker = new Expression("Sticker", this, faces.Sticker);
			this.Balloon = new Expression("Sticker", this, faces.Balloon);
			this.Sparkle = new Expression("Sticker", this, faces.Sparkle);
			this.Music = new Expression("Music", this, faces.Music);
			this.Tickled = new Expression("Tickled", this, faces.Tickled);
			this.BionicJoy = new Expression("Robodancer", this, faces.Robodancer);
			this.Happy = new Expression("Happy", this, faces.Happy);
			this.Relief = new Expression("Relief", this, faces.Happy);
			this.Neutral = new Expression("Neutral", this, faces.Neutral);
			for (int i = this.Count - 1; i >= 0; i--)
			{
				this.resources[i].priority = 100 * (this.Count - i);
			}
		}

		// Token: 0x04005786 RID: 22406
		public Expression Neutral;

		// Token: 0x04005787 RID: 22407
		public Expression Happy;

		// Token: 0x04005788 RID: 22408
		public Expression Uncomfortable;

		// Token: 0x04005789 RID: 22409
		public Expression Cold;

		// Token: 0x0400578A RID: 22410
		public Expression Hot;

		// Token: 0x0400578B RID: 22411
		public Expression FullBladder;

		// Token: 0x0400578C RID: 22412
		public Expression Tired;

		// Token: 0x0400578D RID: 22413
		public Expression Hungry;

		// Token: 0x0400578E RID: 22414
		public Expression Angry;

		// Token: 0x0400578F RID: 22415
		public Expression Unhappy;

		// Token: 0x04005790 RID: 22416
		public Expression RedAlert;

		// Token: 0x04005791 RID: 22417
		public Expression Suffocate;

		// Token: 0x04005792 RID: 22418
		public Expression RecoverBreath;

		// Token: 0x04005793 RID: 22419
		public Expression Sick;

		// Token: 0x04005794 RID: 22420
		public Expression SickSpores;

		// Token: 0x04005795 RID: 22421
		public Expression Zombie;

		// Token: 0x04005796 RID: 22422
		public Expression SickFierySkin;

		// Token: 0x04005797 RID: 22423
		public Expression SickCold;

		// Token: 0x04005798 RID: 22424
		public Expression Pollen;

		// Token: 0x04005799 RID: 22425
		public Expression Relief;

		// Token: 0x0400579A RID: 22426
		public Expression Productive;

		// Token: 0x0400579B RID: 22427
		public Expression Determined;

		// Token: 0x0400579C RID: 22428
		public Expression Sticker;

		// Token: 0x0400579D RID: 22429
		public Expression Balloon;

		// Token: 0x0400579E RID: 22430
		public Expression Sparkle;

		// Token: 0x0400579F RID: 22431
		public Expression Music;

		// Token: 0x040057A0 RID: 22432
		public Expression Tickled;

		// Token: 0x040057A1 RID: 22433
		public Expression Radiation1;

		// Token: 0x040057A2 RID: 22434
		public Expression Radiation2;

		// Token: 0x040057A3 RID: 22435
		public Expression Radiation3;

		// Token: 0x040057A4 RID: 22436
		public Expression Radiation4;

		// Token: 0x040057A5 RID: 22437
		public Expression BionicJoy;
	}
}
