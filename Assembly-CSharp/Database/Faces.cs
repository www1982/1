using System;

namespace Database
{
	// Token: 0x02000EF2 RID: 3826
	public class Faces : ResourceSet<Face>
	{
		// Token: 0x0600798E RID: 31118 RVA: 0x002FC048 File Offset: 0x002FA248
		public Faces()
		{
			this.Neutral = base.Add(new Face("Neutral", null));
			this.Happy = base.Add(new Face("Happy", null));
			this.Uncomfortable = base.Add(new Face("Uncomfortable", null));
			this.Cold = base.Add(new Face("Cold", null));
			this.Hot = base.Add(new Face("Hot", "headfx_sweat"));
			this.Tired = base.Add(new Face("Tired", null));
			this.Sleep = base.Add(new Face("Sleep", null));
			this.Hungry = base.Add(new Face("Hungry", null));
			this.Angry = base.Add(new Face("Angry", null));
			this.Suffocate = base.Add(new Face("Suffocate", null));
			this.Sick = base.Add(new Face("Sick", "headfx_sick"));
			this.SickSpores = base.Add(new Face("Spores", "headfx_spores"));
			this.Zombie = base.Add(new Face("Zombie", null));
			this.SickFierySkin = base.Add(new Face("Fiery", "headfx_fiery"));
			this.SickCold = base.Add(new Face("SickCold", "headfx_sickcold"));
			this.Pollen = base.Add(new Face("Pollen", "headfx_pollen"));
			this.Dead = base.Add(new Face("Death", null));
			this.Productive = base.Add(new Face("Productive", null));
			this.Determined = base.Add(new Face("Determined", null));
			this.Sticker = base.Add(new Face("Sticker", null));
			this.Sparkle = base.Add(new Face("Sparkle", null));
			this.Balloon = base.Add(new Face("Balloon", null));
			this.Tickled = base.Add(new Face("Tickled", null));
			this.Music = base.Add(new Face("Music", null));
			this.Radiation1 = base.Add(new Face("Radiation1", "headfx_radiation1"));
			this.Radiation2 = base.Add(new Face("Radiation2", "headfx_radiation2"));
			this.Radiation3 = base.Add(new Face("Radiation3", "headfx_radiation3"));
			this.Radiation4 = base.Add(new Face("Radiation4", "headfx_radiation4"));
			this.Robodancer = base.Add(new Face("robotdance", null));
		}

		// Token: 0x040057A6 RID: 22438
		public Face Neutral;

		// Token: 0x040057A7 RID: 22439
		public Face Happy;

		// Token: 0x040057A8 RID: 22440
		public Face Uncomfortable;

		// Token: 0x040057A9 RID: 22441
		public Face Cold;

		// Token: 0x040057AA RID: 22442
		public Face Hot;

		// Token: 0x040057AB RID: 22443
		public Face Tired;

		// Token: 0x040057AC RID: 22444
		public Face Sleep;

		// Token: 0x040057AD RID: 22445
		public Face Hungry;

		// Token: 0x040057AE RID: 22446
		public Face Angry;

		// Token: 0x040057AF RID: 22447
		public Face Suffocate;

		// Token: 0x040057B0 RID: 22448
		public Face Dead;

		// Token: 0x040057B1 RID: 22449
		public Face Sick;

		// Token: 0x040057B2 RID: 22450
		public Face SickSpores;

		// Token: 0x040057B3 RID: 22451
		public Face Zombie;

		// Token: 0x040057B4 RID: 22452
		public Face SickFierySkin;

		// Token: 0x040057B5 RID: 22453
		public Face SickCold;

		// Token: 0x040057B6 RID: 22454
		public Face Pollen;

		// Token: 0x040057B7 RID: 22455
		public Face Productive;

		// Token: 0x040057B8 RID: 22456
		public Face Determined;

		// Token: 0x040057B9 RID: 22457
		public Face Sticker;

		// Token: 0x040057BA RID: 22458
		public Face Balloon;

		// Token: 0x040057BB RID: 22459
		public Face Sparkle;

		// Token: 0x040057BC RID: 22460
		public Face Tickled;

		// Token: 0x040057BD RID: 22461
		public Face Music;

		// Token: 0x040057BE RID: 22462
		public Face Radiation1;

		// Token: 0x040057BF RID: 22463
		public Face Radiation2;

		// Token: 0x040057C0 RID: 22464
		public Face Radiation3;

		// Token: 0x040057C1 RID: 22465
		public Face Radiation4;

		// Token: 0x040057C2 RID: 22466
		public Face Robodancer;
	}
}
