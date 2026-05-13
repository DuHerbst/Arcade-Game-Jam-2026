using UnityEngine;

public class ObjComputer : ObjectsBase
{
    protected override void Start()
    {
        base.Start();
        RigidbodyObject.gravityScale = gravityScale;
        
    }
    
}
