using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] GameObject endCanvas;
    [SerializeField] GameObject collectiblesMenu;

    // Start is called before the first frame update
    void Start()
    {
        startCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);
        collectiblesMenu.SetActive(false);
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
        if (GameManager.Instance.EnoughCollectibles())
        {
            collectiblesMenu.SetActive(true);
            GameManager.Instance.IncrementCollectibleAmount(-GameManager.Instance.GetCollectibleAmount());
            GameManager.Instance.IncrementRubyAmount(100);
        }
        else
        {
            endCanvas.SetActive(true);
        }
    }

    public void CloseCollectibleMenu()
    {
        collectiblesMenu.SetActive(false);
        endCanvas.SetActive(true);
    }
}
