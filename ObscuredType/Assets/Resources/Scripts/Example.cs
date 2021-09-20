using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public Obscured<int> Score = new Obscured<int>(20);
    public Obscured<string> Name = new Obscured<string>("ABC");

    void Start()
    {
        Debug.Log($"Score : {Score.Value}");
        Score.Value = 32;
        Debug.Log($"Score : {Score.Value}");
        Score.Hack_SetValue(52);
        Debug.Log($"Score : {Score.Value}");

        Debug.Log($"Name : {Name.Value}");
        Name.Value = "CDE";
        Debug.Log($"Name : {Name.Value}");
        Name.Hack_SetValue("FGH");
        Debug.Log($"Name : {Name.Value}");
    }
}
