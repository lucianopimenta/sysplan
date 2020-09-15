export class Result{
    constructor(
        public success: boolean = null,
        public errorMessage: string = null,
        public data: object = null){
    }
}