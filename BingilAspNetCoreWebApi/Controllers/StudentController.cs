using Microsoft.AspNetCore.Mvc;
using BingilAspNetCoreWebApi.Models;

namespace BingilAspNetCoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        // In-memory storage — resets when the app restarts
        private static StudentInfo _student = new StudentInfo();

        // ---------- GET METHODS ----------

        [HttpGet("fullname")]
        public IActionResult GetFullName()
        {
            try
            {
                return Ok(_student.FullName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("idno")]
        public IActionResult GetIdNo()
        {
            try
            {
                return Ok(_student.IdNo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("program")]
        public IActionResult GetProgram()
        {
            try
            {
                return Ok(_student.Program);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("birthdate")]
        public IActionResult GetBirthDate()
        {
            try
            {
                if (_student.BirthDate == null)
                    return NotFound("Birthdate has not been set.");

                return Ok(_student.BirthDate.Value.ToString("yyyy-MM-dd"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("age")]
        public IActionResult GetAge()
        {
            try
            {
                if (_student.BirthDate == null)
                    return NotFound("Birthdate has not been set, cannot compute age.");

                var today = DateTime.Today;
                var birthDate = _student.BirthDate.Value;
                int age = today.Year - birthDate.Year;

                if (birthDate.Date > today.AddYears(-age))
                    age--;

                return Ok(age);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // ---------- POST METHODS ----------

        [HttpPost("fullname")]
        public IActionResult SetFullName([FromBody] string fullName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fullName))
                    return BadRequest("Full name cannot be empty.");

                _student.FullName = fullName;
                return Ok($"Full name: {fullName}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("idno")]
        public IActionResult SetIdNo([FromBody] string idNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idNo))
                    return BadRequest("ID number cannot be empty.");

                _student.IdNo = idNo;
                return Ok($"ID number: {idNo}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("program")]
        public IActionResult SetProgram([FromBody] string program)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(program))
                    return BadRequest("Program cannot be empty.");

                _student.Program = program;
                return Ok($"Program: {program}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("birthdate")]
        public IActionResult SetBirthDate([FromBody] DateTime birthDate)
        {
            try
            {
                if (birthDate > DateTime.Today)
                    return BadRequest("Birthdate cannot be in the future.");

                _student.BirthDate = birthDate;
                return Ok($"Birthdate: {birthDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
