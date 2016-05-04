using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CManagerGameScreen : MonoBehaviour {
    // text hiển thị thời gian
    public Text textTime;
    // text hiển thị số button Number còn lại
    public Text textCountNumber;
    // số button còn lại 
    public int countButtonRemain;
    // xác định khi nào đếm thời gian
    public bool isStartTime = true;
    // get id hiện taị của number khi click vào button
    public int idNumberClick = 0;
    // tổng số lần click để so sánh vs idNumberClick
    public int totalClick = 0;
    // số button number truyền vào gamePlay
    public int myCountButtonNumber;
    // thời gian chơi
    public float timer = 0.0f;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(isStartTime)
        {
            DisplayTimePlay();
            DisPlayButtonRemain();
        }
	}

    // bắt đầu đém thời gian 
    // hiển thị thời gian
    void DisplayTimePlay()
    {
        timer += Time.deltaTime;
        string minutes = Mathf.Floor(timer / 59).ToString("00");
        string seconds = (timer % 59).ToString("00");
        textTime.text = minutes + ":" + seconds;
    }

    void DisPlayButtonRemain()
    {
        textCountNumber.text = countButtonRemain.ToString();
    }
}
