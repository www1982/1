using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000647 RID: 1607
public class TransitionDriver
{
	// Token: 0x170001BD RID: 445
	// (get) Token: 0x060026B1 RID: 9905 RVA: 0x000DC67F File Offset: 0x000DA87F
	private Action<object> onAnimCompleteBinding
	{
		get
		{
			if (this.onAnimComplete_ == null)
			{
				this.onAnimComplete_ = new Action<object>(this.OnAnimComplete);
			}
			return this.onAnimComplete_;
		}
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000DC6A1 File Offset: 0x000DA8A1
	public Navigator.ActiveTransition GetTransition
	{
		get
		{
			return this.transition;
		}
	}

	// Token: 0x060026B3 RID: 9907 RVA: 0x000DC6A9 File Offset: 0x000DA8A9
	public TransitionDriver(Navigator navigator)
	{
		this.log = new LoggerFS("TransitionDriver", 35);
	}

	// Token: 0x060026B4 RID: 9908 RVA: 0x000DC6DC File Offset: 0x000DA8DC
	public void BeginTransition(Navigator navigator, NavGrid.Transition transition, float defaultSpeed)
	{
		Navigator.ActiveTransition instance = TransitionDriver.TransitionPool.GetInstance();
		instance.Init(transition, defaultSpeed);
		this.BeginTransition(navigator, instance);
	}

	// Token: 0x060026B5 RID: 9909 RVA: 0x000DC704 File Offset: 0x000DA904
	private void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		bool flag = this.interruptOverrideStack.Count != 0;
		foreach (TransitionDriver.OverrideLayer overrideLayer in this.overrideLayers)
		{
			if (!flag || !(overrideLayer is TransitionDriver.InterruptOverrideLayer))
			{
				overrideLayer.BeginTransition(navigator, transition);
			}
		}
		this.navigator = navigator;
		this.transition = transition;
		this.isComplete = false;
		Grid.SceneLayer sceneLayer = navigator.sceneLayer;
		if (transition.navGridTransition.start == NavType.Tube || transition.navGridTransition.end == NavType.Tube)
		{
			sceneLayer = Grid.SceneLayer.BuildingUse;
		}
		else if (transition.navGridTransition.start == NavType.Solid && transition.navGridTransition.end == NavType.Solid)
		{
			sceneLayer = Grid.SceneLayer.FXFront;
			navigator.animController.SetSceneLayer(sceneLayer);
		}
		else if (transition.navGridTransition.start == NavType.Solid || transition.navGridTransition.end == NavType.Solid)
		{
			navigator.animController.SetSceneLayer(sceneLayer);
		}
		int num = Grid.OffsetCell(Grid.PosToCell(navigator), transition.x, transition.y);
		this.targetPos = this.GetTargetPosition(transition.navGridTransition, num, sceneLayer);
		if (transition.isLooping)
		{
			KAnimControllerBase animController = navigator.animController;
			animController.PlaySpeedMultiplier = transition.animSpeed;
			bool flag2 = transition.preAnim != "";
			bool flag3 = animController.CurrentAnim != null && animController.CurrentAnim.name == transition.anim;
			if (flag2 && animController.CurrentAnim != null && animController.CurrentAnim.name == transition.preAnim)
			{
				animController.ClearQueue();
				animController.Queue(transition.anim, KAnim.PlayMode.Loop, 1f, 0f);
			}
			else if (flag3)
			{
				if (animController.PlayMode != KAnim.PlayMode.Loop)
				{
					animController.ClearQueue();
					animController.Queue(transition.anim, KAnim.PlayMode.Loop, 1f, 0f);
				}
			}
			else if (flag2)
			{
				animController.Play(transition.preAnim, KAnim.PlayMode.Once, 1f, 0f);
				animController.Queue(transition.anim, KAnim.PlayMode.Loop, 1f, 0f);
			}
			else
			{
				animController.Play(transition.anim, KAnim.PlayMode.Loop, 1f, 0f);
			}
		}
		else if (transition.anim != null)
		{
			KBatchedAnimController animController2 = navigator.animController;
			animController2.PlaySpeedMultiplier = transition.animSpeed;
			animController2.Play(transition.anim, KAnim.PlayMode.Once, 1f, 0f);
			navigator.Subscribe(-1061186183, this.onAnimCompleteBinding);
		}
		if (transition.navGridTransition.y != 0)
		{
			if (transition.navGridTransition.start == NavType.RightWall)
			{
				navigator.facing.SetFacing(transition.navGridTransition.y < 0);
			}
			else if (transition.navGridTransition.start == NavType.LeftWall)
			{
				navigator.facing.SetFacing(transition.navGridTransition.y > 0);
			}
		}
		if (transition.navGridTransition.x != 0)
		{
			if (transition.navGridTransition.start == NavType.Ceiling)
			{
				navigator.facing.SetFacing(transition.navGridTransition.x > 0);
			}
			else if (transition.navGridTransition.start != NavType.LeftWall && transition.navGridTransition.start != NavType.RightWall)
			{
				navigator.facing.SetFacing(transition.navGridTransition.x < 0);
			}
		}
		this.brain = navigator.GetComponent<Brain>();
	}

