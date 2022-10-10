// See https://aka.ms/new-console-template for more information
using Mankala;

const int startingStonesPerHole = 4;
const int holesPerPlayer  = 6;

Board board = new(startingStonesPerHole, holesPerPlayer);
board.Print();