using System;
using UnityEngine;

// Token: 0x02000AFA RID: 2810
[AddComponentMenu("KMonoBehaviour/scripts/ScreenPrefabs")]
public class ScreenPrefabs : KMonoBehaviour
{
	// Token: 0x170005CD RID: 1485
	// (get) Token: 0x060052B1 RID: 21169 RVA: 0x001E20A9 File Offset: 0x001E02A9
	// (set) Token: 0x060052B2 RID: 21170 RVA: 0x001E20B0 File Offset: 0x001E02B0
	public static ScreenPrefabs Instance { get; private set; }

	// Token: 0x060052B3 RID: 21171 RVA: 0x001E20B8 File Offset: 0x001E02B8
	protected override void OnPrefabInit()
	{
		ScreenPrefabs.Instance = this;
	}

	// Token: 0x060052B4 RID: 21172 RVA: 0x001E20C0 File Offset: 0x001E02C0
	public void ConfirmDoAction(string message, global::System.Action action, Transform parent)
	{
		((ConfirmDialogScreen)KScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, parent.gameObject)).PopupConfirmDialog(message, action, delegate
		{
		}, null, null, null, null, null, null);
	}

	// Token: 0x04003789 RID: 14217
	public ControlsScreen ControlsScreen;

	// Token: 0x0400378A RID: 14218
	public Hud HudScreen;

	// Token: 0x0400378B RID: 14219
	public HoverTextScreen HoverTextScreen;

	// Token: 0x0400378C RID: 14220
	public OverlayScreen OverlayScreen;

	// Token: 0x0400378D RID: 14221
	public TileScreen TileScreen;

	// Token: 0x0400378E RID: 14222
	public SpeedControlScreen SpeedControlScreen;

	// Token: 0x0400378F RID: 14223
	public ManagementMenu ManagementMenu;

	// Token: 0x04003790 RID: 14224
	public ToolTipScreen ToolTipScreen;

	// Token: 0x04003791 RID: 14225
	public DebugPaintElementScreen DebugPaintElementScreen;

	// Token: 0x04003792 RID: 14226
	public UserMenuScreen UserMenuScreen;

	// Token: 0x04003793 RID: 14227
	public KButtonMenu OwnerScreen;

	// Token: 0x04003794 RID: 14228
	public KButtonMenu ButtonGrid;

	// Token: 0x04003795 RID: 14229
	public NameDisplayScreen NameDisplayScreen;

	// Token: 0x04003796 RID: 14230
	public ConfirmDialogScreen ConfirmDialogScreen;

	// Token: 0x04003797 RID: 14231
	public CustomizableDialogScreen CustomizableDialogScreen;

	// Token: 0x04003798 RID: 14232
	public SpriteListDialogScreen SpriteListDialogScreen;

	// Token: 0x04003799 RID: 14233
	public InfoDialogScreen InfoDialogScreen;

	// Token: 0x0400379A RID: 14234
	public StoryMessageScreen StoryMessageScreen;

	// Token: 0x0400379B RID: 14235
	public SubSpeciesInfoScreen SubSpeciesInfoScreen;

	// Token: 0x0400379C RID: 14236
	public EventInfoScreen eventInfoScreen;

	// Token: 0x0400379D RID: 14237
	public FileNameDialog FileNameDialog;

	// Token: 0x0400379E RID: 14238
	public TagFilterScreen TagFilterScreen;

	// Token: 0x0400379F RID: 14239
	public ResearchScreen ResearchScreen;

	// Token: 0x040037A0 RID: 14240
	public MessageDialogFrame MessageDialogFrame;

	// Token: 0x040037A1 RID: 14241
	public ResourceCategoryScreen ResourceCategoryScreen;

	// Token: 0x040037A2 RID: 14242
	public ColonyDiagnosticScreen ColonyDiagnosticScreen;

	// Token: 0x040037A3 RID: 14243
	public LanguageOptionsScreen languageOptionsScreen;

	// Token: 0x040037A4 RID: 14244
	public LargeImpactorSequenceUIReticle largeImpactorSequenceReticlePrefab;

	// Token: 0x040037A5 RID: 14245
	public ModsScreen modsMenu;

	// Token: 0x040037A6 RID: 14246
	public RailModUploadScreen RailModUploadMenu;

	// Token: 0x040037A7 RID: 14247
	public GameObject GameOverScreen;

	// Token: 0x040037A8 RID: 14248
	public GameObject VictoryScreen;

	// Token: 0x040037A9 RID: 14249
	public GameObject StatusItemIndicatorScreen;

	// Token: 0x040037AA RID: 14250
	public GameObject CollapsableContentPanel;

	// Token: 0x040037AB RID: 14251
	public GameObject DescriptionLabel;

	// Token: 0x040037AC RID: 14252
	public LoadingOverlay loadingOverlay;

	// Token: 0x040037AD RID: 14253
	public LoadScreen LoadScreen;

	// Token: 0x040037AE RID: 14254
	public InspectSaveScreen InspectSaveScreen;

	// Token: 0x040037AF RID: 14255
	public OptionsMenuScreen OptionsScreen;

	// Token: 0x040037B0 RID: 14256
	public WorldGenScreen WorldGenScreen;

	// Token: 0x040037B1 RID: 14257
	public ModeSelectScreen ModeSelectScreen;

	// Token: 0x040037B2 RID: 14258
	public ColonyDestinationSelectScreen ColonyDestinationSelectScreen;

	// Token: 0x040037B3 RID: 14259
	public RetiredColonyInfoScreen RetiredColonyInfoScreen;

	// Token: 0x040037B4 RID: 14260
	public VideoScreen VideoScreen;

	// Token: 0x040037B5 RID: 14261
	public ComicViewer ComicViewer;

	// Token: 0x040037B6 RID: 14262
	public GameObject OldVersionWarningScreen;

	// Token: 0x040037B7 RID: 14263
	public GameObject DLCBetaWarningScreen;

	// Token: 0x040037B8 RID: 14264
	[Header("Klei Items")]
	public GameObject KleiItemDropScreen;

	// Token: 0x040037B9 RID: 14265
	public GameObject LockerMenuScreen;

	// Token: 0x040037BA RID: 14266
	public GameObject LockerNavigator;

	// Token: 0x040037BB RID: 14267
	[Header("Main Menu")]
	public GameObject MainMenuForVanilla;

	// Token: 0x040037BC RID: 14268
	public GameObject MainMenuForSpacedOut;

	// Token: 0x040037BD RID: 14269
	public GameObject MainMenuIntroShort;

	// Token: 0x040037BE RID: 14270
	public GameObject MainMenuHealthyGameMessage;
}