	// Token: 0x060026B6 RID: 9910 RVA: 0x000DCA90 File Offset: 0x000DAC90
	private Vector3 GetTargetPosition(NavGrid.Transition trans, int target_cell, Grid.SceneLayer layer)
	{
		if (trans.useXOffset)
		{
			if (trans.x < 0)
			{
				return Grid.CellToPosRBC(target_cell, layer);
			}
			if (trans.x > 0)
			{
				return Grid.CellToPosLBC(target_cell, layer);
			}
		}
		return Grid.CellToPosCBC(target_cell, layer);
	}

	// Token: 0x060026B7 RID: 9911 RVA: 0x000DCAC4 File Offset: 0x000DACC4
	public void UpdateTransition(float dt)
	{
		if (this.navigator == null)
		{
			return;
		}
		foreach (TransitionDriver.OverrideLayer overrideLayer in this.overrideLayers)
		{
			bool flag = this.interruptOverrideStack.Count != 0;
			bool flag2 = overrideLayer is TransitionDriver.InterruptOverrideLayer;
			if (!flag || !flag2 || this.interruptOverrideStack.Peek() == overrideLayer)
			{
				overrideLayer.UpdateTransition(this.navigator, this.transition);
			}
		}
		if (!this.isComplete && this.transition.isCompleteCB != null)
		{
			this.isComplete = this.transition.isCompleteCB();
		}
		if (this.brain != null)
		{
			bool flag3 = this.isComplete;
		}
		if (this.transition.isLooping)
		{
			float speed = this.transition.speed;
			Vector3 position = this.navigator.transform.GetPosition();
			int num = Grid.PosToCell(position);
			if (this.transition.x > 0)
			{
				position.x += dt * speed;
				if (position.x > this.targetPos.x)
				{
					this.isComplete = true;
				}
			}
			else if (this.transition.x < 0)
			{
				position.x -= dt * speed;
				if (position.x < this.targetPos.x)
				{
					this.isComplete = true;
				}
			}
			else
			{
				position.x = this.targetPos.x;
			}
			if (this.transition.y > 0)
			{
				position.y += dt * speed;
				if (position.y > this.targetPos.y)
				{
					this.isComplete = true;
				}
			}
			else if (this.transition.y < 0)
			{
				position.y -= dt * speed;
				if (position.y < this.targetPos.y)
				{
					this.isComplete = true;
				}
			}
			else
			{
				position.y = this.targetPos.y;
			}
			this.navigator.transform.SetPosition(position);
			int num2 = Grid.PosToCell(position);
			if (num2 != num)
			{
				this.navigator.Trigger(915392638, num2);
			}
		}
		if (this.isComplete)
		{
			this.isComplete = false;
			Navigator navigator = this.navigator;
			navigator.SetCurrentNavType(this.transition.end);
			navigator.transform.SetPosition(this.targetPos);
			this.EndTransition();
			navigator.AdvancePath(true);
		}
	}

