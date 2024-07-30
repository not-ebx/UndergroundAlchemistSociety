using System;
using Script.BodyParts;
using Script.Loaders;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Script.DropSystem
{
    [Serializable]
    public class ItemCard : MonoBehaviour
    {
         public BodyPart bodyPart;
     
         public static ItemCard CreateItemCardPicker(BodyPart bodyPart, Transform parent)
         {
             GameObject itemCardPickerGameObject = new GameObject("ItemCardPicker_"+bodyPart.id);
             itemCardPickerGameObject.transform.parent = parent;
             itemCardPickerGameObject.transform.localPosition = Vector3.zero;
             itemCardPickerGameObject.transform.localScale = Vector3.one;
 
             ItemCard itemCardPicker = itemCardPickerGameObject.AddComponent<ItemCard>();
             itemCardPicker.bodyPart = bodyPart;
 
             return itemCardPicker;
         }
         
         public BodyPart ChooseBodyPart()
         {
             return bodyPart != null ? bodyPart : null;
         }
         
         public GameObject InitializeCardButton(Vector2 localPosition)
         {
             if (bodyPart != null)
             {
                 Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();   
                 GameObject itemCardButtonPrefab = Resources.Load<GameObject>("ItemCardButton");
                 GameObject itemCardButton = Instantiate(itemCardButtonPrefab, canvas.transform);
                 itemCardButton.name = "ItemCardButton_" + bodyPart.id;
                 
                 TextMeshProUGUI titleText = itemCardButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
                 titleText.text = bodyPart.name;
 
                 TextMeshProUGUI descText = itemCardButton.transform.Find("Description").GetComponent<TextMeshProUGUI>();
                 descText.text = bodyPart.description;
                 
                 Image buttonImage = itemCardButton.transform.Find("Image").GetComponent<Image>();
                 buttonImage.sprite = bodyPart.sprite;
                 buttonImage.color = bodyPart.color;

                 RectTransform rectTransform = itemCardButton.GetComponent<RectTransform>();
                 rectTransform.anchoredPosition += localPosition;
                 
                 // On Click event
                 Button buttonComponent = itemCardButton.GetComponent<Button>();
                 buttonComponent.onClick.AddListener(() =>
                 {
                     GameManager.Instance.SelectDropAndContinue(bodyPart.id);
                 });
             }
             return null;
         }   
    }
}