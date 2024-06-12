#if UNITY_EDITOR
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

class MyPreprocessBuild : MonoBehaviour, IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        // report.
        // Debug.Log("public void OnPreprocessBuild(BuildReport report)");
        // EditorCoroutineUtility.StartCoroutine(Test(), gameObject);
    }

    private IEnumerator Test()
    {
        int second = 5;
        while (second-- > 0)
        {
            yield return new WaitForSeconds(1);
        }
    }
}
#endif