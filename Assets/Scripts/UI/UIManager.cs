using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] GameObject endCanvas;
    [SerializeField] GameObject collectiblesMenu;

    [SerializeField] FloaterController floaterPrefab;
    [SerializeField] Transform collectiblesMenuFloaterSpawn;
    [SerializeField] Transform endRubiesFloaterSpawn;
    [SerializeField] TargetManager targetManager;
    [SerializeField] Transform floaterTarget;


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
            StartCoroutine(SpawnFloaters(collectiblesMenuFloaterSpawn, floaterTarget, 100));
        }
        else
        {
            endCanvas.SetActive(true);
            StartCoroutine(SpawnFloaters(endRubiesFloaterSpawn, floaterTarget, targetManager.GetRubiesGainedPerLevel()));
        }
    }

    public void CloseCollectibleMenu()
    {
        collectiblesMenu.SetActive(false);
        endCanvas.SetActive(true);
        StartCoroutine(SpawnFloaters(endRubiesFloaterSpawn, floaterTarget, targetManager.GetRubiesGainedPerLevel()));
    }

    IEnumerator SpawnFloaters(Transform spawnPoint, Transform targetPoint, int floaterNum)
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < floaterNum; i++)
        {
            Vector3 randomSpawnPos = spawnPoint.position + (Vector3)Random.insideUnitCircle;
            FloaterController floater = Instantiate(floaterPrefab, randomSpawnPos, Quaternion.identity, spawnPoint);
            floater.InitializeFloater(targetPoint.position);
        }
        GameManager.Instance.IncrementRubyAmount(floaterNum);
    }



}
