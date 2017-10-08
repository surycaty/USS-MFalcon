using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemy : MonoBehaviour {

	private AudioSource audioSouce;
	public GameObject shot;
	public Transform showSpawn;
	public float fireRate, delay;

	// Use this for initialization
	void Start () {
		audioSouce = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire () {
		Instantiate(shot, showSpawn.position, showSpawn.rotation);	
	}

}
