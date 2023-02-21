using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonCrudwithDapper.DTO;
using PersonCrudwithDapper.Models;
using PersonCrudwithDapper.Service;

namespace PersonCrudwithDapper.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly PersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(ILogger<PersonController> logger, PersonService personService, IMapper mapper)
        {
            _logger = logger;
            _personService = personService;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            var persons = await _personService.GetAllPeopleAsync();
            var peopleViewModels = _mapper.Map<List<Person>, List<PersonViewModel>>(persons.ToList());
            return View(peopleViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var person = await _personService.GetPersonByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            var personViewModel = new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                Gender = (Gender)Enum.Parse(typeof(Gender), person.Gender),
                Country = person.Country,
                State = person.State,
                City = person.City,
                DateOfBirth = person.DateOfBirth,
                Hobbies = person.Hobbies,
                AcceptTermsAndCondition = person.AcceptTermsAndCondition
            };

            return View(personViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonViewModel personViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(personViewModel);
            }

            var person = new Person
            {
                FirstName = personViewModel.FirstName,
                Gender = personViewModel.Gender.ToString(),
                Country = personViewModel.Country,
                State = personViewModel.State,
                City = personViewModel.City,
                DateOfBirth = personViewModel.DateOfBirth,
                Hobbies = personViewModel.Hobbies,
                AcceptTermsAndCondition = personViewModel.AcceptTermsAndCondition
            };

            await _personService.CreatePersonAsync(person);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personService.GetPersonByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            var personViewModel = new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                Gender = (Gender)Enum.Parse(typeof(Gender), person.Gender),
                Country = person.Country,
                State = person.State,
                City = person.City,
                DateOfBirth = person.DateOfBirth,
                Hobbies = person.Hobbies,
                AcceptTermsAndCondition = person.AcceptTermsAndCondition
            };

            return View(personViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonViewModel personViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(personViewModel);
            }

            var person = new Person
            {
                Id = personViewModel.Id,
                FirstName = personViewModel.FirstName,
                Gender = personViewModel.Gender.ToString(),
                Country = personViewModel.Country,
                State = personViewModel.State,
                City = personViewModel.City,
                DateOfBirth = personViewModel.DateOfBirth,
                Hobbies = personViewModel.Hobbies,
                AcceptTermsAndCondition = personViewModel.AcceptTermsAndCondition
            };

            await _personService.UpdatePersonAsync(person);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personService.GetPersonByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personService.DeletePersonAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }

}
