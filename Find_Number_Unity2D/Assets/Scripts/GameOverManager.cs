using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{

    public GameObject txtResult;

    public List<GameObject> listStar = new List<GameObject>();

    public List<Sprite> starOpen;

    public List<Sprite> starClose; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    if (GameController.Instance.isWin == true)
	    {
	        GameController.Instance.SetTimeForGameOverScreen(txtResult);
            GameController.Instance.DisplayStar(listStar, starOpen);
	        GameController.Instance.isWin = false;
	    }
	}

    //Sự kiện click button menu
    public void ButtonMenuClicked()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        GameController.Instance.MainMenuClicked();
        ResetStarToCloseStar();
    }

    //Sự kiện click button replay
    public void ButtonReplayClicked()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        GameController.Instance.ReplayClicked();
        GameController.Instance.EnableGameScreen();
        ResetStarToCloseStar();
    }

    //Sự kiện click button next
    public void ButtonNextClicked()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        GameController.Instance.NextLevelClicked();
        GameController.Instance.EnableGameScreen();
        ResetStarToCloseStar();
    }

    public void ResetStarToCloseStar()
    {
        for (int i = 0; i < listStar.Count; i++)
        {
            listStar[i].GetComponent<Image>().sprite = starClose[i];
        }
    }
}
