
        // Товари та послуги
        class Номенклатура
        {
                public Номенклатура()
                {
                    
                }
            
                /* Назва */
                string Назва { get; set; } = "";
            
                /* Код номенклатури */
                string Код { get; set; } = "";
            
                /* Назва номенклатури */
                string НазваПовна { get; set; } = "";
            
                /* Повний опис */
                string Опис { get; set; } = "";
            
                /* Артикул для товарів */
                string Артикул { get; set; } = "";
            
                /* Дата створення */
                DateTime ДатаСтворення { get; set; } = DateTime.MinValue;
            
                /* Залишок */
                decimal Залишок { get; set; } = 0;
            
                /* Доступний */
                bool Доступний { get; set; } = false;
            
        }
        
    