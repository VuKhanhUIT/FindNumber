using UnityEngine;
using System.Collections;

public class SquareButtonManager : MonoBehaviour {

	public static SquareButtonManager Instance { get; private set; }
	public bool isRight = false;
    public int buttonId;

	void Awake()
	{
		Instance = this;
	}

    public int GetButtonId()
    {
        return this.buttonId;
    }

    public void SetButtonId(int value)
    {
        this.buttonId = value;
    }

	public void ButtonPressed()
	{
        GameScreenManager.Instance.buttonId = GetButtonId();
        Debug.Log("buttonId " + buttonId);
        if (GameScreenManager.Instance.CheckButtonPressed())
        {
            if (!GameScreenManager.Instance.IsFinished())
            {
                GameScreenManager.Instance.DisplayAllButton();
            }
            else
            {
                GameController.Instance.isWin = true;
                GameController.Instance.EnableGameOverScreen();
                PreGameManager.Instance.isEnabled = true;
                AudioSource.PlayClipAtPoint(SoundController.Instance.audio[1], transform.position);
            }
        }
	}
}
