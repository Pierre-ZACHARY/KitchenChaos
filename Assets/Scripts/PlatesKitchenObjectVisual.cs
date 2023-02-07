using System;
using UnityEngine;
using UnityEngine.UI;

public class PlatesKitchenObjectVisual: MonoBehaviour
{
         [SerializeField] private PlateKitchenObject plateKitchenObject;
         [SerializeField] private GameObject iconTemplate;
         [SerializeField] private GridLayoutGroup gridUI;

         private void Start()
         {
             plateKitchenObject.OnObjetsOnPlateChanged += OnObjetsOnPlateChanged;
         }

         private void OnEnable()
         {
                plateKitchenObject.OnObjetsOnPlateChanged += OnObjetsOnPlateChanged;
         }

         private void OnDisable()
         {
                plateKitchenObject.OnObjetsOnPlateChanged -= OnObjetsOnPlateChanged;
         }

         private void OnObjetsOnPlateChanged(object sender, PlateKitchenObject.OnObjetsOnPlateChangedEventArgs e)
         {
             foreach(Transform t in gridUI.transform)
             {
                 if(t.gameObject != iconTemplate)
                    Destroy(t.gameObject);
             }
             
            foreach (var item in e.objectsOnPlate)
            {
                GameObject icon = Instantiate(iconTemplate, gridUI.transform);
                icon.SetActive(true);
                icon.GetComponent<Image>().sprite = item.associatedKitchenObject.kitchenObjectSprite;
            }
         }
}