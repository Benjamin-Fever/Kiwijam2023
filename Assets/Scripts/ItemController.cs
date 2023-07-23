using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.PointerEventData;

public class ItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 itemLocation;
    private Vector2 offset;

    [SerializeField] private GameManager.Actions actionType = GameManager.Actions.None;
    [SerializeField] private RectTransform region;
    [SerializeField] private RectTransform attackPosition;
    [SerializeField] private Image wateringCan;
    [SerializeField] private Sprite wateringCanImg1;
    [SerializeField] private Sprite wateringCanImg2;

    [SerializeField] private Image umbrella;
    [SerializeField] private Sprite umbrellaImg1;
    [SerializeField] private Sprite umbrellaImg2;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        itemLocation = rectTransform.anchoredPosition;
    }

    private void Update()
    {
        // Gradually move the item back to its original position
        if (GameManager.instance.action != actionType)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(itemLocation, rectTransform.anchoredPosition, 0.95f);
            return;
        }
        // Move the item the players position
        // TODO: Make compatiable for mobile use!!
        if (actionType == GameManager.Actions.Attack)
            offset = new Vector2(50, 200);
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 newPosition = mousePosition - offset;
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, newPosition, 0.7f);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (actionType == GameManager.Actions.Attack)
            {
                GameManager.instance.attack();
            }
        }

        // Change action type if item is in region
        if (RectTransformUtility.RectangleContainsScreenPoint(region, mousePosition))
        {
            GameManager.instance.actioning = true;
            if (actionType == GameManager.Actions.Water)
                wateringCan.sprite = wateringCanImg2;
            else if (actionType == GameManager.Actions.Blocksun)
                umbrella.sprite = umbrellaImg2;
        }
        else
        {
            GameManager.instance.actioning = false;
            if (actionType == GameManager.Actions.Water)
                wateringCan.sprite = wateringCanImg1;
            else if (actionType == GameManager.Actions.Blocksun)
                umbrella.sprite = umbrellaImg1;
        }
    }

    // On mouse click
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left && actionType != GameManager.instance.action)
        {
            GameManager.instance.action = actionType;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }
    }

    // On mouse release
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left && actionType != GameManager.Actions.Attack)
        {
            GameManager.instance.action = GameManager.Actions.Attack;
        }
    }

    public Vector3 ConvertCanvasToWorldSpace(Vector2 canvasPosition)
    {


        // Convert the screen position to a world space position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(canvasPosition.x, canvasPosition.y, Camera.main.nearClipPlane));

        return worldPosition;
    }
}