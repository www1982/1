using System;

// Token: 0x0200045E RID: 1118
public static class Result
{
	// Token: 0x06001771 RID: 6001 RVA: 0x00082A81 File Offset: 0x00080C81
	public static Result.Internal.Value_Ok<T> Ok<T>(T value)
	{
		return new Result.Internal.Value_Ok<T>(value);
	}

	// Token: 0x06001772 RID: 6002 RVA: 0x00082A89 File Offset: 0x00080C89
	public static Result.Internal.Value_Err<T> Err<T>(T value)
	{
		return new Result.Internal.Value_Err<T>(value);
	}

	// Token: 0x02001237 RID: 4663
	public static class Internal
	{
		// Token: 0x02002624 RID: 9764
		public readonly struct Value_Ok<T>
		{
			// Token: 0x0600C28C RID: 49804 RVA: 0x0040AC8A File Offset: 0x00408E8A
			public Value_Ok(T value)
			{
				this.value = value;
			}

			// Token: 0x0400A9AB RID: 43435
			public readonly T value;
		}

		// Token: 0x02002625 RID: 9765
		public readonly struct Value_Err<T>
		{
			// Token: 0x0600C28D RID: 49805 RVA: 0x0040AC93 File Offset: 0x00408E93
			public Value_Err(T value)
			{
				this.value = value;
			}

			// Token: 0x0400A9AC RID: 43436
			public readonly T value;
		}
	}
}
