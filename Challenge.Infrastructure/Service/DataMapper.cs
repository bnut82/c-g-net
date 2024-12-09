using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Challenge.Application.Exception;
using Challenge.Core;
using ChallengeContracts;
using ChallengeContracts.Dto;

namespace Challenge.Infrastructure.Service;

public class DataMapper : IDataMapper
{
    private readonly string _pathToHotels = "hotels.json";
    private readonly string _pathToBookings = "bookings.json";

    private Hotel? FindHotelById(List<Hotel> hotels , string hotelId)
    {
        return hotels.FirstOrDefault(h => h!.HotelId == hotelId);
    }

    private Room? FindRoomInHotel(Hotel hotel, string roomType)
    {
        return hotel!.Rooms!.FirstOrDefault(r => r.RoomType == roomType);
    }
    
    
    public List<Hotel>? Map()
    {
        var hotels = new List<Hotel>();
       
        if (File.Exists(_pathToHotels))
        {
            hotels = JsonSerializer.Deserialize<List<Hotel>>(File.ReadAllText(_pathToHotels));
        }

        if (File.Exists(_pathToBookings) && hotels!.Count > 0)
        {
            var bookings = JsonSerializer.Deserialize<List<Bookings>>(File.ReadAllText(_pathToBookings));

            foreach (var item in bookings!) 
            {
                var hotel = FindHotelById(hotels, item.HotelId!);
                
                if (hotel == null)
                {
                    throw new HotelNotExistException();
                }
                
                var room = FindRoomInHotel(hotel!, item.RoomType!);
                if (room!.RoomReservation is null)
                {
                    room.RoomReservation = new RoomReservationDto()
                    {
                        Arrival = DateTime.ParseExact(item.Arrival!, EnumCore.DateFormat,System.Globalization.CultureInfo.InvariantCulture),
                        Departure = DateTime.ParseExact(item.Departure! , EnumCore.DateFormat,System.Globalization.CultureInfo.InvariantCulture)
                    };
                }
            }
        }
        
        return hotels;
    }
}