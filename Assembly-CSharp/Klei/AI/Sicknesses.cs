using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FE4 RID: 4068
	public class Sicknesses : Modifications<Sickness, SicknessInstance>
	{
		// Token: 0x06007DC1 RID: 32193 RVA: 0x0032506F File Offset: 0x0032326F
		public Sicknesses(GameObject go)
			: base(go, Db.Get().Sicknesses)
		{
		}

		// Token: 0x06007DC2 RID: 32194 RVA: 0x00325084 File Offset: 0x00323284
		public void Infect(SicknessExposureInfo exposure_info)
		{
			Sickness sickness = Db.Get().Sicknesses.Get(exposure_info.sicknessID);
			if (!base.Has(sickness))
			{
				this.CreateInstance(sickness).ExposureInfo = exposure_info;
			}
		}

		// Token: 0x06007DC3 RID: 32195 RVA: 0x003250C0 File Offset: 0x003232C0
		public override SicknessInstance CreateInstance(Sickness sickness)
		{
			SicknessInstance sicknessInstance = new SicknessInstance(base.gameObject, sickness);
			this.Add(sicknessInstance);
			base.Trigger(GameHashes.SicknessAdded, sicknessInstance);
			ReportManager.Instance.ReportValue(ReportManager.ReportType.DiseaseAdded, 1f, base.gameObject.GetProperName(), null);
			return sicknessInstance;
		}

		// Token: 0x06007DC4 RID: 32196 RVA: 0x0032510B File Offset: 0x0032330B
		public bool IsInfected()
		{
			return base.Count > 0;
		}

		// Token: 0x06007DC5 RID: 32197 RVA: 0x00325116 File Offset: 0x00323316
		public bool Cure(Sickness sickness)
		{
			return this.Cure(sickness.Id);
		}

		// Token: 0x06007DC6 RID: 32198 RVA: 0x00325124 File Offset: 0x00323324
		public bool Cure(string sickness_id)
		{
			SicknessInstance sicknessInstance = null;
			foreach (SicknessInstance sicknessInstance2 in this)
			{
				if (sicknessInstance2.modifier.Id == sickness_id)
				{
					sicknessInstance = sicknessInstance2;
					break;
				}
			}
			if (sicknessInstance != null)
			{
				this.Remove(sicknessInstance);
				base.Trigger(GameHashes.SicknessCured, sicknessInstance);
				ReportManager.Instance.ReportValue(ReportManager.ReportType.DiseaseAdded, -1f, base.gameObject.GetProperName(), null);
				return true;
			}
			return false;
		}
	}
}
