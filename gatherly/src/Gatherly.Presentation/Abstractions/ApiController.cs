﻿using Gatherly.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }

    protected IActionResult HandleFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult => BadRequest(CreateProblemDetails("Validation Error", StatusCodes.Status400BadRequest, result.Error, validationResult.Errors)),
            _ => BadRequest(CreateProblemDetails("Bad Request", StatusCodes.Status400BadRequest, result.Error))
        };
    }

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null)
    {
        return new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
    }
}