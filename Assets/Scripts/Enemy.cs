using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour 
{

	public float speed;
	public GameObject explosion;
	public GameObject playExplosion;
	public int scoreValue;
	private GameController gameController;
	public RectTransform painel;

	void Start () {

		Rigidbody rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;

		//rb.angularVelocity = Random.insideUnitSphere * 0.1f;

		Quaternion q = new Quaternion();
		//q.y = 180;
		transform.rotation = q;
	}

	void OnTriggerEnter(Collider other)	
	{
		
		if (other.tag == "Boundary" || other.tag == "Enemy")
			return;

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {
			Instantiate (playExplosion, other.transform.position, other.transform.rotation);
			GameOver ();
		}

		gameController.AddScore (scoreValue);

		Destroy (other.gameObject);
		Destroy (gameObject);

	}


	void GameOver()
	{
		GameObject obj = GameObject.FindWithTag ("PanelGameOver");

		for(int i = 0; i < obj.GetComponents<RectTransform>().Length; i++)
		{
			painel = (RectTransform) obj.GetComponents<RectTransform> ().GetValue (i);
			painel.Find ("Panel").gameObject.SetActive(true);
		}

		Time.timeScale = 0;

	}
}
