using UnityEngine;

public class ApplicationCalls : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}