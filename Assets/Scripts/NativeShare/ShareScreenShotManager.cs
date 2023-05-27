using UnityEngine;
using System.Collections;
using System.IO;

public class ShareScreenShotManager : MonoBehaviour
{
    public void ShareMaxScore()
    {
        StartCoroutine(TakeScreenshotAndShare("I have beaten my own record, try to beat me!\n\n"));
    }

    public void ShareScore()
    {
        StartCoroutine(TakeScreenshotAndShare("This is my record, what's yours?\n\n"));
    }

    private IEnumerator TakeScreenshotAndShare(string message)
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Im insane!").SetText(message).SetUrl("https://play.google.com/store/apps/details?id=com.GalanDev.Onethousandclicks")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }
}