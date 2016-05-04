using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SimpleJSON;

public class GameController : MonoSingleton<GameController> {

    public int timer;

    public List<GameObject> listButtonLevel = new List<GameObject>(); 

    public int idLevel;

    public bool isWin = false;
    public bool reActive = false;

    public GameObject pauseScreen;

    public GameObject menuGame;
    public bool menuGameOn;

    public GameObject fakeScreen;

    public GameObject waitingScreen;

    public GameObject preGame;
    public bool preGameOn;

    public GameObject gameScreen;
    public bool gameScreenOn;

    public GameObject gameOver;
    public bool gameOverOn;

    public GameObject backgroundScreen;

    public List<Sprite> listOfRandomSprite = new List<Sprite>();
    public List<GameObject> listOfButton = new List<GameObject>();

    public int amountOfNumber; //Số lượng nút cho mỗi màn chơi
    public int degreeToRotate; //Góc quay
    public int playMode; //Chế độ chơi
    public int[] starArrays;
    public string[] timeArrays;
    public int[] timeArraysTemp;

    public Text coundownText;

    JSONNode jsonNode;

    public Sprite spriteOpen;

    public int totalClick = 0;

    public string timeToSave;

    private int idTemp;

    private int timerTemp; //Biến dùng để so sánh thời gian để gán sao cho GameOverScreen

    public GameObject timeDecs;

	// Use this for initialization
	void Start () {
        Debug.Log("GameController");
        timer = 0;
	    idTemp = 1;
    }

    public void Initialize()
    {
        starArrays = new int[24];
        timeArrays = new string[24];
        timeArraysTemp = new int[24];
        for (int i = 0; i < timeArrays.Length; i++)
        {
            timeArrays[i] = "";
            Debug.Log(timeArrays[i] + i);
        }
    }

    private float temp = 0;
    private float min = 0;
    public void TimeRun(GameObject obj)
    {
        if ((temp += Time.deltaTime) >= 1)
        {
            timer += 1;
            timerTemp += 1;
            temp = 0;
            if (timer > 59)
            {
                min++;
                timer = 0;
            }
        }
        timeToSave = min + ":" + timer;
        obj.GetComponent<Text>().text = timeToSave;
    }

    public void LoadJsonNode()
    {
        jsonNode = ResourceManager.Instance.jsonNode;
    }

    //Load star từ dưới PlayerPrefs gán vào starArrays
    public void LoadStar()
    {
        if (!PlayerPrefs.HasKey("stars"))
        {
            PlayerPrefsX.SetIntArray("stars", starArrays);
            PlayerPrefs.Save();
        }
        else
        {
            starArrays = PlayerPrefsX.GetIntArray("stars");
        }
    }

    //Load thời gian đã chơi được của từng level
    public void LoadTime()
    {
        if (!PlayerPrefs.HasKey("times"))
        {
            PlayerPrefsX.SetStringArray("times", timeArrays);
            PlayerPrefs.Save();
            Debug.Log("Chưa tồn tại timeArrays");
        }
        else
        {
            timeArrays = PlayerPrefsX.GetStringArray("times");
            Debug.Log("Đã tồn tại timeArrays");
        }
        Debug.Log(timeArrays.Length);
    }

    public void LoadTimeArrayTemp()
    {
        if (!PlayerPrefs.HasKey("timeInt"))
        {
            PlayerPrefsX.SetIntArray("timeInt", timeArraysTemp);
            PlayerPrefs.Save();
        }
        else
        {
            timeArraysTemp = PlayerPrefsX.GetIntArray("timeInt");
        }
        Debug.Log(timeArraysTemp.Length);
    }

    //Kiểm tra điều kiện để unlock những level đủ điều kiện
    public void CheckRequirementStar(List<GameObject> button)
    {
        int starTemp = 0;
        for (int i = 0; i < starArrays.Length; i++)
        {
            starTemp += starArrays[i];
        }
        for (int j = 0; j < button.Count; j++)
        {
            if (starTemp >= int.Parse(jsonNode["levelArrays"][j]["star"]))
            {
                button[j].GetComponent<Image>().sprite = spriteOpen;
                button[j].GetComponent<Button>().enabled = true;
                foreach (Transform item in button[j].transform)
                {
                    if (item.tag == "Level")
                    {
                        item.GetComponent<Text>().color = Color.magenta;
                    }
                }
            }
        }
    }
    
    //Gán các thời gian chơi cho từng level đã hoàn thành
    public void CheckRequirementTime(List<GameObject> listButton)
    {
        for (int i = 0; i < listButton.Count; i++)
        {
            if (listButton[i].GetComponent<Image>().sprite == spriteOpen)
            {
                listButton[i].transform.FindChild("time finish").GetComponent<Text>().text = timeArrays[i];
            }
            else
            {
                listButton[i].transform.FindChild("time finish").GetComponent<Text>().text = "";
            }
        }
    }

