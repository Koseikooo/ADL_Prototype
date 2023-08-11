using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Recepie
{
    public Recepie(Recepie recepie)
	{
        Name = recepie.Name;
        State = recepie.State;
        TimeInSeconds = recepie.TimeInSeconds;

        Cost = new(recepie.Cost);

        Needed = new ResourceAmount[recepie.Needed.Length];
        for (int i = 0; i < recepie.Needed.Length; i++)
            Needed[i] = new(recepie.Needed[i]);

        Crafted = new(recepie.Crafted);
    }

    public string Name;
	[SerializeField] RecepieState _state;
    public RecepieState State
    {
		get => _state;
		set
		{
			_state = value;
			if(View != null)
				View.UpdateView();
		}
    }
    [Space]
	public int TimeInSeconds;
	[Space]
	public ResourceAmount Cost;
	[Space]
	public ResourceAmount[] Needed;
	[Space]
	public ResourceAmount Crafted;

	public int XpReward => RecepieService.GetResourceXP(Crafted.Resource) * Crafted.Amount;

	public RecepieView View;

	public void AssignView(RecepieView view)
	{
		if (view == null)
			return;

		View = view;
		View.Recepie = this;
	}

	public IEnumerator CraftingRoutine(Action OnComplete)
	{
		float time = 0f;
		float duration = TimeInSeconds;

		while (duration > 0f)
		{
			duration -= Time.deltaTime;
			View.UpdateActiveRecepie(Mathf.RoundToInt(duration));
			yield return null;
			
		}

		OnComplete?.Invoke();

	}


}

