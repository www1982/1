using System;

namespace ProcGenGame
{
	// Token: 0x02000E9D RID: 3741
	public class WorldgenException : Exception
	{
		// Token: 0x0600775F RID: 30559 RVA: 0x002E0ABD File Offset: 0x002DECBD
		public WorldgenException(string message, string userMessage)
			: base(message)
		{
			this.userMessage = userMessage;
		}

		// Token: 0x040052FA RID: 21242
		public readonly string userMessage;
	}
}
