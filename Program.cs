using System.Text.RegularExpressions;

namespace FinalProject
    //Author: Demetrius Richards
    //Purpose: Final Project
    //Course: COMP-003A-L01
{
    class PatientIntakeForm
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our New Patient Intake Form\nPlease fill out the upcoming instructions");
            try
            {
                //Here we will collect the users input for thier First name and all the other things
                string firstName = InputStringWithValidation("Enter your First Name (Alphabets only): ", @"^[a-zA-Z]+$");
                string lastName = InputStringWithValidation("Enter your Last Name (Alphabets only): ", @"^[a-zA-Z]+$");
                int birthYear = InputIntWithValidation("Enter your Birth Year (1900-2024): ", 1900, 2024);
                string gender = InputStringWithValidation("Enter your Gender (M/F/O); ", @"^[MFOmfo]$", true);

                //here will be the questionnaire
                List<string> questions = GenerateQuestions();
                List<string> responses = new List<string>();

                //Collect the responses with loops
                Console.WriteLine("\nPlease answer the upcoming questions:");
                foreach (var question in questions)
                {
                    string response = CollectResponse(question);
                    responses.Add(response);
                }
                // here we calculate age and convert gender to full desctription
                int age = CalculateAge(birthYear);
                string fullGender = ConvertGender(gender);

                //here we display profile summary and questionnaire responses
                DisplayProfileSummary(firstName, lastName, age, fullGender);
                DisplayResponses(questions, responses);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
        //here are the input methods with validation
        private static string InputStringWithValidation (string prompt, string pattern, bool toUpperCase = false)
        {
            while(true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (! string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern))
                {
                    return toUpperCase ? input.ToUpper() : input;
                }Console.WriteLine("Invalid input. Try again");
                
            }
        }

        private static int InputIntWithValidation(string prompt, int min, int max)
        {
            while(true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int input) && input >= min && input <= max)
                {
                    return input; 
                }Console.WriteLine($"Invalid input. Enter a value between {min} and {max}");
                

            }
        }

        // here we have something that will  make questions for our questionnaire
        private static List<string> GenerateQuestions()
        {
            return new List<string>
            {
                "Are you experiencing any symptoms as of right now?",
                "Have you had any surgeries in the past year?",
                "Have you been diagnosed with any chronic illnesses?",
                "Are you currently taking any type of medication?",
                "Do you have any type of allergies?",
                "Have you been hospitalized in the past year?",
                "Do you drink alcohol?",
                "Do you smoke?",
                "How often do you excercise?",
                "Does your family have a history for any major illnesses?",
            };
        }

        // here we will collect a response with validation to make sure it is not empty
        private static string CollectResponse(string question)
        {
            Console.WriteLine(question); string response; do
            {
                response = Console.ReadLine();
                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("Response cannot be null or empty. Please retry your response.");
                }
            } while (string.IsNullOrEmpty(response));
            return response;
        }

        //calculating age
        private static int CalculateAge(int birthYear)
        {
            return DateTime.Now.Year - birthYear;
        }

        //converting gender input to the full description
        private static string ConvertGender(string gender)
        {
            switch (gender.ToUpper())
            {
                case "M": return "Male";
                case "F": return "Female";
                case "O": return "Other not listed";
                default: return "Unknown";
            }
        }

        //Display the users profile summary
        private static void DisplayProfileSummary(string firstName, string lastName, int age, string gender)
        {
            Console.WriteLine("\nProfile Summary:");
            Console.WriteLine($"Full Name: {firstName} {lastName}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Gender: {gender}");

            
        }

        private static void DisplayResponses(List<string> questions, List<string> responses)
        {
            Console.WriteLine("\nQuestionnaire Responses:");
            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {questions[i]}");
                Console.WriteLine($"Response: {responses[i]}\n");
                
            }
        }

        
        
    }
}
