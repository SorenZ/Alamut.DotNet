using Alamut.Data.Structure;
using Alamut.Sample.DataDriven.Dto;
using Alamut.Sample.DataDriven.Models;
using Alamut.Sample.DataDriven.ViewModels;
using AutoMapper;

namespace Alamut.Sample.DataDriven.Mapper
{
    public class MapStrapper : Profile
    {
        public MapStrapper()
        {
            CreateMap<ArticleCreateVm, Article>();
            CreateMap<Article, ArticleSimple>();
            CreateMap<Article, IdTitle>();
        }
    }
}
