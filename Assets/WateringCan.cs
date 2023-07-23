using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [SerializeField] private Transform particle;
    void Update()
    {
        if (GameManager.instance.actioning && GameManager.instance.action == GameManager.Actions.Water)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            particle.transform.position = new Vector3(rectTransform.position.x / 200, rectTransform.position.y / 100, rectTransform.position.z / 100);
            particle.gameObject.SetActive(true);
        }
        else
        {
            //particle.gameObject.SetActive(false);
        }
    }
}
