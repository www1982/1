using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F06 RID: 3846
	public class RoomTypeCategories : ResourceSet<RoomTypeCategory>
	{
		// Token: 0x060079D3 RID: 31187 RVA: 0x00300D98 File Offset: 0x002FEF98
		private RoomTypeCategory Add(string id, string name, string colorName, string icon)
		{
			RoomTypeCategory roomTypeCategory = new RoomTypeCategory(id, name, colorName, icon);
			base.Add(roomTypeCategory);
			return roomTypeCategory;
		}

		// Token: 0x060079D4 RID: 31188 RVA: 0x00300DBC File Offset: 0x002FEFBC
		public RoomTypeCategories(ResourceSet parent)
			: base("RoomTypeCategories", parent)
		{
			base.Initialize();
			this.None = this.Add("None", ROOMS.CATEGORY.NONE.NAME, "roomNone", "unknown");
			this.Food = this.Add("Food", ROOMS.CATEGORY.FOOD.NAME, "roomFood", "ui_room_food");
			this.Sleep = this.Add("Sleep", ROOMS.CATEGORY.SLEEP.NAME, "roomSleep", "ui_room_sleep");
			this.Recreation = this.Add("Recreation", ROOMS.CATEGORY.RECREATION.NAME, "roomRecreation", "ui_room_recreational");
			if (DlcManager.IsContentSubscribed("DLC3_ID"))
			{
				this.Bionic = this.Add("Bionic", ROOMS.CATEGORY.BIONIC.NAME, "roomBionic", "ui_room_bionicupkeep");
			}
			this.Bathroom = this.Add("Bathroom", ROOMS.CATEGORY.BATHROOM.NAME, "roomBathroom", "ui_room_bathroom");
			this.Hospital = this.Add("Hospital", ROOMS.CATEGORY.HOSPITAL.NAME, "roomHospital", "ui_room_hospital");
			this.Industrial = this.Add("Industrial", ROOMS.CATEGORY.INDUSTRIAL.NAME, "roomIndustrial", "ui_room_industrial");
			this.Agricultural = this.Add("Agricultural", ROOMS.CATEGORY.AGRICULTURAL.NAME, "roomAgricultural", "ui_room_agricultural");
			this.Park = this.Add("Park", ROOMS.CATEGORY.PARK.NAME, "roomPark", "ui_room_park");
			this.Science = this.Add("Science", ROOMS.CATEGORY.SCIENCE.NAME, "roomScience", "ui_room_science");
		}

		// Token: 0x040058BA RID: 22714
		public RoomTypeCategory None;

		// Token: 0x040058BB RID: 22715
		public RoomTypeCategory Food;

		// Token: 0x040058BC RID: 22716
		public RoomTypeCategory Sleep;

		// Token: 0x040058BD RID: 22717
		public RoomTypeCategory Recreation;

		// Token: 0x040058BE RID: 22718
		public RoomTypeCategory Bathroom;

		// Token: 0x040058BF RID: 22719
		public RoomTypeCategory Bionic;

		// Token: 0x040058C0 RID: 22720
		public RoomTypeCategory Hospital;

		// Token: 0x040058C1 RID: 22721
		public RoomTypeCategory Industrial;

		// Token: 0x040058C2 RID: 22722
		public RoomTypeCategory Agricultural;

		// Token: 0x040058C3 RID: 22723
		public RoomTypeCategory Park;

		// Token: 0x040058C4 RID: 22724
		public RoomTypeCategory Science;
	}
}
