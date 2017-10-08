using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject painelOpcoes;
	public Slider volume;
	public Toggle usarControle;
	
	public void PlayGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("Main");
	}

	public void MenuGame()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void OpcoesGame()
	{
		bool opcao = false;
		volume.value = PlayerPrefs.GetFloat (Constantes.VOLUME);

		if (PlayerPrefs.GetInt (Constantes.USAR_CONTROLE) == 1)
			opcao = true;
		
		PlayerPrefs.SetFloat(Constantes.VOLUME, volume.value);

		if (PlayerPrefs.GetFloat (Constantes.VOLUME) == 0.0f && PlayerPrefs.GetInt (Constantes.IS_MUTE) == 0) {
			PlayerPrefs.SetInt (Constantes.IS_MUTE, 0);
		}

		usarControle.isOn = opcao;

		painelOpcoes.SetActive (true);
	}

	public void VoltarMenuGame()
	{
		painelOpcoes.SetActive (false);
	}

	public void SalvarOpcoes()
	{
		//Salvar
		PlayerPrefs.SetInt (Constantes.USAR_CONTROLE, (usarControle.isOn) ? 1 : 0);
		PlayerPrefs.SetFloat(Constantes.VOLUME, volume.value);

		//Fecha Panel
		painelOpcoes.SetActive(false);
	}

	public void CreditosGame()
	{
		SceneManager.LoadScene ("Creditos");
	}

	public void RestartGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
