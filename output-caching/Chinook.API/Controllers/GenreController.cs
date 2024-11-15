﻿using System.Net;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Chinook.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("CorsPolicy")]
public class GenreController : ControllerBase
{
    private readonly IChinookSupervisor _chinookSupervisor;
    private readonly ILogger<GenreController> _logger;
    private readonly IOutputCacheStore _cache;

    public GenreController(IChinookSupervisor chinookSupervisor, ILogger<GenreController> logger, IOutputCacheStore cache)
    {
        _chinookSupervisor = chinookSupervisor;
        _logger = logger;
        _cache = cache;
    }

    [HttpGet]
    [Produces("application/json")]
    [OutputCache(PolicyName = "GenrePolicy")]
    public async Task<ActionResult<List<GenreApiModel>>> Get()
    {
        try
        {
            var genres = await _chinookSupervisor.GetAllGenre();

            if (genres.Any())
            {
                return Ok(genres);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.NotFound, "No Genres Could Be Found");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside the GenreController Get action: {ex}");
            return StatusCode((int)HttpStatusCode.InternalServerError,
                "Error occurred while executing Get All Genres");
        }
    }

    [HttpGet("{id}", Name = "GetGenreById")]
    [Produces("application/json")]
    [OutputCache(Tags = new[] { "GenrePolicy_Tag" })]
    public async Task<ActionResult<GenreApiModel>> Get(int id)
    {
        try
        {
            var genre = await _chinookSupervisor.GetGenreById(id);

            if (genre != null)
            {
                return Ok(genre);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.NotFound, "Genre Not Found");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside the GenreController GetById action: {ex}");
            return StatusCode((int)HttpStatusCode.InternalServerError,
                "Error occurred while executing Get Genre By Id");
        }
    }

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<GenreApiModel>> Post([FromBody] GenreApiModel input)
    {
        try
        {
            if (input == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Given Genre is null");
            }
            else
            {
                return Ok(await _chinookSupervisor.AddGenre(input));
            }
        }
        catch (ValidationException ex)
        {
            _logger.LogError($"Something went wrong inside the GenreController Add Genre action: {ex}");
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Genres");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside the GenreController Add Genre action: {ex}");
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Genres");
        }
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<GenreApiModel>> Put(int id, [FromBody] GenreApiModel input)
    {
        try
        {
            if (input == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Given Genre is null");
            }
            else
            {
                return Ok(await _chinookSupervisor.UpdateGenre(input));
            }
        }
        catch (ValidationException ex)
        {
            _logger.LogError($"Something went wrong inside the GenreController Update Genre action: {ex}");
            return StatusCode((int)HttpStatusCode.InternalServerError,
                "Error occurred while executing Update Genres");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside the GenreController Update Genre action: {ex}");
            return StatusCode((int)HttpStatusCode.InternalServerError,
                "Error occurred while executing AUpdatedd Genres");
        }
    }

    [HttpDelete("{id}")]
    [OutputCache(PolicyName = "GenrePolicy")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _cache.EvictByTagAsync("GenrePolicy_Tag", new CancellationToken());
            return Ok(await _chinookSupervisor.DeleteGenre(id));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside the GenreController Delete action: {ex}");
            return StatusCode((int)HttpStatusCode.InternalServerError,
                "Error occurred while executing Delete Genre");
        }
    }
}