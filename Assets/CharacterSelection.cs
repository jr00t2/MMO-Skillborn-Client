using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace Skillborn {
public class CharacterSelection : MonoBehaviour {
		public Button firstCharacter;
		public Button secondCharacter;
		public Button thirdCharacter;
		public Button fourthCharacter;
		public Button fifthCharacter;

		Databasemanager db;
	// Use this for initialization
	void Start () {
		 	db = new Databasemanager ();
			string[] characters = db.GetCharactersByPlayerId (GameObject.Find ("_ACCOUNTINFO").GetComponent<Accountinformation> ().userId);
			List<string> tempchars = new List<string>();
			foreach (string character in characters) {
				if(character != "") {
					tempchars.Add(character);
				}
			}
			while (tempchars.Count < 5) {
				tempchars.Add ("Empty");
			}
			characters = tempchars.ToArray ();
			firstCharacter.GetComponentInChildren<Text> ().text = characters [0];
			secondCharacter.GetComponentInChildren<Text> ().text = characters [1];
			thirdCharacter.GetComponentInChildren<Text> ().text = characters [2];
			fourthCharacter.GetComponentInChildren<Text> ().text = characters [3];
			fifthCharacter.GetComponentInChildren<Text> ().text = characters [4];

			firstCharacter.onClick.AddListener (() => CharacterInformation (firstCharacter.GetComponentInChildren<Text> ().text));
	}
		private void CharacterInformation (string name) {
			db = new Databasemanager ();
			string[] response = db.GetCharacterInformationByName (name);
			//response is made up with 4 parameters charname, classname, racename, lvl
		} 
	// Update is called once per frame
	void Update () {
	
	}
}
}