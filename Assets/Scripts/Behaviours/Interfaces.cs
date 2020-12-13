using System;
using System.Collections.Generic;
using UnityEngine;

//Input Interfaces
public interface IHasLookInput {

    void Look(Vector2 velocity);
    }

public interface IHasTwoOptionsUI {

    void LeftEvent();
    void RightEvent();
    }

public interface IHasOpenAndClose {

    void Open();
    void Close();
    }