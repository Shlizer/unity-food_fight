using System.Collections.Generic;
using UnityEngine;

namespace FoodFight
{
    [CreateAssetMenu(fileName = "Sound Manager", menuName = "Food Fight/Sound Manager")]
    public class SoundManager : ScriptableObject
    {
        [Header("Source")]
        public AudioSource source;

        [Header("UI")]
        public List<AudioClip> showMenu;
        public List<AudioClip> hideMenu;
        public List<AudioClip> btnHover;
        public List<AudioClip> btnClick;

        [Header("Gameplay")]
        public List<AudioClip> scored;
        public List<AudioClip> foodSliced;

        public void Play(AudioClip clip = null)
        {
            if (clip == null) return;
            if (source == null) source = Camera.main.GetComponent<AudioSource>();
            source?.PlayOneShot(clip);
        }

        public void Play(List<AudioClip> clipList) => Play(clipList.Count >= 0 ? clipList[Random.Range(0, clipList.Count)] : null);

        public void PlayMenuShow() => Play(showMenu);
        public void PlayMenuHide() => Play(hideMenu);
        public void PlayButtonHover() => Play(btnHover);
        public void PlayButtonClick() => Play(btnClick);
        public void PlayGameScored() => Play(scored);
        public void PlayGameFoodSliced() => Play(foodSliced);
    }
}
