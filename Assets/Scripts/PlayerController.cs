using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {


	public float speed;
	public Boundary boundary;
	public float tilt;

	public GameObject shot;
	public Transform shooterSpaw;
	public float fireRate;
	public GameObject controles;

	private float nextFire;
	private float moveHorizontal;
	private float moveVertical;

	private int auxDirecaoVertical;
	private int auxDirecaoHorizontal;

	// Use this for initialization
	void Start () {
		
		if (PlayerPrefs.GetInt (Constantes.USAR_CONTROLE) == 1)
			controles.SetActive (true);

	}

	//void OnGUI () {

		//Rigidbody rb = GetComponent<Rigidbody>();
		//GameObject player = GameObject.FindWithTag ("Player");

		//if (GUI.Button (new Rect (Screen.width - 90f, Screen.height - 84f, 80f, 80f), "O")) {
		//	Shoot ();
		//}
		//else if (GUI.Button (new Rect (90f,  Screen.height - 84f, 80f, 80f), "->")) {
			//Debug.Log ("posicao atual: -->" + player.transform.position.x);
			//player.transform.position.x += 10f;
		//	moveHorizontal = player.transform.position.x + 0.5f;
			//Debug.Log ("posicao atual: -->" + moveHorizontal);

			/*Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			Rigidbody rb = player.GetComponent<Rigidbody>();
			player.GetComponent<Rigidbody>().velocity = movement * speed;

			player.GetComponent<Rigidbody>().position = new Vector3 
				(
					Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
					0.0f,
					Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
				);

			rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);*/

		//}
		//else if (GUI.Button (new Rect (4f,  Screen.height - 84f, 80f, 80f), "<-")) {
			//Debug.Log ("posicao atual <--: " + player.transform.position.x);
		//	moveHorizontal = player.transform.position.x - 0.5f;
			//Debug.Log ("posicao atual: <--" + moveHorizontal);

			//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			/*Rigidbody rb = GetComponent<Rigidbody>();
			rb.velocity = movement * speed;

			rb.position = new Vector3 
				(
					Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
					0.0f,
					Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
				);

			rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);*/
		//}

		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//player.transform.position = movement;
	//}

	// Update is called once per frame
	/*void Update () {
		
		/*if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			moveHorizontal = touch.position.x;
			//moveVertical = touch.position.z;
		}
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			Shoot ();
		}

	}*/


	void FixedUpdate ()
	{
		//      float moveHorizontal = Input.GetAxis ("Horizontal");
		//      float moveVertical = Input.GetAxis ("Vertical");

		//      Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		//      Vector3 accelerationRaw = Input.acceleration;
		//      Vector3 acceleration = FixAcceleration (accelerationRaw);
		//      Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);
		if (controles.activeSelf)
			return;

		Rigidbody rigidbody = GetComponent<Rigidbody>();
		//Vector2 direction = Input.a touchPad.GetDirection ();
		Vector3 acceleration = Input.acceleration;

		Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);
		rigidbody.velocity = movement * (speed*2);

		rigidbody.position = new Vector3
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			);

		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

	void Update () 
	{
		if (!controles.activeSelf)
			return;
		
		if ((auxDirecaoVertical == 1 && transform.position.z <= 9) || (auxDirecaoVertical == -1 && transform.position.z >= -3.5f)) {
			transform.Translate (0, 0, speed * Time.deltaTime * auxDirecaoVertical);
		}

		if ((auxDirecaoHorizontal == 1 && transform.position.x <= 5) || (auxDirecaoHorizontal == -1 && transform.position.x >= -5)) {
			transform.Translate (speed * Time.deltaTime * auxDirecaoHorizontal, 0, 0);
		}
	}

	public void MoveVertical(int direcao)
	{
		auxDirecaoVertical = direcao;
	}

	public void MoveHorizontal(int direcao)
	{
		auxDirecaoHorizontal = direcao;
	}

	public void Shoot()
	{
		nextFire = Time.time + fireRate;
		Instantiate (shot, shooterSpaw.position, shooterSpaw.rotation);

		AudioSource audio = GetComponent<AudioSource>();

		audio.Play();
	}

}
