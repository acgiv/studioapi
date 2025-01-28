using System.Linq.Expressions;
using API.Data;
using API.Model.Domain;
using API.Model.DTO;
using API.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // creare variabili instaziate nell' injection
        private readonly ApiDbContext dbContext;
        private readonly RegionRepository rigionRepository;

        // richimare i metodi nel parametro del contruttore e poi assegnarli come valore
        public RegionsController(ApiDbContext dbContext, RegionRepository rigionRepository)
        {
            this.dbContext = dbContext;
            this.rigionRepository = rigionRepository;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll() {

            var regionsDomain = await rigionRepository.GetAllAsync();

            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain) {

                regionsDto.Add(new RegionDto() {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("GetByiDRegion{id:Guid}")]
        public async Task<IActionResult> GetByiDRegion([FromRoute] Guid id)
        {
           

            var regionsDomain =  await rigionRepository.GetbyIdAsync(id);
            if (regionsDomain == null) {
                return NotFound();
            }

            var regionsDto = new RegionDto{
                Id =regionsDomain.Id,
                Code = regionsDomain!.Code,
                Name = regionsDomain!.Name,
                RegionImageUrl = regionsDomain!.RegionImageUrl
            }; 

            return Ok(regionsDto);
        }


        [HttpPost]
        [Route("insertregion")]
        public async  Task<IActionResult> InsertRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            regionDomainModel= await rigionRepository.insertRegionAsync(regionDomainModel);

            var regionsDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel!.Code,
                Name = regionDomainModel!.Name,
                RegionImageUrl = regionDomainModel!.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetByiDRegion), new { id = regionDomainModel.Id }, regionDomainModel);
        }

        [HttpPut]
        [Route("putregion{id:Guid}")]
        public async Task<IActionResult> putRegion([FromRoute] Guid id, [FromBody] PutRegionDto putRegionDto) {

            var regionDomainModel = new Region()
            {
                Code = putRegionDto.Code,
                Name = putRegionDto.Name,
                RegionImageUrl = putRegionDto.RegionImageUrl
            };

            var regionModal = await rigionRepository.putRegionAsync(id, regionDomainModel);
            if (regionModal == null)
            {
                return NotFound();
            }   ;

            RegionDto regionDto = new RegionDto()
            {
                Id = regionModal.Id,
                RegionImageUrl = regionModal.RegionImageUrl,
                Code= regionModal.Code,
                 Name = regionModal.Name
            };

            return Ok(regionModal);
        }

        [HttpDelete]
        [Route("deleteregion{id:Guid}")]
        public async Task<IActionResult> deleteRegion([FromRoute] Guid id) {
           Region? region = await rigionRepository.delateRegionAsync(id);
            if(region == null)
            {
                return NotFound();
            }

            RegionDto regionDto = new RegionDto()
            {
                Id = region.Id,
                RegionImageUrl = region.RegionImageUrl,
                Code = region.Code,
                Name = region.Name
            };

            return Ok(regionDto);
        }

    }
}
