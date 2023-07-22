using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

public class ItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform imageRectTransform;
    private Vector2 originalPosition;
    private Vector2 offset;
    private bool isMoving;
    [SerializeField] private GameManager.Actions actionType = GameManager.Actions.None;
    [SerializeField] private RectTransform region;


    private void Awake()
    {
        imageRectTransform = GetComponent<RectTransform>();
        originalPosition = imageRectTransform.anchoredPosition;
    }

    private void Update()
    {
        // Gradually move the item back to its original position
        if (!isMoving)
        {
            imageRectTransform.anchoredPosition = Vector2.Lerp(originalPosition, imageRectTransform.anchoredPosition, 0.95f);
            return;
        }
        // Move the item the players position
        // TODO: Make compatiable for mobile use!!
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 newPosition = mousePosition - offset;
        imageRectTransform.anchoredPosition = newPosition;

        // Change action type if item is in region
        if (RectTransformUtility.RectangleContainsScreenPoint(region, mousePosition))
            GameManager.instance.action = actionType;
    }

    // On mouse click
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            isMoving = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }
    }

    // On mouse release
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            isMoving = false;
            GameManager.instance.action = GameManager.Actions.None;
        }
    }
}