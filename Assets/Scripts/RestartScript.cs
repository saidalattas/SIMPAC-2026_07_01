using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RestartFunction()
    {
        SceneManager.LoadScene("Welcome Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
