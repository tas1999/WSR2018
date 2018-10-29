using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSR2018
{

    public enum CardType
    {
        Name,
        Owner,
        Number,
        Month,
        Year,
        CVC
    }
    public enum UserType
    {
        Email,
        Password
    }
    public enum RegUserType
    {
        Email,
        Password,
        Name,
        Surname,
        Photo
    }
    public class Utilities
    {
        /// <summary>
        /// Список поддерживаемых расширений файлов изображений
        /// </summary>
        static List<string> imagePath = new List<string>()
           {
                ".bmp",
                ".png",
                ".gif",
                ".jpg"
           };
        /// <summary>
        /// Проверка полей окна "SponsorARunnerWindow" на удволитворение условию
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="propertyName">Тип значения</param>
        /// <returns></returns>
        static public string CardValidate(string value, CardType propertyName)
        {
            StringBuilder validErrors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(value))
            {
                validErrors.Append("Все поля обязательны для заполнения\n");
            }
            else
            {
                if (propertyName == CardType.Number && value.Length != 16)
                {
                    validErrors.Append("Номер кредитной карты должен быть 16 цифр\n");
                }
                if(propertyName == CardType.CVC && value.Length != 3)
                {
                    validErrors.Append("CVC является кодом безопасности, который должен содержать 3 цифры\n");
                }
            }
            return validErrors.ToString();
        }
        /// <summary>
        /// Проверка полей окна "Login" на удволитворение условию
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="propertyName">Тип значения</param>
        /// <returns></returns>
        static public string UserValidate(string value, UserType propertyName)
        {
            StringBuilder validErrors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(value))
            {
                validErrors.Append("Все поля обязательны для заполнения\n");
            }
            
            return validErrors.ToString();
        }
        /// <summary>
        /// Проверка полейс данными пользователя на удволитворение условию
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="propertyName">Тип значения</param>
        /// <returns></returns>
        static public string RegistrateValidate(string value, RegUserType propertyName)
        {
            StringBuilder validErrors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(value))
            {
                validErrors.Append("Все поля обязательны для заполнения\n");
            }
            else
            {
                if(propertyName == RegUserType.Email)
                {
                    if(value.Where(v => v == '@').ToList().Count != 1)
                    {
                        validErrors.Append("E-mail должен содержать один знак @\n");
                    }
                    if (value.Where(v => v == '.').ToList().Count <1)
                    {
                        validErrors.Append("E-mail адрес должен быть в правильном формате, например, x@x.x\n");
                    }
                    if (string.IsNullOrWhiteSpace(validErrors.ToString()))
                    {
                        if (!ServerController.CheckEmail(value))
                        {
                            validErrors.Append("Данный E-mail адрес уже зарегестрирован\n");
                        }
                    }
                }
                else if(propertyName == RegUserType.Password)
                {
                    if (value.Length < 6)
                    {
                        validErrors.Append("Пароль должен иметь минимум 6 символов\n");
                    }
                    if(value.Where(v => Char.IsDigit(v)).ToList().Count < 1)
                    {
                        validErrors.Append("Пароль должен иметь минимум 1 прописную цифру\n");
                    }
                    if (value.Where(v => Char.IsUpper(v)).ToList().Count < 1)
                    {
                        validErrors.Append("Пароль должен иметь минимум 1 прописную букву\n");
                    }
                    if (value.Where(v => v == '!' || v=='@' || v=='#' || v == '$' || v == '%' || v == '^').ToList().Count < 1 )
                    {
                        validErrors.Append("Пароль должен иметь минимум один из следующих символов: ! @ # $ % ^\n");
                    }
                    if (string.IsNullOrWhiteSpace(validErrors.ToString()))
                    {
                        if (!ServerController.CheckPassword(value))
                        {
                            validErrors.Append("Данный пароль адрес уже зарегестрирован\n");
                        }
                    }
                }
                else if(propertyName == RegUserType.Photo)
                {
                    if(imagePath.Where(i => value.IndexOf(i) > -1).ToList().Count != 1)
                    {
                        validErrors.Append("Ссылка должна указывать на фото\n");
                    }
                }
            }

            return validErrors.ToString();
        }

    }
}
