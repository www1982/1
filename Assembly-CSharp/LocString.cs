using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020009AB RID: 2475
[Serializable]
public class LocString
{
	// Token: 0x17000506 RID: 1286
	// (get) Token: 0x060047F6 RID: 18422 RVA: 0x0019FDB0 File Offset: 0x0019DFB0
	public string text
	{
		get
		{
			return this._text;
		}
	}

	// Token: 0x17000507 RID: 1287
	// (get) Token: 0x060047F7 RID: 18423 RVA: 0x0019FDB8 File Offset: 0x0019DFB8
	public StringKey key
	{
		get
		{
			return this._key;
		}
	}

	// Token: 0x060047F8 RID: 18424 RVA: 0x0019FDC0 File Offset: 0x0019DFC0
	public LocString(string text)
	{
		this._text = text;
		this._key = default(StringKey);
	}

	// Token: 0x060047F9 RID: 18425 RVA: 0x0019FDDB File Offset: 0x0019DFDB
	public LocString(string text, string keystring)
	{
		this._text = text;
		this._key = new StringKey(keystring);
	}

	// Token: 0x060047FA RID: 18426 RVA: 0x0019FDF6 File Offset: 0x0019DFF6
	public LocString(string text, bool isLocalized)
	{
		this._text = text;
		this._key = default(StringKey);
	}

	// Token: 0x060047FB RID: 18427 RVA: 0x0019FE11 File Offset: 0x0019E011
	public static implicit operator LocString(string text)
	{
		return new LocString(text);
	}

	// Token: 0x060047FC RID: 18428 RVA: 0x0019FE19 File Offset: 0x0019E019
	public static implicit operator string(LocString loc_string)
	{
		return loc_string.text;
	}

	// Token: 0x060047FD RID: 18429 RVA: 0x0019FE21 File Offset: 0x0019E021
	public override string ToString()
	{
		return Strings.Get(this.key).String;
	}

	// Token: 0x060047FE RID: 18430 RVA: 0x0019FE33 File Offset: 0x0019E033
	public void SetKey(string key_name)
	{
		this._key = new StringKey(key_name);
	}

	// Token: 0x060047FF RID: 18431 RVA: 0x0019FE41 File Offset: 0x0019E041
	public void SetKey(StringKey key)
	{
		this._key = key;
	}

	// Token: 0x06004800 RID: 18432 RVA: 0x0019FE4A File Offset: 0x0019E04A
	public string Replace(string search, string replacement)
	{
		return this.ToString().Replace(search, replacement);
	}

	// Token: 0x06004801 RID: 18433 RVA: 0x0019FE5C File Offset: 0x0019E05C
	public static void CreateLocStringKeys(Type type, string parent_path = "STRINGS.")
	{
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		string text = parent_path;
		if (text == null)
		{
			text = "";
		}
		text = text + type.Name + ".";
		foreach (FieldInfo fieldInfo in fields)
		{
			if (!(fieldInfo.FieldType != typeof(LocString)))
			{
				if (!fieldInfo.IsStatic)
				{
					DebugUtil.DevLogError("LocString fields must be static, skipping. " + parent_path);
				}
				else
				{
					string text2 = text + fieldInfo.Name;
					LocString locString = (LocString)fieldInfo.GetValue(null);
					locString.SetKey(text2);
					string text3 = locString.text;
					Strings.Add(new string[] { text2, text3 });
					fieldInfo.SetValue(null, locString);
				}
			}
		}
		Type[] nestedTypes = type.GetNestedTypes(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		for (int i = 0; i < nestedTypes.Length; i++)
		{
			LocString.CreateLocStringKeys(nestedTypes[i], text);
		}
	}

	// Token: 0x06004802 RID: 18434 RVA: 0x0019FF48 File Offset: 0x0019E148
	public static string[] GetStrings(Type type)
	{
		List<string> list = new List<string>();
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		for (int i = 0; i < fields.Length; i++)
		{
			LocString locString = (LocString)fields[i].GetValue(null);
			list.Add(locString.text);
		}
		return list.ToArray();
	}

	// Token: 0x04002F9B RID: 12187
	[SerializeField]
	private string _text;

	// Token: 0x04002F9C RID: 12188
	[SerializeField]
	private StringKey _key;

	// Token: 0x04002F9D RID: 12189
	public const BindingFlags data_member_fields = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
}
