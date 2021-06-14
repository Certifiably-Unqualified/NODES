using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
	public GameObject flowbutton;
	public GameObject controlbutton;

	void Start()
	{
        if (name == "MainGUI")
        {
            flowbutton.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 127, 255);
            controlbutton.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
	}

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        //Debug.Log("game qiuqitugqeog");
        Application.Quit();
    }

    public void FlowMode()
    {
    	PlayerPrefs.SetString("flowmode", "true");
		flowbutton.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 127, 255);
		controlbutton.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
    }

    public void ControlMode()
    {
    	PlayerPrefs.SetString("flowmode", "false");
    	flowbutton.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
		controlbutton.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 127, 255);
    }
}
