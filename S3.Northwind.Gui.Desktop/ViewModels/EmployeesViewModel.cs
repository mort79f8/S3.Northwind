using S3.Northwind.DataAccess;
using S3.Northwind.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3.Northwind.Gui.Desktop.ViewModels
{
    class EmployeesViewModel : INotifyPropertyChanged
    {

        private List<Employee> employees;
        private Employee selectedEmployee;
        Repository repository;

        public EmployeesViewModel()
        {
            repository = new Repository();
            GetAllEmployee();
        }

        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
        public List<Employee> Employees
        {
            get
            {             
                return employees;
            }
            set
            {
                employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }
        
        public List<string> Countries
        {
            get => AllCountries();
        }

        public List<Employee> AllBosses
        {
            get
            {
                List<Employee> bosses = new List<Employee>();
                foreach (var employee in Employees)
                {
                    if (employee.Employees1.Count > 0)
                    {
                        bosses.Add(employee);
                    }
                }
                return bosses;
            }
        }

        public void Update(Employee employee)
        {
            
            repository.Update(employee);
            GetAllEmployee();
        }




        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void GetAllEmployee()
        {
            
            employees = repository.GetAllEmployees();
        }

        public List<string> AllCountries()
        {
            List<string> countries = new List<string>();
            foreach (var employee in Employees)
            {
                countries.Add(employee.Country);
            }

            return countries.Distinct().ToList();
        }

        public List<string> RegionBasedOnCountry(string country)
        {
            List<string> regions = new List<string>();
            List<Employee> employeesInCountry = new List<Employee>();
            foreach (var employee in Employees)
            {
                if (employee.Country == country)
                {
                    employeesInCountry.Add(employee);
                }
            }

            foreach (var employee in employeesInCountry)
            {
                regions.Add(employee.Region);
            }
            
            return regions.Distinct().ToList();
        }

        public void AddEmployee(Employee employee)
        {
            repository.Insert(employee);
            Employees = repository.GetAllEmployees();
        }


    }
}
