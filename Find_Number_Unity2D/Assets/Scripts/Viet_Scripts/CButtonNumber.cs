using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CButtonNumber : MonoBehaviour {
    // id của button number
    public int idNumber = 0;
    // text của button number
    private GameObject objText;
    //public GameObject particalEffect;
    public int maxTextSize;
	// Use this for initialization
	void Start () {
        objText = transform.FindChild("Text_number").gameObject;
        //particalEffect = Resources.Load<GameObject>("Partical/explosionParticle"); 
        if(objText!= null)
        {
            objText.GetComponent<Text>().resizeTextMaxSize = maxTextSize;
        }
	}

    /// <summary>
    /// - tạo hiệu ứng nổ 
    /// - truyền id của button sang CManagerGameScreen
    /// - âm thanh click true or false
    /// </summary>
    public void Click_button()
    {
        Destroy(Instantiate(ResourceManager.Instance.particalEffectExplosion,new Vector3(transform.position.x,transform.position.y ,1),Quaternion.identity),2.0f);
        Debug.Log(transform.position);
        
        gameObject.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
        if(objText != null)
        {
            objText.GetComponent<Text>().text = idNumber.ToString();
        }
	}
}
