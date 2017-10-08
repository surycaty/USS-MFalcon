using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour 
{

	public float tumble;
	public GameObject explosion;
	public GameObject playExplosion;

	//Score
	private GameController gameController;
	private Menu menuGame;
	public int scoreValue;

	void Start() 
	{
		Rigidbody rb = GetComponent<Rigidbody>();

		rb.angularVelocity = Random.insideUnitSphere * tumble;

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
			menuGame = gameControllerObject.GetComponent<Menu> ();
		} else {
			Debug.Log ("Objeto 'GameController' não foi encontrado");
		}
	}

	void OnTriggerEnter(Collider other)	
	{
		
		if (other.tag == "Boundary" || other.tag == "Enemy")
			return;

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {
			//menuGame.RestartGame ();
			Instantiate (playExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}

		gameController.AddScore (scoreValue);

		Destroy (other.gameObject);
		Destroy (gameObject);

	}
}
