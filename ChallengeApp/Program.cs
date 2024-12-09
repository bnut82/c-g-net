using System;
using Challenge.Application.Transformer;
using Challenge.Infrastructure.Service;
using ChallengeContracts;
using CommandLine;
using static System.Boolean;

namespace Challenge
{
    public class Options
    {
        [Option('s', "search", Required = true, HelpText = "Search string ex. ()")]
        public string Search { get; set; }

        [Option('n', "name", Required = true, HelpText = "Name.")]
        public string Name { get; set; }

        [Option('a', "annualCycle", Required = true, HelpText = "Annual Cycle set true")]
        public string AnnualCycle { get; set; }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var engine = new SearchEngine();
            while (true)
            {
                Parser.Default.ParseArguments<Options>(args)
                    .WithParsed<Options>(opts =>
                    {
                        TryParse(opts.AnnualCycle, out bool isAnnualCycle);

                        try
                        {
                            var searchArg = new SearchArg()
                            {
                                Dates = TransformStringToDate.Handle(opts.Search),
                                HotelName = TransformStringByKey.Handle(opts.Search, 0),
                                RoomType = TransformStringByKey.Handle(opts.Search, 2)
                            };
                            
                            if (isAnnualCycle is false)
                            {
                                engine.SearchHandler(searchArg);
                            }

                            if (isAnnualCycle is true)
                            {
                                engine.SearchHandler(searchArg, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    })
                    .WithNotParsed(errors => { Console.WriteLine("Wystąpił błąd przy parsowaniu parametrów."); });

                var input = Console.ReadKey(intercept: true);
                if (input.Key == ConsoleKey.Spacebar)
                {
                    break;
                }
            }
        }
    }
}