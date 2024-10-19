using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMainMenu : MonoBehaviour
{
    public void MoveGame1() {
        SceneManager.LoadScene("Game1");
    }

    public void MoveGame2() {
        SceneManager.LoadScene("Game2");
    }
}
