// See https://aka.ms/new-console-template for more information

const int steentjesPerGat = 4;
const int gatenPerSpeler  = 6;

(byte, byte)[] eigenhokjes = new (byte, byte)[gatenPerSpeler];

string thuisHok(int n) => $@"##
##
#{n}
##
##";
string spelerHokjes(int u, int l) => $@"#
{u}
#
{l}
#";

Console.WriteLine(
	thuisHok(0)
	+ eigenhokjes.Select(x => spelerHokjes(x.Item1, x.Item2))
);