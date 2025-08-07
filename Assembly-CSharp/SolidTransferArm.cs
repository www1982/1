using System;
using System.Collections.Generic;
using System.Diagnostics;
using Database;
using FMODUnity;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020007CA RID: 1994
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidTransferArm : StateMachineComponent<SolidTransferArm.SMInstance>, ISim1000ms, IRenderEveryTick
{
	// Token: 0x06003551 RID: 13649 RVA: 0x0012A518 File Offset: 0x00128718
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.choreConsumer.AddProvider(GlobalChoreProvider.Instance);
		this.choreConsumer.SetReach(this.pickupRange);
		Klei.AI.Attributes attributes = this.GetAttributes();
		if (attributes.Get(Db.Get().Attributes.CarryAmount) == null)
		{
			attributes.Add(Db.Get().Attributes.CarryAmount);
		}
		AttributeModifier attributeModifier = new AttributeModifier(Db.Get().Attributes.CarryAmount.Id, this.max_carry_weight, base.gameObject.GetProperName(), false, false, true);
		this.GetAttributes().Add(attributeModifier);
		this.worker.usesMultiTool = false;
		this.storage.fxPrefix = Storage.FXPrefix.PickedUp;
		this.simRenderLoadBalance = false;
	}

	// Token: 0x06003552 RID: 13650 RVA: 0x0012A5DC File Offset: 0x001287DC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		string text = component.name + ".arm";
		this.arm_go = new GameObject(text);
		this.arm_go.SetActive(false);
		this.arm_go.transform.parent = component.transform;
		this.looping_sounds = this.arm_go.AddComponent<LoopingSounds>();
		string sound = GlobalAssets.GetSound(this.rotateSoundName, false);
		this.rotateSound = RuntimeManager.PathToEventReference(sound);
		this.arm_go.AddComponent<KPrefabID>().PrefabTag = new Tag(text);
		this.arm_anim_ctrl = this.arm_go.AddComponent<KBatchedAnimController>();
		this.arm_anim_ctrl.AnimFiles = new KAnimFile[] { component.AnimFiles[0] };
		this.arm_anim_ctrl.initialAnim = "arm";
		this.arm_anim_ctrl.isMovable = true;
		this.arm_anim_ctrl.sceneLayer = Grid.SceneLayer.TransferArm;
		component.SetSymbolVisiblity("arm_target", false);
		bool flag;
		Vector3 vector = component.GetSymbolTransform(new HashedString("arm_target"), out flag).GetColumn(3);
		vector.z = Grid.GetLayerZ(Grid.SceneLayer.TransferArm);
		this.arm_go.transform.SetPosition(vector);
		this.arm_go.SetActive(true);
		this.gameCell = Grid.PosToCell(this.arm_go);
		this.link = new KAnimLink(component, this.arm_anim_ctrl);
		ChoreGroups choreGroups = Db.Get().ChoreGroups;
		for (int i = 0; i < choreGroups.Count; i++)
		{
			this.choreConsumer.SetPermittedByUser(choreGroups[i], true);
		}
		base.Subscribe<SolidTransferArm>(-592767678, SolidTransferArm.OnOperationalChangedDelegate);
		base.Subscribe<SolidTransferArm>(1745615042, SolidTransferArm.OnEndChoreDelegate);
		this.RotateArm(this.rotatable.GetRotatedOffset(Vector3.up), true, 0f);
		this.DropLeftovers();
		component.enabled = false;
		component.enabled = true;
		MinionGroupProber.Get().SetValidSerialNos(this, this.serial_no, this.serial_no);
		base.smi.StartSM();
	}

	// Token: 0x06003553 RID: 13651 RVA: 0x0012A7FA File Offset: 0x001289FA
	protected override void OnCleanUp()
	{
		MinionGroupProber.Get().ReleaseProber(this);
		base.OnCleanUp();
	}

	// Token: 0x06003554 RID: 13652 RVA: 0x0012A80E File Offset: 0x00128A0E
	public static void BatchUpdate(List<UpdateBucketWithUpdater<ISim1000ms>.Entry> solid_transfer_arms, float time_delta)
	{
		SolidTransferArm.SolidTransferArmBatchUpdater.Instance.Reset(solid_transfer_arms);
		GlobalJobManager.Run(SolidTransferArm.SolidTransferArmBatchUpdater.Instance);
		SolidTransferArm.SolidTransferArmBatchUpdater.Instance.Finish();
	}

	// Token: 0x06003555 RID: 13653 RVA: 0x0012A830 File Offset: 0x00128A30
	private void Sim()
	{
		Chore.Precondition.Context context = default(Chore.Precondition.Context);
		if (this.choreConsumer.FindNextChore(ref context))
		{
			if (context.chore is FetchChore)
			{
				this.choreDriver.SetChore(context);
				FetchChore fetchChore = context.chore as FetchChore;
				this.storage.DropUnlessMatching(fetchChore);
				this.arm_anim_ctrl.enabled = false;
				this.arm_anim_ctrl.enabled = true;
			}
			else
			{
				bool flag = false;
				string text = "I am but a lowly transfer arm. I should only acquire FetchChores: ";
				Chore chore = context.chore;
				global::Debug.Assert(flag, text + ((chore != null) ? chore.ToString() : null));
			}
		}
		this.operational.SetActive(this.choreDriver.HasChore(), false);
	}

	// Token: 0x06003556 RID: 13654 RVA: 0x0012A8D8 File Offset: 0x00128AD8
	public void Sim1000ms(float dt)
	{
	}

	// Token: 0x06003557 RID: 13655 RVA: 0x0012A8DC File Offset: 0x00128ADC
	private void UpdateArmAnim()
	{
		FetchAreaChore fetchAreaChore = this.choreDriver.GetCurrentChore() as FetchAreaChore;
		if (this.worker.GetWorkable() && fetchAreaChore != null && this.rotation_complete)
		{
			this.StopRotateSound();
			this.SetArmAnim(fetchAreaChore.IsDelivering ? SolidTransferArm.ArmAnim.Drop : SolidTransferArm.ArmAnim.Pickup);
			return;
		}
		this.SetArmAnim(SolidTransferArm.ArmAnim.Idle);
	}

	// Token: 0x06003558 RID: 13656 RVA: 0x0012A938 File Offset: 0x00128B38
	private static bool AsyncUpdateVisitor(object obj, SolidTransferArm arm)
	{
		Pickupable pickupable = obj as Pickupable;
		if (Grid.GetCellRange(arm.gameCell, pickupable.cachedCell) <= arm.pickupRange && arm.IsPickupableRelevantToMyInterests(pickupable.KPrefabID, pickupable.cachedCell) && pickupable.CouldBePickedUpByTransferArm(arm.kPrefabID.InstanceID))
		{
			arm.pickupables.Add(pickupable);
		}
		return true;
	}

	// Token: 0x06003559 RID: 13657 RVA: 0x0012A99C File Offset: 0x00128B9C
	private bool AsyncUpdate()
	{
		int num;
		int num2;
		Grid.CellToXY(this.gameCell, out num, out num2);
		bool flag = false;
		for (int i = num2 - this.pickupRange; i < num2 + this.pickupRange + 1; i++)
		{
			for (int j = num - this.pickupRange; j < num + this.pickupRange + 1; j++)
			{
				int num3 = Grid.XYToCell(j, i);
				if ((Grid.IsValidCell(num3) && Grid.IsPhysicallyAccessible(num, num2, j, i, true)) != this.reachableCells.Contains(num3))
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			this.reachableCells.Clear();
			for (int k = num2 - this.pickupRange; k < num2 + this.pickupRange + 1; k++)
			{
				for (int l = num - this.pickupRange; l < num + this.pickupRange + 1; l++)
				{
					int num4 = Grid.XYToCell(l, k);
					if (Grid.IsValidCell(num4) && Grid.IsPhysicallyAccessible(num, num2, l, k, true))
					{
						this.reachableCells.Add(num4);
					}
				}
			}
			this.IncrementSerialNo();
		}
		this.pickupables.Clear();
		GameScenePartitioner.Instance.AsyncSafeVisit<SolidTransferArm>(num - this.pickupRange, num2 - this.pickupRange, 2 * this.pickupRange + 1, 2 * this.pickupRange + 1, GameScenePartitioner.Instance.pickupablesLayer, SolidTransferArm.AsyncUpdateVisitor_s, this);
		GameScenePartitioner.Instance.AsyncSafeVisit<SolidTransferArm>(num - this.pickupRange, num2 - this.pickupRange, 2 * this.pickupRange + 1, 2 * this.pickupRange + 1, GameScenePartitioner.Instance.storedPickupablesLayer, SolidTransferArm.AsyncUpdateVisitor_s, this);
		return flag;
	}

	// Token: 0x0600355A RID: 13658 RVA: 0x0012AB37 File Offset: 0x00128D37
	private void IncrementSerialNo()
	{
		this.serial_no += 1;
		MinionGroupProber.Get().SetValidSerialNos(this, this.serial_no, this.serial_no);
		MinionGroupProber.Get().Occupy(this, this.serial_no, this.reachableCells);
	}

	// Token: 0x0600355B RID: 13659 RVA: 0x0012AB76 File Offset: 0x00128D76
	public bool IsCellReachable(int cell)
	{
		return this.reachableCells.Contains(cell);
	}

	// Token: 0x0600355C RID: 13660 RVA: 0x0012AB84 File Offset: 0x00128D84
	private bool IsPickupableRelevantToMyInterests(KPrefabID prefabID, int storage_cell)
	{
		return Assets.IsTagSolidTransferArmConveyable(prefabID.PrefabTag) && this.IsCellReachable(storage_cell);
	}

	// Token: 0x0600355D RID: 13661 RVA: 0x0012AB9C File Offset: 0x00128D9C
	public Pickupable FindFetchTarget(Storage destination, FetchChore chore)
	{
		return FetchManager.FindFetchTarget(this.pickupables, destination, chore);
	}

	// Token: 0x0600355E RID: 13662 RVA: 0x0012ABAC File Offset: 0x00128DAC
	public void RenderEveryTick(float dt)
	{
		if (this.worker.GetWorkable())
		{
			Vector3 targetPoint = this.worker.GetWorkable().GetTargetPoint();
			targetPoint.z = 0f;
			Vector3 position = base.transform.GetPosition();
			position.z = 0f;
			Vector3 vector = Vector3.Normalize(targetPoint - position);
			this.RotateArm(vector, false, dt);
		}
		this.UpdateArmAnim();
	}

	// Token: 0x0600355F RID: 13663 RVA: 0x0012AC1C File Offset: 0x00128E1C
	private void OnEndChore(object data)
	{
		this.DropLeftovers();
	}

	// Token: 0x06003560 RID: 13664 RVA: 0x0012AC24 File Offset: 0x00128E24
	private void DropLeftovers()
	{
		if (!this.storage.IsEmpty() && !this.choreDriver.HasChore())
		{
			this.storage.DropAll(false, false, default(Vector3), true, null);
		}
	}

	// Token: 0x06003561 RID: 13665 RVA: 0x0012AC64 File Offset: 0x00128E64
	private void SetArmAnim(SolidTransferArm.ArmAnim new_anim)
	{
		if (new_anim == this.arm_anim)
		{
			return;
		}
		this.arm_anim = new_anim;
		switch (this.arm_anim)
		{
		case SolidTransferArm.ArmAnim.Idle:
			this.arm_anim_ctrl.Play("arm", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		case SolidTransferArm.ArmAnim.Pickup:
			this.arm_anim_ctrl.Play("arm_pickup", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		case SolidTransferArm.ArmAnim.Drop:
			this.arm_anim_ctrl.Play("arm_drop", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		default:
			return;
		}
	}

	// Token: 0x06003562 RID: 13666 RVA: 0x0012ACFE File Offset: 0x00128EFE
	private void OnOperationalChanged(object data)
	{
		if (!(bool)data)
		{
			if (this.choreDriver.HasChore())
			{
				this.choreDriver.StopChore();
			}
			this.UpdateArmAnim();
		}
	}

	// Token: 0x06003563 RID: 13667 RVA: 0x0012AD26 File Offset: 0x00128F26
	private void SetArmRotation(float rot)
	{
		this.arm_rot = rot;
		this.arm_go.transform.rotation = Quaternion.Euler(0f, 0f, this.arm_rot);
	}

	// Token: 0x06003564 RID: 13668 RVA: 0x0012AD54 File Offset: 0x00128F54
	private void RotateArm(Vector3 target_dir, bool warp, float dt)
	{
		float num = MathUtil.AngleSigned(Vector3.up, target_dir, Vector3.forward) - this.arm_rot;
		if (num < -180f)
		{
			num += 360f;
		}
		if (num > 180f)
		{
			num -= 360f;
		}
		if (!warp)
		{
			num = Mathf.Clamp(num, -this.turn_rate * dt, this.turn_rate * dt);
		}
		this.arm_rot += num;
		this.SetArmRotation(this.arm_rot);
		this.rotation_complete = Mathf.Approximately(num, 0f);
		if (!warp && !this.rotation_complete)
		{
			if (!this.rotateSoundPlaying)
			{
				this.StartRotateSound();
			}
			this.SetRotateSoundParameter(this.arm_rot);
			return;
		}
		this.StopRotateSound();
	}

	// Token: 0x06003565 RID: 13669 RVA: 0x0012AE0B File Offset: 0x0012900B
	private void StartRotateSound()
	{
		if (!this.rotateSoundPlaying)
		{
			this.looping_sounds.StartSound(this.rotateSound);
			this.rotateSoundPlaying = true;
		}
	}

	// Token: 0x06003566 RID: 13670 RVA: 0x0012AE2E File Offset: 0x0012902E
	private void SetRotateSoundParameter(float arm_rot)
	{
		if (this.rotateSoundPlaying)
		{
			this.looping_sounds.SetParameter(this.rotateSound, SolidTransferArm.HASH_ROTATION, arm_rot);
		}
	}

	// Token: 0x06003567 RID: 13671 RVA: 0x0012AE4F File Offset: 0x0012904F
	private void StopRotateSound()
	{
		if (this.rotateSoundPlaying)
		{
			this.looping_sounds.StopSound(this.rotateSound);
			this.rotateSoundPlaying = false;
		}
	}

	// Token: 0x06003568 RID: 13672 RVA: 0x0012AE71 File Offset: 0x00129071
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void BeginDetailedSample(string region_name)
	{
	}

	// Token: 0x06003569 RID: 13673 RVA: 0x0012AE73 File Offset: 0x00129073
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void BeginDetailedSample(string region_name, int count)
	{
	}

	// Token: 0x0600356A RID: 13674 RVA: 0x0012AE75 File Offset: 0x00129075
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void EndDetailedSample(string region_name)
	{
	}

	// Token: 0x0600356B RID: 13675 RVA: 0x0012AE77 File Offset: 0x00129077
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void EndDetailedSample(string region_name, int count)
	{
	}

	// Token: 0x0400202A RID: 8234
	[MyCmpReq]
	private Operational operational;

	// Token: 0x0400202B RID: 8235
	[MyCmpReq]
	private KPrefabID kPrefabID;

	// Token: 0x0400202C RID: 8236
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x0400202D RID: 8237
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x0400202E RID: 8238
	[MyCmpAdd]
	private StandardWorker worker;

	// Token: 0x0400202F RID: 8239
	[MyCmpAdd]
	private ChoreConsumer choreConsumer;

	// Token: 0x04002030 RID: 8240
	[MyCmpAdd]
	private ChoreDriver choreDriver;

	// Token: 0x04002031 RID: 8241
	public int pickupRange = 4;

	// Token: 0x04002032 RID: 8242
	private float max_carry_weight = 1000f;

	// Token: 0x04002033 RID: 8243
	private List<Pickupable> pickupables = new List<Pickupable>();

	// Token: 0x04002034 RID: 8244
	private KBatchedAnimController arm_anim_ctrl;

	// Token: 0x04002035 RID: 8245
	private GameObject arm_go;

	// Token: 0x04002036 RID: 8246
	private LoopingSounds looping_sounds;

	// Token: 0x04002037 RID: 8247
	private bool rotateSoundPlaying;

	// Token: 0x04002038 RID: 8248
	private string rotateSoundName = "TransferArm_rotate";

	// Token: 0x04002039 RID: 8249
	private EventReference rotateSound;

	// Token: 0x0400203A RID: 8250
	private KAnimLink link;

	// Token: 0x0400203B RID: 8251
	private float arm_rot = 45f;

	// Token: 0x0400203C RID: 8252
	private float turn_rate = 360f;

	// Token: 0x0400203D RID: 8253
	private bool rotation_complete;

	// Token: 0x0400203E RID: 8254
	private int gameCell;

	// Token: 0x0400203F RID: 8255
	private SolidTransferArm.ArmAnim arm_anim;

	// Token: 0x04002040 RID: 8256
	private HashSet<int> reachableCells = new HashSet<int>();

	// Token: 0x04002041 RID: 8257
	private static readonly EventSystem.IntraObjectHandler<SolidTransferArm> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<SolidTransferArm>(delegate(SolidTransferArm component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04002042 RID: 8258
	private static readonly EventSystem.IntraObjectHandler<SolidTransferArm> OnEndChoreDelegate = new EventSystem.IntraObjectHandler<SolidTransferArm>(delegate(SolidTransferArm component, object data)
	{
		component.OnEndChore(data);
	});

	// Token: 0x04002043 RID: 8259
	private static Func<object, SolidTransferArm, bool> AsyncUpdateVisitor_s = new Func<object, SolidTransferArm, bool>(SolidTransferArm.AsyncUpdateVisitor);

	// Token: 0x04002044 RID: 8260
	private short serial_no;

	// Token: 0x04002045 RID: 8261
	private static HashedString HASH_ROTATION = "rotation";

	// Token: 0x02001701 RID: 5889
	private enum ArmAnim
	{
		// Token: 0x0400745C RID: 29788
		Idle,
		// Token: 0x0400745D RID: 29789
		Pickup,
		// Token: 0x0400745E RID: 29790
		Drop
	}

	// Token: 0x02001702 RID: 5890
	public class SMInstance : GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.GameInstance
	{
		// Token: 0x0600975C RID: 38748 RVA: 0x0037BDBB File Offset: 0x00379FBB
		public SMInstance(SolidTransferArm master)
			: base(master)
		{
		}
	}

	// Token: 0x02001703 RID: 5891
	public class States : GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm>
	{
		// Token: 0x0600975D RID: 38749 RVA: 0x0037BDC4 File Offset: 0x00379FC4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.DoNothing();
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (SolidTransferArm.SMInstance smi) => smi.GetComponent<Operational>().IsOperational).Enter(delegate(SolidTransferArm.SMInstance smi)
			{
				smi.master.StopRotateSound();
			});
			this.on.DefaultState(this.on.idle).EventTransition(GameHashes.OperationalChanged, this.off, (SolidTransferArm.SMInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.on.idle.PlayAnim("on").EventTransition(GameHashes.ActiveChanged, this.on.working, (SolidTransferArm.SMInstance smi) => smi.GetComponent<Operational>().IsActive);
			this.on.working.PlayAnim("working").EventTransition(GameHashes.ActiveChanged, this.on.idle, (SolidTransferArm.SMInstance smi) => !smi.GetComponent<Operational>().IsActive);
		}

		// Token: 0x0400745F RID: 29791
		public StateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.BoolParameter transferring;

		// Token: 0x04007460 RID: 29792
		public GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.State off;

		// Token: 0x04007461 RID: 29793
		public SolidTransferArm.States.ReadyStates on;

		// Token: 0x020027DC RID: 10204
		public class ReadyStates : GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.State
		{
			// Token: 0x0400B0B9 RID: 45241
			public GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.State idle;

			// Token: 0x0400B0BA RID: 45242
			public GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.State working;
		}
	}

	// Token: 0x02001704 RID: 5892
	private class SolidTransferArmBatchUpdater : WorkItemCollection<List<UpdateBucketWithUpdater<ISim1000ms>.Entry>>
	{
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600975F RID: 38751 RVA: 0x0037BF2B File Offset: 0x0037A12B
		public static SolidTransferArm.SolidTransferArmBatchUpdater Instance
		{
			get
			{
				if (SolidTransferArm.SolidTransferArmBatchUpdater.instance == null)
				{
					SolidTransferArm.SolidTransferArmBatchUpdater.instance = new SolidTransferArm.SolidTransferArmBatchUpdater();
				}
				return SolidTransferArm.SolidTransferArmBatchUpdater.instance;
			}
		}

		// Token: 0x06009760 RID: 38752 RVA: 0x0037BF43 File Offset: 0x0037A143
		public void Reset(List<UpdateBucketWithUpdater<ISim1000ms>.Entry> entries)
		{
			this.sharedData = entries;
			this.count = (entries.Count + 8 - 1) / 8;
		}

		// Token: 0x06009761 RID: 38753 RVA: 0x0037BF60 File Offset: 0x0037A160
		public override void RunItem(int item, ref List<UpdateBucketWithUpdater<ISim1000ms>.Entry> shared_data, int threadIndex)
		{
			int num = item * 8;
			int num2 = Math.Min(shared_data.Count, num + 8);
			for (int i = num; i < num2; i++)
			{
				SolidTransferArm solidTransferArm = (SolidTransferArm)shared_data[i].data;
				if (solidTransferArm.operational.IsOperational)
				{
					solidTransferArm.AsyncUpdate();
				}
			}
		}

		// Token: 0x06009762 RID: 38754 RVA: 0x0037BFB4 File Offset: 0x0037A1B4
		public void Finish()
		{
			foreach (UpdateBucketWithUpdater<ISim1000ms>.Entry entry in this.sharedData)
			{
				SolidTransferArm solidTransferArm = (SolidTransferArm)entry.data;
				if (solidTransferArm.operational.IsOperational)
				{
					solidTransferArm.Sim();
				}
			}
			this.Reset(SolidTransferArm.SolidTransferArmBatchUpdater.EmptyList);
		}

		// Token: 0x04007462 RID: 29794
		private static readonly List<UpdateBucketWithUpdater<ISim1000ms>.Entry> EmptyList = new List<UpdateBucketWithUpdater<ISim1000ms>.Entry>();

		// Token: 0x04007463 RID: 29795
		private const int kBatchSize = 8;

		// Token: 0x04007464 RID: 29796
		private static SolidTransferArm.SolidTransferArmBatchUpdater instance;
	}

	// Token: 0x02001705 RID: 5893
	public struct CachedPickupable
	{
		// Token: 0x04007465 RID: 29797
		public Pickupable pickupable;

		// Token: 0x04007466 RID: 29798
		public int storage_cell;
	}
}
