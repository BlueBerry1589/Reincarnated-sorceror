/*
 * ChangeScene.cs
 *
 * Utilisé par le bouton Jouer du menu principal pour changer de scène.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
