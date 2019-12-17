using System.Collections;
using System.Collections.Generic;
using Aura2API;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class LastFloorBreaker : MonoBehaviour
{
    Animator anim;
    Animator canvasAnim;
    AudioSource audio;
    Image canvas;
    public GameObject[] linkedCrystals;
    public GameObject brokenFloor;
    public GameObject floor;

    public AudioMixerSnapshot silence;

    private GameObject player;


    void Start()
    {
        anim = GameObject.Find("UICanvas").GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        canvas = GameObject.Find("WhiteCanvas").GetComponent<Image>();
        canvasAnim = canvas.GetComponent<Animator>();
        player = GameObject.Find("Samurai");
    }

    // Update is called once per frame
    void Update()
    {
        if (floor && CheckCrystals())
        {
            BreakFloor();
        }
    }

    bool CheckCrystals()
    {
        foreach(GameObject crystal in linkedCrystals)
        {
            CrystalSwitch c = crystal.GetComponent<CrystalSwitch>();
            if (!c._active)
            {
                return false;
            }
        }
        return true;
    }

    void BreakFloor()
    {
        Instantiate(brokenFloor, floor.transform.position + Vector3.down, Quaternion.identity);
        audio.Play();
        PlayerBehavior.S.controllerScript.editingMirror = false;
        PlayerBehavior.S.sword.SetActive(true);
        anim.SetBool("isEditingObjectOnPedestal", false);
        Destroy(floor);
        silence.TransitionTo(10f);
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        canvasAnim.SetBool("WhiteFade", true);
        yield return new WaitUntil(() => canvas.color.a == 1);
        player.GetComponent<FirstPersonController>().public_MouseLook.SetCursorLock(false);
        SceneManager.LoadScene("menu");
    }

}
