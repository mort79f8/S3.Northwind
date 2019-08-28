using S3.Northwind.DataAccess;
using S3.Northwind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S3.Northwind.Gui.Desktop
{
    /// <summary>
    /// Interaction logic for HrUserControl.xaml
    /// </summary>
    public partial class HrUserControl : UserControl
    {

        private Employee selectedEmployee;

        public HrUserControl()
        {
            InitializeComponent();
            DisplayAllEmployees();
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
            selectedEmployee.BirthDate = DateTime.Parse(textBoxBirthday.Text);
            selectedEmployee.HireDate = DateTime.Parse(textBoxHireDate.Text);
            selectedEmployee.Address = textBoxAddress.Text;
            selectedEmployee.City = textBoxCity.Text;
            selectedEmployee.PostalCode = textBoxPostalCode.Text;
            selectedEmployee.Region = textBoxRegion.Text;
            selectedEmployee.Country = textBoxCountry.Text;
            selectedEmployee.HomePhone = textBoxHomePhone.Text;
            // Extension most not be more than 4 char. long.
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
            textBoxBirthday.Text = "";
            textBoxHireDate.Text = "";
            textBoxAddress.Text = "";
            textBoxCity.Text = "";
            textBoxPostalCode.Text = "";
            textBoxRegion.Text = "";
            textBoxCountry.Text = "";
            textBoxHomePhone.Text = "";
            textBoxExtension.Text = "";
            textBoxNotes.Text = "";
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
                textBoxBirthday.Text = selectedEmployee.BirthDate.ToString();
                textBoxHireDate.Text = selectedEmployee.HireDate.ToString();
                textBoxAddress.Text = selectedEmployee.Address;
                textBoxCity.Text = selectedEmployee.City;
                textBoxPostalCode.Text = selectedEmployee.PostalCode;
                textBoxRegion.Text = selectedEmployee.Region;
                textBoxCountry.Text = selectedEmployee.Country;
                textBoxHomePhone.Text = selectedEmployee.HomePhone;
                textBoxExtension.Text = selectedEmployee.Extension;
                textBoxNotes.Text = selectedEmployee.Notes;
            }
        }

        private void textBoxInitials_TextChanged(object sender, TextChangedEventArgs e)
        {
            Repository repository = new Repository();
            List<Employee> employees = repository.GetAllEmployees();
            List<Employee> initialMatch = new List<Employee>();
            if (textBoxInitials.GetLineLength(0) == 4)
            {
                foreach (var employee in employees)
                {
                    if (textBoxInitials.Text.ToLower() == Employee.Initials(employee).ToLower())
                    {
                        initialMatch.Add(employee);
                    }
                }
                employeeDataGrid.ItemsSource = initialMatch;
            }

            if (textBoxInitials.GetLineLength(0) <= 3)
            {
                DisplayAllEmployees();
            }
        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Repository repository = new Repository();
            List<Employee> employees = repository.GetAllEmployees();

        }
    }
}
