using employee.Logic;
using employee.Model;

internal class Program
{
    private static List<Employee> empss = new List<Employee>();
    private static void Main(string[] args)
    {
        empss = CSVHelper.GetDataFromCSV("CSVdata/employees.csv"); //LISTAAA
        PrintEmployees();
        Feladatok();
    }
    private static void PrintEmployees()
    {
        foreach (var emp in empss)
        {
            Console.WriteLine($"Name: {emp.FirstName} {emp.LastName}, Gross Wage: {emp.GrossWage}, Net Wage: {emp.NetWage}, Job Title: {emp.JobTitle}, Department: {emp.Department}, Begin Date: {emp.BeginDate.ToShortDateString()}, End Date: {emp.EndDate.ToShortDateString()}");
        }
    }
    private static void Feladat1() 
    {
        //5 legjobban kereső munkavállaló
        var talalat = empss.OrderByDescending(e => e.GrossWage).Take(5);
        Console.WriteLine("\n1. Feladat: \t");
        foreach (var emp in talalat)
        {
            Console.WriteLine($"\t - Name: {emp.FirstName} {emp.LastName}, Gross Wage: {emp.GrossWage}");
        }
    }
    private static void Feladat2() 
    {
        //felhasználótól bekérünk egy nevet, és kiírjuk a hozzá tartozó információkat
        Console.WriteLine("\n2. Feladat: \t");
        Console.Write("\tKérem adja meg a munkavállaló nevét: ");
       
        bool found = false;
        do
        {
            string? inputName = Console.ReadLine();
            var emp = empss.FirstOrDefault(e => $"{e.FirstName} {e.LastName}".Equals(inputName, StringComparison.OrdinalIgnoreCase)); //nem key sensitive
            if (emp != null)
            {
                Console.WriteLine($"\t - {emp.ToString()}");
                found = true;
            }
            else
            {
                Console.WriteLine("\tNincs ilyen nevű munkavállaló.");
            }
        }
        while (found == false);

        
    }
    private static void Feladat3()
    {
        //akik inaktív dolgozók
        Console.WriteLine("\n3. Feladat:");
        foreach (var emp in empss)
        {
            if (emp.EndDate != default(DateTime))
            {
                Console.WriteLine($"\t - {emp.ToString()}");
            }
        }
    }
    private static void Feladat4()
    {
        //adatbázisba mentés
        var talalat = Database.SaveEmployees(empss);
        if (talalat)
        {
            Console.WriteLine("\n4. Feladat: \tAdatok sikeresen mentve az adatbázisba.");
        }
        else
        {
            Console.WriteLine("\n4. Feladat: \tHiba történt az adatok mentése során.");
        }
    }
    private static void Feladatok() 
    {
        //Feladat1();
        //Feladat2();
        //Feladat3();
        Feladat4();
    }
}