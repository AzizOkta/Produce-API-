using API.Context;
using API.Models;
using API.Models.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
       private readonly MyContext Context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.Context = myContext;
        }


        public int Register(RegisterVM registerVM)
        {
            var EmailExist = IsEmailExist(registerVM);    //ini dari void dibawah
            var PhoneExist = IsPhoneExist(registerVM);    //ini juga
            if (EmailExist == false)    //cek email gak ada
            {
                if (PhoneExist == false) //cek nomor hp gak ada
                {
                    Employee emp = new Employee();

                    var NIK = GetLastNIK() + 1; //ngambil nik terakhir, ini dijalankan
                    var Year = DateTime.Now.Year;
                    emp.NIK = Year + "00" + NIK.ToString();

                    emp.FirstName = registerVM.FirstName;
                    emp.LastName = registerVM.LastName;
                    emp.Phone = registerVM.Phone;
                    emp.BirthDate = registerVM.BirthDate;
                    emp.Salary = registerVM.Salary;
                    emp.Email = registerVM.Email;
                    emp.Gender = (Models.Gender)registerVM.Gender;

                    Context.Employees.Add(emp);
                    Context.SaveChanges();

                    Account acc = new Account();
                    acc.NIK = emp.NIK;
                   
                    acc.Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password);
                    Context.Accounts.Add(acc);
                    Context.SaveChanges();

                    Education ed = new Education();
                    ed.Degree = registerVM.Degree;
                    ed.GPA = registerVM.GPA;
                    ed.University_Id = registerVM.University_Id;
                    Context.Educations.Add(ed);
                    Context.SaveChanges();

                    Profiling pro = new Profiling();
                    pro.NIK = acc.NIK;
                    pro.Education_Id = ed.id;
                    Context.Profilings.Add(pro);
                    Context.SaveChanges();

                    AccountRole AR = new AccountRole();
                    AR.Id_Account = emp.NIK;
                    AR.Id_Role = 1;
                    Context.AccountRoles.Add(AR);
                    Context.SaveChanges();

                    return 1;
                }
                else
                {
                    return 5; //nomor telepon sudah ada
                }
            }
            else if (EmailExist == true && PhoneExist == true)
            {
                return 6; //email dan nomor telepon sudah ada
            }
            else
            {
                return 4; //email sudah ada
            }
            /*var result = context.SaveChanges();
            return result;*/
        }



        public IEnumerable GetRegisteredData()
        {
            var employees = Context.Employees;
            var accounts = Context.Accounts;
            var profilings = Context.Profilings;
            var educations = Context.Educations;
            var universities = Context.Universities;
            var accountrole = Context.AccountRoles;
            var role = Context.Roles;

            var result = (from emp in employees
                          join acc in accounts on emp.NIK equals acc.NIK
                          join ar in accountrole on acc.NIK equals ar.Id_Account
                          join r in role on ar.Id_Role equals r.id
                          join pro in profilings on acc.NIK equals pro.NIK
                          join edu in educations on pro.Education_Id equals edu.id
                          join univ in universities on edu.University_Id equals univ.id

                          select new
                          {
                              FullName = emp.FirstName + " " + emp.LastName,
                              Phone = emp.Phone,
                              BirthDate = emp.BirthDate,
                              Salary = emp.Salary,
                              Email = emp.Email,
                              Degree = edu.Degree,
                              GPA = edu.GPA,
                              UnivName = univ.Name,
                              RoleName = r.nama
                          }).ToList();

            return result;
        }

        public bool IsEmailExist(RegisterVM registerVM)
        {
            var CekEmail = Context.Employees.Where(emp => emp.Email == registerVM.Email).FirstOrDefault(); //0 atau 1 diambil pertama
            if (CekEmail != null)
            {
                return true; //kalau menemukan email sama
            }
            else
            {
                return false; //tidak menemukan email sama
            }
        }
        public bool IsPhoneExist(RegisterVM registerVM)
        {
            var CekPhone = Context.Employees.Where(emp => emp.Phone == registerVM.Phone).FirstOrDefault(); //null atau 1 diambil pertama
            if (CekPhone != null)
            {
                return true; //kalau menemukan nomor hp sama
            }
            else
            {
                return false; //kalau menemukan email sama
            }
        }
        public int GetLastNIK()
        {
            var lastEmp = Context.Employees.OrderByDescending(emp => emp.NIK).FirstOrDefault(); //diurutkan dari nik terakhir
            if (lastEmp == null)
            {
                return 0; //tidak ditemukan atau belum ada nik
            }
            else
            {
                var lastNIK = lastEmp.NIK.Remove(0, 5); //kalau ada nik yang terakhir 
                return int.Parse(lastNIK);
            }
        }






    }

 }
