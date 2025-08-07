using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000614 RID: 1556
[AddComponentMenu("KMonoBehaviour/scripts/SnapOn")]
public class SnapOn : KMonoBehaviour
{
	// Token: 0x060024F1 RID: 9457 RVA: 0x000D2E04 File Offset: 0x000D1004
	protected override void OnPrefabInit()
	{
		this.kanimController = base.GetComponent<KAnimControllerBase>();
	}

	// Token: 0x060024F2 RID: 9458 RVA: 0x000D2E14 File Offset: 0x000D1014
	protected override void OnSpawn()
	{
		foreach (SnapOn.SnapPoint snapPoint in this.snapPoints)
		{
			if (snapPoint.automatic)
			{
				this.DoAttachSnapOn(snapPoint);
			}
		}
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x000D2E70 File Offset: 0x000D1070
	public void AttachSnapOnByName(string name)
	{
		foreach (SnapOn.SnapPoint snapPoint in this.snapPoints)
		{
			if (snapPoint.pointName == name)
			{
				HashedString context = base.GetComponent<AnimEventHandler>().GetContext();
				if (!context.IsValid || !snapPoint.context.IsValid || context == snapPoint.context)
				{
					this.DoAttachSnapOn(snapPoint);
				}
			}
		}
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x000D2F04 File Offset: 0x000D1104
	public void DetachSnapOnByName(string name)
	{
		foreach (SnapOn.SnapPoint snapPoint in this.snapPoints)
		{
			if (snapPoint.pointName == name)
			{
				HashedString context = base.GetComponent<AnimEventHandler>().GetContext();
				if (!context.IsValid || !snapPoint.context.IsValid || context == snapPoint.context)
				{
					base.GetComponent<SymbolOverrideController>().RemoveSymbolOverride(snapPoint.overrideSymbol, 5);
					this.kanimController.SetSymbolVisiblity(snapPoint.overrideSymbol, false);
					break;
				}
			}
		}
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x000D2FBC File Offset: 0x000D11BC
	private void DoAttachSnapOn(SnapOn.SnapPoint point)
	{
		SnapOn.OverrideEntry overrideEntry = null;
		KAnimFile kanimFile = point.buildFile;
		string text = "";
		if (this.overrideMap.TryGetValue(point.pointName, out overrideEntry))
		{
			kanimFile = overrideEntry.buildFile;
			text = overrideEntry.symbolName;
		}
		KAnim.Build.Symbol symbol = SnapOn.GetSymbol(kanimFile, text);
		base.GetComponent<SymbolOverrideController>().AddSymbolOverride(point.overrideSymbol, symbol, 5);
		this.kanimController.SetSymbolVisiblity(point.overrideSymbol, true);
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x000D3030 File Offset: 0x000D1230
	private static KAnim.Build.Symbol GetSymbol(KAnimFile anim_file, string symbol_name)
	{
		KAnim.Build.Symbol symbol = anim_file.GetData().build.symbols[0];
		KAnimHashedString kanimHashedString = new KAnimHashedString(symbol_name);
		foreach (KAnim.Build.Symbol symbol2 in anim_file.GetData().build.symbols)
		{
			if (symbol2.hash == kanimHashedString)
			{
				symbol = symbol2;
				break;
			}
		}
		return symbol;
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x000D3091 File Offset: 0x000D1291
	public void AddOverride(string point_name, KAnimFile build_override, string symbol_name)
	{
		this.overrideMap[point_name] = new SnapOn.OverrideEntry
		{
			buildFile = build_override,
			symbolName = symbol_name
		};
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x000D30B2 File Offset: 0x000D12B2
	public void RemoveOverride(string point_name)
	{
		this.overrideMap.Remove(point_name);
	}

	// Token: 0x040015AF RID: 5551
	private KAnimControllerBase kanimController;

	// Token: 0x040015B0 RID: 5552
	public List<SnapOn.SnapPoint> snapPoints = new List<SnapOn.SnapPoint>();

	// Token: 0x040015B1 RID: 5553
	private Dictionary<string, SnapOn.OverrideEntry> overrideMap = new Dictionary<string, SnapOn.OverrideEntry>();

	// Token: 0x020014A9 RID: 5289
	[Serializable]
	public class SnapPoint
	{
		// Token: 0x04006D6A RID: 28010
		public string pointName;

		// Token: 0x04006D6B RID: 28011
		public bool automatic = true;

		// Token: 0x04006D6C RID: 28012
		public HashedString context;

		// Token: 0x04006D6D RID: 28013
		public KAnimFile buildFile;

		// Token: 0x04006D6E RID: 28014
		public HashedString overrideSymbol;
	}

	// Token: 0x020014AA RID: 5290
	public class OverrideEntry
	{
		// Token: 0x04006D6F RID: 28015
		public KAnimFile buildFile;

		// Token: 0x04006D70 RID: 28016
		public string symbolName;
	}
}
