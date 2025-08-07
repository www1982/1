using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;

// Token: 0x02000455 RID: 1109
[DebuggerDisplay("has_value={hasValue} {value}")]
[Serializable]
public readonly struct Option<T> : IEquatable<Option<T>>, IEquatable<T>
{
	// Token: 0x06001713 RID: 5907 RVA: 0x00081FD8 File Offset: 0x000801D8
	public Option(T value)
	{
		this.value = value;
		this.hasValue = true;
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06001714 RID: 5908 RVA: 0x00081FE8 File Offset: 0x000801E8
	public bool HasValue
	{
		get
		{
			return this.hasValue;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06001715 RID: 5909 RVA: 0x00081FF0 File Offset: 0x000801F0
	public T Value
	{
		get
		{
			return this.Unwrap();
		}
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x00081FF8 File Offset: 0x000801F8
	public T Unwrap()
	{
		if (!this.hasValue)
		{
			throw new Exception("Tried to get a value for a Option<" + typeof(T).FullName + ">, but hasValue is false");
		}
		return this.value;
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x0008202C File Offset: 0x0008022C
	public T UnwrapOr(T fallback_value, string warn_on_fallback = null)
	{
		if (!this.hasValue)
		{
			if (warn_on_fallback != null)
			{
				DebugUtil.DevAssert(false, "Failed to unwrap a Option<" + typeof(T).FullName + ">: " + warn_on_fallback, null);
			}
			return fallback_value;
		}
		return this.value;
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x00082067 File Offset: 0x00080267
	public T UnwrapOrElse(Func<T> get_fallback_value_fn, string warn_on_fallback = null)
	{
		if (!this.hasValue)
		{
			if (warn_on_fallback != null)
			{
				DebugUtil.DevAssert(false, "Failed to unwrap a Option<" + typeof(T).FullName + ">: " + warn_on_fallback, null);
			}
			return get_fallback_value_fn();
		}
		return this.value;
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x000820A8 File Offset: 0x000802A8
	public T UnwrapOrDefault()
	{
		if (!this.hasValue)
		{
			return default(T);
		}
		return this.value;
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x000820CD File Offset: 0x000802CD
	public T Expect(string msg_on_fail)
	{
		if (!this.hasValue)
		{
			throw new Exception(msg_on_fail);
		}
		return this.value;
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x000820E4 File Offset: 0x000802E4
	public bool IsSome()
	{
		return this.hasValue;
	}

	// Token: 0x0600171C RID: 5916 RVA: 0x000820EC File Offset: 0x000802EC
	public bool IsNone()
	{
		return !this.hasValue;
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x000820F7 File Offset: 0x000802F7
	public Option<U> AndThen<U>(Func<T, U> fn)
	{
		if (this.IsNone())
		{
			return Option.None;
		}
		return Option.Maybe<U>(fn(this.value));
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x0008211D File Offset: 0x0008031D
	public Option<U> AndThen<U>(Func<T, Option<U>> fn)
	{
		if (this.IsNone())
		{
			return Option.None;
		}
		return fn(this.value);
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x0008213E File Offset: 0x0008033E
	public static implicit operator Option<T>(T value)
	{
		return Option.Maybe<T>(value);
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x00082146 File Offset: 0x00080346
	public static explicit operator T(Option<T> option)
	{
		return option.Unwrap();
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x00082150 File Offset: 0x00080350
	public static implicit operator Option<T>(Option.Internal.Value_None value)
	{
		return default(Option<T>);
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x00082166 File Offset: 0x00080366
	public static implicit operator Option.Internal.Value_HasValue(Option<T> value)
	{
		return new Option.Internal.Value_HasValue(value.hasValue);
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x00082173 File Offset: 0x00080373
	public void Deconstruct(out bool hasValue, out T value)
	{
		hasValue = this.hasValue;
		value = this.value;
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x00082189 File Offset: 0x00080389
	public bool Equals(Option<T> other)
	{
		return EqualityComparer<bool>.Default.Equals(this.hasValue, other.hasValue) && EqualityComparer<T>.Default.Equals(this.value, other.value);
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000821BC File Offset: 0x000803BC
	public override bool Equals(object obj)
	{
		if (obj is Option<T>)
		{
			Option<T> option = (Option<T>)obj;
			return this.Equals(option);
		}
		return false;
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x000821E1 File Offset: 0x000803E1
	public static bool operator ==(Option<T> lhs, Option<T> rhs)
	{
		return lhs.Equals(rhs);
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x000821EB File Offset: 0x000803EB
	public static bool operator !=(Option<T> lhs, Option<T> rhs)
	{
		return !(lhs == rhs);
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x000821F8 File Offset: 0x000803F8
	public override int GetHashCode()
	{
		return (-363764631 * -1521134295 + this.hasValue.GetHashCode()) * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.value);
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x00082236 File Offset: 0x00080436
	public override string ToString()
	{
		if (!this.hasValue)
		{
			return "None";
		}
		return string.Format("{0}", this.value);
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x0008225B File Offset: 0x0008045B
	public static bool operator ==(Option<T> lhs, T rhs)
	{
		return lhs.Equals(rhs);
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x00082265 File Offset: 0x00080465
	public static bool operator !=(Option<T> lhs, T rhs)
	{
		return !(lhs == rhs);
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x00082271 File Offset: 0x00080471
	public static bool operator ==(T lhs, Option<T> rhs)
	{
		return rhs.Equals(lhs);
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x0008227B File Offset: 0x0008047B
	public static bool operator !=(T lhs, Option<T> rhs)
	{
		return !(lhs == rhs);
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x00082287 File Offset: 0x00080487
	public bool Equals(T other)
	{
		return this.HasValue && EqualityComparer<T>.Default.Equals(this.value, other);
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x0600172F RID: 5935 RVA: 0x000822A4 File Offset: 0x000804A4
	public static Option<T> None
	{
		get
		{
			return default(Option<T>);
		}
	}

	// Token: 0x04000D91 RID: 3473
	[Serialize]
	private readonly bool hasValue;

	// Token: 0x04000D92 RID: 3474
	[Serialize]
	private readonly T value;
}
