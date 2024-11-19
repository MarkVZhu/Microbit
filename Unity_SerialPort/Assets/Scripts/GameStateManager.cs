using UnityEngine;
using MarkFramework;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Manage Game State with FSM
/// </summary>
public class GameStateManager : SingletonMono<GameStateManager>
{
	public enum GameState
	{
		Beginning,
		Playing,
		Pause,
		Success,
		Fail,
		End
	}
	
	public GameState currentState;
	public bool isPlaying;
	public SaveData saveData;
	private GameObject player;

	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		saveData.defaultPosition = player.transform.position;
		
		player.transform.position = saveData.lastSavePosition;
		
		UIManager.Instance.ShowPanel<MainPanel>("MainPanel");
		currentState = GameState.Beginning;
		EnterState(currentState);
	}
	
	void Update()
	{
		// 在这里可以处理不同状态下的逻辑
		switch (currentState)
		{
			case GameState.Beginning:
				//CheckGameStart();
				break;
			case GameState.Success:
				// 处理End状态下的逻辑
				break;
		}
	}
	
	//这里把ConfirmState的作用也融合到了EnterState
	//该方法兼顾离开状态时执行的方法和进入状态时执行的方法
	public void EnterState(GameState state)
	{
		//离开当前状态前执行的逻辑
		switch (currentState)
		{
			case GameState.Beginning:
				break;
			case GameState.Playing:
				break;
			case GameState.Pause:
				break;
		}
		
		//进入目标状态后执行的逻辑
		switch (state)
		{
			case GameState.Beginning:
				currentState = state;
				isPlaying = false;
				break;
			case GameState.Playing:
				currentState = state;
				isPlaying = true;
				break;
			case GameState.Pause:
				currentState = state;
				isPlaying = false;
				break;
			case GameState.Success:
				currentState = state;
				isPlaying = false;
				//UIManager.Instance.ShowPanel<SuccessPanel>("SuccessPanel");
				Invoke("DelayShowSuccessResult", 1.7f);					
				break;
			case GameState.Fail:
				currentState = state;
				isPlaying = false;
				//UIManager.Instance.ShowPanel<ResultPanel>("ResultPanel");	
				Invoke("DelayShowResult", 1f);			
				break;
			case GameState.End:
				currentState = state;
				isPlaying = false;	
				break;
		}
	}
	
	void DelayShowResult()
	{
		UIManager.Instance.ShowPanel<ResultPanel>("ResultPanel");
	}
	
	void DelayShowSuccessResult()
	{
		UIManager.Instance.ShowPanel<SuccessPanel>("SuccessPanel");
	}
	
	// // 确定当前状态并进入下一个状态的方法
	// public void ConfirmState()
	// { 
	// 	switch (currentState)
	// 	{
	// 		case GameState.Playing:
	// 			break;
	// 		case GameState.Pause:
	// 			break;
	// 	}

	// 	// 进入新的状态
	// 	EnterState(currentState);
	// }
	
	public void UpdateLastSavePosition()
	{
		saveData.lastSavePosition = player.transform.position;
	}
	

	private void GameOver(string winningPlayer)
	{
		currentState = GameState.Success;
		EnterState(GameState.Success);
	}

	/// <summary>
	/// Restarts the game state
	/// </summary>
	public void RestartGame()
	{
		//TODO: 重新开始逻辑

		SceneManager.LoadScene(SceneManager.GetActiveScene().name); //TODO:换成不需要Reload当前关卡的方法
	}


	// // 等待所有小球停止移动的协程
	// IEnumerator WaitForBallsToStop(List<Rigidbody> rgs)
	// {
	// 	// 持续每帧检查，直到所有小球停止
	// 	while (!CheckBallsMoving(rgs))
	// 	{
	// 		// 等待一帧再继续检查
	// 		yield return null;
	// 	}
	// }

	//包括重新加载时调用，记得Remove Listener!
	private void OnDestroy() 
	{
		
	}
}
