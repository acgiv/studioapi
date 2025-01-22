using System.Linq.Expressions;
using API.Data;
using API.Model.Domain;
using API.Model.DTO;
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

        // richimare i metodi nel parametro del contruttore e poi assegnarli come valore
        public RegionsController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll() {
         
            var regionsDomain = await dbContext.Regions.ToListAsync();

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
            // altro controllo 
            // dbContext.Regions.Find(id);
            //dbContext.Regions.Where(elem =>  elem.Id == id).ToList();
           

            var regionsDomain = await dbContext.Regions.FirstOrDefaultAsync(elem => elem.Id == id);

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

            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

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

           var regionModal = await this.dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if (regionModal == null)
            {
                return NotFound();
            }

            regionModal.Name = putRegionDto.Name;
            regionModal.Code = putRegionDto.Code;
            regionModal.RegionImageUrl = putRegionDto.RegionImageUrl;

           await  dbContext.SaveChangesAsync();

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
          var region =  await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if(region == null)
            {
                return NotFound();
            }
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();

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
