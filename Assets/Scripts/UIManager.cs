using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDictionaryElement[] dictionaryElements;
    private Dictionary<UI_ELEMENT, GameObject> elements;
    public static UIManager Instance;

    void Awake()
    {
        if (Instance != null) { return; }
        Instance = this;

        elements = new();
        foreach (UIDictionaryElement element in dictionaryElements)
        {
            elements[element.elementType] = element.element;
        }
    }

    private void OnEnable()
    {
        GameManager.OnLevelStarted += ShowLevelUI;
    }

    public void UpdateLevelInfoUI()
    {
        Debug.Log("New player gold: " + PlayerStats.Coin + ", new player health: " + PlayerStats.Lives);
    }

    public void ShowTowerMenu()
    {
        elements[UI_ELEMENT.TOWER_SELECTION_MENU].transform.position = Node.CurrentNode.transform.position;
        elements[UI_ELEMENT.TOWER_SELECTION_MENU].SetActive(true);
    }

    public void CloseTowerMenu()
    {
        elements[UI_ELEMENT.TOWER_SELECTION_MENU].SetActive(false);
    }

    private void ShowLevelUI()
    {
        elements[UI_ELEMENT.LEVEL_INFO_CANVAS].SetActive(true);
        UpdateCoin();
        UpdateLive();
    }

    private void OnDisable()
    {
        GameManager.OnLevelStarted -= ShowLevelUI;
    }

    public void UpdateCoin() { elements[UI_ELEMENT.COIN_TEXT].GetComponent<TMP_Text>().text = ((int)PlayerStats.Coin).ToString(); }
    public void UpdateLive() { elements[UI_ELEMENT.LIVES_TEXT].GetComponent<TMP_Text>().text = PlayerStats.Lives.ToString(); }
}

[System.Serializable]
public enum UI_ELEMENT
{
    LEVEL_INFO_CANVAS,
    COIN_TEXT,
    LIVES_TEXT,
    TOWER_SELECTION_MENU
}

[System.Serializable]
public struct UIDictionaryElement
{
    public UI_ELEMENT elementType;
    public GameObject element;
}
