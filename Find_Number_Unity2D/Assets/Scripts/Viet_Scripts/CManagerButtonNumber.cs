// class tạo các button trên gamescreen 
// + getlistbutton đã có sẵn
// + random vị trí các button
//....................................................................... 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CManagerButtonNumber : MonoBehaviour
{
	// list chưa button number đã tạo sẵn
    public List<GameObject> myListButtonNumber;
    // list chứa id của các button number
    public List<int> myListID;

    public List<GameObject> listContainButton;
	//Chứa những button đã bị disable number đi
	public List<GameObject> listDiable = new List<GameObject>();

	public float timeTemp;

    //Chế độ chơi
    public int playMode;

    //List chứ 4 containbutton
    //public List<GameObject> containButtonNumber = new List<GameObject>();

    public int myCountNumber = 20;
    public int myAngle = 0;
    public int[] myCountStar;

    // xác định tạo lại button khi chọn sai số
    public bool isCreateButtonAgain = false;
    // id của button khi chọn sai 
    public int idButton;

	void Start () {
        myCountStar = new int[24];
        myListButtonNumber = GetListButtonFromObjectParent(0);
        myListID = new List<int>();


        //test............................
        DisplayAllButton();
	}

    /// <summary>
    /// - active containButton tương ứng vs số button cần 
    /// - lấy ds button từ containbutton tương ứng
    /// </summary>
    /// <param name="indexOfContainButton"></param>
    /// <returns>ds button của containbutton </returns>
    public List<GameObject> GetListButtonFromObjectParent(int indexOfContainButton)
    {
        List<GameObject> listButton = new List<GameObject>();

        for (int i = 0; i < listContainButton.Count;i++)
        {
            if(i == indexOfContainButton)
            {
                if (listContainButton[i].activeInHierarchy == false)
                {
                    listContainButton[i].SetActive(true);
                }
            }
            else
                listContainButton[i].SetActive(false);
        }
        foreach (Transform child in listContainButton[indexOfContainButton].transform)
        {
            if (child.tag == "ButtonNumber")
            {
                listButton.Add(child.gameObject);
            }
        }

        if (listButton.Count > 0)
        {
            return listButton;
        }
        else
        {
            Debug.Log("can't get list button from object parent!");
            return null;
        }
    }

    /// <summary>
    /// _ Hiển thi số button theo level
    /// _ gắn id cho từng button
    /// _ gắn sprite cho button
    /// </summary>
    /// <param name="countButton"></param>
    /// <return> : 
    public void DisplayAllButton()
    {
        List<GameObject> ListButtonPrefabs = new List<GameObject>();
        //Get list button
        //myListButtonPrefabs = ConfuseListPosition(GetListButtonFromObjectParent());
        ListButtonPrefabs = ConfuseListPosition(myListButtonNumber);

        //ManagerGameScreen.Instance.myCountButton = myCountNumber;
        //ManagerGameScreen.Instance.countButtonRemain = myCountNumber;

        // set active tất cả button trước khi random lại
        foreach (GameObject obj in ListButtonPrefabs)
        {
            if (obj != null)
            {
                if (obj.activeInHierarchy == true)
                {
                    obj.SetActive(false);
                }
            }
        }

        myListID = ConfuseListNumber(myCountNumber, 1); // Get list id của button
        for(int i = 0; i< myCountNumber; i++)
        {
            if (ListButtonPrefabs[i] != null)
            {
                ListButtonPrefabs[i].SetActive(true);
				ListButtonPrefabs[i].GetComponentInChildren<Text>().enabled = true;
                ListButtonPrefabs[i].GetComponent<Image>().sprite = GetSpriteFromListSprite(ResourceManager.Instance.listOfSprite);
                CButtonNumber buttonScripts = ListButtonPrefabs[i].GetComponent<CButtonNumber>();
                buttonScripts.idNumber = myListID[i];
            }
            else
                Debug.Log("null refrence!!");
        }
    }
    
    public Sprite GetSpriteFromListSprite(Sprite[] listSprite)
    {
        if (listSprite.Length > 0)
        {
            int index = Random.Range(0, listSprite.Length);
            return listSprite[index];
        }
        else
            return null;
    }

    // Trộn list int từ list có thứ tự
    // tham số:
            //- countNumber : bao nhiêu số
            // from : từ số nào ->...
    public List<int> ConfuseListNumber( int countNumber, int from)
    {
        List<int> listNumber = new List<int>();
        System.Random rnd = new System.Random();
        int number = 0;
        for (int i = from; i <= countNumber; i++)
        {
            listNumber.Add(i);
        }

        for (int i = from - 1; i < countNumber; i++)
        {
            int r = i + (int)(rnd.NextDouble() * (countNumber - i));
            number = listNumber[r];
            listNumber[r] = listNumber[i];
            listNumber[i] = number;
        }
        return listNumber;
    }

    // Trộn vị trí các position button trong 1 listButton
    // tham so : list <Button>
    public List<GameObject> ConfuseListPosition(List<GameObject> listButton)
    {
        System.Random rnd = new System.Random();
 
        int n = listButton.Count;

        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(rnd.NextDouble() * (n - i));
            GameObject buttonNumber = listButton[r];
            listButton[r] = listButton[i];
            listButton[i] = buttonNumber;
        }
        return listButton;
    }

    void CreateOneButton(List<GameObject> listButton)
    {
        foreach(GameObject objButton in listButton)
        {
            if(objButton.activeInHierarchy == false)
            {
                objButton.SetActive(true);
                CButtonNumber buttonScripts = objButton.GetComponent<CButtonNumber>();
                buttonScripts.idNumber = idButton;
                objButton.GetComponent<Image>().sprite = GetSpriteFromListSprite(ResourceManager.Instance.listOfSprite);
                break;
            }
        }
    }

	// Update is called once per frame
	void Update () {
	    if(isCreateButtonAgain)
        {
            isCreateButtonAgain = false;
            CreateOneButton(myListButtonNumber);
        }
	}


    ////Trộn các số cho chế độ shuffle
    //public void ShuffleButton()
    //{
    //    //List chứa những số còn active trên màn hình
    //    List<GameObject> listTemp = new List<GameObject>();

    //    //List chứa những số còn lại trên màn hình
    //    List<int> listIdTemp = new List<int>();
    //    foreach (Transform child in transform)
    //    {
    //        if (child.tag == "ButtonNumber" && child.gameObject.activeSelf == true)
    //        {
    //            listTemp.Add(child.gameObject);
    //            listIdTemp.Add(child.GetComponent<CButtonNumber>().idNumber);
    //        }
    //    }

    //    listTemp = ConfuseListPosition(listTemp);
    //    for (int i = 0; i < listTemp.Count; i++)
    //    {
    //        if (listTemp[i] != null)
    //        {
    //            listTemp[i].GetComponent<Image>().sprite = GetSpriteFromListSprite(ResourceManager.Instance.listOfSprite);
    //            CButtonNumber buttonScripts = listTemp[i].GetComponent<CButtonNumber>();
    //            buttonScripts.idNumber = listIdTemp[i];
    //        }
    //        else
    //            Debug.Log("null refrence!!");
    //    }
    //}

    //public void Reload()
    //{
    //    switch (LevelManager.Instance.playMode)
    //    {
    //        case 0:
    //            break;
    //        case 1:
    //            if (ManagerGameScreen.Instance.totalClick != 0 && ManagerGameScreen.Instance.totalClick % 5 == 0)
    //            {
    //                ShuffleButton();
    //                Debug.Log("Gọi 1");
    //            }
    //            break;
    //        case 2:
    //            if (ManagerGameScreen.Instance.totalClick != 0 && ManagerGameScreen.Instance.totalClick % 5 == 0)
    //            {
    //                Debug.Log("Gọi 2");
    //                BlindMode();
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}

    // active contain button 
    //public void ActiveContainButton()
    //{
    //    for (int i = 0; i < containButtonNumber.Count; i++)
    //    {
    //        containButtonNumber[i].SetActive(false);
    //    }
    //    if (myCountNumber <= 30)
    //    {
    //        containButtonNumber[0].SetActive(true);
    //    }
    //    else if (myCountNumber <= 40)
    //    {
    //        containButtonNumber[1].SetActive(true);
    //    }
    //    else if (myCountNumber <= 50)
    //    {
    //        containButtonNumber[2].SetActive(true);
    //    }
    //    else if (myCountNumber <= 65)
    //    {
    //        containButtonNumber[3].SetActive(true);
    //    }
    //    myListButtonNumber = GetListButtonFromObjectParent();
    //}

    //public void BlindMode()
    //{
    //    //Chứa toàn bộ các button còn active trên màn hình.
    //    List<GameObject> listTemp = new List<GameObject>();

    //    timeTemp = ManagerGameScreen.Instance.timer;

    //    foreach (Transform child in transform)
    //    {
    //        if (child.tag == "ButtonNumber" && child.gameObject.activeSelf == true)
    //        {
    //            listTemp.Add(child.GetComponent<CButtonNumber>().gameObject);
    //        }
    //    }

    //    for (int i = 0; i < (int)(listTemp.Count / 2); i++)
    //    {
    //        listTemp[i].GetComponentInChildren<Text>().enabled = false;
    //        listDiable.Add(listTemp[i]);
    //    }
    //}
}
