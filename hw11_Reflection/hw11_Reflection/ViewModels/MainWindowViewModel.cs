using hw11_Reflection.Services;
using ReactiveUI;
using ReflectionAvalonia.Models.User;
using System.Collections.Generic;
using System.ComponentModel;

namespace hw11_Reflection.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _selectedObject;

        public object SelectedObject
        {
            get => _selectedObject;
            set => this.RaiseAndSetIfChanged(ref _selectedObject, value);
        }

        public MainWindowViewModel() 
        {
            var jsonAnswer = HttpRequest.Request("https://jsonplaceholder.typicode.com/users");
            JsonDeserializer<List<User>> jsonDeserializer = new JsonDeserializer<List<User>>();
            var users = jsonDeserializer.Deserialize(jsonAnswer);
            SelectedObject = users[0];
        }
    }
}
