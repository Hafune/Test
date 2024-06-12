namespace Core.Components
{
    public struct LStickTag : IButtonComponent
    {
    }
    
    public struct ButtonHorizontalTag : IButtonComponent
    {
    }
    
    public struct ButtonLeft : IButtonComponent
    {
    }

    public struct ButtonRight : IButtonComponent
    {
    }

    public struct ButtonUp : IButtonComponent
    {
    }

    public struct ButtonDown : IButtonComponent
    {
    }

    public struct ButtonLightAttackTag : IButtonComponent
    {
    }

    public struct ButtonStrongAttackTag : IButtonComponent
    {
    }

    public struct ButtonUseHealing : IButtonComponent
    {
    }

    public struct ButtonDashTag : IButtonComponent
    {
    }

    public struct ButtonJumpTag : IButtonComponent
    {
    }

    public struct EventButtonPerformed<T> where T : struct, IButtonComponent
    {
    }

    public struct EventButtonCanceled<T> where T : struct, IButtonComponent
    {
    }

    public interface IButtonComponent
    {
    }
}