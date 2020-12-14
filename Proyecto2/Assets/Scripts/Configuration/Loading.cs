using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    void Start()
    {
        string level = LevelLoader.nextLevel;
        StartCoroutine(this.MakeTheLoad(level));
    }

    IEnumerator MakeTheLoad(string level)
    {
        
      yield return new WaitForSeconds(1f); 
      AsyncOperation operation= SceneManager.LoadSceneAsync(level);
      while (operation.isDone==false)
      {
          yield return null;
      }
    }
}
