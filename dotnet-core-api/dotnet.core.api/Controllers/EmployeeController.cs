using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace dotnet.core.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpPost]
        [Route("serversidepagination")]
        public DataTableResponse Post([FromBody] DataTableParameters dataTableParameters)
        {
            List<Employee> employees = null;
            if(string.IsNullOrWhiteSpace(dataTableParameters.SearchText))
            {
                employees = getEmployees()
                    .OrderBy(x=>x.Id).ToList();
            }
            else if(int.TryParse(dataTableParameters.SearchText, out int searchTextNumber))
            {
                employees = getEmployees()
                    .Where(x=> Convert.ToString(x.Id).Contains(dataTableParameters.SearchText) 
                                || x.Name.Contains(dataTableParameters.SearchText)
                                || x.Email.Contains(dataTableParameters.SearchText)
                                || x.Designation.Contains(dataTableParameters.SearchText))
                    .OrderBy(x=>x.Id).ToList();
            }
            else 
            {
                employees = getEmployees()
                    .Where(x => x.Name.Contains(dataTableParameters.SearchText)
                                || x.Email.Contains(dataTableParameters.SearchText)
                                || x.Designation.Contains(dataTableParameters.SearchText))
                    .OrderBy(x => x.Id).ToList();
            }
           
           return new DataTableResponse()
           {
               TotalRecords = employees.Count,
               PageNumber = dataTableParameters.PageNumber,
               PageSize = dataTableParameters.PageSize,
               Data = employees.Skip(dataTableParameters.PageSize * (dataTableParameters.PageNumber -1)).Take(dataTableParameters.PageSize)
           };
        }
        
        private List<Employee> getEmployees()
        {
            return new List<Employee>()
            {
                new Employee() { Id = 1, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 2, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 3, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 4, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 5, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 6, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 7, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 8, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 9, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 10, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 11, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 12, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 13, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 14, Name = "AAA", Email = "b@abc.com", Designation = "Developer"},
                new Employee() { Id = 15, Name = "AAA", Email = "c@abc.com", Designation = "Developer"},
                new Employee() { Id = 16, Name = "AAA", Email = "d@abc.com", Designation = "Developer"},
                new Employee() { Id = 17, Name = "AAA", Email = "e@abc.com", Designation = "Developer"},
                new Employee() { Id = 18, Name = "AAA", Email = "f@abc.com", Designation = "Developer"},
                new Employee() { Id = 19, Name = "AAA", Email = "g@abc.com", Designation = "Developer"},
                new Employee() { Id = 20, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 21, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 22, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 23, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 24, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 25, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 26, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 27, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 28, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 29, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 30, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 31, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 32, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 33, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 34, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 35, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 36, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 37, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 38, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 39, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 40, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 41, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 42, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 43, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 44, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 45, Name = "AAA", Email = "a@abc.com", Designation = "Developer"},
                new Employee() { Id = 46, Name = "AAA", Email = "a@abc.com", Designation = "Developer"}
            };
        }
    }

    public class DataTableParameters 
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SearchText { get; set; }
    }

    public class DataTableResponse 
    {
        public object Data { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class Employee 
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public string Email { get; set; } 
        public string Designation { get; set; }     
    }
}