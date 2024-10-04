using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void GoGameScene(int input)
    {
        SceneManager.LoadScene(input);
    }
}
