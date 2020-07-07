using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas startCanvas;
    [SerializeField] Canvas optionsCanvas;
    [SerializeField] Canvas endCanvas;

    // Start is called before the first frame update
    void Start()
    {
        startCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += HideStartCanvas;
        EventManager.Instance.onEndGamePlay += ShowEndCanvas;

    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= HideStartCanvas;
            EventManager.Instance.onEndGamePlay -= ShowEndCanvas;
        }
    }

    public void HideStartCanvas()
    {
        startCanvas.gameObject.SetActive(false);
    }

    public void ShowEndCanvas()
    {
        endCanvas.gameObject.SetActive(true);
    }
}
