using People.Models;
using People.Services;

namespace People;

public partial class MainPage : ContentPage
{
    private readonly PersonRepository _personRepo;

    public MainPage(PersonRepository personRepo)
    {
        InitializeComponent();
        _personRepo = personRepo;
    }

    private void OnAddPersonClicked(object sender, EventArgs e)
    {
        string name = nameEntry.Text?.Trim();
        if (!string.IsNullOrEmpty(name))
        {
            _personRepo.AddNewPerson(name);
            statusLabel.Text = _personRepo.StatusMessage;
            nameEntry.Text = "";
        }
        else
        {
            statusLabel.Text = "Введите имя!";
        }
    }

    private void OnGetAllPeopleClicked(object sender, EventArgs e)
    {
        List<Person> people = _personRepo.GetAllPeople();
        peopleListView.ItemsSource = people;
        statusLabel.Text = $"Найдено {people.Count} записей";
    }
}
