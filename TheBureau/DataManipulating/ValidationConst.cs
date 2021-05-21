namespace TheBureau.Models.DataManipulating
{
    public static class ValidationConst
    {
        public const string NameRegex = @"^[а-яА-Я-]+$";
        public const string EmailRegex = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
        public const string ContactNumberRegex = @"^((\+7|7|8)+([0-9]){10})$";

        public const string FieldCannotBeEmpty = "Поле не может быть пустым";
        public const string EmailLengthExceeded = "Превышена максимальная длина адреса почты (255)";
        public const string IncorrectEmailStructure = "Некорректная структура адреса электронной почты";
        public const string IncorrectNumberStructure = "Некорректный номер телефона: длина должна быть равна 12";
        public const string IncorrectFirstname = "Имя может состоять лишь из букв и знака \"-\"";
        public const string IncorrectSurname = "Фамилия может состоять лишь из букв и знака \"-\"";
        public const string IncorrectPatronymic = "Отчество может состоять лишь из букв и знака \"-\"";
        
            
    }
}