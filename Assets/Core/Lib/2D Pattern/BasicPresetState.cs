#region Script Synopsis

//Simple state-based object that holds spreadpattern (controller) parameters and returns a new preset upon request
//Example: SpreadPattern.presetCheck()
//Learn more about presets at: https://neondagger.com/variabullet2d-in-depth-controller-guide/#presets

#endregion

namespace Core.Lib
{
    public class BasicPresetState
    {
        public readonly int emitterAmount;
        public readonly float spreadDegrees;
        public readonly float pitch;
        public readonly float spreadRadius;
        public readonly float spreadYAxis;
        public readonly float spreadXAxis;
        public readonly BasePattern.PatternSelect patternSelect;

        public BasicPresetState()
        {
        }

        private BasicPresetState(int emitterAmount, float spreadDegrees, float pitch, float spreadRadius,
            float spreadYAxis, float spreadXAxis, BasePattern.PatternSelect patternSelect)
        {
            this.emitterAmount = emitterAmount;
            this.spreadDegrees = spreadDegrees;
            this.pitch = pitch;
            this.spreadRadius = spreadRadius;
            this.spreadYAxis = spreadYAxis;
            this.spreadXAxis = spreadXAxis;
            this.patternSelect = patternSelect;
        }

        // @formatter:off
        public BasicPresetState RequestNewDefault(PresetName selection)
        {
            return selection switch
            {
                PresetName.reset => new BasicPresetState(1, 0, 0, 0, 0, 0, BasePattern.PatternSelect.Radial),
                PresetName.reAngle => new BasicPresetState(6, 64, 98, 3.5f, -0.55f, 1.25f, BasePattern.PatternSelect.Stack),
                PresetName.threeWay => new BasicPresetState(3, 13, -31, 1, 0, 0, BasePattern.PatternSelect.Radial),
                PresetName.verticalize => new BasicPresetState(1, 0, 0, 0, 0, 0, BasePattern.PatternSelect.Radial),
                PresetName.hellRadial => new BasicPresetState(9, 40, 0, 3.2f, 0, 0, BasePattern.PatternSelect.Radial),
                PresetName.tripleTriple => new BasicPresetState(9, 128, 0, 3.2f, 0, 0, BasePattern.PatternSelect.Radial),
                PresetName.vFormation => new BasicPresetState(5, 0, 0, 2.6f, 0.7f, -1, BasePattern.PatternSelect.Stack),
                PresetName.frontNBack => new BasicPresetState(3, 0, -170, 2.6f, 1.6f, -1, BasePattern.PatternSelect.Stack),
                PresetName.multiBomber => new BasicPresetState(3, 0, -54, 3.2f, 1.3f, 0, BasePattern.PatternSelect.Stack),
                PresetName.randomSpread => new BasicPresetState(15, 203, -40, 4.4f, 0.3f, 0, BasePattern.PatternSelect.Stack),
                PresetName.pentaWall => new BasicPresetState(10, 140, -100, 5, 0, 0, BasePattern.PatternSelect.Radial),
                _ => null
            };
        }
        // @formatter:on
    }

    public enum PresetName
    {
        none,
        reset,
        reAngle,
        threeWay,
        verticalize,
        hellRadial,
        tripleTriple,
        vFormation,
        frontNBack,
        multiBomber,
        randomSpread,
        pentaWall
    }
}