using S3.Northwind.DataAccess;
using S3.Northwind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace S3.Northwind.Gui.Desktop
{
    /// <summary>
    /// Interaction logic for HrUserControl.xaml
    /// </summary>
    public partial class HrUserControl : UserControl
    {

        private Employee selectedEmployee;
        private List<Employee> employees;

        public HrUserControl()
        {
            InitializeComponent();
            DisplayAllEmployees();            
            buttonUpdate.IsEnabled = false;

            Repository repository = new Repository();
            employees = repository.GetAllEmployees();

        }

        public void DisplayAllEmployees()
        {
            Repository repository = new Repository();
            List<Employee> employees = repository.GetAllEmployees();
            employeeDataGrid.ItemsSource = employees;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {

            selectedEmployee.FirstName = textBoxFirstName.Text;
            selectedEmployee.LastName = textBoxLastName.Text;
            selectedEmployee.Title = textBoxJob.Text;
            selectedEmployee.TitleOfCourtesy = textBoxTitle.Text;
            selectedEmployee.BirthDate = DateTime.Parse(datePickerBirthday.Text);
            selectedEmployee.HireDate = DateTime.Parse(datePickerHireDate.Text);
            selectedEmployee.Address = textBoxAddress.Text;
            selectedEmployee.City = textBoxCity.Text;
            selectedEmployee.PostalCode = textBoxPostalCode.Text;
            selectedEmployee.Region = textBoxRegion.Text;
            selectedEmployee.Country = textBoxCountry.Text;
            selectedEmployee.HomePhone = textBoxHomePhone.Text;
            selectedEmployee.Extension = textBoxExtension.Text;
            selectedEmployee.Notes = textBoxNotes.Text;
           
            Repository repository = new Repository();
            repository.Update(selectedEmployee);


            DisplayAllEmployees();



            // clearing the textbox'
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxJob.Text = "";
            textBoxTitle.Text = "";
            datePickerBirthday.Text = "";
            datePickerHireDate.Text = "";
            textBoxAddress.Text = "";
            textBoxCity.Text = "";
            textBoxPostalCode.Text = "";
            textBoxRegion.Text = "";
            textBoxCountry.Text = "";
            textBoxHomePhone.Text = "";
            textBoxExtension.Text = "";
            textBoxNotes.Text = "";
            textBoxReportTo.Text = "";

            //clearing searchfields
            textBoxName.Text = "";
            textBoxInitials.Text = "";
            comboBoxCountry.Text = "";

            buttonUpdate.IsEnabled = false;
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = employeeDataGrid.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                textBoxFirstName.Text = selectedEmployee.FirstName;
                textBoxLastName.Text = selectedEmployee.LastName;
                textBoxJob.Text = selectedEmployee.Title;
                textBoxTitle.Text = selectedEmployee.TitleOfCourtesy;
                datePickerBirthday.Text = selectedEmployee.BirthDate.ToString();
                datePickerHireDate.Text = selectedEmployee.HireDate.ToString();
                textBoxAddress.Text = selectedEmployee.Address;
                textBoxCity.Text = selectedEmployee.City;
                textBoxPostalCode.Text = selectedEmployee.PostalCode;
                textBoxRegion.Text = selectedEmployee.Region;
                textBoxCountry.Text = selectedEmployee.Country;
                textBoxHomePhone.Text = selectedEmployee.HomePhone;
                textBoxExtension.Text = selectedEmployee.Extension;
                textBoxNotes.Text = selectedEmployee.Notes;
                textBoxReportTo.Text = selectedEmployee.ReportsTo.ToString();
            }
            buttonUpdate.IsEnabled = true;
        }

        private void textBoxInitials_TextChanged(object sender, TextChangedEventArgs e)
        {
            employeeDataGrid.ItemsSource = employees.Where(em => em.Initials.ToLower().Contains(textBoxInitials.Text.ToLower()));
        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            employeeDataGrid.ItemsSource = employees.Where(em => (em.FirstName + " " + em.LastName).ToLower().Contains(textBoxName.Text.ToLower()));
        }

        private void ComboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employeeDataGrid.ItemsSource = employees.Where(em => em.Country.Trim() == comboBoxCountry.Text);
        }
    }
}
