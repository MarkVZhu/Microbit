using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarkFramework;
using System;

public class TestInput : MonoBehaviour
{
	public PlayerController pc;
	
	// Start is called before the first frame update
	void Start()
	{
		//InputMgr.Instance.StartOrEndCheck(true);
		InputMgr.Instance.InitiateInputMgr();
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		EventCenter.Instance.AddEventListener<string>(E_EventType.E_Micro_Input, CheckInput);
	}

	private void CheckInput(String code)
	{
		if(code.Contains("S"))
		{
			pc.MoveForward();
			Debug.Log("Forward");
		}
		if(code.Contains("V"))
		{
			pc.Jump(int.Parse(code[code.Length - 1].ToString()));
			Debug.Log("Jump" + code[code.Length - 1]);
		}
	}
	
	private void OnDestroy() 
	{
		EventCenter.Instance.RemoveEventListener<string>(E_EventType.E_Micro_Input, CheckInput);
	}
}
