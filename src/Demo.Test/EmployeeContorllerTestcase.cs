using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Demo.Models.Poco;
using NUnit.Framework;

namespace Demo.Test {
    internal static class EmployeeContorllerTestcase {

        private const string category = nameof(EmployeeServiceTestcase);

        internal static IEnumerable Get_One_Employee_Testcase {
            get {
                var guid = Guid.NewGuid().ToString();
                Employee item = null;
                yield return new TestCaseData(guid, item).SetName("取得一位員工的資料回傳NULL").SetCategory(category);
                yield return new TestCaseData(guid, new Employee {
                    guid = Guid.NewGuid().ToString(),
                    LastName = "Andy",
                    FirstName = "Wei",
                    Country = "Taiwan",
                    Email = "abc@gmail.com",
                    Title = "RD",
                    Isleave = false,
                    CreateAt = DateTime.Now
                }).SetName("取得一位員工的資料").SetCategory(category);
            }
        }

        internal static IEnumerable Add_Employee_Testcase {
            get {
                yield return new TestCaseData(true, new List<Employee>())
                    .SetName("新增員工失敗，應該要回傳 200 OK").SetCategory(category);
                yield return new TestCaseData(true, new List<Employee> {
                        new Employee {
                            guid = Guid.NewGuid().ToString(),
                            LastName = "Andy",
                            FirstName = "Wei",
                            Country = "Taiwan",
                            Email = "abc@gmail.com",
                            Title = "RD",
                            Isleave = false,
                            CreateAt = DateTime.Now
                        },
                        new Employee {
                            guid = Guid.NewGuid().ToString(),
                            LastName = "Jack",
                            FirstName = "Chen",
                            Country = "Taiwan",
                            Email = "ddc@gmail.com",
                            Title = "Manager",
                            Isleave = false,
                            CreateAt = DateTime.Now
                        }
                    }                    
                ).SetName("新增員工成功，應該要回傳 200 OK").SetCategory(category);
            }
        }

        internal static IEnumerable Update_Employee_Testcase {
            get {
                var item = new Employee {
                    guid = Guid.NewGuid().ToString(),
                    LastName = "Andy",
                    FirstName = "Wei",
                    Country = "Taiwan",
                    Email = "abc@gmail.com",
                    Title = "RD",
                    Isleave = false,
                    CreateAt = DateTime.Now
                };
                yield return new TestCaseData(true, item)
                    .SetName("更新員工失敗，應該要回傳 200 OK").SetCategory(category);
                yield return new TestCaseData(true, item)
                    .SetName("更新員工成功，應該要回傳 200 OK").SetCategory(category);
            }
        }

    }
}