	// Token: 0x060026B8 RID: 9912 RVA: 0x000DCD68 File Offset: 0x000DAF68
	public void EndTransition()
	{
		if (this.navigator != null)
		{
			this.interruptOverrideStack.Clear();
			foreach (TransitionDriver.OverrideLayer overrideLayer in this.overrideLayers)
			{
				overrideLayer.EndTransition(this.navigator, this.transition);
			}
			this.navigator.animController.PlaySpeedMultiplier = 1f;
			this.navigator.Unsubscribe(-1061186183, this.onAnimCompleteBinding);
			if (this.brain != null)
			{
				this.brain.Resume("move_handler");
			}
			TransitionDriver.TransitionPool.ReleaseInstance(this.transition);
			this.transition = null;
			this.navigator = null;
			this.brain = null;
		}
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x000DCE50 File Offset: 0x000DB050
	private void OnAnimComplete(object data)
	{
		if (this.navigator != null)
		{
			this.navigator.Unsubscribe(-1061186183, this.onAnimCompleteBinding);
		}
		this.isComplete = true;
	}

	// Token: 0x060026BA RID: 9914 RVA: 0x000DCE7D File Offset: 0x000DB07D
	public static Navigator.ActiveTransition SwapTransitionWithEmpty(Navigator.ActiveTransition src)
	{
		Navigator.ActiveTransition instance = TransitionDriver.TransitionPool.GetInstance();
		instance.Copy(src);
		src.Copy(TransitionDriver.emptyTransition);
		return instance;
	}

	// Token: 0x04001697 RID: 5783
	private static Navigator.ActiveTransition emptyTransition = new Navigator.ActiveTransition();

	// Token: 0x04001698 RID: 5784
	public static ObjectPool<Navigator.ActiveTransition> TransitionPool = new ObjectPool<Navigator.ActiveTransition>(() => new Navigator.ActiveTransition(), 128);

	// Token: 0x04001699 RID: 5785
	private Stack<TransitionDriver.InterruptOverrideLayer> interruptOverrideStack = new Stack<TransitionDriver.InterruptOverrideLayer>(8);

	// Token: 0x0400169A RID: 5786
	private Navigator.ActiveTransition transition;

	// Token: 0x0400169B RID: 5787
	private Navigator navigator;

	// Token: 0x0400169C RID: 5788
	private Vector3 targetPos;

	// Token: 0x0400169D RID: 5789
	private bool isComplete;

	// Token: 0x0400169E RID: 5790
	private Brain brain;

	// Token: 0x0400169F RID: 5791
	public List<TransitionDriver.OverrideLayer> overrideLayers = new List<TransitionDriver.OverrideLayer>();

	// Token: 0x040016A0 RID: 5792
	private LoggerFS log;

	// Token: 0x040016A1 RID: 5793
	private Action<object> onAnimComplete_;

	// Token: 0x020014C8 RID: 5320
	public class OverrideLayer
	{
		// Token: 0x06008EE1 RID: 36577 RVA: 0x0035C8AE File Offset: 0x0035AAAE
		public OverrideLayer(Navigator navigator)
		{
		}

		// Token: 0x06008EE2 RID: 36578 RVA: 0x0035C8B6 File Offset: 0x0035AAB6
		public virtual void Destroy()
		{
		}

		// Token: 0x06008EE3 RID: 36579 RVA: 0x0035C8B8 File Offset: 0x0035AAB8
		public virtual void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
		}

		// Token: 0x06008EE4 RID: 36580 RVA: 0x0035C8BA File Offset: 0x0035AABA
		public virtual void UpdateTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
		}

		// Token: 0x06008EE5 RID: 36581 RVA: 0x0035C8BC File Offset: 0x0035AABC
		public virtual void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
		}
	}

	// Token: 0x020014C9 RID: 5321
	public class InterruptOverrideLayer : TransitionDriver.OverrideLayer
	{
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06008EE6 RID: 36582 RVA: 0x0035C8BE File Offset: 0x0035AABE
		protected bool InterruptInProgress
		{
			get
			{
				return this.originalTransition != null;
			}
		}

		// Token: 0x06008EE7 RID: 36583 RVA: 0x0035C8C9 File Offset: 0x0035AAC9
		public InterruptOverrideLayer(Navigator navigator)
			: base(navigator)
		{
			this.driver = navigator.transitionDriver;
		}

		// Token: 0x06008EE8 RID: 36584 RVA: 0x0035C8DE File Offset: 0x0035AADE
		public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
			this.driver.interruptOverrideStack.Push(this);
			this.originalTransition = TransitionDriver.SwapTransitionWithEmpty(transition);
		}

		// Token: 0x06008EE9 RID: 36585 RVA: 0x0035C900 File Offset: 0x0035AB00
		public override void UpdateTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
			if (!this.IsOverrideComplete())
			{
				return;
			}
			this.driver.interruptOverrideStack.Pop();
			transition.Copy(this.originalTransition);
			TransitionDriver.TransitionPool.ReleaseInstance(this.originalTransition);
			this.originalTransition = null;
			this.EndTransition(navigator, transition);
			this.driver.BeginTransition(navigator, transition);
		}

		// Token: 0x06008EEA RID: 36586 RVA: 0x0035C95F File Offset: 0x0035AB5F
		public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
			base.EndTransition(navigator, transition);
			if (this.originalTransition == null)
			{
				return;
			}
			TransitionDriver.TransitionPool.ReleaseInstance(this.originalTransition);
			this.originalTransition = null;
		}

		// Token: 0x06008EEB RID: 36587 RVA: 0x0035C989 File Offset: 0x0035AB89
		protected virtual bool IsOverrideComplete()
		{
			return this.originalTransition != null && this.driver.interruptOverrideStack.Count != 0 && this.driver.interruptOverrideStack.Peek() == this;
		}

		// Token: 0x04006DC5 RID: 28101
		protected Navigator.ActiveTransition originalTransition;

		// Token: 0x04006DC6 RID: 28102
		protected TransitionDriver driver;
	}
}
