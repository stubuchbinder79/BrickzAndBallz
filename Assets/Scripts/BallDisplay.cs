using UnityEngine;
using TMPro;

public class BallDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text numberOfBallsText;

    private int _numberOfBalls = 0;
    public int numberOfBalls {
        set {
            _numberOfBalls = value;
            numberOfBallsText.text = "x" +_numberOfBalls.ToString();
            if (value <= 0)
                numberOfBallsText.text = "";
        } get {
            return _numberOfBalls;
        }
    }

    private void Awake()
    {
        numberOfBallsText = (numberOfBallsText != null) ? numberOfBallsText : GetComponentInChildren<TMP_Text>();
    }
}
