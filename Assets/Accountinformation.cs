using UnityEngine;
using System.Collections;

public class Accountinformation : MonoBehaviour {
    public int userId { get; set; }
    public string username { get; set; }
    public string calculatedResult { get; set; }

    void Awake() {
        DontDestroyOnLoad(this.transform.gameObject);
    }

}
