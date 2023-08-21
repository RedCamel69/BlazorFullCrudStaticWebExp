using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerceStaticWebApp.Shared
{
    public class Everything
    {
        public IEnumerable<Language>? Languages;

        public IEnumerable<Tutor>? Tutors;

        public IEnumerable<Business>? Businesses;
       
        public IEnumerable<Course>? Courses;
    }
}
