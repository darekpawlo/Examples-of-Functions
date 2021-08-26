using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abstract_DamageOnClick : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                ITakeDamage damageable = hitInfo.collider.GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    damageable.TakeDamage(1);
                }
            }
        }
    }
}
