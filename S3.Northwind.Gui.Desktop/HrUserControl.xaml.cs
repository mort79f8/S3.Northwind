using S3.Northwind.DataAccess;
using S3.Northwind.Entities;
using S3.Northwind.Gui.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            
        }

        // methods for making sure only numbers are typed into any textbox which uses the method.
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {


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
            //textBoxReportTo.Text = "";

            //clearing searchfields
            textBoxName.Text = "";
            textBoxInitials.Text = "";
            comboBoxCountry.Text = "";

            buttonUpdate.IsEnabled = false;
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employeesViewModel.SelectedEmployee = employeeDataGrid.SelectedItem as Employee;
            buttonUpdate.IsEnabled = true;
        }


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
    }
}
