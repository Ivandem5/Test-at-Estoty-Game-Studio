using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public void Restart()
    {        
        DOTween.KillAll(true);        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
