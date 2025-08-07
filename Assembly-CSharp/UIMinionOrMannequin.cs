using System;
using System.Collections.Generic;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E72 RID: 3698
public class UIMinionOrMannequin : KMonoBehaviour
{
	// Token: 0x17000816 RID: 2070
	// (get) Token: 0x060075EE RID: 30190 RVA: 0x002D2865 File Offset: 0x002D0A65
	// (set) Token: 0x060075EF RID: 30191 RVA: 0x002D286D File Offset: 0x002D0A6D
	public UIMinionOrMannequin.ITarget current { get; private set; }

	// Token: 0x060075F0 RID: 30192 RVA: 0x002D2876 File Offset: 0x002D0A76
	protected override void OnSpawn()
	{
		this.TrySpawn();
	}

	// Token: 0x060075F1 RID: 30193 RVA: 0x002D2880 File Offset: 0x002D0A80
	public bool TrySpawn()
	{
		bool flag = false;
		if (this.mannequin.IsNullOrDestroyed())
		{
			GameObject gameObject = new GameObject("UIMannequin");
			gameObject.AddOrGet<RectTransform>().Fill(Padding.All(10f));
			gameObject.transform.SetParent(base.transform, false);
			AspectRatioFitter aspectRatioFitter = gameObject.AddOrGet<AspectRatioFitter>();
			aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
			aspectRatioFitter.aspectRatio = 1f;
			this.mannequin = gameObject.AddOrGet<UIMannequin>();
			this.mannequin.TrySpawn();
			gameObject.SetActive(false);
			flag = true;
		}
		if (this.minion.IsNullOrDestroyed())
		{
			GameObject gameObject2 = new GameObject("UIMinion");
			gameObject2.AddOrGet<RectTransform>().Fill(Padding.All(10f));
			gameObject2.transform.SetParent(base.transform, false);
			AspectRatioFitter aspectRatioFitter2 = gameObject2.AddOrGet<AspectRatioFitter>();
			aspectRatioFitter2.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
			aspectRatioFitter2.aspectRatio = 1f;
			this.minion = gameObject2.AddOrGet<UIMinion>();
			this.minion.TrySpawn();
			gameObject2.SetActive(false);
			flag = true;
		}
		if (flag)
		{
			this.SetAsMannequin();
		}
		return flag;
	}

	// Token: 0x060075F2 RID: 30194 RVA: 0x002D2988 File Offset: 0x002D0B88
	public UIMinionOrMannequin.ITarget SetFrom(Option<Personality> personality)
	{
		if (personality.IsSome())
		{
			return this.SetAsMinion(personality.Unwrap());
		}
		return this.SetAsMannequin();
	}

	// Token: 0x060075F3 RID: 30195 RVA: 0x002D29A8 File Offset: 0x002D0BA8
	public UIMinion SetAsMinion(Personality personality)
	{
		this.mannequin.gameObject.SetActive(false);
		this.minion.gameObject.SetActive(true);
		this.minion.SetMinion(personality);
		this.current = this.minion;
		return this.minion;
	}

	// Token: 0x060075F4 RID: 30196 RVA: 0x002D29F5 File Offset: 0x002D0BF5
	public UIMannequin SetAsMannequin()
	{
		this.minion.gameObject.SetActive(false);
		this.mannequin.gameObject.SetActive(true);
		this.current = this.mannequin;
		return this.mannequin;
	}

	// Token: 0x060075F5 RID: 30197 RVA: 0x002D2A2C File Offset: 0x002D0C2C
	public MinionVoice GetMinionVoice()
	{
		return MinionVoice.ByObject(this.current.SpawnedAvatar).UnwrapOr(MinionVoice.Random(), null);
	}

	// Token: 0x040051D5 RID: 20949
	public UIMinion minion;

	// Token: 0x040051D6 RID: 20950
	public UIMannequin mannequin;

	// Token: 0x02002068 RID: 8296
	public interface ITarget
	{
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600B64D RID: 46669
		GameObject SpawnedAvatar { get; }

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x0600B64E RID: 46670
		Option<Personality> Personality { get; }

		// Token: 0x0600B64F RID: 46671
		void SetOutfit(ClothingOutfitUtility.OutfitType outfitType, IEnumerable<ClothingItemResource> clothingItems);

		// Token: 0x0600B650 RID: 46672
		void React(UIMinionOrMannequinReactSource source);
	}
}
