using AutoMapper;
using WebApi.Entities;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static WebApi.Application.GenreOperations.Queries.GenGenreDetail.GetGenreDetailQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;

namespace WebApi.Common;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<CreateGenreModel, Genre>();
        CreateMap<UpdateGenreModel, Genre>();
        CreateMap<Genre, GenreViewModel>();
        CreateMap<Genre, GenreDetailViewModel>();
    }
}