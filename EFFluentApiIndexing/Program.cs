using EFFluentApiIndexing.Data;
using EFFluentApiIndexing.Model;

namespace EFFluentApiIndexing
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new PeopleContext();
            ctx.People.Add(new People()
            {
                Firstname = "Gavin",
                Lastname = "Draper",
                PhoneNumber = "01273 548669",
                NationalInsuranceNo = "PL33424"
            });
            ctx.SaveChanges();
        }
    }
}
