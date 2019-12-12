using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class menu : MonoBehaviour
{
    AudioSource click;
    public TextMeshProUGUI textmeshPro;

    // Start is called before the first frame update
    void Start()
    {
        click = GetComponent<AudioSource>();
        //textmeshPro = GameObject.Find("play").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void exit()
    {
        textmeshPro.faceColor = new Color32(212, 6, 6, 255);
        click.Play();
        StartCoroutine(exiting());
    }
    public void enter()
    {
        textmeshPro.faceColor = new Color32(212, 6, 6, 255);
        click.Play();
        StartCoroutine(entering());
    }

    IEnumerator entering()
    {
        yield return new WaitForSeconds(0.2f);
        textmeshPro.faceColor = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("sampleScene");
    }
    IEnumerator exiting()
    {
        yield return new WaitForSeconds(0.2f);
        textmeshPro.faceColor = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}

