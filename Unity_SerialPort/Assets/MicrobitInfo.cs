using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrobitInfo : MonoBehaviour
{
	private UnitySerialPort unitySerialPort;
	// Start is called before the first frame update
	void Start()
	{
		unitySerialPort = GetComponent<UnitySerialPort>();
		unitySerialPort.OpenSerialPort();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
