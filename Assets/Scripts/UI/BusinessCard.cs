using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] private TMP_Text _incomeText;
    [SerializeField] private TMP_Text _lvlUpCostText;
    [SerializeField] private TMP_Text _lvlMaxText;
    [SerializeField] private Slider _progressSlider;

    public string businessName;

    public BusinessBonusButton bonusButton1;
    public BusinessBonusButton bonusButton2;

    public Button lvlUpButton;
    public Button bonus1Button;
    public Button bonus2Button;

    public void UpdateData(string name, int lvl, int income, int lvlUpCost, float incomeProgress, bool isMaxlvl)
    {
        businessName = name;
        _nameText.text = name;
        _lvlText.text = lvl.ToString();
        _incomeText.text = income.ToString();
        _lvlUpCostText.text = lvlUpCost.ToString();
        _progressSlider.value = incomeProgress;

        if (isMaxlvl)
        {
            _lvlMaxText.gameObject.SetActive(true);
            _lvlUpCostText.gameObject.SetActive(false);
        }
        else
        {
            _lvlMaxText.gameObject.SetActive(false);
            _lvlUpCostText.gameObject.SetActive(true);
        }
    }

    public void UpdateProgress(float progress)
    {
        _progressSlider.value = progress;
    }
}
