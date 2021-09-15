using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that can be accessed by any script without needing to be in scene
//For more info check "UtilitesFunctionsExplain.cs"
public static class UtilitiesClass
{
    private static Camera mainCamera;

    //Returns mouse position relative to the screen
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        return mouseWorldPosition;
    }

    //Returns a normalized random x and y vector
    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    //Returns angle from vector
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

    //Returns the direction, exmaple:
    //-if targetPosition is directly on top of startPosition, the value will be (0,1,0)
    //-if targetPosition is directly under startPosition, the value will be (0,-1,0)
    //-if targetPosition is directly right of startPosition, the value will be (1,0,0)    
    public static Vector3 GetDirection(Vector3 targetPosition, Vector3 startPosition)
    {
        return (targetPosition - startPosition).normalized;
    }
    
    //Returns the distance between two points
    public static float GetDistance(Vector3 targetPosition, Vector3 startPosition)
    {
        return Vector3.Distance(targetPosition, startPosition);
    }

    public static TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public static Vector3 GetMouseWorldPosition3D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, new LayerMask()))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
