using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int manaCost;
    [SerializeField] private TextMeshProUGUI manaText;

    void Start()
    {
        manaText.text = $"{manaCost}";
    }
}
