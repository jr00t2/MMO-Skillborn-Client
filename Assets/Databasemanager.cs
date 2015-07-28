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

        public void Create(List<string> values) {
            this.controller = "charactercreation/";
            this.action = "create/";

            NameValueCollection formdata = new NameValueCollection();
            formdata["charname"] = values[0];
            formdata["race"] = values[1];
            formdata["class"] = values[2];
            formdata["axe"] = values[3];
            formdata["dagger"] = values[4];
            formdata["unarmed"] = values[5];
            formdata["hammer"] = values[6];
            formdata["polearm"] = values[7];
            formdata["spear"] = values[8];
            formdata["staff"] = values[9];
            formdata["sword"] = values[10];
            formdata["archery"] = values[11];
            formdata["crossbow"] = values[12];
            formdata["sling"] = values[13];
            formdata["thrown"] = values[14];
            formdata["armor"] = values[15];
            formdata["dualweapon"] = values[16];
            formdata["shield"] = values[17];
            formdata["bardic"] = values[18];
            formdata["conjuring"] = values[19];
            formdata["druidic"] = values[20];
            formdata["illusion"] = values[21];
            formdata["necromancy"] = values[22];
            formdata["shamanic"] = values[23];
            formdata["sorcery"] = values[24];
            formdata["summoning"] = values[25];
            formdata["spellcraft"] = values[26];
            formdata["focus"] = values[27];
            formdata["armorsmithing"] = values[28];
            formdata["tailoring"] = values[29];
            formdata["fletching"] = values[30];
            formdata["weaponsmithing"] = values[31];
            formdata["lapidary"] = values[32];
            formdata["strength"] = values[33];
            formdata["intelligence"] = values[34];
            formdata["dexterity"] = values[35];
            formdata["constitution"] = values[36];
            formdata["developmentpoints"] = values[37];
            formdata["accountid"] = values[38];

            return (RequestData.Pull(url + controller + action, formdata));
        }
    }
