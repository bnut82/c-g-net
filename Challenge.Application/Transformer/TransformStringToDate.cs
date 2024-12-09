using System;
using Challenge.Core;
using ChallengeContracts;

namespace Challenge.Application.Transformer;

public static class TransformStringToDate
{
    public static SearchArgDate Handle(string data , int key = 1)
    {
        var dataWithComma = data.Split(",");
        var dates = dataWithComma[key].Split('-');
        
        return new SearchArgDate()
        {
            From = DateTime.ParseExact(dates[0], EnumCore.DateFormat,  System.Globalization.CultureInfo.InvariantCulture),
            To = DateTime.ParseExact(dates[0], EnumCore.DateFormat,  System.Globalization.CultureInfo.InvariantCulture),
        };
    }
}