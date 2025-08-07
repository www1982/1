using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000D7E RID: 3454
public class NotificationScreen_TemporaryActions : KMonoBehaviour
{
	// Token: 0x1700078B RID: 1931
	// (get) Token: 0x06006B7C RID: 27516 RVA: 0x00289DAD File Offset: 0x00287FAD
	// (set) Token: 0x06006B7D RID: 27517 RVA: 0x00289DB4 File Offset: 0x00287FB4
	public static NotificationScreen_TemporaryActions Instance { get; private set; }

	// Token: 0x06006B7E RID: 27518 RVA: 0x00289DBC File Offset: 0x00287FBC
	public static void DestroyInstance()
	{
		NotificationScreen_TemporaryActions.Instance = null;
	}

	// Token: 0x06006B7F RID: 27519 RVA: 0x00289DC4 File Offset: 0x00287FC4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		NotificationScreen_TemporaryActions.Instance = this;
		this.originalRow.gameObject.SetActive(false);
	}

	// Token: 0x06006B80 RID: 27520 RVA: 0x00289DE4 File Offset: 0x00287FE4
	private TemporaryActionRow CreateActionRow()
	{
		TemporaryActionRow temporaryActionRow = Util.KInstantiateUI<TemporaryActionRow>(this.originalRow.gameObject, this.originalRow.transform.parent.gameObject, false);
		temporaryActionRow.gameObject.SetActive(true);
		temporaryActionRow.transform.SetAsLastSibling();
		this.rows.Add(temporaryActionRow);
		return temporaryActionRow;
	}

	// Token: 0x06006B81 RID: 27521 RVA: 0x00289E3C File Offset: 0x0028803C
	private void RemoveRow(TemporaryActionRow row)
	{
		if (this.rows.Contains(row))
		{
			this.rows.Remove(row);
		}
		row.OnRowHidden = null;
		row.gameObject.DeleteObject();
	}

	// Token: 0x06006B82 RID: 27522 RVA: 0x00289E6C File Offset: 0x0028806C
	protected override void OnCleanUp()
	{
		if (this.rows != null)
		{
			foreach (TemporaryActionRow temporaryActionRow in this.rows.ToArray())
			{
				if (temporaryActionRow != null)
				{
					this.RemoveRow(temporaryActionRow);
				}
			}
			this.rows.Clear();
		}
		base.OnCleanUp();
	}

	// Token: 0x06006B83 RID: 27523 RVA: 0x00289EC0 File Offset: 0x002880C0
	public void CreateCameraReturnActionButton(Vector3 positionToReturnTo)
	{
		if (this.cameraReturnRow == null)
		{
			this.cameraReturnRow = this.CreateActionRow();
			this.cameraReturnRow.Setup(UI.TEMPORARY_ACTIONS.CAMERA_RETURN.NAME, UI.TEMPORARY_ACTIONS.CAMERA_RETURN.TOOLTIP, Assets.GetSprite("action_follow_cam"));
			this.cameraReturnRow.gameObject.name = "TemporaryActionRow_CameraReturn";
			this.cameraPositionToReturnTo = positionToReturnTo;
			this.cameraReturnRow.OnRowHidden = new Action<TemporaryActionRow>(this.RemoveRow);
			this.cameraReturnRow.OnRowClicked = new Action<TemporaryActionRow>(this.OnCameraReturnActionButtonClicked);
		}
		this.cameraReturnRow.SetLifetime(10f);
	}

	// Token: 0x06006B84 RID: 27524 RVA: 0x00289F72 File Offset: 0x00288172
	private void OnCameraReturnActionButtonClicked(TemporaryActionRow row)
	{
		if (this.cameraPositionToReturnTo != Vector3.zero)
		{
			GameUtil.FocusCamera(this.cameraPositionToReturnTo, 2f, true, false);
		}
	}

	// Token: 0x04004945 RID: 18757
	public TemporaryActionRow originalRow;

	// Token: 0x04004946 RID: 18758
	private List<TemporaryActionRow> rows = new List<TemporaryActionRow>();

	// Token: 0x04004947 RID: 18759
	private TemporaryActionRow cameraReturnRow;

	// Token: 0x04004948 RID: 18760
	private Vector3 cameraPositionToReturnTo = Vector3.zero;

	// Token: 0x04004949 RID: 18761
	private const float CAMERA_RETURN_BUTTON_LIFETIME = 10f;
}
