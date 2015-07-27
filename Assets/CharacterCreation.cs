using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

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
	// Use this for initialization
	void Start () {
        ResetPoints();
        db = new Databasemanager();
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
        }
	}
    void ResetPoints() {
        developmentPoints = 1500;
        attributePoints = 30;
    }
    void GetRaces() {
        for (int i = 0; i < db.GetAll("races").Length - 1; i++)
        {
            var racebtn = Instantiate(classPrefabs);
            racebtn.transform.parent = racePanel.transform;
            racebtn.GetComponentInChildren<Text>().text = db.GetAll("races")[i];
            racebtn.onClick.AddListener(() => GetRaceByName(racebtn));
            racebtn.onClick.AddListener(() => SelectRace(racebtn));
            Debug.Log("Races made");
        }
    }
    void SelectRace(Button chosen) {
        chosenRace.GetComponentInChildren<Text>().text = chosen.GetComponentInChildren<Text>().text;
        chosen.transform.parent.gameObject.SetActive(false);
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
    }
    void GetClassByName(Button cBtn) {
        for (int i = 0; i < 30; i++)
        {
            classMods[i] = Convert.ToInt32(db.GetModsByName("classesbyname", cBtn.GetComponentInChildren<Text>().text)[i+2]);
        }
        CalculateMods();
    }
    void GetRaceByName(Button cBtn)
    {
        for (int i = 0; i < 30; i++)
        {
            raceMods[i] = Convert.ToInt32(db.GetModsByName("racesbyname", cBtn.GetComponentInChildren<Text>().text)[i+2]);
            Debug.Log(db.GetModsByName("racesbyname", cBtn.GetComponentInChildren<Text>().text)[i]);
        }
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
    int CalculateCosts(int skillnumber) {
        int costs = 50 + Convert.ToInt32(modLabels[skillnumber].text);
        return costs;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
