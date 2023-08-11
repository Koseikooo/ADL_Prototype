using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Recepie[] ShopRecepies;

    [Header("Visual")]

    [SerializeField] Transform ShopParent;
    [SerializeField] RecepieView RecepieViewPrefab;

    [SerializeField] List<RecepieView> ViewPool;

    private void Start()
    {
        for (int i = 0; i < ShopRecepies.Length; i++)
        {
            var view = Instantiate(RecepieViewPrefab, ShopParent);

            view.Recepie = new(ShopRecepies[i]);
            

            view.Recepie.State = InventoryService.HasResourceAmount(view.Recepie.Cost) ?
                RecepieState.CanBuy :
                RecepieState.NotEnoughResources;

            view.Recepie.AssignView(view);
        }
    }
}
