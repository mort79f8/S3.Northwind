using S3.Northwind.DataAccess;
using S3.Northwind.Entities;
using S3.Northwind.Gui.Desktop.ViewModels;
using S3.Northwind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace S3.Northwind.Gui.Desktop
{
    /// <summary>
    /// Interaction logic for HrUserControl.xaml
    /// </summary>
    public partial class HrUserControl : UserControl
    {

        private EmployeesViewModel employeesViewModel;

        public HrUserControl()
        {
            InitializeComponent();
            employeesViewModel = new EmployeesViewModel();
            DataContext = employeesViewModel;         
            buttonUpdate.IsEnabled = false;
            //employeesViewModel.SelectedEmployee = new Employee();
            datePickerBirthday.DisplayDateEnd = DateTime.Today;
            comboBoxAllBosses.ItemsSource = employeesViewModel.AllBosses;
        }

        // methods for making sure only numbers are typed into any textbox which uses the method.
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }



        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumberService phoneNumberService = new PhoneNumberService();
            phoneNumberService.PhoneNumber = employeesViewModel.SelectedEmployee.HomePhone;
            if (phoneNumberService.PhoneNumberReturnedFromApiCall().Valid)
            {
                employeesViewModel.SelectedEmployee = new Employee();
                buttonUpdate.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("ikke valid telefon nummer");
                //textBoxHomePhone.BorderBrush = Brushes.Red;
            }
                                  
            
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employeesViewModel.SelectedEmployee = employeeDataGrid.SelectedItem as Employee;
            comboBoxAllBosses.SelectedItem = employeesViewModel.Employees.SingleOrDefault(em => em.EmployeeID == employeesViewModel.SelectedEmployee.ReportsTo);
            buttonUpdate.IsEnabled = true;
        }

        #region Search Related methods
        private void textBoxInitials_TextChanged(object sender, TextChangedEventArgs e)
        {
            employeeDataGrid.ItemsSource = employeesViewModel.Employees.Where(em => em.Initials.ToLower().Contains(textBoxInitials.Text.ToLower()));
        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            employeeDataGrid.ItemsSource = employeesViewModel.Employees.Where(em => (em.FirstName + " " + em.LastName).ToLower().Contains(textBoxName.Text.ToLower()));
        }

        private void ComboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCountry.SelectedValue != null)
            {
                employeeDataGrid.ItemsSource = employeesViewModel.Employees.Where(em => em.Country == comboBoxCountry.SelectedValue.ToString());
                comboBoxRegion.ItemsSource = employeesViewModel.RegionBasedOnCountry(comboBoxCountry.SelectedValue.ToString());
            }

        }

        private void ComboBoxRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxRegion.SelectedValue != null)
            {
                employeeDataGrid.ItemsSource = employeesViewModel.Employees.Where(em => em.Region == comboBoxRegion.SelectedValue.ToString());
            }
        }
        
        private void ButtonResetSearch_Click(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = "";
            textBoxInitials.Text = "";
            comboBoxCountry.SelectedValue = "";
            comboBoxRegion.SelectedValue = "";
            employeeDataGrid.ItemsSource = employeesViewModel.Employees;
        }
        #endregion

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var isInputValid = InputValidation();
            if (!isInputValid.allInputsAreValid)
            {
                MessageBox.Show(isInputValid.errorMessages[0]);
            }
            else
            {
        
                Employee newEmployee = new Employee()
                {
                    FirstName = textBoxFirstName.Text,
                    LastName = textBoxLastName.Text,
                    Title = textBoxJob.Text,
                    TitleOfCourtesy = textBoxTitle.Text,
                    Address = textBoxAddress.Text,
                    City = textBoxCity.Text,
                    Region = textBoxRegion.Text,
                    PostalCode = textBoxPostalCode.Text,
                    Country = textBoxCountry.Text,
                    HomePhone = textBoxHomePhone.Text,
                    Extension = textBoxExtension.Text,
                    Notes = textBoxNotes.Text,
                    BirthDate = datePickerBirthday.SelectedDate.Value,
                    HireDate = datePickerHireDate.SelectedDate.Value

                };

                if (comboBoxAllBosses.SelectedItem != null)
                {
                    Employee reportsTo = comboBoxAllBosses.SelectedItem as Employee;
                    reportsTo.Employees1.Add(newEmployee);
                    newEmployee.ReportsTo = reportsTo.EmployeeID;
                    newEmployee.Employee1 = reportsTo;

                }

                employeesViewModel.AddEmployee(newEmployee);

                MessageBox.Show("ansat tilføjet til DB");


            }
        }
        
        private (bool allInputsAreValid, List<string> errorMessages,  List<Control> controlsWithError) InputValidation()
        {
            List<string> messages = new List<string>();
            List<Control> controls = new List<Control>();
            bool allInputValid = true;
            string firstNameInput = textBoxFirstName.Text;
            string lastNameInput = textBoxLastName.Text;
            string titleInput = textBoxJob.Text;
            string titleOfCourtesyInput = textBoxTitle.Text;
            string addressInput = textBoxAddress.Text;
            string cityInput = textBoxCity.Text;
            string regionInput = textBoxRegion.Text;
            string postalCodeInput = textBoxPostalCode.Text;
            string countryInput = textBoxCountry.Text;
            string homePhoneInput = textBoxHomePhone.Text;
            string extensionInput = textBoxExtension.Text;
            string notesInput = textBoxNotes.Text;
            DateTime birthDayInput = default;
            if (datePickerBirthday.SelectedDate.HasValue)
            {
                birthDayInput = datePickerBirthday.SelectedDate.Value;

            }

            var firstNameValidationResult = Employee.FirstnameValidation(firstNameInput);
            var lastNameValidationResult = Employee.LastNameValidation(lastNameInput);
            var titleValidationResult = Employee.TitleValidation(titleInput);
            var titleOfCourtesyValidationResult = Employee.TitleOfCourtesyValidation(titleOfCourtesyInput);
            var addressValidationResult = Employee.AddressValidation(addressInput);
            var cityValidationResult = Employee.CityValidation(cityInput);
            var regionValidationResult = Employee.RegionValidation(regionInput);
            var postalCodeValidationResult = Employee.PostalCodeValidation(postalCodeInput);
            var countryValidationResult = Employee.CountryValidation(countryInput);
            var homePhoneValidationResult = Employee.HomePhoneValidation(homePhoneInput);
            var extensionValidationResult = Employee.ExtensionValidation(extensionInput);
            var notesValidationResult = Employee.NotesValidation(notesInput);
            var birthDayValidationResult = Employee.BirthDayValidation(birthDayInput);
            
            if (!firstNameValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(firstNameValidationResult.errorMessage);
                controls.Add(textBoxFirstName);
            }
            if (!lastNameValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(lastNameValidationResult.errorMessage);
                controls.Add(textBoxLastName);
            }
            if (!titleValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(titleValidationResult.errorMessage);
                controls.Add(textBoxJob);
            }
            if (!titleOfCourtesyValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(titleOfCourtesyValidationResult.errorMessage);
                controls.Add(textBoxTitle);
            }
            if (!addressValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(addressValidationResult.errorMessage);
                controls.Add(textBoxAddress);
            }
            if (!cityValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(cityValidationResult.errorMessage);
                controls.Add(textBoxCity);
            }
            if (!regionValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(regionValidationResult.errorMessage);
                controls.Add(textBoxRegion);
            }
            if (!postalCodeValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(postalCodeValidationResult.errorMessage);
                controls.Add(textBoxPostalCode);
            }
            if (!countryValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(countryValidationResult.errorMessage);
                controls.Add(textBoxCountry);
            }
            if (!homePhoneValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(homePhoneValidationResult.errorMessage);
                controls.Add(textBoxFirstName);
            }
            if (!extensionValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(extensionValidationResult.errorMessage);
                controls.Add(textBoxExtension);
            }
            if (!notesValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(notesValidationResult.errorMessage);
                controls.Add(textBoxNotes);
            }
            if (!birthDayValidationResult.isValid)
            {
                allInputValid &= false; // allInputValid = allInputValid && false;
                messages.Add(birthDayValidationResult.errorMessage);
                controls.Add(datePickerBirthday);
            }

            return (allInputValid, messages, controls);
        } 
    }
}
