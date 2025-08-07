using System;
using FMODUnity;
using KSerialization;
using UnityEngine;

// Token: 0x020006DC RID: 1756
[SerializationConfig(MemberSerialization.OptIn)]
public class AutoMiner : StateMachineComponent<AutoMiner.Instance>, ISim1000ms
{
	// Token: 0x17000219 RID: 537
	// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000FA309 File Offset: 0x000F8509
	private bool HasDigCell
	{
		get
		{
			return this.dig_cell != Grid.InvalidCell;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06002B51 RID: 11089 RVA: 0x000FA31B File Offset: 0x000F851B
	private bool RotationComplete
	{
		get
		{
			return this.HasDigCell && this.rotation_complete;
		}
	}

	// Token: 0x06002B52 RID: 11090 RVA: 0x000FA32D File Offset: 0x000F852D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.simRenderLoadBalance = true;
	}

	// Token: 0x06002B53 RID: 11091 RVA: 0x000FA33C File Offset: 0x000F853C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.hitEffectPrefab = Assets.GetPrefab("fx_dig_splash");
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		string text = component.name + ".gun";
		this.arm_go = new GameObject(text);
		this.arm_go.SetActive(false);
		this.arm_go.transform.parent = component.transform;
		this.looping_sounds = this.arm_go.AddComponent<LoopingSounds>();
		string sound = GlobalAssets.GetSound(this.rotateSoundName, false);
		this.rotateSound = RuntimeManager.PathToEventReference(sound);
		this.arm_go.AddComponent<KPrefabID>().PrefabTag = new Tag(text);
		this.arm_anim_ctrl = this.arm_go.AddComponent<KBatchedAnimController>();
		this.arm_anim_ctrl.AnimFiles = new KAnimFile[] { component.AnimFiles[0] };
		this.arm_anim_ctrl.initialAnim = "gun";
		this.arm_anim_ctrl.isMovable = true;
		this.arm_anim_ctrl.sceneLayer = Grid.SceneLayer.TransferArm;
		component.SetSymbolVisiblity("gun_target", false);
		bool flag;
		Vector3 vector = component.GetSymbolTransform(new HashedString("gun_target"), out flag).GetColumn(3);
		vector.z = Grid.GetLayerZ(Grid.SceneLayer.TransferArm);
		this.arm_go.transform.SetPosition(vector);
		this.arm_go.SetActive(true);
		this.link = new KAnimLink(component, this.arm_anim_ctrl);
		base.Subscribe<AutoMiner>(-592767678, AutoMiner.OnOperationalChangedDelegate);
		this.RotateArm(this.rotatable.GetRotatedOffset(Quaternion.Euler(0f, 0f, -45f) * Vector3.up), true, 0f);
		this.StopDig();
		base.smi.StartSM();
	}

	// Token: 0x06002B54 RID: 11092 RVA: 0x000FA50A File Offset: 0x000F870A
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06002B55 RID: 11093 RVA: 0x000FA512 File Offset: 0x000F8712
	public void Sim1000ms(float dt)
	{
		if (!this.operational.IsOperational)
		{
			return;
		}
		this.RefreshDiggableCell();
		this.operational.SetActive(this.HasDigCell, false);
	}

	// Token: 0x06002B56 RID: 11094 RVA: 0x000FA53A File Offset: 0x000F873A
	private void OnOperationalChanged(object data)
	{
		if (!(bool)data)
		{
			this.dig_cell = Grid.InvalidCell;
			this.rotation_complete = false;
		}
	}

	// Token: 0x06002B57 RID: 11095 RVA: 0x000FA558 File Offset: 0x000F8758
	public void UpdateRotation(float dt)
	{
		if (this.HasDigCell)
		{
			Vector3 vector = Grid.CellToPosCCC(this.dig_cell, Grid.SceneLayer.TileMain);
			vector.z = 0f;
			Vector3 position = this.arm_go.transform.GetPosition();
			position.z = 0f;
			Vector3 vector2 = Vector3.Normalize(vector - position);
			this.RotateArm(vector2, false, dt);
		}
	}

	// Token: 0x06002B58 RID: 11096 RVA: 0x000FA5BA File Offset: 0x000F87BA
	private Element GetTargetElement()
	{
		if (this.HasDigCell)
		{
			return Grid.Element[this.dig_cell];
		}
		return null;
	}

