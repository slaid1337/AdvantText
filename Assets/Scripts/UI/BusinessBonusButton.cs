using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessBonusButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _incomeText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _buyedText;
    public Button button;

    public void UpdateData(string name, int income, int cost, bool isBuyed = false)
    {
        if (isBuyed)
        {
            _buyedText.gameObject.SetActive(true);
            _costText.gameObject.SetActive(false);
        }
        else
        {
            _buyedText.gameObject.SetActive(false);
            _costText.gameObject.SetActive(true);
        }

        _nameText.text = name;

        _incomeText.text = "+ " + income.ToString() + "%";
        _costText.text = cost.ToString() + "$";
    }
}
