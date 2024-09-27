using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundManager
{
    void PlaySound(string name);
    void PlayMusic(string name);
    void StopMusic();
}
