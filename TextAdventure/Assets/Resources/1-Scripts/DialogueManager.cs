using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //public TextNode rootNode
    //public TextNode choicesMade;

    public TextMeshProUGUI bodyText;
    public RectTransform endGameScreen;
    public RectTransform startGameScreen;
    public RectTransform optionsContainer;
    public GameObject buttonPrefab;
    #region Cache
    public int edCounter;
    #endregion

    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        edCounter = 0;
    }

    /*void DisplayNode(TextNode _textNode)
    {
        bodyText.text = _textNode.GetText(edCounter);
    }*/

    void ClearOptions()
    {
        foreach(Transform option in optionsContainer)
        {
            option.gameObject.SetActive(false);
        }
    }

    /*void GenerateOptions(TextNode _nodes)
    {
        int index = 0;
        foreach (NodeOption option in _nodes.nodeOptions)
        {
            GameObject optionGO = optionsContainer.GetChild(index).gameObject;
            optionGO.SetActive(true);

            SetOption(optionGO, option);
        }
    }*/

    /*void SetOption(GameObject _nodeObject, NodeOption _option)
    {
        _nodeObject.GetComponentInChildren<TextMeshProUGUI>().text = _option.bodyText;
        // Get the button
    }*/
}
