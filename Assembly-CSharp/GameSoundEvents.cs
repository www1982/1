using System;

// Token: 0x0200092C RID: 2348
public static class GameSoundEvents
{
	// Token: 0x04002B53 RID: 11091
	public static GameSoundEvents.Event BatteryFull = new GameSoundEvents.Event("game_triggered.battery_full");

	// Token: 0x04002B54 RID: 11092
	public static GameSoundEvents.Event BatteryWarning = new GameSoundEvents.Event("game_triggered.battery_warning");

	// Token: 0x04002B55 RID: 11093
	public static GameSoundEvents.Event BatteryDischarged = new GameSoundEvents.Event("game_triggered.battery_drained");

	// Token: 0x020018EA RID: 6378
	public class Event
	{
		// Token: 0x06009E04 RID: 40452 RVA: 0x00394DCF File Offset: 0x00392FCF
		public Event(string name)
		{
			this.Name = name;
		}

		// Token: 0x04007A36 RID: 31286
		public HashedString Name;
	}
}
