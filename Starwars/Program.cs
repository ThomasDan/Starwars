using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Starwars
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Planet> planets = LoadData();

            // Opg 1, udskriv alle planeter der starter med M vha. Linq
            
            Console.WriteLine("1. M Planets:\n" + 
                string.Join("\n", 
                    planets.Select(p => p.Name).Where(n => n[0].Equals('M')))
                );

            // Opg 2, udskriv en liste over specifikke planeter

            Console.WriteLine("\n\n2. Planets containing Y:\n" + 
                string.Join("\n", 
                    planets.Select(p => p.Name).Where(n => n.ToUpper().Contains('Y')))
                );

            // Opg 3, udksriv alle planeter med mere end 9 og færre end 15 tegn.

            Console.WriteLine(
                "\n\n3. Planets with names that are between 9 and 15 characters long:\n" +
                string.Join("\n", 
                    planets.Select(p => p.Name).Where(n => n.Length > 9 && n.Length < 15))
                );

            // Opg 4, udskriv alle planet hvor [1] er 'a' og [-1] er 'e'

            Console.WriteLine(
                "\n\n4. Planets where 2'nd char is 'a' and the last character is 'e':\n" +
                string.Join("\n", 
                    planets.Select(p => p.Name).Where(n => n.Length > 1 && n[1].Equals('a') && n.Last().Equals('e')))
                );

            // Opg 5, udskriv alle planeter hvor rotationsperioden er større end 40, og sorter efter rotationsperioden

            Console.WriteLine(
                "\n\n5. Planets with rotational period greater than 40, sorted by rotation period:\n" +
                string.Join("\n",
                    planets.Where(p => p.RotationPeriod > 40).OrderBy(p => p.RotationPeriod).Select(p => p.Name)
                    ));

            // Opg 6, Udskriv alle planeter med rotationsperioder større end 10 og mindre end 20. Sorterert på navn.

            Console.WriteLine(
                "\n\n6. Planets with rotationperiod greater than 10 and less than 20, sorted by name:\n" +
                string.Join("\n",
                    planets.Where(p => p.RotationPeriod > 10 && p.RotationPeriod < 20).OrderBy(p => p.Name).Select(p => p.Name)
                    ));

            // Opg 7, Udskriv bla bla

            Console.WriteLine(
                "\n\n7. Planets with greater than 30 rotationperiod:\n" +
                string.Join("\n",
                    planets.Where(p => p.RotationPeriod > 30).OrderBy(p => p.Name).ThenBy(p => p.RotationPeriod).Select(p => p.Name)
                    ));

            // Opg 8

            Console.WriteLine(
                "\n\n8. Planets with rotation period less than 30 or surfacewater greater than 50, and there is 'ba' in the name, sorted by name, surfacewater, rotationperiod:\n" +
                string.Join("\n",
                    planets.Where(p => (p.RotationPeriod < 30 || p.SurfaceWater > 50) && p.Name.Contains("ba")).OrderBy(p => p.Name).ThenBy(p => p.SurfaceWater).ThenBy(p => p.RotationPeriod).Select(p => p.Name)
                    ));

            // Opg 9

            Console.WriteLine(
                "\n\n9. Planets where there is surface water, ordered by surfacewater:\n" +
                string.Join("\n", 
                    planets.Where(p => p.SurfaceWater > 0).OrderByDescending(p => p.SurfaceWater).Select(p => p.Name)
                    ));

            // Opg 10

            Console.WriteLine(
                "\n\n10. Planets where the diameter and number of citizens is known, ordered by how many square kilometers are available to each person ascending:\n" +
                string.Join("\n", // I couldn't find a nicer way of checking for null or undefined, but it seems to handle it perfectly. "!p.Diameter.Equals(null)", for example, did Not work
                    planets.Where(p => p.Diameter > 0 && p.Population > 0)
                        .OrderBy(p => 4 * Math.PI * Math.Pow((double)p.Diameter / 2, 2) / p.Population)
                        .Select(p => p.Name + ": " + 4 * Math.PI * Math.Pow((double)p.Diameter / 2, 2) / p.Population)
                    ));

            // Opg 11

            Console.WriteLine(
                "\n\n11. Planets where the rotationperiod is greater than 0, using Except" + 
                string.Join("\n", 
                    planets.Except(new List<Planet>(planets.Where(p => p.RotationPeriod > 0))).Select(p => p.Name)
                    ));

            // Opg 12

            Console.WriteLine(
                "\n\n12. Planets:\n" +
                string.Join("\n", 
                    new List<Planet>(planets.Where(p => p.Name[0].Equals('A') || p.Name.Last().Equals('s')))
                        .Union( // I don't like using !=, but I found no other effective way of doing it.
                            new List<Planet>(planets.Where(p => p.Terrain != null && p.Terrain.Contains("rainforests")))
                        ).Select(p => p.Name)
                    ));

            // Opg 13

            Console.WriteLine(
                "\n\n13. Planets:\n" +
                string.Join("\n",
                    planets.Where(p => p.Terrain != null && (p.Terrain.Contains("deserts") || p.Terrain.Contains("desert"))).Select(p => p.Name)
                    ));

            // Opg 14

            Console.WriteLine(
                "\n\n14. Planets:\n" +
                string.Join("\n",
                    planets.Where(p => p.Terrain != null && (p.Terrain.Contains("swamp") || p.Terrain.Contains("swamps"))).OrderBy(p => p.RotationPeriod).ThenBy(p => p.Name).Select(p => p.Name)
                ));

            // Opg 15

            Console.WriteLine(
                "\n\n15. Planets:\n" +
                string.Join("\n",
                    planets.Where(p => Regex.IsMatch(p.Name, "(aa|ee|ii|oo|uu|yy)")).Select(p => p.Name)
                ));

            // Opg 16

            Console.WriteLine(
                "\n\n16. Planets:\n" +
                string.Join("\n",
                    planets.Where(p => Regex.IsMatch(p.Name, "(kk|ll|rr|nn)")).OrderByDescending(p => p.Name).Select(p => p.Name)
                ));

            Console.ReadKey();
        }



        static List<Planet> LoadData()
        {
            List<Planet> planets = new List<Planet>()
            {
                new Planet { Name="Corellia", Terrain= new List<string>{ "plains", "urban", "hills", "forests" },RotationPeriod=25,SurfaceWater=70, Diameter=11000, Population=3000000000},
                new Planet { Name="Rodia", Terrain= new List<string>{ "jungles", "oceans", "urban", "swamps" },RotationPeriod=29,SurfaceWater=60, Diameter=7549, Population=1300000000},
                new Planet { Name="Nal Hutta", Terrain= new List<string>{ "urban", "oceans", "bogs", "swamps" },RotationPeriod=87, Diameter=12150, Population=7000000000},
                new Planet { Name="Dantooine",Terrain= new List<string>{ "savannas", "oceans", "mountains", "grasslands" },RotationPeriod=25, Diameter=9830,Population=1000},
                new Planet { Name="Bestine IV",Terrain= new List<string>{ "rocky islands", "oceans" },RotationPeriod=26,SurfaceWater=98, Diameter=6400,Population=62000000},
                new Planet { Name="Ord Mantell",Terrain= new List<string>{ "plains", "seas","mesas" },RotationPeriod=26,SurfaceWater=10, Diameter=14050, Population=4000000000},
                new Planet { Name="Trandosha",Terrain= new List<string>{ "mountains", "seas","grasslands" ,"deserts"},RotationPeriod=25, Diameter=0, Population=42000000},
                new Planet { Name="Socorro", Terrain= new List<string>{ "mountains", "deserts"},RotationPeriod=20, Population=300000000},
                new Planet { Name="Mon Cala",Terrain= new List<string>{ "oceans", "reefs","islands"},RotationPeriod=21,SurfaceWater=100,Diameter=11030,Population=27000000000},
                new Planet { Name="Chandrila", Terrain= new List<string>{ "plains", "forests"},RotationPeriod=20,SurfaceWater=40,Diameter=13500,Population=1200000000},
                new Planet { Name="Sullust", Terrain= new List<string>{ "mountains", "volcanoes","rocky deserts"},RotationPeriod=20,SurfaceWater=5, Diameter=12780, Population=18500000000},
                new Planet { Name="Toydaria", Terrain= new List<string>{ "swamps", "lakes"},RotationPeriod=21, Diameter=7900, Population=11000000},
                new Planet { Name="Malastare",Terrain= new List<string>{ "swamps", "deserts","jungles","mountains"},RotationPeriod=26, Diameter=18880, Population=2000000000},
                new Planet { Name="Dathomir",Terrain= new List<string>{ "forests", "deserts","savannas"},RotationPeriod=24, Diameter=10480, Population=5200},
                new Planet { Name="Ryloth",Terrain= new List<string>{ "mountains", "valleys","deserts","tundra"},RotationPeriod=30,SurfaceWater=5,Diameter=10600, Population=1500000000 },
                new Planet { Name="Aleen Minor"},
                new Planet { Name="Vulpter",Terrain= new List<string>{ "urban", "barren"} ,RotationPeriod=22, Diameter=14900, Population=421000000},
                new Planet { Name="Troiken",Terrain= new List<string>{ "desert", "tundra","rainforests","mountains"} },
                new Planet { Name="Tund",Terrain= new List<string>{ "barren", "ash"} ,RotationPeriod=48, Diameter=12190},
                new Planet { Name="Haruun Kal",Terrain= new List<string>{ "toxic cloudsea", "plateaus","volcanoes"},RotationPeriod=25,Diameter=10120,Population=705300},
                new Planet { Name="Cerea",Terrain= new List<string>{ "verdant"},RotationPeriod=27,SurfaceWater=20,Population=450000000},
                new Planet { Name="Glee Anselm",Terrain= new List<string>{ "islands","lakes","swamps", "seas"},RotationPeriod=33,SurfaceWater=80, Diameter=15600,Population=500000000},
                new Planet { Name="Iridonia",Terrain= new List<string>{ "rocky canyons","acid pools"},RotationPeriod=29},
                new Planet { Name="Tholoth"},
                new Planet { Name="Iktotch",Terrain= new List<string>{ "rocky"},RotationPeriod=22},
                new Planet { Name="Quermia",},
                new Planet { Name="Dorin",RotationPeriod=22, Diameter=13400},
                new Planet { Name="Champala",Terrain= new List<string>{ "oceans","rainforests","plateaus"},RotationPeriod=27, Population=3500000000},
                new Planet { Name="Mirial",Terrain= new List<string>{ "deserts"}},
                new Planet { Name="Serenno",Terrain= new List<string>{ "rivers","rainforests","mountains"}},
                new Planet { Name="Concord Dawn",Terrain= new List<string>{ "jungles","forests","deserts"}},
                new Planet { Name="Zolan" },
                new Planet { Name="Ojom",Terrain= new List<string>{ "oceans","glaciers"},SurfaceWater=100, Population=500000000},
                new Planet { Name="Skako", Terrain = new List<string>{ "urban","vines"},RotationPeriod=27, Population=500000000000},
                new Planet { Name="Muunilinst",Terrain= new List<string>{ "plains","forests","hills","mountains"} ,RotationPeriod=28,SurfaceWater=25, Diameter=13800, Population=5000000000},
                new Planet { Name="Shili",Terrain= new List<string>{ "cities","savannahs","seas","plains"}},
                new Planet { Name="Kalee",Terrain= new List<string>{ "rainforests","cliffs","seas","canyons"},RotationPeriod=23, Diameter=13850, Population=4000000000},
                new Planet { Name="Umbara"},
                new Planet { Name="Tatooine",Terrain= new List<string>{ "deserts"},RotationPeriod=23,SurfaceWater=1, Diameter=10465, Population=200000 },
                new Planet { Name="Jakku",Terrain= new List<string>{ "deserts"}},
                new Planet { Name="Alderaan",Terrain= new List<string>{ "grasslands","mountains"},RotationPeriod=24,SurfaceWater=40, Diameter=12500, Population= 2000000000},
                new Planet { Name="Yavin IV", Terrain= new List<string>{ "rainforests","jungle"},RotationPeriod=24,SurfaceWater=8,Diameter=10200,Population=     1000},
                new Planet { Name="Hoth", Terrain= new List<string>{ "tundra","ice caves","mountain ranges"},RotationPeriod=23,SurfaceWater=100},
                new Planet { Name="Dagobah",Terrain= new List<string>{ "swamp","jungles"},RotationPeriod=23,SurfaceWater=8},
                new Planet { Name="Bespin",Terrain= new List<string>{ "gas giant"},RotationPeriod=12, Diameter=118000,Population=  6000000},
                new Planet { Name="Endor",Terrain= new List<string>{ "forests","mountains","lakes"},RotationPeriod=18,SurfaceWater=8, Diameter=4900, Population= 30000000},
                new Planet { Name="Naboo",Terrain= new List<string>{ "grassy hills","swamps","forests","mountains"},RotationPeriod=26,SurfaceWater=12, Diameter=12120, Population=  4500000000},
                new Planet { Name="Coruscant",Terrain= new List<string>{ "cityscape","mountains"},RotationPeriod=24,Diameter=12240,Population=1000000000000},
                new Planet { Name="Kamino",Terrain= new List<string>{ "ocean"},RotationPeriod=27,SurfaceWater=100,Diameter=19720, Population=1000000000},
                new Planet { Name="Geonosis",Terrain= new List<string>{ "rock","desert","mountain","barren"},RotationPeriod=30,SurfaceWater=5,Diameter=11370, Population=100000000000},
                new Planet { Name="Utapau",Terrain= new List<string>{ "scrublands","savanna","canyons","sinkholes"},RotationPeriod=27,SurfaceWater=0.9f,Diameter=12900,Population=  95000000},
                new Planet { Name="Mustafar",Terrain= new List<string>{ "volcanoes","lava rivers","mountains","caves"},RotationPeriod=36,  Diameter=4200, Population=20000},
                new Planet { Name="Kashyyyk",Terrain= new List<string>{ "jungle","forests","lakes","rivers"},RotationPeriod=26 ,SurfaceWater=60,Diameter=12765, Population=45000000},
                new Planet { Name="Polis Massa",Terrain= new List<string>{ "airless","asteroid"},RotationPeriod=24, Diameter=0, Population=1000000},
                new Planet { Name="Mygeeto",Terrain= new List<string>{ "glaciers","mountains","ice canyons"},RotationPeriod=12, Diameter=10088, Population=  19000000},
                new Planet { Name="Felucia",Terrain= new List<string>{ "fungus","forests"},RotationPeriod=34, Diameter=9100,Population=8500000},
                new Planet { Name="Cato Neimoidia",Terrain= new List<string>{ "mountains","fields","forests","rock arches"},RotationPeriod=25, Population=10000000},
                new Planet { Name="Saleucami",Terrain= new List<string>{ "caves", "deserts","mountains","volcanoes"},RotationPeriod=26, Population=1400000000, Diameter=14920},
                new Planet { Name="Stewjon",Terrain= new List<string>{ "grass"}},
                new Planet { Name="Eriadu",Terrain= new List<string>{ "cityscape"},RotationPeriod=24, Diameter=13490  , Population= 22000000000},
             };
          return planets;
        }
    }
}
