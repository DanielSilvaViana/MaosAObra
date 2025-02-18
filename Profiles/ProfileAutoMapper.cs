using AutoMapper;
using MaosAObra.DTO;
using MaosAObra.Models;

namespace MaosAObra.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
           
            CreateMap<EnderecoModel, EnderecoEditarDTO>();
            CreateMap<EnderecoEditarDTO, EnderecoModel>();
        }
    }
}
