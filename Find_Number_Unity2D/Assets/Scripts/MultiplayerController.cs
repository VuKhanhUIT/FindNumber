using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MultiplayerController : RealTimeMultiplayerListener
{

    private static MultiplayerController _instance;
    private readonly uint gameVariation = 0;
    private readonly uint maximumOpponents = 1;
    private readonly uint minimumOpponents = 1;

    //public MenuScreenManager menuScreen;

    public MPLobbyListener lobbyListener; 

    private MultiplayerController()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public static MultiplayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MultiplayerController();
            }
            return _instance;
        }
    }

    public List<Participant> GetAllParticipants()
    {
        return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
    }

    public string GetMyParticipantId()
    {
        return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
    }

    public void SignInAndStartMPGame()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    PreGameManager.Instance.text.text = "We're sign in! Welcome " + PlayGamesPlatform.Instance.localUser.userName;
                    //Start match making here
                    StartMatchMaking();
                }
                else
                {
                    PreGameManager.Instance.text.text = "Oh... we're not signed in.";
                }
            });
        }
        else
        {
            PreGameManager.Instance.text.text = "You're already signed in.";
            //Start match making here
            StartMatchMaking();
        }
    }

    public void TrySilentSignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Authenticate((bool success) =>
            {
                if (success)
                {
                    PreGameManager.Instance.text.text = "Silently signed in! Welcome " + PlayGamesPlatform.Instance.localUser.userName;
                }
                else
                {
                    PreGameManager.Instance.text.text = "Oh... we're not signed in";
                }
            }, true);
        }
        else
        {
            PreGameManager.Instance.text.text = "We're already signed in";
        }
    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
        PreGameManager.Instance.text.text = "We're signed out!";
    }

    public bool IsAuthenticated()
    {
        return PlayGamesPlatform.Instance.localUser.authenticated;
    }

    public void StartMatchMaking()
    {
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minimumOpponents, maximumOpponents, gameVariation, this);
    }

    public void OnRoomSetupProgress(float percent)
    {
        ShowPSStatus("We are " + percent + "% done with setup");
    }

    public void OnRoomConnected(bool success)
    {
        if (success)
        {
            ShowPSStatus("We are connected to the room! I would probably start our game now");
            lobbyListener.HideLobby();
            lobbyListener = null;

        }
        else
        {
            ShowPSStatus("Uh-oh. Encountered some error connecting to the room");
        }
    }

    public void OnLeftRoom()
    {
        ShowPSStatus("We have left the room. We should probably perform some clean-up tasks.");
    }

    public void OnParticipantLeft(Participant participant)
    {
        throw new System.NotImplementedException();
    }

    public void OnPeersConnected(string[] participantIds)
    {
        foreach (string participantId in participantIds)
        {
            ShowPSStatus("Player " + participantId + " has joined.");
        }
    }

    public void OnPeersDisconnected(string[] participantIds)
    {
        foreach (string participantId in participantIds)
        {
            ShowPSStatus("Player " + participantId + " has left.");
        }
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        ShowPSStatus("We have received some gameplay messages from participant ID: " + senderId);
    }

    private void ShowPSStatus(string message)
    {
        PreGameManager.Instance.text.text = message;
        if (lobbyListener != null)
        {
            lobbyListener.SetLobbyStatusMessage(message);
        }
    }
}