    public void LoadStarThenAssignToLevelButton(List<GameObject> listButton, Sprite starOpen)
    {
        List<GameObject> listStarTemp = new List<GameObject>();
        for (int i = 0; i < starArrays.Length; i++)
        {
            if (starArrays[i] > 0)
            {
                Debug.Log(listButton[i].name);
                foreach (Transform item in listButton[i].transform)
                {
                    if (item.tag == "Star")
                    {
                        listStarTemp.Add(item.gameObject);
                    }
                }
                for (int j = 0; j < starArrays[i]; j++)
                {
                    listStarTemp[j].GetComponent<Image>().sprite = starOpen;
                    Debug.Log(listStarTemp[j].name);
                }
                listStarTemp.RemoveRange(0, 3);
            }
        }
    }

    #region Lấy thông tin của màn chơi
    //Lấy số lượng số phụ thuộc vào id
    public int GetAmountOfNumber(int id)
    {
        amountOfNumber = int.Parse(ResourceManager.Instance.jsonNode["levelArrays"][id]["number"]);
        return amountOfNumber;
    }

    //Lấy góc quay phụ thuộc vào id
    public int GetDegreeToRatate(int id)
    {
        degreeToRotate = int.Parse(ResourceManager.Instance.jsonNode["levelArrays"][id]["angle"]);
        return degreeToRotate;
    }

    //Lấy play mode phụ thuộc vào id
    public int GetPlayMode(int id)
    {
        playMode = int.Parse(ResourceManager.Instance.jsonNode["levelArrays"][id]["play_mode"]);
        return playMode;
    }
#endregion

    //Splash screen image giảm dần
    public void SplashScreen(Image img)
    {
        img.GetComponent<Image>().fillAmount -= Time.deltaTime;
    }


    #region Active các màn hình
    
    
    //Khi nhấn nút Advanture hoặc Multiplayer
    public void AdvantureClick()
    {
        preGame.GetComponent<Canvas>().enabled = (true);
        gameOver.GetComponent<Canvas>().enabled = (false);
        gameScreen.GetComponent<Canvas>().enabled = (false);
        menuGame.GetComponent<Canvas>().enabled = (false);
    }

    public void PauseGameClick()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueClick()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void MultiplayerClick()
    {
        EnableWaitingScreen();
    }

    public void FindMatchClick()
    {
        MultiplayerController.Instance.StartMatchMaking();
    }

    public void SignOutClick()
    {
        MultiplayerController.Instance.SignOut();
    }

