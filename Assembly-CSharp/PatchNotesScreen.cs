using System;
using UnityEngine;

// Token: 0x02000C2F RID: 3119
public class PatchNotesScreen : KModalScreen
{
	// Token: 0x06005EC9 RID: 24265 RVA: 0x0022C2B0 File Offset: 0x0022A4B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.changesLabel.text = PatchNotesScreen.m_patchNotesText;
		this.closeButton.onClick += this.MarkAsReadAndClose;
		this.closeButton.soundPlayer.widget_sound_events()[0].OverrideAssetName = "HUD_Click_Close";
		this.okButton.onClick += this.MarkAsReadAndClose;
		this.previousVersion.onClick += delegate
		{
			App.OpenWebURL("http://support.kleientertainment.com/customer/portal/articles/2776550");
		};
		this.fullPatchNotes.onClick += this.OnPatchNotesClick;
		PatchNotesScreen.instance = this;
	}

	// Token: 0x06005ECA RID: 24266 RVA: 0x0022C368 File Offset: 0x0022A568
	protected override void OnCleanUp()
	{
		PatchNotesScreen.instance = null;
	}

	// Token: 0x06005ECB RID: 24267 RVA: 0x0022C370 File Offset: 0x0022A570
	public static bool ShouldShowScreen()
	{
		return false;
	}

	// Token: 0x06005ECC RID: 24268 RVA: 0x0022C373 File Offset: 0x0022A573
	private void MarkAsReadAndClose()
	{
		KPlayerPrefs.SetInt("PatchNotesVersion", PatchNotesScreen.PatchNotesVersion);
		this.Deactivate();
	}

	// Token: 0x06005ECD RID: 24269 RVA: 0x0022C38A File Offset: 0x0022A58A
	public static void UpdatePatchNotes(string patchNotesSummary, string url)
	{
		PatchNotesScreen.m_patchNotesUrl = url;
		PatchNotesScreen.m_patchNotesText = patchNotesSummary;
		if (PatchNotesScreen.instance != null)
		{
			PatchNotesScreen.instance.changesLabel.text = PatchNotesScreen.m_patchNotesText;
		}
	}

	// Token: 0x06005ECE RID: 24270 RVA: 0x0022C3B9 File Offset: 0x0022A5B9
	private void OnPatchNotesClick()
	{
		App.OpenWebURL(PatchNotesScreen.m_patchNotesUrl);
	}

	// Token: 0x06005ECF RID: 24271 RVA: 0x0022C3C5 File Offset: 0x0022A5C5
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.MarkAsReadAndClose();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x04003F38 RID: 16184
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003F39 RID: 16185
	[SerializeField]
	private KButton okButton;

	// Token: 0x04003F3A RID: 16186
	[SerializeField]
	private KButton fullPatchNotes;

	// Token: 0x04003F3B RID: 16187
	[SerializeField]
	private KButton previousVersion;

	// Token: 0x04003F3C RID: 16188
	[SerializeField]
	private LocText changesLabel;

	// Token: 0x04003F3D RID: 16189
	private static string m_patchNotesUrl;

	// Token: 0x04003F3E RID: 16190
	private static string m_patchNotesText;

	// Token: 0x04003F3F RID: 16191
	private static int PatchNotesVersion = 9;

	// Token: 0x04003F40 RID: 16192
	private static PatchNotesScreen instance;
}
