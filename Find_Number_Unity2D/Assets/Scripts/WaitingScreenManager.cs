using UnityEngine;
using System.Collections;

public class WaitingScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FindingMatchClicked()
    {
        GameController.Instance.FindMatchClick();
    }
}
