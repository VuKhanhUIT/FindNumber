using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;

public class ResourceManager : MonoSingleton<ResourceManager> {

    TextAsset textAsset;

    public JSONNode jsonNode;

    public Sprite[] listOfSprite;

    public GameObject particalEffectExplosion;
	// Use this for initialization
	void Start () {
        Debug.Log("ResourceManager");
        LoadSprite();
        LoadParticalEffect();
    }
	
	// Update is called once per frame
	void Update () {
	}

    //Load dữ liệu từ file json đưa vào jsonNode variable
    public void LoadJsonFile()
    {
        textAsset = (TextAsset)Resources.Load("level", typeof(TextAsset));
        jsonNode = JSON.Parse(textAsset.text);
    }

    //Load sprite của các button lưu vào biến mảng listOfSprite
    void LoadSprite()
    {
        listOfSprite = Resources.LoadAll<Sprite>("SpriteButton/Lady bugs");
    }

    void LoadParticalEffect()
    {
        particalEffectExplosion = Resources.Load<GameObject>("Partical/explosionParticle");
    }
}
