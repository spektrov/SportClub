using System;
using GemBox.Document;

namespace SportClub.Miscellaneous
{
    public enum MonthNames
    {
        Января = 1,
        Фервраля,
        Марта,
        Апреля,
        Мая,
        Июня,
        Июля,
        Августа,
        Сентября,
        Октября,
        Ноября,
        Декабря
    }


    class TrainerDocument
    {
        public DocumentModel GenerateTrainerDoc(int id, string surname, string name, DateTime registration, decimal? salary)
        {
            var money = salary == null ? "Не определено контрактом." : salary.ToString() + "грн.";
            var day = registration.Day < 10 ? "0" + registration.Day.ToString() : registration.Day.ToString();
            var month = (MonthNames)registration.Month;

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var document = new DocumentModel();

            document.Sections.Add(
                new Section(document,
                    new Paragraph(document,
                        new Run(document, "Договор № " + id) { CharacterFormat = { Size = 16, Bold = true } },
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new Run(document, "От " + day + " " + month + " " + registration.Year) { CharacterFormat = { Size = 16, Bold = true } }),

                    new Paragraph(document,
                        new Run(document, $"Фитнес клуб «Фитнесс-Харьков» ИП Спектров Д.Е.," +
                                " именуемый в дальнейшем «Заказчик», действующий на основании Устава, с одной Стороны, и ")
                        { CharacterFormat = { Size = 14 } },
                        new Run(document, surname + " " + name) { CharacterFormat = { Size = 14, Bold = true } },
                        new Run(document, " именуемый в дальнейшем Исполнитель, с другой стороны, именуемые в дальнейшем также «Стороны», заключили настоящий договор о нижеследующем.") { CharacterFormat = { Size = 14 } }),

                    new Paragraph(document,
                        new Run(document, "ПРЕДМЕТ ДОГОВОРА") { CharacterFormat = { Size = 16 } }),

                    new Paragraph(document,
                        new Run(document, "Исполнитель обязуется оказать услуги по тренеровке клиентов" +
                            " «Фитнесс-Харьков» в объеме выбранных заказчиком услуг и на условиях, установленных настоящим Договором." +
                            " Заказчик обязуется полностью и в срок оплачивать услуги Исполнителя.")
                        { CharacterFormat = { Size = 14 } },
                        new SpecialCharacter(document, SpecialCharacterType.LineBreak),
                        new SpecialCharacter(document, SpecialCharacterType.LineBreak),
                        new Run(document, "Вознаграждение Исполнителя: " + money) { CharacterFormat = { Size = 14 } }),

                     new Paragraph(document,
                        new SpecialCharacter(document, SpecialCharacterType.LineBreak),
                        new SpecialCharacter(document, SpecialCharacterType.LineBreak),
                        new Run(document, "Подпись Заказчик " + "___________"),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new Run(document, "Подпись Исполнитель " + "___________"))));

            return document;
        }
    }
}
