using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000B7B RID: 2939
public class ClusterMapVisualizer : KMonoBehaviour
{
	// Token: 0x060057ED RID: 22509 RVA: 0x001FD8D8 File Offset: 0x001FBAD8
	public void Init(ClusterGridEntity entity, ClusterMapPathDrawer pathDrawer)
	{
		this.entity = entity;
		this.pathDrawer = pathDrawer;
		this.animControllers = new List<KBatchedAnimController>();
		if (this.animContainer == null)
		{
			GameObject gameObject = new GameObject("AnimContainer", new Type[] { typeof(RectTransform) });
			RectTransform component = base.GetComponent<RectTransform>();
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.SetParent(component, false);
			component2.SetLocalPosition(new Vector3(0f, 0f, 0f));
			component2.sizeDelta = component.sizeDelta;
			component2.localScale = Vector3.one;
			this.animContainer = component2;
		}
		Vector3 position = ClusterGrid.Instance.GetPosition(entity);
		this.rectTransform().SetLocalPosition(position);
		this.RefreshPathDrawing();
		entity.Subscribe(543433792, new Action<object>(this.OnClusterDestinationChanged));
	}

	// Token: 0x060057EE RID: 22510 RVA: 0x001FD9AE File Offset: 0x001FBBAE
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.doesTransitionAnimation)
		{
			new ClusterMapTravelAnimator.StatesInstance(this, this.entity).StartSM();
		}
	}

	// Token: 0x060057EF RID: 22511 RVA: 0x001FD9D0 File Offset: 0x001FBBD0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.entity != null)
		{
			if (this.doesTransitionAnimation)
			{
				base.gameObject.GetSMI<ClusterMapTravelAnimator.StatesInstance>().keepRotationOnIdle = this.entity.KeepRotationWhenSpacingOutInHex();
			}
			if (this.entity is Clustercraft)
			{
				new ClusterMapRocketAnimator.StatesInstance(this, this.entity).StartSM();
				return;
			}
			if (this.entity is ClusterMapLongRangeMissileGridEntity)
			{
				new ClusterMapLongRangeMissileAnimator.StatesInstance(this, this.entity).StartSM();
				return;
			}
			if (this.entity is BallisticClusterGridEntity)
			{
				new ClusterMapBallisticAnimator.StatesInstance(this, this.entity).StartSM();
				return;
			}
			if (this.entity.Layer == EntityLayer.FX)
			{
				new ClusterMapFXAnimator.StatesInstance(this, this.entity).StartSM();
			}
		}
	}

	// Token: 0x060057F0 RID: 22512 RVA: 0x001FDA94 File Offset: 0x001FBC94
	protected override void OnCleanUp()
	{
		if (this.mapPath != null)
		{
			global::Util.KDestroyGameObject(this.mapPath);
		}
		if (this.entity != null)
		{
			this.entity.Unsubscribe(543433792, new Action<object>(this.OnClusterDestinationChanged));
		}
		base.OnCleanUp();
	}

	// Token: 0x060057F1 RID: 22513 RVA: 0x001FDAEA File Offset: 0x001FBCEA
	private void OnClusterDestinationChanged(object data)
	{
		this.RefreshPathDrawing();
	}

	// Token: 0x060057F2 RID: 22514 RVA: 0x001FDAF4 File Offset: 0x001FBCF4
	public void Select(bool selected)
	{
		if (this.animControllers == null || this.animControllers.Count == 0)
		{
			return;
		}
		if (!selected == this.isSelected)
		{
			this.isSelected = selected;
			this.RefreshPathDrawing();
		}
		this.GetFirstAnimController().SetSymbolVisiblity("selected", selected);
	}

	// Token: 0x060057F3 RID: 22515 RVA: 0x001FDB46 File Offset: 0x001FBD46
	public void PlayAnim(string animName, KAnim.PlayMode playMode)
	{
		if (this.animControllers.Count > 0)
		{
			this.GetFirstAnimController().Play(animName, playMode, 1f, 0f);
		}
	}

	// Token: 0x060057F4 RID: 22516 RVA: 0x001FDB72 File Offset: 0x001FBD72
	public KBatchedAnimController GetFirstAnimController()
	{
		return this.GetAnimController(0);
	}

	// Token: 0x060057F5 RID: 22517 RVA: 0x001FDB7B File Offset: 0x001FBD7B
	public KBatchedAnimController GetAnimController(int index)
	{
		if (index < this.animControllers.Count)
		{
			return this.animControllers[index];
		}
		return null;
	}

	// Token: 0x060057F6 RID: 22518 RVA: 0x001FDB99 File Offset: 0x001FBD99
	public void ManualAddAnimController(KBatchedAnimController externalAnimController)
	{
		this.animControllers.Add(externalAnimController);
	}

	// Token: 0x060057F7 RID: 22519 RVA: 0x001FDBA8 File Offset: 0x001FBDA8
	public void Show(ClusterRevealLevel level)
	{
		if (!this.entity.IsVisible)
		{
			level = ClusterRevealLevel.Hidden;
		}
		if (level == this.lastRevealLevel)
		{
			return;
		}
		this.lastRevealLevel = level;
		switch (level)
		{
		case ClusterRevealLevel.Hidden:
			base.gameObject.SetActive(false);
			break;
		case ClusterRevealLevel.Peeked:
		{
			this.ClearAnimControllers();
			KBatchedAnimController kbatchedAnimController = global::UnityEngine.Object.Instantiate<KBatchedAnimController>(this.peekControllerPrefab, this.animContainer);
			kbatchedAnimController.gameObject.SetActive(true);
			this.animControllers.Add(kbatchedAnimController);
			base.gameObject.SetActive(true);
			break;
		}
		case ClusterRevealLevel.Visible:
			this.ClearAnimControllers();
			if (this.animControllerPrefab != null && this.entity.AnimConfigs != null)
			{
				foreach (ClusterGridEntity.AnimConfig animConfig in this.entity.AnimConfigs)
				{
					KBatchedAnimController kbatchedAnimController2 = global::UnityEngine.Object.Instantiate<KBatchedAnimController>(this.animControllerPrefab, this.animContainer);
					kbatchedAnimController2.AnimFiles = new KAnimFile[] { animConfig.animFile };
					kbatchedAnimController2.initialMode = animConfig.playMode;
					kbatchedAnimController2.initialAnim = animConfig.initialAnim;
					kbatchedAnimController2.Offset = animConfig.animOffset;
					kbatchedAnimController2.gameObject.AddComponent<LoopingSounds>();
					if (animConfig.animPlaySpeedModifier != 0f)
					{
						kbatchedAnimController2.PlaySpeedMultiplier = animConfig.animPlaySpeedModifier;
					}
					if (!string.IsNullOrEmpty(animConfig.symbolSwapTarget) && !string.IsNullOrEmpty(animConfig.symbolSwapSymbol))
					{
						SymbolOverrideController component = kbatchedAnimController2.GetComponent<SymbolOverrideController>();
						KAnim.Build.Symbol symbol = kbatchedAnimController2.AnimFiles[0].GetData().build.GetSymbol(animConfig.symbolSwapSymbol);
						component.AddSymbolOverride(animConfig.symbolSwapTarget, symbol, 0);
					}
					kbatchedAnimController2.gameObject.SetActive(true);
					this.animControllers.Add(kbatchedAnimController2);
				}
			}
			base.gameObject.SetActive(true);
			break;
		}
		this.entity.OnClusterMapIconShown(level);
	}

	// Token: 0x060057F8 RID: 22520 RVA: 0x001FDDA8 File Offset: 0x001FBFA8
	public void RefreshPathDrawing()
	{
		if (this.entity == null)
		{
			return;
		}
		ClusterTraveler component = this.entity.GetComponent<ClusterTraveler>();
		if (component == null)
		{
			return;
		}
		List<AxialI> list = ((this.entity.IsVisible && component.IsTraveling()) ? component.CurrentPath : null);
		if (list != null && list.Count > 0)
		{
			if (this.mapPath == null)
			{
				this.mapPath = this.pathDrawer.AddPath();
			}
			this.mapPath.SetPoints(ClusterMapPathDrawer.GetDrawPathList(base.transform.GetLocalPosition(), list));
			Color color;
			if (this.isSelected)
			{
				color = ClusterMapScreen.Instance.rocketSelectedPathColor;
			}
			else if (this.entity.ShowPath())
			{
				color = ClusterMapScreen.Instance.rocketPathColor;
			}
			else
			{
				color = new Color(0f, 0f, 0f, 0f);
			}
			this.mapPath.SetColor(color);
			return;
		}
		if (this.mapPath != null)
		{
			global::Util.KDestroyGameObject(this.mapPath);
			this.mapPath = null;
		}
	}

	// Token: 0x060057F9 RID: 22521 RVA: 0x001FDEC2 File Offset: 0x001FC0C2
	public void SetAnimRotation(float rotation)
	{
		this.animContainer.localRotation = Quaternion.Euler(0f, 0f, rotation);
	}

	// Token: 0x060057FA RID: 22522 RVA: 0x001FDEDF File Offset: 0x001FC0DF
	public float GetPathAngle()
	{
		if (this.mapPath == null)
		{
			return 0f;
		}
		return this.mapPath.GetRotationForNextSegment();
	}

	// Token: 0x060057FB RID: 22523 RVA: 0x001FDF00 File Offset: 0x001FC100
	private void ClearAnimControllers()
	{
		if (this.animControllers == null)
		{
			return;
		}
		foreach (KBatchedAnimController kbatchedAnimController in this.animControllers)
		{
			global::Util.KDestroyGameObject(kbatchedAnimController.gameObject);
		}
		this.animControllers.Clear();
	}

	// Token: 0x04003AA2 RID: 15010
	public KBatchedAnimController animControllerPrefab;

	// Token: 0x04003AA3 RID: 15011
	public KBatchedAnimController peekControllerPrefab;

	// Token: 0x04003AA4 RID: 15012
	public Transform nameTarget;

	// Token: 0x04003AA5 RID: 15013
	public AlertVignette alertVignette;

	// Token: 0x04003AA6 RID: 15014
	public bool doesTransitionAnimation;

	// Token: 0x04003AA7 RID: 15015
	[HideInInspector]
	public Transform animContainer;

	// Token: 0x04003AA8 RID: 15016
	private ClusterGridEntity entity;

	// Token: 0x04003AA9 RID: 15017
	private ClusterMapPathDrawer pathDrawer;

	// Token: 0x04003AAA RID: 15018
	private ClusterMapPath mapPath;

	// Token: 0x04003AAB RID: 15019
	private List<KBatchedAnimController> animControllers;

	// Token: 0x04003AAC RID: 15020
	private bool isSelected;

	// Token: 0x04003AAD RID: 15021
	private ClusterRevealLevel lastRevealLevel;

	// Token: 0x02001CBD RID: 7357
	private class UpdateXPositionParameter : LoopingSoundParameterUpdater
	{
		// Token: 0x0600AC25 RID: 44069 RVA: 0x003C1001 File Offset: 0x003BF201
		public UpdateXPositionParameter()
			: base("Starmap_Position_X")
		{
		}

		// Token: 0x0600AC26 RID: 44070 RVA: 0x003C1020 File Offset: 0x003BF220
		public override void Add(LoopingSoundParameterUpdater.Sound sound)
		{
			ClusterMapVisualizer.UpdateXPositionParameter.Entry entry = new ClusterMapVisualizer.UpdateXPositionParameter.Entry
			{
				transform = sound.transform,
				ev = sound.ev,
				parameterId = sound.description.GetParameterId(base.parameter)
			};
			this.entries.Add(entry);
		}

		// Token: 0x0600AC27 RID: 44071 RVA: 0x003C1078 File Offset: 0x003BF278
		public override void Update(float dt)
		{
			foreach (ClusterMapVisualizer.UpdateXPositionParameter.Entry entry in this.entries)
			{
				if (!(entry.transform == null))
				{
					EventInstance ev = entry.ev;
					ev.setParameterByID(entry.parameterId, entry.transform.GetPosition().x / (float)Screen.width, false);
				}
			}
		}

		// Token: 0x0600AC28 RID: 44072 RVA: 0x003C1100 File Offset: 0x003BF300
		public override void Remove(LoopingSoundParameterUpdater.Sound sound)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].ev.handle == sound.ev.handle)
				{
					this.entries.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x04008736 RID: 34614
		private List<ClusterMapVisualizer.UpdateXPositionParameter.Entry> entries = new List<ClusterMapVisualizer.UpdateXPositionParameter.Entry>();

		// Token: 0x020028C8 RID: 10440
		private struct Entry
		{
			// Token: 0x0400B4BC RID: 46268
			public Transform transform;

			// Token: 0x0400B4BD RID: 46269
			public EventInstance ev;

			// Token: 0x0400B4BE RID: 46270
			public PARAMETER_ID parameterId;
		}
	}

	// Token: 0x02001CBE RID: 7358
	private class UpdateYPositionParameter : LoopingSoundParameterUpdater
	{
		// Token: 0x0600AC29 RID: 44073 RVA: 0x003C1158 File Offset: 0x003BF358
		public UpdateYPositionParameter()
			: base("Starmap_Position_Y")
		{
		}

		// Token: 0x0600AC2A RID: 44074 RVA: 0x003C1178 File Offset: 0x003BF378
		public override void Add(LoopingSoundParameterUpdater.Sound sound)
		{
			ClusterMapVisualizer.UpdateYPositionParameter.Entry entry = new ClusterMapVisualizer.UpdateYPositionParameter.Entry
			{
				transform = sound.transform,
				ev = sound.ev,
				parameterId = sound.description.GetParameterId(base.parameter)
			};
			this.entries.Add(entry);
		}

		// Token: 0x0600AC2B RID: 44075 RVA: 0x003C11D0 File Offset: 0x003BF3D0
		public override void Update(float dt)
		{
			foreach (ClusterMapVisualizer.UpdateYPositionParameter.Entry entry in this.entries)
			{
				if (!(entry.transform == null))
				{
					EventInstance ev = entry.ev;
					ev.setParameterByID(entry.parameterId, entry.transform.GetPosition().y / (float)Screen.height, false);
				}
			}
		}

		// Token: 0x0600AC2C RID: 44076 RVA: 0x003C1258 File Offset: 0x003BF458
		public override void Remove(LoopingSoundParameterUpdater.Sound sound)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].ev.handle == sound.ev.handle)
				{
					this.entries.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x04008737 RID: 34615
		private List<ClusterMapVisualizer.UpdateYPositionParameter.Entry> entries = new List<ClusterMapVisualizer.UpdateYPositionParameter.Entry>();

		// Token: 0x020028C9 RID: 10441
		private struct Entry
		{
			// Token: 0x0400B4BF RID: 46271
			public Transform transform;

			// Token: 0x0400B4C0 RID: 46272
			public EventInstance ev;

			// Token: 0x0400B4C1 RID: 46273
			public PARAMETER_ID parameterId;
		}
	}

	// Token: 0x02001CBF RID: 7359
	private class UpdateZoomPercentageParameter : LoopingSoundParameterUpdater
	{
		// Token: 0x0600AC2D RID: 44077 RVA: 0x003C12B0 File Offset: 0x003BF4B0
		public UpdateZoomPercentageParameter()
			: base("Starmap_Zoom_Percentage")
		{
		}

		// Token: 0x0600AC2E RID: 44078 RVA: 0x003C12D0 File Offset: 0x003BF4D0
		public override void Add(LoopingSoundParameterUpdater.Sound sound)
		{
			ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry entry = new ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry
			{
				ev = sound.ev,
				parameterId = sound.description.GetParameterId(base.parameter)
			};
			this.entries.Add(entry);
		}

		// Token: 0x0600AC2F RID: 44079 RVA: 0x003C131C File Offset: 0x003BF51C
		public override void Update(float dt)
		{
			foreach (ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry entry in this.entries)
			{
				EventInstance ev = entry.ev;
				ev.setParameterByID(entry.parameterId, ClusterMapScreen.Instance.CurrentZoomPercentage(), false);
			}
		}

		// Token: 0x0600AC30 RID: 44080 RVA: 0x003C1388 File Offset: 0x003BF588
		public override void Remove(LoopingSoundParameterUpdater.Sound sound)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].ev.handle == sound.ev.handle)
				{
					this.entries.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x04008738 RID: 34616
		private List<ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry> entries = new List<ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry>();

		// Token: 0x020028CA RID: 10442
		private struct Entry
		{
			// Token: 0x0400B4C2 RID: 46274
			public Transform transform;

			// Token: 0x0400B4C3 RID: 46275
			public EventInstance ev;

			// Token: 0x0400B4C4 RID: 46276
			public PARAMETER_ID parameterId;
		}
	}
}
