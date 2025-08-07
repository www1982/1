using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x02000E0D RID: 3597
public class MonumentSideScreen : SideScreenContent
{
	// Token: 0x0600718E RID: 29070 RVA: 0x002B309C File Offset: 0x002B129C
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<MonumentPart>() != null;
	}

	// Token: 0x0600718F RID: 29071 RVA: 0x002B30AC File Offset: 0x002B12AC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.debugVictoryButton.onClick += delegate
		{
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.Thriving.Id);
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.Clothe8Dupes.Id);
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.Build4NatureReserves.Id);
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.ReachedSpace.Id);
			GameScheduler.Instance.Schedule("ForceCheckAchievements", 0.1f, delegate(object data)
			{
				Game.Instance.Trigger(395452326, null);
			}, null, null);
		};
		this.debugVictoryButton.gameObject.SetActive(DebugHandler.InstantBuildMode && this.target.part == MonumentPartResource.Part.Top);
		this.flipButton.onClick += delegate
		{
			this.target.GetComponent<Rotatable>().Rotate();
		};
	}

	// Token: 0x06007190 RID: 29072 RVA: 0x002B3128 File Offset: 0x002B1328
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.target = target.GetComponent<MonumentPart>();
		this.debugVictoryButton.gameObject.SetActive(DebugHandler.InstantBuildMode && this.target.part == MonumentPartResource.Part.Top);
		this.GenerateStateButtons();
	}

	// Token: 0x06007191 RID: 29073 RVA: 0x002B3178 File Offset: 0x002B1378
	public void GenerateStateButtons()
	{
		for (int i = this.buttons.Count - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.buttons[i]);
		}
		this.buttons.Clear();
		using (List<MonumentPartResource>.Enumerator enumerator = Db.GetMonumentParts().GetParts(this.target.part).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MonumentPartResource state = enumerator.Current;
				GameObject gameObject = Util.KInstantiateUI(this.stateButtonPrefab, this.buttonContainer.gameObject, true);
				string state2 = state.State;
				string symbolName = state.SymbolName;
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.target.SetState(state.Id);
				};
				this.buttons.Add(gameObject);
				gameObject.GetComponent<KButton>().fgImage.sprite = Def.GetUISpriteFromMultiObjectAnim(state.AnimFile, state2, false, symbolName);
			}
		}
	}

	// Token: 0x04004E27 RID: 20007
	private MonumentPart target;

	// Token: 0x04004E28 RID: 20008
	public KButton debugVictoryButton;

	// Token: 0x04004E29 RID: 20009
	public KButton flipButton;

	// Token: 0x04004E2A RID: 20010
	public GameObject stateButtonPrefab;

	// Token: 0x04004E2B RID: 20011
	private List<GameObject> buttons = new List<GameObject>();

	// Token: 0x04004E2C RID: 20012
	[SerializeField]
	private RectTransform buttonContainer;
}
