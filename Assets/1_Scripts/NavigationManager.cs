using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] GameObject ShopUI;
    [SerializeField] GameObject CraftUI;

    [SerializeField] Button ShopButton;
    [SerializeField] Button CraftButton;

    private void Awake()
    {
        ShopButton.onClick.RemoveAllListeners();
        CraftButton.onClick.RemoveAllListeners();

        ShopButton.onClick.AddListener(() => NavigateToUI(NavigationOption.Shop));
        CraftButton.onClick.AddListener(() => NavigateToUI(NavigationOption.Craft));
    }



    public void NavigateToUI(NavigationOption target)
    {
        ShopUI.SetActive(false);
        CraftUI.SetActive(false);

        switch (target)
        {
            case NavigationOption.Craft:
                CraftUI.SetActive(true);
                break;

            case NavigationOption.Shop:
                ShopUI.SetActive(true);
                break;
        }
    }
}
