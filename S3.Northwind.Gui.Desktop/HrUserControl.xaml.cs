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
            string firstName = textBoxFirstName.Text;
            selectedEmployee.FirstName = firstName;
            // TODO lave resten

            Repository repository = new Repository();
            repository.Update(selectedEmployee);

            DisplayAllEmployees();
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = employeeDataGrid.SelectedItem as Employee;
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

            // TODO lav resten af relevante props på employee.
        }
    }
}
