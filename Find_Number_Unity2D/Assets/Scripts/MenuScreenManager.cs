using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuScreenManager : MonoBehaviour, MPLobbyListener {

    public bool singlePlay;
    public bool multiPlay;
    public bool isSoundOn;
    public bool shareGame;
    public bool banAds;

    public List<Sprite> soundOn;
    public List<Sprite> soundOff;

    public List<Sprite> banAdsOn;
    public List<Sprite> banAdsOff;

    private bool _showLobbyDialog;
    private string _lobbyMessage;


	// Use this for initialization
	void Start () {
        SetFalse();
	}

    public void SetLobbyStatusMessage(string message)
    {
        _lobbyMessage = message;
    }

    public void HideLobby()
    {
        _lobbyMessage = "";
        _showLobbyDialog = false;
    }

    //Set false tất cả các biến trước khi động đến nó
    void SetFalse()
    {
        singlePlay = false;
        multiPlay = false;
        isSoundOn = true;
        shareGame = false;
        banAds = false;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    //Sự kiện button Advance click
    public void ButtonAdvanceClicked()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        GameController.Instance.AdvantureClick();
    }

    //Sự kiện button Multiplayer click
    public void ButtonMultiplayerClicked()
    {
        GameController.Instance.MultiplayerClick();
        _lobbyMessage = "Starting a multiplayer game...";
        _showLobbyDialog = true;
        MultiplayerController.Instance.lobbyListener = this;
        MultiplayerController.Instance.SignInAndStartMPGame();
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
    }


    //Sự kiện click button Sign out
    public void ButtonSignOutClick()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        GameController.Instance.SignOutClick();
    }

    //Sự kiện button sound click
    public void ButtonSoundClicked(Button btn)
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        if (btn.GetComponent<Image>().sprite == soundOn[0])
        {
            isSoundOn = false;
            btn.GetComponent<Image>().sprite = soundOff[0];
            SpriteState spriteState = new SpriteState();
            spriteState.pressedSprite = soundOff[1];
            btn.GetComponent<Button>().spriteState = spriteState;
        }
        else
        {
            isSoundOn = true;
            btn.GetComponent<Image>().sprite = soundOn[0];
            SpriteState spriteState = new SpriteState();
            spriteState.pressedSprite = soundOn[1];
            btn.GetComponent<Button>().spriteState = spriteState;
        }
    }

    //Sự kiện button rate click
    public void ButtonRateClicked()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
    }

    //Sự kiện button leaderboard click
    public void ButtonLeaderBoardClicked()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
    }

    //Sự kiện button share click
    public void ButtonShareClicked()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
    }

    //Sự kiện button banAds click
    public void ButtonBanAdsClicked(Button btn)
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        if (banAds)
        {
            btn.GetComponent<Image>().sprite = banAdsOff[0];
            SpriteState spriteState = new SpriteState();
            spriteState.pressedSprite = banAdsOff[1];
            btn.GetComponent<Button>().spriteState = spriteState;
        }
        else
        {
            btn.GetComponent<Image>().sprite = banAdsOn[0];
            SpriteState spriteState = new SpriteState();
            spriteState.pressedSprite = banAdsOn[1];
            btn.GetComponent<Button>().spriteState = spriteState;
        }
    }
}
