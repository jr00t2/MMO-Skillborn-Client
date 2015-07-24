using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using UnityEngine;

    class Databasemanager : MonoBehaviour
    {
        private string url = "127.0.0.1/";
        private string controller;
        private string action;

        public void Login(string username, string password) {
            this.controller = "account/";
            this.action = "login/";

            NameValueCollection formdata = new NameValueCollection();
            formdata["email"] = username;
            formdata["password"] = password;
            string[] response = RequestData.Pull(url + controller + action, formdata);
            if (response.Length > 1 && response[0] == "Login") {
                WindowManager wm = GameObject.Find("_ACCOUNTINFO").GetComponent<WindowManager>();
                wm.GetCharacterSelection();
                Accountinformation ac = GetComponent<Accountinformation>();
                ac.username = response[1];
                ac.userId =Convert.ToInt32( response[2]);
                ac.calculatedResult = response[3];
                wm.LoginPanel.gameObject.SetActive(false);
                Debug.Log("yay");
            }
        }

        public void Register(string username, string password)
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// action can either be "races" or "classes"
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string[] GetAll(string action) {
            this.controller = "charactercreation/";
            this.action = action + "/";
            NameValueCollection formdata = new NameValueCollection();
            string[] response = RequestData.Pull(url + controller + action, formdata);

            return response;
        }
        /// <summary>
        /// action is racesbyname or classesbyname - the name is either classname or racename
        /// </summary>
        /// <param name="action"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string[] GetModsByName(string action, string name) {
            this.controller = "charactercreation/";
            this.action = action + "/";
            NameValueCollection formdata = new NameValueCollection();
            if (action == "racesbyname")
            {
                formdata["racename"] = name;
            }
            else {
                formdata["classname"] = name;
            }
            string[] response = RequestData.Pull(url + controller + action, formdata);
            return response;
        }
    }
