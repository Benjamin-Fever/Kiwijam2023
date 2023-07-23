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
            particle.transform.position = Camera.main.ScreenToWorldPoint(transform.position);
            particle.gameObject.SetActive(true);
        }
        else
        {
            particle.gameObject.SetActive(false);
        }
    }
}
