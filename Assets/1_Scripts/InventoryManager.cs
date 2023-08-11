using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager acc;
    public static event Action OnInventoryChanged;

    public Dictionary<Resource, int> ResourceDict = new();

    public List<Recepie> Recepies = new();

    [Header("Visual")]

    [SerializeField] Transform InventoryParent;
    [SerializeField] List<RecepieView> ViewPool;

    private void Awake()
    {
        acc = this;
    }

    private void Start()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Resource)).Length; i++)
        {
            ResourceDict[(Resource)i] = 0;
        }

        ResourceDict[Resource.water] = 100;
        ResourceDict[Resource.fire] = 100;
    }

    public void AddRecepie(Recepie recepie)
    {
        recepie.View = null;
        var newRecepie = new Recepie(recepie);
        newRecepie.State = RecepieState.Inventory;

        Recepies.Add(newRecepie);
        UpdateInventoryVisual();
    }

    public void RemoveRecepie(Recepie recepie)
    {
        UpdateInventoryVisual();
        
    }

    public void UpdateInventoryVisual()
    {
        for (int i = 0; i < ViewPool.Count; i++)
        {
            if(i >= Recepies.Count)
            {
                ViewPool[i].Recepie = null;
                ViewPool[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < Recepies.Count; i++)
        {
            if (Recepies[i].View != null)
                continue;

            var index = i;
            var view = InventoryService.GetPooledView(ViewPool, InventoryParent);
            Recepies[i].AssignView(view);
            view.Recepie = Recepies[i];

            view.SelectButton.onClick.RemoveAllListeners();
            view.SelectButton.onClick.AddListener(() =>
            {
                CraftManager.acc.AddToCrafting(Recepies[index]);
            });
        }
    }

    public void AddResource(ResourceAmount resource)
    {
        ResourceDict[resource.Resource] += resource.Amount;
        Debug.Log($"{resource.Resource}: {ResourceDict[resource.Resource]}");
        OnInventoryChanged?.Invoke();
    }

    public bool TryRemoveResource(ResourceAmount resource)
    {
        if (!InventoryService.HasResourceAmount(resource))
            return false;

        ResourceDict[resource.Resource] -= resource.Amount;
        OnInventoryChanged?.Invoke();
        return true;
    }
}
