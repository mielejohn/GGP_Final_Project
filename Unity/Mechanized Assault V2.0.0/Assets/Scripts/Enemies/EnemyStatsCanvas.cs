using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatsCanvas : MonoBehaviour {

	public GameObject Player;
	public Transform playerCameraObj;
	public Camera playerCamera;

	void Start () {

	}

	void Update () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		playerCameraObj = Player.transform.GetChild(0);
		playerCamera = playerCameraObj.gameObject.GetComponent<Camera>();
		transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward, playerCamera.transform.rotation * Vector3.up);
	}
}

