using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoleAdvisor.API.Models.Recommendations;
using RoleAdvisor.Application.Interfaces;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class RecommendationsController : ControllerBase
{
    private readonly IRecommendationService _recommendationService;
    private readonly IMapper _mapper;

    public RecommendationsController(IRecommendationService recommendationService, IMapper mapper)
    {
        _recommendationService = recommendationService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetRecommendations([FromQuery] int projectId)
    {
        var project = await _recommendationService.SelectBestEmployees(projectId);

        return Ok(_mapper.Map<Project, ProjectRecommendationsModel>(project));
    }
}
