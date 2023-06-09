using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNdrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform imageTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    SoujiManager manager;
    private bool candrag = true;
    private Vector2 offset;
    private void Awake()
    {
        imageTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        offset = transform.localPosition;
        manager = GameObject.FindObjectOfType<SoujiManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!candrag)
            return;
        canvasGroup.alpha = 0.6f; // Reduce the image opacity during dragging
        canvasGroup.blocksRaycasts = false; // Allow raycasts to pass through the image

    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out Vector2 localPoint);
        if(candrag)
        imageTransform.localPosition = localPoint - new Vector2(115,0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!candrag)
            return;
        canvasGroup.alpha = 1f; // Restore the image opacity
        canvasGroup.blocksRaycasts = true; // Enable raycasts for the image
                                           // Builds a ray from camera point of view to the mouse position 
        Vector2 mousePosition = Input.mousePosition;
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();

        System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            Image image = result.gameObject.GetComponent<Image>();
            if (image != null)
            {
                switch(image.name)
                {
                    case ("TrashBin"):
                        if (gameObject.tag == "delete")
                        {
                            manager.nFolder++;
                            if (manager.nFolder >= 10)
                                manager.LevelCompleted(1);
                        }
                        else if (gameObject.tag == "virus")
                        {
                            manager.nViruses--;
                            if (manager.nViruses <= 0)
                                manager.GameClear();
                        }
                        gameObject.SetActive(false);
                        break;
                    case ("slot"):
                        candrag = false;
                        manager.setfolders++;
                        if(manager.setfolders == 2)
                            manager.LevelCompleted(2);   
                        else if(manager.setfolders == 6)
                            manager.LevelCompleted(3);

                        break;
                }
            }
        }
    }
}
