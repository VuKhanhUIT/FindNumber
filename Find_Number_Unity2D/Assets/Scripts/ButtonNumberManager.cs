using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonNumberManager : MonoBehaviour {

    // id của button number
    public int idNumber = 0;
    // text của button number
    private GameObject objText;
    //public GameObject particalEffect;
    public int maxTextSize;
    // Use this for initialization
    void Start()
    {
        objText = transform.FindChild("Text_number").gameObject;
        if (objText != null)
        {
            objText.GetComponent<Text>().resizeTextMaxSize = maxTextSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objText != null)
        {
            objText.GetComponent<Text>().text = idNumber.ToString();
        }
    }
}
