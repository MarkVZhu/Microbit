using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/SaveData", order = 1)]
public class SaveData : ScriptableObject
{
	public Vector3 defaultPosition;
	public Vector3 lastSavePosition;
}