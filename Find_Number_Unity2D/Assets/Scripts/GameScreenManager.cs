using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameScreenManager : MonoSingleton<GameScreenManager> {


    public List<GameObject> listOfContain = new List<GameObject>();

    public GameObject img;
    public int row;
    public int column;
    public List<Color> colorList = new List<Color>();

    public GameObject squarePrefab;
    public GameObject squareParent;
    public GridLayoutGroup gridLayoutGroup;
    public List<int> findNumber;
    public int buttonId;
    public int index;
    

    public float timeRequire;

    public float squareX;
    public float squareY;

    // Use this for initialization
    void Start () {
        index = 1;
    }

    public void CustomGrid()
    {
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = column;
        gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        squareX = (gridLayoutGroup.gameObject.GetComponent<RectTransform>().rect.size.x / column) - 13;
        squareY = (gridLayoutGroup.gameObject.GetComponent<RectTransform>().rect.size.y / row) - 13;
        gridLayoutGroup.cellSize = new Vector2(squareX, squareY);
    }

    public void PauseGameClicked()
    {
        GameController.Instance.PauseGameClick();
    }

    public void ContinueGameClicked()
    {
        GameController.Instance.ContinueClick();
    }

    public void DisplayButtonNumber()
    {
        DisplayAllButton();
    }

    public void DisplayAllButton()
    {
        CustomGrid();
        if (GameObject.Find("SquareParent").GetComponentInChildren<Transform>().childCount != 0)
        {
            var childIndex = GameObject.Find("SquareParent").GetComponentInChildren<Transform>().childCount;
            for (int i = 0; i < childIndex; i++)
            {
                Destroy(GameObject.Find("SquareParent").GetComponentInChildren<Transform>().GetChild(i).gameObject);
            }
        }
        int randButton = Random.Range(0, column * row);
        int randColor = Random.Range(0, colorList.Count);
        for (int i = 0; i < column * row & (column * row) < 37; i++)
        {
            GameObject child = Instantiate(squarePrefab) as GameObject;
            child.transform.SetParent(squareParent.transform);
            child.transform.localScale = squareParent.transform.localScale;
            child.GetComponent<Image>().color = colorList[randColor];
            if (randButton == i)
            {
                child.transform.gameObject.GetComponentInChildren<Text>().text = index.ToString();
                SquareButtonManager.Instance.SetButtonId(index);
            }
            else
            {
                int rand;
                do
                {
                    rand = Random.Range(0, 100); //Random số từ 1 tới 100 để gán cho các ô khác

                } while (rand == index);
                child.transform.gameObject.GetComponentInChildren<Text>().text = rand.ToString();
                SquareButtonManager.Instance.SetButtonId(rand);
            }
        }
    }

    bool HardMode()
    {
        if (PlayModeController.Instance.increaseMode)
        {
            return true;
        }
        return false;
    }

    public bool CheckButtonPressed()
    {
        if (buttonId == findNumber[index - 1])
        {
            AudioSource.PlayClipAtPoint(SoundController.Instance.audio[2], transform.position);
            index++;
            return true;
        }
        AudioSource.PlayClipAtPoint(SoundController.Instance.audio[3], transform.position);
        return false;
    }

    public bool IsFinished()
    {
        if ((index - 1) == findNumber.Count)
        {
            return true;
        }
        return false;
    }

    public void ResetGameScreen()
    {
        index = 1;
        CustomGrid();
    }
}