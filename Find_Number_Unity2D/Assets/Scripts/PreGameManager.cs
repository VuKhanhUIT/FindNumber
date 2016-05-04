using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Security.Policy;

public class PreGameManager : MonoSingleton<PreGameManager>
{

    public Text text;

    public bool backClick;

    public List<GameObject> listOfButton = new List<GameObject>();

    public bool isEnabled = false;

    public Sprite starOpen;

	// Use this for initialization
	void Start ()
	{
        //PlayerPrefs.DeleteAll();
        ResourceManager.Instance.LoadJsonFile();
        GameController.Instance.LoadJsonNode();
	    GameController.Instance.listButtonLevel = listOfButton;
        GameController.Instance.Initialize();
        Loading();
        MultiplayerController.Instance.TrySilentSignIn();
	}

    public void Loading()
    {
        GameController.Instance.LoadTime();
        GameController.Instance.LoadStar();
        GameController.Instance.LoadTimeArrayTemp();
        GameController.Instance.CheckRequirementStar(listOfButton);
        GameController.Instance.CheckRequirementTime(listOfButton);
        GameController.Instance.LoadStarThenAssignToLevelButton(listOfButton, starOpen);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (gameObject.GetComponent<Canvas>().enabled)
	    {
	        Loading();
	    }
	}

    public void BackButtonClicked()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        GameController.Instance.BackButtonCliked();
    }

    public void ButtonLevelClick(Button btn)
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        //GameController.Instance.idLevel = btn.GetComponent<ButtonLevelManager>().id;
        GameController.Instance.ButtonLevelClick(btn);
        GameController.Instance.EnableGameScreen();
    }
}
