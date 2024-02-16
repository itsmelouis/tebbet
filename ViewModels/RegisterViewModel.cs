using Avalonia.Controls;
using BCrypt.Net;
using DynamicData;
using DynamicData.Aggregation;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tebbet.Database;
using Tebbet.Models;

namespace Tebbet.ViewModels;
public class RegisterViewModel : ViewModelBase, INotifyPropertyChanged
{
    private ObservableCollection<string> _ListErrors;
    public ObservableCollection<string> ListErrors
    {
        get => _ListErrors;
        set
        {
            if (_ListErrors != value)
            {
                _ListErrors = value;
                this.RaisePropertyChanged(nameof(ListErrors));
            }
        }
    }

    public RegisterViewModel()
    {
        ListErrors = [];
    }
    public bool IsBirthdateValid(string value)
    {
        int date = DateTime.Now.Year - 18;
        string year = date.ToString();
        char year_decimal = year[2];
        char year_unit = year[3];
        string regex = $@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/(19[0-9][0-9]|20[0-{year_decimal}][0-{year_unit}])$";

        if (!Regex.IsMatch(value, regex))
        {
            AddError("Format de date non valide. Format: jj/mm/aaaa");
            return false;
        }
        else
        {
            RemoveError("Format de date non valide. Format: jj/mm/aaaa");
        }
        
        return true;
    }

    public bool IsMailValid(string value)
    {
        string regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (!Regex.IsMatch(value, regex))
        {
            AddError("Format d'email non valide. Format: exemple@mail.com");
            return false;   
        }
        else
        {
            RemoveError("Format d'email non valide. Format: exemple@mail.com");
        }

        return true;
    }

    public bool IsPostalCodeValid(string value) 
    {
        string regex = @"^\d{5}$";
        if (!Regex.IsMatch(value, regex))
        {
            AddError("Format du code postal non valide. Format: 62543");
            return false;
        }
        else
        {
            RemoveError("Format du code postal non valide. Format: 62543");
        }

        return true;
    }

    public void VerifyRegister(string[] formData)
    {
        if (IsFirstAndLastNameValid(formData[0], formData[1]) == true && IsMailValid(formData[2]) == true && IsPostalCodeValid(formData[4]) == true && IsBirthdateValid(formData[6]) == true && IsPasswordValid(formData[7], formData[8]) == true)
        {
            RegiserUser(formData);
        }
    }

    private void RegiserUser(string[] formData)
    {
        CultureInfo cultureinfo = new CultureInfo("fr-FR");
        DateTime datetime = DateTime.Parse(formData[6], cultureinfo).ToUniversalTime();
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(formData[7]);
        Users user = new() {Lastname = formData[0], Firstname = formData[1], Email = formData[2], Address = formData[3], Postalcode = formData[4], City = formData[5], Birthdate = datetime, Password = passwordHash, Role = "user", Credits=100.00 };
        if (user != null)
        {
            DatabaseConnection db = new();
            db.Add<Users>(user);
            db.SaveChanges();
        }
        else 
        {
            AddError("L'email utilisé existe déjà.");
        }
    }

    private bool IsPasswordValid(string password, string confirmPassword)
    {
        bool error = false;
        // regex pour une politique de mot de passe suivant ces règles suivantes : 
        // - au moins une majuscule
        // - au moins une minuscule
        // - au moins un chiffre
        // - au moins un caractère spécial (dont voici une liste @$!%*?&_-())
        // - le mot de passe doit au moins faire 8 caractères de longueur minimum
        Regex passwordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*_-]).{8,}$");
        if (!passwordRegex.IsMatch(password))
        {
            AddError("Le mot de passe doit contenir au minimum une majuscule, un chiffre et un caractère spécial.");
            error = true;
        }
        else
        {
            RemoveError("Le mot de passe doit contenir au minimum une majuscule, un chiffre et un caractère spécial.");
        }

        if (password.Length < 8)
        {
            AddError("Le mot de passe doit contenir au minimum 8 caractères.");
            error = true;
        }
        else
        {
            RemoveError("Le mot de passe doit contenir au minimum 8 caractères.");
        }

        if (password != confirmPassword)
        {
            AddError("Le mot de passe et la confirmation du mot de passe ne concorde pas.");
            error = true;
        }
        else
        {
            RemoveError("Le mot de passe et la confirmation du mot de passe ne concorde pas.");
        }

        if (error)
        {
            return false; 
        }

        return true;
    }

    private bool IsFirstAndLastNameValid(string lastName, string firstName) 
    {
        bool error = false;
        if (lastName.Length < 3)
        {
            AddError("Le nom de famille doit contenir au minimum 3 caractères");
            error = true;
        }
        else
        {
            RemoveError("Le nom de famille doit contenir au minimum 3 caractères");
        }
        if (firstName.Length < 3)
        {
            AddError("Le prénom doit contenir au minimum 3 caractères.");
            error = true;
        }
        else
        {
            RemoveError("Le prénom doit contenir au minimum 3 caractères.");
        }
        if (error)
        {
            return false;
        }

        return true;
    }

    private void AddError(string message)
    {
        if (!ListErrors.Contains(message))
        {
            ListErrors.Add(message);
        }
    }

    private void RemoveError(string message)
    {
        if (ListErrors.Contains(message))
        {
            ListErrors.Remove(message);
        }
    }

}
