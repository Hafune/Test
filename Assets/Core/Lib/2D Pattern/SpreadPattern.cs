#region Script Synopsis

//Extended class for a SpreadPattern Controller script, mainly involved in setting up controller emitter positions
//Attached to the Controller gameobject to form the core behavior for the controller/emitters.
//Learn more about controllers/emitters at: http://neondagger.com/variabullet2d-quick-start-guide/#setting-emitters-controls

#endregion

using System;
using UnityEngine;

namespace Core.Lib
{
    [ExecuteInEditMode]
    public class SpreadPattern : BasePattern
    {
        [Range(-80, 80)] [Tooltip("Shifts emitters on Y-axis.")]
        public float SpreadYAxis;

        [Range(-80, 80)] [Tooltip("Shifts emitters on X-axis.")]
        public float SpreadXAxis;

        [Tooltip("Sets base algorithm for creating emitter placement.")]
        [SerializeField] private PatternSelect patternSelect = PatternSelect.Radial;

        [SerializeField] private PresetName Preset;

        public override void LateUpdate()
        {
            if (FreezeEdits) 
                return;
            
            PresetCheck();
            base.LateUpdate();

            SetAllPositions();
        }

        private void SetPositionsStack()
        {
            int mod = 0;

            if (Emitters.Count % 2 == 0)
                mod = Emitters.Count / 2;
            else
                mod = Emitters.Count / 2 + 1;

            int modAngle = mod;

            for (int i = 0; i < Emitters.Count; i++)
            {
                if (Emitters.Count % 2 == 0)
                {
                    if (i < Emitters.Count / 2)
                    {
                        modAngle = (Math.Abs(modAngle) - 1) * -1;
                        mod--;
                    }
                    else if (i > Emitters.Count / 2)
                    {
                        modAngle += 1;
                        mod++;
                    }
                }
                else
                {
                    if (i <= Emitters.Count / 2)
                    {
                        modAngle = (Math.Abs(modAngle) - 1) * -1;
                        mod--;
                    }
                    else
                    {
                        modAngle += 1;
                        mod++;
                    }
                }

                float oy = i - (Emitters.Count - 1) / 2f;
                float xOffset = SpreadXAxis * mod;

                Emitters[i].transform.localPosition = new Vector2(xOffset, SpreadYAxis * oy);
                Emitters[i].transform.localRotation =
                    new Quaternion(); //fixes issue of creating rotation issues on play
                Emitters[i].transform.localRotation = Quaternion.Euler(0, 0, SpreadDegrees * modAngle);
                Emitters[i].transform.localScale =
                    new Vector3(1, 1, 1); //for maintaining ratio if using nested emitters/points

                foreach (Transform child in Emitters[i].transform)
                {
                    float tilt = 0;

                    if (i < Emitters.Count / 2 || Emitters.Count == 1 || UniDirectionPitch)
                        tilt = Pitch;
                    else if (i >= (float)Emitters.Count / 2)
                        tilt = (Pitch) * -1;

                    child.localPosition = new Vector2(SpreadRadius, 0);
                    child.localRotation = Quaternion.Euler(0, 0, tilt);
                    child.localScale = new Vector3(PointScale, PointScale, 1);
                }
            }
        }

        private void SetPositionsRadial()
        {
            float oy = SpreadDegrees * (Emitters.Count + 1) / 2f;
            int mod = 0;
            for (int i = 0; i < Emitters.Count; i++)
            {
                mod++;

                Emitters[i].transform.localPosition = new Vector2(SpreadXAxis, SpreadYAxis);
                Emitters[i].transform.localRotation = Quaternion.Euler(0, 0, SpreadDegrees * mod - oy);
                Emitters[i].transform.localScale = new Vector3(1, 1, 1);

                foreach (Transform child in Emitters[i].transform)
                {
                    float tilt = 0;

                    if (i < Emitters.Count / 2 || Emitters.Count == 1 || UniDirectionPitch)
                        tilt = Pitch;
                    else if (i >= (float)Emitters.Count / 2)
                        tilt = Pitch * -1;

                    child.localPosition = new Vector2(SpreadRadius, 0);
                    child.localRotation = Quaternion.Euler(0, 0, tilt);
                    child.localScale = new Vector3(PointScale, PointScale, 1);
                }
            }
        }

        private void SetAllPositions()
        {
            if (Emitters != null)
            {
                if (patternSelect == PatternSelect.Stack)
                {
                    SetPositionsStack();
                }
                else
                {
                    SetPositionsRadial();
                }
            }
        }

        private void PresetCheck()
        {
            if (Preset != PresetName.none) //load from preset if new state requested
            {
                BasicPresetState preset = new BasicPresetState();
                preset = preset.RequestNewDefault(Preset);

                if (preset != null)
                {
                    EmitterAmount = preset.emitterAmount;
                    SpreadDegrees = preset.spreadDegrees;
                    Pitch = preset.pitch;
                    SpreadRadius = preset.spreadRadius;
                    SpreadYAxis = preset.spreadYAxis;
                    SpreadXAxis = preset.spreadXAxis;
                    patternSelect = preset.patternSelect;
                }

                Preset = PresetName.none;
            }
        }
    }
}