using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStem : MonoBehaviour
{
    public GameObject steam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(steam, transform.position, Quaternion.identity);
        }
            
    }
}
