using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIOptionsController : MonoBehaviour
{
    [SerializeField] Text currentRubyAmountText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRubyAmount();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRubyAmount();
    }

    public void UpdateRubyAmount()
    {
        currentRubyAmountText.text = GameManager.Instance.GetRubyAmount().ToString();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}