    //Khi nhấn nút back trên preGame
    public void BackButtonCliked() //Button back was click
    {
        ResetGamePlay();
        waitingScreen.GetComponent<Canvas>().enabled = false;
        preGame.GetComponent<Canvas>().enabled = (false);
        gameOver.GetComponent<Canvas>().enabled = (false);
        gameScreen.GetComponent<Canvas>().enabled = (false);
        menuGame.GetComponent<Canvas>().enabled = (true);
        if (pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(false);
        }
        if (Time.timeScale <= 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    //Khi chơi xong màn sẽ hiện gameOver lên
    public void EnableGameOverScreen()
    {
        ResetGamePlay();
        waitingScreen.GetComponent<Canvas>().enabled = false;
        preGame.GetComponent<Canvas>().enabled = (false);
        gameOver.GetComponent<Canvas>().enabled = (true);
        gameScreen.GetComponent<Canvas>().enabled = (false);
        menuGame.GetComponent<Canvas>().enabled = (false);
    }

    public void EnableWaitingScreen()
    {
        waitingScreen.GetComponent<Canvas>().enabled = true;
        preGame.GetComponent<Canvas>().enabled = (false);
        gameOver.GetComponent<Canvas>().enabled = (false);
        gameScreen.GetComponent<Canvas>().enabled = (false);
        menuGame.GetComponent<Canvas>().enabled = (false);
    }

    public Text txtLevel;

    //Khi vào chơi thì sẽ hiện GameScreen lên
    public void EnableGameScreen()
    {
        timerTemp = 0;
        txtLevel.text = "Level " + (idLevel + 1);
        coundownText.text = amountOfNumber.ToString();
        waitingScreen.GetComponent<Canvas>().enabled = false;
        preGame.GetComponent<Canvas>().enabled = (false);
        gameOver.GetComponent<Canvas>().enabled = (false);
        gameScreen.GetComponent<Canvas>().enabled = (true);
        menuGame.GetComponent<Canvas>().enabled = (false);
    }

    //Khi nhấn nút mainmenu của GameOverScreen
    public void MainMenuClicked()
    {
        waitingScreen.GetComponent<Canvas>().enabled = false;
        preGame.GetComponent<Canvas>().enabled = (false);
        gameOver.GetComponent<Canvas>().enabled = (false);
        gameScreen.GetComponent<Canvas>().enabled = (false);
        menuGame.GetComponent<Canvas>().enabled = (true);
        if (fakeScreen.activeSelf)
        {
            fakeScreen.SetActive(false);
            backgroundScreen.GetComponent<Canvas>().enabled = true;
        }
    }

    #endregion

    //Khi nhấn nút Replay của GameOverScreen
    public void ReplayClicked()
    {
        //AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        amountOfNumber = GetAmountOfNumber(idLevel);
        degreeToRotate = GetDegreeToRatate(idLevel);
        playMode = GetPlayMode(idLevel);
        GameScreenManager.Instance.DisplayButtonNumber();
    }

    //Khi nhấn nút NextLevel của GameOverScreen
    public void NextLevelClicked()
    {
        //AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        idLevel += 1;
        GameScreenManager.Instance.column = int.Parse(jsonNode["levelArrays"][idLevel]["column"]);
        GameScreenManager.Instance.row = int.Parse(jsonNode["levelArrays"][idLevel]["row"]);
        GameScreenManager.Instance.DisplayButtonNumber();
    }

    //Khi nhấn vào các nút chọn level để chơi
    public void ButtonLevelClick(Button btn)
    {
        //AudioSource.PlayClipAtPoint(SoundController.Instance.audio[0], transform.position);
        idLevel = btn.GetComponent<ButtonLevelManager>().id - 1;
        GameScreenManager.Instance.column = int.Parse(jsonNode["levelArrays"][idLevel]["column"]);
        GameScreenManager.Instance.row = int.Parse(jsonNode["levelArrays"][idLevel]["row"]);
        GameScreenManager.Instance.DisplayButtonNumber();
    }

    public void SetTimeArrays(int id)
    {
        Debug.Log(id + " id");
        if (timeArraysTemp[id] != 0)
        {
            if (timeArraysTemp[id] > timerTemp)
            {
                timeArrays[id] = timeToSave;
                timeArraysTemp[id] = timerTemp;
            }
        }
        else
        {
            timeArrays[id] = timeToSave;
            timeArraysTemp[id] = timerTemp;
        }
        SaveTime();
    }

    public void SaveTime()
    {
        PlayerPrefsX.SetIntArray("timeInt", timeArraysTemp);
        PlayerPrefsX.SetStringArray("times", timeArrays);
        PlayerPrefs.Save();
    }

    public void ResetGamePlay()
    {
        temp = 0;
        min = 0;
        idTemp = 1;
        timer = 0;
        totalClick = 0;
        GameScreenManager.Instance.ResetGameScreen();
    }

    public void SetTimeForGameOverScreen(GameObject obj)
    {
        obj.GetComponent<Text>().text = timeToSave;
    }

    public void DisplayStar(List<GameObject> listStar, List<Sprite> starOpen)
    {
        if (timerTemp < int.Parse(jsonNode["levelArrays"][idLevel]["time"][0]))
        {
            Debug.Log("3 sao");
            Debug.Log(timerTemp);
            SetStar(listStar, starOpen, 3);
            SaveStar(idLevel, 3);
        }
        else if (timerTemp < int.Parse(jsonNode["levelArrays"][idLevel]["time"][1]))
        {
            Debug.Log("2 sao");
            Debug.Log(timerTemp);
            SetStar(listStar, starOpen, 2);
            SaveStar(idLevel, 2);
        }
        else if (timerTemp < int.Parse(jsonNode["levelArrays"][idLevel]["time"][2]))
        {
            Debug.Log("1 sao");
            Debug.Log(timerTemp);
            SetStar(listStar, starOpen, 1);
            SaveStar(idLevel, 1);
        }
        else
        {
            Debug.Log("0 sao");
            Debug.Log(timerTemp);
            SetStar(listStar, starOpen, 0);
            SaveStar(idLevel, 0);
        }
        SetTimeArrays(idLevel);
    }

    public void SaveStar(int id, int count)
    {
        if (starArrays[id] < count)
        {
            starArrays[id] = count;
            Debug.Log(count + " count");
        }
        PlayerPrefsX.SetIntArray("stars", starArrays);
        PlayerPrefs.Save();
    }

    public void SetStar(List<GameObject> listStar , List<Sprite> starOpen, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 0; i < count; i++)
        {
            listStar[i].GetComponent<Image>().sprite = starOpen[i];
        }
    }

    void Update()
    {
        if (gameScreen.GetComponent<Canvas>().enabled)
        {
            TimeRun(timeDecs);
        }
    }
}