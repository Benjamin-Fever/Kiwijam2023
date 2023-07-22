using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlantController : MonoBehaviour
{
    public float followSpeed;
    public Transform flower;
    public Transform lookAt;
    private Sprite[] vines;

    private void Start()
    {
        vines = Resources.LoadAll<Sprite>("Plant\\Vines");
    }


    private void Update()
    {
        Vector3 newPos = new Vector3(lookAt.position.x, lookAt.position.y, -10f);
        Camera.main.transform.position = newPos;

        if (Input.GetKeyDown("space"))
        {
            GameObject stemObj = new GameObject();
            stemObj.AddComponent<SpriteRenderer>().sprite = vines[Random.Range(0,12)];
            stemObj.transform.parent = transform.GetChild(0).GetChild(0);
            stemObj.transform.position = new Vector3(flower.transform.parent.position.x, flower.transform.parent.position.y + 2.5f, -0.01f);
            stemObj.transform.localScale = Vector3.one;
            flower.transform.position = new Vector3(flower.transform.position.x, flower.transform.position.y + 2.5f, flower.transform.position.z);
            flower.parent = stemObj.transform;
            
        }
    }
}
