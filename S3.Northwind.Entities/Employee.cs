namespace S3.Northwind.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Employee
    {
        protected string firstname;
        protected string lastname;
        protected string title;
        protected string titleOfCourtesy;
        protected string address;
        protected string city;
        protected string region;
        protected string postalCode;
        protected string country;
        protected string homePhone;
        protected string extension;
        protected string notes;
        protected DateTime birthDay;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Employees1 = new HashSet<Employee>();
            Orders = new HashSet<Order>();
            Territories = new HashSet<Territory>();
        }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName
        {
            get => lastname;
            set
            {
                var validationResult = LastNameValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(LastName));
                }
                else
                {
                    if (value != lastname)
                    {
                        lastname = value;
                    }
                }
            }
        }

        [Required]
        [StringLength(10)]
        public string FirstName
        {
            get => firstname;
            set
            {
                var validationResult = FirstnameValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(FirstName));
                }
                else
                {
                    if(value != firstname)
                    {
                        firstname = value;
                    }
                }
            }
        }

        [StringLength(30)]
        public string Title
        {
            get => title;
            set
            {
                var validationResult = TitleValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(Title));
                }
                else
                {
                    if (value != title)
                    {
                        title = value;
                    }
                }
            }
        }

        [StringLength(25)]
        public string TitleOfCourtesy
        {
            get => titleOfCourtesy;
            set
            {
                var validationResult = TitleOfCourtesyValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(TitleOfCourtesy));
                }
                else
                {
                    if (value != titleOfCourtesy)
                    {
                        titleOfCourtesy = value;
                    }
                }
            }
        }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(60)]
        public string Address
        {
            get => address;
            set
            {
                var validationResult = AddressValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(Address));
                }
                else
                {
                    if (value != address)
                    {
                        address = value;
                    }
                }
            }
        }

        [StringLength(15)]
        public string City
        {
            get => city;
            set
            {
                var validationResult = CityValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(City));
                }
                else
                {
                    if (value != city)
                    {
                        city = value;
                    }
                }
            }
        }

        [StringLength(15)]
        public string Region {
            get => region;
            set
            {
                var validationResult = RegionValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(Region));
                }
                else
                {
                    if (value != region)
                    {
                        region = value;
                    }
                }
            }
        }

        [StringLength(10)]
        public string PostalCode {
            get =>postalCode;
            set
            {
                var validationResult = PostalCodeValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(PostalCode));
                }
                else
                {
                    if (value != postalCode)
                    {
                        postalCode = value;
                    }
                }
            }
        }

        [StringLength(15)]
        public string Country {
            get => country;
            set
            {
                var validationResult = CountryValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(Country));
                }
                else
                {
                    if (value != country)
                    {
                        country = value;
                    }
                }
            }
        }

        [StringLength(24)]
        public string HomePhone {
            get => homePhone;
            set
            {
                var validationResult = HomePhoneValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(HomePhone));
                }
                else
                {
                    if (value != homePhone)
                    {
                        homePhone = value;
                    }
                }
            }
        }

        [StringLength(4)]
        public string Extension {
            get => extension;
            set
            {
                var validationResult = ExtensionValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(Extension));
                }
                else
                {
                    if (value != extension)
                    {
                        extension = value;
                    }
                }
            }
        }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes {
            get => notes;
            set
            {
                var validationResult = NotesValidation(value);
                if (!validationResult.isValid)
                {
                    throw new ArgumentException(validationResult.errorMessage, nameof(Notes));
                }
                else
                {
                    if (value != notes)
                    {
                        notes = value;
                    }
                }
            }
        }

        public int? ReportsTo { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }

        [StringLength(10)]
        public string Initials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees1 { get; set; }

        public virtual Employee Employee1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Territory> Territories { get; set; }


        public static (bool isValid, string errorMessage) FirstnameValidation(string firstname)
        {
            if (firstname.Length > 10)
            {
                return (false, "Fornavnet er for langt, må ikke være mere end 10 bogstaver langt");
            }
            if (firstname.Any(char.IsDigit))
            {
                return (false, "Fornavnet må ikke indeholde tal");
            }
            if (firstname.Length < 2)
            {
                return (false, "Fornavnet skal være på mere end 1 bogstav");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) LastNameValidation(string lastname)
        {
            if (lastname.Length > 20)
            {
                return (false, "Efternavnet er for langt, må ikke være mere end 20 bogstaver langt");
            }
            if (lastname.Any(char.IsDigit))
            {
                return (false, "Efternavnet må ikke indeholde tal");
            }
            if (lastname.Length < 2)
            {
                return (false, "Efternavnet skal være på mere end 1 bogstav");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) TitleValidation(string title)
        {
            if (title.Any(char.IsDigit))
            {
                return (false, "Job titlen må ikke indeholde tal");
            }
            if (title.Length > 30)
            {
                return (false, "Job titlen er for lang, den må ikke være mere end 30 bogstaver lang");
            }
            if (title.Length < 2)
            {
                return (false, "Job titlen er for kort, skal som minimum være 2 bogstaver lang");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) TitleOfCourtesyValidation(string titleOfCourtesy)
        {
            if (titleOfCourtesy.Length > 25)
            {
                return (false, "Titlen er for lang, må ikke være mere 25 bogstaver lang");
            }
            if (titleOfCourtesy.Any(char.IsDigit))
            {
                return (false, "Titlen må ikke indholde tal");
            }
            if (titleOfCourtesy.Length < 2)
            {
                return (false, "Titlen er for kort, skal som minimum være 2 bogstaver lang");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) AddressValidation(string address)
        {
            if (address.Length > 60)
            {
                return (false, "Addressen er for lang, må ikke være mere end 60 bogstaver lang");
            }
            if (address.Length < 2)
            {
                return (false, "Addressen er for kort, skal som minimum være 2 bogstaver lang");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) CityValidation(string city)
        {
            if (city.Length > 15)
            {
                return (false, "Byens navn er for langt, det må ikke være mere end 15 bogstaver langt");
            }

            if (city.Any(char.IsDigit))
            {
                return (false, "Byens navn må ikke indhold tal");
            }
            if (city.Length < 2)
            {
                return (false, "By navnet skal være som minimum være 2 bogstaver lang");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) RegionValidation(string region)
        {
            if (region is null)
            {
                return (true, String.Empty);
            }
            if (region.Length > 15)
            {
                return (false, "Region's navn må ikke været over 15 bogstaver lang");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) PostalCodeValidation(string postalCode)
        {
            if (postalCode.Length > 10)
            {
                return (false, "Postnummeret må ikke være mere end 10 bogstave/tal langt");
            }
            if (postalCode.Length < 2)
            {
                return (false, "Postnummeret skal være som minimum være 2 bogstaver/tal lang");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) CountryValidation(string country)
        {
            if (country.Length > 15)
            {
                return (false, "Lande navnet må ikke være mere end 15 bogstaver langt");
            }
            if (country.Any(char.IsDigit))
            {
                return (false, "Lande navnet må ikke indholde tal");
            }
            if (country.Length < 2)
            {
                return (false, "lande navnet skal være som minimum være 2 bogstaver lang");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) HomePhoneValidation(string homePhone)
        {
            if (homePhone.Length > 24)
            {
                return (false, "Telefon nummeret er for langt, det må ikke være mere end 24 bogstaver/tal langt");
            }
            if (homePhone.Length < 2)
            {
                return (false, "Telefon nummeret skal være som minimum være 2 bogstaver/tal lang");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) ExtensionValidation(string extension)
        {
            if (extension.Length > 4)
            {
                return (false, "Extension er for lang, den må kun være 4 tal lang");
            }
            if (!extension.All(c => char.IsDigit(c)))
            {
                return (false, "Extension skal kun indholde tal");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) NotesValidation(string notes)
        {
            if (notes.Length < 1)
            {
                return (false, "Notes skal indholde nået");
            }

            return (true, String.Empty);
        }

        public static (bool isValid, string errorMessage) BirthDayValidation(DateTime birthDay)
        {           
            if (birthDay.Date > DateTime.Now)
            {
                return (false, "Personen er ikke født endnu, fødsels datoen må ikke være ude i fremtiden");
            }

            return (true, String.Empty);
        }

    }
}
