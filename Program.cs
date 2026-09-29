using System.Diagnostics.Tracing;

namespace Shop_OOP_Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shop RiverCitySupply = new Shop("River City Supply");
            PerishableGood WildflowerHoney = new PerishableGood("HON01", "Wildflower honey", 8.00m, 12, 1.5, 2);
            DurableGood CastIronKettle = new DurableGood("KTL11", "Cast iron kettle", 24.00m, 5, 4.0, 24);
            PerishableGood FarmCheddarWedge = new PerishableGood("CHZ07", "Farm cheddar wedge", 3.50m, 40, 0.5, 9);
            ServiceItem KnifeSharpening = new ServiceItem("SRV20", "Knife sharpening", 60.00m, 2, 2.5);
            ServiceItem GiftWrapping = new ServiceItem("SRV21", "Gift wrapping", 15.00m, 3, 1.0);

            PerishableGood SecretSixth = new PerishableGood("HON01", "Should not work", 13.00m, 13, 1.3, 5);

            WildflowerHoney.Recieve(6);
            CastIronKettle.Release(2);
            CastIronKettle.Release(99);
            FarmCheddarWedge.Recieve(-5);

            Console.Write($"Opening catalog:  {RiverCitySupply.ReportLine()}");
            Console.WriteLine();
            Console.WriteLine("Loading five records...");

            RiverCitySupply.Add(WildflowerHoney);
            RiverCitySupply.Add(CastIronKettle);
            RiverCitySupply.Add(FarmCheddarWedge);
            RiverCitySupply.Add(KnifeSharpening);
            RiverCitySupply.Add(GiftWrapping);


            RiverCitySupply.PrintReport();
        }
    }
}
