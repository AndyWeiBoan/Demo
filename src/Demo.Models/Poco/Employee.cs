using System;

namespace Demo.Models.Poco {
    public class Employee {
        public string guid { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string Title { get; set; }

        public DateTime CreateAt { get; set; }

        public bool Isleave { get; set; }
    }
}
