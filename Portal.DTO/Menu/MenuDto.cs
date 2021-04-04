using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuType { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string IconName { get; set; }
        public string Link { get; set; }
        public List<MenuDto> Childs { get; set; }
            
    }
}
