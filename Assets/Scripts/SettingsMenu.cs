using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public AudioSource audio;
    [SerializeField] public Slider slider;
    [SerializeField] public Text text;

    [Header("Keys")]
    [SerializeField] public string saveVolumekey;

    [Header("Tags")]
    [SerializeField] public string sliderTag;
    [SerializeField] public string textVolumeTag;

    [Header("Parametrs")]
    [SerializeField] public float volume;

    private void Avake()
    {
        if(PlayerPrefs.HasKey(this.saveVolumekey))
        {
            this.volume = PlayerPrefs.GetFloat(this.saveVolumekey);
            this.audio.volume = this.volume;

            GameObject sliderObj = GameObject.FindWithTag(this.sliderTag);
            if (sliderObj != null)
            {
                this.slider = sliderObj.GetComponent<Slider>();
                this.slider.value = this.volume;
            }
        }
        else
        {
            this.volume = 0.5f;
            PlayerPrefs.SetFloat(this.saveVolumekey, this.volume); 
            this.audio.volume = this.volume;
        }
    }

    private void LateUpdate()
    {
        GameObject sliderObj = GameObject.FindWithTag(this.sliderTag);
        if (sliderObj != null ) 
        { 
            this.slider = sliderObj.GetComponent<Slider>();
            this.volume = slider.value;
        }

        if (this.audio.volume != this.volume)
        {
            PlayerPrefs.SetFloat(this.saveVolumekey, this.volume);
        }

        GameObject textObj = GameObject.FindWithTag(this.textVolumeTag);

        if (textObj != null )
        {
            this.text = textObj.GetComponent<Text>();
            this.text.text = Mathf.Round( f: this.volume * 100 ) + "%";
        }

        this.audio.volume = this.volume;
    }
}