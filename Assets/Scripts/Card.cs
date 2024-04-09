using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public int manaCost;
    [SerializeField] private TextMeshProUGUI manaText;

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        manaText.text = $"{manaCost}";
    }
}
