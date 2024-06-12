using System.Collections.Generic;
using Reflex;
using Action = System.Action;

namespace Core.Services
{
    public class DialogService : IInitializableService, ISerializableService
    {
        public event Action OnShow;
        public event Action OnComplete;

        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;

        public DialogData Dialog { get; private set; }

        public void InitializeService(Context context)
        {
            _playerDataService = context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);
        }

        public void RunDialog(DialogData dialog)
        {
            Dialog = dialog;
            OnShow?.Invoke();
        }

        public void DialogComplete() => OnComplete?.Invoke();

        public void SaveDialogAsSpoken(string uuid)
        {
            if (_serviceData.dialogKeys.Add(uuid))
                _playerDataService.SetDirty(this);
        }

        public bool DialogIsSpoken(string uuid) => _serviceData.dialogKeys.Contains(uuid);

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData() => _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

        private class ServiceData
        {
            public HashSet<string> dialogKeys = new();
        }
    }
}