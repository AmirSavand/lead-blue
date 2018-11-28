using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject soundOff;
    public GameObject musicOff;

    void Start()
    {
        UpdateOptions(false);
    }

    void UpdateOptions(bool commit = true)
    {
        soundOff.SetActive(!Storage.EnableSound);
        musicOff.SetActive(!Storage.EnableMusic);

        audioMixer.SetFloat("volGame", Storage.EnableSound ? 0 : -80);
        audioMixer.SetFloat("volMusic", Storage.EnableMusic ? 0 : -80);

        if (commit)
        {
            Storage.Save();
        }
    }

    public void ToggleSound()
    {
        Storage.EnableSound = !Storage.EnableSound;
        UpdateOptions();
    }

    public void ToggleMusic()
    {
        Storage.EnableMusic = !Storage.EnableMusic;
        UpdateOptions();
    }
}
