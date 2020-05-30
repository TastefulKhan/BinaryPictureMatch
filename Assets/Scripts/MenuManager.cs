using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button EasyButton;
    //public Button MedButton;
    public Button HardButton;
    //public Button ExtrButton;
    //public Button quitGameButton;
    //public Button quesoButton;
    public string easyGameSceneName;
    //public string medGameSceneName;
    public string hardGameSceneName;
    //public string extrGameSceneName;
    //public string quesoSceneName;
    AudioSource audio_source;
    AudioClip sound;
    public Settings settings;
    
    public void Awake() {
        EasyButton.onClick.AddListener(EasyGame);
        //MedButton.onClick.AddListener(MedGame);
        HardButton.onClick.AddListener(HardGame);
        //ExtrButton.onClick.AddListener(ExtrGame);
        //quitGameButton.onClick.AddListener(QuitGame);
        //quesoButton.onClick.AddListener(QuesoButton);
        audio_source = gameObject.AddComponent<AudioSource>();
        sound = Resources.Load<AudioClip>("Audio/button");
    }

    public void EasyGame() {
        audio_source.clip = sound;
        audio_source.Play();
        settings.setDifficulty("easy");
        SceneManager.LoadScene("Game");
    }
    //public void MedGame() {
    //    audio_source.clip = sound;
    //    audio_source.Play();
    //    SceneManager.LoadScene(medGameSceneName);
    //}
    public void HardGame() {
        audio_source.clip = sound;
        audio_source.Play();
        settings.setDifficulty("hard");
        SceneManager.LoadScene("Game");
    }
    //public void ExtrGame() {
    //    audio_source.clip = sound;
    //    audio_source.Play();
    //    SceneManager.LoadScene(extrGameSceneName);
    //}
    //public void QuitGame() {
    //    Application.Quit();
    //}

    //public void QuesoButton()
    //{
    //    audio_source.clip = sound;
    //    audio_source.Play();
    //    SceneManager.LoadScene(quesoSceneName);
    //}
}
