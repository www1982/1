using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000528 RID: 1320
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/KAnimSequencer")]
public class KAnimSequencer : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06001CC6 RID: 7366 RVA: 0x0009B8E5 File Offset: 0x00099AE5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.kbac = base.GetComponent<KBatchedAnimController>();
		this.mb = base.GetComponent<MinionBrain>();
		if (this.autoRun)
		{
			this.PlaySequence();
		}
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x0009B913 File Offset: 0x00099B13
	public void Reset()
	{
		this.currentIndex = 0;
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x0009B91C File Offset: 0x00099B1C
	public void PlaySequence()
	{
		if (this.sequence != null && this.sequence.Length != 0)
		{
			if (this.mb != null)
			{
				this.mb.Suspend("AnimSequencer");
			}
			this.kbac.onAnimComplete += this.PlayNext;
			this.PlayNext(null);
		}
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x0009B97C File Offset: 0x00099B7C
	private void PlayNext(HashedString name)
	{
		if (this.sequence.Length > this.currentIndex)
		{
			this.kbac.Play(new HashedString(this.sequence[this.currentIndex].anim), this.sequence[this.currentIndex].mode, this.sequence[this.currentIndex].speed, 0f);
			this.currentIndex++;
			return;
		}
		this.kbac.onAnimComplete -= this.PlayNext;
		if (this.mb != null)
		{
			this.mb.Resume("AnimSequencer");
		}
	}

	// Token: 0x040010D3 RID: 4307
	[Serialize]
	public bool autoRun;

	// Token: 0x040010D4 RID: 4308
	[Serialize]
	public KAnimSequencer.KAnimSequence[] sequence = new KAnimSequencer.KAnimSequence[0];

	// Token: 0x040010D5 RID: 4309
	private int currentIndex;

	// Token: 0x040010D6 RID: 4310
	private KBatchedAnimController kbac;

	// Token: 0x040010D7 RID: 4311
	private MinionBrain mb;

	// Token: 0x02001386 RID: 4998
	[SerializationConfig(MemberSerialization.OptOut)]
	[Serializable]
	public class KAnimSequence
	{
		// Token: 0x04006992 RID: 27026
		public string anim;

		// Token: 0x04006993 RID: 27027
		public float speed = 1f;

		// Token: 0x04006994 RID: 27028
		public KAnim.PlayMode mode = KAnim.PlayMode.Once;
	}
}
