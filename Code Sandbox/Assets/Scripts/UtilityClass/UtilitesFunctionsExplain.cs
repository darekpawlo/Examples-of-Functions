using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UtilitesFunctionsExplain : MonoBehaviour
{
    [SerializeField] private Tests tests;
    [SerializeField] private TextMeshProUGUI targetPostionText;
    [SerializeField] private TextMeshProUGUI resultText;
    private Vector2 targetPosition;

    [Header("RandomDir")]
    [SerializeField] private float maxTimer = 1;
    private Vector2 randomDir;
    private float timer;

    private enum Tests
    {
        Direction,
        Distance,
        RandomDir,
        AngleFromVector
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = UtilitiesClass.GetMouseWorldPosition();
        timer -= Time.deltaTime;

        switch (tests)
        {
            case Tests.Direction:                
                Vector2 val = UtilitiesClass.GetDirection(targetPosition, transform.position);
                Debug.Log(val);              
                
                resultText.SetText(val.ToString());

                SetTextPositions();

                break;

            case Tests.Distance:
                var val1 = UtilitiesClass.GetDistance(targetPosition, transform.position);
                Debug.Log(val1);
                
                resultText.SetText(val1.ToString());

                SetTextPositions();

                break;

            case Tests.RandomDir:
                if (timer <= 0)
                {
                    randomDir = UtilitiesClass.GetRandomDir();
                    Debug.Log(randomDir);

                    resultText.SetText(randomDir.ToString());

                    SetTextPositions(randomDir, false);

                    timer = maxTimer;
                }
                break;

            case Tests.AngleFromVector:
                var val2 = UtilitiesClass.GetAngleFromVector(targetPosition);

                resultText.SetText(val2.ToString());

                SetTextPositions(targetPosition, false);

                break;
        }
    }

    private void OnDrawGizmos()
    {
        switch (tests)
        {
            case Tests.Direction:
                Gizmos.DrawLine(transform.position, targetPosition);
                break;
            case Tests.Distance:
                Gizmos.DrawLine(transform.position, targetPosition);
                break;
            case Tests.RandomDir:
                Gizmos.DrawLine(transform.position, randomDir);
                break;
            case Tests.AngleFromVector:
                Gizmos.DrawLine(transform.position, targetPosition);
                break;
        }
    }

    private void SetTextPositions(Vector2 position = new Vector2(), bool activeTargetText = true)
    {        

        if (!activeTargetText)
        {
            resultText.transform.position = position;

            targetPostionText.SetText("");
        }
        else
        {
            resultText.transform.position = targetPosition / 2;

            targetPostionText.transform.position = targetPosition;
            targetPostionText.SetText(targetPosition.ToString());
        }     
    }
}
