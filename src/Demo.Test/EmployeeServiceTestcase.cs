using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Demo.Models.Poco;
using NUnit.Framework;

namespace Demo.Test {
    internal static class EmployeeServiceTestcase {

        private const string category = nameof(EmployeeServiceTest);

        internal static IEnumerable Add_One_Employee_Testcase {
            get {
                // expected, rowaffected, employee
                yield return new TestCaseData(false, 0, new Employee { guid = Guid.NewGuid().ToString() })
                    .SetName("新增不完整的員工資料，應該要回傳False").SetCategory(category);

                yield return new TestCaseData(true, 1, new Employee {
                    guid = Guid.NewGuid().ToString(),
                    LastName = "Adny",
                    FirstName = "Wei",
                    Country = "Taiwan",
                    Email = "abc@gmail.com",
                    Title = "RD",
                    Isleave = false,
                    CreateAt = DateTime.Now
                }).SetName("新增完整的員工資料，True").SetCategory(category);
            }
        }

        internal static IEnumerable Add_More_Than_One_Employee_Testcase {
            get {
                yield return new TestCaseData(true, 2, new List<Employee>())
                    .SetName("新增多位員工成功，應該要回傳True").SetCategory(category);

                yield return new TestCaseData(false, 0, new List<Employee>())
                    .SetName("新增多位員工失敗，應該要回傳False").SetCategory(category);
            }
        }

        internal static IEnumerable Del_One_Employee_Testcase {
            get {
                yield return new TestCaseData(false, 0, Guid.NewGuid().ToString())
                    .SetName("刪除一個員工資料失敗，應該要回傳False").SetCategory(category);

                yield return new TestCaseData(true, 1, Guid.NewGuid().ToString())
                    .SetName("刪除一個員工資料成功，應該要回傳True").SetCategory(category);
            }
        }

        internal static IEnumerable Del_All_Employee_Testcase {
            get {
                yield return new TestCaseData(false, 0)
                    .SetName("刪除所有員工資料失敗，應該要回傳False").SetCategory(category);

                yield return new TestCaseData(true, 10)
                    .SetName("刪除所有員工資料成功，應該要回傳True").SetCategory(category);
            }
        }

        internal static IEnumerable Get_All_Employee_Testcase {
            get {
                yield return new TestCaseData(2, new List<Employee> {
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
                }).SetName("取得員工資料總比數為2，應該要正確").SetCategory(category);

            }
        }

        internal static IEnumerable Get_One_Employee_Testcase {
            get {
                var guid = Guid.NewGuid().ToString();
                yield return new TestCaseData(guid, new Employee {
                    guid = guid.ToString(),
                    LastName = "Adny",
                    FirstName = "Wei",
                    Country = "Taiwan",
                    Email = "abc@gmail.com",
                    Title = "RD",
                    Isleave = false,
                    CreateAt = DateTime.Now
                }).SetName("取得一個員工資料成功，回傳物件不應該為NULL").SetCategory(category);
            }
        }

        internal static IEnumerable Update_One_Employee_Testcase {
            get {
                // expected, rowaffected, employee
                yield return new TestCaseData(false, 0, new Employee { guid = Guid.NewGuid().ToString() })
                    .SetName("更新不完整的員工資料，應該要回傳False").SetCategory(category);

                yield return new TestCaseData(true, 1, new Employee {
                    guid = Guid.NewGuid().ToString(),
                    LastName = "Adny",
                    FirstName = "Wei",
                    Country = "Taiwan",
                    Email = "abc@gmail.com",
                    Title = "RD",
                    Isleave = false,
                    CreateAt = DateTime.Now
                }).SetName("更新完整的員工資料，True").SetCategory(category);
            }
        }
    }
}
