using System;
using System.Collections.Generic;

namespace PastryFlow.Application.Common;

public class BusinessException : Exception
{
    public List<string> Errors { get; }

    public BusinessException(string message) : base(message)
    {
        Errors = new List<string>();
    }

    public BusinessException(string message, List<string> errors) : base(message)
    {
        Errors = errors ?? new List<string>();
    }
}
