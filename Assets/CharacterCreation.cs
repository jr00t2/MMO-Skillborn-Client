using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
namespace Skillborn {
public class CharacterCreation : MonoBehaviour {
    public Text[] modLabels;
    public Text[] levelLabels;

    public Button[] minusButtons;
    public Button[] plusButtons;

    public List<int> raceMods;
    public List<int> classMods;

    public Button racePrefabs;
    public Button classPrefabs;

    public GameObject racePanel;
    public GameObject classPanel;

    public Button chosenRace;
    public Button chosenClass;

    Databasemanager db;
    public int developmentPoints;
    public int attributePoints;
    public Text dpLabel;
    public Text attLabel;
    public InputField charname;

    public Button[] attributePlus;
    public Button[] attributeMinus;
    public Text[] attributeLevel;
    public List<int> attributeSkilled;
    public Button CreateBtn;
	// Use this for initialization
	void Start () {
        ResetPoints();
        db = new Databasemanager();
        attributeSkilled = new List<int>();
        for (int i = 0; i < 30; i++)
        {
            raceMods.Add(0);
            classMods.Add(0);
        }
        racePanel.SetActive(true);
        classPanel.SetActive(true);
        GetRaces();
        GetClasses();
        racePanel.SetActive(false);
        classPanel.SetActive(false);
        for (int y = 0; y < plusButtons.Length; y++) {
            int temp = y;
            plusButtons[temp].onClick.AddListener(() => AddSkillLevel(temp));
            minusButtons[temp].onClick.AddListener(() => SubSkillLevel(temp));
            
            plusButtons[temp].enabled = false;
            minusButtons[temp].enabled = false;
        }
        for (int x = 0; x < 4; x++) {
            int temp = x;
                attributePlus[temp].onClick.AddListener(() => AddAttributeLevel(temp));
                attributeMinus[temp].onClick.AddListener(() => SubAttributeLevel(temp));
                attributePlus[temp].enabled = false;
                attributeMinus[temp].enabled = false;
        }
        CreateBtn.onClick.AddListener(() => CreateCharacter());
        
	}
    void ResetPoints() {
        developmentPoints = 1500;
        attributePoints = 30;
        for (int i = 0; i < 30; i++) {
            levelLabels[i].text = "0";
        }
        for (int x = 0; x < 4; x++)
        {
            attributeSkilled.Add(0);
        }
    }
    void GetRaces() {
        for (int i = 0; i < db.GetAll("races").Length - 1; i++)
        {
            var racebtn = Instantiate(classPrefabs);
            racebtn.transform.parent = racePanel.transform;
            racebtn.GetComponentInChildren<Text>().text = db.GetAll("races")[i];
            racebtn.onClick.AddListener(() => GetRaceByName(racebtn));
            racebtn.onClick.AddListener(() => SelectRace(racebtn));
        }
    }
    void SelectRace(Button chosen) {
        chosenRace.GetComponentInChildren<Text>().text = chosen.GetComponentInChildren<Text>().text;
        chosen.transform.parent.gameObject.SetActive(false);
        if (chosenClass.GetComponentInChildren<Text>().text != "Choose a Class") {
            for (int i = 0; i < 30; i++) {
                plusButtons[i].enabled = true;
                minusButtons[i].enabled = true;
                if(i < 4) {
                    attributeMinus[i].enabled = true;
                    attributePlus[i].enabled = true;
                }
            }
        }
    }
    void CalculateMods()
    {
         for (int i = 0; i < 30; i++)
        {
            modLabels[i].text = (raceMods[i] + classMods[i]).ToString() ;
        }
         ResetPoints();
    }
    void GetClasses() {
        for (int i = 0; i < db.GetAll("classes").Length-1; i++ )
        {
            var classbtn = Instantiate(classPrefabs);
            classbtn.transform.parent = classPanel.transform;
            classbtn.GetComponentInChildren<Text>().text = db.GetAll("classes")[i];
            classbtn.onClick.AddListener(() => GetClassByName(classbtn));
            classbtn.onClick.AddListener(() => SelectClass(classbtn));
        }
    }
    void SelectClass(Button chosen)
    {
        chosenClass.GetComponentInChildren<Text>().text = chosen.GetComponentInChildren<Text>().text;
        chosen.transform.parent.gameObject.SetActive(false);
        if (chosenRace.GetComponentInChildren<Text>().text != "Choose a Race")
        {
            for (int i = 0; i < 30; i++)
            {
                plusButtons[i].enabled = true;
                minusButtons[i].enabled = true;
                if (i < 4)
                {
                    attributeMinus[i].enabled = true;
                    attributePlus[i].enabled = true;
                }
            }
        }
    }
    void GetClassByName(Button cBtn) {
        string[] response = db.GetModsByName("classesbyname", cBtn.GetComponentInChildren<Text>().text);
        for (int i = 0; i < 30; i++)
        {
            classMods[i] = Convert.ToInt32(response[i+2]);
        }
        CalculateMods();
    }
    void GetRaceByName(Button cBtn)
    {
        string[] response = db.GetModsByName("racesbyname", cBtn.GetComponentInChildren<Text>().text);
        for (int i = 0; i < 30; i++)
        {
            raceMods[i] = Convert.ToInt32(response[i + 2]); 
        }
        attributeLevel[0].text = response[32];
        attributeLevel[1].text = response[33];
        attributeLevel[2].text = response[34];
        attributeLevel[3].text = response[35];

        CalculateMods();
    }
    void AddSkillLevel(int skillnumber) {
        if(developmentPoints >= CalculateCosts(skillnumber) && levelLabels[skillnumber].text != "49") {
            developmentPoints -= CalculateCosts(skillnumber);
            int levelincrease = Convert.ToInt32(levelLabels[skillnumber].text) + 1;
            levelLabels[skillnumber].text = levelincrease.ToString();
        }
    }
    void SubSkillLevel(int skillnumber)
    {
        if (levelLabels[skillnumber].text != "0")
        {
            developmentPoints += CalculateCosts(skillnumber);
            int leveldecrease = Convert.ToInt32(levelLabels[skillnumber].text) - 1;
            levelLabels[skillnumber].text = leveldecrease.ToString();
        }
    }
    void AddAttributeLevel(int attributenumber) {
        if (attributePoints > 0) {
            attributePoints--;
            int tempLevel = Convert.ToInt32(attributeLevel[attributenumber].text) + 1;
            Debug.Log(attributenumber);
            attributeLevel[attributenumber].text = tempLevel.ToString();
            attributeSkilled[attributenumber]++;
        }
    }
    void SubAttributeLevel(int attributenumber) {
        if (attributeSkilled[attributenumber] > 0) {
            attributePoints++;
            int tempLevel = Convert.ToInt32(attributeLevel[attributenumber].text) - 1;
            attributeLevel[attributenumber].text = tempLevel.ToString();
            attributeSkilled[attributenumber]--;
        }
    }
    int CalculateCosts(int skillnumber) {
        int costs = 50 + Convert.ToInt32(modLabels[skillnumber].text);
        return costs;
    }
	// Update is called once per frame
	void FixedUpdate () {
        dpLabel.text = "Developmentpoints: " + developmentPoints.ToString();
        attLabel.text = "Attributepoints: " + attributePoints.ToString();
	}
    void CreateCharacter() {
        List<string> gatheredValues = new List<string>();
        //0 be the name, 1 be the Race, 2 be the class
        gatheredValues.Add(charname.text);
        gatheredValues.Add(chosenRace.GetComponentInChildren<Text>().text);
        gatheredValues.Add(chosenClass.GetComponentInChildren<Text>().text);
        for (int i = 0; i < 30; i++)
        {
            //3 - 32 are skills
            gatheredValues.Add(levelLabels[i].text);
        }
        //33 - 36 are attributes
        for (int y = 0; y < 4; y++) {
            gatheredValues.Add(attributeLevel[y].text);
        }
        //37 is developmentpoints left
        gatheredValues.Add(developmentPoints.ToString());
        //38 is accId
        var go = GameObject.Find("_ACCOUNTINFO");
        Accountinformation ac = go.GetComponent<Accountinformation>();
        gatheredValues.Add(ac.userId.ToString());
        if (attributePoints == 0)
        {
           db.Create(gatheredValues);
        }
     }
}
}