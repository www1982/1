using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FEA RID: 4074
	public class AnimatedSickness : Sickness.SicknessComponent
	{
		// Token: 0x06007DCE RID: 32206 RVA: 0x00325FBC File Offset: 0x003241BC
		public AnimatedSickness(HashedString[] kanim_filenames, Expression expression)
		{
			this.kanims = new KAnimFile[kanim_filenames.Length];
			for (int i = 0; i < kanim_filenames.Length; i++)
			{
				this.kanims[i] = Assets.GetAnim(kanim_filenames[i]);
			}
			this.expression = expression;
		}

		// Token: 0x06007DCF RID: 32207 RVA: 0x00326008 File Offset: 0x00324208
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			for (int i = 0; i < this.kanims.Length; i++)
			{
				go.GetComponent<KAnimControllerBase>().AddAnimOverrides(this.kanims[i], 10f);
			}
			if (this.expression != null)
			{
				go.GetComponent<FaceGraph>().AddExpression(this.expression);
			}
			return null;
		}

		// Token: 0x06007DD0 RID: 32208 RVA: 0x0032605C File Offset: 0x0032425C
		public override void OnCure(GameObject go, object instace_data)
		{
			if (this.expression != null)
			{
				go.GetComponent<FaceGraph>().RemoveExpression(this.expression);
			}
			for (int i = 0; i < this.kanims.Length; i++)
			{
				go.GetComponent<KAnimControllerBase>().RemoveAnimOverrides(this.kanims[i]);
			}
		}

		// Token: 0x04005EED RID: 24301
		private KAnimFile[] kanims;

		// Token: 0x04005EEE RID: 24302
		private Expression expression;
	}
}
