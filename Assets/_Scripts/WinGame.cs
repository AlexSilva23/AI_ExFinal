using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject PlayerCam;
    public GameObject WinCamera;
    public Transform player;
    public Vector3 NewPos;
    public Quaternion NewRot;
    public ParticleSystem fireworks;
    public GameObject canvas;
    public GameObject Crown;

    bool enableR;
    private void Update()
    {
        if (enabled)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }



    }


    private void OnCollisionEnter(Collision collision)
    {
        WinGameFunction();
    }

    void WinGameFunction()
    {
        PlayerCam.SetActive(false);
        WinCamera.SetActive(true);
        player.position = NewPos;
        player.rotation = NewRot;

        player.GetComponentInChildren<PlayerMovement>().enabled = false;
        fireworks.Play();
        canvas.SetActive(false);
        enableR = true;
        Crown.SetActive(true);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
