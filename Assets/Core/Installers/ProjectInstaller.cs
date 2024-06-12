using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Generated;
using Core.Lib;
using Core.Services;
using Core.Services.UILoadingProgressService;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using Reflex.Scripts;
using Reflex.Scripts.Core;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class ProjectInstaller : Installer
{
    public Action OnInitComplete;

    [SerializeField] private ProjectDependencies _projectDependencies;
    [SerializeField] private SceneField _nextScene;
    private InitializableServices _initializableServices;

    public bool IsInitialized { get; private set; }

    public override void InstallBindings(Context context)
    {
        Application.targetFrameRate = -1;

        var projectDependencies = context.Instantiate(_projectDependencies);
        projectDependencies.BindInstances(context);

        _initializableServices = new InitializableServices();
        var world = new EcsWorld();
        var componentPools = new ComponentPools(world);

        // context.BindInstanceAs(_initializableServices.Add(new AbilitiesService()));
        context.BindInstanceAs(_initializableServices.Add(new ActionSystemsService()));
        context.BindInstanceAs(_initializableServices.Add(new DialogService()));
        context.BindInstanceAs(_initializableServices.Add(new GemService()));
        context.BindInstanceAs(_initializableServices.Add(new GraphicsSettingsService()));
        context.BindInstanceAs(_initializableServices.Add(new KeyGoldService()));
        context.BindInstanceAs(_initializableServices.Add(new KeySilverService()));
        context.BindInstanceAs(_initializableServices.Add(new PoolService()));
        context.BindInstanceAs(_initializableServices.Add(new SdkService()));
        context.BindInstanceAs(_initializableServices.Add(new TutorialsService()));
        context.BindInstanceAs(_initializableServices.Add(new UiInputsService()));
        context.BindInstanceAs(componentPools);
        context.BindInstanceAs(new BossHitPointBarService());
        context.BindInstanceAs(new GlobalStateService());
        context.BindInstanceAs(new DarkScreenService());
        context.BindInstanceAs(new PlayerInputs().Player);
        context.BindInstanceAs(new TimeScaleService());
        context.BindInstanceAs(new UILoadingProgressService());
        context.BindInstanceAs(new UiFocusableService());
        context.BindInstanceAs(world);

        _initializableServices.Initialize(context);

        StartCoroutine(AwaitLocalization(projectDependencies.transform, context));
    }

    private IEnumerator AwaitLocalization(Transform projectDependencies, Context context)
    {
        yield return LocalizationSettings.InitializationOperation;
        yield return LocalizationSettings.StringDatabase.GetAllTables();
        yield return null;
        EnableProjectDependencies(projectDependencies);

        yield return null;
        //Включение екс с задержкой что бы весь UI успел создать себе системы
        //П.С. Переделать на явный вызов инициализации UI !!!
        context.Resolve<EcsEngine>().SetupSystems();

        IsInitialized = true;
        OnInitComplete?.Invoke();

        if (!string.IsNullOrEmpty(_nextScene))
            SceneManager.LoadScene(_nextScene);
    }

    private void EnableProjectDependencies(Transform projectDependencies)
    {
        DontDestroyOnLoad(projectDependencies);
        projectDependencies.gameObject.SetActive(true);

#if UNITY_EDITOR
        int index = 0;
        foreach (var go in gameObject.scene.GetRootGameObjects().OrderBy(i => i.name))
            go.transform.SetSiblingIndex(index++);
#endif
    }

    private class InitializableServices
    {
        private readonly List<IInitializableService> _list = new(16);

        public T Add<T>(T service) where T : IInitializableService
        {
            _list.Add(service);
            return service;
        }

        public void Initialize(Context context)
        {
            foreach (var service in _list)
                service.InitializeService(context);
        }

        public void Dispose()
        {
            foreach (var service in _list)
                service.EditorRestoreDefaults();
        }
    }

#if UNITY_EDITOR
    private void OnDestroy() => _initializableServices.Dispose();
#endif
}