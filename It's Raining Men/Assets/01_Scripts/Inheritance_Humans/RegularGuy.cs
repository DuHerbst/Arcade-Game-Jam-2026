using UnityEngine;

public class RegularGuy : HumanBase

{
    
    protected override void Start()
    {
        base.Start();
        StartFalling();
    }
    
}
