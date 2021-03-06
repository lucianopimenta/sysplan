﻿using SysPlan.Core.Entity;

namespace SysPlan.Data.Entities
{
    public class Filme: EntityBase
    {
        public string Catalogo { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public string Imagem { get; set; }
        public string Slogan { get; set; }
        public string VisaoGeral { get; set; }
        public int Classificacao { get; set; }
    }
}
