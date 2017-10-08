using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait, startWait, waveWait;
	public RectTransform painel;

	//Score
	private string scoreText;
	private int score;
	private AudioSource somBG;
	public int record;

	//Level
	int faseAtual = 0;
	int proxFase = 1;

	void OnGUI () {

		if (SceneManager.GetActiveScene ().name != "Menu") {
			GUIStyle style = new GUIStyle ();
			style.normal.textColor = Color.white;
			//style.font = new Font ();
			//style.font.material.color = new Color (255, 255, 255);
			style.fontSize = 32;

			GUI.Label (new Rect (10f, 4f, 100f, 20f), "Record: " + record, style);

			GUI.Label (new Rect (10f, 40f, 100f, 20f), "Pontos: " + score, style);
		}
	}

	void Start()
	{
		score = 0;

		record = PlayerPrefs.GetInt (Constantes.PONTOS_USUARIO);

		somBG = GetComponent<AudioSource>();

		somBG.volume = (PlayerPrefs.GetFloat (Constantes.VOLUME) / 10);


		UpdateScore ();
		StartCoroutine(SpawnWaves ());
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);

		while (true) {
			
			for (int i = 0; i < hazardCount; i++) {

				//Passando de level
				if (score > 0 && score % 100 == 0 && (faseAtual < proxFase)) {
					faseAtual = proxFase;
					hazardCount = hazardCount + 3;
					spawnWait = spawnWait - 0.5f;
					break;
				}

				GameObject hazard = hazards[Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-5f, 5f), spawnValues.y, spawnValues.z);
				Quaternion SpawnRotation = Quaternion.identity;

				Instantiate (hazard, spawnPosition, SpawnRotation);

				/*Vector3 enemyPosition = new Vector3 (Random.Range (-5f, 5f), spawnValues.y, spawnValues.z);
				Quaternion enemyRotation = new Quaternion();
				enemyRotation.x = 0;
				enemyRotation.y = 0;
				enemyRotation.z = 0;

				Instantiate (enemy, enemyPosition, enemyRotation);*/

				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);
		}
		
	}

	public void AddScore(int newScore)
	{
		score += newScore;
		UpdateScore ();

		if (faseAtual == proxFase)
			proxFase++;
	}

	void UpdateScore()
	{
		if (score > record) {
			record = score;
			PlayerPrefs.SetInt(Constantes.PONTOS_USUARIO, record);
		}
	}


	public void GameOver()
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
