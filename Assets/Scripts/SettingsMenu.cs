using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;

    [Header("Keys")]
    [SerializeField] private string saveVolumekey;

    [Header("Tags")]
    [SerializeField] private string sliderTag;
    [SerializeField] private string textVolumeTag;

    [Header("Parametrs")]
    [SerializeField] private float volume;

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
            this.slider = sliderObj.GetComponent<Slider> ();
            this.volume = slider.value;
        }

        if (this.audio.volume != this.volume)
        {
            PlayerPrefs.SetFloat(this.saveVolumekey, this.volume);
        }

        GameObject textObj = GameObject.FindWithTag(this.textVolumeTag);

        if (textObj != null )
        {
            this.text = textObj.GetComponent<Text> ();
            this.text.text = Mathf.Round( f: this.volume * 100 ) + "%";
        }

        this.audio.volume = this.volume;
    }
}