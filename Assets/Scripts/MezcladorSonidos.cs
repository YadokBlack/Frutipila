using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MezcladorSonidos : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider volumenMusica; 
    public Slider volumenEfectos;

    public string musicaParametro = "Musica"; 
    public string efectoParametro = "Efectos"; 

    void Start()
    {
        if (volumenMusica != null)
        {
            volumenMusica.value = PlayerPrefs.GetFloat(musicaParametro, 0.75f); 
            volumenMusica.onValueChanged.AddListener(AsignarVolumenMusica);
            AsignarVolumenMusica(volumenMusica.value);
        }

        if (volumenEfectos != null)
        {
            volumenEfectos.value = PlayerPrefs.GetFloat(efectoParametro, 0.75f); 
            volumenEfectos.onValueChanged.AddListener(AsignarVolumenEfecto);
            AsignarVolumenEfecto(volumenEfectos.value);
        }
    }

    void AsignarVolumenMusica(float volume)
    {
        audioMixer.SetFloat(musicaParametro, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(musicaParametro, volume);
        PlayerPrefs.Save();
    }

    void AsignarVolumenEfecto(float volume)
    {
        audioMixer.SetFloat(efectoParametro, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(efectoParametro, volume);
        PlayerPrefs.Save();
    }
}
