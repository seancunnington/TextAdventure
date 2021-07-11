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
    public Animator bookAnim;
    public RectTransform startUI;
    public AudioClip pageFlipSFX;
    #endregion

    #region Cache
    public int edCounter;
    public List<TextNode> optionsChosen = new List<TextNode>();
    bool storyStarted = false;
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
        ClearPage();
    }

    void AnimTurnPage()
    {
        bookAnim.SetTrigger("TurnPage");
    }

    public void AnimCloseBook()
    {
        bookAnim.SetTrigger("CloseBook");
    }

    public void AnimOpenBook()
    {
        bookAnim.SetTrigger("OpenBook");
    }

    public void StartStory()
    {
        startUI.gameObject.SetActive(false);
        AnimOpenBook();
        GetNextNode(rootNode);
    }

    void ClearPage()
    {
        bodyText.text = "";
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

            index++;
        }
    }

    void SetOption(GameObject _nodeObject, NodeOption _option)
    {
        _nodeObject.GetComponentInChildren<TextMeshProUGUI>().text = _option.bodyText;
        _nodeObject.GetComponentInChildren<Button>().onClick.AddListener(_option.linkedNode.GoToThisOption);
    }

    public void GetNextNode(TextNode _textNode)
    {
        StartCoroutine(GetNextNodeCR(_textNode));
    }

    IEnumerator GetNextNodeCR(TextNode _textNode)
    {
        ClearPage();

        if (storyStarted)
        {
            AnimTurnPage();
            AudioManager.Instance.PlaySFX(pageFlipSFX);
        }
        else
        {
            storyStarted = true;
            AnimOpenBook();
        }

        yield return new WaitForSeconds(0.5F);

        AudioManager.Instance.RequestAmbienceTrack(_textNode.AmbienceTrack);
        AudioManager.Instance.RequestMusicTrack(_textNode.MusicTrack);

        if (_textNode.edMeeting == true)
        {
            edCounter++;
        }

        bodyText.text = _textNode.GetText(edCounter);
        GenerateOptions(_textNode);

        optionsChosen.Add(_textNode);
    }
}
