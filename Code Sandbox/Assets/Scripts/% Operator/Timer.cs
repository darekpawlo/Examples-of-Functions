using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private event EventHandler OnSecondsChanged;

    [SerializeField] [Range(1,6)] private int HowManyToSpawn;
    [SerializeField] private int TimeMultiplier = 1;
    private TMP_InputField inputField;

    private float seconds;
    private int previousValue = 0;

    private List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();

    private void Awake()
    {
        inputField = transform.parent.Find("TimeMultiplier").GetComponent<TMP_InputField>();
        SpawnEveryTimerText();
    }

    private void Start()
    {
        OnSecondsChanged += Timer_OnSecondsChanged;
    }

    private void Timer_OnSecondsChanged(object sender, EventArgs e)
    {
        UpdateTimerText();   
    }

    private void Update()
    {
        seconds += Time.deltaTime * TimeMultiplier;
        OnSecondsChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SpawnEveryTimerText()
    {
        Transform textTemplate = transform.Find("TextTemplate");
        textTemplate.gameObject.SetActive(false);

        for (int i = 0; i < HowManyToSpawn; i++)
        {
            Transform textTransform = Instantiate(textTemplate, transform);
            
            //Activates the TextHolderGameObject
            textTransform.gameObject.SetActive(true);

            TextMeshProUGUI text = textTransform.Find("Text").GetComponent<TextMeshProUGUI>();

            //Deactives Text from TextHolder
            text.gameObject.SetActive(false);

            //Activates Text from first TextHolder
            if (i == 0) text.gameObject.SetActive(true);

            textList.Add(text);
        }
    }

    private void UpdateTimerText()
    {
        float displaySeconds = (int)seconds % 60;
        float displayMinutes = ((int)seconds / 60) % 60;
        float displayHours = ((int)seconds / 3600) % 24;
        float displayDays = ((int)seconds / 86400) % 30;
        float displayMonths = ((int)seconds / 2592000) % 12;
        float displayYears = ((int)seconds / 31536000);

        for (int i = 0; i < HowManyToSpawn; i++)
        {
            switch (i)
            {
                case 0:

                    textList[i].SetText(displaySeconds.ToString() + " s");

                    break;
                case 1:

                    textList[i].SetText(displayMinutes.ToString() + " m");

                    break;
                case 2:

                    textList[i].SetText(displayHours.ToString() + " h");

                    break;
                case 3:

                    textList[i].SetText(displayDays.ToString() + " Days");

                    break;
                case 4:

                    textList[i].SetText(displayMonths.ToString() + " Months");

                    break;
                case 5:

                    textList[i].SetText(displayYears.ToString() + " Years");

                    break;
            }
        }
    }

    public void ShowText(int val)
    {
        if (previousValue > val)
        {
            for (int i = val + 1; i < textList.Count; i++)
            {
                textList[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = previousValue + 1; i <= val; i++)
            {
                textList[i].gameObject.SetActive(true);
            }
        }        
        previousValue = val;
    }

    public void TimeSpeed(string val)
    {
        if (val.Length > 0 && val[0] == '-')
        {
            SetTimerIfStringIsNumbers(val);
            inputField.text = val.Remove(0, 1);
        }
        else
        {
            SetTimerIfStringIsNumbers(val);
        }
    }

    private void SetTimerIfStringIsNumbers(string val)
    {
        if (int.TryParse(val, out int number))
        {
            TimeMultiplier = Mathf.Abs(number);
        }
    }
}
