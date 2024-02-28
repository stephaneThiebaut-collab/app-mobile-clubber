using Clubber.Models.User;
using System.Text;
using System.Text.Json;

namespace Clubber
{
    public partial class MainPage : ContentPage
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        bool FormInscriptionIsVisible = false;
        bool FormConnexionIsVisible = false;
        public MainPage()
        {
            InitializeComponent();
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public void ShowInscriptionForm(object sender, EventArgs e)
        {
            FormInscriptionVisible.IsVisible = FormInscriptionIsVisible;
            FormInscriptionIsVisible = !FormInscriptionIsVisible;
        }

        public void ShowConnexionForm(object sender, EventArgs e)
        {
            FormConnexionVisible.IsVisible = FormConnexionIsVisible;
            FormConnexionIsVisible = !FormConnexionIsVisible;
        }

        private async void SendConnexionUser(object sender, EventArgs e)
        {
            string Email = EmailEntry.Text;
            string Password = PasswordEntrey.Text;
            Uri uri = new Uri("http://localhost:3000/connexion");

            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                await DisplayAlert("Alert", "OK", "OK");
                List<Models.User.Connexion> Connexion = new List<Models.User.Connexion>();
                Connexion.Add(new Models.User.Connexion
                {
                    Email = EmailEntry.Text,
                    Password = PasswordEntrey.Text
                });
                try
                {
                    string json = JsonSerializer.Serialize(Connexion, _serializerOptions);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _client.PostAsync(uri, content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<Models.ResponseAPI>(responseContent);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Alert", apiResponse.Message, "OK");
                    }
                    else
                    {
                        await DisplayAlert("Alert", apiResponse.Message, "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alert", $"Une erreur est survenue: {ex.ToString()}", "OK");
                }
            } 
            else
            {
                await DisplayAlert("Alert", "Tout les champs sont obligatoire!", "OK");
            }
        }

        private async void SendInscriptionUser(object sender, EventArgs e)
        {
            string Email = EntryEmailInscription.Text;
            string Password = EntryPassword.Text;
            string ConfirmPassword = EntryConfirmPassword.Text;
            string Nom = EntryNom.Text;
            string Prenom = EntryPrenom.Text;


            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword) && !string.IsNullOrEmpty(Nom) && !string.IsNullOrEmpty(Prenom) )
            {
                if (Password == ConfirmPassword)
                {
                    Uri uri = new Uri("http://localhost:3000/inscription");
                    List<Models.User.Inscription> Inscriptions = new List<Models.User.Inscription>();
                    Inscriptions.Add(new Models.User.Inscription
                    {
                        Email = Email,
                        Password = Password,
                        Nom = Nom,
                        Prenom = Prenom 
                    });

                    try
                    {
                        string json = JsonSerializer.Serialize(Inscriptions, _serializerOptions);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await _client.PostAsync(uri, content);
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonSerializer.Deserialize<Models.ResponseAPI>(responseContent);

                        if (response.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Alert", apiResponse.Message, "OK");
                        }
                        else
                        {
                            await DisplayAlert("Alert", apiResponse.Message, "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alert", $"Une erreur est survenue: {ex.ToString()}", "OK");
                    }

                } 
                else
                {
                    await DisplayAlert("Alert", "Les mots de passe ne corresponde pas", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Tout les champs sont obligatoire", "OK");
            }
        }


    }

}
