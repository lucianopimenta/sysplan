export class Filme{
    constructor(
        public Codigo: number = 0,
        public Catalogo: string = '',
        public Nome: string = '',
        public Ano: number = null,
        public Imagem: string = '',
        public Slogan: string = '',
        public VisaoGeral: string = '',
        public Classificacao: number = null,
        ){
    }
}