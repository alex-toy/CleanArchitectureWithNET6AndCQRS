﻿namespace Gatherly.Domain.Shared;

public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error) => _value = value;
    protected internal Result(bool isSuccess, Error error) : base(isSuccess, error) { }

    public TValue? Value => IsSuccess ? _value! : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);

    private static Result<TValue> Create(TValue? value)
    {
        return new Result<TValue>(value, false, Error.None);
    }
}
