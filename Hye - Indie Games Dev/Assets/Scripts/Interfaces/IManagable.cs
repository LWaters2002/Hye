using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManagable
{
    GameManager gameManager {get;}

    void Setup(GameManager gameManager);
}

public interface IUIManagable
{
    UIManager UImanager {get;}

    void Setup(UIManager UImanager);
}