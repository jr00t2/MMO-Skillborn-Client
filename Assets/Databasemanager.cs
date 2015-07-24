using UnityEngine;
using System.Collections.Generic;
using System;
using System.Net;
using System.Collections;
using System.Collections.Specialized;

public class Databasemanager : MonoBehaviour {
    public string url = "127.0.0.1/";
    public string controller = "";
    public string action = "";
    
    //Account Methods
   public void Register(string email, string password) {
        this.controller = "account/";
        this.action = "register/";
        NameValueCollection formdata = new NameValueCollection();
        formdata["email"] = email;
        formdata["password"] = password;

        string[] response = RequestData.Pull(url + controller + action, formdata); 
    }

   public void Login(string email, string password)
   {
       this.controller = "account/";
       this.action = "login/";
       NameValueCollection formdata = new NameValueCollection();
       formdata["email"] = email;
       formdata["password"] = password;

       string[] response = RequestData.Pull(url + controller + action, formdata);
       if (response[0] != "Benutzername oder Passwort falsch!")
       {
           GameObject go = GameObject.Find("_ACCOUNTINFO");
           Accountinformation ac = (Accountinformation)go.GetComponent(typeof(Accountinformation));
           ac.username = response[1];
           ac.userId = Convert.ToInt32(response[2]);
           ac.calculatedResult = response[3];

           GameObject goac = GameObject.Find("AccountPanel");
           Destroy(goac.transform.parent.gameObject); //Get the parent and destroy it
           GameObject _wm = GameObject.Find("_WINDOWMANAGER");
           WindowManager wm = (WindowManager)_wm.GetComponent(typeof(WindowManager));
           wm.GetCharacterSelection();
       }
       else {
           Debug.Log(response[0]);
       }
   }
    //CharacterCreationMethods
   public string[] GetClassSettings(string classname) {
       this.controller = "charactercreation/";
       this.action = "classesbyname/";
       NameValueCollection formdata = new NameValueCollection();
       formdata["classname"] = classname;

       return RequestData.Pull(url + controller + action, formdata);
   }

   public string[] GetRaceSettings(string racename)
   {
       this.controller = "charactercreation/";
       this.action = "racesbyname/";
       NameValueCollection formdata = new NameValueCollection();
       formdata["racename"] = racename;

       return RequestData.Pull(url + controller + action, formdata);
   }

   public string[] PreloadClasses() {
       this.controller = "charactercreation/";
       this.action = "classes";
       NameValueCollection formdata = new NameValueCollection();
       return RequestData.Pull(url + controller + action, formdata);
   }
   public string[] PreloadRaces() {
       this.controller = "charactercreation/";
       this.action = "races/";
       NameValueCollection formdata = new NameValueCollection();
       return RequestData.Pull(url + controller + action, formdata);
   }
    //Helper Functions
    string[] SplitString(string s) {
        string[] strings = s.Split(':');
        return strings;
    }
}
