using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutParameter_Test : MonoBehaviour
{
    /*private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string erroMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.Prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        //If the area isn't clear stops the rest of the function
        if (!isAreaClear)
        {
            erroMessage = "Area is not clear!";
            return false;
        }

        //Checks if the building is in the minimum radius
        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            //Colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                //Has BuildingTypeHolder
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    //There's already a building of this type within the construction radius
                    erroMessage = "Too close to another building of the same type!";
                    return false;
                }
            }
        }

        if (buildingType.hasResourceGeneratorData)
        {
            ResourceGeneratorData resourceGeneratorData = buildingType.resourceGeneratorData;
            int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, position);

            if (nearbyResourceAmount == 0)
            {
                erroMessage = "There are no nearby Resource Nodes!";
                return false;
            }

        }


        //Checks if the building is in the maximum radius
        float maxConstructionRadius = 25;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            //Colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                //It's a building!
                erroMessage = null;
                return true;
            }
        }

        erroMessage = "Too far from any other building!";
        return false;
    }*/
}
