using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000EF4 RID: 3828
	public class GameplaySeasons : ResourceSet<GameplaySeason>
	{
		// Token: 0x06007996 RID: 31126 RVA: 0x002FD699 File Offset: 0x002FB899
		public GameplaySeasons(ResourceSet parent)
			: base("GameplaySeasons", parent)
		{
			this.VanillaSeasons();
			this.Expansion1Seasons();
			this.DLCSeasons();
			this.UnusedSeasons();
		}

		// Token: 0x06007997 RID: 31127 RVA: 0x002FD6C0 File Offset: 0x002FB8C0
		private void VanillaSeasons()
		{
			this.MeteorShowers = base.Add(new MeteorShowerSeason("MeteorShowers", GameplaySeason.Type.World, 14f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, -1f, null, null).AddEvent(Db.Get().GameplayEvents.MeteorShowerIronEvent).AddEvent(Db.Get().GameplayEvents.MeteorShowerGoldEvent).AddEvent(Db.Get().GameplayEvents.MeteorShowerCopperEvent));
		}

		// Token: 0x06007998 RID: 31128 RVA: 0x002FD740 File Offset: 0x002FB940
		private void Expansion1Seasons()
		{
			this.RegolithMoonMeteorShowers = base.Add(new MeteorShowerSeason("RegolithMoonMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.MeteorShowerDustEvent).AddEvent(Db.Get().GameplayEvents.ClusterIronShower).AddEvent(Db.Get().GameplayEvents.ClusterIceShower));
			this.TemporalTearMeteorShowers = base.Add(new MeteorShowerSeason("TemporalTearMeteorShowers", GameplaySeason.Type.World, 1f, false, 0f, false, -1, 0f, float.PositiveInfinity, 1, false, -1f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.MeteorShowerFullereneEvent));
			this.GassyMooteorShowers = base.Add(new MeteorShowerSeason("GassyMooteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, false, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.GassyMooteorEvent));
			this.SpacedOutStyleStartMeteorShowers = base.Add(new MeteorShowerSeason("SpacedOutStyleStartMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null));
			this.SpacedOutStyleRocketMeteorShowers = base.Add(new MeteorShowerSeason("SpacedOutStyleRocketMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.ClusterOxyliteShower));
			this.SpacedOutStyleWarpMeteorShowers = base.Add(new MeteorShowerSeason("SpacedOutStyleWarpMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.ClusterCopperShower).AddEvent(Db.Get().GameplayEvents.ClusterIceShower).AddEvent(Db.Get().GameplayEvents.ClusterBiologicalShower));
			this.ClassicStyleStartMeteorShowers = base.Add(new MeteorShowerSeason("ClassicStyleStartMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.ClusterCopperShower).AddEvent(Db.Get().GameplayEvents.ClusterIceShower).AddEvent(Db.Get().GameplayEvents.ClusterBiologicalShower));
			this.ClassicStyleWarpMeteorShowers = base.Add(new MeteorShowerSeason("ClassicStyleWarpMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.ClusterGoldShower).AddEvent(Db.Get().GameplayEvents.ClusterIronShower));
			this.TundraMoonletMeteorShowers = base.Add(new MeteorShowerSeason("TundraMoonletMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null));
			this.MarshyMoonletMeteorShowers = base.Add(new MeteorShowerSeason("MarshyMoonletMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null));
			this.NiobiumMoonletMeteorShowers = base.Add(new MeteorShowerSeason("NiobiumMoonletMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null));
			this.WaterMoonletMeteorShowers = base.Add(new MeteorShowerSeason("WaterMoonletMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null));
			this.MiniMetallicSwampyMeteorShowers = base.Add(new MeteorShowerSeason("MiniMetallicSwampyMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.ClusterBiologicalShower).AddEvent(Db.Get().GameplayEvents.ClusterGoldShower));
			this.MiniForestFrozenMeteorShowers = base.Add(new MeteorShowerSeason("MiniForestFrozenMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.ClusterOxyliteShower));
			this.MiniBadlandsMeteorShowers = base.Add(new MeteorShowerSeason("MiniBadlandsMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.ClusterIceShower));
			this.MiniFlippedMeteorShowers = base.Add(new MeteorShowerSeason("MiniFlippedMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null));
			this.MiniRadioactiveOceanMeteorShowers = base.Add(new MeteorShowerSeason("MiniRadioactiveOceanMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1, null).AddEvent(Db.Get().GameplayEvents.ClusterUraniumShower));
		}

		// Token: 0x06007999 RID: 31129 RVA: 0x002FDCB4 File Offset: 0x002FBEB4
		private void DLCSeasons()
		{
			this.CeresMeteorShowers = base.Add(new MeteorShowerSeason("CeresMeteorShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 10f, float.PositiveInfinity, 1, true, 6000f, DlcManager.DLC2, null).AddEvent(Db.Get().GameplayEvents.ClusterIceAndTreesShower));
			this.MiniCeresStartShowers = base.Add(new MeteorShowerSeason("MiniCeresStartShowers", GameplaySeason.Type.World, 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.EXPANSION1.Append(DlcManager.DLC2), null).AddEvent(Db.Get().GameplayEvents.ClusterOxyliteShower).AddEvent(Db.Get().GameplayEvents.ClusterSnowShower));
			this.LargeImpactor = base.Add(new GameplaySeason("LargeImpactor", GameplaySeason.Type.World, 1f, false, -1f, true, 1, 0f, float.PositiveInfinity, 1, DlcManager.DLC4, null).AddEvent(Db.Get().GameplayEvents.LargeImpactor));
			this.PrehistoricMeteorShowers = base.Add(new MeteorShowerSeason("PrehistoricMeteorShowers", GameplaySeason.Type.World, 50f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1, true, 6000f, DlcManager.DLC4, null).AddEvent(Db.Get().GameplayEvents.ClusterCopperShower).AddEvent(Db.Get().GameplayEvents.ClusterIronShower).AddEvent(Db.Get().GameplayEvents.ClusterGoldShower));
		}

		// Token: 0x0600799A RID: 31130 RVA: 0x002FDE3D File Offset: 0x002FC03D
		private void UnusedSeasons()
		{
		}

		// Token: 0x040057F3 RID: 22515
		public GameplaySeason NaturalRandomEvents;

		// Token: 0x040057F4 RID: 22516
		public GameplaySeason DupeRandomEvents;

		// Token: 0x040057F5 RID: 22517
		public GameplaySeason PrickleCropSeason;

		// Token: 0x040057F6 RID: 22518
		public GameplaySeason BonusEvents;

		// Token: 0x040057F7 RID: 22519
		public GameplaySeason MeteorShowers;

		// Token: 0x040057F8 RID: 22520
		public GameplaySeason TemporalTearMeteorShowers;

		// Token: 0x040057F9 RID: 22521
		public GameplaySeason SpacedOutStyleStartMeteorShowers;

		// Token: 0x040057FA RID: 22522
		public GameplaySeason SpacedOutStyleRocketMeteorShowers;

		// Token: 0x040057FB RID: 22523
		public GameplaySeason SpacedOutStyleWarpMeteorShowers;

		// Token: 0x040057FC RID: 22524
		public GameplaySeason ClassicStyleStartMeteorShowers;

		// Token: 0x040057FD RID: 22525
		public GameplaySeason ClassicStyleWarpMeteorShowers;

		// Token: 0x040057FE RID: 22526
		public GameplaySeason TundraMoonletMeteorShowers;

		// Token: 0x040057FF RID: 22527
		public GameplaySeason MarshyMoonletMeteorShowers;

		// Token: 0x04005800 RID: 22528
		public GameplaySeason NiobiumMoonletMeteorShowers;

		// Token: 0x04005801 RID: 22529
		public GameplaySeason WaterMoonletMeteorShowers;

		// Token: 0x04005802 RID: 22530
		public GameplaySeason GassyMooteorShowers;

		// Token: 0x04005803 RID: 22531
		public GameplaySeason RegolithMoonMeteorShowers;

		// Token: 0x04005804 RID: 22532
		public GameplaySeason MiniMetallicSwampyMeteorShowers;

		// Token: 0x04005805 RID: 22533
		public GameplaySeason MiniForestFrozenMeteorShowers;

		// Token: 0x04005806 RID: 22534
		public GameplaySeason MiniBadlandsMeteorShowers;

		// Token: 0x04005807 RID: 22535
		public GameplaySeason MiniFlippedMeteorShowers;

		// Token: 0x04005808 RID: 22536
		public GameplaySeason MiniRadioactiveOceanMeteorShowers;

		// Token: 0x04005809 RID: 22537
		public GameplaySeason MiniCeresStartShowers;

		// Token: 0x0400580A RID: 22538
		public GameplaySeason CeresMeteorShowers;

		// Token: 0x0400580B RID: 22539
		public GameplaySeason LargeImpactor;

		// Token: 0x0400580C RID: 22540
		public GameplaySeason PrehistoricMeteorShowers;
	}
}
