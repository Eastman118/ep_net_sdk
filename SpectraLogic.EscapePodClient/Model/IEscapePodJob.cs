﻿namespace SpectraLogic.EscapePodClient.Model
{
    public interface IEscapePodJob
    {
        string Id { get; }
        Status Status { get; }
    }
}
