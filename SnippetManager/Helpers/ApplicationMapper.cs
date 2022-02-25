using AutoMapper;
using SnippetManager.Models;

namespace SnippetManager.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Snippet, SnippetDto>().ReverseMap();
        }
    }
}