	// Token: 0x06002B59 RID: 11097 RVA: 0x000FA5D4 File Offset: 0x000F87D4
	public void StartDig()
	{
		Element targetElement = this.GetTargetElement();
		base.Trigger(-1762453998, targetElement);
		this.CreateHitEffect();
		this.arm_anim_ctrl.Play("gun_digging", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06002B5A RID: 11098 RVA: 0x000FA61A File Offset: 0x000F881A
	public void StopDig()
	{
		base.Trigger(939543986, null);
		this.DestroyHitEffect();
		this.arm_anim_ctrl.Play("gun", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06002B5B RID: 11099 RVA: 0x000FA650 File Offset: 0x000F8850
	public void UpdateDig(float dt)
	{
		if (!this.HasDigCell)
		{
			return;
		}
		if (!this.rotation_complete)
		{
			return;
		}
		Diggable.DoDigTick(this.dig_cell, dt, WorldDamage.DamageType.NoBuildingDamage);
		float num = Grid.Damage[this.dig_cell];
		this.mining_sounds.SetPercentComplete(num);
		Vector3 vector = Grid.CellToPosCCC(this.dig_cell, Grid.SceneLayer.FXFront2);
		vector.z = 0f;
		Vector3 position = this.arm_go.transform.GetPosition();
		position.z = 0f;
		float sqrMagnitude = (vector - position).sqrMagnitude;
		this.arm_anim_ctrl.GetBatchInstanceData().SetClipRadius(position.x, position.y, sqrMagnitude, true);
		if (!AutoMiner.ValidDigCell(this.dig_cell))
		{
			this.dig_cell = Grid.InvalidCell;
			this.rotation_complete = false;
		}
	}

	// Token: 0x06002B5C RID: 11100 RVA: 0x000FA71C File Offset: 0x000F891C
	private void CreateHitEffect()
	{
		if (this.hitEffectPrefab == null)
		{
			return;
		}
		if (this.hitEffect != null)
		{
			this.DestroyHitEffect();
		}
		Vector3 vector = Grid.CellToPosCCC(this.dig_cell, Grid.SceneLayer.FXFront2);
		this.hitEffect = GameUtil.KInstantiate(this.hitEffectPrefab, vector, Grid.SceneLayer.FXFront2, null, 0);
		this.hitEffect.SetActive(true);
		KBatchedAnimController component = this.hitEffect.GetComponent<KBatchedAnimController>();
		component.sceneLayer = Grid.SceneLayer.FXFront2;
		component.initialMode = KAnim.PlayMode.Loop;
		component.enabled = false;
		component.enabled = true;
	}

	// Token: 0x06002B5D RID: 11101 RVA: 0x000FA7A3 File Offset: 0x000F89A3
	private void DestroyHitEffect()
	{
		if (this.hitEffectPrefab == null)
		{
			return;
		}
		if (this.hitEffect != null)
		{
			this.hitEffect.DeleteObject();
			this.hitEffect = null;
		}
	}

	// Token: 0x06002B5E RID: 11102 RVA: 0x000FA7D4 File Offset: 0x000F89D4
	private void RefreshDiggableCell()
	{
		CellOffset rotatedCellOffset = this.vision_offset;
		if (this.rotatable)
		{
			rotatedCellOffset = this.rotatable.GetRotatedCellOffset(this.vision_offset);
		}
		int num = Grid.PosToCell(base.transform.gameObject);
		int num2 = Grid.OffsetCell(num, rotatedCellOffset);
		int num3;
		int num4;
		Grid.CellToXY(num2, out num3, out num4);
		float num5 = float.MaxValue;
		int num6 = Grid.InvalidCell;
		Vector3 vector = Grid.CellToPos(num2);
		bool flag = false;
		for (int i = 0; i < this.height; i++)
		{
			for (int j = 0; j < this.width; j++)
			{
				CellOffset rotatedCellOffset2 = new CellOffset(this.x + j, this.y + i);
				if (this.rotatable)
				{
					rotatedCellOffset2 = this.rotatable.GetRotatedCellOffset(rotatedCellOffset2);
				}
				int num7 = Grid.OffsetCell(num, rotatedCellOffset2);
				if (Grid.IsValidCell(num7))
				{
					int num8;
					int num9;
					Grid.CellToXY(num7, out num8, out num9);
					if (Grid.IsValidCell(num7) && AutoMiner.ValidDigCell(num7) && Grid.TestLineOfSight(num3, num4, num8, num9, new Func<int, bool>(AutoMiner.DigBlockingCB), false, false))
					{
						if (num7 == this.dig_cell)
						{
							flag = true;
						}
						Vector3 vector2 = Grid.CellToPos(num7);
						float num10 = Vector3.Distance(vector, vector2);
						if (num10 < num5)
						{
							num5 = num10;
							num6 = num7;
						}
					}
				}
			}
		}
		if (!flag && this.dig_cell != num6)
		{
			this.dig_cell = num6;
			this.rotation_complete = false;
		}
	}

	// Token: 0x06002B5F RID: 11103 RVA: 0x000FA944 File Offset: 0x000F8B44
	private static bool ValidDigCell(int cell)
	{
		bool flag = Grid.HasDoor[cell] && Grid.Foundation[cell] && Grid.ObjectLayers[9].ContainsKey(cell);
		if (flag)
		{
			Door component = Grid.ObjectLayers[9][cell].GetComponent<Door>();
			flag = component != null && component.IsOpen() && !component.IsPendingClose();
		}
		return Grid.Solid[cell] && (!Grid.Foundation[cell] || flag) && Grid.Element[cell].hardness < 150;
	}

	// Token: 0x06002B60 RID: 11104 RVA: 0x000FA9EC File Offset: 0x000F8BEC
	public static bool DigBlockingCB(int cell)
	{
		bool flag = Grid.HasDoor[cell] && Grid.Foundation[cell] && Grid.ObjectLayers[9].ContainsKey(cell);
		if (flag)
		{
			Door component = Grid.ObjectLayers[9][cell].GetComponent<Door>();
			flag = component != null && component.IsOpen() && !component.IsPendingClose();
		}
		return (Grid.Foundation[cell] && Grid.Solid[cell] && !flag) || Grid.Element[cell].hardness >= 150;
	}

	// Token: 0x06002B61 RID: 11105 RVA: 0x000FAA90 File Offset: 0x000F8C90
	private void RotateArm(Vector3 target_dir, bool warp, float dt)
	{
		if (this.rotation_complete)
		{
			return;
		}
		float num = MathUtil.AngleSigned(Vector3.up, target_dir, Vector3.forward) - this.arm_rot;
		num = MathUtil.Wrap(-180f, 180f, num);
		this.rotation_complete = Mathf.Approximately(num, 0f);
		float num2 = num;
		if (warp)
		{
			this.rotation_complete = true;
		}
		else
		{
			num2 = Mathf.Clamp(num2, -this.turn_rate * dt, this.turn_rate * dt);
		}
		this.arm_rot += num2;
		this.arm_rot = MathUtil.Wrap(-180f, 180f, this.arm_rot);
		this.arm_go.transform.rotation = Quaternion.Euler(0f, 0f, this.arm_rot);
		if (!this.rotation_complete)
		{
			this.StartRotateSound();
			this.looping_sounds.SetParameter(this.rotateSound, AutoMiner.HASH_ROTATION, this.arm_rot);
			return;
		}
		this.StopRotateSound();
	}

	// Token: 0x06002B62 RID: 11106 RVA: 0x000FAB85 File Offset: 0x000F8D85
	private void StartRotateSound()
	{
		if (!this.rotate_sound_playing)
		{
			this.looping_sounds.StartSound(this.rotateSound);
			this.rotate_sound_playing = true;
		}
	}

	// Token: 0x06002B63 RID: 11107 RVA: 0x000FABA8 File Offset: 0x000F8DA8
	private void StopRotateSound()
	{
		if (this.rotate_sound_playing)
		{
			this.looping_sounds.StopSound(this.rotateSound);
			this.rotate_sound_playing = false;
		}
	}

	// Token: 0x04001995 RID: 6549
	private static HashedString HASH_ROTATION = "rotation";

	// Token: 0x04001996 RID: 6550
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001997 RID: 6551
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04001998 RID: 6552
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001999 RID: 6553
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x0400199A RID: 6554
	[MyCmpReq]
	private MiningSounds mining_sounds;

	// Token: 0x0400199B RID: 6555
	public int x;

	// Token: 0x0400199C RID: 6556
	public int y;

	// Token: 0x0400199D RID: 6557
	public int width;

	// Token: 0x0400199E RID: 6558
	public int height;

	// Token: 0x0400199F RID: 6559
	public CellOffset vision_offset;

	// Token: 0x040019A0 RID: 6560
	private KBatchedAnimController arm_anim_ctrl;

	// Token: 0x040019A1 RID: 6561
	private GameObject arm_go;

	// Token: 0x040019A2 RID: 6562
	private LoopingSounds looping_sounds;

	// Token: 0x040019A3 RID: 6563
	private string rotateSoundName = "AutoMiner_rotate";

	// Token: 0x040019A4 RID: 6564
	private EventReference rotateSound;

	// Token: 0x040019A5 RID: 6565
	private KAnimLink link;

	// Token: 0x040019A6 RID: 6566
	private float arm_rot = 45f;

	// Token: 0x040019A7 RID: 6567
	private float turn_rate = 180f;

	// Token: 0x040019A8 RID: 6568
	private bool rotation_complete;

	// Token: 0x040019A9 RID: 6569
	private bool rotate_sound_playing;

	// Token: 0x040019AA RID: 6570
	private GameObject hitEffectPrefab;

	// Token: 0x040019AB RID: 6571
	private GameObject hitEffect;

	// Token: 0x040019AC RID: 6572
	private int dig_cell = Grid.InvalidCell;

	// Token: 0x040019AD RID: 6573
	private static readonly EventSystem.IntraObjectHandler<AutoMiner> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<AutoMiner>(delegate(AutoMiner component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x02001560 RID: 5472
	public class Instance : GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.GameInstance
	{
		// Token: 0x06009100 RID: 37120 RVA: 0x00362584 File Offset: 0x00360784
		public Instance(AutoMiner master)
			: base(master)
		{
		}
	}

	// Token: 0x02001561 RID: 5473
	public class States : GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner>
	{
		// Token: 0x06009101 RID: 37121 RVA: 0x00362590 File Offset: 0x00360790
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.DoNothing();
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (AutoMiner.Instance smi) => smi.GetComponent<Operational>().IsOperational);
			this.on.DefaultState(this.on.idle).EventTransition(GameHashes.OperationalChanged, this.off, (AutoMiner.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.on.idle.PlayAnim("on").EventTransition(GameHashes.ActiveChanged, this.on.moving, (AutoMiner.Instance smi) => smi.GetComponent<Operational>().IsActive);
			this.on.moving.Exit(delegate(AutoMiner.Instance smi)
			{
				smi.master.StopRotateSound();
			}).PlayAnim("working").EventTransition(GameHashes.ActiveChanged, this.on.idle, (AutoMiner.Instance smi) => !smi.GetComponent<Operational>().IsActive)
				.Update(delegate(AutoMiner.Instance smi, float dt)
				{
					smi.master.UpdateRotation(dt);
				}, UpdateRate.SIM_33ms, false)
				.Transition(this.on.digging, new StateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.Transition.ConditionCallback(AutoMiner.States.RotationComplete), UpdateRate.SIM_200ms);
			this.on.digging.Enter(delegate(AutoMiner.Instance smi)
			{
				smi.master.StartDig();
			}).Exit(delegate(AutoMiner.Instance smi)
			{
				smi.master.StopDig();
			}).PlayAnim("working")
				.EventTransition(GameHashes.ActiveChanged, this.on.idle, (AutoMiner.Instance smi) => !smi.GetComponent<Operational>().IsActive)
				.Update(delegate(AutoMiner.Instance smi, float dt)
				{
					smi.master.UpdateDig(dt);
				}, UpdateRate.SIM_200ms, false)
				.Transition(this.on.moving, GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.Not(new StateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.Transition.ConditionCallback(AutoMiner.States.RotationComplete)), UpdateRate.SIM_200ms);
		}

		// Token: 0x06009102 RID: 37122 RVA: 0x0036280C File Offset: 0x00360A0C
		public static bool RotationComplete(AutoMiner.Instance smi)
		{
			return smi.master.RotationComplete;
		}

		// Token: 0x04006F83 RID: 28547
		public StateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.BoolParameter transferring;

		// Token: 0x04006F84 RID: 28548
		public GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State off;

		// Token: 0x04006F85 RID: 28549
		public AutoMiner.States.ReadyStates on;

		// Token: 0x02002762 RID: 10082
		public class ReadyStates : GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State
		{
			// Token: 0x0400ADE5 RID: 44517
			public GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State idle;

			// Token: 0x0400ADE6 RID: 44518
			public GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State moving;

			// Token: 0x0400ADE7 RID: 44519
			public GameStateMachine<AutoMiner.States, AutoMiner.Instance, AutoMiner, object>.State digging;
		}
	}
}
