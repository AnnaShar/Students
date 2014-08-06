﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using FilmService;
using FilmService.KindsOfGenerators;
using FilmService.KindsOfMovies;
using System.Xml;

namespace RefactoringByKirill
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new Customer("Igor", new StatementGeneratorJSON());
            user.Rentals.Add(new Rental(new Movie("Edge of Tomorrow", new CalculatorForMovieNewRelease()), 5));
            user.Rentals.Add(new Rental(new Movie("Gravity", new CalculatorForMovieRegular()), 2));
            user.CurrentStatementGenerator.FormDataForStatement(user.Name, user.Rentals);
            var path = "userSerialize.txt";
            user.CurrentStatementGenerator.Generate(path);

            var JsonSerializer = new DataContractJsonSerializer(typeof(DataForStatement));
            DataForStatement deserializeData;
            using (FileStream input = File.OpenRead(path))
            {
                deserializeData = JsonSerializer.ReadObject(input) as DataForStatement;
            }
            if (user.CurrentStatementGenerator.CurrentData.Equals(deserializeData))
            {
                Console.Write("eqels");
            }
            else
            {
                Console.Write("Noooooooooooooooooooo");
            }
            Console.ReadLine();
        }
    }
}
