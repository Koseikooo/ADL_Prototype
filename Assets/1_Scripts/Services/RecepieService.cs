using System;
using UnityEngine;

public static class RecepieService
{
	public static int GetResourceXP(Resource resource)
	{
        return resource switch
        {
            Resource.water => 1,
            Resource.seawater => 2,
            Resource.fire => 2,
            Resource.heat => 4,
            Resource.steam => 8,
            _ => 0,
        };
    }

    public static Sprite GetResourceSprite(Resource resource)
    {
        var sprite = Resources.Load<Sprite>($"ResourceIcons/{resource}");
        return sprite;
    }

    public static string GetParsedSeconds(int seconds)
    {
        return seconds.ToString();
    }
}

