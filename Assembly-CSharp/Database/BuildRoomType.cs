using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000F31 RID: 3889
	public class BuildRoomType : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007A94 RID: 31380 RVA: 0x003088C9 File Offset: 0x00306AC9
		public BuildRoomType(RoomType roomType)
		{
			this.roomType = roomType;
		}

		// Token: 0x06007A95 RID: 31381 RVA: 0x003088D8 File Offset: 0x00306AD8
		public override bool Success()
		{
			using (List<Room>.Enumerator enumerator = Game.Instance.roomProber.rooms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.roomType == this.roomType)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007A96 RID: 31382 RVA: 0x00308940 File Offset: 0x00306B40
		public void Deserialize(IReader reader)
		{
			string text = reader.ReadKleiString();
			this.roomType = Db.Get().RoomTypes.Get(text);
		}

		// Token: 0x06007A97 RID: 31383 RVA: 0x0030896A File Offset: 0x00306B6A
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILT_A_ROOM, this.roomType.Name);
		}

		// Token: 0x040059AF RID: 22959
		private RoomType roomType;
	}
}
