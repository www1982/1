using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B33 RID: 2867
public class ClusterMapMeteorShowerVisualizer : ClusterGridEntity
{
	// Token: 0x17000600 RID: 1536
	// (get) Token: 0x060054A1 RID: 21665 RVA: 0x001EC31A File Offset: 0x001EA51A
	public override string Name
	{
		get
		{
			return this.p_name;
		}
	}

	// Token: 0x17000601 RID: 1537
	// (get) Token: 0x060054A2 RID: 21666 RVA: 0x001EC322 File Offset: 0x001EA522
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.Meteor;
		}
	}

	// Token: 0x17000602 RID: 1538
	// (get) Token: 0x060054A3 RID: 21667 RVA: 0x001EC325 File Offset: 0x001EA525
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000603 RID: 1539
	// (get) Token: 0x060054A4 RID: 21668 RVA: 0x001EC328 File Offset: 0x001EA528
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x17000604 RID: 1540
	// (get) Token: 0x060054A5 RID: 21669 RVA: 0x001EC32C File Offset: 0x001EA52C
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim(this.clusterAnimName),
					initialAnim = this.AnimName,
					animPlaySpeedModifier = 0.5f
				},
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("shower_identify_kanim"),
					initialAnim = "identify_off",
					playMode = KAnim.PlayMode.Once
				},
				this.questionMarkAnimConfig
			};
		}
	}

	// Token: 0x17000605 RID: 1541
	// (get) Token: 0x060054A6 RID: 21670 RVA: 0x001EC3C4 File Offset: 0x001EA5C4
	public ClusterRevealLevel clusterCellRevealLevel
	{
		get
		{
			ClusterRevealLevel cellRevealLevel = ClusterGrid.Instance.GetCellRevealLevel(base.Location);
			if (cellRevealLevel == ClusterRevealLevel.Visible)
			{
				return cellRevealLevel;
			}
			if (this.forceRevealed)
			{
				return ClusterRevealLevel.Peeked;
			}
			return cellRevealLevel;
		}
	}

	// Token: 0x17000606 RID: 1542
	// (get) Token: 0x060054A7 RID: 21671 RVA: 0x001EC3F3 File Offset: 0x001EA5F3
	public string AnimName
	{
		get
		{
			if (!this.forceRevealed && (!this.revealed || this.clusterCellRevealLevel != ClusterRevealLevel.Visible))
			{
				return "unknown";
			}
			return "idle_loop";
		}
	}

	// Token: 0x17000607 RID: 1543
	// (get) Token: 0x060054A8 RID: 21672 RVA: 0x001EC419 File Offset: 0x001EA619
	public string QuestionMarkAnimName
	{
		get
		{
			if (!this.forceRevealed && (!this.revealed || this.clusterCellRevealLevel != ClusterRevealLevel.Visible))
			{
				return this.questionMarkAnimConfig.initialAnim;
			}
			return "off";
		}
	}

	// Token: 0x060054A9 RID: 21673 RVA: 0x001EC448 File Offset: 0x001EA648
	public KBatchedAnimController CreateQuestionMarkInstance(KBatchedAnimController origin, Transform parent)
	{
		KBatchedAnimController kbatchedAnimController = global::UnityEngine.Object.Instantiate<KBatchedAnimController>(origin, parent);
		kbatchedAnimController.gameObject.SetActive(true);
		kbatchedAnimController.SwapAnims(new KAnimFile[] { this.questionMarkAnimConfig.animFile });
		kbatchedAnimController.Play(this.QuestionMarkAnimName, KAnim.PlayMode.Once, 1f, 0f);
		kbatchedAnimController.gameObject.AddOrGet<ClusterMapIconFixRotation>();
		return kbatchedAnimController;
	}

	// Token: 0x060054AA RID: 21674 RVA: 0x001EC4AC File Offset: 0x001EA6AC
	protected override void OnCleanUp()
	{
		if (ClusterMapScreen.Instance != null)
		{
			ClusterMapVisualizer entityVisAnim = ClusterMapScreen.Instance.GetEntityVisAnim(this);
			if (entityVisAnim != null)
			{
				entityVisAnim.gameObject.SetActive(false);
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x060054AB RID: 21675 RVA: 0x001EC4ED File Offset: 0x001EA6ED
	public void SetInitialLocation(AxialI startLocation)
	{
		this.m_location = startLocation;
		this.RefreshVisuals();
	}

	// Token: 0x060054AC RID: 21676 RVA: 0x001EC4FC File Offset: 0x001EA6FC
	public override bool SpaceOutInSameHex()
	{
		return true;
	}

	// Token: 0x060054AD RID: 21677 RVA: 0x001EC4FF File Offset: 0x001EA6FF
	public override bool KeepRotationWhenSpacingOutInHex()
	{
		return true;
	}

	// Token: 0x060054AE RID: 21678 RVA: 0x001EC502 File Offset: 0x001EA702
	public override bool ShowPath()
	{
		return this.m_selectable.IsSelected;
	}

	// Token: 0x060054AF RID: 21679 RVA: 0x001EC510 File Offset: 0x001EA710
	public override void OnClusterMapIconShown(ClusterRevealLevel levelUsed)
	{
		ClusterMapVisualizer entityVisAnim = ClusterMapScreen.Instance.GetEntityVisAnim(this);
		switch (levelUsed)
		{
		case ClusterRevealLevel.Hidden:
			this.Deselect();
			break;
		case ClusterRevealLevel.Peeked:
		{
			KBatchedAnimController firstAnimController = entityVisAnim.GetFirstAnimController();
			if (firstAnimController != null)
			{
				firstAnimController.SwapAnims(new KAnimFile[] { this.AnimConfigs[0].animFile });
				KBatchedAnimController kbatchedAnimController = this.CreateQuestionMarkInstance(entityVisAnim.peekControllerPrefab, firstAnimController.transform.parent);
				entityVisAnim.ManualAddAnimController(kbatchedAnimController);
			}
			this.RefreshVisuals();
			this.Deselect();
			break;
		}
		case ClusterRevealLevel.Visible:
			this.RefreshVisuals();
			break;
		}
		KBatchedAnimController animController = entityVisAnim.GetAnimController(2);
		if (animController != null && !this.revealed)
		{
			animController.gameObject.AddOrGet<ClusterMapIconFixRotation>();
		}
	}

	// Token: 0x060054B0 RID: 21680 RVA: 0x001EC5CD File Offset: 0x001EA7CD
	public void Deselect()
	{
		if (this.m_selectable.IsSelected)
		{
			this.m_selectable.Unselect();
		}
	}

	// Token: 0x060054B1 RID: 21681 RVA: 0x001EC5E8 File Offset: 0x001EA7E8
	public void RefreshVisuals()
	{
		ClusterMapVisualizer entityVisAnim = ClusterMapScreen.Instance.GetEntityVisAnim(this);
		if (entityVisAnim != null)
		{
			KBatchedAnimController firstAnimController = entityVisAnim.GetFirstAnimController();
			if (firstAnimController != null)
			{
				firstAnimController.Play(this.AnimName, KAnim.PlayMode.Loop, 1f, 0f);
			}
			KBatchedAnimController animController = entityVisAnim.GetAnimController(2);
			if (animController != null)
			{
				animController.Play(this.QuestionMarkAnimName, KAnim.PlayMode.Once, 1f, 0f);
			}
		}
	}

	// Token: 0x060054B2 RID: 21682 RVA: 0x001EC664 File Offset: 0x001EA864
	public void PlayRevealAnimation(bool playIdentifyAnimationIfVisible)
	{
		this.revealed = true;
		this.RefreshVisuals();
		if (playIdentifyAnimationIfVisible)
		{
			ClusterMapVisualizer entityVisAnim = ClusterMapScreen.Instance.GetEntityVisAnim(this);
			KBatchedAnimController animController = entityVisAnim.GetAnimController(1);
			entityVisAnim.GetAnimController(2);
			if (animController != null)
			{
				animController.Play("identify", KAnim.PlayMode.Once, 1f, 0f);
			}
		}
	}

	// Token: 0x060054B3 RID: 21683 RVA: 0x001EC6BF File Offset: 0x001EA8BF
	public void PlayHideAnimation()
	{
		this.revealed = false;
		if (ClusterMapScreen.Instance.GetEntityVisAnim(this) != null)
		{
			this.RefreshVisuals();
		}
	}

	// Token: 0x040038E7 RID: 14567
	private ClusterGridEntity.AnimConfig questionMarkAnimConfig = new ClusterGridEntity.AnimConfig
	{
		animFile = Assets.GetAnim("shower_question_mark_kanim"),
		initialAnim = "idle",
		playMode = KAnim.PlayMode.Once
	};

	// Token: 0x040038E8 RID: 14568
	public string p_name;

	// Token: 0x040038E9 RID: 14569
	public string clusterAnimName;

	// Token: 0x040038EA RID: 14570
	public bool revealed;

	// Token: 0x040038EB RID: 14571
	public bool forceRevealed;
}
