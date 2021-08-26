using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay_Dictionary : MonoBehaviour
{
    //private Transform template;

    [SerializeField] private List<ResoureType_Dictionary> resourceTypeList = new List<ResoureType_Dictionary>();

    private Dictionary<ResoureType_Dictionary, Transform> resourceTypeTransform = new Dictionary<ResoureType_Dictionary, Transform>();

    private void Awake()
    {
        Transform template = transform.Find("ResourceDisplayTemplate");
        template.gameObject.SetActive(false);

        int index = 0;
        foreach (ResoureType_Dictionary resoureType in resourceTypeList)
        {
            Transform resourceTransform = Instantiate(template, transform);
            resourceTransform.gameObject.SetActive(true);

            float offset = 200;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);

            resourceTypeTransform[resoureType] = resourceTransform;

            index++;
        }
    }

    private void Start()
    {
        UpdateResourceAmount();
        ResourceManager_Dictionary.instance.OnResourceAdded += Instance_OnResourceAdded;
    }

    private void Instance_OnResourceAdded(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResoureType_Dictionary resoureType in resourceTypeList)
        {
            Transform resourceTransform = resourceTypeTransform[resoureType];

            int resourceAmount = ResourceManager_Dictionary.instance.GetResourceAmount(resoureType);
            resourceTransform.Find("Holder").Find("Text").GetComponent<TextMeshProUGUI>().SetText(resoureType.stringName + ": " + resourceAmount.ToString());
        }
    }
}
