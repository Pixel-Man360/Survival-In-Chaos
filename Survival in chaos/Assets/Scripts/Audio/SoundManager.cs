using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
   [SerializeField] private Sound[] sounds;

   private Dictionary<string, Sound> dictionary = new Dictionary<string, Sound>();
   public static SoundManager instance;

   public AudioSource levelMusicSource;

   void Awake()
   {
       if(instance == null)
         {
             instance = this;
         }

       else
       {
          Destroy(gameObject);
          return;
       }

     

       for(int i = 0 ; i < sounds.Length; i++)
       { 
           if(sounds[i].name != UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
           {
               sounds[i].source = gameObject.AddComponent<AudioSource>();
           }
           else
           {
               sounds[i].source = levelMusicSource;
           }
           sounds[i].source.clip = sounds[i].clip;
           sounds[i].source.volume = sounds[i].volume;
           sounds[i].source.pitch = sounds[i].pitch;
           sounds[i].source.loop = sounds[i].loop;
           sounds[i].source.playOnAwake = sounds[i].canPlayOnAwake;

           dictionary[sounds[i].name] = sounds[i];
       }

       if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level")
       {
           PlaySound("Level");
       }
   }

   void OnDestroy()
   {
       instance = null;
   }


   public void PlaySound(string name)
   {

     if(dictionary.ContainsKey(name))
     {
         dictionary[name].source.Play();
     }

     else
      return;
   }



}
    
