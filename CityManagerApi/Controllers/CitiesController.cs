using AutoMapper;
using CityManagerApi.Data;
using CityManagerApi.Dtos;
using CityManagerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityManagerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        [HttpGet("{id?}")]
        public IActionResult GetCities(int id=3)
        {
            var cities = _appRepository.GetCities(id);
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);

            return Ok(citiesToReturn);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(CityDto dto)
        {
            var item=_mapper.Map<City>(dto);
            _appRepository.Add(item);
            _appRepository.SaveAll();
            return Ok(item);
        }

        [HttpGet("Detail")]
        public CityForDetailDto GetCityById(int id)
        {
            var city=_appRepository.GetCityById(id);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return cityToReturn;
        }

        [HttpGet("Photos/{cityId}")]
        public List<CityImage> GetPhotosByCityId(int cityId)
        {
            var photos=_appRepository.GetPhotosByCityId(cityId);
            return photos;
        }

        [HttpGet("SinglePhoto/{id}")]
        public CityImage GetPhotoById(int id)
        {
            var photo = _appRepository.GetPhotoById(id);
            return photo;
        }
    }
}
