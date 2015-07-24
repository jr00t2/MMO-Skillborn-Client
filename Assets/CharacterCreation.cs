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
	// Use this for initialization
	void Start () {
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
        // for (int i = 0; i < 30; i++)
        //{
           // modLabels[i].text = (raceMods[i] + classMods[i]).ToString() ;
      //  } TEEST
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
	// Update is called once per frame
	void Update () {
	
	}
}
