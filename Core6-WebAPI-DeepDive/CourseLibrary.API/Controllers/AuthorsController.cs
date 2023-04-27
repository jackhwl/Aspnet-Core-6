﻿
using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace CourseLibrary.API.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly ICourseLibraryRepository _courseLibraryRepository;
    private readonly IMapper _mapper;
    private readonly IPropertyMappingService _propertyMappingService;
    private readonly IPropertyCheckerService _propertyCheckerService;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public AuthorsController(
        ICourseLibraryRepository courseLibraryRepository,
        IMapper mapper, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService, ProblemDetailsFactory problemDetailsFactory)
    {
        _courseLibraryRepository = courseLibraryRepository ??
            throw new ArgumentNullException(nameof(courseLibraryRepository));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
        _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        _propertyCheckerService= propertyCheckerService ?? throw new ArgumentNullException(nameof(propertyCheckerService));
        _problemDetailsFactory= problemDetailsFactory ?? throw new ArgumentNullException(nameof(problemDetailsFactory));
    }

    [HttpGet(Name = "GetAuthors")]
    [HttpHead]
    public async Task<IActionResult> GetAuthors([FromQuery] AuthorsResourceParameters authorsResourceParameters)
    { 
        if (!_propertyMappingService.ValidMappingExistsFor<AuthorDto, Author>(authorsResourceParameters.OrderBy))
        {
            return BadRequest();
        }

        if (!_propertyCheckerService.TypeHasProperties<AuthorDto>(authorsResourceParameters.Fields))
        {
            return BadRequest(_problemDetailsFactory.CreateProblemDetails(HttpContext, 
                statusCode: 400, 
                detail: $"Not all requested data shaping fields exist on " + $"the resource: {authorsResourceParameters.Fields}")
                );
        }

        var authorsFromRepo = await _courseLibraryRepository
            .GetAuthorsAsync(authorsResourceParameters); 

        var paginationMetadata = new
        {
            totalCount = authorsFromRepo.TotalCount,
            pageSize = authorsFromRepo.PageSize,
            currentPage = authorsFromRepo.CurrentPage,
            totalPages = authorsFromRepo.TotalPages
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        // create links
        var links = CreateLinksForAuthors(authorsResourceParameters, authorsFromRepo.HasNext, authorsFromRepo.HasPrevious);
        var shapedAuthors = _mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo).ShapeData(authorsResourceParameters.Fields);
        var shapedAuthorsWithLinks = shapedAuthors.Select(author =>
        {
            var authorAsDictionary = author as IDictionary<string, object?>;
            var authorLinks = CreateLinksForAuthor((Guid)authorAsDictionary["Id"]!, null);
            authorAsDictionary.Add("links", authorLinks);
            return authorAsDictionary;
        });

        var linkedCollectionResource = new
        {
            value = shapedAuthorsWithLinks,
            links = links
        };

        return Ok(linkedCollectionResource);
    }

    private string? CreateAuthorsResourceUri(AuthorsResourceParameters authorsResourceParameters, ResourceUriType type)
    {
        switch (type)
        {
            case ResourceUriType.PreviousPage:
                return Url.Link("GetAuthors", new
                {
                    fields = authorsResourceParameters.Fields,
                    orderBy = authorsResourceParameters.OrderBy,
                    pageNumber = authorsResourceParameters.PageNumber - 1,
                    pageSize = authorsResourceParameters.PageSize,
                    mainCategory = authorsResourceParameters.MainCategory,
                    searchQuery = authorsResourceParameters.SearchQuery
                });
            case ResourceUriType.NextPage:
                return Url.Link("GetAuthors", new
                {
                    fields = authorsResourceParameters.Fields,
                    orderBy = authorsResourceParameters.OrderBy,
                    pageNumber = authorsResourceParameters.PageNumber + 1,
                    pageSize = authorsResourceParameters.PageSize,
                    mainCategory = authorsResourceParameters.MainCategory,
                    searchQuery = authorsResourceParameters.SearchQuery
                });
            case ResourceUriType.Current:
            default:
                return Url.Link("GetAuthors", new
                {
                    fields = authorsResourceParameters.Fields,
                    orderBy = authorsResourceParameters.OrderBy,
                    pageNumber = authorsResourceParameters.PageNumber,
                    pageSize = authorsResourceParameters.PageSize,
                    mainCategory = authorsResourceParameters.MainCategory,
                    searchQuery = authorsResourceParameters.SearchQuery
                });
        }
    }

    [HttpGet("{authorId}", Name = "GetAuthor")]
    public async Task<IActionResult> GetAuthor(Guid authorId, string? fields, [FromHeader(Name = "Accept")] string? mediaType)
    {
        // check if the inputted media type is a valid media type
        if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
        {
            return BadRequest(_problemDetailsFactory.CreateProblemDetails(HttpContext, statusCode: 400, detail: $"Accept header media type value is not a valid media type."));
        }

        if (!_propertyCheckerService.TypeHasProperties<AuthorDto>(fields))
        {
            return BadRequest(_problemDetailsFactory.CreateProblemDetails(HttpContext,
                statusCode: 400,
                detail: $"Not all requested data shaping fields exist on " + $"the resource: {fields}")
                );
        }

        // get author from repo
        var authorFromRepo = await _courseLibraryRepository.GetAuthorAsync(authorId);

        if (authorFromRepo == null)
        {
            return NotFound();
        }

        if (parsedMediaType.MediaType == "application/vnd.marvin.hateoas+json")
        {
            // create links
            var links = CreateLinksForAuthor(authorId, fields);

            var linkedResourceToReturn = _mapper.Map<AuthorDto>(authorFromRepo).ShapeData(fields) as IDictionary<string, object?>;

            linkedResourceToReturn.Add("links", links);

            return Ok(linkedResourceToReturn);
        }

        return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
    }

    private IEnumerable<LinkDto> CreateLinksForAuthors(AuthorsResourceParameters authorsResourceParameters, bool hasNext, bool hasPrevious)
    {
        var links = new List<LinkDto>();

        links.Add(new(CreateAuthorsResourceUri(authorsResourceParameters, ResourceUriType.Current), "self", "GET"));

        if (hasNext) 
            links.Add(new(CreateAuthorsResourceUri(authorsResourceParameters, ResourceUriType.NextPage), "nextPage", "GET"));

        if (hasPrevious)
            links.Add(new(CreateAuthorsResourceUri(authorsResourceParameters, ResourceUriType.PreviousPage), "previousPage", "GET"));

        return links;
    }

    private IEnumerable<LinkDto> CreateLinksForAuthor(Guid authorId, string? fields)
    {
        var links = new List<LinkDto>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            links.Add(new(Url.Link("GetAuthor", new { authorId}), "self", "GET"));
        }
        else
        {
            links.Add(new(Url.Link("GetAuthor", new { authorId, fields}), "self", "GET"));
        }

        links.Add(new(Url.Link("CreateCourseForAuthor", new { authorId }), "create_course_for_author", "POST"));
        links.Add(new(Url.Link("GetCoursesForAuthor", new { authorId }), "courses", "GET"));

        return links;
    }

    [HttpPost(Name = nameof(CreateAuthor))]
    public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorForCreationDto author)
    {
        var authorEntity = _mapper.Map<Entities.Author>(author);

        _courseLibraryRepository.AddAuthor(authorEntity);
        await _courseLibraryRepository.SaveAsync();

        var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);

        // create links
        var links = CreateLinksForAuthor(authorToReturn.Id, null);

        var linkedResourceToReturn = authorToReturn.ShapeData(null) as IDictionary<string, object?>;
        linkedResourceToReturn.Add("links", links);

        return CreatedAtRoute("GetAuthor",
            new { authorId = linkedResourceToReturn["Id"] },
            linkedResourceToReturn);
    }

    [HttpOptions]
    public IActionResult GetAuthorsOptions()
    {
        Response.Headers.Add("Allow", "GET,HEAD,POST,OPTIONS");
        return Ok();
    }
}
