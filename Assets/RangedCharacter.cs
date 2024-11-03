using UnityEngine;

public abstract class RangedCharacter : Character //Ranged character-specific implementation
{
    //Ranged characters should not move while skill casting
    protected override void OnAnimationStart()
    {
        base.OnAnimationStart();
        //characterAttributes.DecreaseMovementSpeedByPercentage(1);
    }

    protected override void OnAnimationEnd()
    {
        base.OnAnimationEnd();
        //characterAttributes.IncreaseMovementSpeedByPercentage(1);
    }


}
