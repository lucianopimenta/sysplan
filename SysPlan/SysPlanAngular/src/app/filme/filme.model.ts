export class Filme{
    constructor(
        public Codigo: number = 0,
        public Catalogo: string = null,
        public Nome: string = null,
        public Ano: number = null,
        public Imagem: string = null,
        public Slogan: string = null,
        public VisaoGeral: string = null,
        public Classificacao: number = null,
        ){
    }
}