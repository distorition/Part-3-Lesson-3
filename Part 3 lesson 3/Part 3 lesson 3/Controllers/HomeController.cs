using Microsoft.AspNetCore.Mvc;
using Part_3_lesson_3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_3_lesson_3.Controllers
{
    public class HomeController : Controller
    {

        public PersonRepositories person;
        public HomeController(PersonRepositories person1)
        {
            person = person1;
        }

        [HttpGet("read")]
        public IEnumerable<Person> GetById([FromQuery]int personId)
        {

            return (IEnumerable<Person>)person.data.Where(x => x.Id == personId);
        }

        [HttpGet("person")]
        public IEnumerable<Person> Skiptake([FromForm] int skip=5,[FromQuery] int take = 10)
        {
            return (IEnumerable<Person>)person.data.Skip(skip).Take(take);
        }

        [HttpGet("name")]
        public IEnumerable<Person> GetName([FromQuery] string name)
        {
            return (IEnumerable<Person>)person.data.Where(x => x.FirstName == name);
        }

        [HttpPost("new person")]
        public IActionResult NewPerson([FromQuery]string name,[FromQuery] string emael,[FromQuery]int age,[FromQuery] int id)
        {
            person.data.Add(new Person() { Age=age,Email=emael,FirstName=name,Id=id});
            return View();
        }

        [HttpDelete("person delete")]
        public IActionResult DeletePerson(int IdtoDelete)
        {
           for(int i = 0; i < person.data.Count; i++)
            {
                if (person.data[i].Id == IdtoDelete)
                {
                    person.data = person.data.Where(x => x.Id != IdtoDelete).ToList();
                }
            }
            return View();
        }

        [HttpPut("update")]
        public IActionResult PersonUpdtade([FromQuery]string value,int idForSearch, string email,[FromQuery]string name)
        {
            for (int i = 0; i < person.data.Count; i++)
            {
                if (person.data[i].Id == idForSearch)
                {
                    person.data[i].Email = email;
                    person.data[i].FirstName = name;
                }
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
