using System;
using UnityEngine;
using System.Collections.Generic;

public static class InventoryService
{
	public static bool HasResourceAmount(ResourceAmount resource)
	{
		return InventoryManager.acc.ResourceDict[resource.Resource] >= resource.Amount;
    }

	public static RecepieView GetPooledView(List<RecepieView> pool, Transform spawnParent)
	{
		for (int i = 0; i < pool.Count; i++)
		{
			if (pool[i].gameObject.activeSelf)
				continue;
			else
			{
				pool[i].transform.SetParent(spawnParent);
				pool[i].gameObject.SetActive(true);
                return pool[i];
            }
				
		}

		var pre = Resources.Load<RecepieView>("Recepie");
		var newView = GameObject.Instantiate(pre, spawnParent);
		pool.Add(newView);
		return newView;

	}
}

