using System.Collections;
using System.Collections.Generic;
using MarkFramework;
using UnityEngine;

public class SaveZone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) 
	{
		float distance = Vector2.Distance(transform.position, GameStateManager.Instance.saveData.lastSavePosition);
		if (distance < 3) return;
		Debug.Log("Save Anim");
		Animator anim = GetComponentInChildren<Animator>();
		if(anim != null) anim.SetTrigger("Save");
		SoundMgr.Instance.PlaySound("Save");
		GameStateManager.Instance.UpdateLastSavePosition();
	}
}
