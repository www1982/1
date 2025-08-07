using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F41 RID: 3905
	public class VentXKG : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AD7 RID: 31447 RVA: 0x00309BAF File Offset: 0x00307DAF
		public VentXKG(SimHashes element, float kilogramsToVent)
		{
			this.element = element;
			this.kilogramsToVent = kilogramsToVent;
		}

		// Token: 0x06007AD8 RID: 31448 RVA: 0x00309BC8 File Offset: 0x00307DC8
		public override bool Success()
		{
			float num = 0f;
			foreach (UtilityNetwork utilityNetwork in Conduit.GetNetworkManager(ConduitType.Gas).GetNetworks())
			{
				FlowUtilityNetwork flowUtilityNetwork = utilityNetwork as FlowUtilityNetwork;
				if (flowUtilityNetwork != null)
				{
					foreach (FlowUtilityNetwork.IItem item in flowUtilityNetwork.sinks)
					{
						Vent component = item.GameObject.GetComponent<Vent>();
						if (component != null)
						{
							num += component.GetVentedMass(this.element);
						}
					}
				}
			}
			return num >= this.kilogramsToVent;
		}

		// Token: 0x06007AD9 RID: 31449 RVA: 0x00309C90 File Offset: 0x00307E90
		public void Deserialize(IReader reader)
		{
			this.element = (SimHashes)reader.ReadInt32();
			this.kilogramsToVent = reader.ReadSingle();
		}

		// Token: 0x06007ADA RID: 31450 RVA: 0x00309CAC File Offset: 0x00307EAC
		public override string GetProgress(bool complete)
		{
			float num = 0f;
			foreach (UtilityNetwork utilityNetwork in Conduit.GetNetworkManager(ConduitType.Gas).GetNetworks())
			{
				FlowUtilityNetwork flowUtilityNetwork = utilityNetwork as FlowUtilityNetwork;
				if (flowUtilityNetwork != null)
				{
					foreach (FlowUtilityNetwork.IItem item in flowUtilityNetwork.sinks)
					{
						Vent component = item.GameObject.GetComponent<Vent>();
						if (component != null)
						{
							num += component.GetVentedMass(this.element);
						}
					}
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.VENTED_MASS, GameUtil.GetFormattedMass(complete ? this.kilogramsToVent : num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"), GameUtil.GetFormattedMass(this.kilogramsToVent, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"));
		}

		// Token: 0x040059C6 RID: 22982
		private SimHashes element;

		// Token: 0x040059C7 RID: 22983
		private float kilogramsToVent;
	}
}
