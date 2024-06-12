using System;
using System.Collections;
using System.Collections.Generic;
using Core.EcsCommon.ValueComponents;
using JetBrains.Annotations;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Core.Services
{
    public class LocalizationService : MonoConstruct, ISerializableService
    {
        public enum MyLocales
        {
            ru,
            en,
            be,
            fr,
            de,
            id,
            it,
            pl,
            pt,
            es,
            tr,
            vi
        }

        public Action OnChange;

        [SerializeField] private List<Locale> _locales;

        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;
        private SdkService _sdkService;
        private Context _context;
        [CanBeNull] private Coroutine _coroutine;

        public string LocaleIdentifierCode => _serviceData.localeIdentifierCode;

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _serviceData.localeIdentifierCode = LocalizationSettings.SelectedLocale.Identifier.Code;
            _sdkService = _context.Resolve<SdkService>();
            _playerDataService = _context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this, true);
        }

        public void SelectNext()
        {
            if (_coroutine is not null)
                return;

            var locale = _locales.CircularNext(LocalizationSettings.SelectedLocale);
            _coroutine = StartCoroutine(ChangeLocale(locale));
        }

        public void SelectPrevious()
        {
            if (_coroutine is not null)
                return;

            var locale = _locales.CircularNext(LocalizationSettings.SelectedLocale);
            _coroutine = StartCoroutine(ChangeLocale(locale));
        }

        public void ChangeLocale(MyLocales locale)
        {
            if (_coroutine is not null)
                return;

            _coroutine = StartCoroutine(ChangeLocale(Locale.CreateLocale(MyEnumUtility<MyLocales>.Name((int)locale))));
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData()
        {
            _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

            Locale locale;
            if (!string.IsNullOrEmpty(_serviceData.localeIdentifierCode))
            {
                locale = Locale.CreateLocale(_serviceData.localeIdentifierCode);
            }
            else
            {
                var localeCode = _sdkService.GetLocale();
                locale = Locale.CreateLocale(localeCode == "ru" ? localeCode : "en");
            }

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeLocale(locale));
        }

        //https://forum.unity.com/threads/nullreferenceexception-the-table-xyz_en-does-not-have-a-sharedtabledata.1512374/
        //Проблема нул рефов при загрузке новых локалей, правим LocalizedDatabase.cs в общей либе
        private IEnumerator ChangeLocale(Locale locale)
        {
            yield return LocalizationSettings.InitializationOperation;
            yield return LocalizationSettings.StringDatabase.GetAllTables();
            LocalizationSettings.SelectedLocale = locale;
            yield return LocalizationSettings.InitializationOperation;
            yield return LocalizationSettings.StringDatabase.GetAllTables();
            _coroutine = null;
            
            if (_serviceData.localeIdentifierCode != LocalizationSettings.SelectedLocale.Identifier.Code)
            {
                _serviceData.localeIdentifierCode = LocalizationSettings.SelectedLocale.Identifier.Code;
                _playerDataService.SetDirty(this);
            }
            
            OnChange?.Invoke();
        }

        private class ServiceData
        {
            public string localeIdentifierCode;
        }
    }
}