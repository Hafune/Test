#region Script Synopsis

//Base class for a SpreadPattern Controller script, mainly involved in creating emitters and triggering fire.
//Attached to the Controller gameobject to form the core behavior for the controller/emitters.
//Learn more about controllers/emitters at: http://neondagger.com/variabullet2d-quick-start-guide/#setting-emitters-controls

#endregion

using System.Collections.Generic;
using UnityEngine;

namespace Core.Lib
{
    public abstract class BasePattern : MonoBehaviour
    {
        public Emitter EmitterPrefab;

        [Tooltip("Disables realtime changes to emitter pattern characteristics.")]
        public bool FreezeEdits;

        [Range(0, 40)] public int EmitterAmount;

        [SerializeField] protected List<Emitter> Emitters;
        [SerializeField] protected List<Emitter> EmittersCached;

        [SerializeField] [Range(0.1f, 1f)] protected float PointScale = 0.2f;

        public PointDisplayType pointDisplay = PointDisplayType.Always;

        [Range(-180, 180)] [Tooltip("Sets rotation for all child emitters.")]
        public float Pitch;

        [Tooltip("Sets pitch as uni-directional or bi-directional rotation.")]
        public bool UniDirectionPitch;

        [Range(-360, 360)] [Tooltip("Sets the degrees of separation between child emitters.")]
        public float SpreadDegrees;

        [Range(-20, 20)] [Tooltip("Sets the central radius between child emitters")]
        public float SpreadRadius;

        public IReadOnlyList<Emitter> EmittersList => Emitters;

        public void Start()
        {
            LinkEmittersAtLaunch();
            SetIndicatorDisplay(true);
        }

        public virtual void LateUpdate()
        {
            SetEmitters();
            SetIndicatorDisplay(false);
        }

        private void SetEmitters()
        {
            if (Emitters != null && Emitters.Count == EmitterAmount)
                return;

            void AddNewEmitter()
            {
                var newEmitter = Instantiate(EmitterPrefab, transform, false);
                // newEmitter.hideFlags = HideFlags.DontSave;
                newEmitter.transform.localPosition = new Vector2(0, 0);
                Emitters.Add(newEmitter);
            }

            void ManageCache(bool isActive, Emitter emit, List<Emitter> addTo, List<Emitter> removeFrom)
            {
                emit.gameObject.SetActive(isActive);
                addTo.Add(emit);
                removeFrom.Remove(emit);
            }

            if (Emitters == null)
            {
                Emitters = new();
                EmittersCached = new();

                for (int i = 0; i < EmitterAmount; i++)
                    AddNewEmitter();

                return;
            }

            int difference;
            int totalEmitters = Emitters.Count + EmittersCached.Count;

            if (EmitterAmount < Emitters.Count)
            {
                difference = Emitters.Count - EmitterAmount;

                for (int i = 0; i < difference; i++)
                {
                    var lastEmitter = Emitters[^1];
                    ManageCache(false, lastEmitter, EmittersCached, Emitters);
                }
            }
            else if (EmitterAmount > Emitters.Count)
            {
                difference = EmitterAmount - totalEmitters;

                if (EmitterAmount > totalEmitters)
                {
                    foreach (Emitter emitterStored in
                             EmittersCached
                                 .ToArray()) //ToArray fix to ensure collection items aren't removed as it's being iterated over.
                        ManageCache(true, emitterStored, Emitters, EmittersCached);

                    for (int i = 0; i < difference; i++)
                        AddNewEmitter();
                }
                else
                {
                    difference = EmitterAmount - Emitters.Count;

                    for (int i = 0; i < difference; i++)
                    {
                        var lastEmitter = EmittersCached[^1];
                        ManageCache(true, lastEmitter, Emitters, EmittersCached);
                    }
                }
            }
        }

        private void LinkEmittersAtLaunch() //for re-establishing emitter list on project startup
        {
            if (Emitters != null)
                return;

            if (transform.childCount > 0)
            {
                Emitters = new();
                EmittersCached = new();

                foreach (Transform child in transform)
                {
                    if (child.parent == transform)
                    {
                        if (child.gameObject.activeSelf)
                            Emitters.Add(child.GetComponent<Emitter>());
                        else
                            EmittersCached.Add(child.GetComponent<Emitter>());
                    }
                }
            }
        }

        private void SetIndicatorDisplay(bool forceOnStart)
        {
            if (!Application.isPlaying || forceOnStart)
            {
                bool display = false;

                switch (pointDisplay)
                {
                    case PointDisplayType.Always:
                        display = true;
                        break;
                    case PointDisplayType.Never:
                        display = false;
                        break;
                    case PointDisplayType.EditorOnly:
                        display = !forceOnStart;
                        break;
                }

                foreach (Transform child in transform)
                {
                    Transform point = child.GetChild(0);
                    if (point.name == "Point")
                        point.GetComponent<SpriteRenderer>().enabled = display;
                }
            }
        }

        public void ClearEmitterCache()
        {
            foreach (var emitter in EmittersCached)
            {
                DestroyImmediate(emitter.gameObject);
            }

            EmittersCached = new();
        }

        public void ClearEmitters()
        {
            foreach (Transform emitter in transform)
                DestroyImmediate(emitter.gameObject);

            Emitters = new();
            EmittersCached = new();
            EmitterAmount = 0;
        }

        public void cloneFirstEmitter()
        {
            if (EmitterAmount <= 1)
            {
                Warn("Not enough Emitters to clone to (2 or more required).", this);
                return;
            }

            if (FreezeEdits)
            {
                Warn(
                    "Cannot clone while edits are frozen. Disable FreezeEdits temporarily in order to use clone procedure.",
                    this);
            }
        }

        public enum PatternSelect
        {
            Stack,
            Radial
        }

        public enum PointDisplayType
        {
            Always,
            Never,
            EditorOnly
        }

        private void Warn(string message, params object[] senderObjects)
        {
            string objects = "";

            if (senderObjects != null)
                foreach (object item in senderObjects)
                    objects += item + "; ";

            Debug.Log("WARNING: " + message + " | Object(s): " + objects);
        }
    }
}