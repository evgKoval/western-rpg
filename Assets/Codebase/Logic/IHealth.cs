﻿using System;

namespace Codebase.Logic
{
  public interface IHealth
  {
    event Action Changed;
    int Current { get; }
    int Max { get; }
    void TakeDamage(int damage);
  }
}