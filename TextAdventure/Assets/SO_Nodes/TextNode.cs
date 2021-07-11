using UnityEngine;


[System.Serializable]
public class EdOptions
{
     public int requiredMeetings = 0;
     public string bodyText = "";
}


[System.Serializable]
public class NodeOption
{
     public TextNode linkedNode;
     public string bodyText = "";
     public TextNode requiredPreviousNode;
}



[CreateAssetMenu(fileName = "New Text Node", menuName = "Text Node")]
public class TextNode : ScriptableObject
{
     [Header("Is Edquardo met here?")]
     public bool edMeeting = false;

     [Header("Text and required meetings for Edguardo")]
     public EdOptions[] edOptions;

     [Header("Next Nodes Transitioning To")]
     public NodeOption[] nodeOptions;

     [Header("Audio")]
     public AudioClip sfx;
     public AudioClip AmbienceTrack;
     public AudioClip MusicTrack;



     // Get the EdOption based on the number of times we've met them.
     public string GetText(int edMeetings)
     {
          // Start with a debug log.
          string finalText = "No text found.";

          // Go through the list of Ed Options, starting from the END of the list
          for (int i = edOptions.Length - 1; i >= 0; i--)
          {
               // And find the text node with ENOUGH required meetings
               if ( edMeetings >= edOptions[i].requiredMeetings)
               {
                    finalText = edOptions[i].bodyText;
                    return finalText;
               }
          }

          // If no option was found, return debug string.
          return finalText;
     }

    public void GoToThisOption()
    {
        DialogueManager.Instance.GetNextNode(this);
    }
}
