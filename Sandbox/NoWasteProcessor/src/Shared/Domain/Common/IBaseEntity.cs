﻿namespace Domain.Common;

public interface IBaseEntity<T>
{
    T Id { get; set; }
}
