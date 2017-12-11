using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StarDisplay : MonoBehaviour
{
    private Text _scoreDisplay;
    public int Stars = 50;

    void Start()
    {
        _scoreDisplay = GetComponent<Text>();
        _scoreDisplay.text = Stars.ToString();
    }

    public StarTransactionStatus AddStars(int amount)
    {
        _scoreDisplay.text = (Stars += amount).ToString();
        return StarTransactionStatus.SUCCESS;
    }

    public StarTransactionStatus UseStars(int amount)
    {
        if (amount > Stars) return StarTransactionStatus.FAILURE;

        _scoreDisplay.text = (Stars -= amount).ToString();
        return StarTransactionStatus.SUCCESS;
    }
}
