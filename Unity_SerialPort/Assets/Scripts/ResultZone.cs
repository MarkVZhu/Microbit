using System.Collections;
using System.Collections.Generic;
using MarkFramework;
using UnityEngine;

public class ResultZone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(tag == "Fail")
		{
			SoundMgr.Instance.PlaySound("die");
			GameStateManager.Instance.EnterState(GameStateManager.GameState.Fail);
		} 
		if(tag == "Success")
		{
			Debug.Log("Start Anim");
			SoundMgr.Instance.PlaySound("Success");
			Animator anim = GetComponentInChildren<Animator>();
			if(anim != null) anim.SetTrigger("Success");
			GameStateManager.Instance.EnterState(GameStateManager.GameState.Success);
		} 
	}
}
