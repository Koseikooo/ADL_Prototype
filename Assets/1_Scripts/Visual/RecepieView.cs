using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class RecepieView : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI Label;
	[SerializeField] TextMeshProUGUI Duration;
	[Space]
    [SerializeField] TextMeshProUGUI[] NeededAmount;
	[SerializeField] Image[] NeededResource;
    [SerializeField] TextMeshProUGUI CraftedAmount;
    [SerializeField] Image CraftedResource;
	[Header("Buttons")]
	[SerializeField] GameObject LockedUI;
	[SerializeField] GameObject CraftingUI;
	public Button StartButton;
    public Button SelectButton;
    public Button SwapButton;
    [Header("Buy")]
    public Button BuyButton;
    [SerializeField] TextMeshProUGUI BuyAmount;
    [SerializeField] TextMeshProUGUI BuyLabel;
    [SerializeField] Image BuyResource;
	[Space]
	[SerializeField] Sprite SufficientSprite;
	[SerializeField] Color SufficientColor;
	[SerializeField] Color SufficientTextColor;
	[SerializeField] Sprite InSufficientSprite;
    [SerializeField] Color InSufficientColor;
    [SerializeField] Color InSufficientTextColor;
    [Header("Collectable")]
    [SerializeField] GameObject Collectable;
    [SerializeField] Image CollectableIcon;
    [SerializeField] TextMeshProUGUI CollectableAmount;
    [SerializeField] TextMeshProUGUI CollectableXP;
    [SerializeField] TextMeshProUGUI RestDuration;
    [SerializeField] RectTransform ClockHandRect;

    Dictionary<RecepieState, GameObject> _buttonDict = new();
    Sprite _costSprite;
    Sprite _craftedSprite;
    List<Sprite> _neededSprites = new();

    Recepie _recepie;
	public Recepie Recepie
	{
		get => _recepie;
		set
		{
			_recepie = value;
            if (_recepie == null)
                return;

            _costSprite = RecepieService.GetResourceSprite(Recepie.Cost.Resource);

            for (int i = 0; i < Recepie.Needed.Length; i++)
            {
                _neededSprites.Clear();
                _neededSprites.Add(RecepieService.GetResourceSprite(Recepie.Needed[i].Resource));
            }

            _craftedSprite = RecepieService.GetResourceSprite(Recepie.Crafted.Resource);

			UpdateView();
		}
	}

    void SetupDict()
    {
        _buttonDict[RecepieState.Locked] = LockedUI;
        _buttonDict[RecepieState.NotEnoughResources] = BuyButton.gameObject;
        _buttonDict[RecepieState.CanBuy] = BuyButton.gameObject;
        _buttonDict[RecepieState.Inventory] = SelectButton.gameObject;
        _buttonDict[RecepieState.Swap] = SwapButton.gameObject;
        _buttonDict[RecepieState.Inactive] = StartButton.gameObject;
        _buttonDict[RecepieState.Active] = CraftingUI;
    }

    public void UpdateView()
	{
        SetupDict();

        Label.text = Recepie.Name;
        Duration.text = RecepieService.GetParsedSeconds(Recepie.TimeInSeconds);

        for (int i = 0; i < NeededResource.Length; i++)
            NeededResource[i].gameObject.SetActive(false);

        for (int i = 0; i < Recepie.Needed.Length; i++)
        {
            NeededResource[i].gameObject.SetActive(true);
            NeededResource[i].sprite = _neededSprites[i];
            NeededAmount[i].text = Recepie.Needed[i].Amount.ToString();
        }
        

        CraftedResource.sprite = _craftedSprite;
        CraftedAmount.text = Recepie.Crafted.Amount.ToString();


        UpdateButton();
	}

    public void UpdateActiveRecepie(int secondsLeft)
    {
        var left = RecepieService.GetParsedSeconds(secondsLeft);
        RestDuration.text = left;
    }

	public void UpdateButton()
	{
        foreach (var button in _buttonDict)
        {
            button.Value.SetActive(false);
        }

        _buttonDict[Recepie.State].SetActive(true);

        switch (Recepie.State)
        {

            case RecepieState.NotEnoughResources:
                ((Image)BuyButton.targetGraphic).sprite = InSufficientSprite;
                BuyLabel.color = InSufficientTextColor;
                BuyAmount.color = InSufficientColor;
                BuyAmount.text = Recepie.Cost.Amount.ToString();
                BuyResource.sprite = _costSprite;

                
                break;

            case RecepieState.CanBuy:
                ((Image)BuyButton.targetGraphic).sprite = SufficientSprite;
                BuyLabel.color = SufficientTextColor;
                BuyAmount.color = SufficientTextColor;
                BuyAmount.text = Recepie.Cost.Amount.ToString();
                BuyResource.sprite = _costSprite;

                BuyButton.onClick.RemoveAllListeners();
                BuyButton.onClick.AddListener(() =>
                {
                    InventoryManager.acc.AddRecepie(Recepie);

                });
                break;

            case RecepieState.Inventory:

              
                break;

            case RecepieState.Swap:
                break;

            case RecepieState.Inactive:
                break;

            case RecepieState.Active:

                break;
        }
    }
}

