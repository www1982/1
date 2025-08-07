using System;

// Token: 0x02000456 RID: 1110
public static class Option
{
	// Token: 0x06001730 RID: 5936 RVA: 0x000822BA File Offset: 0x000804BA
	public static Option<T> Some<T>(T value)
	{
		return new Option<T>(value);
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x000822C4 File Offset: 0x000804C4
	public static Option<T> Maybe<T>(T value)
	{
		if (value.IsNullOrDestroyed())
		{
			return default(Option<T>);
		}
		return new Option<T>(value);
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06001732 RID: 5938 RVA: 0x000822F0 File Offset: 0x000804F0
	public static Option.Internal.Value_None None
	{
		get
		{
			return default(Option.Internal.Value_None);
		}
	}

	// Token: 0x06001733 RID: 5939 RVA: 0x00082308 File Offset: 0x00080508
	public static bool AllHaveValues(params Option.Internal.Value_HasValue[] options)
	{
		if (options == null || options.Length == 0)
		{
			return false;
		}
		for (int i = 0; i < options.Length; i++)
		{
			if (!options[i].HasValue)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0200122E RID: 4654
	public static class Internal
	{
		// Token: 0x02002622 RID: 9762
		public readonly struct Value_None
		{
		}

		// Token: 0x02002623 RID: 9763
		public readonly struct Value_HasValue
		{
			// Token: 0x0600C28B RID: 49803 RVA: 0x0040AC81 File Offset: 0x00408E81
			public Value_HasValue(bool hasValue)
			{
				this.HasValue = hasValue;
			}

			// Token: 0x0400A9AA RID: 43434
			public readonly bool HasValue;
		}
	}
}
