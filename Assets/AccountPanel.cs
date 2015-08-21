using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace Skillborn {
public class AccountPanel : MonoBehaviour {
    public Button RegisterBtn;
    public Button LoginBtn;
    public InputField username;
    public InputField password;
	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("_DATABASEMANAGER");
        Databasemanager db = (Databasemanager)go.GetComponent(typeof(Databasemanager));
        
        RegisterBtn.onClick.AddListener(() => db.Register(username.text, password.text));
        LoginBtn.onClick.AddListener(() => db.Login(username.text, password.text));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
}