[System.Serializable]
public class ResourceAmount
{
    public ResourceAmount(Resource resource, int amount)
    {
        Resource = resource;
        Amount = amount;
    }

    public ResourceAmount(ResourceAmount reference)
    {
        Resource = reference.Resource;
        Amount = reference.Amount;
    }

    public Resource Resource;
    public int Amount;
}
