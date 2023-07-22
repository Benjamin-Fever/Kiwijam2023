using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

public class ItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform imageRectTransform;
    private Vector2 originalPosition;
    private Vector2 offset;
    private bool isMoving;
    public RectTransform region;

    public GameManager.Actions actionType = GameManager.Actions.None;

    private void Awake()
    {
        imageRectTransform = GetComponent<RectTransform>();
        originalPosition = imageRectTransform.anchoredPosition;
    }

    private void Update()
    {
        if (!isMoving)
        {
            imageRectTransform.anchoredPosition = Vector2.Lerp(originalPosition, imageRectTransform.anchoredPosition, 0.95f);
            return;
        }

        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 newPosition = mousePosition - offset;
        imageRectTransform.anchoredPosition = newPosition;

        bool isInsideCanvasItem = RectTransformUtility.RectangleContainsScreenPoint(region, mousePosition);

        if (isInsideCanvasItem)
        {
            GameManager.instance.action = actionType;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            isMoving = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            isMoving = false;
            GameManager.instance.action = GameManager.Actions.None;
        }
    }
}