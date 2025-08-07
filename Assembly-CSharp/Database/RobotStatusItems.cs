using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000F05 RID: 3845
	public class RobotStatusItems : StatusItems
	{
		// Token: 0x060079D1 RID: 31185 RVA: 0x003009E1 File Offset: 0x002FEBE1
		public RobotStatusItems(ResourceSet parent)
			: base("RobotStatusItems", parent)
		{
			this.CreateStatusItems();
		}

		// Token: 0x060079D2 RID: 31186 RVA: 0x003009F8 File Offset: 0x002FEBF8
		private void CreateStatusItems()
		{
			this.CantReachStation = new StatusItem("CantReachStation", "ROBOTS", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.CantReachStation.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject = (GameObject)data;
				return str.Replace("{0}", gameObject.GetProperName());
			};
			this.LowBattery = new StatusItem("LowBattery", "ROBOTS", "status_item_need_power", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.LowBattery.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject2 = (GameObject)data;
				return str.Replace("{0}", gameObject2.GetProperName());
			};
			this.LowBatteryNoCharge = new StatusItem("LowBatteryNoCharge", "ROBOTS", "status_item_need_power", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.LowBatteryNoCharge.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject3 = (GameObject)data;
				return str.Replace("{0}", gameObject3.GetProperName());
			};
			this.DeadBattery = new StatusItem("DeadBattery", "ROBOTS", "status_item_need_power", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.DeadBattery.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject4 = (GameObject)data;
				return str.Replace("{0}", gameObject4.GetProperName());
			};
			this.DeadBatteryFlydo = new StatusItem("DeadBatteryFlydo", "ROBOTS", "status_item_need_power", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.DeadBatteryFlydo.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject5 = (GameObject)data;
				return str.Replace("{0}", gameObject5.GetProperName());
			};
			this.DustBinFull = new StatusItem("DustBinFull", "ROBOTS", "status_item_pending_clear", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.DustBinFull.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject6 = (GameObject)data;
				return str.Replace("{0}", gameObject6.GetProperName());
			};
			this.Working = new StatusItem("Working", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.Working.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject7 = (GameObject)data;
				return str.Replace("{0}", gameObject7.GetProperName());
			};
			this.MovingToChargeStation = new StatusItem("MovingToChargeStation", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.MovingToChargeStation.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject8 = (GameObject)data;
				return str.Replace("{0}", gameObject8.GetProperName());
			};
			this.UnloadingStorage = new StatusItem("UnloadingStorage", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.UnloadingStorage.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject9 = (GameObject)data;
				return str.Replace("{0}", gameObject9.GetProperName());
			};
			this.ReactPositive = new StatusItem("ReactPositive", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.ReactPositive.resolveStringCallback = (string str, object data) => str;
			this.ReactNegative = new StatusItem("ReactNegative", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.ReactNegative.resolveStringCallback = (string str, object data) => str;
		}

		// Token: 0x040058AF RID: 22703
		public StatusItem LowBattery;

		// Token: 0x040058B0 RID: 22704
		public StatusItem LowBatteryNoCharge;

		// Token: 0x040058B1 RID: 22705
		public StatusItem DeadBattery;

		// Token: 0x040058B2 RID: 22706
		public StatusItem DeadBatteryFlydo;

		// Token: 0x040058B3 RID: 22707
		public StatusItem CantReachStation;

		// Token: 0x040058B4 RID: 22708
		public StatusItem DustBinFull;

		// Token: 0x040058B5 RID: 22709
		public StatusItem Working;

		// Token: 0x040058B6 RID: 22710
		public StatusItem UnloadingStorage;

		// Token: 0x040058B7 RID: 22711
		public StatusItem ReactPositive;

		// Token: 0x040058B8 RID: 22712
		public StatusItem ReactNegative;

		// Token: 0x040058B9 RID: 22713
		public StatusItem MovingToChargeStation;
	}
}
