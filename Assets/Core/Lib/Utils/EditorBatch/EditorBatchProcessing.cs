#if UNITY_EDITOR
using System;
using System.Collections;
using Core.Lib.Utils;
using Lib;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorBatchProcessing : MonoBehaviour
{
    [SerializeField] private bool _refresh;
    [SerializeField] private GameObject[] _prefabs;
    private int _prefabIndex;
    private int _processorIndex;
    private EditorBatchFilter[] _processors;

    private void OnValidate()
    {
        if (!_refresh)
            return;

        _refresh = false;

        EditorApplication.delayCall += OpenPrefab;
    }

    private void OpenPrefab()
    {
        _prefabIndex = -1;
        NextPrefab();
    }

    private void NextPrefab()
    {
        _prefabIndex++;

        if (_prefabIndex >= _prefabs.Length)
        {
            Debug.Log("Процессоры завершены");
            return;
        }

        ProcessorStart();
    }

    private void ProcessorStart()
    {
        _processorIndex = 0;
        PrefabStageUtility.OpenPrefab(PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(_prefabs[_prefabIndex]));
        EditorFunctions.SetAutoSave(false);
        _processors = transform.GetSelfChildrenComponents<EditorBatchFilter>();
        ProcessorNext();
    }

    private void ProcessorNext()
    {
        if (_processorIndex >= _processors.Length)
        {
            Completed();
            return;
        }

        var processor = _processors[_processorIndex++];

        if (!processor.gameObject.activeSelf)
        {
            ProcessorNext();
            return;
        }

        processor.RunProcessing(ProcessorNext, () =>
        {
            StageUtility.GoBackToPreviousStage();
            Debug.Log("Processors break");
        });
    }

    private void Completed()
    {
        EditorFunctions.SetAutoSave(true);
        EditorCoroutineUtility.StartCoroutine(CompletedPrivate(), gameObject);
    }

    private IEnumerator CompletedPrivate()
    {
        yield return null;

        bool hasError = true;
        float wait = 10;

        while (hasError && wait > 0)
        {
            wait -= Time.deltaTime;

            try
            {
                StageUtility.GoBackToPreviousStage();
                hasError = false;
            }
            catch (Exception)
            {
                hasError = true;
            }

            yield return null;
        }

        if (wait <= 0)
            throw new Exception("StageUtility.GoBackToPreviousStage(); ошибка, закончилось время попыток");

        yield return null;

        NextPrefab();
    }
}

#endif