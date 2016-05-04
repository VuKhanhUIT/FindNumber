using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoSingleton<BackgroundManager> {

    public List<GameObject> listOfBackground;

    public int indexOfBackground;

	// Use this for initialization
	void Start () {
        listOfBackground = new List<GameObject>();
        SetActiveFalse();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //SetActive = false cho tất cả các background trước khi hiển thị lựa chọn
    public void SetActiveFalse()
    {
        foreach (var item in listOfBackground)
        {
            item.SetActive(false);
        }
    }

    //Chọn loại background nào dựa vào index
    public void SetBackgroundWithIndex()
    {
        switch (indexOfBackground)
        {
            case 0:
                listOfBackground[0].SetActive(true);
                break;
            case 1:
                listOfBackground[1].SetActive(true);
                break;
            case 2:
                listOfBackground[2].SetActive(true);
                break;
            case 3:
                listOfBackground[3].SetActive(true);
                break;
            default:
                break;
        }
    }
}
