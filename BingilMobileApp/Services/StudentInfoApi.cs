using BingilMobileApp.Models;

namespace BingilMobileApp.Services
{
    // The Student Info API of the mobile app.
    //
    // It runs INSIDE the app, so the app works on its own: no BingilAPI, no
    // server, no network needed. It has the same operations as BingilAPI's
    // student API (full name, id no, program, birth date, age).
    public class StudentInfoApi
    {
        private string _fullName = "Sam Ariz G. Bingil";
        private string _idNo = "24-1217";
        private string _program = "Information Technology";   // EDIT: your exact program name
        private DateTime _birthDate = new(2005, 2, 6);          // EDIT if 02-06-05 means June 2

        // Always calculated from the birth date, so it never goes out of date.
        private int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - _birthDate.Year;
                if (_birthDate.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        // ---------- GET ----------
        public Task<StudentInfo> GetFullStudentInfoAsync() => Task.FromResult(new StudentInfo
        {
            FullName = _fullName,
            IdNo = _idNo,
            Program = _program,
            BirthDate = _birthDate.ToString("yyyy-MM-dd"),
            Age = Age
        });

        public Task<string?> GetFullNameAsync() => Task.FromResult<string?>(_fullName);
        public Task<string?> GetIdNoAsync() => Task.FromResult<string?>(_idNo);
        public Task<string?> GetProgramAsync() => Task.FromResult<string?>(_program);
        public Task<string?> GetBirthDateAsync() => Task.FromResult<string?>(_birthDate.ToString("yyyy-MM-dd"));
        public Task<int?> GetAgeAsync() => Task.FromResult<int?>(Age);

        // ---------- SET (returns false if the value isn't valid) ----------
        public Task<bool> SetFullNameAsync(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return Task.FromResult(false);
            _fullName = value.Trim();
            return Task.FromResult(true);
        }

        public Task<bool> SetIdNoAsync(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return Task.FromResult(false);
            _idNo = value.Trim();
            return Task.FromResult(true);
        }

        public Task<bool> SetProgramAsync(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return Task.FromResult(false);
            _program = value.Trim();
            return Task.FromResult(true);
        }

        public Task<bool> SetBirthDateAsync(DateTime value)
        {
            if (value.Date > DateTime.Today) return Task.FromResult(false);
            _birthDate = value.Date;
            return Task.FromResult(true);
        }
    }
}
