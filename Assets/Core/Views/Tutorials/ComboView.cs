using Core.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Views.MainMenu
{
    public class ComboView : AbstractUIDocumentView
    {
        [SerializeField] private MonoBehaviour _comboContainer;
        private TaskTutorialAwaitPlayerCombo[] _taskCombo;

        private VisualElement _success;
        private ComboVT _root;
        private int openedCombos;

        protected override void Awake()
        {
            base.Awake();
            _root = new ComboVT(RootVisualElement);
            _taskCombo = _comboContainer.GetComponentsInChildren<TaskTutorialAwaitPlayerCombo>();

            var combos = RootVisualElement.Query(_root.combo.name).ToList();
            var successes = RootVisualElement.Query(_root.success.name).ToList();

            combos[openedCombos].AddToClassList(ComboVT.s_combo_visible);

            for (int i = 0, totalCombos = _taskCombo.Length; i < totalCombos; i++)
            {
                _taskCombo[i].OnComboStepChange += (step, totalSteps) =>
                {
                    if (step != totalSteps)
                        return;

                    successes[openedCombos++].AddToClassList(ComboVT.s_success);

                    if (openedCombos >= totalCombos)
                        return;

                    combos[openedCombos].AddToClassList(ComboVT.s_combo_visible);
                };
            }

            DisplayFlex();
        }
    }
}