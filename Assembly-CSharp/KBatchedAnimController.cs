using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200052B RID: 1323
[DebuggerDisplay("{name} visible={isVisible} suspendUpdates={suspendUpdates} moving={moving}")]
public class KBatchedAnimController : KAnimControllerBase, KAnimConverter.IAnimConverter
{
	// Token: 0x06001CE0 RID: 7392 RVA: 0x0009C0EE File Offset: 0x0009A2EE
	public int GetCurrentFrameIndex()
	{
		return this.curAnimFrameIdx;
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x0009C0F6 File Offset: 0x0009A2F6
	public KBatchedAnimInstanceData GetBatchInstanceData()
	{
		return this.batchInstanceData;
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0009C0FE File Offset: 0x0009A2FE
	// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0009C106 File Offset: 0x0009A306
	protected bool forceRebuild
	{
		get
		{
			return this._forceRebuild;
		}
		set
		{
			this._forceRebuild = value;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0009C10F File Offset: 0x0009A30F
	public bool IsMoving
	{
		get
		{
			return this.moving;
		}
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x0009C118 File Offset: 0x0009A318
	public KBatchedAnimController()
	{
		this.batchInstanceData = new KBatchedAnimInstanceData(this);
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x0009C196 File Offset: 0x0009A396
	public bool IsActive()
	{
		return base.isActiveAndEnabled && this._enabled;
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x0009C1A8 File Offset: 0x0009A3A8
	public bool IsVisible()
	{
		return this.isVisible;
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x0009C1B0 File Offset: 0x0009A3B0
	public Vector4 GetPositionData()
	{
		if (this.getPositionDataFunctionInUse != null)
		{
			return this.getPositionDataFunctionInUse();
		}
		Vector3 position = base.transform.GetPosition();
		Vector3 positionIncludingOffset = base.PositionIncludingOffset;
		return new Vector4(position.x, position.y, positionIncludingOffset.x, positionIncludingOffset.y);
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x0009C204 File Offset: 0x0009A404
	public void SetSymbolScale(KAnimHashedString symbol_name, float scale)
	{
		KAnim.Build.Symbol symbol = KAnimBatchManager.Instance().GetBatchGroupData(this.GetBatchGroupID(false)).GetSymbol(symbol_name);
		if (symbol == null)
		{
			return;
		}
		base.symbolInstanceGpuData.SetSymbolScale(symbol.symbolIndexInSourceBuild, scale);
		this.SuspendUpdates(false);
		this.SetDirty();
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x0009C24C File Offset: 0x0009A44C
	public void SetSymbolTint(KAnimHashedString symbol_name, Color color)
	{
		KAnim.Build.Symbol symbol = KAnimBatchManager.Instance().GetBatchGroupData(this.GetBatchGroupID(false)).GetSymbol(symbol_name);
		if (symbol == null)
		{
			return;
		}
		base.symbolInstanceGpuData.SetSymbolTint(symbol.symbolIndexInSourceBuild, color);
		this.SuspendUpdates(false);
		this.SetDirty();
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x0009C294 File Offset: 0x0009A494
	public Vector2I GetCellXY()
	{
		Vector3 positionIncludingOffset = base.PositionIncludingOffset;
		if (Grid.CellSizeInMeters == 0f)
		{
			return new Vector2I((int)positionIncludingOffset.x, (int)positionIncludingOffset.y);
		}
		return Grid.PosToXY(positionIncludingOffset);
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x0009C2CE File Offset: 0x0009A4CE
	public float GetZ()
	{
		return base.transform.GetPosition().z;
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x0009C2E0 File Offset: 0x0009A4E0
	public string GetName()
	{
		return base.name;
	}

	// Token: 0x06001CEE RID: 7406 RVA: 0x0009C2E8 File Offset: 0x0009A4E8
	public override KAnim.Anim GetAnim(int index)
	{
		if (!this.batchGroupID.IsValid || !(this.batchGroupID != KAnimBatchManager.NO_BATCH))
		{
			global::Debug.LogError(base.name + " batch not ready");
		}
		KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.batchGroupID);
		global::Debug.Assert(batchGroupData != null);
		return batchGroupData.GetAnim(index);
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x0009C34C File Offset: 0x0009A54C
	private void Initialize()
	{
		if (this.batchGroupID.IsValid && this.batchGroupID != KAnimBatchManager.NO_BATCH)
		{
			this.DeRegister();
			this.Register();
		}
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x0009C387 File Offset: 0x0009A587
	private void OnMovementStateChanged(bool is_moving)
	{
		if (is_moving == this.moving)
		{
			return;
		}
		this.moving = is_moving;
		this.SetDirty();
		this.ConfigureUpdateListener();
	}

	// Token: 0x06001CF1 RID: 7409 RVA: 0x0009C3A6 File Offset: 0x0009A5A6
	private static void OnMovementStateChanged(Transform transform, bool is_moving)
	{
		transform.GetComponent<KBatchedAnimController>().OnMovementStateChanged(is_moving);
	}

	// Token: 0x06001CF2 RID: 7410 RVA: 0x0009C3B4 File Offset: 0x0009A5B4
	private void SetBatchGroup(KAnimFileData kafd)
	{
		if (this.batchGroupID.IsValid && kafd != null && this.batchGroupID == kafd.batchTag)
		{
			return;
		}
		DebugUtil.Assert(!this.batchGroupID.IsValid, "Should only be setting the batch group once.");
		DebugUtil.Assert(kafd != null, "Null anim data!! For", base.name);
		base.curBuild = kafd.build;
		DebugUtil.Assert(base.curBuild != null, "Null build for anim!! ", base.name, kafd.name);
		KAnimGroupFile.Group group = KAnimGroupFile.GetGroup(base.curBuild.batchTag);
		HashedString hashedString = kafd.build.batchTag;
		if (group.renderType == KAnimBatchGroup.RendererType.DontRender || group.renderType == KAnimBatchGroup.RendererType.AnimOnly)
		{
			bool isValid = group.swapTarget.IsValid;
			string text = "Invalid swap target fro group [";
			HashedString id = group.id;
			global::Debug.Assert(isValid, text + id.ToString() + "]");
			hashedString = group.swapTarget;
		}
		this.batchGroupID = hashedString;
		base.symbolInstanceGpuData = new SymbolInstanceGpuData(KAnimBatchManager.instance.GetBatchGroupData(this.batchGroupID).maxSymbolsPerBuild);
		base.symbolOverrideInfoGpuData = new SymbolOverrideInfoGpuData(KAnimBatchManager.instance.GetBatchGroupData(this.batchGroupID).symbolFrameInstances.Count);
		if (!this.batchGroupID.IsValid || this.batchGroupID == KAnimBatchManager.NO_BATCH)
		{
			global::Debug.LogError("Batch is not ready: " + base.name);
		}
		if (this.materialType == KAnimBatchGroup.MaterialType.Default && this.batchGroupID == KAnimBatchManager.BATCH_HUMAN)
		{
			this.materialType = KAnimBatchGroup.MaterialType.Human;
		}
	}

	// Token: 0x06001CF3 RID: 7411 RVA: 0x0009C550 File Offset: 0x0009A750
	public void LoadAnims()
	{
		if (!KAnimBatchManager.Instance().isReady)
		{
			global::Debug.LogError("KAnimBatchManager is not ready when loading anim:" + base.name);
		}
		if (this.animFiles.Length == 0)
		{
			DebugUtil.Assert(false, "KBatchedAnimController has no anim files:" + base.name);
		}
		if (!this.animFiles[0].IsBuildLoaded)
		{
			DebugUtil.LogErrorArgs(base.gameObject, new object[] { string.Format("First anim file needs to be the build file but {0} doesn't have an associated build", this.animFiles[0].GetData().name) });
		}
		this.overrideAnims.Clear();
		this.anims.Clear();
		this.SetBatchGroup(this.animFiles[0].GetData());
		for (int i = 0; i < this.animFiles.Length; i++)
		{
			base.AddAnims(this.animFiles[i]);
		}
		this.forceRebuild = true;
		if (this.layering != null)
		{
			this.layering.HideSymbols();
		}
		if (this.usingNewSymbolOverrideSystem)
		{
			DebugUtil.Assert(base.GetComponent<SymbolOverrideController>() != null);
		}
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x0009C65C File Offset: 0x0009A85C
	public void SwapAnims(KAnimFile[] anims)
	{
		if (this.batchGroupID.IsValid)
		{
			this.DeRegister();
			this.batchGroupID = HashedString.Invalid;
		}
		base.AnimFiles = anims;
		this.LoadAnims();
		if (base.curBuild != null)
		{
			this.UpdateHiddenSymbolSet(this.hiddenSymbolsSet);
		}
		this.Register();
	}

	// Token: 0x06001CF5 RID: 7413 RVA: 0x0009C6B4 File Offset: 0x0009A8B4
	public void UpdateAnim(float dt)
	{
		if (this.batch != null && base.transform.hasChanged)
		{
			base.transform.hasChanged = false;
			if (this.batch != null && this.batch.group.maxGroupSize == 1 && this.lastPos.z != base.transform.GetPosition().z)
			{
				this.batch.OverrideZ(base.transform.GetPosition().z);
			}
			Vector3 positionIncludingOffset = base.PositionIncludingOffset;
			this.lastPos = positionIncludingOffset;
			if (this.visibilityType != KAnimControllerBase.VisibilityType.Always && KAnimBatchManager.ControllerToChunkXY(this) != this.lastChunkXY && this.lastChunkXY != KBatchedAnimUpdater.INVALID_CHUNK_ID)
			{
				this.DeRegister();
				this.Register();
			}
			this.SetDirty();
		}
		if (this.batchGroupID == KAnimBatchManager.NO_BATCH || !this.IsActive())
		{
			return;
		}
		if (!this.forceRebuild && (this.mode == KAnim.PlayMode.Paused || this.stopped || this.curAnim == null || (this.mode == KAnim.PlayMode.Once && this.curAnim != null && (this.elapsedTime > this.curAnim.totalTime || this.curAnim.totalTime <= 0f) && this.animQueue.Count == 0)))
		{
			this.SuspendUpdates(true);
		}
		if (!this.isVisible && !this.forceRebuild)
		{
			if (this.visibilityType == KAnimControllerBase.VisibilityType.OffscreenUpdate && !this.stopped && this.mode != KAnim.PlayMode.Paused)
			{
				base.SetElapsedTime(this.elapsedTime + dt * this.playSpeed);
			}
			return;
		}
		this.curAnimFrameIdx = base.GetFrameIdx(this.elapsedTime, true);
		if (this.eventManagerHandle.IsValid() && this.aem != null)
		{
			float elapsedTime = this.aem.GetElapsedTime(this.eventManagerHandle);
			if ((int)((this.elapsedTime - elapsedTime) * 100f) != 0)
			{
				base.UpdateAnimEventSequenceTime();
			}
		}
		this.UpdateFrame(this.elapsedTime);
		if (!this.stopped && this.mode != KAnim.PlayMode.Paused)
		{
			base.SetElapsedTime(this.elapsedTime + dt * this.playSpeed);
		}
		this.forceRebuild = false;
	}

	// Token: 0x06001CF6 RID: 7414 RVA: 0x0009C8DC File Offset: 0x0009AADC
	protected override void UpdateFrame(float t)
	{
		base.previousFrame = base.currentFrame;
		if (!this.stopped || this.forceRebuild)
		{
			if (this.curAnim != null && (this.mode == KAnim.PlayMode.Loop || this.elapsedTime <= base.GetDuration() || this.forceRebuild))
			{
				base.currentFrame = this.curAnim.GetFrameIdx(this.mode, this.elapsedTime);
				if (base.currentFrame != base.previousFrame || this.forceRebuild)
				{
					this.SetDirty();
				}
			}
			else
			{
				this.TriggerStop();
			}
			if (!this.stopped && this.mode == KAnim.PlayMode.Loop && base.currentFrame == 0)
			{
				base.AnimEnter(this.curAnim.hash);
			}
		}
		if (this.synchronizer != null)
		{
			this.synchronizer.SyncTime();
		}
	}

	// Token: 0x06001CF7 RID: 7415 RVA: 0x0009C9AC File Offset: 0x0009ABAC
	public override void TriggerStop()
	{
		if (this.animQueue.Count > 0)
		{
			base.StartQueuedAnim();
			return;
		}
		if (this.curAnim != null && this.mode == KAnim.PlayMode.Once)
		{
			base.currentFrame = this.curAnim.numFrames - 1;
			base.Stop();
			base.gameObject.Trigger(-1061186183, null);
			if (this.destroyOnAnimComplete)
			{
				base.DestroySelf();
			}
		}
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x0009CA18 File Offset: 0x0009AC18
	public override void UpdateHiddenSymbol(KAnimHashedString symbolToUpdate)
	{
		KBatchGroupData batchGroupData = KAnimBatchManager.instance.GetBatchGroupData(this.batchGroupID);
		for (int i = 0; i < batchGroupData.frameElementSymbols.Count; i++)
		{
			if (!(symbolToUpdate != batchGroupData.frameElementSymbols[i].hash))
			{
				KAnim.Build.Symbol symbol = batchGroupData.frameElementSymbols[i];
				bool flag = !this.hiddenSymbolsSet.Contains(symbol.hash);
				base.symbolInstanceGpuData.SetVisible(i, flag);
			}
		}
		this.SetDirty();
	}

	// Token: 0x06001CF9 RID: 7417 RVA: 0x0009CA9C File Offset: 0x0009AC9C
	public override void UpdateHiddenSymbolSet(HashSet<KAnimHashedString> symbolsToUpdate)
	{
		KBatchGroupData batchGroupData = KAnimBatchManager.instance.GetBatchGroupData(this.batchGroupID);
		for (int i = 0; i < batchGroupData.frameElementSymbols.Count; i++)
		{
			if (symbolsToUpdate.Contains(batchGroupData.frameElementSymbols[i].hash))
			{
				KAnim.Build.Symbol symbol = batchGroupData.frameElementSymbols[i];
				bool flag = !this.hiddenSymbolsSet.Contains(symbol.hash);
				base.symbolInstanceGpuData.SetVisible(i, flag);
			}
		}
		this.SetDirty();
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x0009CB20 File Offset: 0x0009AD20
	public override void UpdateAllHiddenSymbols()
	{
		KBatchGroupData batchGroupData = KAnimBatchManager.instance.GetBatchGroupData(this.batchGroupID);
		for (int i = 0; i < batchGroupData.frameElementSymbols.Count; i++)
		{
			KAnim.Build.Symbol symbol = batchGroupData.frameElementSymbols[i];
			bool flag = !this.hiddenSymbolsSet.Contains(symbol.hash);
			base.symbolInstanceGpuData.SetVisible(i, flag);
		}
		this.SetDirty();
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x0009CB89 File Offset: 0x0009AD89
	public int GetMaxVisible()
	{
		return this.maxSymbols;
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0009CB91 File Offset: 0x0009AD91
	// (set) Token: 0x06001CFD RID: 7421 RVA: 0x0009CB99 File Offset: 0x0009AD99
	public HashedString batchGroupID { get; private set; }

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06001CFE RID: 7422 RVA: 0x0009CBA2 File Offset: 0x0009ADA2
	// (set) Token: 0x06001CFF RID: 7423 RVA: 0x0009CBAA File Offset: 0x0009ADAA
	public HashedString batchGroupIDOverride { get; private set; }

	// Token: 0x06001D00 RID: 7424 RVA: 0x0009CBB4 File Offset: 0x0009ADB4
	public HashedString GetBatchGroupID(bool isEditorWindow = false)
	{
		global::Debug.Assert(isEditorWindow || this.animFiles == null || this.animFiles.Length == 0 || (this.batchGroupID.IsValid && this.batchGroupID != KAnimBatchManager.NO_BATCH));
		return this.batchGroupID;
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x0009CC09 File Offset: 0x0009AE09
	public HashedString GetBatchGroupIDOverride()
	{
		return this.batchGroupIDOverride;
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x0009CC11 File Offset: 0x0009AE11
	public void SetBatchGroupOverride(HashedString id)
	{
		this.batchGroupIDOverride = id;
		this.DeRegister();
		this.Register();
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x0009CC26 File Offset: 0x0009AE26
	public int GetLayer()
	{
		return base.gameObject.layer;
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x0009CC33 File Offset: 0x0009AE33
	public KAnimBatch GetBatch()
	{
		return this.batch;
	}

	// Token: 0x06001D05 RID: 7429 RVA: 0x0009CC3C File Offset: 0x0009AE3C
	public void SetBatch(KAnimBatch new_batch)
	{
		this.batch = new_batch;
		if (this.materialType == KAnimBatchGroup.MaterialType.UI)
		{
			KBatchedAnimCanvasRenderer kbatchedAnimCanvasRenderer = base.GetComponent<KBatchedAnimCanvasRenderer>();
			if (kbatchedAnimCanvasRenderer == null && new_batch != null)
			{
				kbatchedAnimCanvasRenderer = base.gameObject.AddComponent<KBatchedAnimCanvasRenderer>();
			}
			if (kbatchedAnimCanvasRenderer != null)
			{
				kbatchedAnimCanvasRenderer.SetBatch(this);
			}
		}
	}

	// Token: 0x06001D06 RID: 7430 RVA: 0x0009CC88 File Offset: 0x0009AE88
	public int GetCurrentNumFrames()
	{
		if (this.curAnim == null)
		{
			return 0;
		}
		return this.curAnim.numFrames;
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x0009CC9F File Offset: 0x0009AE9F
	public int GetFirstFrameIndex()
	{
		if (this.curAnim == null)
		{
			return -1;
		}
		return this.curAnim.firstFrameIdx;
	}

	// Token: 0x06001D08 RID: 7432 RVA: 0x0009CCB8 File Offset: 0x0009AEB8
	private Canvas GetRootCanvas()
	{
		if (this.rt == null)
		{
			return null;
		}
		RectTransform rectTransform = this.rt.parent.GetComponent<RectTransform>();
		while (rectTransform != null)
		{
			Canvas component = rectTransform.GetComponent<Canvas>();
			if (component != null && component.isRootCanvas)
			{
				return component;
			}
			rectTransform = rectTransform.parent.GetComponent<RectTransform>();
		}
		return null;
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x0009CD1C File Offset: 0x0009AF1C
	public override Matrix2x3 GetTransformMatrix()
	{
		Vector3 vector = base.PositionIncludingOffset;
		vector.z = 0f;
		Vector2 vector2 = new Vector2(this.animScale * this.animWidth, -this.animScale * this.animHeight);
		if (this.materialType == KAnimBatchGroup.MaterialType.UI)
		{
			this.rt = base.GetComponent<RectTransform>();
			if (this.rootCanvas == null)
			{
				this.rootCanvas = this.GetRootCanvas();
			}
			if (this.scaler == null && this.rootCanvas != null)
			{
				this.scaler = this.rootCanvas.GetComponent<CanvasScaler>();
			}
			if (this.rootCanvas == null)
			{
				this.screenOffset.x = (float)(Screen.width / 2);
				this.screenOffset.y = (float)(Screen.height / 2);
			}
			else
			{
				this.screenOffset.x = ((this.rootCanvas.renderMode == RenderMode.WorldSpace) ? 0f : (this.rootCanvas.rectTransform().rect.width / 2f));
				this.screenOffset.y = ((this.rootCanvas.renderMode == RenderMode.WorldSpace) ? 0f : (this.rootCanvas.rectTransform().rect.height / 2f));
			}
			float num = 1f;
			if (this.scaler != null)
			{
				num = 1f / this.scaler.scaleFactor;
			}
			vector = (this.rt.localToWorldMatrix.MultiplyPoint(this.rt.pivot) + this.offset) * num - this.screenOffset;
			float num2 = this.animWidth * this.animScale;
			float num3 = this.animHeight * this.animScale;
			if (this.setScaleFromAnim && this.curAnim != null)
			{
				num2 *= this.rt.rect.size.x / this.curAnim.unScaledSize.x;
				num3 *= this.rt.rect.size.y / this.curAnim.unScaledSize.y;
			}
			else
			{
				num2 *= this.rt.rect.size.x / this.animOverrideSize.x;
				num3 *= this.rt.rect.size.y / this.animOverrideSize.y;
			}
			vector2 = new Vector3(this.rt.lossyScale.x * num2 * num, -this.rt.lossyScale.y * num3 * num, this.rt.lossyScale.z * num);
			this.pivot = this.rt.pivot;
		}
		Matrix2x3 matrix2x = Matrix2x3.Scale(vector2);
		Matrix2x3 matrix2x2 = Matrix2x3.Scale(new Vector2(this.flipX ? (-1f) : 1f, this.flipY ? (-1f) : 1f));
		Matrix2x3 matrix2x6;
		if (this.rotation != 0f)
		{
			Matrix2x3 matrix2x3 = Matrix2x3.Translate(-this.pivot);
			Matrix2x3 matrix2x4 = Matrix2x3.Rotate(this.rotation * 0.017453292f);
			Matrix2x3 matrix2x5 = Matrix2x3.Translate(this.pivot) * matrix2x4 * matrix2x3;
			matrix2x6 = Matrix2x3.TRS(vector, base.transform.rotation, base.transform.localScale) * matrix2x5 * matrix2x * this.navMatrix * matrix2x2;
		}
		else
		{
			matrix2x6 = Matrix2x3.TRS(vector, base.transform.rotation, base.transform.localScale) * matrix2x * this.navMatrix * matrix2x2;
		}
		return matrix2x6;
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x0009D13C File Offset: 0x0009B33C
	public Matrix2x3 GetTransformMatrix(Vector2 customScale)
	{
		Vector3 vector = base.PositionIncludingOffset;
		vector.z = 0f;
		Vector2 vector2 = customScale;
		if (this.materialType == KAnimBatchGroup.MaterialType.UI)
		{
			this.rt = base.GetComponent<RectTransform>();
			if (this.rootCanvas == null)
			{
				this.rootCanvas = this.GetRootCanvas();
			}
			if (this.scaler == null && this.rootCanvas != null)
			{
				this.scaler = this.rootCanvas.GetComponent<CanvasScaler>();
			}
			if (this.rootCanvas == null)
			{
				this.screenOffset.x = (float)(Screen.width / 2);
				this.screenOffset.y = (float)(Screen.height / 2);
			}
			else
			{
				this.screenOffset.x = ((this.rootCanvas.renderMode == RenderMode.WorldSpace) ? 0f : (this.rootCanvas.rectTransform().rect.width / 2f));
				this.screenOffset.y = ((this.rootCanvas.renderMode == RenderMode.WorldSpace) ? 0f : (this.rootCanvas.rectTransform().rect.height / 2f));
			}
			float num = 1f;
			if (this.scaler != null)
			{
				num = 1f / this.scaler.scaleFactor;
			}
			vector = (this.rt.localToWorldMatrix.MultiplyPoint(this.rt.pivot) + this.offset) * num - this.screenOffset;
			float num2 = this.animWidth * this.animScale;
			float num3 = this.animHeight * this.animScale;
			if (this.setScaleFromAnim && this.curAnim != null)
			{
				num2 *= this.rt.rect.size.x / this.curAnim.unScaledSize.x;
				num3 *= this.rt.rect.size.y / this.curAnim.unScaledSize.y;
			}
			else
			{
				num2 *= this.rt.rect.size.x / this.animOverrideSize.x;
				num3 *= this.rt.rect.size.y / this.animOverrideSize.y;
			}
			vector2 = new Vector3(this.rt.lossyScale.x * num2 * num, -this.rt.lossyScale.y * num3 * num, this.rt.lossyScale.z * num);
			this.pivot = this.rt.pivot;
		}
		Matrix2x3 matrix2x = Matrix2x3.Scale(vector2);
		Matrix2x3 matrix2x2 = Matrix2x3.Scale(new Vector2(this.flipX ? (-1f) : 1f, this.flipY ? (-1f) : 1f));
		Matrix2x3 matrix2x6;
		if (this.rotation != 0f)
		{
			Matrix2x3 matrix2x3 = Matrix2x3.Translate(-this.pivot);
			Matrix2x3 matrix2x4 = Matrix2x3.Rotate(this.rotation * 0.017453292f);
			Matrix2x3 matrix2x5 = Matrix2x3.Translate(this.pivot) * matrix2x4 * matrix2x3;
			matrix2x6 = Matrix2x3.TRS(vector, base.transform.rotation, base.transform.localScale) * matrix2x5 * matrix2x * this.navMatrix * matrix2x2;
		}
		else
		{
			matrix2x6 = Matrix2x3.TRS(vector, base.transform.rotation, base.transform.localScale) * matrix2x * this.navMatrix * matrix2x2;
		}
		return matrix2x6;
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x0009D53C File Offset: 0x0009B73C
	public override Matrix4x4 GetSymbolTransform(HashedString symbol, out bool symbolVisible)
	{
		if (this.curAnimFrameIdx != -1 && this.batch != null)
		{
			Matrix2x3 symbolLocalTransform = this.GetSymbolLocalTransform(symbol, out symbolVisible);
			if (symbolVisible)
			{
				return this.GetTransformMatrix() * symbolLocalTransform;
			}
		}
		symbolVisible = false;
		return default(Matrix4x4);
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x0009D58C File Offset: 0x0009B78C
	public override Matrix2x3 GetSymbolLocalTransform(HashedString symbol, out bool symbolVisible)
	{
		KAnim.Anim.Frame frame;
		if (this.curAnimFrameIdx != -1 && this.batch != null && this.batch.group.data.TryGetFrame(this.curAnimFrameIdx, out frame))
		{
			for (int i = 0; i < frame.numElements; i++)
			{
				int num = frame.firstElementIdx + i;
				if (num < this.batch.group.data.frameElements.Count)
				{
					KAnim.Anim.FrameElement frameElement = this.batch.group.data.frameElements[num];
					if (frameElement.symbol == symbol)
					{
						symbolVisible = true;
						return frameElement.transform;
					}
				}
			}
		}
		symbolVisible = false;
		return Matrix2x3.identity;
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x0009D642 File Offset: 0x0009B842
	public override void SetLayer(int layer)
	{
		if (layer == base.gameObject.layer)
		{
			return;
		}
		base.SetLayer(layer);
		this.DeRegister();
		base.gameObject.layer = layer;
		this.Register();
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x0009D672 File Offset: 0x0009B872
	public override void SetDirty()
	{
		if (this.batch != null)
		{
			this.batch.SetDirty(this);
		}
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x0009D688 File Offset: 0x0009B888
	protected override void OnStartQueuedAnim()
	{
		this.SuspendUpdates(false);
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x0009D694 File Offset: 0x0009B894
	protected override void OnAwake()
	{
		this.LoadAnims();
		if (this.visibilityType == KAnimControllerBase.VisibilityType.Default)
		{
			this.visibilityType = ((this.materialType == KAnimBatchGroup.MaterialType.UI) ? KAnimControllerBase.VisibilityType.Always : this.visibilityType);
		}
		if (this.materialType == KAnimBatchGroup.MaterialType.Default && this.batchGroupID == KAnimBatchManager.BATCH_HUMAN)
		{
			this.materialType = KAnimBatchGroup.MaterialType.Human;
		}
		this.symbolOverrideController = base.GetComponent<SymbolOverrideController>();
		this.UpdateAllHiddenSymbols();
		this.hasEnableRun = false;
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x0009D704 File Offset: 0x0009B904
	protected override void OnStart()
	{
		if (this.batch == null)
		{
			this.Initialize();
		}
		if (this.visibilityType == KAnimControllerBase.VisibilityType.Always || this.visibilityType == KAnimControllerBase.VisibilityType.OffscreenUpdate)
		{
			this.ConfigureUpdateListener();
		}
		CellChangeMonitor instance = Singleton<CellChangeMonitor>.Instance;
		if (instance != null)
		{
			instance.RegisterMovementStateChanged(base.transform, new Action<Transform, bool>(KBatchedAnimController.OnMovementStateChanged));
			this.moving = instance.IsMoving(base.transform);
		}
		this.symbolOverrideController = base.GetComponent<SymbolOverrideController>();
		this.SetDirty();
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x0009D77C File Offset: 0x0009B97C
	protected override void OnStop()
	{
		this.SetDirty();
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x0009D784 File Offset: 0x0009B984
	private void OnEnable()
	{
		if (this._enabled)
		{
			this.Enable();
		}
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x0009D794 File Offset: 0x0009B994
	protected override void Enable()
	{
		if (this.hasEnableRun)
		{
			return;
		}
		this.hasEnableRun = true;
		if (this.batch == null)
		{
			this.Initialize();
		}
		this.SetDirty();
		this.SuspendUpdates(false);
		this.ConfigureVisibilityListener(true);
		if (!this.stopped && this.curAnim != null && this.mode != KAnim.PlayMode.Paused && !this.eventManagerHandle.IsValid())
		{
			base.StartAnimEventSequence();
		}
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x0009D7FF File Offset: 0x0009B9FF
	private void OnDisable()
	{
		this.Disable();
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x0009D808 File Offset: 0x0009BA08
	protected override void Disable()
	{
		if (App.IsExiting || KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		if (!this.hasEnableRun)
		{
			return;
		}
		this.hasEnableRun = false;
		this.SuspendUpdates(true);
		if (this.batch != null)
		{
			this.DeRegister();
		}
		this.ConfigureVisibilityListener(false);
		base.StopAnimEventSequence();
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x0009D858 File Offset: 0x0009BA58
	protected override void OnDestroy()
	{
		if (App.IsExiting)
		{
			return;
		}
		CellChangeMonitor instance = Singleton<CellChangeMonitor>.Instance;
		if (instance != null)
		{
			instance.UnregisterMovementStateChanged(base.transform, new Action<Transform, bool>(KBatchedAnimController.OnMovementStateChanged));
		}
		KBatchedAnimUpdater instance2 = Singleton<KBatchedAnimUpdater>.Instance;
		if (instance2 != null)
		{
			instance2.UpdateUnregister(this);
		}
		this.isVisible = false;
		this.DeRegister();
		this.stopped = true;
		base.StopAnimEventSequence();
		this.batchInstanceData = null;
		this.batch = null;
		base.OnDestroy();
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x0009D8CC File Offset: 0x0009BACC
	public void SetBlendValue(float value)
	{
		this.batchInstanceData.SetBlend(value);
		this.SetDirty();
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x0009D8E0 File Offset: 0x0009BAE0
	public SymbolOverrideController SetupSymbolOverriding()
	{
		if (!this.symbolOverrideController.IsNullOrDestroyed())
		{
			return this.symbolOverrideController;
		}
		this.usingNewSymbolOverrideSystem = true;
		this.symbolOverrideController = SymbolOverrideControllerUtil.AddToPrefab(base.gameObject);
		return this.symbolOverrideController;
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x0009D914 File Offset: 0x0009BB14
	public bool ApplySymbolOverrides()
	{
		this.batch.atlases.Apply(this.batch.matProperties);
		if (this.symbolOverrideController != null)
		{
			if (this.symbolOverrideControllerVersion != this.symbolOverrideController.version || this.symbolOverrideController.applySymbolOverridesEveryFrame)
			{
				this.symbolOverrideControllerVersion = this.symbolOverrideController.version;
				this.symbolOverrideController.ApplyOverrides();
			}
			this.symbolOverrideController.ApplyAtlases();
			return true;
		}
		return false;
	}

	// Token: 0x06001D1B RID: 7451 RVA: 0x0009D994 File Offset: 0x0009BB94
	public void SetSymbolOverrides(int symbol_start_idx, int symbol_num_frames, int atlas_idx, KBatchGroupData source_data, int source_start_idx, int source_num_frames)
	{
		base.symbolOverrideInfoGpuData.SetSymbolOverrideInfo(symbol_start_idx, symbol_num_frames, atlas_idx, source_data, source_start_idx, source_num_frames);
	}

	// Token: 0x06001D1C RID: 7452 RVA: 0x0009D9AA File Offset: 0x0009BBAA
	public void SetSymbolOverride(int symbol_idx, ref KAnim.Build.SymbolFrameInstance symbol_frame_instance)
	{
		base.symbolOverrideInfoGpuData.SetSymbolOverrideInfo(symbol_idx, ref symbol_frame_instance);
	}

	// Token: 0x06001D1D RID: 7453 RVA: 0x0009D9BC File Offset: 0x0009BBBC
	protected override void Register()
	{
		if (!this.IsActive())
		{
			return;
		}
		if (this.batch != null)
		{
			return;
		}
		if (this.batchGroupID.IsValid && this.batchGroupID != KAnimBatchManager.NO_BATCH)
		{
			this.lastChunkXY = KAnimBatchManager.ControllerToChunkXY(this);
			KAnimBatchManager.Instance().Register(this);
			this.forceRebuild = true;
			this.SetDirty();
		}
	}

	// Token: 0x06001D1E RID: 7454 RVA: 0x0009DA21 File Offset: 0x0009BC21
	protected override void DeRegister()
	{
		if (this.batch != null)
		{
			this.batch.Deregister(this);
		}
	}

	// Token: 0x06001D1F RID: 7455 RVA: 0x0009DA38 File Offset: 0x0009BC38
	private void ConfigureUpdateListener()
	{
		if ((this.IsActive() && !this.suspendUpdates && this.isVisible) || this.moving || this.visibilityType == KAnimControllerBase.VisibilityType.OffscreenUpdate || this.visibilityType == KAnimControllerBase.VisibilityType.Always)
		{
			Singleton<KBatchedAnimUpdater>.Instance.UpdateRegister(this);
			return;
		}
		Singleton<KBatchedAnimUpdater>.Instance.UpdateUnregister(this);
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x0009DA93 File Offset: 0x0009BC93
	protected override void SuspendUpdates(bool suspend)
	{
		this.suspendUpdates = suspend;
		this.ConfigureUpdateListener();
	}

	// Token: 0x06001D21 RID: 7457 RVA: 0x0009DAA2 File Offset: 0x0009BCA2
	public void SetVisiblity(bool is_visible)
	{
		if (is_visible != this.isVisible)
		{
			this.isVisible = is_visible;
			if (is_visible)
			{
				this.SuspendUpdates(false);
				this.SetDirty();
				base.UpdateAnimEventSequenceTime();
				return;
			}
			this.SuspendUpdates(true);
			this.SetDirty();
		}
	}

	// Token: 0x06001D22 RID: 7458 RVA: 0x0009DAD8 File Offset: 0x0009BCD8
	private void ConfigureVisibilityListener(bool enabled)
	{
		if (this.visibilityType == KAnimControllerBase.VisibilityType.Always || this.visibilityType == KAnimControllerBase.VisibilityType.OffscreenUpdate)
		{
			return;
		}
		if (enabled)
		{
			this.RegisterVisibilityListener();
			return;
		}
		this.UnregisterVisibilityListener();
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x0009DAFD File Offset: 0x0009BCFD
	public virtual KAnimConverter.PostProcessingEffects GetPostProcessingEffectsCompatibility()
	{
		return this.postProcessingEffectsAllowed;
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x0009DB05 File Offset: 0x0009BD05
	public float GetPostProcessingParams()
	{
		return this.postProcessingParameters;
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x0009DB0D File Offset: 0x0009BD0D
	protected override void RefreshVisibilityListener()
	{
		if (!this.visibilityListenerRegistered)
		{
			return;
		}
		this.ConfigureVisibilityListener(false);
		this.ConfigureVisibilityListener(true);
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x0009DB26 File Offset: 0x0009BD26
	private void RegisterVisibilityListener()
	{
		DebugUtil.Assert(!this.visibilityListenerRegistered);
		Singleton<KBatchedAnimUpdater>.Instance.VisibilityRegister(this);
		this.visibilityListenerRegistered = true;
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x0009DB48 File Offset: 0x0009BD48
	private void UnregisterVisibilityListener()
	{
		DebugUtil.Assert(this.visibilityListenerRegistered);
		Singleton<KBatchedAnimUpdater>.Instance.VisibilityUnregister(this);
		this.visibilityListenerRegistered = false;
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x0009DB68 File Offset: 0x0009BD68
	public void SetSceneLayer(Grid.SceneLayer layer)
	{
		float layerZ = Grid.GetLayerZ(layer);
		this.sceneLayer = layer;
		Vector3 position = base.transform.GetPosition();
		position.z = layerZ;
		base.transform.SetPosition(position);
		this.DeRegister();
		this.Register();
	}

	// Token: 0x040010E0 RID: 4320
	[NonSerialized]
	protected bool _forceRebuild;

	// Token: 0x040010E1 RID: 4321
	private Vector3 lastPos = Vector3.zero;

	// Token: 0x040010E2 RID: 4322
	private Vector2I lastChunkXY = KBatchedAnimUpdater.INVALID_CHUNK_ID;

	// Token: 0x040010E3 RID: 4323
	private KAnimBatch batch;

	// Token: 0x040010E4 RID: 4324
	public float animScale = 0.005f;

	// Token: 0x040010E5 RID: 4325
	private bool suspendUpdates;

	// Token: 0x040010E6 RID: 4326
	private bool visibilityListenerRegistered;

	// Token: 0x040010E7 RID: 4327
	private bool moving;

	// Token: 0x040010E8 RID: 4328
	private SymbolOverrideController symbolOverrideController;

	// Token: 0x040010E9 RID: 4329
	private int symbolOverrideControllerVersion;

	// Token: 0x040010EA RID: 4330
	[NonSerialized]
	public KBatchedAnimUpdater.RegistrationState updateRegistrationState = KBatchedAnimUpdater.RegistrationState.Unregistered;

	// Token: 0x040010EB RID: 4331
	public Grid.SceneLayer sceneLayer;

	// Token: 0x040010EC RID: 4332
	private RectTransform rt;

	// Token: 0x040010ED RID: 4333
	private Vector3 screenOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x040010EE RID: 4334
	public Matrix2x3 navMatrix = Matrix2x3.identity;

	// Token: 0x040010EF RID: 4335
	private CanvasScaler scaler;

	// Token: 0x040010F0 RID: 4336
	public bool setScaleFromAnim = true;

	// Token: 0x040010F1 RID: 4337
	public Vector2 animOverrideSize = Vector2.one;

	// Token: 0x040010F2 RID: 4338
	private Canvas rootCanvas;

	// Token: 0x040010F3 RID: 4339
	public bool isMovable;

	// Token: 0x040010F4 RID: 4340
	public Func<Vector4> getPositionDataFunctionInUse;

	// Token: 0x040010F5 RID: 4341
	public KAnimConverter.PostProcessingEffects postProcessingEffectsAllowed;

	// Token: 0x040010F6 RID: 4342
	public float postProcessingParameters;
}
