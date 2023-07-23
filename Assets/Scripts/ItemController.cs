using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

public class ItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 itemLocation;
    private Vector2 offset;

    [SerializeField] private GameManager.Actions actionType = GameManager.Actions.None;
    [SerializeField] private RectTransform region;


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
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 newPosition = mousePosition - offset;
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, newPosition, 0.7f);

        // Change action type if item is in region
        if (RectTransformUtility.RectangleContainsScreenPoint(region, mousePosition))
            GameManager.instance.actioning = true;
        else
            GameManager.instance.actioning = false;
    }

    // On mouse click
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left && actionType != GameManager.instance.action)
        {
            Debug.Log(this.name);
            GameManager.instance.action = actionType;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
            if (actionType == GameManager.Actions.Attack)
            {
                Debug.Log("Check attacker and damage");
            }
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
}