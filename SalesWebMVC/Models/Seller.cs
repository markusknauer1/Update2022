using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        //========================================================================
        [Display(Name = "Nome")]
        [Required(ErrorMessage ="Nome Requirido!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} - o tamanho do nome é invalido!")]
        //[StringLength(60, MinimumLength = 3, ErrorMessage = "{0} - o tamanho do nome deve ser entre {2} e {1}")]
        public string Name { get; set; }

        //========================================================================
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Requirido!")]
        [EmailAddress(ErrorMessage = "Entre com o email válido")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        //========================================================================
        [Display (Name = "Data de Aniversário")]
        [Required(ErrorMessage = "Data Requirida!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        //========================================================================
        [Display(Name = "Salário")]
        [Required(ErrorMessage = "Valor do Salario Requirido!")]
        [Range(100.0, 50000.0, ErrorMessage ="{0} - valor Invalido: o valor deve ser entre {1} e {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        //========================================================================
        [Display(Name = "Departamento")]
        public Department Department{ get; set; }
        //========================================================================
        [Display(Name = "Departamento")]
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string mail, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Mail = mail;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
