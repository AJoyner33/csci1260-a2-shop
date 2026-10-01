using System.Diagnostics.Tracing;

namespace Shop_OOP_Lab2
{
    /// <summary>
    /// Entry point for the sample application that demonstrates inventory operations.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Shop RiverCitySupply = new Shop("River City Supply");

            Console.Write($"Opening catalog:  {RiverCitySupply.ReportLine()}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Loading five records...");

            PerishableGood WildflowerHoney = new PerishableGood("HON01", "Wildflower honey", 8.00m, 12, 1.5, 2);
            DurableGood CastIronKettle = new DurableGood("KTL11", "Cast iron kettle", 24.00m, 5, 4.0, 24);
            PerishableGood FarmCheddarWedge = new PerishableGood("CHZ07", "Farm cheddar wedge", 3.50m, 40, 0.5, 9);
            ServiceItem KnifeSharpening = new ServiceItem("SRV20", "Knife sharpening", 60.00m, 2, 2.5);
            ServiceItem GiftWrapping = new ServiceItem("SRV21", "Gift wrapping", 15.00m, 3, 1.0);

            PerishableGood SecretSixth = new PerishableGood("HON01", "Should not work", 13.00m, 13, 1.3, 5);

            RiverCitySupply.Add(WildflowerHoney);
            RiverCitySupply.Add(CastIronKettle);
            RiverCitySupply.Add(FarmCheddarWedge);
            RiverCitySupply.Add(KnifeSharpening);
            RiverCitySupply.Add(GiftWrapping);
            RiverCitySupply.Add(SecretSixth);

            StockItem sixth = RiverCitySupply.Find("HON01");
            if (sixth != null)
            {
                Console.WriteLine("     REJECTED: duplicate SKU HON01");
            }

            Console.WriteLine();
            Console.WriteLine("Recording four movements...");

            WildflowerHoney.Recieve(6);
            CastIronKettle.Release(2);
            CastIronKettle.Release(99);
            FarmCheddarWedge.Recieve(-5);

            StockItem kettle01 = RiverCitySupply.Find("KTL11");
            if (kettle01 != null && !kettle01.Release(99))
            {
                Console.WriteLine("     REJECTED: release of 99 from KTL11");
            }

            StockItem cheddar = RiverCitySupply.Find("CHZ07");
            if (cheddar != null && !cheddar.Recieve(-5))
            {
                Console.WriteLine("     REJECTED: receive of -5 into CHZ07");
            }

            StockItem kettle02 = RiverCitySupply.Find("KTL11");
            if (kettle02 != null && kettle02.QuantityOnHand < 2)
            {
                Console.WriteLine("     REJECTED: release of 2 from KTL11");
            }

            StockItem honey = RiverCitySupply.Find("HON01");
            if (honey != null && !honey.Recieve(6))
            {
                Console.WriteLine("     REJECTED: receive of 6 into HON01");
            }

            Console.WriteLine();
            Console.WriteLine("Records accepted: {0}", RiverCitySupply.Count);
            Console.WriteLine("Movements accepted: {0}", WildflowerHoney.MoveCount + CastIronKettle.MoveCount + FarmCheddarWedge.MoveCount);

            Console.WriteLine();
            Console.WriteLine("Top record: {0}", FarmCheddarWedge.ToString());
            Console.WriteLine();

            RiverCitySupply.SortByValue();

            RiverCitySupply.PrintReport();

            Console.WriteLine();
            Console.WriteLine("Contract check");
            Console.WriteLine("{0,-43}{1,6} {2,10:0}", "Records signing IDiscountable", "", RiverCitySupply.SignedCount());
            Console.WriteLine("{0,-43}{1,6} {2,10:0}", "Records on sale right now:", "", RiverCitySupply.OnSaleCount());
            Console.WriteLine("{0,-43}{1,6} ${2,9:0.00}", "Difference between the two totals:", "", RiverCitySupply.TotalValue() - RiverCitySupply.SalesValue());

            Console.WriteLine();
            Console.WriteLine("Composition check");
            Console.WriteLine(String.Format(" {0,-46} {1,11}","Movements recorded by HON01:", WildflowerHoney.MoveCount));
            if (WildflowerHoney.MoveCount > 0)
                Console.WriteLine(WildflowerHoney.MovementLines());

            Console.WriteLine(String.Format(" {0,-46} {1,11}", "Movements recorded by KTL11:", CastIronKettle.MoveCount));
            if (CastIronKettle.MoveCount > 0)
                Console.WriteLine(CastIronKettle.MovementLines());

            Console.WriteLine(String.Format(" {0,-46} {1,11}", "Movements recorded by CHZ07:", FarmCheddarWedge.MoveCount));
            if (FarmCheddarWedge.MoveCount > 0)
                Console.WriteLine(FarmCheddarWedge.MovementLines());
        }
    }
}
