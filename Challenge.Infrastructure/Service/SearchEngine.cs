using System;
using System.Collections.Generic;
using System.Linq;
using Challenge.Application.Exception;
using ChallengeContracts;
using ChallengeContracts.Dto;

namespace Challenge.Infrastructure.Service;

public class SearchEngine : ISearchEngine
{
    private readonly IDataMapper _dataMapper = new DataMapper();
    private readonly List<Hotel>? _hotels;

    public SearchEngine()
    {
        _hotels = _dataMapper.Map();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    private Room SearchRoom(SearchArg arg)
    {
        var hotels = _hotels!.SelectMany(r => r.Rooms!)
            .Where(
                r =>
                    r.RoomType == arg.RoomType &&
                    r.RoomReservation is not null
            ).ToList();

        var result = hotels.FirstOrDefault(r => r.RoomReservation!.Arrival >= arg.Dates.From &&
                                                r.RoomReservation!.Arrival <= arg.Dates.To);
        if (result == null)
        {
            throw new RoomNotExistException();
        }

        return result;
    }
    
    /// <summary>
    /// Rooms booked on an annual basis
    /// </summary>
    private List<Room> Availability(SearchArg arg)
    {
        return _hotels!.SelectMany(r => r.Rooms!)
            .Where(
                r =>
                    r.RoomType == arg.RoomType &&
                    r.RoomReservation is not null &&
                    r.RoomReservation!.Arrival >= DateTime.Now &&
                    r.RoomReservation!.Departure <= DateTime.Now.AddYears(1)
            ).ToList();
    }
    
    public string SearchHandler(SearchArg args)
    {
        var result = SearchRoom(args);

        return result.ToString();
    }
    public string SearchHandler(SearchArg args , bool annualCycle)
    {
        var result = Availability(args);

        return string.Join(", ", result);
    }
}