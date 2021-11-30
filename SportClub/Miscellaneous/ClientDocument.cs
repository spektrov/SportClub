using System;
using GemBox.Document;

namespace SportClub.Miscellaneous
{
    class ClientDocument
    {
        public DocumentModel GenerateClientDoc(int id, string surname, string name, DateTime registration)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var document = new DocumentModel();

            document.Sections.Add(
                new Section(document,
                    new Paragraph(document,
                        new Run(document, "Договор № " + id) { CharacterFormat = { Size = 16, Bold = true } },
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new Run(document, "От " + registration.Day + "." + registration.Month + "." + registration.Year) { CharacterFormat = { Size = 16, Bold = true } }),

                    new Paragraph(document, 
                        new Run(document, $"Фитнес клуб «Фитнесс-Харьков» ИП Спектров Д.Е.," +
                                " именуемый в дальнейшем «Исполнитель», действующий на основании Устава, с одной Стороны, и ") { CharacterFormat = { Size = 14 } },
                        new Run(document, surname + " " + name) { CharacterFormat = { Size = 14, Bold = true } },
                        new Run(document, " именуемый в дальнейшем Заказчик, с другой стороны, именуемые в дальнейшем также «Стороны», заключили настоящий договор о нижеследующем.") { CharacterFormat = { Size = 14 } }),

                    new Paragraph(document, 
                        new Run(document, "ПРЕДМЕТ ДОГОВОРА") { CharacterFormat = { Size = 16} }),

                    new Paragraph(document,
                        new Run (document, "Исполнитель обязуется оказать услуги по организации досуга Заказчика в фитнес клубе" +
                            " «Фитнесс-Харьков» в объеме выбранных заказчиком услуг и на условиях, установленных настоящим Договором." +
                            " Заказчик обязуется полностью и в срок оплачивать услуги Исполнителя.") { CharacterFormat = { Size = 14 } }),

                    new Paragraph(document,
                        new SpecialCharacter(document, SpecialCharacterType.LineBreak),
                        new SpecialCharacter(document, SpecialCharacterType.LineBreak),
                        new Run(document, "Подпись Исполнитель " + "___________"),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new SpecialCharacter(document, SpecialCharacterType.Tab),
                        new Run(document,   "Подпись Клиент " + "___________"))));

            
            return document;
        }
    }
}
