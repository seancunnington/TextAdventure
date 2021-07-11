using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    #region Public Variables
    public TextNode rootNode;

    public TextMeshProUGUI bodyText;
    public RectTransform endGameScreen;
    public RectTransform startGameScreen;
    public RectTransform optionsContainer;
    #endregion

    #region Cache
    public int edCounter;
    public List<TextNode> optionsChosen = new List<TextNode>();
    #endregion

    #region Monobehaviour Callbacks
    private void Awake()
    {
        Initialize();
    }
    #endregion

    void Initialize()
    {
        edCounter = 0;
        DisplayNode(rootNode);
    }

    public void DisplayNode(TextNode _textNode)
    {
        if (_textNode.edMeeting == true)
        {
            edCounter++;
        }

        ClearOptions();
        bodyText.text = _textNode.GetText(edCounter);
        GenerateOptions(_textNode);

        optionsChosen.Add(_textNode);
    }

    void ClearOptions()
    {
        foreach(Transform option in optionsContainer)
        {
            option.gameObject.SetActive(false);
        }
    }

    void GenerateOptions(TextNode _node)
    {
        int index = 0;
        foreach (NodeOption option in _node.nodeOptions)
        {
            if (optionsChosen.Contains(option.requiredPreviousNode) || option.requiredPreviousNode == null)
            {
                GameObject optionGO = optionsContainer.GetChild(index).gameObject;
                optionGO.SetActive(true);

                SetOption(optionGO, option);
            }
        }
    }

    void SetOption(GameObject _nodeObject, NodeOption _option)
    {
        _nodeObject.GetComponentInChildren<TextMeshProUGUI>().text = _option.bodyText;
        _nodeObject.GetComponentInChildren<Button>().onClick.AddListener(_option.linkedNode.GoToThisOption);
    }
}
