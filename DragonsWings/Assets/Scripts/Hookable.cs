﻿using UnityEngine;

public interface Hookable
{
    Weight Weight { get; }

    void OnHookHit();
}