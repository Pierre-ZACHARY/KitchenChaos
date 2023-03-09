using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BindingsGridLayout : MonoBehaviour
{
    [SerializeField] private GameObject panelTemplate;
    [SerializeField] private GridLayoutGroup gridUI;
    [SerializeField] private GameInput gameInput;

    private void Start()
    {
        panelTemplate.gameObject.SetActive(false);
        foreach(GameInput.Bindings b in Enum.GetValues(typeof(GameInput.Bindings)))
        {
            GameObject t = Instantiate(panelTemplate, gridUI.transform);
            t.gameObject.SetActive(true);
            BindingPannel bp = t.GetComponent<BindingPannel>();
            bp.Init(b);
        }
    }
}
