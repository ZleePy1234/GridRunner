using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    #region Vars
    public float currentTime;
    public bool isPaused;

    public bool pauseTimer;
    public Canvas hudCanvas;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI songNameText;
    public TextMeshProUGUI songAuthorText;

    public List<MusicInfo> music = new();

    public Image uiCover;

    public Image ProgressBarFill;
    public AudioSource audioSource;

    private PlayerScript playerScript;
    #endregion

    #region Music Info Struct and Data
    public struct MusicInfo
    {
        public string name;
        public string author;
        public Sprite cover;
        public AudioClip song;

        public MusicInfo(string Name, string Author, Sprite Cover, AudioClip Song)
        {
            name = Name;
            author = Author;
            cover = Cover;
            song = Song;
        }
    }


    #endregion
    void Awake()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        currentTime = 0;
        pauseTimer = false;
        isPaused = false;
        AddMusic();
        PlayRandomSong();
    }

    void AddMusic()
    {
        MusicInfo BadEnding = new("Bad Ending", "Benny Smiles", Resources.Load<Sprite>("Covers/BadEnding"), Resources.Load<AudioClip>("Bad Ending"));
        MusicInfo Divide = new("Divide (Miami Edit)", "Magna", Resources.Load<Sprite>("Covers/Divide"), Resources.Load<AudioClip>("Divide (Miami Edit)"));
        MusicInfo Hydrogen = new("Hydrogen", "M.O.O.N.", Resources.Load<Sprite>("Covers/Moon"), Resources.Load<AudioClip>("Hydrogen"));
        MusicInfo LePerv = new("Le Perv", "Carpenter Brut", Resources.Load<Sprite>("Covers/LePerv"), Resources.Load<AudioClip>("Le Perv"));
        MusicInfo MiamiDisco = new("Miami Disco", "Perturbator", Resources.Load<Sprite>("Covers/Perturbator"), Resources.Load<AudioClip>("Miami Disco"));
        MusicInfo Narc = new("Narc", "Mega Drive", Resources.Load<Sprite>("Covers/Narc"), Resources.Load<AudioClip>("Narc"));
        MusicInfo NWH = new("New Wave Hookers", "Vestron Vulture", Resources.Load<Sprite>("Covers/NWH"), Resources.Load<AudioClip>("New Wave Hookers (2025 Remaster)"));
        MusicInfo Paris = new("Paris", "Moon", Resources.Load<Sprite>("Covers/Moon"), Resources.Load<AudioClip>("Paris"));
        MusicInfo RollerMobster = new("Roller Mobster", "Carpenter Brut", Resources.Load<Sprite>("Covers/RollerMobster"), Resources.Load<AudioClip>("Roller Mobster"));
        MusicInfo Sexualizer = new("Sexualizer", "Perturbator", Resources.Load<Sprite>("Covers/Perturbator"), Resources.Load<AudioClip>("Sexualizer"));
        MusicInfo Coals = new("She Swallowed Burning Coals", "El Tigr3", Resources.Load<Sprite>("Covers/Coals"), Resources.Load<AudioClip>("She Swallowed Burning Coals"));
        MusicInfo ToTheTop = new("To The Top", "Scattle", Resources.Load<Sprite>("Covers/ToTheTop"), Resources.Load<AudioClip>("To The Top"));
        music.Add(BadEnding);
        music.Add(Divide);
        music.Add(Hydrogen);
        music.Add(LePerv);
        music.Add(MiamiDisco);
        music.Add(Narc);
        music.Add(NWH);
        music.Add(Paris);
        music.Add(RollerMobster);
        music.Add(Sexualizer);
        music.Add(Coals);
        music.Add(ToTheTop);
    }
    void Update()
    {
        CheckAndPlayNextSong();
        UpdateProgressBar();
        if(playerScript.died == true)
        {
            pauseTimer = true;
            return;
        }
        Timer();
        TimerDisplay();
        
    }

    void Timer()
    {
        if(pauseTimer == true)
        {
            return;
        }
        currentTime += Time.deltaTime;
    }

    void TimerDisplay()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        timerText.text = timeSpan.ToString(@"m\.s\.ff");
    }

    public void PlayRandomSong()
    {
        int index = UnityEngine.Random.Range(0, music.Count);
        MusicInfo selected = music[index];

        songNameText.text = selected.name;
        songAuthorText.text = selected.author;
        uiCover.sprite = selected.cover;

        audioSource.clip = selected.song;
        audioSource.Play();
        Debug.Log("Playing song: " + selected.name + " by " + selected.author);
    }
    void UpdateProgressBar()
    {
        if (audioSource.clip.length > 0f)
        {
            ProgressBarFill.fillAmount = Mathf.Clamp01(audioSource.time / audioSource.clip.length);
        }
        else
        {
            ProgressBarFill.fillAmount = 0f;
        }
    }
    void CheckAndPlayNextSong()
    {
        if (!audioSource.isPlaying && audioSource.time >= audioSource.clip.length)
        {
            PlayRandomSong();
        }
    }

    
    
}
