﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CellsAutomate.Constants;
using CellsAutomate.Food;

namespace CellsAutomate.Creatures
{
    public class SimpleCreature : BaseCreature
    {
        public SimpleCreature(Point position, Random random, int generation)
        {
            Position = position;
            Random = random;
            Generation = generation;
        }

        protected override DirectionEnum GetDirection(FoodMatrix eatMatrix, BaseCreature[,] creatures)
        {
            var points = DirectionEx.GetPoints(Position.X, Position.Y);
            var directions = new List<DirectionEnum>();
            var directionsWithFood = new List<DirectionEnum>();
            foreach (var item in points)
            {
                if (DirectionEx.IsValidAndFree(item, creatures))
                {
                    directions.Add(DirectionEx.DirectionByPoints(Position, item));
                    if(eatMatrix.GetLevelOfFood(item) >= CreatureConstants.OneBite)
                        directionsWithFood.Add(DirectionEx.DirectionByPoints(Position, item));
                }
            }
            if (directions.Count == 0) return DirectionEnum.Stay;
            return directionsWithFood.Count == 0 ? directions.ElementAt(Random.Next(directions.Count)) : directionsWithFood.ElementAt(Random.Next(directionsWithFood.Count));
        }

        public override BaseCreature MakeChild(Point position)
        {
            EnergyPoints -= CreatureConstants.ChildPrice;
            return new SimpleCreature(position, Random, Generation + 1);
        }
    }
}