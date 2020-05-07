using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene("Level 2");
    }
}
