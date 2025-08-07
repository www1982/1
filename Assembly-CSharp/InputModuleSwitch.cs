using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000BEE RID: 3054
public class InputModuleSwitch : MonoBehaviour
{
	// Token: 0x06005C0A RID: 23562 RVA: 0x00217AFC File Offset: 0x00215CFC
	private void Update()
	{
		if (this.lastMousePosition != Input.mousePosition && KInputManager.currentControllerIsGamepad)
		{
			KInputManager.currentControllerIsGamepad = false;
			KInputManager.InputChange.Invoke();
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			this.virtualInput.enabled = KInputManager.currentControllerIsGamepad;
			if (this.standaloneInput.enabled)
			{
				this.standaloneInput.enabled = false;
				this.ChangeInputHandler();
				return;
			}
		}
		else
		{
			this.lastMousePosition = Input.mousePosition;
			this.standaloneInput.enabled = true;
			if (this.virtualInput.enabled)
			{
				this.virtualInput.enabled = false;
				this.ChangeInputHandler();
			}
		}
	}

	// Token: 0x06005C0B RID: 23563 RVA: 0x00217BA0 File Offset: 0x00215DA0
	private void ChangeInputHandler()
	{
		GameInputManager inputManager = Global.GetInputManager();
		for (int i = 0; i < inputManager.usedMenus.Count; i++)
		{
			if (inputManager.usedMenus[i].Equals(null))
			{
				inputManager.usedMenus.RemoveAt(i);
			}
		}
		if (inputManager.GetControllerCount() > 1)
		{
			if (KInputManager.currentControllerIsGamepad)
			{
				Cursor.visible = false;
				inputManager.GetController(1).inputHandler.TransferHandles(inputManager.GetController(0).inputHandler);
				return;
			}
			Cursor.visible = true;
			inputManager.GetController(0).inputHandler.TransferHandles(inputManager.GetController(1).inputHandler);
		}
	}

	// Token: 0x04003CF3 RID: 15603
	public VirtualInputModule virtualInput;

	// Token: 0x04003CF4 RID: 15604
	public StandaloneInputModule standaloneInput;

	// Token: 0x04003CF5 RID: 15605
	private Vector3 lastMousePosition;
}
