using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class CharacterBuilder : MonoBehaviour {
    CBskills skills;
    public static List<int> classMods {get; set;}
    public static List<int> raceMods { get; set; }
    public static List<int> resultMods { get; set; }
    GameObject DBmanager;
    Databasemanager db;

    public Text[] skilllevelLabels;
    public Text[] skillmodLabels;
    public Button[] plusBtns;
    public Button[] minusBtns;

    public Button ClassBtn;
    public GameObject ClassPanel;
    public Transform classbtn;
    public Button RaceBtn;
    public GameObject RacePanel;
    public Transform racebtn;

    public int developmentPoints = 1000;
    public int attributePoints = 30;
	// Use this for initialization
	void Start () {
        DBmanager = GameObject.Find("_DATABASEMANAGER");
        db = (Databasemanager)DBmanager.GetComponent(typeof(Databasemanager));
        raceMods = new List<int>();
        classMods = new List<int>();
        for (int i = 0; i < 30; i++) {
            raceMods.Add(0);
            classMods.Add(0);
        }
            BuildClassPanel();
            ResetParameters();
	}
    void ClassOnClick(string s) {
        ClassBtn.transform.GetComponentInChildren<Text>().text = s;
        string[] settings = db.GetClassSettings(s);
        
        //Hier müsste die for loop hin um die Buttons mit Daten zu füllen 
        //TODO For Loop
        classMods = new List<int>();
        for (int i = 2; i < settings.Length - 3; i++)
        {
            classMods.Add(Convert.ToInt32(settings[i]));
        }
        ResetParameters();
        calculateModResult();
        ClassPanel.SetActive(false);
    }
    private void calculateModResult() {
        resultMods = new List<int>();
        for (int i = 0; i < 30; i++) {
            resultMods.Add(classMods[i] + raceMods[i]);
        }
        
        
        for (int y = 0; y < 30; y++)
        {
            skillmodLabels[y].text = resultMods[y].ToString();
            Debug.Log(resultMods[y].ToString());
        }
    }
    void ResetParameters() {
        //Reset all skill levels
        for (int i = 0; i < 30; i++) {
            skilllevelLabels[i].GetComponentInChildren<Text>().text = "0";
        }
       
        attributePoints = 30;
        developmentPoints = 1000;
    }
    void RaceOnClick(string s)
    {
        RaceBtn.transform.GetComponentInChildren<Text>().text = s;
        string[] settings = db.GetRaceSettings(s);
        //Hier müsste die for loop hin um die Buttons mit Daten zu füllen 
        //TODO For Loop
        raceMods = new List<int>();
        for (int i = 2; i < settings.Length-1; i++) {
            raceMods.Add(Convert.ToInt32(settings[i]));
        }
        ResetParameters();
        calculateModResult();
            RacePanel.SetActive(false);
    }

    public void GetClassNameSettings(Text btntext) {
        db.GetClassSettings(btntext.text); 
    }
    void BuildClassPanel() {
       var classnames = db.PreloadClasses(); //Get the classes from dabasemanager (dbmanager gets them from Server
       for (int i = 0; i < classnames.Length-1; i++) { //loop all classes (gotten from server db)
           Transform newClass = Instantiate(classbtn);
           newClass.name = classnames[i];
           newClass.transform.parent = ClassPanel.gameObject.transform; //Set the Classpanel as the parent of the button
           ClassPanel.SetActive(true);

           newClass.transform.GetComponentInChildren<Text>().text = classnames[i];
           newClass.transform.GetComponent<Button>().onClick.AddListener(()
               => ClassOnClick(newClass.transform.GetComponentInChildren<Text>().text)); 

           ClassPanel.SetActive(false);
       } //for-loop end Classes Are now preloaded

       var racenames = db.PreloadRaces(); //Get the race from dabasemanager (dbmanager gets them from Server
       for (int i = 0; i < racenames.Length - 1; i++)
       { //loop all races (gotten from server db)
           Transform newClass = Instantiate(racebtn);
           newClass.name = racenames[i];
           newClass.transform.parent = RacePanel.gameObject.transform; //Set the Racepanel as the parent of the button
           RacePanel.SetActive(true);

           newClass.transform.GetComponentInChildren<Text>().text = racenames[i];
           newClass.transform.GetComponent<Button>().onClick.AddListener(()
               => RaceOnClick(newClass.transform.GetComponentInChildren<Text>().text));

           RacePanel.SetActive(false);
       } //for-loop end Races Are now preloaded
       //Adding buttons onCLick Listeners to + and - buttons
       for (int y = 0; y < 30; y++) {
           var label = skilllevelLabels[y];
           var mod = skillmodLabels[y];
           plusBtns[y].onClick.AddListener(() => PlusOnClick(label, mod));
           minusBtns[y].onClick.AddListener(() => MinusOnClick(label, mod));
       }

           colorify();
    }
    //Minus Functions
    public void MinusOnClick(Text label, Text mod) {
        int sLevel =  Convert.ToInt32(label.text);
        int sCost = 50 + Convert.ToInt32(mod.text);
        label.text = CalculateMinus(sLevel, sCost).ToString();
        Debug.Log("MINUS");
    }
    private int CalculateMinus(int _sLevel, int _sCost) {
        if (_sLevel > 0) {
            developmentPoints += _sCost;
            _sLevel--;
        }
        return _sLevel;
    }
    //Plus Functions
    public void PlusOnClick(Text label, Text mod)
    {
        int sLevel = Convert.ToInt32(label.text);
        int sCost = 50 + Convert.ToInt32(mod.text);
        Debug.Log(mod.text);
        label.text = CalculatePlus(sLevel, sCost).ToString();
    }
    private int CalculatePlus(int _sLevel, int _sCost)
    {
        if (developmentPoints >= _sCost)
        {
            developmentPoints -= _sCost;
            _sLevel++;
        }
        return _sLevel;
    }
    void colorify() {
        Color newcol;
        Color newcol2;
        var buttons = GameObject.FindObjectsOfType<Button>();
        var textlbls = GameObject.FindObjectsOfType<Text>();
        for (int y = 0; y < textlbls.Length; y++)
        {
            if (Color.TryParseHexString("fff", out newcol))
            {
                textlbls[y].color = newcol;
            }
        }
        
        for (int i = 0; i < buttons.Length; i++)
        {
            if (Color.TryParseHexString("2E8DEF", out newcol2))
            {
                buttons[i].image.color = newcol2;
            }
        }
    }
}
