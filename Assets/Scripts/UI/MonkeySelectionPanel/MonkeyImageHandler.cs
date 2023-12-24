using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Image monkeyImage;
        private MonkeyCellController owner;
        private Sprite spriteToSet;
        public RectTransform rectTransform;
        private Vector3 originalPosition;
        private Vector3 originalAnchoredPosition;

        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController owner)
        {
            this.spriteToSet = spriteToSet;
            this.owner = owner;
            rectTransform = GetComponent<RectTransform>(); // we need anchor position of image to be dragged, so we need rect transform object
        }

        private void Awake()
        {
            monkeyImage = GetComponent<Image>();
            monkeyImage.sprite = spriteToSet;
            originalPosition = rectTransform.position;
            originalAnchoredPosition = rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta;
            owner.MonkeyDraggedAt(rectTransform.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetMonkey();
            owner.MonkeyDroppedAt(eventData.position);
        }

        private void ResetMonkey()
        {
            // After releasing the mouse button, this method will be called.
            // Resetting monkey means, we should take the dragged image back to the location where it was originally
            monkeyImage.color = new Color(1, 1, 1, 1f); // making monkey image fully opaque
            rectTransform.position = originalPosition;
            rectTransform.anchoredPosition = originalAnchoredPosition;
            GetComponent<LayoutElement>().enabled = false;
            GetComponent<LayoutElement>().enabled = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // This method is used to fade out the monkey image while we are dragging it
            monkeyImage.color = new Color(1, 1, 1, 0.6f);
        }
    }
}