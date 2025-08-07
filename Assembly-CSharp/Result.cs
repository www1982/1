using System;

// Token: 0x0200045D RID: 1117
public readonly struct Result<TSuccess, TError>
{
	// Token: 0x06001769 RID: 5993 RVA: 0x000829AA File Offset: 0x00080BAA
	private Result(TSuccess successValue, TError errorValue)
	{
		this.successValue = successValue;
		this.errorValue = errorValue;
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x000829C4 File Offset: 0x00080BC4
	public bool IsOk()
	{
		return this.successValue.IsSome();
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x000829D1 File Offset: 0x00080BD1
	public bool IsErr()
	{
		return this.errorValue.IsSome() || this.successValue.IsNone();
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x000829ED File Offset: 0x00080BED
	public TSuccess Unwrap()
	{
		if (this.successValue.IsSome())
		{
			return this.successValue.Unwrap();
		}
		if (this.errorValue.IsSome())
		{
			throw new Exception("Tried to unwrap result that is an Err()");
		}
		throw new Exception("Tried to unwrap result that isn't initialized with an Err() or Ok() value");
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x00082A2A File Offset: 0x00080C2A
	public Option<TSuccess> Ok()
	{
		return this.successValue;
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x00082A32 File Offset: 0x00080C32
	public Option<TError> Err()
	{
		return this.errorValue;
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x00082A3C File Offset: 0x00080C3C
	public static implicit operator Result<TSuccess, TError>(Result.Internal.Value_Ok<TSuccess> value)
	{
		return new Result<TSuccess, TError>(value.value, default(TError));
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x00082A60 File Offset: 0x00080C60
	public static implicit operator Result<TSuccess, TError>(Result.Internal.Value_Err<TError> value)
	{
		return new Result<TSuccess, TError>(default(TSuccess), value.value);
	}

	// Token: 0x04000D9C RID: 3484
	private readonly Option<TSuccess> successValue;

	// Token: 0x04000D9D RID: 3485
	private readonly Option<TError> errorValue;
}
