using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour {
    public Transform LoginPanel;
    public Transform SelectionPanel;
    public Transform CharCreate;
	// Use this for initialization
	void Start () {
        string scenename = Application.loadedLevelName;
        Debug.Log(scenename);
        if (scenename == "PhotonSpielplatz")
        {
           Instantiate(LoginPanel);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetCharacterCreation() {
        Instantiate(CharCreate);
    }
    public void GetCharacterSelection() {
        Instantiate(SelectionPanel);
    }
}
