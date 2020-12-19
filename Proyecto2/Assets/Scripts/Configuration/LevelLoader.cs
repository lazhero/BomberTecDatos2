
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoader
{
   public static string nextLevel;
   /// <summary>
   /// Load a level
   /// </summary>
   /// <param name="name"></param>
   public static void LoadLevel(string name)
   {
      nextLevel = name;

      SceneManager.LoadScene("Loading");
   }
}
