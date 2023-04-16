using AutoMapper;
using WebApi.Entities;
using static WebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;
using static WebApi.Application.GameOperations.Commands.CreateGame.CreateGameCommand;
using static WebApi.Application.GameOperations.Commands.UpdateGame.UpdateGameCommand;
using static WebApi.Application.GameOperations.Queries.GetGameDetail.GetGameDetailQuery;
using static WebApi.Application.GameOperations.Queries.GetGames.GetGamesQuery;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static WebApi.Application.GenreOperations.Queries.GenGenreDetail.GetGenreDetailQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;
using static WebApi.Application.OrderOperations.Commands.CreateOrder.CreateOrderCommand;
using static WebApi.Application.OrderOperations.Queries.GetOrderDetail.GetOrderDetailQuery;
using static WebApi.Application.OrderOperations.Queries.GetOrders.GetOrdersQuery;
using static WebApi.Application.PublisherOperations.Command.CreatePublisher.CreatePublisherCommand;
using static WebApi.Application.PublisherOperations.Command.UpdatePublisher.UpdatePublisherCommand;
using static WebApi.Application.PublisherOperations.Queries.GetPublisherDetail.GetPublisherDetailQuery;
using static WebApi.Application.PublisherOperations.Queries.GetPublishers.GetPublishersQuery;

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

        CreateMap<CreatePublisherModel, Publisher>();
        CreateMap<UpdatePublisherModel, Publisher>();
        CreateMap<Publisher, PublisherViewModel>().ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.Games.Select(x => x.Name)));
        CreateMap<Publisher, PublisherDetailViewModel>().ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.Games.Select(x => x.Name)));

        CreateMap<CreateCustomerModel, Customer>();

        CreateMap<CreateOrderModel, Order>();
        CreateMap<Order, OrderViewModel>().ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.Surname)).ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Game.Name));
        CreateMap<Order, OrderDetailViewModel>().ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.Surname)).ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Game.Name));
    }
}