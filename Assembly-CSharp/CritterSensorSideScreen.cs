using System;
using UnityEngine;

// Token: 0x02000DEE RID: 3566
public class CritterSensorSideScreen : SideScreenContent
{
	// Token: 0x060070AB RID: 28843 RVA: 0x002ADCA2 File Offset: 0x002ABEA2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.countCrittersToggle.onClick += this.ToggleCritters;
		this.countEggsToggle.onClick += this.ToggleEggs;
	}

	// Token: 0x060070AC RID: 28844 RVA: 0x002ADCD8 File Offset: 0x002ABED8
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicCritterCountSensor>() != null;
	}

	// Token: 0x060070AD RID: 28845 RVA: 0x002ADCE8 File Offset: 0x002ABEE8
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetSensor = target.GetComponent<LogicCritterCountSensor>();
		this.crittersCheckmark.enabled = this.targetSensor.countCritters;
		this.eggsCheckmark.enabled = this.targetSensor.countEggs;
	}

	// Token: 0x060070AE RID: 28846 RVA: 0x002ADD34 File Offset: 0x002ABF34
	private void ToggleCritters()
	{
		this.targetSensor.countCritters = !this.targetSensor.countCritters;
		this.crittersCheckmark.enabled = this.targetSensor.countCritters;
	}

	// Token: 0x060070AF RID: 28847 RVA: 0x002ADD65 File Offset: 0x002ABF65
	private void ToggleEggs()
	{
		this.targetSensor.countEggs = !this.targetSensor.countEggs;
		this.eggsCheckmark.enabled = this.targetSensor.countEggs;
	}

	// Token: 0x04004D89 RID: 19849
	public LogicCritterCountSensor targetSensor;

	// Token: 0x04004D8A RID: 19850
	public KToggle countCrittersToggle;

	// Token: 0x04004D8B RID: 19851
	public KToggle countEggsToggle;

	// Token: 0x04004D8C RID: 19852
	public KImage crittersCheckmark;

	// Token: 0x04004D8D RID: 19853
	public KImage eggsCheckmark;
}
