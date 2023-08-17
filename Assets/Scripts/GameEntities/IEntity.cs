using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{

    public abstract void TakeHit(int dmgTaken);

    public abstract void Died();

     
}
