// See https://aka.ms/new-console-template for more information
using Mankala;

const byte startingStonesPerHole = 42;
const byte holesPerPlayer = 9;

Board board = new(startingStonesPerHole, holesPerPlayer);
board.Print();