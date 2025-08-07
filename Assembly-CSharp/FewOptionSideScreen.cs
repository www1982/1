using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DF5 RID: 3573
public class FewOptionSideScreen : SideScreenContent
{
	// Token: 0x060070CE RID: 28878 RVA: 0x002AE693 File Offset: 0x002AC893
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.RefreshOptions();
		}
	}

	// Token: 0x060070CF RID: 28879 RVA: 0x002AE6A8 File Offset: 0x002AC8A8
	private void RefreshOptions()
	{
		foreach (KeyValuePair<Tag, GameObject> keyValuePair in this.rows)
		{
			keyValuePair.Value.GetComponent<MultiToggle>().ChangeState((keyValuePair.Key == this.targetFewOptions.GetSelectedOption()) ? 1 : 0);
		}
	}

	// Token: 0x060070D0 RID: 28880 RVA: 0x002AE724 File Offset: 0x002AC924
	private void ClearRows()
	{
		for (int i = this.rowContainer.childCount - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.rowContainer.GetChild(i));
		}
		this.rows.Clear();
	}

	// Token: 0x060070D1 RID: 28881 RVA: 0x002AE768 File Offset: 0x002AC968
	private void SpawnRows()
	{
		FewOptionSideScreen.IFewOptionSideScreen.Option[] options = this.targetFewOptions.GetOptions();
		for (int i = 0; i < options.Length; i++)
		{
			FewOptionSideScreen.IFewOptionSideScreen.Option option = options[i];
			GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.rowContainer.gameObject, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("label").SetText(option.labelText);
			component.GetReference<Image>("icon").sprite = option.iconSpriteColorTuple.first;
			component.GetReference<Image>("icon").color = option.iconSpriteColorTuple.second;
			gameObject.GetComponent<ToolTip>().toolTip = option.tooltipText;
			gameObject.GetComponent<MultiToggle>().onClick = delegate
			{
				this.targetFewOptions.OnOptionSelected(option);
				this.RefreshOptions();
			};
			this.rows.Add(option.tag, gameObject);
		}
		this.RefreshOptions();
	}

	// Token: 0x060070D2 RID: 28882 RVA: 0x002AE871 File Offset: 0x002ACA71
	public override void SetTarget(GameObject target)
	{
		this.ClearRows();
		this.targetFewOptions = target.GetComponent<FewOptionSideScreen.IFewOptionSideScreen>();
		this.SpawnRows();
	}

	// Token: 0x060070D3 RID: 28883 RVA: 0x002AE88B File Offset: 0x002ACA8B
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<FewOptionSideScreen.IFewOptionSideScreen>() != null;
	}

	// Token: 0x04004D9C RID: 19868
	public GameObject rowPrefab;

	// Token: 0x04004D9D RID: 19869
	public RectTransform rowContainer;

	// Token: 0x04004D9E RID: 19870
	public Dictionary<Tag, GameObject> rows = new Dictionary<Tag, GameObject>();

	// Token: 0x04004D9F RID: 19871
	private FewOptionSideScreen.IFewOptionSideScreen targetFewOptions;

	// Token: 0x02002006 RID: 8198
	public interface IFewOptionSideScreen
	{
		// Token: 0x0600B508 RID: 46344
		FewOptionSideScreen.IFewOptionSideScreen.Option[] GetOptions();

		// Token: 0x0600B509 RID: 46345
		void OnOptionSelected(FewOptionSideScreen.IFewOptionSideScreen.Option option);

		// Token: 0x0600B50A RID: 46346
		Tag GetSelectedOption();

		// Token: 0x0200290A RID: 10506
		public struct Option
		{
			// Token: 0x0600CE27 RID: 52775 RVA: 0x0041EC60 File Offset: 0x0041CE60
			public Option(Tag tag, string labelText, global::Tuple<Sprite, Color> iconSpriteColorTuple, string tooltipText = "")
			{
				this.tag = tag;
				this.labelText = labelText;
				this.iconSpriteColorTuple = iconSpriteColorTuple;
				this.tooltipText = tooltipText;
			}

			// Token: 0x0400B597 RID: 46487
			public Tag tag;

			// Token: 0x0400B598 RID: 46488
			public string labelText;

			// Token: 0x0400B599 RID: 46489
			public string tooltipText;

			// Token: 0x0400B59A RID: 46490
			public global::Tuple<Sprite, Color> iconSpriteColorTuple;
		}
	}
}
