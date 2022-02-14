using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Button button;
    private BowController controller;
    public BaseArrow arrowPrefab;

    private bool wasHovered = false;

    private void Start()
    {
        button = GetComponent<Button>();
        controller = Object.FindObjectOfType<BowController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        wasHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        wasHovered = false;
    }

    private void OnDisable()
    {
        if (wasHovered && arrowPrefab != null)
        {
            controller.equippedArrow = arrowPrefab;
            wasHovered = false;
        }
    }
}
