using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000F32 RID: 3890
	public class BuildNRoomTypes : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007A98 RID: 31384 RVA: 0x00308986 File Offset: 0x00306B86
		public BuildNRoomTypes(RoomType roomType, int numToCreate = 1)
		{
			this.roomType = roomType;
			this.numToCreate = numToCreate;
		}

		// Token: 0x06007A99 RID: 31385 RVA: 0x0030899C File Offset: 0x00306B9C
		public override bool Success()
		{
			int num = 0;
			using (List<Room>.Enumerator enumerator = Game.Instance.roomProber.rooms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.roomType == this.roomType)
					{
						num++;
					}
				}
			}
			return num >= this.numToCreate;
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x00308A10 File Offset: 0x00306C10
		public void Deserialize(IReader reader)
		{
			string text = reader.ReadKleiString();
			this.roomType = Db.Get().RoomTypes.Get(text);
			this.numToCreate = reader.ReadInt32();
		}

		// Token: 0x06007A9B RID: 31387 RVA: 0x00308A48 File Offset: 0x00306C48
		public override string GetProgress(bool complete)
		{
			int num = 0;
			using (List<Room>.Enumerator enumerator = Game.Instance.roomProber.rooms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.roomType == this.roomType)
					{
						num++;
					}
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILT_N_ROOMS, this.roomType.Name, complete ? this.numToCreate : num, this.numToCreate);
		}

		// Token: 0x040059B0 RID: 22960
		private RoomType roomType;

		// Token: 0x040059B1 RID: 22961
		private int numToCreate;
	}
}
