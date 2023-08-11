using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public static CraftManager acc;

    public List<Recepie> CraftRecepies = new();

    [SerializeField] List<RecepieView> ViewPool;
    [SerializeField] Transform CraftParent;

    List<Coroutine> activeCraftingRoutines = new();

    private void Awake()
    {
        acc = this;
    }

    public void AddToCrafting(Recepie recepie)
    {
        InventoryManager.acc.Recepies.Remove(recepie);
        CraftRecepies.Add(recepie);
        recepie.State = RecepieState.Inactive;

        UpdateCraftingVisual();
        InventoryManager.acc.UpdateInventoryVisual();
    }

    public void RemoveRecepie(Recepie recepie)
    {
        InventoryManager.acc.Recepies.Add(recepie);
        CraftRecepies.Remove(recepie);

        UpdateCraftingVisual();
        InventoryManager.acc.UpdateInventoryVisual();
    }

    public void UpdateCraftingVisual()
    {
        for (int i = 0; i < ViewPool.Count; i++)
        {
            if(i >= CraftRecepies.Count)
            {
                ViewPool[i].Recepie = null;
                ViewPool[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < CraftRecepies.Count; i++)
        {
            int index = i;
            var view = InventoryService.GetPooledView(ViewPool, CraftParent);
            CraftRecepies[i].AssignView(view);
            view.Recepie = CraftRecepies[i];

            view.StartButton.onClick.RemoveAllListeners();
            view.StartButton.onClick.AddListener(() =>
            {
                CraftRecepies[index].State = RecepieState.Active;
                var routine = StartCoroutine(CraftRecepies[index].CraftingRoutine(() =>
                {
                    CraftRecepies[index].State = RecepieState.Inactive;
                    InventoryManager.acc.AddResource(CraftRecepies[index].Crafted);
                }));

                //activeCraftingRoutines.Add(routine);
            });
            
        }
    }


}
