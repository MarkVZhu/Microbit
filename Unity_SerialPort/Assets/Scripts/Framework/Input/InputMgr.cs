using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkFramework
{
	public class InputMgr : BaseManager<InputMgr>
	{
		private bool isStart = false;
		public InputMgr()
		{
			MonoManager.Instance.AddUpdateListener(MyUpdate);
		}
		
		public void StartOrEndCheck(bool isOpen)
		{
			isStart = isOpen;
		}
		
		public void InitiateInputMgr()
		{
			//Initiate Input Manager in case no other functions are invoked 
		}
		
		private void MyUpdate()
		{
			if(!GameStateManager.Instance.isPlaying)
				return;
			CheckKeyCode(KeyCode.Space);
			CheckKeyCode(KeyCode.D);
		}
		
		private void CheckKeyCode(KeyCode key)
		{
			if(Input.GetKeyDown(key) && key == KeyCode.D)
				EventCenter.Instance.EventTrigger(E_EventType.E_Micro_Input, "S");
			if(Input.GetKeyDown(key) && key == KeyCode.Space)
				EventCenter.Instance.EventTrigger(E_EventType.E_Micro_Input, "V5");
			// if(Input.GetKeyUp(key))
			// 	EventCenter.Instance.EventTrigger(E_EventType.E_Key_Up, key);
		}
	}
}
