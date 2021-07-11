using UnityEngine;

public class TrackFading : MonoBehaviour
{

     public float maxVolume;
     public float fadeRate = 0.1f;

     [SerializeField]
     
     private float fadeInvert = 1f;
     private bool beginFade = false;
     private AudioSource source = null;


     private void OnEnable()
     {
          source = GetComponent<AudioSource>();
     }

     private void Update()
     {
          if (beginFade)
          {
               source.volume += (Time.deltaTime * fadeRate * fadeInvert);

               // If this source is near max volume, stop fading.
               if (source.volume >= maxVolume)
                    beginFade = false;

               // If this source's volume is less than 0, delete this object.
               if (source.volume <= 0f)
               {
                    Destroy(gameObject);
               }
          }
     }


     // Fade the track out
     public void FadeOut()
     {
          fadeInvert = -1f;
          beginFade = true;
     }


     // Fade the track in
     public void FadeIn()
     {
          fadeInvert = 1f;
          beginFade = true;
     }

}
