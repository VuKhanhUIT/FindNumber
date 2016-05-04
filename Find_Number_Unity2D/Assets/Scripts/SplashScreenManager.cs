using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour {

    public Image img;

    public float timeDes = 2;
    public float timeRun;

	// Use this for initialization
	void Start () {
        timeRun = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if ((timeRun += Time.deltaTime) >= timeDes)
        {
            GameController.Instance.SplashScreen(img);
        }
        if (timeRun > 3 && timeRun < 4)
        {
            GameController.Instance.fakeScreen.GetComponent<Canvas>().enabled = true;
            gameObject.SetActive(false);
        }
	}

    public void TapToContinue()
    {
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        GameController.Instance.MainMenuClicked();
    }
}
