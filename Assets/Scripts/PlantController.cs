using UnityEngine;

public class PlantController : MonoBehaviour
{
    private static PlantController plantController = null;
    public static PlantController instance { get { return plantController; } }
    [SerializeField] private float followSpeed;
    [SerializeField] private Transform flower;
    [SerializeField] private Transform lookAt;

    private Sprite[] vines;

    private void Start()
    {
        vines = Resources.LoadAll<Sprite>("Plant\\Vines");
        plantController = this;
    }


    private void Update()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(lookAt.position.x, lookAt.position.y, -10f), 0.125f);
    }

    /// <summary>
    /// Method to grow the plant
    /// </summary>
    public void growPlant()
    {
        GameObject stemObj = new GameObject();
        stemObj.AddComponent<SpriteRenderer>().sprite = vines[0];
        stemObj.transform.parent = transform.GetChild(0).GetChild(0);
        stemObj.transform.position = new Vector3(flower.transform.parent.position.x, flower.transform.parent.position.y + 3.5f, 0f);
        stemObj.transform.localScale = Vector3.one;
        flower.transform.position = new Vector3(flower.transform.position.x, flower.transform.position.y + 3.5f, flower.transform.position.z);
        flower.parent.GetComponent<SpriteRenderer>().sprite = vines[Random.Range(1, 12)];
        flower.parent = stemObj.transform;
    }
}