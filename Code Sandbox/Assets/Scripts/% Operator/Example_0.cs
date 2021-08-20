using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_0 : MonoBehaviour
{
    private float seconds;
       
    void Update()
    {
        //Adds values over time (1 second per second)
        seconds += Time.deltaTime;

        //When the value reaches value that is after % it starts from 0 again,
        //but the total value doesn't change
        int displaySeconds = (int)seconds % 60;
                
        //Examples
        int displayMinutes = ((int)seconds / 60) % 60;
        float displayHours = ((int)seconds / 3600) % 24;
        float displayDays = ((int)seconds / 86400) % 30;
        float displayMonths = ((int)seconds / 2592000) % 12;
        float displayYears = ((int)seconds / 31536000);

        //For claricifactions 
        Debug.Log("Total value: " + seconds);
        Debug.Log("Value that resets: " + displaySeconds);
    }
}
