using AutoMapper;
using WebApi.Entities;
using static WebApi.Application.GameOperations.Commands.CreateGame.CreateGameCommand;
using static WebApi.Application.GameOperations.Commands.UpdateGame.UpdateGameCommand;
using static WebApi.Application.GameOperations.Queries.GetGameDetail.GetGameDetailQuery;
using static WebApi.Application.GameOperations.Queries.GetGames.GetGamesQuery;
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

        CreateMap<CreateGameModel, Game>();
        CreateMap<UpdateGameModel, Game>();
        CreateMap<Game, GameViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.Name));
        CreateMap<Game, GameDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.Name));
    }
}