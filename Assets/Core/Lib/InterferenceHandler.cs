using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Lib
{
    public class InterferenceHandler : MonoBehaviour, ITriggerDispatcherTarget2D
    {
        //Тело может менять материал в системах поэтому нельзя влиять на материал здесь
        //выключение тела как вариант позволяющий не трогать то что может меняться
        [SerializeField] private Collider2D _interferenceCollision;
        [SerializeField] private Collider2D _bodyCollision;

        private void Awake()
        {
            Assert.IsNotNull(_interferenceCollision);
            Assert.IsNotNull(_bodyCollision);
        }

        private void Start() => _interferenceCollision.gameObject.SetActive(false);

        public void OnTriggerEnter2D(Collider2D col)
        {
            _interferenceCollision.gameObject.SetActive(true);
            _bodyCollision.gameObject.SetActive(false);
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            _interferenceCollision.gameObject.SetActive(false);
            _bodyCollision.gameObject.SetActive(true);
        }
    }
}